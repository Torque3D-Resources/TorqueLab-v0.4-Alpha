//==============================================================================
// TorqueLab -> Fonts Setup
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TerrainMatDlg_SaveWhenUnselected = true;
$TerrainMatDlg_CreateFromClone = true;
//==============================================================================
function TerrainMaterialDlg::show( %this, %matIndex, %terrMat, %onApplyCallback ) {
	Canvas.pushDialog( %this );
	%this.matIndex = %matIndex;
	%this.onApplyCallback = %onApplyCallback;
	%this.selectObjectInTree(%terrMat,true);
	
	TMatManager_OptionalSettingRollout.expanded = false;
	%gcoverMenu = %this-->groundCoverMaterialSrcMenu;
	%gcoverMenu.clear();
	%gcoverMenu.add("Select a Source",0);
	foreach(%mat in TerrainMaterialSet){
		%gcoverMenu.add(%mat.internalName,%mat.getId());
	}
}
//------------------------------------------------------------------------------

//==============================================================================
//Show terrain material information (if no ObjectId, user need to select one)
function TerrainMaterialDlg::showByObjectId( %this, %matObjectId, %onApplyCallback ) {
	Canvas.pushDialog( %this );
	%this.matIndex = -1;
	%this.onApplyCallback = %onApplyCallback;
	%this.selectObjectInTree(%matObjectId);
	
}
//------------------------------------------------------------------------------

//==============================================================================
function TerrainMaterialDlg::onWake( %this ) {

	if( !isObject( ETerrainMaterialPersistMan ) )
		new PersistenceManager( ETerrainMaterialPersistMan );

	if( !isObject( TerrainMaterialDlgNewGroup ) )
		new SimGroup( TerrainMaterialDlgNewGroup );

	if( !isObject( TerrainMaterialDlgDeleteGroup ) )
		new SimGroup( TerrainMaterialDlgDeleteGroup );

	if (!$TerrainMaterialDlg_Initialized)
		%this.initFiltersData();

	// Snapshot the materials.
	%this.snapshotMaterials();
	// Refresh the material list.
	%this.setFilteredMaterialsSet();
	$TerrainMaterialDlg_Initialized = true;
	TerrainMatDlg_MaterialInfo.expanded = false;
	TerrainMatDlg_Cloning.expanded = false;
	//%this.activateMaterialCtrls( true );
}
//------------------------------------------------------------------------------

//==============================================================================
function TerrainMaterialDlg::onSleep( %this ) {
	if( isObject( TerrainMaterialDlgSnapshot ) )
		TerrainMaterialDlgSnapshot.delete();

	%this-->matLibTree.clear();
}
//------------------------------------------------------------------------------
//==============================================================================
function TMD_MaterialTree::onMouseUp( %this ,%itemId,%click) {
	if (%click $= "2")
		TerrainMaterialDlg.dialogApply(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainMaterialDlg::dialogApply( %this,%closeDlg ) {
	// Move all new materials we have created to the root group.
	%newCount = TerrainMaterialDlgNewGroup.getCount();

	for( %i = 0; %i < %newCount; %i ++ )
		RootGroup.add( TerrainMaterialDlgNewGroup.getObject( %i ) );

	// Finalize deletion of all removed materials.
	%deletedCount = TerrainMaterialDlgDeleteGroup.getCount();

	for( %i = 0; %i < %deletedCount; %i ++ ) {
		%mat = TerrainMaterialDlgDeleteGroup.getObject( %i );
		ETerrainMaterialPersistMan.removeObjectFromFile( %mat );
		%matIndex = ETerrainEditor.getMaterialIndex( %mat.internalName );

		if( %matIndex != -1 ) {
			ETerrainEditor.removeMaterial( %matIndex );
			EPainter.updateLayers();
		}

		%mat.delete();
	}

	// Make sure we save any changes to the current selection.
	%this.saveDirtyMaterial( %this.activeMat );
	// Save all changes.
	ETerrainMaterialPersistMan.saveDirty();
	// Delete the snapshot.
	if( isObject( TerrainMaterialDlgSnapshot ) )
		TerrainMaterialDlgSnapshot.delete();

	// Remove ourselves from the canvas.
	if (%closeDlg)
		Canvas.popDialog( TerrainMaterialDlg );

	call( %this.onApplyCallback, %this.activeMat, %this.matIndex );
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainMaterialDlg::dialogCancel( %this ) {
	// Restore material properties we have changed.
	%this.restoreMaterials();
	// Clear the persistence manager state.
	ETerrainMaterialPersistMan.clearAll();
	// Delete all new object we have created.
	TerrainMaterialDlgNewGroup.clear();
	// Restore materials we have marked for deletion.
	%deletedCount = TerrainMaterialDlgDeleteGroup.getCount();

	for( %i = 0; %i < %deletedCount; %i ++ ) {
		%mat = TerrainMaterialDlgDeleteGroup.getObject( %i );
		%mat.parentGroup = RootGroup;
		TerrainMaterialSet.add( %mat );
	}

	Canvas.popDialog( TerrainMaterialDlg );
}
//------------------------------------------------------------------------------

//==============================================================================
function TerrainMaterialDlg::_selectTextureFileDialog( %this, %defaultFileName ) {
	if( $Pref::TerrainEditor::LastPath $= "" )
		$Pref::TerrainEditor::LastPath = "art/terrains/";
	%defaultFile = %defaultFileName;
	if (!isFile(%defaultFile))
		%defaultFile = $Pref::TerrainEditor::LastPath;
	
	if (TerrainPainterTools.defaultTexturesFolder !$= "")
		%defaultPath = TerrainPainterTools.defaultTexturesFolder;
	else
		%defaultPath = $Pref::TerrainEditor::LastPath;

	%dlg = new OpenFileDialog() {
		Filters        = $TerrainEditor::TextureFileSpec;
		DefaultPath    = $Pref::TerrainEditor::LastPath;
		DefaultFile    = %defaultFile;
		ChangePath     = false;
		MustExist      = true;
	};
	%ret = %dlg.Execute();

	if ( %ret ) {
		$Pref::TerrainEditor::LastPath = filePath( %dlg.FileName );
		%file = %dlg.FileName;
	}

	%dlg.delete();

	if ( !%ret )
		return;

	%file = filePath(%file) @ "/" @ fileBase(%file);
	return %file;
}
//------------------------------------------------------------------------------



//==============================================================================
function TMatManager_GroundCoverSrcMenu::onSelect( %this, %matId,%matInternalName ) {
	%terrMat = %this.activeMat;
	if (!isObject(%matId) || !isObject(%terrMat))
		return;
	
	labMsg("Not implemented","This feature have been blocked for now and will need some enhancements to work properly");
	
}
//------------------------------------------------------------------------------

