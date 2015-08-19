//==============================================================================
// TorqueLab -> Lab PostFX Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$EPostFx_PresetFolder = "scripts/client/gfx/postFX/presets";
$EPostFx_PresetFileFilter = "Post Effect Presets|*.pfx.cs|*.postfxpreset.cs";

$EPostFx_Fields_General = "ColorCorrectionRamp";
$EPostFx_Fields_DOF = "BlurCurveFar BlurCurveNear BlurMax BlurMin EnableAutoFocus EnableDOF FocusRangeMax FocusRangeMin";
$EPostFx_Fields_HDR = "adaptRate blueShiftColor brightPassThreshold enableBloom enableBlueShift enableToneMapping gaussMean gaussMultiplier gaussStdDev keyValue minLuminace whiteCutoff DebugEnabled ColorCorrectionRamp";
$EPostFx_Fields_LightRays = "brightScalar decay density numSamples weight";
$EPostFx_Fields_SSAO = "blurDepthTol blurNormalTol lDepthMax lDepthMin lDepthPow lNormalPow lNormalTol lRadius lStrength overallStrength quality sDepthMax sDepthMin sDepthPow sDepthPow sNormalPow sNormalTol sRadius sStrength";
$EPostFx_PostFxList = "HDR DOF SSAO LightRays Vignette";
//==============================================================================
function EPostFxManager::initPresets(%this) {
	EPostFx_Presets-->presetName.text = "[Preset Name]";
	EPostFx_Presets-->saveActivePreset.active = 0;
	%this.updatePresetMenu();
	
	%this.activatePresetFile($EPostFx_PresetFolder@"/default.pfx.cs");
}
//------------------------------------------------------------------------------
//==============================================================================
//EPostFxManager.updatePresetMenu();
function EPostFxManager::updatePresetMenu(%this) {
	%searchFolder = $EPostFx_PresetFolder@"/*.pfx.cs";
	PFXM_PresetMenu.clear();
	PFXM_PresetMenu.add("[Preset Name]",0);
	%menuId = 0;
	for(%file = findFirstFile(%searchFolder); %file !$= ""; %file = findNextFile(%searchFolder)) {
		%fileName = fileBase(%file);
		%fileNameClean = strreplace(%fileName,".pfx","");
		PFXM_PresetMenu.add(%fileNameClean,%menuId++);
	}
	PFXM_PresetMenu.setSelected(0,false);
}
//------------------------------------------------------------------------------
//==============================================================================
function PFXM_PresetMenu::onSelect(%this,%id,%text) {
	
	%file = $EPostFx_PresetFolder@"/"@%text@".pfx.cs";	
	EPostFxManager.activatePresetFile(%file);
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::activatePresetFile(%this,%file) {

	%result = EPostFxManager.loadPresetsFromFile(%file);
	
	if (!%result){
		EPostFx_Presets-->saveActivePreset.active = 0;
		EPostFx_Presets-->presetName.text = "[Invalid Preset]";
		return;
	}
	%fileName = fileBase(%file);
	%fileName = strreplace(%fileName,".pfx","");
	EPostFx_Presets-->saveActivePreset.active = 1;
	EPostFx_Presets-->presetName.text = %fileName;		
}
//------------------------------------------------------------------------------
//==============================================================================
// Save PostFx Presets
//==============================================================================
//==============================================================================

function EPostFxManager::saveActivePreset(%this) {	
	%name = %this.validatePresetName();
	if (%name $= "")
		return;
	
	%file = $EPostFx_PresetFolder@"/"@%name@".pfx.cs";	
	%this.savePresetsToFile(%file);
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::validatePresetName(%this) {
	%name = EPostFx_Presets-->presetName.getText();
	if (strFindWords(%name,"[ ]") || %name $= "")
		%invalidName = true;
	
	if(%invalidName){
		warnLog("Preset name is invalid:",%name);
		EPostFx_Presets-->saveActivePreset.active = 0;
		return "";
	}
	EPostFx_Presets-->saveActivePreset.active = 1;
	return %name;
	
}
//------------------------------------------------------------------------------
//==============================================================================

function EPostFxManager::selectPresetFileSave(%this) {
%default = $EPostFx_PresetFolder@"/default.pfx.cs";
 getSaveFilename($EPostFx_PresetFileFilter, "EPostFxManager.savePresetsToFile", %defaultFile);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::savePresetsToFile(%this,%file) {
	%useStockFormat = EPostFxManager_Main-->storeAsStockPreset.isStateOn();
	EPostFxManager.saveMissionPresets(%file);
	/*
	if (%useStockFormat){
		EPostFxManager.saveMissionPresets(%file);
	}
	else {
		$PostFXPresetFormat = "Lab";
		foreach$(%field in $EPostFx_PostFxList SPC "PostFX"){
				eval("%value = $LabPostFx_Enabled_"@%field@";");
				$PostFxPreset_["Enabled",%field] = %value;				
		}
		foreach$(%type in "DOF HDR LightRays SSAO"){
			foreach$(%field in $EPostFx_Fields_[%type]){
				eval("%value = $"@%type@"PostFx::"@%field@";");			
				$PostFxPreset_[%type,%field] = %value;			
			}
		}
		
		export("$PostFxPreset_*", %file);		
	}*/


	info("PostFX Presets exported to file:",%file);
	%this.updatePresetMenu();
}
//------------------------------------------------------------------------------

//==============================================================================
//EPostFxManager.saveMissionPresets
function EPostFxManager::saveMissionPresets(%this,%postFxFile) {
	if (%postFxFile $= ""){
		%missionFile = $Client::MissionFile;
		%postFxFile = strreplace(%missionFile,".mis",".postfxpreset.cs");
	}
	//%postFxFile = strreplace(%missionFile,".pfx.",".postfxpreset.");
	$PostFXPresetFormat = "Stock";
	//Convert some fields to work with stock templates (Those are bad set in templates, not torquelab)
	$GeneralPostFx::ColorCorrectionRamp = $HDRPostFx::ColorCorrectionRamp;
	
	foreach$(%field in $EPostFx_PostFxList SPC "PostFX"){
				eval("%value = $LabPostFx_Enabled_"@%field@";");			
				eval("$PostFXManager::Settings::Enable"@%field@" = %value;");	
	}
	foreach$(%type in "DOF HDR LightRays SSAO"){
		foreach$(%field in $EPostFx_Fields_[%type]){
			eval("%value = $"@%type@"PostFx::"@%field@";");			
				
			if (strFind($EPostFx_Fields_General,%field))
				eval("$PostFXManager::Settings::"@%field@" = %value;");	
			else
				eval("$PostFXManager::Settings::"@%type@"::"@%field@" = %value;");	
			
		}
	}	
	
	export("$PostFXManager::Settings::*", %postFxFile);
	info("MISSION PostFX Presets exported to mission file:",%postFxFile);
	
}
//------------------------------------------------------------------------------

//==============================================================================
// LOAD PostFx Presets
//==============================================================================
//==============================================================================
function EPostFxManager::selectPresetFileLoad(%this) {
%default = $EPostFx_PresetFolder@"/default.pfx.cs";
 getSaveFilename($EPostFx_PresetFileFilter, "EPostFxManager.loadPresetsFromFile", %defaultFile);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::loadPresetsFromFile(%this,%file) {
	logd(" EPostFxManager::loadPresetsFromFile");
	if (!isFile(%file))
		return false;
	$PostFXPresetFormat = "";
	exec(%file);
	%this.loadStockPresets();
	info("PostFX Presets imported from file:",%file,"With Format:",$PostFXPresetFormat);
	%this.applyPostFXSettings();
	return true;
	/*
	if (strFind(%file,"postfxpreset")){
		devLog("Loding stock presets",%file);
		%this.loadStockPresets();
	}	
	else {
		%this.loadLabPresets();		
	}
	info("PostFX Presets imported from file:",%file,"With Format:",$PostFXPresetFormat);
	%this.applyPostFXSettings();
	return true;*/
}
//------------------------------------------------------------------------------
//==============================================================================
/*function EPostFxManager::loadLabPresets(%this) {	
	foreach$(%field in $EPostFx_PostFxList SPC "PostFX"){			
				%value = $PostFxPreset_["Enabled",%field];
				if (%value $= "")
					%value = "0";
				eval("$LabPostFx_Enabled_"@%field@" = %value;");
				
		}
		foreach$(%type in "DOF HDR LightRays SSAO"){
			foreach$(%field in $EPostFx_Fields_[%type]){
				%value = $PostFxPreset_[%type,%field];
				eval("$"@%type@"PostFx::"@%field@" = %value;");	
				//info("Pref:","$"@%type@"PostFx::"@%field,"Value",%value);	
			}
		}
}
//------------------------------------------------------------------------------*/
//==============================================================================
function EPostFxManager::loadStockPresets(%this) {
	
	foreach$(%field in $EPostFx_PostFxList SPC "PostFX"){			
			eval("%value = $PostFXManager::Settings::Enable"@%field@";");
		
			eval("$LabPostFx_Enabled_"@%field@" = %value;");
			
	}
	foreach$(%type in "DOF HDR LightRays SSAO"){
		foreach$(%field in $EPostFx_Fields_[%type]){
			if (strFind($EPostFx_Fields_General,%field))
				eval("%value = $PostFXManager::Settings::"@%field@";");
			else
				eval("%value = $PostFXManager::Settings::"@%type@"::"@%field@";");
			
			
			eval("$"@%type@"PostFx::"@%field@" = %value;");	
			//info("Pref:","$"@%type@"PostFx::"@%field,"Value",%value);	
		}
		
	}	
	
	$HDRPostFX::ColorCorrectionRamp = $PostFXManager::Settings::ColorCorrectionRamp;		
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::dumpPrefs(%this) {
	export("$LabPostFx*", "tlab/postFxPrefs.cs");
	
}
//------------------------------------------------------------------------------