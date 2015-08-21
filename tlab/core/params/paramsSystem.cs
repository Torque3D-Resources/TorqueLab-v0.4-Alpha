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
	paramLog("LabParams::syncParamField( %this,%paramArray,%field,%data,%syncTarget )",%this,%paramArray,%field,%data,%syncTarget );
	%cfgObj = %paramArray.cfgObject;

	if (!%paramArray.prefModeOnly){
		if (isObject(%cfgObj)) {
			%value = %cfgObj.getCfg(%field);
			paramLog(%cfgObj.getName()@" value from LabCfg is:",%value);
		} else {
			paramLog("Couldn't find a related LabCfb value! Sync operation aborted!");
			return false;
		}

		if (%value $= "")
			paramLog(%paramArray.getName(),"Cfg Value for field:",%field,"Is Blank!");
		
		
	}
	else{
		%value = getParamValue(%paramArray,%field,true);
		paramLog("prefModeOnly",%field,%value);
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
	paramLog("LabParams::updateParamSyncData( %this,%field,%value,%paramArray )",%this,%field,%value,%paramArray );
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
	paramLog("LabParams::updateParamSyncData( %this,%field,%value,%paramArray )",%this,%field,%value,%paramArray );
	
}

//---------------------------------------------------------------------------

//------------------------------------------------------------------------------

//==============================================================================
// Update the Params after control value changed in ParamsDlg
//==============================================================================

//==============================================================================
function LabParams::updateParamArrayCtrl( %this,%field,%value,%ctrl,%paramArray,%arg1,%arg2 ) {
	paramLog("LabParams::updateParamArrayCtrl( %this,%field,%value,%ctrl,%paramArray,%arg1,%arg2 )",%this,%field,%value,%ctrl,%paramArray,%arg1,%arg2 );
	
	if (%paramArray.prefModeOnly)
		return;
	%cfgObj = %paramArray.cfgObject;	
	
	if (isObject(%cfgObj)) {
		%cfgObj.setCfg(%field,%value);
		paramLog(%cfgObj.getName()@" value stored as:",%value);
	} else {
		paramLog("Couldn't find a related LabCfg Object to update! No change to the LabCfg object!");
		return false;
	}

	//%ctrl.updateFriends();
	//%this.updateParamSyncData(%field,%value,%paramArray);
	//%this.setParamPillValue(%field,%value,%paramArray);
}
//------------------------------------------------------------------------------
function LabParams::updateParamFromCtrl( %this,%ctrl,%field,%value,%paramArray ) {
	paramLog("LabParams::updateParamFromCtrl( %this,%ctrl,%field,%value,%paramArray )",%this,%ctrl,%field,%value,%paramArray );
	
	if (!%paramArray.prefModeOnly){		
		%cfgObj = %paramArray.cfgObject;
		if (isObject(%cfgObj)) {
			%cfgObj.setCfg(%field,%value);
			paramLog(%cfgObj.getName()@" value stored as:",%value);
		} else {
			paramLog("Couldn't find a related LabCfg Object to update! No change to the LabCfg object!");
			return false;
		}
	}
	%ctrl.updateFriends();
	%this.updateParamSyncData(%field,%value,%paramArray);
	%this.setParamPillValue(%field,%value,%paramArray);
}
//------------------------------------------------------------------------------
