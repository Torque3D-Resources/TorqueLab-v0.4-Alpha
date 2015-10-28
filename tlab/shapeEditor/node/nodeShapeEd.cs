//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// SceneEditorTools Frame Set Scripts
//==============================================================================
function ShapeEdShapeView::onNodeSelected( %this, %index ) {
	ShapeEd_NodeTree.clearSelection();

	if ( %index > 0 ) {
		%name = ShapeEditor.shape.getNodeName( %index );
		%id = ShapeEd_NodeTree.findItemByName( %name );

		if ( %id > 0 )
			ShapeEd_NodeTree.selectItem( %id );
	}
}

/*
onEditNodeTransform
onNodeSelected
onEditNodeTransform




























*/