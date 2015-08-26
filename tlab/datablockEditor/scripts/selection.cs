//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
//Called from EditorInspectorBase to open selected datablock
function DatablockEditorPlugin::openDatablock( %this, %datablock ) {
	// EditorGui.setEditor( DatablockEditorPlugin );
	%this.selectDatablock( %datablock );
	DatablockEditorTreeTabBook.selectedPage = 0;
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::onObjectSelected( %this, %object ) {
	// Select datablock of object if this is a GameBase object.
	if( %object.isMemberOfClass( "GameBase" ) )
		%this.selectDatablock( %object.getDatablock() );
	else if( %object.isMemberOfClass( "SFXEmitter" ) && isObject( %object.track ) )
		%this.selectDatablock( %object.track );
	else if( %object.isMemberOfClass( "LightBase" ) && isObject( %object.animationType ) )
		%this.selectDatablock( %object.animationType );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::getNumSelectedDatablocks( %this ) {
	return DatablockEditorTree.getSelectedItemsCount();
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::getSelectedDatablock( %this, %index ) {
	%tree = DatablockEditorTree;

	if( !%tree.getSelectedItemsCount() )
		return 0;

	if( !%index )
		%id = %tree.getSelectedItem();
	else
		%id = getWord( %tree.getSelectedItemList(), %index );

	return %tree.getItemValue( %id );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::resetSelectedDatablock( %this ) {
	DatablockEditorTree.clearSelection();
	DatablockEditorInspector.inspect(0);
	DatablockEditorInspectorWindow-->DatablockFile.setText("");
	EditorGuiStatusBar.setSelection( "" );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::selectDatablockCheck( %this, %datablock ) {
	if( %this.selectedDatablockIsDirty() )
		%this.showSaveDialog( %datablock );
	else
		%this.selectDatablock( %datablock );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::selectDatablock( %this, %datablock, %add, %dontSyncTree ) {
	if( %add )
		DatablockEditorInspector.addInspect( %datablock );
	else
		DatablockEditorInspector.inspect( %datablock );

	if( !%dontSyncTree ) {
		%id = DatablockEditorTree.findItemByValue( %datablock.getId() );

		if( !%add )
			DatablockEditorTree.clearSelection();

		DatablockEditorTree.selectItem( %id, true );
		DatablockEditorTree.scrollVisible( %id );
	}

	%this.syncDirtyState();
	// Update the filename text field.
	%numSelected = %this.getNumSelectedDatablocks();
	%fileNameField = DatablockEditorInspectorWindow-->DatablockFile;

	if( %numSelected == 1 ) {
		%fileName = %datablock.getFilename();

		if( %fileName !$= "" )
			%fileNameField.setText( %fileName );
		else
			%fileNameField.setText( $DATABLOCK_EDITOR_DEFAULT_FILENAME );
	} else {
		%fileNameField.setText( "" );
	}
	
	DbEd.activeDatablock = %datablock;
	
	if (DbEd.isMethod("build"@%datablock.getClassName()@"Params")){
		show(DbEd_EditorStack);
		eval("DbEd.build"@%datablock.getClassName()@"Params();");		
		hide(DbEd_NoConfigPill);
	}
	else
	{
		DbEd_EditorStack.clear();		
		show(DbEd_NoConfigPill);
		%text = "There's no predefined settings for datablock of this class:\c1" SPC
		%datablock.getClassName() @". You can create one by using one of the current as reference." SPC
		"You can find those predefined datablocks in tlab> DatablockEditor> Editor> ClassParams> folder.";
		DbEd_NoConfigPill-->infoText.setText(%text); 		
	}
	

	EditorGuiStatusBar.setSelection( %this.getNumSelectedDatablocks() @ " Datablocks Selected" );
}
//------------------------------------------------------------------------------
//==============================================================================
function DatablockEditorPlugin::unselectDatablock( %this, %datablock, %dontSyncTree ) {
	DatablockEditorInspector.removeInspect( %datablock );

	if( !%dontSyncTree ) {
		%id = DatablockEditorTree.findItemByValue( %datablock.getId() );
		DatablockEditorTree.selectItem( %id, false );
	}

	%this.syncDirtyState();
	// If we have exactly one selected datablock remaining, re-enable
	// the save-as button.
	%numSelected = %this.getNumSelectedDatablocks();

	if( %numSelected == 1 ) {
		DatablockEditorInspectorWindow-->saveAsButton.setActive( true );
		%fileNameField = DatablockEditorInspectorWindow-->DatablockFile;
		%fileNameField.setText( %this.getSelectedDatablock().getFilename() );
		%fileNameField.setActive( true );
	}

	EditorGuiStatusBar.setSelection( %this.getNumSelectedDatablocks() @ " Datablocks Selected" );
}
//------------------------------------------------------------------------------