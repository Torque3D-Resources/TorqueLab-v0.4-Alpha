//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SceneEditorDialogs::onPreEditorSave( %this ) {
	%c1 = SEP_ScatterSky_Custom-->StackA SPC SEP_ScatterSky_Custom-->StackB;
	%c1 = %c1 SPC SEP_LegacySkyProperties-->StackA SPC SEP_LegacySkyProperties-->StackB;
	%c1 = %c1 SPC SEP_CloudLayer-->StackA SPC SEP_CloudLayer-->StackB;

	foreach$(%ctrl in %c1)
		%ctrl.clear();

	if (isObject(SEP_PostFXManager_Clone-->MainContainer))
		EPostFxManager.moveFromGui();

	SEP_GroundCoverLayerArray.clear();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorDialogs::onPostEditorSave( %this ) {
	//EPostFxManager.moveToGui(SEP_PostFXManager_Clone);
}
//------------------------------------------------------------------------------


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
	SceneEditorTools.removeToolClones();
	ETransformTool.cloneToCtrl(SEP_ToolsStack);
	ECloneTool.cloneToCtrl(SEP_CloneToolsStack);
	return;
	SEP_ToolsStack.add(ETransformTool-->toolsStack.deepClone());
	SEP_CloneToolsStack.add(ECloneTool-->cloneTools.deepClone());
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
function SceneEditorTools::onObjectAdded( %this,%obj ) {
	//devLog("SceneEditorTools::onObjectAdded:",%obj);
}
function SceneEditorTools::onObjectRemoved( %this,%obj ) {
	//devLog("SceneEditorTools::onObjectRemoved:",%obj);
}