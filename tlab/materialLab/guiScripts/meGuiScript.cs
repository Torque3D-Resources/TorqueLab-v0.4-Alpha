//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$MLP_ShowGroupCtrl["Animation"] = "materialAnimationPropertiesRollout";
$MLP_ShowGroupCtrl["Rendering"] = "MLP_GroupRendering";
$MLP_ShowGroupCtrl["Advanced"] = "materialAdvancedPropertiesRollout2";
$MLP_ShowGroupCtrl["Lighting"] = "MLP_GroupLighting";
//==============================================================================
function MaterialLabPlugin::initPropertySetting(%this) {
	devLog("MaterialLabPlugin::initPropertySetting(%this)",%this,%ctrl,"Name",%ctrl.internalName);
	%textureMapStack = MaterialLabGui-->textureMapsOptionsStack;

	foreach(%ctrl in %textureMapStack) {
		%map = %ctrl.internalName;
		%ctrl.variable = "$Cfg_Plugins_MaterialLab_PropShowMap_"@%map;
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Material Properties Display Options Callback
//==============================================================================
//==============================================================================
function MaterialLabPlugin::toggleMap(%this,%ctrl) {
	devLog("MaterialLabPlugin::toggleMap(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
	%map = %ctrl.internalName;
	%visible = %ctrl.isStateOn();
	$MLP_ShowMap[%map] = %visible;
	%cont = MLP_TextureMapStack.findObjectByInternalName(%map,true);
	%cont.setVisible(%visible);
	MaterialLabPlugin.setCfg("PropShowMap_"@%map,%visible);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabPlugin::toggleGroup(%this,%ctrl) {
	devLog("MaterialLabPlugin::toggleGroup(%this,%ctrl)",%this,%ctrl,"Name",%ctrl.internalName);
	%group = %ctrl.internalName;
	%visible = %ctrl.isStateOn();
	%groupCtrl = $MLP_ShowGroupCtrl[%group];

	if (!isObject(%groupCtrl))
		return;

	%groupCtrl.setVisible(%visible);
	MaterialLabPlugin.setCfg("PropShowGroup_"@%group,%visible);
}
//------------------------------------------------------------------------------


//==============================================================================
// Image thumbnail right-clicks.

// not yet functional
function MaterialLabMapThumbnail::onRightClick( %this ) {
	if( !isObject( "MaterialLabMapThumbnailPopup" ) )
		new PopupMenu( MaterialLabMapThumbnailPopup ) {
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
			%material = MaterialLabGui.currentMaterial;

			if( isObject( %material ) ) {
				%materialPath = filePath( makeFullPath( %material.getFilename(), getMainDotCsDir() ) );
				%fullPath = makeFullPath( %fileName, %materialPath );
				%isValid = isFile( %fullPath );
			}
		}
	}

	%popup = MaterialLabMapThumbnailPopup;
	%popup.enableItem( 0, %isValid );
	%popup.enableItem( 1, %isValid );
	%popup.filePath = %fullPath;
	%popup.showPopup( Canvas );
}
//------------------------------------------------------------------------------