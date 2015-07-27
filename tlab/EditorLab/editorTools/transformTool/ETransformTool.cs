//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//EWorldEditor.transformSelection(doPosition, point, doRelativePos, doRotate,rotation, doRelativeRot, doRotLocal, scaleType, scale, isRelative,  isLocal );

$ETransformToolContainers = "ETransformTool SceneEditorTransformTools";
$ETransformToolEditFields = "PosX PosY PosZ TransX TransY TransZ RotX RotY RotZ RotH RotP RotB ScaleX ScaleY ScaleZ";
$ETransformTool::Active::Position = false;
$ETransformTool::Active::Rotation = false;
$ETransformTool::Active::Scale = false;

$ETransformTool::Relative::Position = true;
$ETransformTool::Relative::Rotation = true;
$ETransformTool::Relative::Scale = true;

$ETransformTool::LocalCenter::Rotation = true;
$ETransformTool::LocalCenter::Scale = true;

$ETransformTool::Proportional::Scale = false;
//==============================================================================
//ETransformTool.resetAll Field TextEditValue Changed
function ETransformTool::resetAll( %this,%ctrl ) {
	foreach$(%field in $ETransformToolEditFields){
		%ctrl = %this.findObjectByInternalName(%field,true);	
		%ctrl.setText("0");
		%this.updateTextEditField(%ctrl);
	}
}
//------------------------------------------------------------------------------

//==============================================================================
//ETransformTool Field TextEditValue Changed
function ETransformTool::editCommand( %this,%ctrl ) {
	devLog("ETransformTool::editCommand",%ctrl);	
	%this.updateTextEditField(%ctrl);
}
//------------------------------------------------------------------------------
//==============================================================================
//ETransformTool Field TextEditValue Changed
function ETransformTool::updateTextEditField( %this,%ctrl ) {
	devLog("ETransformTool::updateTextEditField",%ctrl);
	%value = %ctrl.getText();
	%field = %ctrl.internalName;

	if (!strIsNumeric(%value)) {
		warnLog("Invalid value submitted for:",%field, "Value resetted to 0");
		%value = "0";
	}

	foreach$(%container in $ETransformToolContainers) {
		%textEdit = %container.findObjectByInternalName(%field,true);

		if (!isObject(%textEdit))
			continue;

		%textEdit.setValue(%value);
	}	
}
//------------------------------------------------------------------------------

//==============================================================================
//ETransform tool text edit onValidate
function ETransformEdit::onValidate( %this ) {
	devLog("ECloneEdit::onValidate");
	ETransformTool.updateTextEditField(%this);
	
}
//------------------------------------------------------------------------------

//==============================================================================
//ETransform tool text edit onValidate
function ETransformCheck::onClick( %this ) {
	devLog("ETransformCheck::onClick");
	%value = %this.isStateOn();
	%field = %this.internalName;
	devLog(%field," checkbox cliked and set to:",%value);

	foreach$(%container in $ETransformToolContainers) {
		%check = %container.findObjectByInternalName(%field,true);
		%check.setStateOn(%value);
	}
}
//------------------------------------------------------------------------------
function ETransformTool::setFieldValueContainers( %this,%field,%value ) {
	foreach$(%container in $ETransformToolContainers) {
		%textEdit = %container.findObjectByInternalName(%field,true);

		if (!isObject(%textEdit))
			continue;

		%textEdit.setValue(%value);
	}
}

//==============================================================================
function ETransformTool::getSelectionObj( %this ) {
	%obj = EWorldEditor.getSelectedObject( 0 );
	if( !isObject(%obj)) {
		warnLog("No selected object found");
		return "";
	}
	return %obj;
}
//------------------------------------------------------------------------------


/*
function ETransformSelection::onWake( %this ) {
	// Make everything relative
	%this-->PosRelative.setStateOn( true );
	%this-->RotRelative.setStateOn( true );
	%this-->ScaleRelative.setStateOn( true );
	%this-->SizeRelative.setStateOn( false );

	%this-->GetPosButton.setActive( false );
	%this-->GetRotButton.setActive( false );
	%this-->GetScaleButton.setActive( false );
	%this-->GetSizeButton.setActive( false );

	// Size is always local
	%this-->SizeLocal.setStateOn( true );
	%this-->SizeLocal.setActive( false );

	%this-->ScaleTabBook.selectPage( 0 );   // Scale page

	%this-->ApplyButton.setActive( false );

	EWorldEditor.ETransformSelectionDisplayed = false;
}

function ETransformSelection::onVisible( %this, %state ) {
	// If we are made visible, sync to the world editor
	// selection.

	if( %state )
		%this.onSelectionChanged();
}

function ETransformSelection::hideDialog( %this ) {
	%this.setVisible(false);
	EWorldEditor.ETransformSelectionDisplayed = false;
}

function ETransformSelection::ToggleVisibility( %this ) {
	if ( %this.visible  ) {
		%this.setVisible(false);
		EWorldEditor.ETransformSelectionDisplayed = false;
	} else {
		%this.setVisible(true);
		%this.selectWindow();
		%this.setCollapseGroup(false);
		EWorldEditor.ETransformSelectionDisplayed = true;
	}
}

function ETransformSelection::disableAllButtons( %this ) {
	%this-->GetPosButton.setActive( false );
	%this-->GetRotButton.setActive( false );
	%this-->GetScaleButton.setActive( false );
	%this-->GetSizeButton.setActive( false );

	%this-->ApplyButton.setActive( false );
}

function ETransformSelection::onSelectionChanged( %this ) {
	// Count the number of selected SceneObjects.  There are
	// other object classes that could be selected, such
	// as SimGroups.
	%count = EWorldEditor.getSelectionSize();


	%sceneObjects = 0;
	%globalBoundsObjects = 0;
	for( %i=0; %i<%count; %i++) {
		%obj = EWorldEditor.getSelectedObject( %i );
		if( %obj.isMemberOfClass("SceneObject") ) {
			%sceneObjects++;

			if( %obj.isGlobalBounds() ) {
				%globalBoundsObjects++;
			}
		}
	}

	if( %sceneObjects == 0 ) {
		// With nothing selected, disable all Get buttons
		%this.disableAllButtons();
	} else if( %sceneObjects == 1 ) {
		// With one selected, all Get buttons are active
		%this-->GetPosButton.setActive( true );
		%this-->GetRotButton.setActive( true );

		// Special case for Scale and Size for global bounds objects
		if( %globalBoundsObjects == 1 ) {
			%this-->GetSizeButton.setActive( false );
			%this-->GetScaleButton.setActive( false );
		} else {
			%this-->GetSizeButton.setActive( true );
			%this-->GetScaleButton.setActive( true );
		}

		%this-->ApplyButton.setActive( true );
	} else {
		// With more than one selected, only the position button
		// is active
		%this-->GetPosButton.setActive( true );
		%this-->GetRotButton.setActive( false );
		%this-->GetScaleButton.setActive( false );
		%this-->GetSizeButton.setActive( false );

		%this-->ApplyButton.setActive( true );

		// If both RotRelative and RotLocal are unchecked, then go with RotLocal
		if( %this-->RotRelative.getValue() == 0 && %this-->RotLocal.getValue() == 0 ) {
			%this-->RotLocal.setStateOn( true );
		}
	}
}

function ETransformSelection::apply( %this ) {
	%position = %this-->DoPosition.getValue();
	%p = %this-->PosX.getValue() SPC %this-->PosY.getValue() SPC %this-->PosZ.getValue();
	%relativePos = %this-->PosRelative.getValue();

	%rotate = %this-->DoRotation.getValue();
	%r = mDegToRad(%this-->Pitch.getValue()) SPC mDegToRad(%this-->Bank.getValue()) SPC mDegToRad(%this-->Heading.getValue());
	%relativeRot = %this-->RotRelative.getValue();
	%rotLocal = %this-->RotLocal.getValue();

	// We need to check which Tab page is active
	if( %this-->ScaleTabBook.getSelectedPage() == 0 ) {
		// Scale Page
		%scale = %this-->DoScale.getValue();
		%s = %this-->ScaleX.getValue() SPC %this-->ScaleY.getValue() SPC %this-->ScaleZ.getValue();
		%sRelative = %this-->ScaleRelative.getValue();
		%sLocal = %this-->ScaleLocal.getValue();

		%size = false;
	} else {
		// Size Page
		%size = %this-->DoSize.getValue();
		%s = %this-->SizeX.getValue() SPC %this-->SizeY.getValue() SPC %this-->SizeZ.getValue();
		%sRelative = %this-->SizeRelative.getValue();
		%sLocal = %this-->SizeLocal.getValue();

		%scale = false;
	}

	EWorldEditor.transformSelection(%position, %p, %relativePos, %rotate, %r, %relativeRot, %rotLocal, %scale ? 1 : (%size ? 2 : 0), %s, %sRelative, %sLocal);
}

function ETransformSelection::getAbsPosition( %this ) {
	%pos = EWorldEditor.getSelectionCentroid();
	%this-->PosX.setText(getWord(%pos, 0));
	%this-->PosY.setText(getWord(%pos, 1));
	%this-->PosZ.setText(getWord(%pos, 2));

	// Turn off relative as we're populating absolute values
	%this-->PosRelative.setValue(0);

	// Finally, set the Position check box as active.  The user
	// likely wants this if they're getting the position.
	%this-->DoPosition.setValue(1);
}

function ETransformSelection::getAbsRotation( %this ) {
	%count = EWorldEditor.getSelectionSize();

	// If we have more than one SceneObject selected,
	// we must exit.
	%obj = -1;
	for( %i=0; %i<%count; %i++) {
		%test = EWorldEditor.getSelectedObject( %i );
		if( %test.isMemberOfClass("SceneObject") ) {
			if( %obj != -1 )
				return;

			%obj = %test;
		}
	}

	if( %obj == -1 ) {
		// No SceneObjects selected
		return;
	}

	%rot = %obj.getEulerRotation();
	%this-->Pitch.setText(getWord(%rot, 0));
	%this-->Bank.setText(getWord(%rot, 1));
	%this-->Heading.setText(getWord(%rot, 2));

	// Turn off relative as we're populating absolute values.
	// Of course this means that we need to set local on.
	%this-->RotRelative.setValue(0);
	%this-->RotLocal.setValue(1);

	// Finally, set the Rotation check box as active.  The user
	// likely wants this if they're getting the position.
	%this-->DoRotation.setValue(1);
}

function ETransformSelection::getAbsScale( %this ) {
	%count = EWorldEditor.getSelectionSize();

	// If we have more than one SceneObject selected,
	// we must exit.
	%obj = -1;
	for( %i=0; %i<%count; %i++) {
		%test = EWorldEditor.getSelectedObject( %i );
		if( %test.isMemberOfClass("SceneObject") ) {
			if( %obj != -1 )
				return;

			%obj = %test;
		}
	}

	if( %obj == -1 ) {
		// No SceneObjects selected
		return;
	}

	%scale = %obj.scale;
	%scalex = getWord(%scale, 0);
	%this-->ScaleX.setText(%scalex);
	if( ETransformSelectionScaleProportional.getValue() == false ) {
		%this-->ScaleY.setText(getWord(%scale, 1));
		%this-->ScaleZ.setText(getWord(%scale, 2));
	} else {
		%this-->ScaleY.setText(%scalex);
		%this-->ScaleZ.setText(%scalex);
	}

	// Turn off relative as we're populating absolute values
	%this-->ScaleRelative.setValue(0);

	// Finally, set the Scale check box as active.  The user
	// likely wants this if they're getting the position.
	%this-->DoScale.setValue(1);
}

function ETransformSelection::getAbsSize( %this ) {
	%count = EWorldEditor.getSelectionSize();

	// If we have more than one SceneObject selected,
	// we must exit.
	%obj = -1;
	for( %i=0; %i<%count; %i++) {
		%test = EWorldEditor.getSelectedObject( %i );
		if( %test.isMemberOfClass("SceneObject") ) {
			if( %obj != -1 )
				return;

			%obj = %test;
		}
	}

	if( %obj == -1 ) {
		// No SceneObjects selected
		return;
	}

	%size = %obj.getObjectBox();
	%scale = %obj.getScale();

	%sizex = (getWord(%size, 3) - getWord(%size, 0)) * getWord(%scale, 0);
	%this-->SizeX.setText( %sizex );
	if( ETransformSelectionSizeProportional.getValue() == false ) {
		%this-->SizeY.setText( (getWord(%size, 4) - getWord(%size, 1)) * getWord(%scale, 1) );
		%this-->SizeZ.setText( (getWord(%size, 5) - getWord(%size, 2)) * getWord(%scale, 2) );
	} else {
		%this-->SizeY.setText( %sizex );
		%this-->SizeZ.setText( %sizex );
	}

	// Turn off relative as we're populating absolute values
	%this-->SizeRelative.setValue(0);

	// Finally, set the Size check box as active.  The user
	// likely wants this if they're getting the position.
	%this-->DoSize.setValue(1);
}

function ETransformSelection::RotRelativeChanged( %this ) {
	if( %this-->RotRelative.getValue() == 0 ) {
		// With absolute rotation, it must happen locally
		%this-->RotLocal.setStateOn( true );
	}
}

function ETransformSelection::RotLocalChanged( %this ) {
	if( %this-->RotLocal.getValue() == 0 ) {
		// Non-local rotation can only happen relatively
		%this-->RotRelative.setStateOn( true );
	}
}

//-----------------------------------------------------------------------------

function ETransformSelectionScaleProportional::onClick( %this ) {
	if( %this.getValue() == 1 ) {
		%scalex = %this-->ScaleX.getValue();
		%this-->ScaleY.setValue( %scalex );
		%this-->ScaleZ.setValue( %scalex );

		%this-->ScaleY.setActive( false );
		%this-->ScaleZ.setActive( false );
	} else {
		%this-->ScaleY.setActive( true );
		%this-->ScaleZ.setActive( true );
	}

	Parent::onClick(%this);
}

function ETransformSelectionSizeProportional::onClick( %this ) {
	if( %this.getValue() == 1 ) {
		%scalex = %this-->SizeX.getValue();
		%this-->SizeY.setValue( %scalex );
		%this-->SizeZ.setValue( %scalex );

		%this-->SizeY.setActive( false );
		%this-->SizeZ.setActive( false );
	} else {
		%this-->SizeY.setActive( true );
		%this-->SizeZ.setActive( true );
	}

	Parent::onClick(%this);
}

//-----------------------------------------------------------------------------

function ETransformSelectionButtonClass::onClick( %this ) {
	%id = %this.getRoot().getFirstResponder();
	if( %id > -1 && %this.controlIsChild(%id) ) {
		(%id).clearFirstResponder(true);
	}
}

function ETransformSelectionCheckBoxClass::onClick( %this ) {
	%id = %this.getRoot().getFirstResponder();
	if( %id > -1 && %this.controlIsChild(%id) ) {
		(%id).clearFirstResponder(true);
	}
}

//-----------------------------------------------------------------------------

function ETransformSelectionTextEdit::onGainFirstResponder( %this ) {
	if( %this.isActive() ) {
		%this.selectAllText();
	}
}

function ETransformSelectionTextEdit::onValidate( %this ) {
	if( %this.getInternalName() $= "ScaleX" && ETransformSelectionScaleProportional.getValue() == true ) {
		// Set the Y and Z values to match
		%scalex = %this-->ScaleX.getValue();
		%this-->ScaleY.setValue( %scalex );
		%this-->ScaleZ.setValue( %scalex );
	}

	if( %this.getInternalName() $= "SizeX" && ETransformSelectionSizeProportional.getValue() == true ) {
		// Set the Y and Z values to match
		%sizex = %this-->SizeX.getValue();
		%this-->SizeY.setValue( %sizex );
		%this-->SizeZ.setValue( %sizex );
	}
}


function ETransformSelection::getPos( %this,%id ) {
	%pos = EWorldEditor.getSelectionCentroid();
	%edit = SceneEditorTransformTools.findObjectByInternalName("Pos"@%id,true);
	%edit.setText(getWord(%pos, %id));

	// Turn off relative as we're populating absolute values
	SceneEditorTransformTools-->PosRelative.setValue(0);

	// Finally, set the Position check box as active.  The user
	// likely wants this if they're getting the position.
	SceneEditorTransformTools-->DoPosition.setValue(1);
}
function ETransformSelection::setPos( %this,%id ) {
	%edit = SceneEditorTransformTools.findObjectByInternalName("Pos"@%id,true);
	%newPos = %edit.getValue();
	%obj = EWorldEditor.getSelectedObject(0);
	%objPos = %obj.getPosition();
	%finalPos = setWord(%objPos,%id,%newPos);
	//object->transformSelection(position, point, relativePos, rotate, rotation, relativeRot, rotLocal, scaleType, scale, sRelative, sLocal);
	EWorldEditor.transformSelection(true,%newPos,false,false,"",false,false,   false,false,false,false);
	devLog("Obj Position changed from:",%objPos,"to",%finalPos);
}
*/