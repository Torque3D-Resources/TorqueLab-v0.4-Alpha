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
function SBP::initPageBuilder( %this ) {	
	if (!isObject(SBP_SceneMeshesSet))
		%this.meshSet = newSimSet("SBP_SceneMeshesSet");
	
	SBP.getSceneMeshes();
	SBP_AssetTree.clear();
	SBP_AssetTree.open(SBP_SceneMeshesSet);
	SBP_AssetTree.buildVisibleTree();	
	
}
//------------------------------------------------------------------------------

//==============================================================================
// SBP.initPageBuilder
function SBP::getSceneMeshes( %this ) {	
	SBP_SceneMeshesSet.clear();
	%staticList = getMissionObjectClassList("TSStatic");
	%prefabList = getMissionObjectClassList("Prefab");
	%list = %staticList SPC %prefabList;
	foreach$(%obj in %list)
		SBP_SceneMeshesSet.add(%obj);
}
//---------------------------------------------------------------------