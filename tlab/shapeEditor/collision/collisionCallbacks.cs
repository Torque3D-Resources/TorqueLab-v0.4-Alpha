//==============================================================================
// TorqueLab -> ShapeEditor -> Shape Selection
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ShapeEditor -> Collision editing
//==============================================================================

function ShapeEdCollisions::update_onShapeSelectionChanged( %this ) {
	%this.lastColSettings = "" TAB "Bounds";
	// Initialise collision mesh target list
	ShapeEd_CreateColRollout-->colTarget.clear();
	ShapeEd_CreateColRollout-->colTarget.add( "Bounds" );
	%objCount = ShapeEditor.shape.getObjectCount();

	for ( %i = 0; %i < %objCount; %i++ ) {
		ShapeEd_CreateColRollout-->colTarget.add( ShapeEditor.shape.getObjectName( %i ) );
	}

	ShapeEd_CreateColRollout-->colTarget.setSelected( %this-->colTarget.findText( "Bounds" ), false );
}

function ShapeEdCollisions::update_onCollisionChanged( %this ) {
	// Sync collision settings
	%colData = %this.lastColSettings;
	%typeId = %this-->colType.findText( getField( %colData, 0 ) );
	%this-->colType.setSelected( %typeId, false );
	%targetId = %this-->colTarget.findText( getField( %colData, 1 ) );
	%this-->colTarget.setSelected( %targetId, false );

	if ( %this-->colType.getText() $= "Convex Hulls" ) {
		show(ShapeEdColCreate_Hull);
		hide(ShapeEdColCreate_NoHull);
		%this-->hullDepth.setValue( getField( %colData, 2 ) );
		%this-->hullDepthText.setText( mFloor( %this-->hullDepth.getValue() ) );
		%this-->hullMergeThreshold.setValue( getField( %colData, 3 ) );
		%this-->hullMergeText.setText( mFloor( %this-->hullMergeThreshold.getValue() ) );
		%this-->hullConcaveThreshold.setValue( getField( %colData, 4 ) );
		%this-->hullConcaveText.setText( mFloor( %this-->hullConcaveThreshold.getValue() ) );
		%this-->hullMaxVerts.setValue( getField( %colData, 5 ) );
		%this-->hullMaxVertsText.setText( mFloor( %this-->hullMaxVerts.getValue() ) );
		%this-->hullMaxBoxError.setValue( getField( %colData, 6 ) );
		%this-->hullMaxBoxErrorText.setText( mFloor( %this-->hullMaxBoxError.getValue() ) );
		%this-->hullMaxSphereError.setValue( getField( %colData, 7 ) );
		%this-->hullMaxSphereErrorText.setText( mFloor( %this-->hullMaxSphereError.getValue() ) );
		%this-->hullMaxCapsuleError.setValue( getField( %colData, 8 ) );
		%this-->hullMaxCapsuleErrorText.setText( mFloor( %this-->hullMaxCapsuleError.getValue() ) );
	} else {
		hide(ShapeEdColCreate_Hull);
		show(ShapeEdColCreate_NoHull);
	}
}



//==============================================================================
// TorqueLab Collision Functions
//==============================================================================