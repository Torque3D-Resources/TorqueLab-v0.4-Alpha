//==============================================================================
// TorqueLab -> Lab PostFX Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
EPostFxManager.initialized = false;
function EPostFxManager::onWake(%this) {	
	//Check if the UI is all there (Might be moved and forgotten)
	%this.onShow();
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onShow(%this) {
	
	//Check if the UI is all there (Might be moved and forgotten)
	if (!isObject(%this-->MainContainer)){		
		EPostFxManager.add(EPostFxManager_Main);
		EPostFxManager.schedule(1000,"init","1");
		return;
	}
		
	if (!EPostFxManager.initialized)
		EPostFxManager.init();
	$EPostFxManagerActive = true;
	if (isObject(EditorGuiToolbarStack-->PostFXManager))
		EditorGuiToolbarStack-->PostFXManager.setStateOn(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onHide(%this) {	
	$EPostFxManagerActive = false;
	if (isObject(EditorGuiToolbarStack-->PostFXManager))
		EditorGuiToolbarStack-->PostFXManager.setStateOn(false);
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::onSleep(%this) {
	
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::init(%this,%forceInit,%notInitialized) {
	devLog("EPostFxManager::init(%this,%forceInit,%notInitialized)",%forceInit,%notInitialized);
	
	%this.buildParamsHDR();
	%this.buildParamsSSAO();
	%this.buildParamsLightRays();
	%this.buildParamsDOF();
	%this.buildParamsVignette();
	if (EPostFxManager.initialized && !%forceInit)
		return;
	EPostFxManager.initPresets();
	if (%notInitialized $= "")
		EPostFxManager.initialized = true;
	else
		EPostFxManager.initialized = false;
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::moveToGui(%this,%gui) {		
	%gui.add(EPostFxManager_Main);	
	hide(EPostFxManager);
	
	//Schedule a new init to adapt the params to new host stack
	EPostFxManager.schedule(1000,"init","1");
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::moveFromGui(%this,%gui) {
	%this.add(EPostFxManager_Main);	
	EPostFxManager.schedule(1000,"init");
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onPreEditorSave(%this) {
	
	%this-->StackBloom.clear();
	%this-->StackBrightness.clear();
	EPostFxPage_SSAOStack.clear();

}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onPostEditorSave(%this) {
	
	EPostFxManager.init();
}
//------------------------------------------------------------------------------


