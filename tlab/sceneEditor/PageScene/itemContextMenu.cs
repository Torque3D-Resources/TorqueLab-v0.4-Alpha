//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================
function SceneEd::showSceneTreeItemContext( %this,%tree, %itemId, %mouse, %obj ) {
	%haveObjectEntries = false;
	%haveLockAndHideEntries = true;


	// Handle multi-selection.
	if( %tree.getSelectedItemsCount() > 1 ) {
		%popup = ETMultiSelectionContextPopup;

		if( !isObject( %popup ) )
			%popup = new PopupMenu( ETMultiSelectionContextPopup ) {
			superClass = "MenuBuilder";
			isPopup = "1";
			item[ 0 ] = "Delete" TAB "" TAB "EditorMenuEditDelete();";
			item[ 1 ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";
		};
	}
	// Open context menu if this is a CameraBookmark
	else if( %obj.isMemberOfClass( "CameraBookmark" ) ) {
		%popup = ETCameraBookmarkContextPopup;

		if( !isObject( %popup ) )
			%popup = new PopupMenu( ETCameraBookmarkContextPopup ) {
			superClass = "MenuBuilder";
			isPopup = "1";
			item[ 0 ] = "Go To Bookmark" TAB "" TAB "EManageBookmarks.jumpToBookmark( %tree.bookmark.getInternalName() );";
			bookmark = -1;
		};

		ETCameraBookmarkContextPopup.bookmark = %obj;
	}
	// Open context menu if this is set CameraBookmarks group.
	else if( %obj.name $= "CameraBookmarks" ) {
		%popup = ETCameraBookmarksGroupContextPopup;

		if( !isObject( %popup ) )
			%popup = new PopupMenu( ETCameraBookmarksGroupContextPopup ) {
			superClass = "MenuBuilder";
			isPopup = "1";
			item[ 0 ] = "Add Camera Bookmark" TAB "" TAB "EManageBookmarks.addCameraBookmarkByGui();";
		};
	}
	// Open context menu if this is a SimGroup
	else if( %obj.isMemberOfClass( "SimGroup" ) ) {
		%popup = ETSimGroupContextPopup;

		if( !isObject( %popup ) )
			%popup = new PopupMenu( ETSimGroupContextPopup ) {
			superClass = "MenuBuilder";
			isPopup = "1";
			
			item[ %id = 0 ] = "Rename" TAB "" TAB "SceneEditorTree.showItemRenameCtrl( SceneEditorTree.findItemByObjectId( %tree.object ) );";
			item[%id++] = "Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %tree.object );";
			item[ %id++ ] = "Inspect" TAB "" TAB "inspectObject( %tree.object );";
			item[ %id++ ] = "-";
			item[ %id++ ] = "Toggle Lock Children" TAB "" TAB "EWorldEditor.toggleLockChildren( %tree.object );";
			item[ %id++ ] = "Toggle Hide Children" TAB "" TAB "EWorldEditor.toggleHideChildren( %tree.object );";
			item[ %id++ ] = "-";
			item[ %id++ ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";
			item[ %id++ ] = "-";
			item[ %id++ ] = "Add New Objects Here" TAB "" TAB "SEP_Creator.setNewObjectGroup( %tree.object );";
			item[ %id++ ] = "Add Children to Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %tree.object, false );";			
			item[ %id++ ] = "Remove Children from Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %tree.object, true );";		
			
			item[ %id++ ] = "-";
			item[ %id++ ] = "Lock auto-arrange" TAB "" TAB "SceneEd.toggleAutoArrangeGroupLock( %tree.object);";
			autoLockId = %id;
			object = -1;
			
		};
	
		if (%obj.autoArrangeLocked)
			%popup.setItem(%popup.autoLockId,"Unlock auto-arrange","","SceneEd.toggleAutoArrangeGroupLock( %tree.object);");
		else
			%popup.setItem(%popup.autoLockId,"Lock auto-arrange","","SceneEd.toggleAutoArrangeGroupLock( %tree.object);");
		%popup.object = %obj;

		%hasChildren = %obj.getCount() > 0;

		%popup.enableItem( 10, %hasChildren );

		%popup.enableItem( 11, %hasChildren );

		%haveObjectEntries = true;

		%haveLockAndHideEntries = false;
	}
	// Open generic context menu.
	else {
		%popup = ETContextPopup;

		if( !isObject( %popup ) )
			%popup = new PopupMenu( ETContextPopup ) {
			superClass = "MenuBuilder";
			isPopup = "1";
			item[ 0 ] = "Rename" TAB "" TAB "SceneEditorTree.showItemRenameCtrl( SceneEditorTree.findItemByObjectId( %tree.object ) );";
			item[ 1 ] = "Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %tree.object );";
			item[ 2 ] = "Inspect" TAB "" TAB "inspectObject( %tree.object );";
			item[ 3 ] = "-";
			item[ 4 ] = "Locked" TAB "" TAB "%this.object.setLocked( !%tree.object.locked ); EWorldEditor.syncGui();";
			item[ 5 ] = "Hidden" TAB "" TAB "EWorldEditor.hideObject( %tree.object, !%tree.object.hidden ); EWorldEditor.syncGui();";
			item[ 6 ] = "-";
			item[ 7 ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";
			object = -1;
		};

		// Specialized version for ConvexShapes.
		if( %obj.isMemberOfClass( "ConvexShape" ) ) {
			%popup = ETConvexShapeContextPopup;

			if( !isObject( %popup ) )
				%popup = new PopupMenu( ETConvexShapeContextPopup : ETContextPopup ) {
				superClass = "MenuBuilder";
				isPopup = "1";
				item[ 8 ] = "-";
				item[ 9 ] = "Convert to Zone" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"Zone\" );";
				item[ 10 ] = "Convert to Portal" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"Portal\" );";
				item[ 11 ] = "Convert to Occluder" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"OcclusionVolume\" );";
				item[ 12 ] = "Convert to Sound Space" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"SFXSpace\" );";
			};
		}
		// Specialized version for polyhedral objects.
		else if( %obj.isMemberOfClass( "Zone" ) ||
					%obj.isMemberOfClass( "Portal" ) ||
					%obj.isMemberOfClass( "OcclusionVolume" ) ||
					%obj.isMemberOfClass( "SFXSpace" ) ) {
			%popup = ETPolyObjectContextPopup;

			if( !isObject( %popup ) )
				%popup = new PopupMenu( ETPolyObjectContextPopup : ETContextPopup ) {
				superClass = "MenuBuilder";
				isPopup = "1";
				item[ 8 ] = "-";
				item[ 9 ] = "Convert to ConvexShape" TAB "" TAB "EWorldEditor.convertSelectionToConvexShape();";
			};
		}

		%popup.object = %obj;
		%haveObjectEntries = true;
	}

	if( %haveObjectEntries ) {
		%popup.enableItem( 0, %obj.isNameChangeAllowed() && %obj.getName() !$= "MissionGroup" );
		%popup.enableItem( 1, %obj.getName() !$= "MissionGroup" );

		if( %haveLockAndHideEntries ) {
			%popup.checkItem( 4, %obj.locked );
			%popup.checkItem( 5, %obj.hidden );
		}

		%popup.enableItem( 7, %tree.isItemSelected( %itemId ) );
	}

	%popup.showPopup( Canvas );
}

function SceneEditorTree::positionContextMenu( %this, %menu ) {
	if( (getWord(%menu.position, 0) + getWord(%menu.extent, 0)) > getWord(EWorldEditor.extent, 0) ) {
		%posx = getWord(%menu.position, 0);
		%offset = getWord(EWorldEditor.extent, 0) - (%posx + getWord(%menu.extent, 0)) - 5;
		%posx += %offset;
		%menu.position = %posx @ " " @ getWord(%menu.position, 1);
	}
}