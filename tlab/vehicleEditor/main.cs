//==============================================================================
// TorqueLab -> VehicleEditorPlugin Initialization
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function initializeVehicleEditor() {
	return;
	echo( " - Initializing Vehicle Editor" );
	
	execVEP(true);
	
	Lab.createPlugin("VehicleEditor","Vehicle Editor");
	Lab.addPluginEditor("VehicleEditor",VehicleEditorGui);
	Lab.addPluginGui("VehicleEditor",VehicleEditorTools);
	//Lab.addPluginGui("VehicleEditor",VehicleEditorOptionsWindow);
	//Lab.addPluginGui("VehicleEditor",VehicleEditorTreeWindow);
	Lab.addPluginToolbar("VehicleEditor",VehicleEditorToolbar);
	Lab.addPluginPalette("VehicleEditor",   VehicleEditorPalette);
	
	VehicleEditorPlugin.editorGui = VehicleEditorGui;
	$VEP = newScriptObject("VEP");
	%map = new ActionMap();
	%map.bindCmd( keyboard, "backspace", "VehicleEditorGui.onDeleteKey();", "" );
	%map.bindCmd( keyboard, "1", "VehicleEditorGui.prepSelectionMode();", "" );
	%map.bindCmd( keyboard, "2", "EWToolsPaletteArray->VehicleEditorMoveMode.performClick();", "" );
	%map.bindCmd( keyboard, "4", "EWToolsPaletteArray->VehicleEditorScaleMode.performClick();", "" );
	%map.bindCmd( keyboard, "5", "EWToolsPaletteArray->VehicleEditorAddRoadMode.performClick();", "" );
	%map.bindCmd( keyboard, "=", "EWToolsPaletteArray->VehicleEditorInsertPointMode.performClick();", "" );
	%map.bindCmd( keyboard, "numpadadd", "EWToolsPaletteArray->VehicleEditorInsertPointMode.performClick();", "" );
	%map.bindCmd( keyboard, "-", "EWToolsPaletteArray->VehicleEditorRemovePointMode.performClick();", "" );
	%map.bindCmd( keyboard, "numpadminus", "EWToolsPaletteArray->VehicleEditorRemovePointMode.performClick();", "" );
	%map.bindCmd( keyboard, "z", "VehicleEditorShowSplineBtn.performClick();", "" );
	%map.bindCmd( keyboard, "x", "VehicleEditorWireframeBtn.performClick();", "" );
	%map.bindCmd( keyboard, "v", "VehicleEditorShowRoadBtn.performClick();", "" );
	VehicleEditorPlugin.map = %map;
	//VehicleEditorPlugin.initSettings();
}
//------------------------------------------------------------------------------
//==============================================================================
// Load all the Scripts and GUIs (if specified)
function execVEP(%loadGui) {
	if (%loadGui) {
		exec( "tlab/VehicleEditor/gui/VehicleEditorGui.gui" );
		exec( "tlab/VehicleEditor/gui/VehicleEditorTools.gui" );
		exec( "tlab/VehicleEditor/gui/VehicleEditorToolbar.gui");
		exec( "tlab/VehicleEditor/gui/VehicleEditorPalette.gui");
	}
	
	exec( "tlab/VehicleEditor/VehicleEditorPlugin.cs" );
	execPattern("tlab/VehicleEditor/editor/*.cs");
	execPattern("tlab/VehicleEditor/shapeEd/*.cs");
}
//------------------------------------------------------------------------------
//==============================================================================
function destroyVehicleEditor() {
}
//------------------------------------------------------------------------------