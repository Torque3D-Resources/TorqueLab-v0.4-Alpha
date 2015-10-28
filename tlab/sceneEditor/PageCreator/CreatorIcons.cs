//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::findIconCtrl( %this, %name ) {
	for ( %i = 0; %i < %this.contentCtrl.getCount(); %i++ ) {
		%ctrl = %this.contentCtrl.getObject( %i );

		if ( %ctrl.text $= %name )
			return %ctrl;
	}

	return -1;
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::createIcon( %this ) {
	%ctrl = new GuiIconButtonCtrl() {
		profile = "ToolsButtonArray";
		buttonType = "radioButton";
		groupNum = "-1";
	};

	if ( %this.isList ) {
		%ctrl.iconLocation = "Left";
		%ctrl.textLocation = "Right";
		%ctrl.extent = "348 19";
		%ctrl.textMargin = 8;
		%ctrl.buttonMargin = "2 2";
		%ctrl.autoSize = true;
	} else {
		%ctrl.iconLocation = "Center";
		%ctrl.textLocation = "Bottom";
		%ctrl.extent = "40 40";
	}

	return %ctrl;
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::addFolderIcon( %this, %text ) {
	%ctrl = %this.createIcon();
	%ctrl.altCommand = "SEP_Creator.navigateDown(\"" @ %text @ "\");";
	%ctrl.iconBitmap = "tlab/gui/icons/24-assets/folder_open.png";
	%ctrl.text = %text;
	%ctrl.tooltip = %text;
	%ctrl.class = "CreatorFolderIconBtn";
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%this.contentCtrl.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::addMissionObjectIcon( %this, %class, %name, %buildfunc ) {
	%ctrl = %this.createIcon();
	// If we don't find a specific function for building an
	// object then fall back to the stock one
	%method = "build" @ %buildfunc;
	devLog("Method = ",%method);
	if( !ObjectBuilderGui.isMethod( %method ) )
		%method = "build" @ %class;
	
	if( !ObjectBuilderGui.isMethod( %method ) )
		%method = "build" @ %class;
		
	if( !ObjectBuilderGui.isMethod( %method ) ){
		%func = "build" @ %buildfunc;
		if (isFunction("build" @ %buildfunc))
			%cmd = "return build" @ %buildfunc @ "();";
		else
			%cmd = "return new " @ %class @ "();";
	}
	else{
		%cmd = "ObjectBuilderGui." @ %method @ "();";
	}

	%ctrl.altCommand = "ObjectBuilderGui.newObjectCallback = \"SEP_Creator.onFinishCreateObject\"; SEP_Creator.createObject( \"" @ %cmd @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( %class );
	%ctrl.text = %name;
	%ctrl.class = "CreatorMissionObjectIconBtn";
	%ctrl.tooltip = %class;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%this.contentCtrl.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::addShapeIcon( %this, %datablock ) {
	%ctrl = %this.createIcon();
	%name = %datablock.getName();
	%class = %datablock.getClassName();
	%cmd = %class @ "::create(" @ %name @ ");";
	%shapePath = ( %datablock.shapeFile !$= "" ) ? %datablock.shapeFile : %datablock.shapeName;
	%createCmd = "Scene.createObject( \\\"" @ %cmd @ "\\\" );";
	%ctrl.altCommand = "ColladaImportDlg.showDialog( \"" @ %shapePath @ "\", \"" @ %createCmd @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( %class );
	%ctrl.text = %name;
	%ctrl.class = "CreatorShapeIconBtn";
	%ctrl.tooltip = %name;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%this.contentCtrl.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::addStaticIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%createCmd = "Scene.createStatic( \\\"" @ %fullPath @ "\\\" );";
	%ctrl.altCommand = "ColladaImportDlg.showDialog( \"" @ %fullPath @ "\", \"" @ %createCmd @ "\" );";
	%ctrl.iconBitmap = ( ( %ext $= ".dts" ) ? EditorIconRegistry::findIconByClassName( "TSStatic" ) : "tlab/gui/icons/default/iconCollada" );
	%ctrl.text = %file;
	%ctrl.class = "CreatorStaticIconBtn";
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%this.contentCtrl.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::addPrefabIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%ctrl.altCommand = "Scene.createPrefab( \"" @ %fullPath @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( "Prefab" );
	%ctrl.text = %file;
	%ctrl.class = "CreatorPrefabIconBtn";
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%this.contentCtrl.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
