//==============================================================================
// TorqueLab -> ShapeEditor -> Shape Selection
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Collision editing
//==============================================================================

//==============================================================================
function ShapeEdColCreate_TypeMenu::onSelect( %this,%id,%text ) {
	info("Create collision type menu select:",%text,"ID:",%id);
}
//------------------------------------------------------------------------------
//==============================================================================
function ShapeEdColCreate_TargetMenu::onSelect( %this,%id,%text ) {
	info("Create collision target menu select:",%text,"ID:",%id);
}
//------------------------------------------------------------------------------

//==============================================================================
function ShapeEdCollisions::generateMesh( %this ) {
	%colType = 	ShapeEdColCreate_TypeMenu.getText();
	%colTarget = 	ShapeEdColCreate_TargetMenu.getText();
	%this.editCollision();
	
}
//------------------------------------------------------------------------------
