//==============================================================================
// TorqueLab -> Lab PostFX Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function EPostFxManager::onWake(%this) {
	devLog("EPostFxManager::onWake");
	//Check if the UI is all there (Might be moved and forgotten)
	%this.onShow();
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onShow(%this) {
	devLog("EPostFxManager::onShow");
	//Check if the UI is all there (Might be moved and forgotten)
	if (!isObject(%this-->MainContainer)){
		devLog("Moving back Data");
		EPostFxManager.add(EPostFxManager_Main);
		EPostFxManager.schedule(1000,"init","1");
		return;
	}
		
	if (!EPostFxManager.initialized)
		EPostFxManager.init();
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onSleep(%this) {
	
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::init(%this,%notInitialized) {
		
	%this.buildParamsHDR();
	%this.buildParamsSSAO();
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

