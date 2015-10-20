//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SEP_Creator::init( %this ) {
	// Just so we can recall this method for testing changes
	// without restarting.
	if ( isObject( %this.array ) )
		%this.array.delete();

	%this.array = new ArrayObject();
	%this.array.caseSensitive = true;
	%this.setListView( true );
	
	%this.registerObjects();
	SEP_Creator.contentCtrl = CreatorIconArray;
}
//==============================================================================
function SEP_Creator::onWake( %this ) {
	if ($SEP_CreatorTypeActive $= "")
		$SEP_CreatorTypeActive = "0";

	CreatorTypeStack.getObject(0).performClick();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function CreatorTabBook::onTabSelected( %this, %text, %idx ) {
	if ( %this.isAwake() ) {
		%lastTabAdress = 	SEP_Creator.lastCreatorAdress[%text];
		//SEP_Creator.tab = %text;
		//SEP_Creator.navigate( %lastTabAdress );
		SEP_Creator.tab = %text;
		SEP_Creator.navigate( %lastTabAdress );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function SEP_CreatorButton::onClick( %this ) {
	%type = %this.text;

	if (%type $= "Options"){
		show(CreatorOptionsArray);
		hide(CreatorIconArray);
		return;
	}
	hide(CreatorOptionsArray);
	show(CreatorIconArray);
	if ( SEP_Creator.isAwake() ) {
		%lastTabAdress = 	SEP_Creator.lastCreatorAdress[%type];
		SEP_Creator.tab = %type;
		SEP_Creator.navigate( %lastTabAdress );
	}

	$SEP_CreatorTypeActive = %this.internalName;
}
//------------------------------------------------------------------------------
//==============================================================================
function CreatorPopupMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	SEP_Creator.navigate( %split );
}
//------------------------------------------------------------------------------




//==============================================================================
// Creator Objects Listing
//==============================================================================
//==============================================================================
function SEP_Creator::setListView( %this, %noupdate ) {
	//CreatorIconArray.clear();
	//CreatorIconArray.setVisible( false );
	CreatorIconArray.setVisible( true );
	SEP_Creator.contentCtrl = CreatorIconArray;
	%this.isList = true;

	if ( %noupdate == true )
		%this.navigate( %this.address );
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.setDisplayMode("Detail");
function SEP_Creator::setDisplayMode( %this,%mode ) {
	%settingObj = $SceneEd_CreatorArray[%mode];
	%dynCount = %settingObj.getDynamicFieldCount();

	for(%i = 0; %i< %dynCount; %i++) {
		%fieldFull = %settingObj.getDynamicField(%i);
		%field = getField(%fieldFull,0);
		%value = getField(%fieldFull,1);
		CreatorIconArray.setFieldValue(%field,%value);
	}

	CreatorIconArray.refresh();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.setDisplayMode("Detail");
function SEP_Creator::setIconWidth( %this,%ctrl ) {
	%width = %ctrl.getValue();
	if (%width < 10 || %width > 300)
		return;
		devLog("Width = ",%width);
	CreatorIconArray.setFieldValue("colSize",%width);
	CreatorIconArray.refresh();
	%ctrl.updateFriends();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.updatePrefabInMeshes("Detail");
function SEP_Creator::updatePrefabInMeshes( %this ) {
	if (%this.tab $= "Meshes")
		%this.navigate(%this.lastCreatorAdress[%this.tab]);
}
//------------------------------------------------------------------------------

//==============================================================================
// Creator Objects Navigation
//==============================================================================

//==============================================================================
function SEP_Creator::navigateDown( %this, %folder ) {
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
function SEP_Creator::navigateUp( %this ) {
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
function SEP_Creator::navigate( %this, %address ) {
	CreatorIconArray.frozen = true;
	CreatorIconArray.clear();
	CreatorPopupMenu.clear();
	%this.lastCreatorAdress[%this.tab] = %address;
	
	if (!%this.isMethod("navigate"@%this.tab)||%this.tab $= ""){
		warnLog("Couldn't find a scene creator navigating function for type:",%this.tab);
		return;
	}
	eval("%this.navigate"@%this.tab@"(%address);");
/*
	if ( %this.tab $= "Scripted" ) {
		%category = getWord( %address, 1 );
		%dataGroup = "DataBlockGroup";

		for ( %i = 0; %i < %dataGroup.getCount(); %i++ ) {
			%obj = %dataGroup.getObject(%i);
			// echo ("Obj: " @ %obj.getName() @ " - " @ %obj.category );

			if ( %obj.category $= "" && %obj.category == 0 )
				continue;

			// Add category to popup menu if not there already
			if ( CreatorPopupMenu.findText( %obj.category ) == -1 )
				CreatorPopupMenu.add( %obj.category );

			if ( %address $= "" ) {
				%ctrl = %this.findIconCtrl( %obj.category );

				if ( %ctrl == -1 ) {
					%this.addFolderIcon( %obj.category );
				}
			} else if ( %address $= %obj.category ) {
				%ctrl = %this.findIconCtrl( %obj.getName() );

				if ( %ctrl == -1 )
					%this.addShapeIcon( %obj );
			}
		}
	}

	if ( %this.tab $= "Meshes" ) {
		//if (%address $= ""){
		//	%address = SceneEditorTools.meshRootFolder;
		//	devLog("Mesh navigation set to default root",%address);
		//}
		%fullPath = findFirstFileMultiExpr( "*.dts" TAB "*.dae" TAB "*.kmz" TAB "*.dif" );

		if (SceneEditorTools.meshRootFolder !$= "")
			devLog("Mesh root setted to:",SceneEditorTools.meshRootFolder);

		while ( %fullPath !$= "" ) {
			if (SceneEditorTools.meshRootFolder !$= "") {
				%found = strFind(%fullPath,SceneEditorTools.meshRootFolder);
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

			%dirCount = getWordCount( %splitPath ) - 1;
			%pathFolders = getWords( %splitPath, 0, %dirCount - 1 );
			// Add this file's path (parent folders) to the
			// popup menu if it isn't there yet.
			%temp = strreplace( %pathFolders, " ", "/" );
			%r = CreatorPopupMenu.findText( %temp );

			if ( %r == -1 ) {
				CreatorPopupMenu.add( %temp );
			}

			// Is this file in the current folder?
			if ( stricmp( %pathFolders, %address ) == 0 ) {
				%this.addStaticIcon( %fullPath );
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

	if ( %this.tab $= "Level" ) {
		// Add groups to popup menu
		%array = %this.array;
		%array.sortk();
		%count = %array.count();

		if ( %count > 0 ) {
			%lastGroup = "";

			for ( %i = 0; %i < %count; %i++ ) {
				%group = %array.getKey( %i );

				if ( %group !$= %lastGroup ) {
					CreatorPopupMenu.add( %group );

					if ( %address $= "" )
						%this.addFolderIcon( %group );
				}

				if ( %address $= %group ) {
					%args = %array.getValue( %i );
					%class = %args.val[0];
					%name = %args.val[1];
					%func = %args.val[2];
					%this.addMissionObjectIcon( %class, %name, %func );
				}

				%lastGroup = %group;
			}
		}
	}

	if ( %this.tab $= "Prefabs" ) {
		%expr = "*.prefab";
		%fullPath = findFirstFile( %expr );

		while ( %fullPath !$= "" ) {
			%fullPath = makeRelativePath( %fullPath, getMainDotCSDir() );
			%splitPath = strreplace( %fullPath, "/", " " );

			if( getWord(%splitPath, 0) $= "tools" ) {
				%fullPath = findNextFile( %expr );
				continue;
			}

			%dirCount = getWordCount( %splitPath ) - 1;
			%pathFolders = getWords( %splitPath, 0, %dirCount - 1 );
			// Add this file's path (parent folders) to the
			// popup menu if it isn't there yet.
			%temp = strreplace( %pathFolders, " ", "/" );
			%r = CreatorPopupMenu.findText( %temp );

			if ( %r == -1 ) {
				CreatorPopupMenu.add( %temp );
			}

			// Is this file in the current folder?
			if ( stricmp( %pathFolders, %address ) == 0 ) {
				%this.addPrefabIcon( %fullPath );
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

			%fullPath = findNextFile( %expr );
		}
	}
*/
	CreatorIconArray.sort( "alphaIconCompare" );

	for ( %i = 0; %i < CreatorIconArray.getCount(); %i++ ) {
		CreatorIconArray.getObject(%i).autoSize = false;
	}

	CreatorIconArray.frozen = false;
	CreatorIconArray.refresh();
	// Recalculate the array for the parent guiScrollCtrl
	CreatorIconArray.getParent().computeSizes();
	%this.address = %address;
	CreatorPopupMenu.sort();
	%str = strreplace( %address, " ", "/" );
	%r = CreatorPopupMenu.findText( %str );

	if ( %r != -1 )
		CreatorPopupMenu.setSelected( %r, false );
	else
		CreatorPopupMenu.setText( %str );

	CreatorPopupMenu.tooltip = %str;
}

//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected 
/*
function SEP_Creator::initArrayCfg( %this ) {
	$SceneEd_CreatorArray["List"] = newScriptObject(SceneEd_CreatorArrayList);
	SceneEd_CreatorArrayList.colSize = "100";
	SceneEd_CreatorArrayList.rowSize = "22";
	SceneEd_CreatorArrayList.autoCellSize = false;
	$SceneEd_CreatorArray["Detail"] = newScriptObject(SceneEd_CreatorArrayDetail);
	SceneEd_CreatorArrayDetail.colSize = "130";
	SceneEd_CreatorArrayDetail.rowSize = "18";
	SceneEd_CreatorArrayDetail.autoCellSize = true;
}*/
//------------------------------------------------------------------------------
