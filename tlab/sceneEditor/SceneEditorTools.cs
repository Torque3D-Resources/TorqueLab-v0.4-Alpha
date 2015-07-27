//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorTools::onPreEditorSave( %this ) {
	devLog("SceneEditorTools::onPreEditorSave:");
	%this.removeToolClones();
}
function SceneEditorTools::onPostEditorSave( %this ) {
	devLog("SceneEditorTools::onPostEditorSave:");
	%this.getToolClones();
}


function SceneEditorTools::removeToolClones( %this ) {
	devLog("SceneEditorTools::removeToolClones:");
	delObj(SceneEditorTools-->toolsStack);	
	delObj(SceneEditorTools-->cloneTools);

}
function SceneEditorTools::getToolClones( %this ) {
	devLog("SceneEditorTools::getToolClones:");
	%this.removeToolClones();
	
	SEP_ToolsStack.add(ETransformTool-->toolsStack.deepClone());
	SEP_CloneToolsStack.add(ECloneTool-->cloneTools.deepClone());
}

//==============================================================================
// SceneEditorTools Frame Set Scripts
//==============================================================================

function SceneEditorTools::setMeshRootFolder( %this,%folder ) {
	devLog("SceneEditorTools::setMeshRootFolder:",%folder);
	%this.meshRootFolder = %folder;
	%this-->meshRootFolder.setText(%folder);
}
// SceneEditorTools.validateMeshRootFolder($ThisControl);
function SceneEditorTools::validateMeshRootFolder( %this,%ctrl ) {
	devLog("SceneEditorTools::setMeshRootFolder:",%ctrl.getValue());
}
function SceneEditorTools::getMeshRootFolder( %this ) {
	getFolderName("","SceneEditorTools.setMeshRootFolder","art/");
}