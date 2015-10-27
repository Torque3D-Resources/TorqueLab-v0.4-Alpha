//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//SceneBrowserTree.rebuild
function SceneBrowserTree::rebuild( %this ) {
	%this.setRoot();
}

function SceneBrowserTree::setRoot( %this,%simSet ) {
	if (%simSet $= "")
		%simSet = MissionGroup;
	
	if (!isObject(%this.currentSet))
		%this.currentSet = %this;
		
	if (%simSet.getId() !$= %this.currentSet.getId()){
		%this.clear();
		%this.open(%simSet);
	}
	%this.currentSet = %simSet;
	%this.buildVisibleTree();	
}

/*
/// @name EditorPlugin Methods
/// @{
function SceneBrowserTree::handleRenameObject( %this, %name, %obj ) {
	if (!isObject(%obj))
		return;
	
	%field = ( %this.renameInternal ) ? "internalName" : "name";
	%isDirty = LabObj.set(%obj,%field,%name);
	info("Group:",%obj,"Is Dirty",%isDirty);
	
}


//-----------------------------------------------------------------------------

function SceneBrowserTree::onDeleteSelection( %this ) {
	%this.undoDeleteList();
}

function SceneBrowserTree::onDeleteObject( %this, %object ) {
	// Don't delete locked objects
	if( %object.locked )
		return true;

	if( %object == SceneEd.objectGroup )
		SceneEd.setNewObjectGroup( MissionGroup );

	// Append it to our list.
	%this.undoDeleteList = %this.undoDeleteList TAB %object;
	// We're gonna delete this ourselves in the
	// completion callback.
	return true;
}

function SceneBrowserTree::onObjectDeleteCompleted( %this ) {
	// This can be called when a deletion is attempted but nothing was
	// actually deleted ( cannot delete the root of the tree ) so only submit
	// the undo if we really deleted something.
	if ( %this.undoDeleteList !$= "" )
		MEDeleteUndoAction::submit( %this.undoDeleteList );
		
	Scene.onObjectDeleteCompleted();

	
}
function SceneBrowserTree::onClearSelected(%this) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onClearSelected cancelled for filtering");
		return;
	}
	Scene.onClearSelected();
	
}

function SceneBrowserTree::onInspect(%this, %obj) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onInspect cancelled for filtering");
		return;
	}
	Scene.onInspect(%obj);
}



function SceneBrowserTree::onAddSelection(%this, %obj, %isLastSelection) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onAddSelection cancelled for filtering");
		return;
	}
	Scene.onAddSelection(%obj, %isLastSelection);
	
}
function SceneBrowserTree::onRemoveSelection(%this, %obj) {
	Scene.onRemoveSelection(%obj);
	
}
function SceneBrowserTree::onSelect(%this, %obj) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onSelect cancelled for filtering");
		return;
	}
	Scene.onSelect(%obj);	
}

function SceneBrowserTree::onUnselect(%this, %obj) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onUnselect cancelled for filtering");
		return;
	}
	Scene.onUnselect(%obj);
	
}

function SceneBrowserTree::onDragDropped(%this) {
	Scene.setDirty();
	
}

function SceneBrowserTree::onAddGroupSelected(%this, %group) {
	if (%this.noCallbacks){
		devLog("SceneBrowserTree::onAddGroupSelected cancelled for filtering");
		return;
	}
	SceneEd.setNewObjectGroup(%group);
}

function SceneBrowserTree::onRightMouseUp( %this, %itemId, %mouse, %obj ) {
	Scene.showSceneTreeItemContext(%this,%itemId, %mouse, %obj );	
}
*/