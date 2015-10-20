//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function SceneEditorTools::onPreEditorSave( %this ) {	
	%this.removeToolClones();
	if (getWordCount(SceneEditorTools.rows) <= 1)
		SceneEditorPlugin.toggleBuildMode();
		
	AssetIconArray.clear();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorTools::onPostEditorSave( %this ) {
	%this.getToolClones();
}
//------------------------------------------------------------------------------
//==============================================================================

function SceneEditorTools::removeToolClones( %this ) {
	delObj(SceneEditorTools-->toolsStack);	
	delObj(SceneEditorTools-->cloneTools);

}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorTools::getToolClones( %this ) {	
	%this.removeToolClones();
	
	SEP_ToolsStack.add(ETransformTool-->toolsStack.deepClone());
	SEP_CloneToolsStack.add(ECloneTool-->cloneTools.deepClone());
}
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// SceneEditorTools Frame Set Scripts
//==============================================================================

function SceneEditorTools::setMeshRootFolder( %this,%folder ) {
	devLog("SceneEditorTools::setMeshRootFolder:",%folder);
	%this.meshRootFolder = %folder;
	%this-->meshRootFolder.setText(%folder);
}
//------------------------------------------------------------------------------
//==============================================================================
// SceneEditorTools.validateMeshRootFolder($ThisControl);
function SceneEditorTools::validateMeshRootFolder( %this,%ctrl ) {
	devLog("SceneEditorTools::setMeshRootFolder:",%ctrl.getValue());
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorTools::getMeshRootFolder( %this ) {
	getFolderName("","SceneEditorTools.setMeshRootFolder","art/");
}
//------------------------------------------------------------------------------
