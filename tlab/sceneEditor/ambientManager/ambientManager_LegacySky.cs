//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_ScatterSkyManager.buildParams();
function SEP_LegacySkyManager::buildParams( %this ) {
	%arCfg = createParamsArray("SEP_LegacySky",SEP_LegacySkyManager);
	%arCfg.updateFunc = "SEP_LegacySkyManager.updateParam";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.group[%gid++] = "Lighting settings" TAB "Stack StackA";
	
	%arCfg.setVal("azimuth",       "" TAB "azimuth" TAB "SliderEdit" TAB "range>>0 360;;tickAt>>0.1" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("elevation",       "" TAB "elevation" TAB "SliderEdit" TAB "range>>0 180;;tickAt>>0.1" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);	
	%arCfg.setVal("color",       "" TAB "color" TAB "ColorSlider" TAB "mode>>float;;flen>>2" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("ambient",       "" TAB "ambient" TAB "ColorSlider" TAB "mode>>float;;flen>>2" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("brightness",       "" TAB "Sun brightness" TAB "SliderEdit" TAB "range>>0 2;;tickAt>>0.01" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("attenuationRatio",       "" TAB "attenuationRatio" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	
	%arCfg.group[%gid++] = "Shadows settings" TAB "Stack StackA";
	%arCfg.setVal("overDarkFactor",       "" TAB "overDarkFactor" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("shadowDistance",       "" TAB "shadowDistance" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("shadowSoftness",       "" TAB "shadowSoftness" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("numSplits",       "" TAB "numSplits" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("logWeight",       "" TAB "logWeight" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("shadowType",       "" TAB "shadowType" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("texSize",       "" TAB "texSize" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("fadeStartDistance",       "" TAB "fadeStartDistance" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("shadowDarkenColor",       "" TAB "shadowDarkenColor" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("castShadows",       "" TAB "castShadows" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("lastSplitTerrainOnly",       "" TAB "lastSplitTerrainOnly" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("includeLightmappedGeometryInShadow",       "" TAB "includeLightmappedGeometryInShadow" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("representedInLightmap",       "" TAB "representedInLightmap" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	
	%arCfg.group[%gid++] = "Fog settings" TAB "Stack StackB";
	%arCfg.setVal("fogColor",       "" TAB "Fog Color" TAB "ColorEdit" TAB "mode>>float;;flen>>2;;noAlpha>>1" TAB "theLevelInfo" TAB %gid);	
	%arCfg.setVal("fogDensity",       "" TAB "fogDensity" TAB "SliderEdit" TAB "range>>0 0.1" TAB "theLevelInfo" TAB %gid);
	%arCfg.setVal("fogDensityOffset",       "" TAB "fogDensityOffset" TAB "SliderEdit" TAB "range>>0 200" TAB "theLevelInfo" TAB %gid);
	%arCfg.setVal("fogAtmosphereHeight",       "" TAB "fogAtmosphereHeight" TAB "SliderEdit" TAB "range>>0 5000" TAB "theLevelInfo" TAB %gid);
	
	%arCfg.group[%gid++] = "Corona and Flare settings" TAB "Stack StackB";
	%arCfg.setVal("coronaEnabled",       "" TAB "coronaEnabled" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("coronaMaterial",       "" TAB "coronaMaterial" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("coronaScale",       "" TAB "coronaScale" TAB "SliderEdit" TAB "range>>0 10;;tickAt>>0.05" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("coronaTint",       "" TAB "coronaTint" TAB "ColorSlider" TAB "mode>>float;;flen>>2" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
	%arCfg.setVal("coronaUseLightColor",       "" TAB "coronaUseLightColor" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);	
	
	%arCfg.setVal("flareType",       "" TAB "flareType" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);
%arCfg.setVal("flareScale",       "" TAB "flareScale" TAB "SliderEdit" TAB "range>>0 10;;tickAt>>0.05" TAB "SEP_LegacySkyManager.selectedSun" TAB %gid);	
	
	%arCfg.group[%gid++] = "SkyBox settings" TAB "Stack StackB;;InternalName SkyBoxParams";
	%arCfg.setVal("Material",       "" TAB "Material" TAB "TextEdit" TAB "" TAB "SEP_LegacySkyManager.selectedSkyBox" TAB %gid);
	%arCfg.setVal("drawBottom",       "" TAB "drawBottom" TAB "Checkbox" TAB "" TAB "SEP_LegacySkyManager.selectedSkyBox" TAB %gid);
	%arCfg.setVal("fogBandHeight",       "" TAB "fogBandHeight" TAB "SliderEdit" TAB "range>>0 500;;tickAt>>0.5" TAB "SEP_LegacySkyManager.selectedSkyBox" TAB %gid);


	buildParamsArray(%arCfg,false);
	SEP_LegacySkyManager.paramArray = %arCfg;
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_LegacySkyManager::updateParam(%this,%field,%value,%ctrl,%array,%arg1,%arg2) {
	devLog("SEP_LegacySkyManager::updateParam(%this,%field,%value,%ctrl,%array,%arg1,%arg2)",%this,%field,%value,%ctrl,%array,%arg1,%arg2);
%arrayValue = %array.getVal(%field);
	%obj = getField(%arrayValue,4);
	devLog("Linked object is:",%obj);
	%this.updateField(%field,%value,%obj);
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function SEP_LegacySkyManager::updateField( %this,%field, %value,%obj ) { 
	devLog("SEP_LegacySkyManager::updateField( %this,%field, %value,%obj )",%this,%field, %value,%obj );	
	
	
	if (!isObject(%obj)){
		eval("%obj = "@%obj@";");
		if (!isObject(%obj)){
			warnLog("Invalid object for Legacy field:",%field,"Object:",%obj);
 			return;
		}
	}
	
	//SceneInspector.inspect(%obj);
	%currentValue = %obj.getFieldValue(%field);
	if (%currentValue $= %value) {		
		return;
	}
	LabObj.set(%obj,%field,%value);
	//SceneInspector.apply(%obj);
	//%obj.setFieldValue(%field,%value,%layerId);
	EWorldEditor.isDirty = true;
	%this.setDirtyObject(%obj,true);  
	
	//syncParamArray(SEP_AmbientManager.FogParamArray);
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_LegacySkyManager::selectSky(%this,%obj) {
	logd("SEP_LegacySkyManager::selectSky(%this,%obj)",%this,%obj);
	
	
	if (!isObject(%obj)) {
		%this.selectedSun = "";
		%this.selectedSunName = "";
		%this.selectedSkyBox = "";
		return;
	}
	%this.selectedSunObj = %obj;
	%this-->legacySkyTitle.text = "Sky type:\c2 Sun+SkyBox \c1-> \c3" @ %obj.getName() @"\c0 Properties";
	%this.selectedSun = %obj;
	%this.selectedSunName = %obj.getName();
	%this.setDirtyObject(%this.selectedSun);
	%skyBoxObj = %this.selectedSun.mySkyBox;
	SEP_LegacySkyProperties-->SkyBoxParams.visible = 0;
	if (isObject(%skyBoxObj)){
		SEP_LegacySkyProperties-->SkyBoxParams.visible = 1;
		%this.selectedSkyBox = %skyBoxObj;
		%this.setDirtyObject(%this.selectedSkyBox);
	}
	
	
	
	syncParamArray(SEP_LegacySkyManager.paramArray);
}
//------------------------------------------------------------------------------

//==============================================================================
// SAVING AND DIRTY MANAGEMENTS
//==============================================================================
//==============================================================================
function SEP_LegacySkyManager::setDirtyObject(%this,%obj,%isDirty) {
	logd("SEP_LegacySkyManager::setDirtyObject(%this,%obj,%isDirty)",%this,%obj,%isDirty);
	
	if (isObject(%obj)){
		SEP_AmbientManager.setObjectDirty(%obj,%isDirty);
	
		if (%isDirty){
			%this.dirtyList = strAddWord(%this.dirtyList,%obj.getId(),true);
		} else {
			%this.dirtyList = strRemoveWord(%this.dirtyList,%obj.getId());
		}
	}
	
	%legacyIsDirty = false;
	if (getWordCount(%this.dirtyList) > 0)
		%legacyIsDirty = true;
	SEP_LegacySkySaveButton.active = %legacyIsDirty;
}
//------------------------------------------------------------------------------

//==============================================================================
// Sync the current profile values into the params objects
function SEP_LegacySkyManager::saveData( %this ) { 		
	
	foreach$(%obj in %this.dirtyList){
		if(SEP_AmbientManager_PM.isDirty(%obj))
			SEP_AmbientManager_PM.saveDirtyObject(%obj);	
		%this.setDirtyObject(%obj,false);
	}

}
//------------------------------------------------------------------------------