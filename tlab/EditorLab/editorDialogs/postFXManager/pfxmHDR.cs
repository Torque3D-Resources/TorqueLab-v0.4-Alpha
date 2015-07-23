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
	%arCfg.style = "StyleA";
	
	%arCfg.useNewSystem = true;
	%arCfg.prefGroup = "$HDRPostFX::";
	%arCfg.autoSyncPref = "1";
	%arCfg.group[%gid++] = "HDR Bloom Settings" TAB "StackType none;;Stack StackBloom";
	%arCfg.setVal("enableBloom",       "" TAB "Enable bloom" TAB "Checkbox" TAB "superClass>>EPostFx_HDRCheckbox" TAB "EPostFxManager" TAB %gid);
	
	%arCfg.setVal("brightPassThreshold",       "" TAB "Bright pass threshold" TAB "SliderEdit" TAB "range>>0 5" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussMean",       "" TAB "Blur mean" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussStdDev",       "" TAB "Blur Std Dev" TAB "SliderEdit" TAB "range>>0 3" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("gaussMultiplier",       "" TAB "Blur Multiplier" TAB "SliderEdit" TAB "range>>0 5" TAB "EPostFxManager" TAB %gid);
	
	%arCfg.group[%gid++] = "HDR Brightness Settings" TAB "StackType none;;Stack StackBrightness";
	%arCfg.setVal("enableToneMapping",       "" TAB "Tone mapping" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("keyValue",       "" TAB "Key value" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("minLuminace",       "" TAB "Liminance min." TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("whiteCutoff",       "" TAB "White cut-off" TAB "SliderEdit" TAB "range>>0 1" TAB "EPostFxManager" TAB %gid);
	%arCfg.setVal("adaptRate",       "" TAB "Adapt rate" TAB "SliderEdit" TAB "range>>0.1 10" TAB "EPostFxManager" TAB %gid);
	



	buildParamsArray(%arCfg,false);
	%this.CloudLayerParamArray = %arCfg;
}
//------------------------------------------------------------------------------
//$HDRPostFX::minLuminace;
//==============================================================================
function EPostFxManager::updateParamHDR(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3) {
	logd("EPostFxManager::updateParamHDR(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3)",%this,%field,%value,%ctrl,%arg1,%arg2,%arg3);
	
}
//------------------------------------------------------------------------------


//==============================================================================
function EPostFxManager::getColorCorrectionFile(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3) {
	logd("EPostFxManager::updateParamHDR(%this,%field,%value,%ctrl,%arg1,%arg2,%arg3)",%this,%field,%value,%ctrl,%arg1,%arg2,%arg3);
	%filter = "Image Files (*.png, *.jpg, *.dds, *.bmp, *.gif, *.jng. *.tga)|*.png;*.jpg;*.dds;*.bmp;*.gif;*.jng;*.tga|All Files (*.*)|*.*|";   
	getLoadFilename(%filter,"EPostFxManager.setColorCorrectionFile",$HDRPostFX::colorCorrectionRamp);
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::setColorCorrectionFile(%this,%filename,%reset) {
	logd("EPostFxManager::updateParamHDR(%this,%filename)",%this,%filename);
	if (%reset)
	 		%filename = "art/gfx/postFx/null_color_ramp.png";
	 		
	 if ( %filename $= "" || !isFile( %filename ) )	 	
      	return;
	
	
	%filename = makeRelativePath( %filename, getMainDotCsDir() );
            
   $HDRPostFX::colorCorrectionRamp = %filename;
   %this-->ColorCorrectionFileName.Text = %filename; 
}
//------------------------------------------------------------------------------
//==============================================================================
// Common HDR Gui COntrol Callbacks
//==============================================================================

//==============================================================================
// Enable/Disable HDR
function EPostFx_EnableHDRCheckbox::onAction(%this)
{
   %toEnable = %this.isStateOn();
   PostFXManager.settingsEffectSetEnabled("HDR", %toEnable);
}
//------------------------------------------------------------------------------
//==============================================================================
// Enable/Disable HDR Debug
function EPostFx_DebugHDRCheckbox::onAction(%this)
{
   if ( %this.getValue() )
      LuminanceVisPostFX.enable();
   else
      LuminanceVisPostFX.disable();   
}
//------------------------------------------------------------------------------
//==============================================================================
// General Checkbox Settings
function EPostFx_HDRCheckbox::onAction(%this)
{
	eval("$HDRPostFX::"@%this.internalName@" = %this.getValue();"); 
}
//------------------------------------------------------------------------------
//==============================================================================
// ColorShift HDR Gui Control Callbacks
//==============================================================================
//==============================================================================
// ColorShift Color Picked
function EPostFx_ColorShiftPicker::onAction(%this)
{
   $HDRPostFX::blueShiftColor = %this.PickColor;
   %this.ToolTip = "Color Values : " @ %this.PickColor;
}
//------------------------------------------------------------------------------
//==============================================================================
// ColorShift Base Color Picked
function EPostFx_ColorShiftBasePicker::onAction(%this)
{
   EPostFx_ColorShiftPicker.baseColor = %this.PickColor;
   %this.ToolTip = "Color Values : " @ %this.PickColor;
}
//------------------------------------------------------------------------------