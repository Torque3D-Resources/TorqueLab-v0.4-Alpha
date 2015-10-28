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
function MaterialEditorGui::initGui(%this,%params ) {
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
// MaterialEditorGui.activatePBR();
function MaterialEditorGui::togglePBR(%this ) {
	%this.activatePBR(!MatEd.PBRenabled);
}
//------------------------------------------------------------------------------
//==============================================================================
// MaterialEditorGui.activatePBR();
function MaterialEditorGui::activatePBR(%this,%activate ) {
	if (%activate $= "")
		%activate = true;
	
	MatEd.PBRenabled = %activate;
	pbr_lightInfuenceProperties.visible = %activate;
	pbr_materialDamageProperties.visible = %activate;
	pbr_lightingProperties.visible = %activate;
	pbr_accumulationProperties.visible = %activate;
	MEP_SpecularContainer.visible = !%activate;	
}
//------------------------------------------------------------------------------
//==============================================================================
// Mode : 0 = all maps >> 1 = Comp only >> 2 = No COmp
function MatEd::pbrMapMode(%this,%mode ) {
	switch$(%mode){
		case "0":
			pbr_lightInfuenceProperties-->compMap.visible = 1;
			pbr_lightInfuenceProperties-->smoothMap.visible = 1;
			pbr_lightInfuenceProperties-->aoMap.visible = 1;
			pbr_lightInfuenceProperties-->metalMap.visible = 1;
		case "1":
			pbr_lightInfuenceProperties-->compMap.visible = 1;
			pbr_lightInfuenceProperties-->smoothMap.visible = 0;
			pbr_lightInfuenceProperties-->aoMap.visible = 0;
			pbr_lightInfuenceProperties-->metalMap.visible = 0;
		case "2":
			pbr_lightInfuenceProperties-->compMap.visible = 0;
			pbr_lightInfuenceProperties-->smoothMap.visible = 1;
			pbr_lightInfuenceProperties-->aoMap.visible = 1;
			pbr_lightInfuenceProperties-->metalMap.visible = 1;
	}
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
function MaterialEditorPlugin::toggleMap(%this,%ctrl) {
	devLog("MaterialEditorPlugin::toggleMap(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
	%map = %ctrl.internalName;
	%visible = %ctrl.isStateOn();
	$MEP_ShowMap[%map] = %visible;
	%cont = MEP_TextureMapStack.findObjectByInternalName(%map,true);
	%cont.setVisible(%visible);
	MaterialEditorPlugin.setCfg("PropShowMap_"@%map,%visible);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorPlugin::toggleGroup(%this,%ctrl) {
	devLog("MaterialEditorPlugin::toggleGroup(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
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

// not yet functional
function MaterialEditorMapThumbnail::onRightClick( %this ) {
	if( !isObject( "MaterialEditorMapThumbnailPopup" ) )
		new PopupMenu( MaterialEditorMapThumbnailPopup ) {
		superClass = "MenuBuilder";
		isPopup = true;
		item[ 0 ] = "Open File" TAB "" TAB "openFile( %this.filePath );";
		item[ 1 ] = "Open Folder" TAB "" TAB "openFolder( filePath( %this.filePath ) );";
		filePath = "";
	};

	// Find the text control containing the filename.
	%textCtrl = %this.parentGroup.findObjectByInternalName( %this.fileNameTextCtrl, true );

	if( !%textCtrl )
		return;

	%fileName = %textCtrl.getText();
	%fullPath = makeFullPath( %fileName, getMainDotCsDir() );
	// Construct a full path.
	%isValid = isFile( %fullPath );

	if( !%isValid ) {
		if( isFile( %fileName ) ) {
			%fullPath = %fileName;
			%isValid = true;
		} else {
			// Try material-relative path.
			%material = MaterialEditorGui.currentMaterial;

			if( isObject( %material ) ) {
				%materialPath = filePath( makeFullPath( %material.getFilename(), getMainDotCsDir() ) );
				%fullPath = makeFullPath( %fileName, %materialPath );
				%isValid = isFile( %fullPath );
			}
		}
	}

	%popup = MaterialEditorMapThumbnailPopup;
	%popup.enableItem( 0, %isValid );
	%popup.enableItem( 1, %isValid );
	%popup.filePath = %fullPath;
	%popup.showPopup( Canvas );
}
//------------------------------------------------------------------------------

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
