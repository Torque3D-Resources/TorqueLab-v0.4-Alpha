//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function SceneObjectsTree::onDefineIcons( %this ) {
	%icons = "tlab/gui/icons/treeview_assets/default:" @
				"tlab/gui/icons/treeview_assets/folderclosed:" @
				"tlab/gui/icons/treeview_assets/groupclosed:" @
				"tlab/gui/icons/treeview_assets/folderopen:" @
				"tlab/gui/icons/treeview_assets/groupopen:" @
				"tlab/gui/icons/treeview_assets/hidden:" @
				"tlab/gui/icons/treeview_assets/shll_icon_passworded_hi:" @
				"tlab/gui/icons/treeview_assets/shll_icon_passworded:" @
				"tlab/gui/icons/treeview_assets/default";
	%this.buildIconTable(%icons);
}

function SceneObjectsTree::onAdd( %this ) {
	Scene.SceneTrees = strAddWord(Scene.SceneTrees,%this.getId());
}

/// @name EditorPlugin Methods
/// @{
function SceneObjectsTree::handleRenameObject( %this, %name, %obj ) {
	devLog(" SceneObjectsTree::handleRenameObject(",%name, %obj);

	if (!isObject(%obj))
		return;

	%field = ( %this.renameInternal ) ? "internalName" : "name";
	%isDirty = LabObj.set(%obj,%field,%name);
	info("Group:",%obj,"Is Dirty",%isDirty);
}
function SceneEditorTree::handleRenameObject( %this, %name, %obj ) {
	devLog(" SceneEditorTree::handleRenameObject(",%name, %obj);

	if (!isObject(%obj))
		return;

	%field = ( %this.renameInternal ) ? "internalName" : "name";
	%isDirty = LabObj.set(%obj,%field,%name);
	info("Group:",%obj,"Is Dirty",%isDirty);
}

//-----------------------------------------------------------------------------

function SceneObjectsTree::onDeleteSelection( %this ) {
	%this.undoDeleteList = "";
}

function SceneObjectsTree::onDeleteObject( %this, %object ) {
	// Don't delete locked objects
	if( %object.locked )
		return true;

	if( %object == SceneEd.objectGroup )
		Scene.setNewObjectGroup( MissionGroup );

	// Append it to our list.
	%this.undoDeleteList = %this.undoDeleteList TAB %object;
	// We're gonna delete this ourselves in the
	// completion callback.
	return true;
}

function SceneObjectsTree::onObjectDeleteCompleted( %this ) {
	// This can be called when a deletion is attempted but nothing was
	// actually deleted ( cannot delete the root of the tree ) so only submit
	// the undo if we really deleted something.
	if ( %this.undoDeleteList !$= "" )
		MEDeleteUndoAction::submit( %this.undoDeleteList );

	Scene.onObjectDeleteCompleted();
}
function SceneObjectsTree::onClearSelected(%this) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onClearSelected cancelled for filtering");
		return;
	}

	Scene.onClearSelected();
}

function SceneObjectsTree::onInspect(%this, %obj) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onInspect cancelled for filtering");
		return;
	}

	//if (isObject(%this.myInspector)){
	//devLog("Updating tree owned inspector",%this.myInspector);
	//%this.myInspector.inspect(%obj);
	//}
	Scene.onInspect(%obj);
}



function SceneObjectsTree::onAddSelection(%this, %obj, %isLastSelection) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onAddSelection cancelled for filtering");
		return;
	}

	Scene.AddObjectToSelection(%obj, %isLastSelection);
}
function SceneObjectsTree::onRemoveSelection(%this, %obj) {
	Scene.unselectObject(%obj,%this);
	//if (isObject(%this.myInspector)){
	//devLog("RemoveInspect tree owned inspector",%this.myInspector);
	//%this.myInspector.removeInspect(%obj);
	//}
//	SceneInspector.removeInspect( %obj );
}
function SceneObjectsTree::onSelect(%this, %obj) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onSelect cancelled for filtering");
		return;
	}

	if (%obj.getClassName() $= "SimGroup") {
		%item = %this.findItemByObjectId(%obj.getId());
		DevLog(%item,"Group slected with child count:",%obj.getCount());
		//	%this.expandItem(%item,true);
		//	Scene.selectObjectGroup(%obj);
	}

	EWorldEditor.selectObject(%obj);
	Scene.onSelect(%obj);
	//EWorldEditor.selectObject(%obj);
	//Scene.onSelect(%obj);
	return;

	if (!$SceneEditor_AutomaticObjectGroup && %this.autoGroups) {
		%this.autoGroups = false;
		Scene.setNewObjectGroup( MissionGroup );
		return;
	}

	%this.autoGroups = true;

	if (%obj.getClassName() $= "SimGroup") {
		%objectGroup = %obj;
	} else if (isObject(%obj.parentGroup)) {
		%objectGroup = %obj.parentGroup;
	} else {
		%objectGroup = MissionGroup;
	}

	Scene.setNewObjectGroup( %objectGroup );
}

function SceneObjectsTree::onUnselect(%this, %obj) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onUnselect cancelled for filtering");
		return;
	}

	Scene.unselectObject(%obj,%this);
}

function SceneObjectsTree::onDragDropped(%this) {
	Scene.setDirty();
}

function SceneObjectsTree::onAddGroupSelected(%this, %group) {
	if (%this.noCallbacks) {
		devLog("SceneBrowserTree::onAddGroupSelected cancelled for filtering");
		return;
	}

	Scene.setNewObjectGroup(%group);
}

function SceneObjectsTree::onRightMouseUp( %this, %itemId, %mouse, %obj ) {
	devLog("SceneObjectsTree Called");
	Scene.showSceneTreeItemContext(%this,%itemId, %mouse, %obj );
}

function SceneObjectsTree::isValidDragTarget( %this, %id, %obj ) {
	if( %obj.isMemberOfClass( "Path" ) )
		return EWorldEditor.areAllSelectedObjectsOfType( "Marker" );

	if( %obj.name $= "CameraBookmarks" )
		return EWorldEditor.areAllSelectedObjectsOfType( "CameraBookmark" );
	else
		return ( %obj.getClassName() $= "SimGroup" );
}

function SceneObjectsTree::onBeginReparenting( %this ) {
	if( isObject( %this.reparentUndoAction ) )
		%this.reparentUndoAction.delete();

	%action = UndoActionReparentObjects::create( %this );
	%this.reparentUndoAction = %action;
}

function SceneObjectsTree::onReparent( %this, %obj, %oldParent, %newParent ) {
	%this.reparentUndoAction.add( %obj, %oldParent, %newParent );
}

function SceneObjectsTree::onEndReparenting( %this ) {
	%action = %this.reparentUndoAction;
	%this.reparentUndoAction = "";

	if( %action.numObjects > 0 ) {
		if( %action.numObjects == 1 )
			%action.actionName = "Reparent Object";
		else
			%action.actionName = "Reparent Objects";

		%action.addToManager( Editor.getUndoManager() );
		EWorldEditor.syncGui();
	} else
		%action.delete();
}

function SceneObjectsTree::update( %this ) {
	%this.buildVisibleTree( false );
}
