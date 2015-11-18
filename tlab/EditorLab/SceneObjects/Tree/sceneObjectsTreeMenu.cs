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
		%popup.addItem(%id++,"Group" TAB "" TAB "Scene.addSimGroup( true );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Add New Objects Here" TAB "" TAB "Scene.setNewObjectGroup( %tree.object );");
		%popup.addItem(%id++,"Add Children to Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( "@%obj@" , false );");
		%popup.addItem(%id++,"Remove Children from Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( "@%obj@" , true );");
		%popup.addItem(%id++,"-");
		%popup.addItem(%id++,"Lock auto-arrange" TAB "" TAB "SceneEd.toggleAutoArrangeGroupLock("@%obj@");");
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
		%popup.addItem(%id++,"Group" TAB "" TAB "Scene.addSimGroup( true );");

		if( %obj.isMemberOfClass( "Prefab" ) ) {
			%popup.addItem(%id++, "Expand Prefab to group" TAB "" TAB "devlog(\"Expanding\");Lab.ExpandPrefab( "@%obj.getId()@");");
		}

		%haveObjectEntries = true;
	}
	
	  // Specialized version for ConvexShapes. 
      if( %obj.isMemberOfClass( "ConvexShape" ) )
      {
      	%popup.addItem(%id++,"-");
			%popup.addItem(%id++,"Convert to Zone" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"Zone\" );");
		%popup.addItem(%id++,"Convert to Portal" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"Portal\" );");
		%popup.addItem(%id++,"Convert to Occluder" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"OcclusionVolume\" );");
		%popup.addItem(%id++,"Convert to Sound Space" TAB "" TAB "EWorldEditor.convertSelectionToPolyhedralObjects( \"SFXSpace\" );");
	
      }
      
      // Specialized version for polyhedral objects.
      else if( %obj.isMemberOfClass( "Zone" ) || %obj.isMemberOfClass( "Portal" ) || %obj.isMemberOfClass( "OcclusionVolume" ) || %obj.isMemberOfClass( "SFXSpace" ) ){
      	%popup.addItem(%id++,"-");
			%popup.addItem(%id++,"Convert to ConvexShape" TAB "" TAB "EWorldEditor.convertSelectionToConvexShape();");        
      }


// Handle multi-selection.
	if( %tree.getSelectedItemsCount() > 1 ) {
		%popup.addItem(%id++,"Delete Selection" TAB "" TAB "EditorMenuEditDelete();");
		%popup.addItem(%id++,"Group Selection" TAB "" TAB "Scene.addSimGroup( true );");
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

/*


function EditorTree::onRightMouseUp( %this, %itemId, %mouse, %obj )
{
   %haveObjectEntries = false;
   %haveLockAndHideEntries = true;
   
   // Handle multi-selection.
   if( %this.getSelectedItemsCount() > 1 )
   {
      %popup = ETMultiSelectionContextPopup;
      if( !isObject( %popup ) )
         %popup = new PopupMenu( ETMultiSelectionContextPopup )
         {
            superClass = "MenuBuilder";
            isPopup = "1";

            item[ 0 ] = "Delete" TAB "" TAB "EditorMenuEditDelete();";
            item[ 1 ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";
         };
   }

   // Open context menu if this is a CameraBookmark
   else if( %obj.isMemberOfClass( "CameraBookmark" ) )
   {
      %popup = ETCameraBookmarkContextPopup;
      if( !isObject( %popup ) )
         %popup = new PopupMenu( ETCameraBookmarkContextPopup )
         {
            superClass = "MenuBuilder";
            isPopup = "1";

            item[ 0 ] = "Go To Bookmark" TAB "" TAB "EditorGui.jumpToBookmark( %this.bookmark.getInternalName() );";

            bookmark = -1;
         };

      ETCameraBookmarkContextPopup.bookmark = %obj;
   }
   
   // Open context menu if this is set CameraBookmarks group.
   else if( %obj.name $= "CameraBookmarks" )
   {
      %popup = ETCameraBookmarksGroupContextPopup;
      if( !isObject( %popup ) )
         %popup = new PopupMenu( ETCameraBookmarksGroupContextPopup )
         {
            superClass = "MenuBuilder";
            isPopup = "1";

            item[ 0 ] = "Add Camera Bookmark" TAB "" TAB "EditorGui.addCameraBookmarkByGui();";
         };
   }

   // Open context menu if this is a SimGroup
   else if( %obj.isMemberOfClass( "SimGroup" ) )
   {
      %popup = ETSimGroupContextPopup;
      if( !isObject( %popup ) )
         %popup = new PopupMenu( ETSimGroupContextPopup )
         {
            superClass = "MenuBuilder";
            isPopup = "1";

            item[ 0 ] = "Rename" TAB "" TAB "EditorTree.showItemRenameCtrl( EditorTree.findItemByObjectId( %this.object ) );";
            item[ 1 ] = "Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %this.object );";
            item[ 2 ] = "Inspect" TAB "" TAB "inspectObject( %this.object );";
            item[ 3 ] = "-";
            item[ 4 ] = "Toggle Lock Children" TAB "" TAB "EWorldEditor.toggleLockChildren( %this.object );";
            item[ 5 ] = "Toggle Hide Children" TAB "" TAB "EWorldEditor.toggleHideChildren( %this.object );";
            item[ 6 ] = "-";
            item[ 7 ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";
            item[ 8 ] = "-";
            item[ 9 ] = "Add New Objects Here" TAB "" TAB "EWCreatorWindow.setNewObjectGroup( %this.object );";
            item[ 10 ] = "Add Children to Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %this.object, false );";
            item[ 11 ] = "Remove Children from Selection" TAB "" TAB "EWorldEditor.selectAllObjectsInSet( %this.object, true );";

            object = -1;
         };

      %popup.object = %obj;
      
      %hasChildren = %obj.getCount() > 0;
      %popup.enableItem( 10, %hasChildren );
      %popup.enableItem( 11, %hasChildren );
      
      %haveObjectEntries = true;
      %haveLockAndHideEntries = false;
   }
   
   // Open generic context menu.
   else
   {
      %popup = ETContextPopup;      
      if( !isObject( %popup ) )
         %popup = new PopupMenu( ETContextPopup )
         {
            superClass = "MenuBuilder";
            isPopup = "1";

            item[ 0 ] = "Rename" TAB "" TAB "EditorTree.showItemRenameCtrl( EditorTree.findItemByObjectId( %this.object ) );";
            item[ 1 ] = "Delete" TAB "" TAB "EWorldEditor.deleteMissionObject( %this.object );";
            item[ 2 ] = "Inspect" TAB "" TAB "inspectObject( %this.object );";
            item[ 3 ] = "-";
            item[ 4 ] = "Locked" TAB "" TAB "%this.object.setLocked( !%this.object.locked ); EWorldEditor.syncGui();";
            item[ 5 ] = "Hidden" TAB "" TAB "EWorldEditor.hideObject( %this.object, !%this.object.hidden ); EWorldEditor.syncGui();";
            item[ 6 ] = "-";
            item[ 7 ] = "Group" TAB "" TAB "EWorldEditor.addSimGroup( true );";

            object = -1;
         };
     
      // Specialized version for ConvexShapes. 
      if( %obj.isMemberOfClass( "ConvexShape" ) )
      {
         %popup = ETConvexShapeContextPopup;      
         if( !isObject( %popup ) )
            %popup = new PopupMenu( ETConvexShapeContextPopup : ETContextPopup )
            {
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
               %obj.isMemberOfClass( "SFXSpace" ) )
      {
         %popup = ETPolyObjectContextPopup;      
         if( !isObject( %popup ) )
            %popup = new PopupMenu( ETPolyObjectContextPopup : ETContextPopup )
            {
               superClass = "MenuBuilder";
               isPopup = "1";

               item[ 8 ] = "-";
               item[ 9 ] = "Convert to ConvexShape" TAB "" TAB "EWorldEditor.convertSelectionToConvexShape();";
            };
      }

      %popup.object = %obj;
      %haveObjectEntries = true;
   }

   if( %haveObjectEntries )
   {         
      %popup.enableItem( 0, %obj.isNameChangeAllowed() && %obj.getName() !$= "MissionGroup" );
      %popup.enableItem( 1, %obj.getName() !$= "MissionGroup" );
      if( %haveLockAndHideEntries )
      {
         %popup.checkItem( 4, %obj.locked );
         %popup.checkItem( 5, %obj.hidden );
      }
      %popup.enableItem( 7, %this.isItemSelected( %itemId ) );
   }
   
   %popup.showPopup( Canvas );
}
*/