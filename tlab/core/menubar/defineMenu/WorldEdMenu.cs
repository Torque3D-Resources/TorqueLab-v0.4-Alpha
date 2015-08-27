//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



//==============================================================================
function WorldEdMenu::initData(%this) {
	//set up $LabCmd variable so that it matches OS standards

	

	
	%id = -1;
	%itemId = -1;
	$LabMenuWorld_[%id++] = "File";
	$LabMenuItemWorld_[%id,%itemId++] = "Save Level" TAB "Ctrl S" TAB "Lab.SaveCurrentMission();";
	$LabMenuItemWorld_[%id,%itemId++] = "Save Level As..." TAB "" TAB "Lab.SaveCurrentMission(true);";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] =  "Open Project in Torsion" TAB "" TAB "EditorOpenTorsionProject();";
	$LabMenuItemWorld_[%id,%itemId++] =  "Open Level File in Torsion" TAB "" TAB "EditorOpenFileInTorsion();";
	$LabMenuItemWorld_[%id,%itemId++] =  "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Create Blank Terrain" TAB "" TAB "Canvas.pushDialog( CreateNewTerrainGui );";
	$LabMenuItemWorld_[%id,%itemId++] = "Import Terrain Heightmap" TAB "" TAB "Canvas.pushDialog( TerrainImportGui );";
	$LabMenuItemWorld_[%id,%itemId++] = "Export Terrain Heightmap" TAB "" TAB "Canvas.pushDialog( TerrainExportGui );";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Export To COLLADA..." TAB "" TAB "EditorExportToCollada();";
	$LabMenuItemWorld_[%id,%itemId++] =  "-";
	$LabMenuItemWorld_[%id,%itemId++] =  "Add FMOD Designer Audio..." TAB "" TAB "AddFMODProjectDlg.show();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Play Level" TAB "F11" TAB "Editor.close($HudCtrl);";
	$LabMenuItemWorld_[%id,%itemId++] = "Exit Level" TAB "" TAB "EditorExitMission();";
	$LabMenuItemWorld_[%id,%itemId++] = "Quit" TAB $LabQuit TAB "EditorQuitGame();";
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Edit";
	$LabMenuItemWorld_[%id,%itemId++] = "Undo" TAB $LabCmd SPC "Z" TAB "Editor.getUndoManager().undo();";
	$LabMenuItemWorld_[%id,%itemId++] = "Redo" TAB $LabRedo TAB "Editor.getUndoManager().redo();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Cut" TAB $LabCmd SPC "X" TAB "EditorMenuEditCut();";
	$LabMenuItemWorld_[%id,%itemId++] = "Copy" TAB $LabCmd SPC "C" TAB "EditorMenuEditCopy();";
	$LabMenuItemWorld_[%id,%itemId++] = "Paste" TAB $LabCmd SPC "V" TAB "EditorMenuEditPaste();";
	$LabMenuItemWorld_[%id,%itemId++] = "Delete" TAB "Delete" TAB "EditorMenuEditDelete();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Deselect" TAB "X" TAB "EditorMenuEditDeselect();";
	$LabMenuItemWorld_[%id,%itemId++] = "Select..." TAB "" TAB "ESelectObjects.toggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Audio Parameters..." TAB "" TAB "EManageSFXParameters.ToggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "LabEditor Settings..." TAB "" TAB "toggleDlg(LabSettingsDlg);";
	$LabMenuItemWorld_[%id,%itemId++] = "Snap Options..." TAB "" TAB "ESnapOptions.ToggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Game Options..." TAB "" TAB "Canvas.pushDialog(DlgOptions);";
	$LabMenuItemWorld_[%id,%itemId++] = "PostEffect Manager" TAB "" TAB "Canvas.pushDialog(PostFXManager);";
	$LabMenuItemWorld_[%id,%itemId++] = "Lab PostFX Manager" TAB "" TAB "toggleVisible(EPostFxManager);";
	$LabMenuItemWorld_[%id,%itemId++] = "Copy Tool" TAB "" TAB "toggleDlg(ToolObjectCopyDlg);";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle transform box" TAB "" TAB "ETransformBox.toggleBox();";
	
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Object";
	$LabMenuItemWorld_[%id,%itemId++] = "Lock Selection" TAB $LabCmd @ " L" TAB "EWorldEditor.lockSelection(true); EWorldEditor.syncGui();";
	$LabMenuItemWorld_[%id,%itemId++] = "Unlock Selection" TAB $LabCmd @ "-Shift L" TAB "EWorldEditor.lockSelection(false); EWorldEditor.syncGui();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Hide Selection" TAB $LabCmd @ " H" TAB "EWorldEditor.hideSelection(true); EWorldEditor.syncGui();";
	$LabMenuItemWorld_[%id,%itemId++] = "Show Selection" TAB $LabCmd @ "-Shift H" TAB "EWorldEditor.hideSelection(false); EWorldEditor.syncGui();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Group Selection" TAB $LabCmd @ " G" TAB "Lab.groupSelectedObjects();";
	$LabMenuItemWorld_[%id,%itemId++] = "Group Selection" TAB $LabCmd @ "-Shift G" TAB "Lab.ungroupSelectedObjects();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Align Bounds";
	$LabMenuItemWorld_[%id,%itemId++] = "Align Center";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Reset Transforms" TAB "Ctrl R" TAB "EWorldEditor.resetTransforms();";
	$LabMenuItemWorld_[%id,%itemId++] = "Reset Selected Rotation" TAB "" TAB "EWorldEditor.resetSelectedRotation();";
	$LabMenuItemWorld_[%id,%itemId++] = "Reset Selected Scale" TAB "" TAB "EWorldEditor.resetSelectedScale();";
	$LabMenuItemWorld_[%id,%itemId++] = "Transform Selection..." TAB "Ctrl T" TAB "ETransformSelection.ToggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	//item[13] = "Drop Camera to Selection" TAB "Ctrl Q" TAB "EWorldEditor.dropCameraToSelection();";
	//item[14] = "Add Selection to Instant Group" TAB "" TAB "EWorldEditor.addSelectionToAddGroup();";
	$LabMenuItemWorld_[%id,%itemId++] = "Drop Selection" TAB "Ctrl D" TAB "EWorldEditor.dropSelection();";
	//item[15] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Drop Location";
	%subId=-1;
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "at Origin" TAB "" TAB "EWorldEditor.droptype = \"atOrigin\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "at Camera" TAB "" TAB "EWorldEditor.droptype = \"atCamera";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "at Camera w/Rotation" TAB "" TAB "EWorldEditor.droptype = \"atCameraRot\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Below Camera" TAB "" TAB "EWorldEditor.droptype = \"belowCamera\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Screen Center" TAB "" TAB "EWorldEditor.droptype = \"screenCenter\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "at Centroid" TAB "" TAB "EWorldEditor.droptype = \"atCentroid\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "to Terrain" TAB "" TAB "EWorldEditor.droptype = \"toTerrain\"";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Below Selection" TAB "" TAB "EWorldEditor.droptype = \"belowSelection\"";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Make Selection Prefab" TAB "" TAB "Lab.CreatePrefab();";
	$LabMenuItemWorld_[%id,%itemId++] = "Explode Selected Prefab" TAB "" TAB "Lab.ExplodePrefab();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Mount Selection A to B" TAB "" TAB "EditorMount();";
	$LabMenuItemWorld_[%id,%itemId++] = "Unmount Selected Object" TAB "" TAB "EditorUnmount();";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Objects Manager" TAB  $LabCmd @ " M" TAB "ESelectObjects.toggleVisibility();";
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Utility";
	$LabMenuItemWorld_[%id,%itemId++] = "Full Relight" TAB "Alt L" TAB "Editor.lightScene(\"\", forceAlways);";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle ShadowViz" TAB "" TAB "toggleShadowViz();";
	$LabMenuItemWorld_[%id,%itemId++] = "-------------";
	$LabMenuItemWorld_[%id,%itemId++] = "Open disabled plugins bin" TAB "" TAB "Lab.openDisabledPluginsBin();";
	$LabMenuItemWorld_[%id,%itemId++] = "Customize Interface" TAB "" TAB "ETools.toggleTool(\"GuiCustomizer\");";
		$LabMenuItemWorld_[%id,%itemId++] = "----------------------";
	$LabMenuItemWorld_[%id,%itemId++] = "Auto arrange MissionGroup Root" TAB "" TAB "SEP_ScenePage.organizeMissionGroup();";
	$LabMenuItemWorld_[%id,%itemId++] = "Auto arrange MissionGroup" TAB "" TAB "SEP_ScenePage.organizeMissionGroup(5);";
	$LabMenuItemWorld_[%id,%itemId++] = "----------------------";
		$LabMenuItemWorld_[%id,%itemId++] = "Capture current view as level preview" TAB "" TAB "Lab.setCurrentViewAsPreview();";
	$LabMenuItemWorld_[%id,%itemId++] = "Set next screenshot as preview" TAB "" TAB "Lab.setNextScreenShotPreview();";
	
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Camera";
	$LabMenuItemWorld_[%id,%itemId++] = "World Camera";
	$LabMenuItemWorld_[%id,%itemId++] = "Player Camera";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle Control Object" TAB $LabMenuCmd SPC "M" TAB "cmdServer(\"Lab.toggleControlObject\");";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle Camera" TAB $LabMenuCmd SPC "C" TAB "cmdServer(\"Game.ToggleClientCamera\");";
	$LabMenuItemWorld_[%id,%itemId++] = "Place Camera at Selection" TAB "Ctrl Q" TAB "EWorldEditor.dropCameraToSelection();";
	$LabMenuItemWorld_[%id,%itemId++] = "Place Camera at Player" TAB "Alt Q" TAB "commandToServer('dropCameraAtPlayer');";
	$LabMenuItemWorld_[%id,%itemId++] = "Place Player at Camera" TAB "Alt W" TAB "commandToServer('DropPlayerAtCamera');";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Fit View to Selection" TAB "F" TAB "commandToServer('EditorCameraAutoFit', EWorldEditor.getSelectionRadius()+1);";
	$LabMenuItemWorld_[%id,%itemId++] = "Fit View To Selection and Orbit" TAB "Alt F" TAB "EditorGuiStatusBar.setCamera(\"Orbit Camera\"; commandToServer('EditorCameraAutoFit', EWorldEditor.getSelectionRadius()+1);";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Speed";
	%subId=-1;
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Slowest" TAB $LabCmd @ "-Shift 1" TAB "5" TAB "$ToggleMe = !$ToggleMe;";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Slow" TAB $LabCmd @ "-Shift 2" TAB "35";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Slower" TAB $LabCmd @ "-Shift 3" TAB "70";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Normal" TAB $LabCmd @ "-Shift 4" TAB "100";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Faster" TAB $LabCmd @ "-Shift 5" TAB "130";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Fast" TAB $LabCmd @ "-Shift 6" TAB "165";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Fastest" TAB $LabCmd @ "-Shift 7" TAB "200";
	$LabMenuItemWorld_[%id,%itemId++] = "View";
	%subId=-1;
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Top" TAB "Alt 2" TAB "Lab.setCameraViewType(\"Top View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Bottom" TAB "Alt 5" TAB "Lab.setCameraViewType(\"Bottom View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Front" TAB "Alt 3" TAB "Lab.setCameraViewType(\"Front View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Back" TAB "Alt 6" TAB "Lab.setCameraViewType(\"Back View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Left" TAB "Alt 4" TAB "Lab.setCameraViewType(\"Left View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++]= "Right" TAB "Alt 7" TAB "Lab.setCameraViewType(\"Right View\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Perspective" TAB "Alt 1" TAB "Lab.setCameraViewType(\"Standard Camera\");";
	$LabSubMenuItemWorld_[%id,%itemId,%subId++] = "Isometric" TAB "Alt 8" TAB "Lab.setCameraViewType(\"Isometric View\");";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Add Bookmark..." TAB "Ctrl B" TAB "EManageBookmarks.addCameraBookmarkByGui();";
	$LabMenuItemWorld_[%id,%itemId++] = "Manage Bookmarks..." TAB "Ctrl-Shift B" TAB "EManageBookmarks.ToggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "Jump to Bookmark";
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Mission";
	$LabMenuItemWorld_[%id,%itemId++] = "Mission settings" TAB "" TAB "toggleDlg(LabMissionSettingsDlg);";
	$LabMenuItemWorld_[%id,%itemId++] = "Set next screenshot as preview" TAB "" TAB "Lab.setNextScreenShotPreview();";
	%itemId = -1;
	$LabMenuWorld_[%id++] = "Tool";
	$LabMenuItemWorld_[%id,%itemId++] = "Editors";
	$LabMenuEditorSubMenu = %id SPC %itemId;
	$LabMenuEditorNextId = -1;

	$LabMenuItemWorld_[%id,%itemId++] = "Toggle GroundCover Manager" TAB "" TAB "SceneEditorDialogs.toggleDlg(\"GroundCover\");";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle Ambient Manager" TAB "" TAB "SceneEditorDialogs.toggleDlg(\"AmbientManager\");";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle Vehicle Manager" TAB "" TAB "SceneEditorDialogs.toggleDlg(\"VehicleManager\");";
	$LabMenuItemWorld_[%id,%itemId++] = "Toggle PostFx Manager" TAB "" TAB "ToggleVisible(EPostFxManager);" TAB "EPostFxManager.visible;";
	
	if ( physicsPluginPresent() && isObject(PhysicsToolsToolbar) ){
		$LabMenuItemWorld_[%id,%itemId++] = "-";
		$LabMenuItemWorld_[%id,%itemId++] = "Toggle PhysicsTools Toolbar" TAB "" TAB "ToggleVisible(PhysicsToolsToolbar);";
		$LabMenuItemWorld_[%id,%itemId++] = "Toggle PhysicsTools Overlay" TAB "" TAB "ETools.toggleTool(\"PhysicsTools\");";
	}
	%itemId = -1;
	$LabMenuWorld_[%id++] = "View";
	$LabMenuItemWorld_[%id,%itemId++] = "Visibility Layers" TAB "Alt V" TAB "EVisibilityLayers.toggleVisibility();";
	$LabMenuItemWorld_[%id,%itemId++] = "Show Grid in Ortho Views" TAB $LabCmd @ "-Shift-Alt G" TAB "EWorldEditor.renderOrthoGrid = !EWorldEditor.renderOrthoGrid;";
	$LabMenuItemWorld_[%id,%itemId++] = "-";
	$LabMenuItemWorld_[%id,%itemId++] = "Set default plugins order" TAB "" TAB "Lab.sortPluginsBar(true);";
	$LabMenuItemWorld_[%id,%itemId++] = "Store plugins order as default" TAB "" TAB "Lab.updatePluginIconOrder(true);";
	
	%itemId = -1; 
	$LabMenuWorld_[%id++] = "Help";
	$LabMenuItemWorld_[%id,%itemId++] = "Online Documentation..." TAB "Alt F1" TAB "gotoWebPage(EWorldEditor.documentationURL);";
	$LabMenuItemWorld_[%id,%itemId++] = "Offline User Guide..." TAB "" TAB "gotoWebPage(EWorldEditor.documentationLocal);";
	$LabMenuItemWorld_[%id,%itemId++] = "Offline Reference Guide..." TAB "" TAB "shellexecute(EWorldEditor.documentationReference);";
	$LabMenuItemWorld_[%id,%itemId++] = "Torque 3D Forums..." TAB "" TAB "gotoWebPage(EWorldEditor.forumURL);";

	
}
//------------------------------------------------------------------------------