//==============================================================================
// TorqueLab -> Lab PostFX Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_ScatterSkyManager.buildParams();
function EPostFxManager::buildParamsLightRays( %this ) {
	%arCfg = createParamsArray("EPostFx_LightRays",EPostFxPage_LightRaysStack);
	%arCfg.updateFunc = "EPostFxManager.updateParamLightRays";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.prefGroup = "$LightRayPostFX::";
	%arCfg.autoSyncPref = "1";
	%arCfg.group[%gid++] = "LightRays Settings";
	%arCfg.setVal("enableLightRays",       "" TAB "Enable LightRays" TAB "Checkbox" TAB "superClass>>EPostFx_EnableLightRaysCheckbox" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("brightScalar",       "" TAB "Brightness" TAB "SliderEdit" TAB "range>>0 5" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("numSamples",       "" TAB "Samples" TAB "SliderEdit" TAB "range>>20 512" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("density",       "" TAB "Density" TAB "SliderEdit" TAB "range>>0.01 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("weight",       "" TAB "Weight" TAB "SliderEdit" TAB "range>>0.1 10" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("decay",       "" TAB "Decay (0 to 1)" TAB "SliderEdit" TAB "range>>0.01 1;;linkSet>>decay_hi" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("decay_hi",       "" TAB "Decay (0.9 to 1)" TAB "SliderEdit" TAB "range>>0.9 1;;linkSet>>decay" TAB "EPostFxManager" TAB %gid);
	buildParamsArray(%arCfg,false);
	%this.LightRaysParamArray = %arCfg;
}
//------------------------------------------------------------------------------
//$LightRaysPostFX::minLuminace;
//==============================================================================
function EPostFxManager::updateParamLightRays(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3) {
	logd("EPostFxManager::updateParamLightRays(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3)",%this,%field,%value,%ctrl,%arg1,%arg2,%arg3);
	eval("$LabPostFx_LightRays_"@%field@" = %value;");
}
//------------------------------------------------------------------------------


//==============================================================================
// Common LightRays Gui COntrol Callbacks
//==============================================================================

//==============================================================================
// Enable/Disable LightRays
function EPostFx_EnableLightRaysCheckbox::onClick(%this) {
	EPostFxManager.settingsEffectSetEnabled("LightRays", %this.isStateOn());
}
//------------------------------------------------------------------------------

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_ScatterSkyManager.buildParams();
function EPostFxManager::buildParamsVignette( %this ) {
	%arCfg = createParamsArray("EPostFx_Vignette",EPostFxPage_VignetteStack);
	%arCfg.updateFunc = "EPostFxManager.updateParamVignette";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.prefGroup = "$VignettePostFX::";
	%arCfg.autoSyncPref = "1";
	%arCfg.group[%gid++] = "Vignette Settings";
	%arCfg.setVal("enableVignette",       "" TAB "Enable Vignette" TAB "Checkbox" TAB "superClass>>EPostFx_EnableVignetteCheckbox" TAB "EPostFxManager" TAB %gid);
	buildParamsArray(%arCfg,false);
	%this.VignetteParamArray = %arCfg;
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::updateParamVignette(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3) {
	logd("EPostFxManager::updateParamVignette(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3)",%this,%field,%value,%ctrl,%arg1,%arg2,%arg3);
	eval("$LabPostFx_Vignette_"@%field@" = %value;");
}
//------------------------------------------------------------------------------

function EPostFx_EnableVignetteCheckbox::onClick(%this) {
	EPostFxManager.settingsEffectSetEnabled("Vignette",  %this.isStateOn());
}