//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

//==============================================================================
//SceneTrees - MarkObject/UnMarkObject (Used for active SimGroup)
//==============================================================================
//==============================================================================
function Scene::treeMarkObject(%this, %obj) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.markItem(%item);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::treeUnmarkObject(%this, %obj) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.markItem(%item,false);
	}
}
//------------------------------------------------------------------------------

//==============================================================================
//SceneTrees - Add an object to selection
function Scene::treeAddSelection(%this, %obj,%isLast) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.addSelection(%item,%isLast);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
//SceneTrees - Remove an object from selection
function Scene::treeRemoveSelection(%this, %obj) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.removeSelection(%item);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
//SceneTrees - Select/Unselect an object
//==============================================================================
//==============================================================================
function Scene::treeSelectObject(%this, %obj) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.selectItem(%item,true);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::treeUnselectObject(%this, %obj) {
	foreach$(%tree in Scene.sceneTrees) {
		%item = %tree.findItemByObjectId(%obj.getId());

		if (%item $= "-1")
			continue;

		%tree.selectItem(%item,false);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// SceneTrees - Clear Selection
function Scene::treeClearSelection(%this) {
	foreach$(%tree in Scene.sceneTrees) {
		%tree.clearSelection();
	}
}
//------------------------------------------------------------------------------

