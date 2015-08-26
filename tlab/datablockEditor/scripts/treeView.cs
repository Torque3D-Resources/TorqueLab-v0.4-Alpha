//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Add the datablocks to the treeView
function DatablockEditorPlugin::populateTrees(%this) {
	// Populate datablock tree.
	if( %this.excludeClientOnlyDatablocks )
		%set = DataBlockGroup;
	else
		%set = DataBlockSet;

	DatablockEditorTree.clear();

	foreach( %datablock in %set ) {
		%unlistedFound = false;
		%id = %datablock.getId();

		foreach( %obj in UnlistedDatablocks )
			if( %obj.getId() == %id ) {
				%unlistedFound = true;
				break;
			}

		if( %unlistedFound )
			continue;

		%this.addExistingItem( %datablock, true );
	}

	DatablockEditorTree.sort( 0, true, false, false );
	// Populate datablock type tree.
	%classList = enumerateConsoleClasses( "SimDatablock" );
	DatablockEditorTypeTree.clear();

	foreach$( %datablockClass in %classList ) {
		if(    !%this.isExcludedDatablockType( %datablockClass )
				 && DatablockEditorTypeTree.findItemByName( %datablockClass ) == 0 )
			DatablockEditorTypeTree.insertItem( 0, %datablockClass );
	}

	DatablockEditorTypeTree.sort( 0, false, false, false );
}
//------------------------------------------------------------------------------
//==============================================================================
//Add an existing datablock to the tree
function DatablockEditorPlugin::addExistingItem( %this, %datablock, %dontSort ) {
	%tree = DatablockEditorTree;
	// Look up class at root level.  Create if needed.
	%class = %datablock.getClassName();
	%parentID = %tree.findItemByName( %class );

	if( %parentID == 0 )
		%parentID = %tree.insertItem( 0, %class );

	// If the datablock is already there, don't
	// do anything.

	if( %tree.findItemByValue( %datablock.getId() ) )
		return;

	// It doesn't exist so add it.
	%name = %datablock.getName();

	if( %this.PM.isDirty( %datablock ) )
		%name = %name @ " *";

	%id = DatablockEditorTree.insertItem( %parentID, %name, %datablock.getId() );

	if( !%dontSort )
		DatablockEditorTree.sort( %parentID, false, false, false );

	return %id;
}
//------------------------------------------------------------------------------
//==============================================================================
//Check for some datablock exclusions
function DatablockEditorPlugin::isExcludedDatablockType( %this, %className ) {
	switch$( %className ) {
	case "SimDatablock":
		return true;

	case "SFXTrack": // Abstract.
		return true;

	case "SFXFMODEvent": // Internally created.
		return true;

	case "SFXFMODEventGroup": // Internally created.
		return true;
	}

	return false;
}
//------------------------------------------------------------------------------

//==============================================================================
// TreeView Events
//==============================================================================

//==============================================================================
function DatablockEditorTree::onDeleteSelection( %this ) {
	%this.undoDeleteList = "";
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onDeleteObject( %this, %object ) {
	// Append it to our list.
	%this.undoDeleteList = %this.undoDeleteList TAB %object;
	// We're gonna delete this ourselves in the
	// completion callback.
	return true;
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onObjectDeleteCompleted( %this ) {
	//MEDeleteUndoAction::submit( %this.undoDeleteList );
	// Let the world editor know to
	// clear its selection.
	//EWorldEditor.clearSelection();
	//EWorldEditor.isDirty = true;
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onClearSelected(%this) {
	DatablockEditorInspector.inspect( 0 );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onAddSelection( %this, %id ) {
	%obj = %this.getItemValue( %id );

	if( !isObject( %obj ) )
		%this.selectItem( %id, false );
	else
		DatablockEditorPlugin.selectDatablock( %obj, true, true );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onRemoveSelection( %this, %id ) {
	%obj = %this.getItemValue( %id );

	if( isObject( %obj ) )
		DatablockEditorPlugin.unselectDatablock( %obj, true );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorTree::onRightMouseUp( %this, %id, %mousePos ) {
	%datablock = %this.getItemValue( %id );

	if( !isObject( %datablock ) )
		return;

	if( !isObject( DatablockEditorTreePopup ) )
		new PopupMenu( DatablockEditorTreePopup ) {
		superClass = "MenuBuilder";
		isPopup = true;
		item[ 0 ] = "Delete" TAB "" TAB "DatablockEditorPlugin.selectDatablock( %this.datablockObject ); DatablockEditorPlugin.deleteDatablock( %this.datablockObject );";
		item[ 1 ] = "Jump to Definition in Torsion" TAB "" TAB "EditorOpenDeclarationInTorsion( %this.datablockObject );";
		datablockObject = "";
	};

	DatablockEditorTreePopup.datablockObject = %datablock;

	DatablockEditorTreePopup.showPopup( Canvas );
}
//------------------------------------------------------------------------------