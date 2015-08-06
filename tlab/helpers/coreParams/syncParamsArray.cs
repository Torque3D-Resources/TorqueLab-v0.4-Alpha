//==============================================================================
// HelpersLab -> Sync all the ParamArray fields controls with current values
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
//Get the current value from each param
function syncParamArray(%paramArray, %keepEmptyValue) {
	%i = 0;

	for( ; %i < %paramArray.count() ; %i++) {
		%field = %paramArray.getKey(%i);
		%data = %paramArray.getValue(%i);
		%fieldWords = strReplace(%field,"_"," ");
		%fieldClean = getWord(%fieldWords,0);
		%special = getWord(%fieldWords,1);
		//Set Value empty
		%value = "";

		if (!strFind($HLabParams_FriendList,%special) && %paramArray.validateSubField) {
			devLog(%special,"SubField is not in official list and will be ignored");
			%fieldClean = %field;
		}

		%ctrl = %paramArray.container.findObjectByInternalName(%field,true);

		if (!isObject(%ctrl)) {
			warnLog("Couln't find a valid GuiControl holding the value for field:",%field);
			continue;
		}

		//===========================================================================
		// PrefGroup autosyncing will add the field to the prefGroup and store the value inside
		//Ex: PrefGroup: $PrefGroup:: Field: myField  Value: myValue will store myValue in $PrefGroup::myField
		if (%paramArray.autoSyncPref $= "1"||%paramArray.autoSyncPref) {
			%prefGroup = %paramArray.prefGroup;
			eval("%value = "@%prefGroup@%fieldClean@";");

			if (%value !$= "") {
				setParamCtrlValue(%ctrl,%value,%field,%paramArray);
				continue;
			}
		}

		//---------------------------------------------------------------------------
		%syncDataStr = getField(%data,4);

		if (%syncDataStr $= "")
			continue;

		//Try each syncData until we get a value
		foreach$(%data in %syncDataStr) {
			//If data is an object, get the value from it
			if (isObject(%data)) {
				%value = %data.getFieldValue(%field);
			}
			//If data start with $, it might be a full global
			else if (getSubStr(%data,0,1) $= "$") {
				%fix = strreplace(%data,"\"","");
				eval("%value = "@%fix@";");
			}
			// If . found, might be an object field EX: EWorldEditor.stickToGround
			else if (strstr(%data,".") !$= "-1") {
				eval("%value = "@%data@";");
			}

			//If we got a value, get out of here
			if (%value !$= "") {
				paramLog(%field,"Param update from Sync Data:",%data,"Value:",%value);
				break;
			}
		}

		if (%value $= "" && !%keepEmptyValue)
			continue;

		//Set the GuiControl value using the common function
		setParamCtrlValue(%ctrl,%value,%field,%paramArray);
	}
}


//==============================================================================
//Sync all the linked objects with paramArray data
function syncParamArrayLinks(%paramArray) {
	%i = 0;

	for( ; %i < %paramArray.count() ; %i++) {
		%field = %paramArray.getKey(%i);
		%data = %paramArray.getValue(%i);
		%ctrl = %paramArray.container.findObjectByInternalName(%field,true);

		if (!isObject(%ctrl)) {
			warnLog("Couln't find a valid GuiControl holding the value for field:",%field);
			continue;
		}

		//Get the value of the control used to sync linked data
		%value = %ctrl.getTypeValue();

		//===========================================================================
		// PrefGroup autosyncing will add the field to the prefGroup and store the value inside
		//Ex: PrefGroup: $PrefGroup:: Field: myField  Value: myValue will store myValue in $PrefGroup::myField
		if (%paramArray.autoSyncPref $= "1"||%paramArray.autoSyncPref) {
			%prefGroup = %paramArray.prefGroup;
			eval(%prefGroup@%fieldClean@" = %value;");
		}

		//---------------------------------------------------------------------------
		%syncDataStr = getField(%data,4);

		if (%syncDataStr $= "")
			continue;

		//Try each syncData until we get a value
		foreach$(%data in %syncDataStr) {
			//If data is an object, get the value from it
			if (isObject(%data)) {
				%data.setFieldValue(%field,%value);
			}
			//If data start with $, it might be a full global
			else if (getSubStr(%data,0,1) $= "$") {
				%fix = strreplace(%data,"\"","");
				eval(%fix@" = %value;");
			}
			// If . found, might be an object field EX: EWorldEditor.stickToGround
			else if (strstr(%data,".") !$= "-1") {
				eval(%data@" = %value;");
			}
			//Check for a function template by searching for );
			else if (strFind(%syncData,");")){
				//Replace *val* occurance with value 
				if (strFind(%syncData,"*val*")){
					%command = strreplace(%syncData,"*val*","\""@%value@"\"");	 	
					eval(%command);
				}
				//Replace ** occurance with value (Old way)
				else if (strFind(%syncData,"**")){
					%command = strreplace(%syncData,"*val*","\""@%value@"\"");	 	
					eval(%command);
				}
			}
		}
	}
}