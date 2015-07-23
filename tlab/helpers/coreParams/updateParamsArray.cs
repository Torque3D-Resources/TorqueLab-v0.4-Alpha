//==============================================================================
// HelpersLab -> Params System - Update the params gui controls
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//===========================================================================

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

		if (%ctrl.isIntColor) {
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
	
	//Get the field from the internalName which is always the part before _ if exist
	%field = getWord(strreplace(%ctrl.internalName,"_"," "),0);
	//Special field setting for custom use
	%special = getWord(strreplace(%ctrl.internalName,"_"," "),1); 
	
	//If Special Edit set, use the getText instead of global getTypeValue
	if (%special $= "Edit") {
		%value = %ctrl.getText();
		devLog("Special Edit value=",%value,"Else",%ctrl.getTypeValue());
	} else
		%value = %ctrl.getTypeValue();

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
		devLog("Pref:",%prefGroup@%field,"Value",%value);
		%prefGroup = %array.prefGroup;
		eval(%prefGroup@%field@" = %value;");		
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
//------------------------------------------------------------------------------