//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Scene Editor Params - Used set default settings and build plugins options GUI
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function ShapeEd::clearNodeTree( %this ) {
	ShapeEd_NodeTree.removeItem( 0 );
}
//------------------------------------------------------------------------------

//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================
function ShapeEd_NodeTree::onClearSelection( %this ) {
	ShapeEd.onNodeSelectionChanged( -1 );
}

function ShapeEd_NodeTree::onSelect( %this, %id ) {
	// Update the node name and transform controls
	ShapeEd.onNodeSelectionChanged( %id );

	// Update orbit position if orbiting the selected node
	if ( ShapeEdShapeView.orbitNode ) {
		%name = %this.getItemText( %id );
		%transform = ShapeEditor.shape.getNodeTransform( %name, 1 );
		ShapeEdShapeView.setOrbitPos( getWords( %transform, 0, 2 ) );
	}
}

// Determine the index of a node in the tree relative to its parent
function ShapeEd_NodeTree::getChildIndexByName( %this, %name ) {
	%id = %this.findItemByName( %name );
	%parentId = %this.getParent( %id );
	%childId = %this.getChild( %parentId );

	if ( %childId <= 0 )
		return 0;   // bad!

	%index = 0;

	while ( %childId != %id ) {
		%childId = %this.getNextSibling( %childId );
		%index++;
	}

	return %index;
}

// Add a node and its children to the node tree view
function ShapeEd_NodeTree::addNodeTree( %this, %nodeName ) {
	// Abort if already added => something dodgy has happened and we'd end up
	// recursing indefinitely
	if ( %this.findItemByName( %nodeName ) ) {
		error( "Recursion error in ShapeEd_NodeTree::addNodeTree" );
		return 0;
	}

	// Find parent and add me to it
	%parentName = ShapeEditor.shape.getNodeParentName( %nodeName );

	if ( %parentName $= "" )
		%parentName = "<root>";

	%parentId = %this.findItemByName( %parentName );
	%id = %this.insertItem( %parentId, %nodeName, 0, "" );
	// Add children
	%count = ShapeEditor.shape.getNodeChildCount( %nodeName );

	for ( %i = 0; %i < %count; %i++ )
		%this.addNodeTree( ShapeEditor.shape.getNodeChildName( %nodeName, %i ) );

	return %id;
}
