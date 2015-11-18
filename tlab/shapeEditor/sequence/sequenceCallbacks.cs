//==============================================================================
// TorqueLab -> ShapeEditor -> Mounted Shapes
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Node Editing
//==============================================================================

// Update the GUI in response to the node selection changing
function ShapeEd::onSequenceObjectInvalid( %this, %id ) {
	ShapeEd_SeqPillStack.clear();
}
