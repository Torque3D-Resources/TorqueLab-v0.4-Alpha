//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$SBP_Creator_ShowPrefabInMeshes = false;

//==============================================================================
// Navigate through Creator Book Data
function SBP_Creator::navigateMeshes( %this, %address ) {
	
	%searchExts = "*.dts" TAB "*.dae" TAB "*.kmz" TAB "*.dif";
	if ($SBP_Creator_ShowPrefabInMeshes)
		%searchExts = %searchExts TAB "*.prefab";
	%fullPath = findFirstFileMultiExpr(%searchExts );

	if (SceneBuilderTools.meshRootFolder !$= "")
		devLog("Mesh root setted to:",SceneBuilderTools.meshRootFolder);

	while ( %fullPath !$= "" ) {
		if (SceneBuilderTools.meshRootFolder !$= "") {
			%found = strFind(%fullPath,SceneBuilderTools.meshRootFolder);
			devLog("Check path member of root:",%fullPath,"Found=",%found);

			if (!%found) {
				%fullPath = findNextFileMultiExpr( "*.dts" TAB "*.dae" TAB "*.kmz"  TAB "*.dif" );
				continue;
			}
		}

		if (strstr(%fullPath, "cached.dts") != -1) {
			%fullPath = findNextFileMultiExpr( "*.dts" TAB "*.dae" TAB "*.kmz"  TAB "*.dif" );
			continue;
		}

		%fullPath = makeRelativePath( %fullPath, getMainDotCSDir() );
		%splitPath = strreplace( %fullPath, "/", " " );

		if( getWord(%splitPath, 0) $= "tools" ) {
			%fullPath = findNextFileMultiExpr( "*.dts" TAB "*.dae" TAB "*.kmz"  TAB "*.dif" );
			continue;
		}
		%ext = fileExt(%fullPath);
		
		%dirCount = getWordCount( %splitPath ) - 1;
		%pathFolders = getWords( %splitPath, 0, %dirCount - 1 );
		// Add this file's path (parent folders) to the
		// popup menu if it isn't there yet.
		%temp = strreplace( %pathFolders, " ", "/" );
		%r = SBP_CreatorPopupMenu.findText( %temp );

		if ( %r == -1 ) {
			SBP_CreatorPopupMenu.add( %temp );
		}

		// Is this file in the current folder?
		if ( stricmp( %pathFolders, %address ) == 0 ) {
			if (%ext $= ".prefab")
				%this.addPrefabIcon( %fullPath );
			else
				%this.addMeshesIcon( %fullPath );
		}
		// Then is this file in a subfolder we need to add
		// a folder icon for?
		else {
			%wordIdx = 0;
			%add = false;

			if ( %address $= "" ) {
				%add = true;
				%wordIdx = 0;
			} else {
				for ( ; %wordIdx < %dirCount; %wordIdx++ ) {
					%temp = getWords( %splitPath, 0, %wordIdx );

					if ( stricmp( %temp, %address ) == 0 ) {
						%add = true;
						%wordIdx++;
						break;
					}
				}
			}

			if ( %add == true ) {
				%folder = getWord( %splitPath, %wordIdx );
				%ctrl = %this.findIconCtrl( %folder );

				if ( %ctrl == -1 )
					%this.addFolderIcon( %folder );
			}
		}

		%fullPath = findNextFileMultiExpr( "*.dts" TAB "*.dae" TAB "*.kmz" TAB "*.dif" );
	}
}

//------------------------------------------------------------------------------

//==============================================================================
function SBP_Creator::addMeshesIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%createCmd = "Scene.createMesh( \\\"" @ %fullPath @ "\\\" );";
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
