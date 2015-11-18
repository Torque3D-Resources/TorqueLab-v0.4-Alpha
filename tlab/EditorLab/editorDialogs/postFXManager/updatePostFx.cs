//==============================================================================
// LabEditor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function EPostFxManager::syncAll(%this) {
	foreach$(%type in $EPostFx_PostFxList) {
		syncParamArray("arEPostFx_"@%type@"Param");

		if (%this.isMethod("customSync"@%type))
			eval("%this.customSync"@%type@"();");
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::applyPostFXSettings(%this) {
	%this.settingsSetEnabled( $LabPostFx_Enabled_PostFX);
	/*foreach$(%type in $EPostFx_PostFxList){
		eval("%enabled = $LabPostFx_Enabled_"@%type@";");
		info(%type,"Enabled:",%enabled);
		%this.settingsEffectSetEnabled(%type,%enabled);
	}*/
	%this.UpdateDOFSettings();
	EPostFxManager.syncAll();
}
//------------------------------------------------------------------------------
function EPostFxManager::settingsRefreshAll(%this) {
	$LabPostFx_General_Enabled           = $pref::enablePostEffects;
	$LabPostFx_General_EnableSSAO        = SSAOPostFx.isEnabled();
	$LabPostFx_General_EnableHDR         = HDRPostFX.isEnabled();
	$LabPostFx_General_EnableLightRays   = LightRayPostFX.isEnabled();
	$LabPostFx_General_EnableDOF         = DOFPostEffect.isEnabled();
	$LabPostFx_General_EnableVignette    = VignettePostEffect.isEnabled();
	%this.syncAll();
}
//==============================================================================
function EPostFxManager_EnableCheck::onAction(%this) {
	EPostFxManager.settingsSetEnabled(%this.isStateOn());
}
//------------------------------------------------------------------------------
function EPostFxManager::settingsSetEnabled(%this, %bEnablePostFX) {
	$LabPostFx_Enabled_PostFX = %bEnablePostFX;

	//if to enable the postFX, apply the ones that are enabled
	if ( %bEnablePostFX ) {
		//SSAO, HDR, LightRays, DOF
		foreach$(%type in $EPostFx_PostFxList) {
			eval("%enabled = $LabPostFx_Enabled_"@%type@";");
			%this.settingsEffectSetEnabled(%type,%enabled);
		}

		postVerbose("% - PostFX Manager - PostFX enabled");
	} else {
		//Disable all postFX
		SSAOPostFx.disable();
		HDRPostFX.disable();
		LightRayPostFX.disable();
		DOFPostEffect.disable();
		VignettePostEffect.disable();
		postVerbose("% - PostFX Manager - PostFX disabled");
	}
}

function EPostFxManager::settingsEffectSetEnabled(%this, %sName, %bEnable) {
	%postEffect = 0;

	if (%bEnable $= "")
		%bEnable = "0";

	//Determine the postFX to enable, and apply the boolean
	if(%sName $= "SSAO") {
		%postEffect = SSAOPostFx;
		$LabPostFx_Enabled_SSAO = %bEnable;
		//$pref::PostFX::SSAO::Enabled = %bEnable;
	} else if(%sName $= "HDR") {
		%postEffect = HDRPostFX;
		$LabPostFx_Enabled_HDR = %bEnable;

		if (%bEnable && $HDRPostFx::DebugEnabled)
			LuminanceVisPostFX.enable();
		else
			LuminanceVisPostFX.disable();

		//$pref::PostFX::HDR::Enabled = %bEnable;
	} else if(%sName $= "LightRays") {
		%postEffect = LightRayPostFX;
		$LabPostFx_Enabled_LightRays = %bEnable;
		//$pref::PostFX::LightRays::Enabled = %bEnable;
	} else if(%sName $= "DOF") {
		%postEffect = DOFPostEffect;
		$LabPostFx_Enabled_DOF = %bEnable;
		//$pref::PostFX::DOF::Enabled = %bEnable;
	} else if(%sName $= "Vignette") {
		%postEffect = VignettePostEffect;
		$LabPostFx_Enabled_Vignette = %bEnable;
		//$pref::PostFX::Vignette::Enabled = %bEnable;
	}

	// Apply the change
	if ( %bEnable ) {
		%postEffect.enable();
		// ("% - PostFX Manager - " @ %sName @ " enabled");
	} else {
		%postEffect.disable();
		//info("% - PostFX Manager - " @ %sName @ " disabled");
	}
}