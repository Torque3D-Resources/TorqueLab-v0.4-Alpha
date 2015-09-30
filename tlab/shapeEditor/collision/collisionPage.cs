//==============================================================================
// TorqueLab -> ShapeEditor -> Shape Selection
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Collision editing
//==============================================================================
//==============================================================================
function ShapeEd::initCollisionPage( %this ) {
	ShapeEdColCreate_TypeMenu.clear();
	ShapeEdColCreate_TypeMenu.add( "Box" );
	ShapeEdColCreate_TypeMenu.add( "Sphere" );
	ShapeEdColCreate_TypeMenu.add( "Capsule" );
	ShapeEdColCreate_TypeMenu.add( "10-DOP X" );
	ShapeEdColCreate_TypeMenu.add( "10-DOP Y" );
	ShapeEdColCreate_TypeMenu.add( "10-DOP Z" );
	ShapeEdColCreate_TypeMenu.add( "18-DOP" );
	ShapeEdColCreate_TypeMenu.add( "26-DOP" );
	ShapeEdColCreate_TypeMenu.add( "Convex Hulls" );
	
	ShapeEdColRollout-->colType.clear();
	ShapeEdColRollout-->colType.add( "Box" );
	ShapeEdColRollout-->colType.add( "Sphere" );
	ShapeEdColRollout-->colType.add( "Capsule" );
	ShapeEdColRollout-->colType.add( "10-DOP X" );
	ShapeEdColRollout-->colType.add( "10-DOP Y" );
	ShapeEdColRollout-->colType.add( "10-DOP Z" );
	ShapeEdColRollout-->colType.add( "18-DOP" );
	ShapeEdColRollout-->colType.add( "26-DOP" );
	ShapeEdColRollout-->colType.add( "Convex Hulls" );
}
//------------------------------------------------------------------------------
