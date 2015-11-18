//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Plugin Object Params - Used set default settings and build plugins options GUI
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function AiDirectorPlugin::initParamsArray( %this,%array ) {
	$AiDirectorCfg = newScriptObject("AiDirectorCfg");
	%array.group[%groupId++] = "General settings";
	%array.setVal("playSoundWhenDone",       "1" TAB "playSoundWhenDone" TAB "checkbox"  TAB "" TAB "" TAB %groupId);
}

//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================
//------------------------------------------------------------------------------
// Material Editor


//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================

//==============================================================================
// Called when TorqueLab is launched for first time
function AiDirectorPlugin::onWorldEditorStartup( %this ) {
	Parent::onWorldEditorStartup( %this );
	ADE_SaveGroupButton.active = 0;
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is activated (Active TorqueLab plugin)
function AiDirectorPlugin::onActivated( %this ) {
	// Set a global variable so everyone knows we're editing!
	ADE.initTools();
	Parent::onActivated(%this);
}
//------------------------------------------------------------------------------

//==============================================================================
// Called when the Plugin is deactivated (active to inactive transition)
function AiDirectorPlugin::onDeactivated( %this ) {
	Parent::onDeactivated(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
// Called from TorqueLab after plugin is initialize to set needed settings
function AiDirectorPlugin::onPluginCreated( %this ) {
}
//------------------------------------------------------------------------------

//==============================================================================
// Called when the mission file has been saved
function AiDirectorPlugin::onSaveMission( %this, %file ) {
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when TorqueLab is closed
function AiDirectorPlugin::onEditorSleep( %this ) {
}
//------------------------------------------------------------------------------
//==============================================================================
//Called when editor is selected from menu
function AiDirectorPlugin::onEditMenuSelect( %this, %editMenu ) {
	WEditorPlugin.onEditMenuSelect( %editMenu );
}
//------------------------------------------------------------------------------

function AiDirectorPlugin::handleDelete(%this) {
	// Event happens when the user hits 'delete'.
	// AiDirectorGui.deleteSelected();
}

function AiDirectorPlugin::handleEscape(%this) {
	return AiDirectorGui.onEscapePressed();
}

function AiDirectorPlugin::isDirty(%this) {
	return AiDirectorGui.isDirty;
}
