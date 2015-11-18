//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



//==============================================================================
function Lab::setCameraMoveSpeedCtrl(%this, %ctrl) {
	%value = %ctrl.getTypeValue();
	%this.setCameraMoveSpeed(%value);
	//%ctrl.updateFriends();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setCameraMoveSpeed(%this, %speed) {
	// Update Toolbar TextEdit
	EditorGuiToolbarStack-->CameraSpeedEdit.setText(%speed);
	//EWorldEditorCameraSpeed.setText( %value );
	$Camera::movementSpeed = %speed;
	//EditorGuiToolbarStack-->CameraMoveSpeed.setTypeValue(%value);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::toggleToolbarCamSpeedSlider(%this, %sourceObj) {
	%srcPos = %sourceObj.getRealPosition();
	%srcPos.y += %sourceObj.extent.y;
	%range = "1 1000";
	%ticks = getTicksFromRange(%range,"1");
	EOverlay.toggleSlider("2",%srcPos,"range \t "@%range@" \n ticks \t "@%ticks@"\n altCommand \t Lab.setCameraMoveSpeedCtrl($ThisControl);",$Camera::movementSpeed);
}
//------------------------------------------------------------------------------



//==============================================================================
function EWorldEditorToggleCamera::toggleBitmap(%this) {
	%currentImage = %this.bitmap;

	if ( %currentImage $= "tlab/gui/icons/default/toolbar/player" )
		%image = "tlab/gui/icons/default/toolbar/camera";
	else
		%image = "tlab/gui/icons/default/toolbar/player";

	%this.setBitmap( %image );
}
//------------------------------------------------------------------------------
//==============================================================================
function EWorldEditorCameraSpeed::updateMenuBar(%this, %editorBarCtrl) {
	// Update Toolbar TextEdit
	if( %editorBarCtrl.getId() == CameraSpeedDropdownCtrlContainer-->slider.getId() ) {
		%value = %editorBarCtrl.getValue();
		EWorldEditorCameraSpeed.setText( %value );
		$Camera::movementSpeed = %value;
	}

	// Update Toolbar Slider
	if( %editorBarCtrl.getId() == EWorldEditorCameraSpeed.getId() ) {
		%value = %editorBarCtrl.getText();

		if ( %value !$= "" ) {
			if ( %value <= 0 ) {  // camera speed must be >= 0
				%value = 1;
				%editorBarCtrl.setText( %value );
			}

			CameraSpeedDropdownCtrlContainer-->slider.setValue( %value );
			$Camera::movementSpeed = %value;
		}
	}

	// Update Editor
	EditorCameraSpeedOptions.checkRadioItem(0, 6, -1);
}

//------------------------------------------------------------------------------

//==============================================================================
function WorldEditor::dropCameraToSelection(%this) {
	if(%this.getSelectionSize() == 0)
		return;

	%pos = %this.getSelectionCentroid();
	%cam = LocalClientConnection.camera.getTransform();
	// set the pnt
	%cam = setWord(%cam, 0, getWord(%pos, 0));
	%cam = setWord(%cam, 1, getWord(%pos, 1));
	%cam = setWord(%cam, 2, getWord(%pos, 2));
	LocalClientConnection.camera.setTransform(%cam);
}
//------------------------------------------------------------------------------