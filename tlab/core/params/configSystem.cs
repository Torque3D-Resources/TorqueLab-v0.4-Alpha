//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function Lab::initConfigSystem( %this,%defaults ) {
	//Start by building all the ConfigArray Params
	$LabConfigArrayGroup = newSimGroup("LabConfigArrayGroup");
	exec("tlab/core/commonSettings.cs");
	LabParamsGroup.clear();
	
	

	%this.initCommonParams();
	%this.initAllPluginConfig();
	if (%defaults)
		LabCfg.resetDefaults();
	else
		LabCfg.read();
	Lab.readAllConfigArray(true);
	//FIXME Rebuild params everytime for development but should be optimized later
	LabParamsDlg.rebuildAll();
	
}
//------------------------------------------------------------------------------


//==============================================================================
function Lab::exportCfgPrefs(%this) {
	export("$Cfg_*", "tlab/configPrefs.cs", false);
}
//------------------------------------------------------------------------------


//==============================================================================
// Set params settings group to their default value
//==============================================================================
//==============================================================================
function Lab::syncAllParams(%this) {
	foreach(%array in LabParamsGroup)
		%this.setParamArrayDefaults(%array);
}
//------------------------------------------------------------------------------

//==============================================================================
// Set params settings group to their default value
//==============================================================================
//==============================================================================
function Lab::setAllParamArrayDefaults(%this) {
	foreach(%array in LabParamsGroup)
		%this.setParamArrayDefaults(%array);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setParamArrayDefaults(%this,%array) {
	%pattern = strreplace(%array.groupLink,"_","/");
	LabCfg.beginGroup( %pattern, true );
	%i = 0;

	for( ; %i < %array.count() ; %i++) {
		%field = %array.getKey(%i);
		%data = %array.getValue(%i);
		%default = getWord(%data,0);
		//%value = LabCfg.setCfg( %field,%default,true );
		//$Cfg_[%array.groupLink,%field] = %value;
	}

	LabCfg.endGroup();
}
//------------------------------------------------------------------------------
//==============================================================================
function quick(%field,%value,%default) {
	ConvexEditorPlugin.setCfg(%field,%value,%default);
}
//------------------------------------------------------------------------------
//==============================================================================
// Set params settings group to their default value
//==============================================================================
//==============================================================================
function Lab::writeCurrentParamsConfig(%this) {
	foreach(%array in LabParamsGroup)
		%this.writeParamConfig(%array);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::writeParamConfig(%this,%array) {
	%pattern = strreplace(%array.groupLink,"_","/");
	LabCfg.beginGroup( %pattern, true );
	%i = 0;
	
	for( ; %i < %array.count() ; %i++) {
		%field = %array.getKey(%i);
		%data = %array.getValue(%i);
		%default = getWord(%data,0);	
		
		%value = getParamValue(%array,%field,true);	
		if (	%value $= ""){
			%value = %default;
			if (	%value $= "")
				continue;
			paramLog(%array.getName(),"Field",%field,"Param empty value set to default:",%value,"SuncData",%array.syncData[%field]);
		}
		%array.setCfg( %field,%value,false );
		
		
	
	}

	LabCfg.endGroup();
}
//------------------------------------------------------------------------------
//==============================================================================
// OLD System kept for reference showing how to read SettingObject file
//==============================================================================
function Lab::initAllConfigArray(%this) {
	foreach(%array in LabParamsGroup)
		%this.initConfigArray(%array,%setEmptyToDefault);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::initConfigArray(%this,%array,%setEmptyToDefault) {
	%pattern = strreplace(%array.groupLink,"_","/");
	LabCfg.beginGroup( %pattern, true );
	%i = 0;	
	for( ; %i < %array.count() ; %i++) {
		%field = %array.getKey(%i);
		%value = LabCfg.value( %field );
				
		if (%value $= ""){
			devLog("Converting empty value to empty...",%field);
			continue;
		}
		setParamFieldValue(%array,%field,%value);
		
	}

	LabCfg.endGroup();
	
}
//------------------------------------------------------------------------------
//==============================================================================
// OLD System kept for reference showing how to read SettingObject file
//==============================================================================
function Lab::readAllConfigArray(%this,%setEmptyToDefault) {
	foreach(%array in LabParamsGroup)		
		%this.readConfigArray(%array,%setEmptyToDefault);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::readConfigArray(%this,%array,%setEmptyToDefault) {
	%pattern = strreplace(%array.groupLink,"_","/");
	LabCfg.beginGroup( %pattern, true );
	%i = 0;	
	for( ; %i < %array.count() ; %i++) {
		%field = %array.getKey(%i);
		%value = LabCfg.value( %field );	
		if (%setEmptyToDefault){
			if ($Cfg_[%array.groupLink,%field] $= "")
				$Cfg_[%array.groupLink,%field] = %value;
			if($CfgParams_[%array.groupLink,%field] $= "")
				$CfgParams_[%array.groupLink,%field] = %value;
			continue;
		}
		setParamFieldValue(%array,%field,%value);
		$Cfg_[%array.groupLink,%field] = %value;
	}

	LabCfg.endGroup();
	
}
//------------------------------------------------------------------------------