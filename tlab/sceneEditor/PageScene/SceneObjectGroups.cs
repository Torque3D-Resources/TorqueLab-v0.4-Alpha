//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================
$SEP_AutoGroupActiveSystem = true;
//==============================================================================
function SceneEd::setActiveSimGroup( %this, %group ) {
	devLog("OLD SceneEd::setActiveSimGroup",%group);
	return;
	if (!isObject(%group))
		return;

	if (!%group.isMemberOfClass("SimGroup"))
		return;

	%this.activeSimGroup = %group;
	devLog("setActiveSimGroup END",%group,%this.activeSimGroup);
	SceneEd.ActiveGroup = %group;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEd::getActiveSimGroup( %this) {
	devLog("OLD SceneEd::getActiveSimGroup",%group);
	return;
	if (!isObject(%this.activeSimGroup))
		%this.setActiveSimGroup(MissionGroup);
	else if (!%this.activeSimGroup.isMemberOfClass(SimGroup))
		%this.setActiveSimGroup(MissionGroup);

	return %this.activeSimGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEd::getNewObjectGroup( %this ) {
	devLog("OLD SceneEd::getNewObjectGroup",%group);
	return;
	return %this.objectGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEd::setNewObjectGroup( %this, %group ) {
	devLog("OLD SceneEd::setNewObjectGroup",%group);
	return;
	if( %this.objectGroup ) {
		%oldItemId = SceneEditorTree.findItemByObjectId( %this.objectGroup );

		if( %oldItemId > 0 )
			SceneEditorTree.markItem( %oldItemId, false );
	}

	if (!isObject(%group))return;

	%group = %group.getID();
	%this.objectGroup = %group;
	%itemId = SceneEditorTree.findItemByObjectId( %group );
	SceneEditorTree.markItem( %itemId );
}
//------------------------------------------------------------------------------