//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function Lab::onEditorOpen( %this ) {
	//Check for methods in each plugin objects
	foreach(%pluginObj in EditorPluginSet){
		if (%pluginObj.isMethod("onEditorOpen"))
			%pluginObj.onEditorOpen();
	}			
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::onEditorWake( %this ) {
	//Check for methods in each plugin objects
	foreach(%pluginObj in EditorPluginSet){
		if (%pluginObj.isMethod("onEditorWake"))
			%pluginObj.onEditorWake();
	}			
}
//------------------------------------------------------------------------------
