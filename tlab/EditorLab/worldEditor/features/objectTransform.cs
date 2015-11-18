//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Synchronize WorldEditor GUI parameters with current plugin
function Lab::rotateSelection( %this,%rotation,%notLocal ) {
	//Transform to radiant
	%applyRot = %rotation;
	%applyRot.x =mDegToRad(%rotation.x);
	%applyRot.y = mDegToRad(%rotation.y);
	%applyRot.z = mDegToRad(%rotation.z);
	EWorldEditor.transformSelection(false,"",false,true,%applyRot,!%notLocal,false,   "","",false,false);
	devLog("Rotate=",%rotation,"Apply",%applyRot);
	Lab.rotateSnapSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
// Synchronize WorldEditor GUI parameters with current plugin
function Lab::rotateSnapSelection( %this,%precision,%obj ) {
	if (%precision $= "")
		%precision = 1;

	for(%i = 0; %i < EWorldEditor.getSelectionSize(); %i++) {
		%obj = EWorldEditor.getSelectedObject(%i);
		%trans = %obj.getTransform();
		%trans = %obj.rotation;
		%rot = getWord(%trans,3);
		%rot = mRound(%rot);
		%newTrans = setWord(%trans,3,%rot);
		%obj.rotation = %newTrans;
		devLog("Snap rotation from:",%trans,"To",%newTrans,"Rot",%rot);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function EWorldEditor::syncToolPalette( %this ) {
	switch$ ( GlobalGizmoProfile.mode ) {
	case "None":
		EWorldEditorNoneModeBtn.performClick();

	case "Move":
		EWorldEditorMoveModeBtn.performClick();

	case "Rotate":
		EWorldEditorRotateModeBtn.performClick();

	case "Scale":
		EWorldEditorScaleModeBtn.performClick();
	}
}
//------------------------------------------------------------------------------

