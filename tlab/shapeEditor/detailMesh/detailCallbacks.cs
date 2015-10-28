//==============================================================================
// TorqueLab -> ShapeEditor -> Mounted Shapes
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


function ShapeEdShapeView::onDetailChanged( %this ) {
	// Update slider
	if ( mRound( ShapeEdAdvancedWindow-->detailSlider.getValue() ) != %this.currentDL )
		ShapeEdAdvancedWindow-->detailSlider.setValue( %this.currentDL );

	ShapeEdAdvancedWindow-->detailSize.setText( %this.detailSize );
	ShapeEd.onDetailsChanged();
	%id = ShapeEd_DetailTree.getSelectedItem();

	if ( ( %id <= 0 ) || ( %this.currentDL != ShapeEd_DetailTree.getDetailLevelFromItem( %id ) ) ) {
		%id = ShapeEd_DetailTree.findItemByValue( %this.detailSize );

		if ( %id > 0 ) {
			ShapeEd_DetailTree.clearSelection();
			ShapeEd_DetailTree.selectItem( %id );
		}
	}
}

function ShapeEd::onEditDetailSize( %this ) {
	// Change the size of the current detail level
	%oldSize = ShapeEditor.shape.getDetailLevelSize( ShapeEdShapeView.currentDL );
	%detailSize = ShapeEdAdvancedWindow-->detailSize.getText();
	ShapeEditor.doEditDetailSize( %oldSize, %detailSize );
}

function ShapeEd::onEditDetailName( %this ) {
	%newName = %this-->meshName.getText();
	// Check if we are renaming a detail or a mesh
	%id = ShapeEd_DetailTree.getSelectedItem();
	%oldName = ShapeEd_DetailTree.getItemText( %id );

	if ( ShapeEd_DetailTree.isDetailItem( %id ) ) {
		// Rename the selected detail level
		%oldSize = getTrailingNumber( %oldName );
		ShapeEditor.doRenameDetail( %oldName, %newName @ %oldSize );
	} else {
		// Rename the selected mesh
		ShapeEditor.doRenameObject( stripTrailingNumber( %oldName ), %newName );
	}
}

function ShapeEdDetails::onSetObjectNode( %this ) {
	// This command is only valid for meshes (not details)
	%id = ShapeEd_DetailTree.getSelectedItem();

	if ( !ShapeEd_DetailTree.isDetailItem( %id ) ) {
		%meshName = ShapeEd_DetailTree.getItemText( %id );
		%objName = stripTrailingNumber( %meshName );
		%node = %this-->objectNode.getText();

		if ( %node $= "<root>" )
			%node = "";

		ShapeEditor.doSetObjectNode( %objName, %node );
	}
}

//==============================================================================
// ShapeEditor -> Node Editing 
//==============================================================================


function ShapeEd::onDetailRenamed( %this, %oldName, %newName ) {
	// --- DETAILS TAB ---
	// Rename detail entry
	%id = ShapeEd_DetailTree.findItemByName( %oldName );

	if ( %id > 0 ) {
		%size = ShapeEd_DetailTree.getItemValue( %id );
		ShapeEd_DetailTree.editItem( %id, %newName, %size );

		// Sync text if item is selected
		if ( ShapeEd_DetailTree.isItemSelected( %id ) &&
				( ShapeEdDetails-->meshName.getText() !$= %newName ) )
			ShapeEdDetails-->meshName.setText( stripTrailingNumber( %newName ) );
	}
}

function ShapeEd::onDetailSizeChanged( %this, %oldSize, %newSize ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	%dl = ShapeEditor.shape.getDetailLevelIndex( %newSize );

	if ( ShapeEdAdvancedWindow-->detailSize.getText() $= %oldSize ) {
		ShapeEdShapeView.currentDL = %dl;
		ShapeEdAdvancedWindow-->detailSize.setText( %newSize );
		ShapeEdDetails-->meshSize.setText( %newSize );
	}

	// --- DETAILS TAB ---
	// Update detail entry then resort details by size
	%id = ShapeEd_DetailTree.findItemByValue( %oldSize );
	%detName = ShapeEditor.shape.getDetailLevelName( %dl );
	ShapeEd_DetailTree.editItem( %id, %detName, %newSize );

	for ( %sibling = ShapeEd_DetailTree.getPrevSibling( %id );
			( %sibling > 0 ) && ( ShapeEd_DetailTree.getItemValue( %sibling ) < %newSize );
			%sibling = ShapeEd_DetailTree.getPrevSibling( %id ) )
		ShapeEd_DetailTree.moveItemUp( %id );

	for ( %sibling = ShapeEd_DetailTree.getNextSibling( %id );
			( %sibling > 0 ) && ( ShapeEd_DetailTree.getItemValue( %sibling ) > %newSize );
			%sibling = ShapeEd_DetailTree.getNextSibling( %id ) )
		ShapeEd_DetailTree.moveItemDown( %id );

	// Update size values for meshes of this detail
	for ( %child = ShapeEd_DetailTree.getChild( %id );
			%child > 0;
			%child = ShapeEd_DetailTree.getNextSibling( %child ) ) {
		%meshName = stripTrailingNumber( ShapeEd_DetailTree.getItemText( %child ) );
		ShapeEd_DetailTree.editItem( %child, %meshName SPC %newSize, "" );
	}
}

function ShapeEd::onDetailsChanged( %this ) {
	%detailCount = ShapeEditor.shape.getDetailLevelCount();
	ShapeEdAdvancedWindow-->detailSlider.range = "0" SPC ( %detailCount-1 );

	if ( %detailCount >= 2 )
		ShapeEdAdvancedWindow-->detailSlider.ticks = %detailCount - 2;
	else
		ShapeEdAdvancedWindow-->detailSlider.ticks = 0;

	// Initialise imposter settings
	ShapeEdAdvancedWindow-->bbUseImposters.setValue( ShapeEditor.shape.getImposterDetailLevel() != -1 );

	// Update detail parameters
	if ( ShapeEdShapeView.currentDL < %detailCount ) {
		%settings = ShapeEditor.shape.getImposterSettings( ShapeEdShapeView.currentDL );
		%isImposter = getWord( %settings, 0 );
		ShapeEdAdvancedWindow-->imposterInactive.setVisible( !%isImposter );
		ShapeEdAdvancedWindow-->bbEquatorSteps.setText( getField( %settings, 1 ) );
		ShapeEdAdvancedWindow-->bbPolarSteps.setText( getField( %settings, 2 ) );
		ShapeEdAdvancedWindow-->bbDetailLevel.setText( getField( %settings, 3 ) );
		ShapeEdAdvancedWindow-->bbDimension.setText( getField( %settings, 4 ) );
		ShapeEdAdvancedWindow-->bbIncludePoles.setValue( getField( %settings, 5 ) );
		ShapeEdAdvancedWindow-->bbPolarAngle.setText( getField( %settings, 6 ) );
	}
}

