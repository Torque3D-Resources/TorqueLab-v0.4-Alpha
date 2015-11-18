//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Based on MaterialLab by Dave Calabrese and Travis Vroman of Gaslight Studios
//==============================================================================

singleton GuiControlProfile(GuiBehaviorTreeViewProfile : ToolsTreeViewProfile) {
	bitmap = "tlab/aiDirector/images/BehaviorTreeView.png";
};
//==============================================================================
function initializeAIDirector() {
	echo( " % - Initializing AI Director" );
	execADE(true);
	// register the class icons
	EditorIconRegistry::loadFromPath("tlab/aiDirector/images/classIcons/");
	$ADE = newScriptObject("ADE");
	Lab.createPlugin("AiDirector","AI Director");
	Lab.addPluginGui("AiDirector",AiDirectorTools);
	//Lab.addPluginGui("RoadEditor",RoadEditorOptionsWindow);
	//Lab.addPluginGui("RoadEditor",RoadEditorTreeWindow);
	Lab.addPluginToolbar("AiDirector",AiDirectorToolbar);
	Lab.addPluginPalette("AiDirector",   AiDirectorPalette);
	Lab.addPluginDlg("AiDirector",   AiDirectorDialogs);
	Lab.initMenu("BT");
	//exec("./gui/profiles.ed.cs");
	//Lab.createPlugin("BehaviorTreeEditor","Behavior Tree Editor");
	//NavEditorPlugin.superClass = "WEditorPlugin";
	//Lab.addPluginEditor("BehaviorTreeEditor",BTEditor); //Tools renamed to Gui to store stuff
	//Lab.addPluginGui("BehaviorTreeEditor",NavEditorTools); //Tools renamed to Gui to store stuff
	//Lab.addPluginDlg("NavEditor",CreateNewNavMeshDlg);
	//Lab.addPluginDlg("BehaviorTreeEditor",NavEditorConsoleDlg);
	//Lab.addPluginToolbar("BehaviorTreeEditor",NavEditorToolbar);
	//Lab.addPluginPalette("BehaviorTreeEditor",NavEditorPalette);
}
//------------------------------------------------------------------------------
//==============================================================================
function execADE(%loadGui) {
	if (%loadGui) {
		// Load MaterialLab Guis
		exec("tlab/aiDirector/gui/BTEditor.gui");
		exec("tlab/aiDirector/gui/BTEditorCreatePrompt.gui");
		exec( "tlab/aiDirector/gui/AiDirectorTools.gui" );
		exec( "tlab/aiDirector/gui/AiDirectorToolbar.gui");
		exec( "tlab/aiDirector/gui/AiDirectorPalette.gui");
		exec( "tlab/aiDirector/gui/AiDirectorDialogs.gui");
	}

	// Load Client Scripts.
	exec("tlab/aiDirector/BTEdMenu.cs");
	execPattern("tlab/aiDirector/system/*.cs");
	execPattern("tlab/aiDirector/scripts/*.cs");
	execPattern("tlab/aiDirector/spawnMode/*.cs");
	execPattern("tlab/aiDirector/groupMode/*.cs");
	execPattern("tlab/aiDirector/triggerMode/*.cs");
	execPattern("tlab/aiDirector/behaviors/*.cs");
}
//==============================================================================
function destroyBehaviorTreeEditor() {
}
//------------------------------------------------------------------------------


function reloadBTE() {
	if(isObject(BehaviorTreeEditorGui))
		BehaviorTreeEditorGui.delete();

	initializeBehaviorTreeEditor();
}

