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
	if (EPostFxManager_Main.parentGroup !$= %this)
		EPostFxManager.add(EPostFxManager_Main);
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
	if (%notInitialized $= "")
		EPostFxManager.initialized = true;
	else
		EPostFxManager.initialized = false;
}
//------------------------------------------------------------------------------

//==============================================================================
function EPostFxManager::onPreEditorSave(%this) {
	
	%this-->StackBloom.clear();
	%this-->StackBrightness.clear();

}
//------------------------------------------------------------------------------
//==============================================================================
function EPostFxManager::onPostEditorSave(%this) {
	
	EPostFxManager.init();
}
//------------------------------------------------------------------------------

