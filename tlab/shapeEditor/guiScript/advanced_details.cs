//==============================================================================
// TorqueLab -> ShapeEditor -> Shape Selection
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Details functions
//==============================================================================
function ShapeEdDetails::onToggleDetails( %this, %useDetails ) {
	ShapeEdAdv_Details-->detailSlider.setActive(%useDetails);
	ShapeEdAdv_Details-->levelsInactive.visible = %useDetails;
}

//==============================================================================
// Imposter functions
//==============================================================================

//==============================================================================
// Toggle details imposters
function ShapeEdDetails::onToggleImposter( %this, %useImposter ) {
	%hasImposterDetail = ( ShapeEditor.shape.getImposterDetailLevel() != -1 );
	ShapeEdAdv_Details-->imposterActive.visible = %useImposter;

	if ( %useImposter == %hasImposterDetail )
		return;

	if ( %useImposter ) {
		// Determine an unused detail size
		for ( %detailSize = 0; %detailSize < 50; %detailSize++ ) {
			if ( ShapeEditor.shape.getDetailLevelIndex( %detailSize ) == -1 )
				break;
		}

		// Set some initial values for the imposter
		%bbEquatorSteps = 6;
		%bbPolarSteps = 0;
		%bbDetailLevel = 0;
		%bbDimension = 128;
		%bbIncludePoles = 0;
		%bbPolarAngle = 0;
		// Add a new imposter detail level to the shape
		ShapeEditor.doEditImposter( -1, %detailSize, %bbEquatorSteps, %bbPolarSteps,
											 %bbDetailLevel, %bbDimension, %bbIncludePoles, %bbPolarAngle );
	} else {
		// Remove the imposter detail level
		ShapeEditor.doRemoveImposter();
	}
}
//==============================================================================
//------------------------------------------------------------------------------
function ShapeEdDetails::onEditImposter( %this ) {
	// Modify the parameters of the current imposter detail level
	%detailSize = ShapeEditor.shape.getDetailLevelSize( ShapeEdShapeView.currentDL );
	%bbDimension = ShapeEdAdv_Details-->bbDimension.getText();
	%bbDetailLevel = ShapeEdAdv_Details-->bbDetailLevel.getText();
	%bbEquatorSteps = ShapeEdAdv_Details-->bbEquatorSteps.getText();
	%bbIncludePoles = ShapeEdAdv_Details-->bbIncludePoles.getValue();
	%bbPolarSteps = ShapeEdAdv_Details-->bbPolarSteps.getText();
	%bbPolarAngle = ShapeEdAdv_Details-->bbPolarAngle.getText();
	ShapeEditor.doEditImposter( ShapeEdShapeView.currentDL, %detailSize,
										 %bbEquatorSteps, %bbPolarSteps, %bbDetailLevel, %bbDimension,
										 %bbIncludePoles, %bbPolarAngle );
}
//------------------------------------------------------------------------------
