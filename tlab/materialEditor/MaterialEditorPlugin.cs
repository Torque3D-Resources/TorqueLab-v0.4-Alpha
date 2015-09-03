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
function MaterialEditorPlugin::initParamsArray( %this,%array ) {
	$MaterialEdCfg = newScriptObject("MaterialEditorCfg");
	%array.group[%groupId++] = "General settings";
	%array.setVal("DefaultMaterialFile",       "10" TAB "Default Width" TAB "SliderEdit"  TAB "range>>0 100;;tickAt>>1" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("DiffuseSuffix",       "_n" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("AutoAddNormal",       "1" TAB "Auto add normal if found" TAB "checkbox"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("NormalSuffix",       "_n" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("AutoAddSpecular",       "1" TAB "Auto add normal if found" TAB "checkbox"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("SpecularSuffix",       "_s" TAB "Default Normal suffix" TAB "TextEdit"  TAB "" TAB "SceneEditorCfg" TAB %groupId);
	%array.setVal("PBRenabled",       "1" TAB "Enable PBR Materials" TAB "checkbox"  TAB "" TAB "MatEd" TAB %groupId);
	%array.setVal("MapModePBR",       "1" TAB "PBR maps mode" TAB "slider"  TAB "range>>0 2;;ticksAt>>1" TAB "MatEd" TAB %groupId);
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
function MaterialEditorPlugin::onWorldEditorStartup( %this ) {
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
	%map.bindCmd( keyboard, "alt s", "MaterialEditorGui.save();", "" );// Save Material
	//%map.bindCmd( keyboard, "delete", "ToggleNodeBar->renderTextBtn.performClick();", "" );// delete Material
	%map.bindCmd( keyboard, "g", "ESnapOptions-->GridSnapButton.performClick();" ); // Grid Snappping
	%map.bindCmd( keyboard, "t", "SnapToBar->objectSnapDownBtn.performClick();", "" );// Terrain Snapping
	%map.bindCmd( keyboard, "b", "SnapToBar-->objectSnapBtn.performClick();" ); // Soft Snappping
	%map.bindCmd( keyboard, "v", "SceneEditorToolbar->boundingBoxColBtn.performClick();", "" );// Bounds Selection
	%map.bindCmd( keyboard, "o", "EToolbarObjectCenterDropdown->objectBoxBtn.performClick(); objectCenterDropdown.toggle();", "" );// Object Center
	%map.bindCmd( keyboard, "p", "EToolbarObjectCenterDropdown->objectBoundsBtn.performClick(); objectCenterDropdown.toggle();", "" );// Bounds Center
	%map.bindCmd( keyboard, "k", "EToolbarObjectTransformDropdown->objectTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// Object Transform
	%map.bindCmd( keyboard, "l", "EToolbarObjectTransformDropdown->worldTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// World Transform
	MaterialEditorPlugin.map = %map;
	MaterialEditorGui.fileSpec = "Torque Material Files (materials.cs)|materials.cs|All Files (*.*)|*.*|";
	MaterialEditorGui.textureFormats = "Image Files (*.png, *.jpg, *.dds, *.bmp, *.gif, *.jng. *.tga)|*.png;*.jpg;*.dds;*.bmp;*.gif;*.jng;*.tga|All Files (*.*)|*.*|";
	MaterialEditorGui.modelFormats = "DTS Files (*.dts)|*.dts";
	MaterialEditorGui.lastTexturePath = "";
	MaterialEditorGui.lastTextureFile = "";
	MaterialEditorGui.lastModelPath = "";
	MaterialEditorGui.lastModelFile = "";
	MaterialEditorGui.currentMaterial = "";
	MaterialEditorGui.lastMaterial = "";
	MaterialEditorGui.currentCubemap = "";
	MaterialEditorGui.currentObject = "";
	MaterialEditorGui.livePreview = "1";
	MaterialEditorGui.currentLayer = "0";
	MaterialEditorGui.currentMode = "Material";
	MaterialEditorGui.currentMeshMode = "EditorShape";
	new ArrayObject(UnlistedCubemaps);
	UnlistedCubemaps.add( "unlistedCubemaps", matEdCubeMapPreviewMat );
	UnlistedCubemaps.add( "unlistedCubemaps", WarnMatCubeMap );
	//MaterialEditor persistence manager
	new PersistenceManager(matEd_PersistMan);
	MaterialEditorGui.establishMaterials();
	//MaterialEditorGui.rows = "0 230";
	//MaterialEditorGui.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is activated (Active TorqueLab plugin)
function MaterialEditorPlugin::onActivated( %this ) {
	//MaterialEditorGui.rows = "0 230";
	//MaterialEditorGui.updateSizes();

	if($gfx::wireframe) {
		$wasInWireFrameMode = true;
		$gfx::wireframe = false;
	} else {
		$wasInWireFrameMode = false;
	}

	MaterialEditorGui.initGui();
	WEditorPlugin.onActivated();
	MaterialEditorGui-->propertiesOptions.expanded = 0;
	SceneEditorToolbar.setVisible( true );
	MaterialEditorGui.currentObject = $Lab::materialEditorList;
	// Execute the back end scripts that actually do the work.
	MaterialEditorGui.open();
	
	Parent::onActivated(%this);
	hide(MEP_CallbackArea);
	hide(matEd_addCubemapWindow);
	matEd_addCubemapWindow.setVisible(0);
	show(MaterialEditorPreviewWindow);
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is deactivated (active to inactive transition)
function MaterialEditorPlugin::onDeactivated( %this ) {
	if($wasInWireFrameMode)
		$gfx::wireframe = true;

	WEditorPlugin.onDeactivated();
		// if we quit, restore with notDirty
	if(MaterialEditorGui.materialDirty) {
		//keep on doing this
		MaterialEditorGui.copyMaterials( notDirtyMaterial, materialEd_previewMaterial );
		MaterialEditorGui.copyMaterials( notDirtyMaterial, MaterialEditorGui.currentMaterial );
		MaterialEditorGui.guiSync( materialEd_previewMaterial );
		materialEd_previewMaterial.flush();
		materialEd_previewMaterial.reload();
		MaterialEditorGui.currentMaterial.flush();
		MaterialEditorGui.currentMaterial.reload();
	}

	if( isObject(MaterialEditorGui.currentMaterial) ) {
		MaterialEditorGui.lastMaterial = MaterialEditorGui.currentMaterial.getName();
	}

	MaterialEditorGui.setMaterialNotDirty();
	// First delete the model so that it releases
	// material instances that use the preview materials.
	matEd_previewObjectView.deleteModel();
	// Now we can delete the preview materials and shaders
	// knowing that there are no matinstances using them.
	matEdCubeMapPreviewMat.delete();
	materialEd_previewMaterial.delete();
	materialEd_justAlphaMaterial.delete();
	materialEd_justAlphaShader.delete();
	$MaterialEditor_MaterialsLoaded = false;
	
	SceneEditorToolbar.setVisible( false );
	
	Parent::onDeactivated(%this);

	//hide(MaterialEditorPreviewWindow);
}
//------------------------------------------------------------------------------
//==============================================================================
// Called from TorqueLab after plugin is initialize to set needed settings
function MaterialEditorPlugin::onPluginCreated( %this ) {
	
}
//------------------------------------------------------------------------------

//==============================================================================
// Called when the mission file has been saved
function MaterialEditorPlugin::onSaveMission( %this, %file ) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when TorqueLab is closed
function MaterialEditorPlugin::onEditorSleep( %this ) {
}
//------------------------------------------------------------------------------
//==============================================================================
//Called when editor is selected from menu
function MaterialEditorPlugin::onEditMenuSelect( %this, %editMenu ) {
	WEditorPlugin.onEditMenuSelect( %editMenu );
}
//------------------------------------------------------------------------------
