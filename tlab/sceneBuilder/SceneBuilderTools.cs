//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function SceneBuilderTools::onVisible( %this,%state ) {	
	if (!%state){
		devLog("SceneBuilderTools is hidden, set default Vis",EVisibilityLayers.currentPresetFile);
		EVisibilityLayers.loadPresetFile(EVisibilityLayers.currentPresetFile);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderTools::onPreEditorSave( %this ) {	
	%this.removeToolClones();
	if (getWordCount(SceneBuilderTools.rows) <= 1)
		SceneBuilderPlugin.toggleBuildMode();
		
	AssetIconArray.clear();
	

}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderTools::onPostEditorSave( %this ) {
	%this.getToolClones();
}
//------------------------------------------------------------------------------
//==============================================================================

function SceneBuilderTools::removeToolClones( %this ) {
	delObj(SceneBuilderTools-->toolsStack);	
	delObj(SceneBuilderTools-->cloneTools);

}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderTools::getToolClones( %this ) {	
	SceneBuilderTools.removeToolClones();
	
	ETransformTool.cloneToCtrl(SBP_ToolsStack);
	ECloneTool.cloneToCtrl(SBP_CloneToolsStack);
	return;
	SBP_ToolsStack.add(ETransformTool-->toolsStack.deepClone());
	SBP_CloneToolsStack.add(ECloneTool-->cloneTools.deepClone());
	
	SBP_ToolsStack.add(ETransformTool-->toolsStack.deepClone());
	SBP_CloneToolsStack.add(ECloneTool-->cloneTools.deepClone());
}
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// SceneBuilderTools Frame Set Scripts
//==============================================================================

function SceneBuilderTools::setMeshRootFolder( %this,%folder ) {
	devLog("SceneBuilderTools::setMeshRootFolder:",%folder);
	%this.meshRootFolder = %folder;
	%this-->meshRootFolder.setText(%folder);
}
//------------------------------------------------------------------------------
//==============================================================================
// SceneBuilderTools.validateMeshRootFolder($ThisControl);
function SceneBuilderTools::validateMeshRootFolder( %this,%ctrl ) {
	devLog("SceneBuilderTools::setMeshRootFolder:",%ctrl.getValue());
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderTools::getMeshRootFolder( %this ) {
	getFolderName("","SceneBuilderTools.setMeshRootFolder","art/");
}
//------------------------------------------------------------------------------
