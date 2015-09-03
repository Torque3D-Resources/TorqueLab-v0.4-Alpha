//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Plugin Object Params - Used set default settings and build plugins options GUI
//==============================================================================

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function MaterialLabPlugin::initParamsArray( %this,%array ) {
	$MaterialEdCfg = newScriptObject("MaterialLabCfg");
	%array.group[%groupId++] = "General settings";
	%array.setVal("DefaultMaterialFile",       "10" TAB "Default Width" TAB "SliderEdit"  TAB "range>>0 100;;tickAt>>1" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("DiffuseSuffix",       "_n" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("AutoAddNormal",       "1" TAB "Auto add normal if found" TAB "checkbox"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("NormalSuffix",       "_n" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("AutoAddSpecular",       "1" TAB "Auto add normal if found" TAB "checkbox"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("SpecularSuffix",       "_s" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("EnablePBR",       "1" TAB "Enable PBR Materials" TAB "checkbox"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
}

//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================
//------------------------------------------------------------------------------
// Material Editor


//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================

//==============================================================================
// Called when TorqueLab is launched for first time
function MaterialLabPlugin::onWorldEditorStartup( %this ) {
	Parent::onWorldEditorStartup( %this );
	%this.customPalette = "SceneEditorPalette";
	%map = new ActionMap();
	%map.bindCmd( keyboard, "1", "EWorldEditorNoneModeBtn.performClick();", "" );  // Select
	%map.bindCmd( keyboard, "2", "EWorldEditorMoveModeBtn.performClick();", "" );  // Move
	%map.bindCmd( keyboard, "3", "EWorldEditorRotateModeBtn.performClick();", "" );  // Rotate
	%map.bindCmd( keyboard, "4", "EWorldEditorScaleModeBtn.performClick();", "" );  // Scale
	%map.bindCmd( keyboard, "f", "FitToSelectionBtn.performClick();", "" );// Fit Camera to Selection
	%map.bindCmd( keyboard, "z", "EditorGuiStatusBar.setCamera(\"Standard Camera\");", "" );// Free Camera
	%map.bindCmd( keyboard, "n", "ToggleNodeBar->renderHandleBtn.performClick();", "" );// Render Node
	%map.bindCmd( keyboard, "shift n", "ToggleNodeBar->renderTextBtn.performClick();", "" );// Render Node Text
	%map.bindCmd( keyboard, "alt s", "MatLab.save();", "" );// Save Material
	//%map.bindCmd( keyboard, "delete", "ToggleNodeBar->renderTextBtn.performClick();", "" );// delete Material
	%map.bindCmd( keyboard, "g", "ESnapOptions-->GridSnapButton.performClick();" ); // Grid Snappping
	%map.bindCmd( keyboard, "t", "SnapToBar->objectSnapDownBtn.performClick();", "" );// Terrain Snapping
	%map.bindCmd( keyboard, "b", "SnapToBar-->objectSnapBtn.performClick();" ); // Soft Snappping
	%map.bindCmd( keyboard, "v", "SceneEditorToolbar->boundingBoxColBtn.performClick();", "" );// Bounds Selection
	%map.bindCmd( keyboard, "o", "EToolbarObjectCenterDropdown->objectBoxBtn.performClick(); objectCenterDropdown.toggle();", "" );// Object Center
	%map.bindCmd( keyboard, "p", "EToolbarObjectCenterDropdown->objectBoundsBtn.performClick(); objectCenterDropdown.toggle();", "" );// Bounds Center
	%map.bindCmd( keyboard, "k", "EToolbarObjectTransformDropdown->objectTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// Object Transform
	%map.bindCmd( keyboard, "l", "EToolbarObjectTransformDropdown->worldTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// World Transform
	MaterialLabPlugin.map = %map;
	MatLab.fileSpec = "Torque Material Files (materials.cs)|materials.cs|All Files (*.*)|*.*|";
	MatLab.textureFormats = "Image Files (*.png, *.jpg, *.dds, *.bmp, *.gif, *.jng. *.tga)|*.png;*.jpg;*.dds;*.bmp;*.gif;*.jng;*.tga|All Files (*.*)|*.*|";
	MatLab.modelFormats = "DTS Files (*.dts)|*.dts";
	MatLab.lastTexturePath = "";
	MatLab.lastTextureFile = "";
	MatLab.lastModelPath = "";
	MatLab.lastModelFile = "";
	MatLab.currentMaterial = "";
	MatLab.lastMaterial = "";
	MatLab.currentCubemap = "";
	MatLab.currentObject = "";
	MatLab.livePreview = "1";
	MatLab.currentLayer = "0";
	MatLab.currentMode = "Material";
	MatLab.currentMeshMode = "EditorShape";
	new ArrayObject(LabUnlistedCubemaps);
	UnlistedCubemaps.add( "LabUnlistedCubemaps", matLabCubeMapPreviewMat );
	UnlistedCubemaps.add( "LabUnlistedCubemaps", WarnMatCubeMap );
	//MaterialLab persistence manager
	new PersistenceManager(matLab_PM);
	MatLab.establishMaterials();
	MatLab.rows = "0 230";
	MatLab.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is activated (Active TorqueLab plugin)
function MaterialLabPlugin::onActivated( %this ) {
	MaterialLabTools.init();
	MaterialLabGui.init();
	MatLab.initGui();	
	Parent::onActivated(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is deactivated (active to inactive transition)
function MaterialLabPlugin::onDeactivated( %this ) {
	if($wasInWireFrameMode)
		$gfx::wireframe = true;

	WEditorPlugin.onDeactivated();
		// if we quit, restore with notDirty
	if(MatLab.materialDirty) {
		//keep on doing this
		MatLab.copyMaterials( notDirtyMaterialLab, materialLab_previewMaterial );
		MatLab.copyMaterials( notDirtyMaterialLab, MatLab.currentMaterial );
		MatLab.guiSync( materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();
		MatLab.currentMaterial.flush();
		MatLab.currentMaterial.reload();
	}

	if( isObject(MatLab.currentMaterial) ) {
		MatLab.lastMaterial = MatLab.currentMaterial.getName();
	}

	MatLab.setMaterialNotDirty();
	// First delete the model so that it releases
	// material instances that use the preview materials.
	matLab_previewObjectView.deleteModel();
	// Now we can delete the preview materials and shaders
	// knowing that there are no matinstances using them.
	matLabCubeMapPreviewMat.delete();
	materialLab_previewMaterial.delete();
	materialLab_justAlphaMaterial.delete();
	materialLab_justAlphaShader.delete();
	$MaterialLab_MaterialsLoaded = false;
	
	SceneEditorToolbar.setVisible( false );
	
	Parent::onDeactivated(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
// Called from TorqueLab after plugin is initialize to set needed settings
function MaterialLabPlugin::onPluginCreated( %this ) {
	
}
//------------------------------------------------------------------------------

//==============================================================================
// Called when the mission file has been saved
function MaterialLabPlugin::onSaveMission( %this, %file ) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when TorqueLab is closed
function MaterialLabPlugin::onEditorSleep( %this ) {
}
//------------------------------------------------------------------------------
//==============================================================================
//Called when editor is selected from menu
function MaterialLabPlugin::onEditMenuSelect( %this, %editMenu ) {
	WEditorPlugin.onEditMenuSelect( %editMenu );
}
//------------------------------------------------------------------------------
