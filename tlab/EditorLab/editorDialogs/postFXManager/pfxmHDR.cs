//==============================================================================
// TorqueLab -> Lab PostFX Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_ScatterSkyManager.buildParams();
function EPostFxManager::buildParamsHDR( %this ) {
	%arCfg = createParamsArray("EPostFx_HDR",EPostFxPage_HDR);
	%arCfg.updateFunc = "EPostFxManager.updateParamHDR";
	%arCfg.style = "LabCfgB_230";
	%arCfg.useNewSystem = true;
	%arCfg.prefGroup = "$HDRPostFX::";
	%arCfg.group[%gid++] = "HDR Bloom Settings" TAB "Stack StackBloom";
	%arCfg.setVal("brightPassThreshold",       "" TAB "Bright pass threshold" TAB "SliderEdit" TAB "range>>0 5" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussMean",       "" TAB "Blur mean" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussStdDev",       "" TAB "Blur Std Dev" TAB "SliderEdit" TAB "range>>0 3" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussMultiplier",       "" TAB "Blur Multiplier" TAB "SliderEdit" TAB "range>>0 5" TAB "EPostFxManager" TAB %gid);
	
	%arCfg.group[%gid++] = "HDR Brightness Settings" TAB "Stack StackBrightness";
	%arCfg.setVal("enableToneMapping",       "" TAB "Tone mapping" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("keyValue",       "" TAB "Key value" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("minLuminace",       "" TAB "Liminance min." TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("whiteCutoff",       "" TAB "White cut-off" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("adaptRate",       "" TAB "Adapt rate" TAB "SliderEdit" TAB "range>>0.1 10" TAB "EPostFxManager" TAB %gid);
	



	buildParamsArray(%arCfg,false);
	%this.CloudLayerParamArray = %arCfg;
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::updateParamHDR(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3) {
	logd("EPostFxManager::updateParamHDR(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3)",%this,%field,%value,%ctrl,%arg1,%arg2,%arg3);
	%fieldData = strreplace(%field,"["," ");
	%fieldData = strreplace(%fieldData,"]","");
	%this.updateCloudLayerField(getWord(%fieldData,0),%value, getWord(%fieldData,1));
}
//------------------------------------------------------------------------------
function EPostFx_HDRCheckbox::onAction(%this)
{
	eval("$HDRPostFX::"@%this.internalName@" = %this.getValue();"); 
}
function EPostFx_ColorShiftPicker::onAction(%this)
{
   $HDRPostFX::blueShiftColor = %this.PickColor;
   %this.ToolTip = "Color Values : " @ %this.PickColor;
}
function EPostFx_ColorShiftBasePicker::onAction(%this)
{
   EPostFx_ColorShiftPicker.baseColor = %this.PickColor;
   %this.ToolTip = "Color Values : " @ %this.PickColor;
}

