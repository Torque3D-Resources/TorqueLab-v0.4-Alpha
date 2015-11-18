//==============================================================================
// TorqueLab -> ShapeEditor -> Mounted Shapes
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Node Editing
//==============================================================================

// Update the GUI in response to the node selection changing
function ShapeEd::onNodeSelectionChanged( %this, %id ) {
	if ( %id > 0 ) {
		// Enable delete button and edit boxes
		if ( ShapeEdSeqNodeTabBook.activePage $= "Node" )
			ShapeEdPropWindow-->deleteBtn.setActive( true );

		ShapeEdNodes-->nodeName.setActive( true );
		ShapeEdNodes-->nodePosition.setActive( true );
		ShapeEdNodes-->nodeRotation.setActive( true );
		// Update the node inspection data
		%name = ShapeEd_NodeTree.getItemText( %id );
		ShapeEdNodes-->nodeName.setText( %name );
		// Node parent list => ancestor and sibling nodes only (can't re-parent to a descendent)
		ShapeEdNodeParentMenu.clear();
		%parentNames = ShapeEditor.getNodeNames( "", "<root>", %name );
		%count = getWordCount( %parentNames );

		for ( %i = 0; %i < %count; %i++ )
			ShapeEdNodeParentMenu.add( getWord(%parentNames, %i), %i );

		%pName = ShapeEditor.shape.getNodeParentName( %name );

		if ( %pName $= "" )
			%pName = "<root>";

		ShapeEdNodeParentMenu.setText( %pName );

		if ( ShapeEdNodes-->worldTransform.getValue() ) {
			// Global transform
			%txfm = ShapeEditor.shape.getNodeTransform( %name, 1 );
			ShapeEdNodes-->nodePosition.setText( getWords( %txfm, 0, 2 ) );
			ShapeEdNodes-->nodeRotation.setText( getWords( %txfm, 3, 6 ) );
		} else {
			// Local transform (relative to parent)
			%txfm = ShapeEditor.shape.getNodeTransform( %name, 0 );
			ShapeEdNodes-->nodePosition.setText( getWords( %txfm, 0, 2 ) );
			ShapeEdNodes-->nodeRotation.setText( getWords( %txfm, 3, 6 ) );
		}

		ShapeEdShapeView.selectedNode = ShapeEditor.shape.getNodeIndex( %name );
	} else {
		// Disable delete button and edit boxes
		if ( ShapeEdSeqNodeTabBook.activePage $= "Node" )
			ShapeEdPropWindow-->deleteBtn.setActive( false );

		ShapeEdNodes-->nodeName.setActive( false );
		ShapeEdNodes-->nodePosition.setActive( false );
		ShapeEdNodes-->nodeRotation.setActive( false );
		ShapeEdNodes-->nodeName.setText( "" );
		ShapeEdNodes-->nodePosition.setText( "" );
		ShapeEdNodes-->nodeRotation.setText( "" );
		ShapeEdShapeView.selectedNode = -1;
	}
}

// Update the GUI in response to a node being added
function ShapeEd::onNodeAdded( %this, %nodeName, %oldTreeIndex ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	ShapeEdShapeView.updateNodeTransforms();
	ShapeEdSelectWindow.updateHints();

	// --- MOUNT WINDOW ---
	if ( ShapeEdMountWindow.isMountableNode( %nodeName ) ) {
		ShapeEdMountWindow-->mountNode.add( %nodeName );
		ShapeEdMountWindow-->mountNode.sort();
	}

	// --- NODES TAB ---
	%id = ShapeEd_NodeTree.addNodeTree( %nodeName );

	if ( %oldTreeIndex <= 0 ) {
		// This is a new node => make it the current selection
		if ( %id > 0 ) {
			ShapeEd_NodeTree.clearSelection();
			ShapeEd_NodeTree.selectItem( %id );
		}
	} else {
		// This node has been un-deleted. Inserting a new item puts it at the
		// end of the siblings, but we want to restore the original order as
		// if the item was never deleted, so move it up as required.
		%childIndex = ShapeEd_NodeTree.getChildIndexByName( %nodeName );

		while ( %childIndex > %oldTreeIndex ) {
			ShapeEd_NodeTree.moveItemUp( %id );
			%childIndex--;
		}
	}

	// --- DETAILS TAB ---
	ShapeEdDetails-->objectNode.add( %nodeName );
}

// Update the GUI in response to a node(s) being removed
function ShapeEd::onNodeRemoved( %this, %nameList, %nameCount ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();
	ShapeEdShapeView.updateNodeTransforms();
	ShapeEdSelectWindow.updateHints();

	// Remove nodes from the mountable list, and any shapes mounted to the node
	for ( %i = 0; %i < %nameCount; %i++ ) {
		%nodeName = getField( %nameList, %i );
		ShapeEdMountWindow-->mountNode.clearEntry( ShapeEdMountWindow-->mountNode.findText( %nodeName ) );

		for ( %j = ShapeEdMountWindow-->mountList.rowCount()-1; %j >= 1; %j-- ) {
			%text = ShapeEdMountWindow-->mountList.getRowText( %j );

			if ( getField( %text, 1 ) $= %nodeName ) {
				ShapeEdShapeView.unmountShape( %j-1 );
				ShapeEdMountWindow-->mountList.removeRow( %j );
			}
		}
	}

	// --- NODES TAB ---
	%lastName = getField( %nameList, %nameCount-1 );
	%id = ShapeEd_NodeTree.findItemByName( %lastName );   // only need to remove the parent item

	if ( %id > 0 ) {
		ShapeEd_NodeTree.removeItem( %id );

		if ( ShapeEd_NodeTree.getSelectedItem() <= 0 )
			ShapeEd.onNodeSelectionChanged( -1 );
	}

	// --- DETAILS TAB ---
	for ( %i = 0; %i < %nameCount; %i++ ) {
		%nodeName = getField( %nameList, %i );
		ShapeEdDetails-->objectNode.clearEntry( ShapeEdDetails-->objectNode.findText( %nodeName ) );
	}
}

// Update the GUI in response to a node being renamed
function ShapeEd::onNodeRenamed( %this, %oldName, %newName ) {
	// --- MISC ---
	ShapeEdSelectWindow.updateHints();
	// --- MOUNT WINDOW ---
	// Update entries for any shapes mounted to this node
	%rowCount = ShapeEdMountWindow-->mountList.rowCount();

	for ( %i = 1; %i < %rowCount; %i++ ) {
		%text = ShapeEdMountWindow-->mountList.getRowText( %i );

		if ( getField( %text, 1 ) $= %oldName ) {
			%text = setField( %text, 1, %newName );
			ShapeEdMountWindow-->mountList.setRowById( ShapeEdMountWindow-->mountList.getRowId( %i ), %text );
		}
	}

	// Update list of mountable nodes
	ShapeEdMountWindow-->mountNode.clearEntry( ShapeEdMountWindow-->mountNode.findText( %oldName ) );

	if ( ShapeEdMountWindow.isMountableNode( %newName ) ) {
		ShapeEdMountWindow-->mountNode.add( %newName );
		ShapeEdMountWindow-->mountNode.sort();
	}

	// --- NODES TAB ---
	%id = ShapeEd_NodeTree.findItemByName( %oldName );
	ShapeEd_NodeTree.editItem( %id, %newName, 0 );

	if ( ShapeEd_NodeTree.getSelectedItem() == %id )
		ShapeEdNodes-->nodeName.setText( %newName );

	// --- DETAILS TAB ---
	%id = ShapeEdDetails-->objectNode.findText( %oldName );

	if ( %id != -1 ) {
		ShapeEdDetails-->objectNode.clearEntry( %id );
		ShapeEdDetails-->objectNode.add( %newName, %id );
		ShapeEdDetails-->objectNode.sortID();

		if ( ShapeEdDetails-->objectNode.getText() $= %oldName )
			ShapeEdDetails-->objectNode.setText( %newName );
	}
}

// Update the GUI in response to a node's parent being changed

// Update the GUI in response to a node's parent being changed
function ShapeEd::onNodeParentChanged( %this, %nodeName ) {
	// --- MISC ---
	ShapeEdShapeView.updateNodeTransforms();
	// --- NODES TAB ---
	%isSelected = 0;
	%id = ShapeEd_NodeTree.findItemByName( %nodeName );

	if ( %id > 0 ) {
		%isSelected = ( ShapeEd_NodeTree.getSelectedItem() == %id );
		ShapeEd_NodeTree.removeItem( %id );
	}

	ShapeEd_NodeTree.addNodeTree( %nodeName );

	if ( %isSelected )
		ShapeEd_NodeTree.selectItem( ShapeEd_NodeTree.findItemByName( %nodeName ) );
}
function ShapeEd::onNodeTransformChanged( %this, %nodeName ) {
	// Default to the selected node if none is specified
	if ( %nodeName $= "" ) {
		%id = ShapeEd_NodeTree.getSelectedItem();

		if ( %id > 0 )
			%nodeName = ShapeEd_NodeTree.getItemText( %id );
		else
			return;
	}

	// --- MISC ---
	ShapeEdShapeView.updateNodeTransforms();

	if ( ShapeEdNodes-->objectTransform.getValue() )
		GlobalGizmoProfile.setFieldValue(alignment, Object);
	else
		GlobalGizmoProfile.setFieldValue(alignment, World);

	// --- NODES TAB ---
	// Update the node transform fields if necessary
	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( ( %id > 0 ) && ( ShapeEd_NodeTree.getItemText( %id ) $= %nodeName ) ) {
		%isWorld = ShapeEdNodes-->worldTransform.getValue();
		%transform = ShapeEditor.shape.getNodeTransform( %nodeName, %isWorld );
		ShapeEdNodes-->nodePosition.setText( getWords( %transform, 0, 2 ) );
		ShapeEdNodes-->nodeRotation.setText( getWords( %transform, 3, 6 ) );
	}
}

function ShapeEd::onObjectNodeChanged( %this, %objName ) {
	// --- MISC ---
	ShapeEdShapeView.refreshShape();

	// --- DETAILS TAB ---
	// Update the node popup menu if this object is selected
	if ( ShapeEdDetails-->meshName.getText() $= %objName ) {
		%nodeName = ShapeEditor.shape.getObjectNode( %objName );

		if ( %nodeName $= "" )
			%nodeName = "<root>";

		%id = ShapeEdDetails-->objectNode.findText( %nodeName );
		ShapeEdDetails-->objectNode.setSelected( %id, false );
	}
}
