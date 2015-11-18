//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function Scene::onInspect(%this, %obj) {
	SceneInspector.inspect(%obj);
	return;

	foreach$(%inspector in Scene.baseInspectors) {
		devLog("inspect Scene Inspectprs inspector",%inspector);
		%inspector.inspect(%obj);
	}
}

function Scene::onRemoveInspect(%this, %obj) {
	SceneInspector.removeInspect(%obj);
	return;

	foreach$(%inspector in Scene.baseInspectors) {
		devLog("RemoveInspect Scene Inspectprs inspector",%inspector);
		%inspector.removeInspect(%obj);
	}
}

function Scene::onAddInspect(%this, %obj,%isLast) {
	SceneInspector.addInspect(%obj,%isLast);
	return;

	foreach$(%inspector in Scene.baseInspectors) {
		devLog("addInspect Scene Inspectprs inspector",%inspector);
		%inspector.addInspect(%obj,%isLast);
	}
}