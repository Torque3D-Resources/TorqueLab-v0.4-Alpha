//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// TorqueLab Plugin Tools Container (Side settings area)
//==============================================================================

function Lab::loadEditorFrameMainDefaults(%this) {
	$LabGui_EditorFrameMain_Column_Thin = "0.8055";
	$LabGui_EditorFrameMain_Column_Normal = "0.75";
	$LabGui_EditorFrameMain_Column_Large = "0.6666";
	$LabGui_EditorFrameMain_BorderWidth = "4";
	$LabCfg_EditorUI_ToolFrameLocked = "0";
	$LabCfg_EditorUI_ToolFrameSize = "Normal";
	$LabGui_ForceToolsSize = true;
}
Lab.loadEditorFrameMainDefaults();

//==============================================================================
// EditorFrameMain Functions
//==============================================================================
function EditorFrameMain::onWake(%this) {
	Lab.setupEditorFrameMain();
}

//==============================================================================
function Lab::setEditorToolFrameSize(%this,%sizeMode) {
	%size = $LabGui_EditorFrameMain_Column_[%sizeMode];

	if (%size $= "")
		return;

	$LabCfg_EditorUI_ToolFrameSize = %sizeMode;
	%this.setupEditorFrameMain();
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setupEditorFrameMain(%this,%setDefaults) {
	EditorFrameMain.frameMinExtent(1,280,100);

	if (%setDefaults) {
		$LabCfg_EditorUI_ToolFrameSize = InterfaceEditor_Param.getCfg("ToolFrameSize");
		$LabCfg_EditorUI_ToolFrameLocked = InterfaceEditor_Param.getCfg("ToolFrameLocked");
	}

	//Leave if no tools for active plugin
	if (!Lab.currentEditor.useTools)
		return;

	%columnRatio = $LabGui_EditorFrameMain_Column_[$LabCfg_EditorUI_ToolFrameSize];
	%sideCol = EditorFrameMain.lastToolsCol;

	if (%sideCol $= "" || $LabCfg_EditorUI_ToolFrameLocked )
		%sideCol = mCeil(EditorFrameMain.extent.x * %columnRatio);

	EditorFrameMain.columns = "0" SPC %sideCol;
	EditorFrameMain.updateSizes();

	if ($LabCfg_EditorUI_ToolFrameLocked && EditorFrameMain.borderWidth !$= "0")
		%this.lockEditorToolFrame(true);
	else if (!$LabCfg_EditorUI_ToolFrameLocked && EditorFrameMain.borderWidth $= "0")
		%this.lockEditorToolFrame(false);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::lockEditorToolFrame(%this,%locked) {
	if (%locked) {
		EditorFrameMain.borderEnable = "alwaysOff";
		EditorFrameMain.borderMovable = "alwaysOff";
		EditorFrameMain.borderWidth = "0";
		EditorFrameMain.updateSizes();
		return;
	}

	EditorFrameMain.borderEnable = "alwaysOn";
	EditorFrameMain.borderMovable = "alwaysOn";
	EditorFrameMain.borderWidth = $LabGui_EditorFrameMain_BorderWidth;
	EditorFrameMain.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
// Plugin GuiFrameSetCtrl Functions
//==============================================================================
//==============================================================================

//==============================================================================
//Called from Toolbar and TerrainManager
function Lab::togglePluginTools(%this) {
	if (getWordCount(EditorFrameMain.columns) > 1) {
		%this.hidePluginTools();
		return false;
	} else if (!Lab.currentEditor.useTools) {
		return false;
	} else {
		%this.showPluginTools();
		return true;
	}
}
//------------------------------------------------------------------------------

//==============================================================================
//Called from EditorPlugin::activateGui
function Lab::checkPluginTools(%this) {
	%currentPlugin = Lab.currentEditor;

	if (!%currentPlugin.useTools) {
		%this.hidePluginTools();
		return;
	}

	if (getWordCount(EditorFrameMain.columns) <= 1) {
		%this.showPluginTools();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::hidePluginTools(%this) {
	EditorFrameMain.lastToolsCol = getWord(EditorFrameMain.columns,1);
	EditorFrameMain.columns = "0";
	EditorFrameMain.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::showPluginTools(%this) {
	Lab.setupEditorFrameMain();
}
//------------------------------------------------------------------------------