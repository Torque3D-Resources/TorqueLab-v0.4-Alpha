//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function MatLab::establishMaterials(%this,%forced) {
	//Cubemap used to preview other cubemaps in the editor.
	singleton CubemapData( matLabCubeMapPreviewMat ) {
		cubeFace[0] = "tlab/materialLab/assets/cube_xNeg";
		cubeFace[1] = "tlab/materialLab/assets/cube_xPos";
		cubeFace[2] = "tlab/materialLab/assets/cube_ZNeg";
		cubeFace[3] = "tlab/materialLab/assets/cube_ZPos";
		cubeFace[4] = "tlab/materialLab/assets/cube_YNeg";
		cubeFace[5] = "tlab/materialLab/assets/cube_YPos";
		parentGroup = "RootGroup";
	};
	//Material used to preview other materials in the editor.
	singleton Material(materialLab_previewMaterial) {
		mapTo = "matLab_mappedMat";
		diffuseMap[0] = "tlab/materialLab/assets/matLab_mappedMat";
	};
	singleton CustomMaterial( materialLab_justAlphaMaterial ) {
		mapTo = "matLab_mappedMatB";
		texture[0] = materialLab_previewMaterial.diffuseMap[0];
	};
	//Custom shader to allow the display of just the alpha channel.
	singleton ShaderData( materialLab_justAlphaShader ) {
		DXVertexShaderFile 	= "shaders/alphaOnlyV.hlsl";
		DXPixelShaderFile 	= "shaders/alphaOnlyP.hlsl";
		pixVersion = 1.0;
	};
}
//------------------------------------------------------------------------------
//==============================================================================
function MatLab::open(%this) {
	logc("MatLab::open(%this)",%this);
	MatLab.establishMaterials();
	// We hide these specific windows here due too there non-modal nature.
	// These guis are also pushed onto Canvas, which means they shouldn't be parented
	// by editorgui
	materialSelector.setVisible(0);
	matLabSaveDialog.setVisible(0);
	MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(0);
	//Setup our dropdown menu contents.
	//Blending Modes
	MaterialLabPropertiesWindow-->blendingTypePopUp.clear();
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(None,0);
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(Mul,1);
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(Add,2);
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(AddAlpha,3);
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(Sub,4);
	MaterialLabPropertiesWindow-->blendingTypePopUp.add(LerpAlpha,5);
	MaterialLabPropertiesWindow-->blendingTypePopUp.setSelected( 0, false );
	//Reflection Types
	MaterialLabPropertiesWindow-->reflectionTypePopUp.clear();
	MaterialLabPropertiesWindow-->reflectionTypePopUp.add("None",0);
	MaterialLabPropertiesWindow-->reflectionTypePopUp.add("cubemap",1);
	MaterialLabPropertiesWindow-->reflectionTypePopUp.setSelected( 0, false );
	//Sounds
	MaterialLabPropertiesWindow-->footstepSoundPopup.clear();
	MaterialLabPropertiesWindow-->impactSoundPopup.clear();
	%sounds = "<None>" TAB "<Soft>" TAB "<Hard>" TAB "<Metal>" TAB "<Snow>";    // Default sounds

	// Get custom sound datablocks
	foreach (%db in DataBlockSet) {
		if (%db.isMemberOfClass("SFXTrack"))
			%sounds = %sounds TAB %db.getName();
	}

	%count = getFieldCount(%sounds);

	for (%i = 0; %i < %count; %i++) {
		%name = getField(%sounds, %i);
		MaterialLabPropertiesWindow-->footstepSoundPopup.add(%name);
		MaterialLabPropertiesWindow-->impactSoundPopup.add(%name);
	}

	//Preview Models
	matLab_quickPreview_Popup.clear();
	matLab_quickPreview_Popup.add("Cube",0);
	matLab_quickPreview_Popup.add("Sphere",1);
	matLab_quickPreview_Popup.add("Pyramid",2);
	matLab_quickPreview_Popup.add("Cylinder",3);
	matLab_quickPreview_Popup.add("Torus",4);
	matLab_quickPreview_Popup.add("Knot",5);
	matLab_quickPreview_Popup.setSelected( 0, false );
	matLab_quickPreview_Popup.selected = matLab_quickPreview_Popup.getText();
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.clear();
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.add("Layer 0",0);
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.add("Layer 1",1);
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.add("Layer 2",2);
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.add("Layer 3",3);
	MaterialLabPropertiesWindow-->MaterialLayerCtrl.setSelected( 0, false );
	//Sift through the RootGroup and find all loaded material items.
	MatLab.updateAllFields();
	MatLab.updatePreviewObject();
	// If no selected object; go to material mode. And edit the last selected material
	MatLab.setMode();
	MatLab.preventUndo = true;

	if( MatLab.currentMode $= "Mesh" )
		MatLab.prepareActiveObject( true );
	else
		MatLab.prepareActiveMaterial( "", true );

	MatLab.preventUndo = false;
}
//------------------------------------------------------------------------------
