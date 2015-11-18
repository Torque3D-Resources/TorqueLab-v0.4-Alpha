//==============================================================================
// TorqueLab -> ShapeEditor -> Node Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


function ShapeEd::addNewNode( %this, %name ) {
	if (%name $= "")
		%name = ShapeEd_AddNodeName.getText();

	if (%name $= "")
		%name = "NewNode";

	ShapeEdNodes.onAddNode(%name);
}




//ShapeEdNodes.onAddNode("Base");
function ShapeEdNodes::onAddNode( %this, %name ) {
	// Add a new node, using the currently selected node as the initial parent
	if ( %name $= "" )
		%name = ShapeEditor.getUniqueName( "node", "myNode" );

	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( %id <= 0 )
		%parent = "";
	else
		%parent = ShapeEd_NodeTree.getItemText( %id );

	ShapeEditor.doAddNode( %name, %parent, "0 0 0 0 0 1 0" );
}

function ShapeEdNodes::onDeleteNode( %this ) {
	// Remove the node and all its children from the shape
	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( %id > 0 ) {
		%name = ShapeEd_NodeTree.getItemText( %id );
		ShapeEditor.doRemoveShapeData( "Node", %name );
	}
}

function ShapeEdNodes::onEditName( %this ) {
	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( %id > 0 ) {
		%oldName = ShapeEd_NodeTree.getItemText( %id );
		%newName = %this-->nodeName.getText();

		if ( %newName !$= "" )
			ShapeEditor.doRenameNode( %oldName, %newName );
	}
}

function ShapeEdNodeParentMenu::onSelect( %this, %id, %text ) {
	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( %id > 0 ) {
		%name = ShapeEd_NodeTree.getItemText( %id );
		ShapeEditor.doSetNodeParent( %name, %text );
	}
}

function ShapeEdNodes::onEditTransform( %this ) {
	%id = ShapeEd_NodeTree.getSelectedItem();

	if ( %id > 0 ) {
		%name = ShapeEd_NodeTree.getItemText( %id );
		// Get the node transform from the gui
		%pos = %this-->nodePosition.getText();
		%rot = %this-->nodeRotation.getText();
		%txfm = %pos SPC %rot;
		%isWorld = ShapeEdNodes-->worldTransform.getValue();

		// Do a quick sanity check to avoid setting wildly invalid transforms
		for ( %i = 0; %i < 7; %i++ ) {  // "x y z aa.x aa.y aa.z aa.angle"
			if ( getWord( %txfm, %i ) $= "" )
				return;
		}

		ShapeEditor.doEditNodeTransform( %name, %txfm, %isWorld, -1 );
	}
}

function ShapeEdShapeView::onEditNodeTransform( %this, %node, %txfm, %gizmoID ) {
	devLog("ShapeEdShapeView::onEditNodeTransform");
	ShapeEditor.doEditNodeTransform( %node, %txfm, 1, %gizmoID );
}
