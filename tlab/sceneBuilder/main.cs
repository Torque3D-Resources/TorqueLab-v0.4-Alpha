//==============================================================================
// TorqueLab -> Scene Editor Plugin
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
//Initialize the Scene Editor Plugin
function initializeSceneBuilder() {
	info( "TorqueLab ->","Initializing Scene Editor plugin" );
	//Load the guis and scripts
	execSBP(true);
	
		//Create the TorqueLab Plugin instance
	Lab.createPlugin("SceneBuilder","Scene Builder",true);
	
	//Add the Plugin related GUIs to TorqueLab
	Lab.addPluginGui("SceneBuilder",SceneBuilderTools);
	Lab.addPluginToolbar("SceneBuilder",SceneBuilderToolbar);
	Lab.addPluginPalette("SceneBuilder",SceneBuilderPalette);
	

	Lab.addPluginDlg("SceneBuilder",SceneBuilderDialogs);
	SceneBuilderPlugin.superClass = "WEditorPlugin";
	$SBP = newScriptObject("SBP");
}
//------------------------------------------------------------------------------
//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function execSBP(%loadGui) {
	if (%loadGui) {
		exec("tlab/sceneBuilder/gui/SceneBuilderTools.gui");
		exec("tlab/sceneBuilder/gui/SceneBuilderToolbar.gui" );
		exec("tlab/sceneBuilder/gui/SceneBuilderPalette.gui" );
		exec("tlab/sceneBuilder/gui/SceneBuilderDialogs.gui");
	}

	exec("tlab/sceneBuilder/SceneBuilderPlugin.cs");
	exec("tlab/sceneBuilder/SceneBuilderToolbar.cs");
	exec("tlab/sceneBuilder/SceneBuilderTools.cs");

	execPattern("tlab/sceneBuilder/PageCreator/*.cs");
	execPattern("tlab/sceneBuilder/PageBuilder/*.cs");

}
//------------------------------------------------------------------------------