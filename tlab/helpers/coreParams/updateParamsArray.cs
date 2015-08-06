//==============================================================================
// HelpersLab -> Params System - Update the params gui controls
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//===========================================================================
$HLabParams_FriendList = "slider";
//==============================================================================
/// Default params controls syncing functions. This function is called whenever
/// a params Gui Control is changed. It will do some automated tasks and then it
/// will call the specific update function is setted.
/// %ctrl:	The GuiControl that have been changed
/// %updateFunc: Callback function to be called (Only the part before (...); is
///				needed. The args will be set as follow: (%field,%value,%ctrl,%array,%arg1,%arg2)
/// %array: The Param ArrayObject which hold all the data for the params set
/// %arg1: Optional argument #1 which can be add to the updateFunc
/// %arg2: Optional argument #2 which can be add to the updateFunc
function syncParamArrayCtrl( %ctrl, %updateFunc,%array,%arg1,%arg2) {	
	//===========================================================================
	// Special script for GuiColorPickerCtrl Params
	if (%ctrl.getClassName() $= "GuiColorPickerCtrl") {
		%ctrl.updateCommand = %updateFunc@"(%ctrl.internalName,%color,%ctrl,\""@%array@"\",\""@%arg1@"\",\""@%arg2@"\");";
		%currentColor =   %ctrl.baseColor;		
		if (%ctrl $= ColorBlendSelect){
			devLog("ColorBlendSelect syncCkipped");
			return;
		}
		if (%ctrl.noAlpha)
				%currentColor.a = "1";
		if (%ctrl.isIntColor ) {
			%currentColor =  ColorFloatToInt(%currentColor);
			%callBack = %ctrl@".ColorPickedI";
			%updateCallback = %ctrl@".ColorUpdatedI";
			
			GetColorI( %currentColor, %callback, %ctrl.getRoot(), %updateCallback, %cancelCallback );
		} else {
			
			%callBack = %ctrl@".ColorPicked";
			%updateCallback = %ctrl@".ColorUpdated";
			GetColorF( %currentColor, %callback, %ctrl.getRoot(), %updateCallback, %cancelCallback );
		}	
		return;
	}
	//---------------------------------------------------------------------------
	
	syncParamArrayCtrlData(%ctrl, %updateFunc,%array,%arg1,%arg2);
	return;
	
}
function syncParamArrayCtrlData( %ctrl, %updateFunc,%array,%arg1,%arg2) {
	//devLog("syncParamArrayCtrlData",%ctrl, %updateFunc,%array,%arg1,%arg2);
	//Get the field from the internalName which is always the part before _ if exist
	%field = getWord(strreplace(%ctrl.internalName,"_"," "),0);
	if (strFind(%ctrl.internalName,"_")){
		
		%special = getWord(strreplace(%ctrl.internalName,"_"," "),1); 
		if (!strFind($HLabParams_FriendList,%special) && %array.validateSubField){
			devLog(%special,"SubField is not in official list and will be ignored");
			if (%ctrl.setting !$= "")
				%field = %ctrl.setting;
			else
				%field = %ctrl.internalName;
		}	
	}
	
	
	//If Special Edit set, use the getText instead of global getTypeValue
	if (%special $= "Edit") {
		%value = %ctrl.getText();
		devLog("Special Edit value=",%value,"Else",%ctrl.getTypeValue());
	} else
		%value = %ctrl.getTypeValue();

//===========================================================================
//Check for data that need to be synced with new value
	%data = %array.getVal(%field);
	%syncData = getField(%data,4);	
	
	//Check for a standard global starting with $
	if (getSubStr(%syncData,0,1) $= "$"){
	 	eval(%syncData @" = %value;");
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
//---------------------------------------------------------------------------
	
	//Check if a validation type is specified
	if (%ctrl.validationType !$="") {
		%valType = getWord(%ctrl.validationType,0);
		%valValue = getWord(%ctrl.validationType,1);

		if (%valType $= "flen")
			%value = mFloatLength(%value,%valValue);

		%ctrl.setTypeValue(%value);
	}
	//===========================================================================
	// PrefGroup autosyncing will add the field to the prefGroup and store the value inside
	//Ex: PrefGroup: $PrefGroup:: Field: myField  Value: myValue will store myValue in $PrefGroup::myField
	if (%array.autoSyncPref $= "1"||%array.autoSyncPref) {		
		%prefGroup = %array.prefGroup;
		eval(%prefGroup@%field@" = %value;");		
	}
	//---------------------------------------------------------------------------
	 
	//===========================================================================
	// PrefGroup autosyncing will add the field to the prefGroup and store the value inside
	//Ex: PrefGroup: $PrefGroup:: Field: myField  Value: myValue will store myValue in $PrefGroup::myField
	if (%ctrl.linkSet !$= "") {
		%link = %array.container.findObjectByInternalName(%ctrl.linkSet,true);
		if (isObject(%link)){
			devLog("Updating linkSet obj:",%ctrl.linkSet,"From",%ctrl);
			%link.setTypeValue(%value);
			%link.updateFriends();
		}
		else {
			warnLog("Can't find the linkSet inside the param array:",	%ctrl.linkSet);
		}
	}
	//---------------------------------------------------------------------------
	
	//===========================================================================
	// Call the specific update function is set
	if (%updateFunc !$= "")
		eval(%updateFunc@"(%field,%value,%ctrl,%array,%arg1,%arg2);");
	
	//===========================================================================
	// Unless noFriends is specified, try to update agregated friends
	if (!%ctrl.noFriends)
		%ctrl.updateFriends();
}
//==============================================================================
// Generic updateRenderer method
function updateParamArrayCtrl( %ctrl, %updateFunc,%arg1,%arg2,%arg3) {
   
devLog("ODL updateParamArrayCtrl");
   if (%ctrl.getClassName() $= "GuiColorPickerCtrl"){
 
      %ctrl.updateCommand = %updateFunc@"(%this.internalName,%color,\""@%arg1@"\",\""@%arg2@"\",\""@%arg3@"\");";
      
       %currentColor =   %ctrl.baseColor;
       %callBack = %ctrl@".ColorPicked";
       %updateCallback = %ctrl@".ColorUpdated";      
      
     if (%ctrl.isIntColor)
      	{
      		 %callBack = %ctrl@".ColorPickedI";
      		 %updateCallback = %ctrl@".ColorUpdatedI";       
      		 GetColorI( %currentColor, %callback, %ctrl.getRoot(), %updateCallback, %cancelCallback );
      	}
			else{
				%callBack = %ctrl@".ColorPicked";
      		%updateCallback = %ctrl@".ColorUpdated";       
				 GetColorF( %currentColor, %callback, %ctrl.getRoot(), %updateCallback, %cancelCallback );
			}
       return;
   }
     
      %field = getWord(strreplace(%ctrl.internalName,"_"," "),0);  
    %special = getWord(strreplace(%ctrl.internalName,"_"," "),1);  
    if (%special $= "Edit"){
    	%value = %ctrl.getText();
		devLog("Special Edit value=",%value,"Else",%ctrl.getTypeValue());
    }
    else
     %value = %ctrl.getTypeValue();  
      
  // %field = getWord(strreplace(%ctrl.internalName,"_"," "),0);   
    // %value = %ctrl.getTypeValue();  
   

   if (%updateFunc !$= "")
   eval(%updateFunc@"(%field,%value,%arg1,%arg2,%arg3,%ctrl);");    
   
   if (!%ctrl.noFriends)
   	%ctrl.updateFriends();
   
}
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function setParamCtrlValue(%ctrl,%value,%field,%paramArray) {  
	if (!isObject(%ctrl)){
		warnLog("Couln't find a valid GuiControl holding the value for field:",%field);
		return;
	}
	//===========================================================================
	// Special script for GuiColorPickerCtrl Params
	if (%ctrl.getClassName() $= "GuiColorPickerCtrl" && false) {
		%ctrl.updateCommand = %updateFunc@"(%ctrl.internalName,%color,%ctrl,\""@%array@"\",\""@%arg1@"\",\""@%arg2@"\");";
		
		return;
	}
	//---------------------------------------------------------------------------
	
	%ctrl.setTypeValue(%value); 
	%ctrl.updateFriends(); 
	foreach$(%syncCtrl in %ctrl.syncCtrls)
		%syncCtrl.setTypeValue(%value);    
   
}
//------------------------------------------------------------------------------