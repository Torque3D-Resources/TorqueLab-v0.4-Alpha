//==============================================================================
// TorqueLab -> ShapeEditor -> Detail/Mesh Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// ShapeEditor -> Detail/Mesh Editing
//==============================================================================

function ShapeEdAdvancedWindow::update_onShapeSelectionChanged( %this ) {
	ShapeEdShapeView.currentDL = 0;
	ShapeEdShapeView.onDetailChanged();
}