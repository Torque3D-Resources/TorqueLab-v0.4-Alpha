//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$Lab_REP_DefaultNodeWidthRange = "0 50";
//==============================================================================
// Road Editor Params - Used set default settings and build plugins options GUI
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function VehicleEditorPlugin::initParamsArray( %this,%array ) {
	%array.group[%gId++] = "Grid settings";
	%array.setVal("ShowGrid",       "1" TAB "ShowGrid" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	%array.setVal("GridSize",       "0.1" TAB "GridSize" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	%array.setVal("GridDimension",       "40 40" TAB "GridDimension" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	
	%array.group[%gId++] = "Sun settings";
	%array.setVal("SunDiffuseColor",       "255 255 255 255" TAB "SunDiffuseColor" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	%array.setVal("SunAmbientColor",       "180 180 180 255" TAB "SunAmbientColor" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	%array.setVal("SunAngleX",       "45" TAB "SunAngleX" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
	%array.setVal("SunAngleZ",       "135" TAB "SunAngleZ" TAB "TextEdit" TAB "" TAB "ShapeEdShapeView" TAB %gId);
}
//------------------------------------------------------------------------------

//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================

//==============================================================================
// Called when TorqueLab is launched for first time
function VehicleEditorPlugin::onWorldEditorStartup( %this ) {
	Parent::onWorldEditorStartup( %this );
	// Add ourselves to the Editor Settings window
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::onActivated( %this ) {	
	Parent::onActivated(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::onDeactivated( %this ) {	
	Parent::onDeactivated(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::onEditMenuSelect( %this, %editMenu ) {	
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::handleDelete( %this ) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::handleEscape( %this ) {	
	return VehicleEditorGui.onEscapePressed();
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::isDirty( %this ) {
	return VehicleEditorGui.isDirty;
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::onSaveMission( %this, %missionFile ) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
function VehicleEditorPlugin::setEditorFunction( %this ) {
	return true;
}
//------------------------------------------------------------------------------
