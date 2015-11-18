//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
//FileBrowser.initFiles
function FileBrowser::initFiles(%this,%path) {
	%lastAddress = FileBrowser.lastAddress;

	if (%lastAddress $= "")
		%lastAddress = "art";

	FileBrowser.navigate( %lastAddress );
}
//------------------------------------------------------------------------------
//==============================================================================
//FileBrowser.initFiles
function FileBrowser::setViewId(%this,%id,%force) {
	if (FileBrowser.currentViewId $= %id && !%force)
		return;

	%text = getField($FileBrowser_View[%i],%id);

	switch$(%id) {
	case "0":
		%width = (FileBrowserArray.extent.x - 10);

	case "1":
		%width = (FileBrowserArray.extent.x - 10) /2;

	case "2":
		%width = (FileBrowserArray.extent.x - 10) /3;
	}

	if (%width !$= "") {
		FileBrowserArray.colSize = %width;
		FileBrowserArray.refresh();
		FileBrowser.currentViewId = %id;
		FileBrowserViewMenu.setText(getField($FileBrowser_View[%id],0));
	}
}
//------------------------------------------------------------------------------
if ( %this.isList ) {
	%width = FileBrowserArray.extent.x - 6;
	FileBrowserArray.colSize = %width;
	%extent = %width SPC "20";
	//%ctrl.extent = %extent;
	//%ctrl.autoSize = true;
} else {
	%width = (FileBrowserArray.extent.x - 10) /2;
	//%extent = %width SPC "20";
	//%ctrl.extent = %extent;
	FileBrowserArray.colSize = %width;
}

//==============================================================================
function FileBrowserMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	FileBrowser.navigate( %split );
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::navigateDown( %this, %folder ) {
	if ( %this.address $= "" )
		%address = %folder;
	else
		%address = %this.address SPC %folder;

	// Because this is called from an IconButton::onClick command
	// we have to wait a tick before actually calling navigate, else
	// we would delete the button out from under itself.
	%this.schedule( 1, "navigate", %address );
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::navigateUp( %this ) {
	%count = getWordCount( %this.address );

	if ( %count == 0 )
		return;

	if ( %count == 1 )
		%address = "";
	else
		%address = getWords( %this.address, 0, %count - 2 );

	%this.navigate( %address );
}
//------------------------------------------------------------------------------

//==============================================================================
// Navigate through Creator Book Data
function FileBrowser::navigate( %this, %address ) {
	FileBrowserArray.frozen = true;
	FileBrowserArray.clear();
	FileBrowserMenu.clear();
	show(FileBrowserArray);
	hide(FileBrowserIconSrc);
	%searchExts = "*.dts" TAB "*.dae" TAB "*.png" TAB "*.dds" TAB "*.tga" TAB "*.prefab";
	FileBrowser.lastAddress = %address;

	if ($SEP_Creator_ShowPrefabInMeshes)
		%searchExts = %searchExts TAB "*.prefab";

	%fullPath = findFirstFileMultiExpr(%searchExts );

	if (SceneEditorTools.meshRootFolder !$= "")
		devLog("Mesh root setted to:",SceneEditorTools.meshRootFolder);

	while ( %fullPath !$= "" ) {
		if (SceneEditorTools.meshRootFolder !$= "") {
			%found = strFind(%fullPath,SceneEditorTools.meshRootFolder);
			devLog("Check path member of root:",%fullPath,"Found=",%found);

			if (!%found) {
				%fullPath = findNextFileMultiExpr( %searchExts );
				continue;
			}
		}

		if (strstr(%fullPath, "cached.dts") != -1) {
			%fullPath = findNextFileMultiExpr( %searchExts );
			continue;
		}

		%fullPath = makeRelativePath( %fullPath, getMainDotCSDir() );
		%splitPath = strreplace( %fullPath, "/", " " );

		if( getWord(%splitPath, 0) $= "tools" ) {
			%fullPath = findNextFileMultiExpr( %searchExts );
			continue;
		}

		%ext = fileExt(%fullPath);
		%dirCount = getWordCount( %splitPath ) - 1;
		%pathFolders = getWords( %splitPath, 0, %dirCount - 1 );
		// Add this file's path (parent folders) to the
		// popup menu if it isn't there yet.
		%temp = strreplace( %pathFolders, " ", "/" );
		%r = FileBrowserMenu.findText( %temp );

		if ( %r == -1 ) {
			FileBrowserMenu.add( %temp );
		}

		// Is this file in the current folder?
		if ( stricmp( %pathFolders, %address ) == 0 ) {
			%this.addFileIcon( %fullPath );
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

		%fullPath = findNextFileMultiExpr(%searchExts );
	}

	FileBrowserArray.sort( "alphaIconCompare" );

	for ( %i = 0; %i < FileBrowserArray.getCount(); %i++ ) {
		FileBrowserArray.getObject(%i).autoSize = false;
	}

	FileBrowser.addFolderUpIcon();
	%this.setViewId($FileBrowser_ViewId);
	FileBrowserArray.refresh();
	/*if ( %this.isList ) {
		%width = FileBrowserArray.extent.x - 6;
		FileBrowserArray.colSize = %width;
		%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		//%ctrl.autoSize = true;
	} else {
		%width = (FileBrowserArray.extent.x - 10) /2;
		//%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		FileBrowserArray.colSize = %width;
	}*/
	//FileBrowserArray.frozen = false;
	//FileBrowserArray.refresh();
	// Recalculate the array for the parent guiScrollCtrl
	FileBrowserArray.getParent().computeSizes();
	%this.address = %address;
	FileBrowserMenu.sort();
	%str = strreplace( %address, " ", "/" );
	%r = FileBrowserMenu.findText( %str );

	if ( %r != -1 )
		FileBrowserMenu.setSelected( %r, false );
	else
		FileBrowserMenu.setText( %str );

	FileBrowserMenu.tooltip = %str;
}

//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::createIcon( %this ) {
	%ctrl = cloneObject(FileBrowserIconSrc);
	%ctrl.profile = "ToolsButtonArray";
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	return %ctrl;
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::addFileIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%stockExt = strreplace(%ext,".","");

	if (strFindWords(strlwr(%ext),"dae dts")) {
		%createCmd = "Scene.createMesh( \"" @ %fullPath @ "\" );";
		%type = "Mesh";
		%ctrl.createCmd = %createCmd;
	} else if (%stockExt $= "prefab") {
		%createCmd = "Scene.createPrefab( \"" @ %fullPath @ "\" );";
		%type = "Prefab";
		%ctrl.createCmd = %createCmd;
	} else if (strFindWords(strlwr(%ext),"png tga jpg bmp dds")) {
		%type = "Image";
	} else	{
		%type = "Unknown";
	}

	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%iconBmp = "tlab/gui/icons/default/file-assets/"@%stockExt@".png";

	if (!isFile(%iconBmp))
		%iconBmp = "tlab/gui/icons/default/file-assets/default.png";

	%ctrl.iconBitmap = %iconBmp;
	%ctrl.altCommand = "FileBrowser.icon"@%type@"Alt($ThisControl);";
	//%ctrl.iconBitmap = ( ( %ext $= ".dts" ) ? EditorIconRegistry::findIconByClassName( "TSStatic" ) : "tlab/gui/icons/default/iconCollada" );
	%ctrl.text = %file;
	%ctrl.type = %type;
	%ctrl.class = "FileBrowserIcon";
	//%ctrl.superClass = "FileBrowserIcon"@%type;
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%ctrl.fullPath = %fullPath;
	FileBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//FileBrowser.addFolderUpIcon
function FileBrowser::addFolderUpIcon( %this ) {
	%ctrl = %this.createIcon();
	%ctrl.command = "FileBrowser.navigateUp();";
	%ctrl.altCommand = "FileBrowser.navigateUp();";
	%ctrl.iconBitmap = "tlab/gui/icons/24-assets/folder_up.png";
	%ctrl.text = "..";
	%ctrl.tooltip = "Go to parent folder";
	%ctrl.buttonMargin = "8 1";
	%ctrl.sizeIconToButton = true;
	%ctrl.makeIconSquare = true;
	//%ctrl.class = "CreatorFolderIconBtn";
	%ctrl.buttonType = "PushButton";
	FileBrowserArray.addGuiControl( %ctrl );
	FileBrowserArray.bringToFront(%ctrl);
}
//------------------------------------------------------------------------------

//==============================================================================
function FileBrowser::addFolderIcon( %this, %text ) {
	%ctrl = %this.createIcon();
	%ctrl.command = "FileBrowser.iconFolderAlt($ThisControl);";
	%ctrl.altCommand = "FileBrowser.iconFolderAlt($ThisControl);";
	%ctrl.iconBitmap = "tlab/gui/icons/24-assets/folder_open.png";
	%ctrl.text = %text;
	%ctrl.tooltip = %text;
	%ctrl.class = "CreatorFolderIconBtn";
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%ctrl.buttonMargin = "6 0";
	%ctrl.sizeIconToButton = true;
	%ctrl.makeIconSquare = true;
	FileBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::findIconCtrl( %this, %name ) {
	for ( %i = 0; %i < FileBrowserArray.getCount(); %i++ ) {
		%ctrl = FileBrowserArray.getObject( %i );

		if ( %ctrl.text $= %name )
			return %ctrl;
	}

	return -1;
}
//------------------------------------------------------------------------------