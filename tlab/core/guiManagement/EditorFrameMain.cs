//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// TorqueLab Plugin Tools Container (Side settings area)
//==============================================================================

function Lab::loadEditorFrameMainDefaults(%this) {
	0.8055
	$LabGui_EditorFrameMain_Column_Thin = "0.8055";
	$LabGui_EditorFrameMain_Column_Normal = "0.75";
	$LabGui_EditorFrameMain_Column_Large = "0.6666";
	$LabGui_EditorFrameMain_SizeMode = "Normal";
	$LabGui_EditorFrameMain_BorderWidth = "2";
	
	$LabGui_EditorFrameMain_Locked = true;
	
	$LabGui_ForceToolsSize = true;
	
}
Lab.loadEditorFrameMainDefaults();

//==============================================================================
// EditorFrameMain Functions
//==============================================================================
//==============================================================================
function Lab::setupEditorFrameMain(%this,%defaultSize,%mode) {
	EditorFrameMain.frameMinExtent(1,280,100);
	
	if (%defaultSize !$= ""){
		if (%mode $= "")
			%mode = "Normal";
		$LabGui_EditorFrameMain_Column_[%mode] = mClamp(%defaultSize,0,1);
	}
	
	%columnRatio = $LabGui_EditorFrameMain_Column_[$LabGui_EditorFrameMain_SizeMode];
	%sideCol = EditorFrameMain.lastToolsCol;
	if (%sideCol $= "" || $LabGui_ForceToolsSize )
		%sideCol = mCeil(EditorFrameMain.extent.x * %columnRatio);

	EditorFrameMain.columns =  
	EditorFrameMain.columns = "0" SPC %sideCol;
	EditorFrameMain.updateSizes();
	
	if ($LabGui_EditorFrameMain_Locked && EditorFrameMain.borderWidth !$= "0")
		%this.lockEditorFrameMain(true)
	else if (!$LabGui_EditorFrameMain_Locked && EditorFrameMain.borderWidth $= "0")
		%this.lockEditorFrameMain(false)
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::lockEditorFrameMain(%this,%locked) {
	if (%locked){
		EditorFrameMain.borderEnable = "alwaysOff";
		EditorFrameMain.borderMovable = "alwaysOff";
		EditorFrameMain.borderWidth = "0";
		return;
	}
	EditorFrameMain.borderEnable = "dynamic";
	EditorFrameMain.borderMovable = "dynamic";
	EditorFrameMain.borderWidth = $LabGui_EditorFrameMain_BorderWidth;		
}
//------------------------------------------------------------------------------
//==============================================================================
// Plugin GuiFrameSetCtrl Functions
//==============================================================================
//==============================================================================
function Lab::checkPluginTools(%this) {
	%currentPlugin = Lab.currentEditor;
	
	%toolsGui = %currentPlugin.toolsGui;
	
	
	if (isObject(%toolsGui))
	{
	}
	if (!%currentPlugin.useTools){
		%this.hidePluginTools();
		return;
	}
	if (getWordCount(EditorFrameMain.columns) <= 1){
		%this.showPluginTools();
	}
	
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::togglePluginTools(%this) {
	if (getWordCount(EditorFrameMain.columns) > 1){
		%this.hidePluginTools();
		return false;
	}
	else {
		%this.showPluginTools();
		return true;
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::checkPluginTools(%this) {
	%currentPlugin = Lab.currentEditor;
	if (!%currentPlugin.useTools){
		%this.hidePluginTools();
		return;
	}
	if (getWordCount(EditorFrameMain.columns) <= 1){
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