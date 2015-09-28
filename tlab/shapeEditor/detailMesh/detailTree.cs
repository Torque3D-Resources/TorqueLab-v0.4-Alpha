//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Scene Editor Params - Used set default settings and build plugins options GUI
//==============================================================================


//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================

function ShapeEd_DetailTree::onDefineIcons(%this) {
	// Set the tree view icon indices and texture paths
	%this._imageNone = 0;
	%this._imageHidden = 1;
	%icons = ":" @                                        // no icon
				"tlab/gui/icons/default/visible_i:";               // hidden
	%this.buildIconTable( %icons );
}

// Return true if the item in the details tree view is a detail level (false if
// a mesh)
function ShapeEd_DetailTree::isDetailItem( %this, %id ) {
	return ( %this.getParent( %id ) == 1 );
}

// Get the detail level index from the ID of an item in the details tree view
function ShapeEd_DetailTree::getDetailLevelFromItem( %this, %id ) {
	if ( %this.isDetailItem( %id ) )
		%detSize = %this.getItemValue( %id );
	else
		%detSize = %this.getItemValue( %this.getParent( %id ) );

	return ShapeEditor.shape.getDetailLevelIndex( %detSize );
}

function ShapeEd_DetailTree::addMeshEntry( %this, %name, %noSync ) {
	// Add new detail level if required
	%size = getTrailingNumber( %name );
	%detailID = %this.findItemByValue( %size );

	if ( %detailID <= 0 ) {
		%dl = ShapeEditor.shape.getDetailLevelIndex( %size );
		%detName = ShapeEditor.shape.getDetailLevelName( %dl );
		%detailID = ShapeEd_DetailTree.insertItem( 1, %detName, %size, "" );

		// Sort details by decreasing size
		for ( %sibling = ShapeEd_DetailTree.getPrevSibling( %detailID );
				( %sibling > 0 ) && ( ShapeEd_DetailTree.getItemValue( %sibling ) < %size );
				%sibling = ShapeEd_DetailTree.getPrevSibling( %detailID ) )
			ShapeEd_DetailTree.moveItemUp( %detailID );

		if ( !%noSync )
			ShapeEd.onDetailsChanged();
	}

	return %this.insertItem( %detailID, %name, "", "" );
}

function ShapeEd_DetailTree::removeMeshEntry( %this, %name, %size ) {
	%size = getTrailingNumber( %name );
	%id = ShapeEd_DetailTree.findItemByName( %name );

	if ( ShapeEditor.shape.getDetailLevelIndex( %size ) < 0 ) {
		// Last mesh of a detail level has been removed => remove the detail level
		%this.removeItem( %this.getParent( %id ) );
		ShapeEd.onDetailsChanged();
	} else
		%this.removeItem( %id );
}





function ShapeEd_DetailTree::onSelect( %this, %id ) {
	%name = %this.getItemText( %id );
	%baseName = stripTrailingNumber( %name );
	%size = getTrailingNumber( %name );
	ShapeEdDetails-->meshName.setText( %baseName );
	ShapeEdDetails-->meshSize.setText( %size );
	// Select the appropriate detail level
	%dl = %this.getDetailLevelFromItem( %id );
	ShapeEdShapeView.currentDL = %dl;

	if ( %this.isDetailItem( %id ) ) {
		// Selected a detail => disable mesh controls
		ShapeEdDetails-->editMeshActive.setVisible( false );
		ShapeEdShapeView.selectedObject = -1;
		ShapeEdShapeView.selectedObjDetail = 0;
	} else {
		// Selected a mesh => sync mesh controls
		ShapeEdDetails-->editMeshActive.setVisible( true );

		switch$ ( ShapeEditor.shape.getMeshType( %name ) ) {
		case "normal":
			ShapeEdDetails-->bbType.setSelected( 0, false );

		case "billboard":
			ShapeEdDetails-->bbType.setSelected( 1, false );

		case "billboardzaxis":
			ShapeEdDetails-->bbType.setSelected( 2, false );
		}

		%node = ShapeEditor.shape.getObjectNode( %baseName );

		if ( %node $= "" )
			%node = "<root>";

		ShapeEdDetails-->objectNode.setSelected( ShapeEdDetails-->objectNode.findText( %node ), false );
		ShapeEdShapeView.selectedObject = ShapeEditor.shape.getObjectIndex( %baseName );
		ShapeEdShapeView.selectedObjDetail = %dl;
	}
}

function ShapeEd_DetailTree::onRightMouseUp( %this, %itemId, %mouse ) {
	// Open context menu if this is a Mesh item
	if ( !%this.isDetailItem( %itemId ) ) {
		if( !isObject( "ShapeEdMeshPopup" ) ) {
			new PopupMenu( ShapeEdMeshPopup ) {
				superClass = "MenuBuilder";
				isPopup = "1";
				item[ 0 ] = "Hidden" TAB "" TAB "ShapeEd_DetailTree.onHideMeshItem( %this._objName, !%this._itemHidden );";
				item[ 1 ] = "-";
				item[ 2 ] = "Hide all" TAB "" TAB "ShapeEd_DetailTree.onHideMeshItem( \"\", true );";
				item[ 3 ] = "Show all" TAB "" TAB "ShapeEd_DetailTree.onHideMeshItem( \"\", false );";
			};
		}

		ShapeEdMeshPopup._objName = stripTrailingNumber( %this.getItemText( %itemId ) );
		ShapeEdMeshPopup._itemHidden = ShapeEdShapeView.getMeshHidden( ShapeEdMeshPopup._objName );
		ShapeEdMeshPopup.checkItem( 0, ShapeEdMeshPopup._itemHidden );
		ShapeEdMeshPopup.showPopup( Canvas );
	}
}

function ShapeEd_DetailTree::onHideMeshItem( %this, %objName, %hide ) {
	if ( %hide )
		%imageId = %this._imageHidden;
	else
		%imageId = %this._imageNone;

	if ( %objName $= "" ) {
		// Show/hide all
		ShapeEdShapeView.setAllMeshesHidden( %hide );

		for ( %parent = %this.getChild(%this.getFirstRootItem()); %parent > 0; %parent = %this.getNextSibling(%parent) )
			for ( %child = %this.getChild(%parent); %child > 0; %child = %this.getNextSibling(%child) )
				%this.setItemImages( %child, %imageId, %imageId );
	} else {
		// Show/hide all meshes for this object
		ShapeEdShapeView.setMeshHidden( %objName, %hide );
		%count = ShapeEditor.shape.getMeshCount( %objName );

		for ( %i = 0; %i < %count; %i++ ) {
			%meshName = ShapeEditor.shape.getMeshName( %objName, %i );
			%id = ShapeEd_DetailTree.findItemByName( %meshName );

			if ( %id > 0 )
				%this.setItemImages( %id, %imageId, %imageId );
		}
	}
}