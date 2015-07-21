//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_AmbientManager.buildBasicCloudsParams();
function SEP_AmbientManager::buildFogParams( %this,%syncAfter ) {
	%arCfg = createParamsArray("SEP_Fog",AMD_FogParamsStack);
	%arCfg.updateFunc = "SEP_AmbientManager.updateFogParam";
	%arCfg.style = "LabCfgB_304";
	%arCfg.useNewSystem = true;

	%arCfg.group[%gid++] = "LevelInfo settings";
	if (isObject(SEP_AmbientManager.scatterSkyObj)){
		%arCfg.setVal("fogScale",       "" TAB "Fog Color" TAB "ColorEdit" TAB "mode>>float;;flen>>2;;noAlpha>>1" TAB "SEP_AmbientManager.scatterSkyObj" TAB %gid);
	}else{
		%arCfg.setVal("fogColor",       "" TAB "Fog Color" TAB "ColorEdit" TAB "mode>>float;;flen>>2;;noAlpha>>1" TAB "theLevelInfo" TAB %gid);
	}
	
	
	%arCfg.setVal("fogDensity",       "" TAB "fogDensity" TAB "SliderEdit" TAB "range>>0 0.03" TAB "theLevelInfo" TAB %gid);
	%arCfg.setVal("fogDensityOffset",       "" TAB "fogDensityOffset" TAB "SliderEdit" TAB "range>>0 100" TAB "theLevelInfo" TAB %gid);
	%arCfg.setVal("fogAtmosphereHeight",       "" TAB "fogAtmosphereHeight" TAB "SliderEdit" TAB "range>>0 1000" TAB "theLevelInfo" TAB %gid);
	
	
	SEP_AmbientManager.setFogDirty(false);
	buildParamsArray(%arCfg,%syncAfter);
	%this.FogParamArray = %arCfg;
}
//------------------------------------------------------------------------------

//==============================================================================
function SEP_AmbientManager::updateFogParam(%this,%field,%value,%ctrl,%array,%arg1,%arg2) {
	devLog("SEP_AmbientManager::updateFogParam(%this,%field,%value,%ctrl,%array,%arg1,%arg2)",%this,%field,%value,%ctrl,%array,%arg1,%arg2);

	%arrayValue = %array.getVal(%field);
	devLog(%field," Fog Array value=",%arrayValue);
	%obj = getField(%arrayValue,4);
	devLog("Linked object is:",%obj);
	%this.updateFogField(%field,%value,%obj);
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function SEP_AmbientManager::updateFogField( %this,%field, %value,%obj ) { 
	devLog("SEP_AmbientManager::updateFogField( %this,%field, %value,%obj )",%this,%field, %value,%obj );	
	
	
	if (!isObject(%obj)){
		eval("%obj = "@%obj@";");
		if (!isObject(%obj)){
			warnLog("Invalid object for Fog field:",%field,"Object:",%obj);
 			return;
		}
	}
	SceneInspector.inspect(%obj);
	%currentValue = %obj.getFieldValue(%field);
	if (%currentValue $= %value) {		
		return;
	}
	SceneInspector.apply(%obj);
	%obj.setFieldValue(%field,%value,%layerId);
	EWorldEditor.isDirty = true;
	%this.setFogDirty(true,%obj);  
	
	//syncParamArray(SEP_AmbientManager.FogParamArray);
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function SEP_AmbientManager::setFogDirty( %this,%isDirty,%obj ) { 
	
	if (isObject(%obj))
		%this.setObjectDirty(%isDirty,%obj);
		
	if (!%isDirty)
		%this.dirtyFogObjects = "";
	else	
		%this.dirtyFogObjects = strAddWord(%this.dirtyFogObjects,%obj.getId(),true);
	
	devLog("Dirty fog objects list = ",%this.dirtyFogObjects);
	AMD_FogSaveButton.active = %isDirty;
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function SEP_AmbientManager::saveFogData( %this,%isDirty,%obj ) { 
	
	if (isObject(%obj))
		%this.setObjectDirty(%isDirty,%obj);
	
	foreach$(%obj in %this.dirtyFogObjects)
		SEP_AmbientManager_PM.saveDirtyObject(%obj);	
	
	%this.setFogDirty(false);

}
//------------------------------------------------------------------------------