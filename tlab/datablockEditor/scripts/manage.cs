//==============================================================================
// TorqueLab -> Datablock Editor - Creation and Deletion
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function DatablockEditorPlugin::deleteDatablock( %this ) {
	%tree = DatablockEditorTree;
	// If we have more than single datablock selected,
	// turn our undos into a compound undo.
	%numSelected = %tree.getSelectedItemsCount();

	if( %numSelected > 1 )
		Editor.getUndoManager().pushCompound( "Delete Multiple Datablocks" );

	for( %i = 0; %i < %numSelected; %i ++ ) {
		%id = %tree.getSelectedItem( %i );
		%db = %tree.getItemValue( %id );
		%fileName = %db.getFileName();
		// Remove the datablock from the tree.
		DatablockEditorTree.removeItem( %id );
		// Create undo.
		%action = %this.createUndo( ActionDeleteDatablock, "Delete Datablock" );
		%action.db = %db;
		%action.dbName = %db.getName();
		%action.fname = %fileName;
		%this.submitUndo( %action );

		// Kill the datablock in the file.

		if( %fileName !$= "" )
			%this.PM.removeObjectFromFile( %db );

		UnlistedDatablocks.add( %db );

		// Show some confirmation.

		if( %numSelected == 1 )
			LabMsgOK( "Datablock Deleted", "The datablock (" @ %db.getName() @ ") has been removed from " @
						 "it's file (" @ %db.getFilename() @ ") and upon restart will cease to exist" );
	}

	// Close compound, if we were deleting multiple datablocks.

	if( %numSelected > 1 )
		Editor.getUndoManager().popCompound();

	// Show confirmation for multiple datablocks.

	if( %numSelected > 1 )
		LabMsgOK( "Datablocks Deleted", "The datablocks have been deleted and upon restart will cease to exist." );

	// Clear selection.
	DatablockEditorPlugin.resetSelectedDatablock();
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::createDatablock(%this) {
	%class = DatablockEditorTypeTree.getItemText(DatablockEditorTypeTree.getSelectedItem());

	if( %class !$= "" ) {
		// Need to prompt for a name.
		DatablockEditorCreatePrompt-->CreateDatablockName.setText("Name");
		DatablockEditorCreatePrompt-->CreateDatablockName.selectAllText();
		// Populate the copy source dropdown.
		%list = DatablockEditorCreatePrompt-->CopySourceDropdown;
		%list.clear();
		%list.add( "", 0 );
		%set = DataBlockSet;
		%count = %set.getCount();

		for( %i = 0; %i < %count; %i ++ ) {
			%datablock = %set.getObject( %i );
			%datablockClass = %datablock.getClassName();

			if( !isMemberOfClass( %datablockClass, %class ) )
				continue;

			%list.add( %datablock.getName(), %i + 1 );
		}

		// Set up state of client-side checkbox.
		%clientSideCheckBox = DatablockEditorCreatePrompt-->ClientSideCheckBox;
		%canBeClientSide = DatablockEditorPlugin::canBeClientSideDatablock( %class );
		%clientSideCheckBox.setStateOn( %canBeClientSide );
		%clientSideCheckBox.setActive( %canBeClientSide );
		// Show the dialog.
		canvas.pushDialog( DatablockEditorCreatePrompt, 0, true );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::createPromptNameCheck(%this) {
	%name = DatablockEditorCreatePrompt-->CreateDatablockName.getText();

	if( !Editor::validateObjectName( %name, true ) )
		return;

	// Fetch the copy source and clear the list.
	%copySource = DatablockEditorCreatePrompt-->copySourceDropdown.getText();
	DatablockEditorCreatePrompt-->copySourceDropdown.clear();
	// Remove the dialog and create the datablock.
	canvas.popDialog( DatablockEditorCreatePrompt );
	%this.createDatablockFinish( %name, %copySource );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::createDatablockFinish( %this, %name, %copySource ) {
	%class = DatablockEditorTypeTree.getItemText(DatablockEditorTypeTree.getSelectedItem());

	if( %class !$= "" ) {
		%action = %this.createUndo( ActionCreateDatablock, "Create New Datablock" );

		if( DatablockEditorCreatePrompt-->ClientSideCheckBox.isStateOn() )
			%dbType = "singleton ";
		else
			%dbType = "datablock ";

		if( %copySource !$= "" )
			%eval = %dbType @ %class @ "(" @ %name @ " : " @ %copySource @ ") { canSaveDynamicFields = \"1\"; };";
		else
			%eval = %dbType @ %class @ "(" @ %name @ ") { canSaveDynamicFields = \"1\"; };";

		%res = eval( %eval );
		%action.db = %name.getId();
		%action.dbName = %name;
		%action.fname = $DATABLOCK_EDITOR_DEFAULT_FILENAME;
		%this.submitUndo( %action );
		%action.redo();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::canBeClientSideDatablock( %className ) {
	switch$( %className ) {
	case "SFXProfile" or
			"SFXPlayList" or
			"SFXAmbience" or
			"SFXEnvironment" or
			"SFXState" or
			"SFXDescription" or
			"SFXFMODProject":
		return true;

	default:
		return false;
	}
}
//------------------------------------------------------------------------------
