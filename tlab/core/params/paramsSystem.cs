//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function Lab::initParamsSystem( %this ) {
	$LabParams = newScriptObject("LabParams");
	$LabParamsGroup = newSimGroup("LabParamsGroup");
}
//------------------------------------------------------------------------------


//==============================================================================
function LabParams::syncArray( %this,%paramArray,%syncTarget ) {
	for( ; %i < %paramArray.count() ; %i++) {
		%field = %paramArray.getKey(%i);
		%data = %paramArray.getValue(%i);
		%this.syncParamField(%paramArray,%field,%data,%syncTarget);
	}
}
//------------------------------------------------------------------------------


//==============================================================================
// Sync the current profile values into the params objects
function LabParams::syncParamField( %this,%paramArray,%field,%data,%syncTarget ) {
	%cfgObj = %paramArray.cfgObject;
	if (%paramArray.noSyncField[%field] )
		return;

	if (!%paramArray.prefModeOnly){
		if (isObject(%cfgObj)) {
			%value = %cfgObj.getCfg(%field);
		} else {
			return false;
		}

		if (%value $= "")
			paramLog(%paramArray.getName(),"Cfg Value for field:",%field,"Is Blank!");
		
		
	}
	else{
		%value = getParamValue(%paramArray,%field,true);
	}
		
	if (%syncTarget)
		%this.updateParamSyncData(%field,%value,%paramArray);

	%pill = %paramArray.pill[%field];

	if (isObject(%pill)) {
		%pillHolder = %pill.findObjectByInternalName(%field,true);
		%pillHolder.setTypeValue(%value,true);
	} else {
		paramLog(%paramArray.getName(),"Invalid control holder for field:",%field,"Couldn't sync the param!");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function LabParams::setParamPillValue( %this,%field,%value,%paramArray ) {
	%pill = %paramArray.pill[%field];
	
	if (isObject(%pill)) {
		%pillHolder = %pill.findObjectByInternalName(%field,true);
		%pillHolder.setTypeValue(%value,true);
	} else {
		paramLog(%paramArray.getName(),"Invalid control holder for field:",%field,"Couldn't sync the param!");
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function LabParams::updateParamSyncData( %this,%field,%value,%paramArray ) {

	
}

//---------------------------------------------------------------------------

//------------------------------------------------------------------------------

//==============================================================================
// Update the Params after control value changed in ParamsDlg
//==============================================================================

//==============================================================================
function LabParams::updateParamArrayCtrl( %this,%field,%value,%ctrl,%paramArray,%arg1,%arg2 ) {
	
	if (%paramArray.prefModeOnly)
		return;
	%cfgObj = %paramArray.cfgObject;	
	
	if (isObject(%cfgObj)) {
		%cfgObj.setCfg(%field,%value);
	} else {
		return false;
	}

	//%ctrl.updateFriends();
	//%this.updateParamSyncData(%field,%value,%paramArray);
	//%this.setParamPillValue(%field,%value,%paramArray);
}
//------------------------------------------------------------------------------
function LabParams::updateParamFromCtrl( %this,%ctrl,%field,%value,%paramArray ) {
	
	if (!%paramArray.prefModeOnly){		
		%cfgObj = %paramArray.cfgObject;
		if (isObject(%cfgObj)) {
			%cfgObj.setCfg(%field,%value);
		} else {
			return false;
		}
	}
	%ctrl.updateFriends();
	%this.updateParamSyncData(%field,%value,%paramArray);
	%this.setParamPillValue(%field,%value,%paramArray);
}
//------------------------------------------------------------------------------
