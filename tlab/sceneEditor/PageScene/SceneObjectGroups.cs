//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================
$SEP_AutoGroupActiveSystem = true;
//==============================================================================
function SceneEd::setActiveSimGroup( %this, %group ) {
	devLog("setActiveSimGroup",%group);
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
	if (!isObject(%this.activeSimGroup))
		%this.setActiveSimGroup(MissionGroup);
	else if (!%this.activeSimGroup.isMemberOfClass(SimGroup))
		%this.setActiveSimGroup(MissionGroup);

	return %this.activeSimGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEd::getNewObjectGroup( %this ) {
	return %this.objectGroup;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneEd::setNewObjectGroup( %this, %group ) {
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