//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function Scene::onInspect(%this, %obj) {
	foreach$(%inspector in Scene.baseInspectors){
		devLog("inspect Scene Inspectprs inspector",%inspector);
		%inspector.inspect(%obj);
	}
}

function Scene::onRemoveInspect(%this, %obj) {
	foreach$(%inspector in Scene.baseInspectors){
		devLog("RemoveInspect Scene Inspectprs inspector",%inspector);
		%inspector.removeInspect(%obj);
	}
}

function Scene::onAddInspect(%this, %obj,%isLast) {
	foreach$(%inspector in Scene.baseInspectors){
		devLog("addInspect Scene Inspectprs inspector",%inspector);
		%inspector.addInspect(%obj,%isLast);
	}
}