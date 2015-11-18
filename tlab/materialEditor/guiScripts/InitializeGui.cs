//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$MEP_ShowGroupCtrl["Animation"] = "materialAnimationPropertiesRollout";
$MEP_ShowGroupCtrl["Rendering"] = "MEP_GroupRendering";
$MEP_ShowGroupCtrl["Advanced"] = "materialAdvancedPropertiesRollout";
$MEP_ShowGroupCtrl["Lighting"] = "MEP_GroupLighting";



//==============================================================================
// Automated editor plugin setting interface
function MaterialEditorGui::activateGui(%this ) {
	advancedTextureMapsRollout.Expanded = false;
	materialAnimationPropertiesRollout.Expanded = false;
	materialAdvancedPropertiesRollout.Expanded = false;
	%this-->previewOptions.expanded = false;
	MatEd.pbrMapMode(MatEd.MapModePBR);
	%radio = MatEd_PBRmodeRadios.findObjectByInternalName(MatEd.MapModePBR);
	%radio.setStateOn(true);
}
//------------------------------------------------------------------------------

//==============================================================================
function MaterialEditorPlugin::initPropertySetting(%this) {
	devLog("MaterialEditorPlugin::initPropertySetting(%this)",%this,%ctrl,"Name",%ctrl.internalName);
	%textureMapStack = MaterialEditorGui-->textureMapsOptionsStack;

	foreach(%ctrl in %textureMapStack) {
		%map = %ctrl.internalName;
		%ctrl.variable = "$Cfg_Plugins_MaterialEditor_PropShowMap_"@%map;
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Material Properties Display Options Callback
//==============================================================================
//==============================================================================
function MaterialEditorGui::toggleMap(%this,%ctrl) {
	devLog("MaterialEditorGui::toggleMap(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
	%map = %ctrl.internalName;
	%visible = %ctrl.isStateOn();
	$MEP_ShowMap[%map] = %visible;
	%cont = MEP_TextureMapStack.findObjectByInternalName(%map,true);
	%cont.setVisible(%visible);
	MaterialEditorPlugin.setCfg("PropShowMap_"@%map,%visible);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::toggleGroup(%this,%ctrl) {
	devLog("MaterialEditorGui::toggleGroup(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
	%group = %ctrl.internalName;
	%visible = %ctrl.isStateOn();
	%groupCtrl = $MEP_ShowGroupCtrl[%group];

	if (!isObject(%groupCtrl))
		return;

	%groupCtrl.setVisible(%visible);
	MaterialEditorPlugin.setCfg("PropShowGroup_"@%group,%visible);
}
//------------------------------------------------------------------------------


//==============================================================================
// Image thumbnail right-clicks.

//==============================================================================
// Set Material GUI Mode (Mesh or Standard)
//==============================================================================
// Set GUI depending of if we have a standard Material or a Mesh Material
function MaterialEditorGui::setMode( %this ) {
	MatEdMaterialMode.setVisible(0);
	MatEdTargetMode.setVisible(0);

	if( isObject(MaterialEditorGui.currentObject) ) {
		MaterialEditorGui.currentMode = "Mesh";
		MatEdTargetMode.setVisible(1);
	} else {
		MaterialEditorGui.currentMode = "Material";
		MatEdMaterialMode.setVisible(1);
		EWorldEditor.clearSelection();
	}
}
//------------------------------------------------------------------------------
