//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Add new asset group
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SceneEd::initPageBuilder( %this ) {	
	if (!isObject(SEP_SceneMeshesSet))
		%this.meshSet = newSimSet("SEP_SceneMeshesSet");
	
	SceneEd.getSceneMeshes();
	SEP_AssetTree.clear();
	SEP_AssetTree.open(SEP_SceneMeshesSet);
	SEP_AssetTree.buildVisibleTree();	
	
}
//------------------------------------------------------------------------------

//==============================================================================
// SceneEd.initPageBuilder
function SceneEd::getSceneMeshes( %this ) {	
	SEP_SceneMeshesSet.clear();
	%staticList = getMissionObjectClassList("TSStatic");
	%prefabList = getMissionObjectClassList("Prefab");
	%list = %staticList SPC %prefabList;
	foreach$(%obj in %list)
		SEP_SceneMeshesSet.add(%obj);
}
//---------------------------------------------------------------------