//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================





function Scene::showSceneTreeItemContext( %this,%tree, %itemId, %mouse, %obj ) {
	devLog("Scene::showSceneTreeItemContext( %this,%tree, %itemId, %mouse, %obj )",%this,%tree, %itemId, %mouse, %obj);
	%haveObjectEntries = false;
	%haveLockAndHideEntries = true;
	delObj(SceneTreePopup);
	%popup = new PopupMenu( SceneTreePopup ) {
		superClass = "MenuBuilder";
		isPopup = "1";
	};
	%popup.object = %obj;
	%id = -1;

	
	// Open context menu if this is a CameraBookmark
	if( %obj.isMemberOfClass( "CameraBookmark" ) ) {
		%popup.addItem(%id++,"Go To Bookmark" TAB "" TAB "EManageBookmarks.jumpToBookmark( %tree.bookmark.getInternalName() );");
		SceneTreePopup.bookmark = %obj;
	}
	// Open context menu if this is set CameraBookmarks group.
	else if( %obj.name $= "CameraBookmarks" ) {
		%popup.addItem(%id++,"Add Camera Bookmark" TAB "" TAB "EManageBookmarks.addCameraBookmarkByGui();");
	}
	// Open context menu if this is a SimGroup
	else if( %obj.isMemberOfClass( "SimGroup" ) ) {
		devlog("Creating SimGroup context menu");
		%popup.addItem(%id++,"Rename" TAB "" TAB "SceneEditorTree.showItemRenameCtrl( SceneEditorTree.findItemByObjectId( %tree.object ) );");
		%popup.addItem(%id++,"Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %tree.object );");
		%popup.addItem(%id++,"Inspect" TAB "" TAB "inspectObject( %tree.object );");
		%popup.addItem(%id++,"-");
		//%popup.addItem(%id++,"Toggle Lock Children" TAB "" TAB "EWorldEditor.toggleLockChildren( %tree.object );");
		//%popup.addItem(%id++,"Toggle Hide Children" TAB "" TAB "EWorldEditor.toggleHideChildren( %tree.object );");
		//%popup.addItem(%id++,"-");
		
		%popup.addItem(%id++,"Show Childrens" TAB "" TAB "Scene.showGroupChilds( "@%obj@" );");
		%popup.addItem(%id++,"Hide Childrens" TAB "" TAB "Scene.hideGroupChilds( "@%obj@" );");
		%popup.addItem(%id++,"Toggle Children Visibility" TAB "" TAB "Scene.toggleHideGroupChilds( "@%obj@" );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Group" TAB "" TAB "EWorldEditor.addSimGroup( true );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Add New Objects Here" TAB "" TAB "Scene.setNewObjectGroup( %tree.object );");
		%popup.addItem(%id++,"Add Children to Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %tree.object, false );");
		%popup.addItem(%id++,"Remove Children from Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %tree.object, true );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Lock auto-arrange" TAB "" TAB "SceneEd.toggleAutoArrangeGroupLock( %tree.object);");
		%popup.autoLockId = %id;

		if (%obj.prefabFile !$= "") {
			%popup.addItem(%id++,"Collapse Prefab group" TAB "" TAB "Lab.CollapsePrefab( "@%obj.getId()@");");
		}

		if (%obj.autoArrangeLocked)
			%popup.setItem(%popup.autoLockId,"Unlock auto-arrange","","SceneEd.toggleAutoArrangeGroupLock( %tree.object);");
		else
			%popup.setItem(%popup.autoLockId,"Lock auto-arrange","","SceneEd.toggleAutoArrangeGroupLock( %tree.object);");

		%hasChildren = %obj.getCount() > 0;
		%popup.enableItem( 10, %hasChildren );
		%popup.enableItem( 11, %hasChildren );
		%haveObjectEntries = true;
		%haveLockAndHideEntries = false;
	}
	// Open generic context menu.
	else {
		%popup.addItem(%id++,"Rename" TAB "" TAB "SceneEditorTree.showItemRenameCtrl( SceneEditorTree.findItemByObjectId( %tree.object ) );");
		%popup.addItem(%id++,"Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %tree.object );");
		%popup.addItem(%id++,"Inspect" TAB "" TAB "inspectObject( %tree.object );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Locked" TAB "" TAB "%this.object.setLocked( !%tree.object.locked ); EWorldEditor.syncGui();");
		%popup.addItem(%id++,"Hidden" TAB "" TAB "EWorldEditor.hideObject( %tree.object, !%tree.object.hidden ); EWorldEditor.syncGui();");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Group" TAB "" TAB "EWorldEditor.addSimGroup( true );");

		if( %obj.isMemberOfClass( "Prefab" ) ) {
			%popup.addItem(%id++, "Expand Prefab to group" TAB "" TAB "devlog(\"Expanding\");Lab.ExpandPrefab( "@%obj.getId()@");");
		}

		%haveObjectEntries = true;
	}

// Handle multi-selection.
	if( %tree.getSelectedItemsCount() > 1 ) {
		%popup.addItem(%id++,"Delete Selection" TAB "" TAB "EditorMenuEditDelete();");
		%popup.addItem(%id++,"Group Selection" TAB "" TAB "EWorldEditor.addSimGroup( true );");
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

