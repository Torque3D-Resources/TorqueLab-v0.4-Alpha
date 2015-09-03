//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Based on MaterialLab by Dave Calabrese and Travis Vroman of Gaslight Studios
//==============================================================================

//==============================================================================
function initializeMaterialLab() {
	
	echo(" % - Initializing Material Lab");
	
	execMLP(true);
	
	$MatLab = newScriptObject("MatLab");
	//exec("./gui/profiles.ed.cs");
	Lab.createPlugin("MaterialLab","Material Lab");
	MaterialLabPlugin.superClass = "WEditorPlugin";
	Lab.addPluginEditor("MaterialLab",MaterialLabGui); //Tools renamed to Gui to store stuff
	Lab.addPluginGui("MaterialLab",MaterialLabTools); //Tools renamed to Gui to store stuff
	Lab.addPluginDlg("MaterialLab",matLab_cubemapEditor);
	Lab.addPluginDlg("MaterialLab",matLab_addCubemapWindow);
	Lab.addPluginToolbar("MaterialLab",MaterialLabToolbar);
}
//------------------------------------------------------------------------------
//==============================================================================
function execMLP(%loadGui) {
	if (%loadGui) {
		// Load MaterialLab Guis
		exec("tlab/materialLab/gui/matLab_cubemapEditor.gui");
		exec("tlab/materialLab/gui/matLab_addCubemapWindow.gui");
		exec("tlab/materialLab/gui/matLabNonModalGroup.gui");
		exec("tlab/materialLab/gui/MaterialLabToolbar.gui");
		exec("tlab/materialLab/gui/MaterialLabTools.gui");
		exec("tlab/materialLab/gui/MaterialLabGui.gui");
		
	}

	// Load Client Scripts.

	exec("./MaterialLabPlugin.cs");
	execPattern("tlab/materialLab/scripts/*.cs");
	execPattern("tlab/materialLab/manager/*.cs");
	execPattern("tlab/materialLab/material/*.cs");
}
//==============================================================================
function destroyMaterialLab() {
}
//------------------------------------------------------------------------------