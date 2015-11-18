//==============================================================================
// TorqueLab -> Scene Editor Plugin
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
//Initialize the Scene Editor Plugin
function initializeSceneEditor() {
	info( "TorqueLab ->","Initializing Scene Editor plugin" );
	//Load the guis and scripts
	execSceneEd(true);
	//Create the TorqueLab Plugin instance
	Lab.createPlugin("SceneEditor","Scene Editor");
	//Add the Plugin related GUIs to TorqueLab
	Lab.addPluginGui("SceneEditor",SceneEditorTools);
	Lab.addPluginToolbar("SceneEditor",SceneEditorToolbar);
	Lab.addPluginPalette("SceneEditor",SceneEditorPalette);
	Lab.addPluginDlg("SceneEditor",SceneEditorDialogs);
	SceneEditorPlugin.superClass = "WEditorPlugin";
	$SceneEd = newScriptObject("SceneEd");
	$SEPtools = newScriptObject("SEPtools");
	$SceneObjectGroupSet = newSimSet(SceneObjectGroupSet);
	$SceneCreator = newScriptObject("SceneCreator");
	$sepVM = newScriptObject("sepVM");
	$sepVM_TempDatablocks = newSimGroup("sepVM_TempDatablocks");
}
//------------------------------------------------------------------------------
//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function execSceneEd(%loadGui) {
	if (%loadGui) {
		exec("tlab/sceneEditor/gui/SceneEditorTools.gui");
		exec("tlab/sceneEditor/gui/SceneEditorToolbar.gui" );
		exec("tlab/sceneEditor/gui/SceneEditorPalette.gui" );
		exec("tlab/sceneEditor/gui/SceneEditorDialogs.gui");
	}

	exec("tlab/sceneEditor/SceneEditorPlugin.cs");
	exec("tlab/sceneEditor/SceneEditorToolbar.cs");
	exec("tlab/sceneEditor/SceneEditorTools.cs");
	execPattern("tlab/sceneEditor/plugin/*.cs");
	execPattern("tlab/sceneEditor/managers/*.cs");
	execPattern("tlab/sceneEditor/tools/*.cs");
	execPattern("tlab/sceneEditor/PageScene/*.cs");
	execPattern("tlab/sceneEditor/PageCreator/*.cs");
	execPattern("tlab/sceneEditor/PageCustom/*.cs");
	//execPattern("tlab/sceneEditor/ambientManager/*.cs");
	//execPattern("tlab/sceneEditor/vehicleManager/*.cs");
}
//------------------------------------------------------------------------------