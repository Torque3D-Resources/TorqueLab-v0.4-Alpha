//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================
$SEP_AutoGroupActiveSystem = true;
//==============================================================================
function Scene::setActiveSimGroup( %this, %group ) {
	devLog("setActiveSimGroup",%group);

	if (!isObject(%group))
		return;

	if (!%group.isMemberOfClass("SimGroup"))
		return;

	if (Scene.activeSimGroup.getId() $= %group.getId())
		return;

	%currentGroup = Scene.activeSimGroup;
	Scene.treeUnmarkObject(%currentGroup);
	Scene.activeSimGroup = %group;
	devLog("setActiveSimGroup END",%group,%this.activeSimGroup);
	Scene.ActiveGroup = %group;
	Scene.treeMarkObject(%group);
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::getActiveSimGroup( %this) {
	if (!isObject(%this.activeSimGroup))
		%this.setActiveSimGroup(MissionGroup);
	else if (!%this.activeSimGroup.isMemberOfClass("SimGroup"))
		%this.setActiveSimGroup(MissionGroup);

	return %this.activeSimGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::getNewObjectGroup( %this ) {
	return %this.objectGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::setNewObjectGroup( %this, %group ) {
	devLog("setNewObjectGroup",%group);

	if( %this.objectGroup ) {
		%oldItemId = SceneitorTree.findItemByObjectId( %this.objectGroup );

		if( %oldItemId > 0 )
			SceneitorTree.markItem( %oldItemId, false );
	}

	if (!isObject(%group))return;

	%group = %group.getID();
	%this.objectGroup = %group;
	%itemId = SceneitorTree.findItemByObjectId( %group );
	SceneitorTree.markItem( %itemId );
}
//------------------------------------------------------------------------------
