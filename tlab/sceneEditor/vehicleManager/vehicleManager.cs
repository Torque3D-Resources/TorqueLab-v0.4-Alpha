//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$sepVM_MainBook_PageId = 0;

//==============================================================================
// OnWake and OnSleep Callbacks
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::onWake( %this ) {
	sepVM.initWheeledPage();
	sepVM.buildWheeledParams();
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::onSleep( %this ) {
	logd("SEP_VehicleManager::onSleep( %this )");
}
//------------------------------------------------------------------------------

//==============================================================================
// OnShow and OnHide Callbacks
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::onShow( %this ) {	
	logd("SEP_VehicleManager::onShow");
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::onHide( %this ) {	
}
//------------------------------------------------------------------------------

//==============================================================================
// onPreEditorSave and onPostEditorSave Callbacks
//==============================================================================
//==============================================================================
function SEP_VehicleManager::onPreEditorSave(%this) {	
	logd("SEP_VehicleManager::onPreEditorSave");	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_VehicleManager::onPostEditorSave(%this) {
	logd("SEP_VehicleManager::onPostEditorSave");
	
}
//------------------------------------------------------------------------------

//==============================================================================
// initDialog and OnActivated Callbacks
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::initDialog( %this ) {
	

	
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_VehicleManager::onActivated( %this ) {
	logd("SEP_VehicleManager::onActivated(%this)");
	sepVM_MainBook.selectPage($sepVM_MainBook_PageId);	

}
//------------------------------------------------------------------------------

//==============================================================================
// Main GUI Functions
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function seVM_MainBook::onTabSelected( %this,%text,%index ) {
	logd("seVM_MainBook::onTabSelected( %this,%text,%index )");
	
	$seVM_MainBook_PageId = %index;
}
//------------------------------------------------------------------------------
