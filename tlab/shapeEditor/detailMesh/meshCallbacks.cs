//==============================================================================
// TorqueLab -> ShapeEditor -> Mounted Shapes
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Node Editing 
//==============================================================================

function ShapeEd::onAddMeshFromFile( %this, %path ) {
	if ( %path $= "" ) {
		getLoadFilename( "DTS Files|*.dts|COLLADA Files|*.dae|Google Earth Files|*.kmz", %this @ ".onAddMeshFromFile", ShapeEdDetails.lastPath );
		return;
	}

	%path = makeRelativePath( %path, getMainDotCSDir() );
	ShapeEdDetails.lastPath = %path;

	// Determine the detail level to use for the new geometry
	if ( ShapeEdDetails-->addGeomTo.getText() $= "current detail" ) {
		%size = ShapeEditor.shape.getDetailLevelSize( ShapeEdShapeView.currentDL );
	} else {
		// Check if the file has an LODXXX hint at the end of it
		%base = fileBase( %path );
		%pos = strstr( %base, "_LOD" );

		if ( %pos > 0 )
			%size = getSubStr( %base, %pos + 4, strlen( %base ) ) + 0;
		else
			%size = 2;

		// Make sure size is not in use
		while ( ShapeEditor.shape.getDetailLevelIndex( %size ) != -1 )
			%size++;
	}

	ShapeEditor.doAddMeshFromFile( %path, %size );
}

function ShapeEd::onDeleteMesh( %this ) {
	%id = ShapeEd_DetailTree.getSelectedItem();

	if ( ShapeEd_DetailTree.isDetailItem( %id ) ) {
		%detSize = ShapeEd_DetailTree.getItemValue( %id );
		ShapeEditor.doRemoveShapeData( "Detail", %detSize );
	} else {
		%name = ShapeEd_DetailTree.getItemText( %id );
		ShapeEditor.doRemoveShapeData( "Mesh", %name );
	}
}


function ShapeEd::onMeshAdded( %this, %meshName ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	ShapeEdShapeView.updateNodeTransforms();

	// --- COLLISION WINDOW ---
	// Add object to target list if it does not already exist
	if ( !ShapeEditor.isCollisionMesh( %meshName ) ) {
		%objName = stripTrailingNumber( %meshName );
		%id = ShapeEd_CreateColRollout-->colTarget.findText( %objName );

		if ( %id == -1 )
			ShapeEd_CreateColRollout-->colTarget.add( %objName );
	}

	// --- DETAILS TAB ---
	%id = ShapeEd_DetailTree.addMeshEntry( %meshName );
	ShapeEd_DetailTree.clearSelection();
	ShapeEd_DetailTree.selectItem( %id );
}

function ShapeEd::onMeshSizeChanged( %this, %meshName, %oldSize, %newSize ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	// --- DETAILS TAB ---
	// Move the mesh to the new location in the tree
	%selected = ShapeEd_DetailTree.getSelectedItem();
	%id = ShapeEd_DetailTree.findItemByName( %meshName SPC %oldSize );
	ShapeEd_DetailTree.removeMeshEntry( %meshName SPC %oldSize );
	%newId = ShapeEd_DetailTree.addMeshEntry( %meshName SPC %newSize );

	// Re-select the new entry if it was selected
	if ( %selected == %id ) {
		ShapeEd_DetailTree.clearSelection();
		ShapeEd_DetailTree.selectItem( %newId );
	}
}

function ShapeEd::onMeshRemoved( %this, %meshName ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	// --- COLLISION WINDOW ---
	// Remove object from target list if it no longer exists
	%objName = stripTrailingNumber( %meshName );

	if ( ShapeEditor.shape.getObjectIndex( %objName ) == -1 ) {
		%id = ShapeEd_CreateColRollout-->colTarget.findText( %objName );

		if ( %id != -1 )
			ShapeEd_CreateColRollout-->colTarget.clearEntry( %id );
	}

	// --- DETAILS TAB ---
	// Determine which item to select next
	%id = ShapeEd_DetailTree.findItemByName( %meshName );

	if ( %id > 0 ) {
		%nextId = ShapeEd_DetailTree.getPrevSibling( %id );

		if ( %nextId <= 0 ) {
			%nextId = ShapeEd_DetailTree.getNextSibling( %id );

			if ( %nextId <= 0 )
				%nextId = 2;
		}

		// Remove the entry from the tree
		%meshSize = getTrailingNumber( %meshName );
		ShapeEd_DetailTree.removeMeshEntry( %meshName, %meshSize );

		// Change selection if needed
		if ( ShapeEd_DetailTree.getSelectedItem() == -1 )
			ShapeEd_DetailTree.selectItem( %nextId );
	}
}


function ShapeEd::onEditMeshSize( %this ) {
	%newSize = ShapeEdDetails-->meshSize.getText();
	// Check if we are changing the size for a detail or a mesh
	%id = ShapeEd_DetailTree.getSelectedItem();

	if ( ShapeEd_DetailTree.isDetailItem( %id ) ) {
		// Change the size of the selected detail level
		%oldSize = ShapeEd_DetailTree.getItemValue( %id );
		ShapeEditor.doEditDetailSize( %oldSize, %newSize );
	} else {
		// Change the size of the selected mesh
		%meshName = ShapeEd_DetailTree.getItemText( %id );
		ShapeEditor.doEditMeshSize( %meshName, %newSize );
	}
}


function ShapeEd::onEditMeshBBType( %this ) {
	// This command is only valid for meshes (not details)
	%id = ShapeEd_DetailTree.getSelectedItem();

	if ( !ShapeEd_DetailTree.isDetailItem( %id ) ) {
		%meshName = ShapeEd_DetailTree.getItemText( %id );
		%bbType = ShapeEdDetails-->bbType.getText();

		switch$ ( %bbType ) {
		case "None":
			%bbType = "normal";

		case "Billboard":
			%bbType = "billboard";

		case "Z Billboard":
			%bbType = "billboardzaxis";
		}

		ShapeEditor.doEditMeshBillboard( %meshName, %bbType );
	}
}
