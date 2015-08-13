//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$NativeMenu = false;
$NativeMenuMode = "Auto";
/// Create the Gui Editor menu bar.
//GuiEditCanvas.onCreateMenu
function GuiEditCanvas::onCreateMenu(%this) {
	
	GuiEditCanvas.initGuiEdMenuData();
	delObj(%this.menuBar);
	if ($NativeMenu) {
		if ($NativeMenuMode $= "Auto")
			%this.createAutoNativeMenu();
		else
			%this.createNativeMenu();
	} else {
		%this.createLabMenu();
	}
}

/// Called before onSleep when the canvas content is changed
function GuiEditCanvas::onDestroyMenu(%this) {
	if( !isObject( %this.menuBar ) )
		return;

	// Destroy menus
	//while( %this.menuBar.getCount() != 0 )
	//%this.menuBar.getObject( 0 ).delete();
	%this.menuBar.removeFromCanvas();
	%this.menuBar.delete();
}



//==============================================================================
function GuiEditCanvas::initGuiEdMenuData(%this,%buildAfter) {
	//set up %cmdctrl variable so that it matches OS standards
	if( $platform $= "macos" ) {
		%cmdCtrl = "Cmd";
		%menuCmdCtrl = "Cmd";
		%quitShortcut = "Cmd Q";
		%redoShortcut = "Cmd-Shift Z";
	} else {
		%cmdCtrl = "Ctrl";
		%menuCmdCtrl = "Alt";
		%quitShortcut = "Alt F4";
		%redoShortcut = "Ctrl Y";
	}

	%id = -1;
	%itemId = -1;
	$GuiEdMenu[%id++] = "File";
	$GuiEdMenuItem[%id,%itemId++] = "New Gui..." TAB %cmdCtrl SPC "N" TAB "GuiEditCanvas.create();";
	$GuiEdMenuItem[%id,%itemId++] = "Open..." TAB %cmdCtrl SPC "O" TAB "GuiEditCanvas.open();";
	$GuiEdMenuItem[%id,%itemId++] = "Save" TAB %cmdCtrl SPC "S" TAB "GuiEditCanvas.save( false, true );";
	$GuiEdMenuItem[%id,%itemId++] = "Save As..." TAB %cmdCtrl @ "-Shift S" TAB "GuiEditCanvas.save( false );";
	$GuiEdMenuItem[%id,%itemId++] = "Save Selected As..." TAB %cmdCtrl @ "-Alt S" TAB "GuiEditCanvas.save( true );";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Revert Gui" TAB "" TAB "GuiEditCanvas.revert();";
	$GuiEdMenuItem[%id,%itemId++] = "Add Gui From File..." TAB "" TAB "GuiEditCanvas.append();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Open Gui File in Torsion" TAB "" TAB "GuiEditCanvas.openInTorsion();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Close Editor" TAB "F10" TAB "GuiEditCanvas.quit();";
	$GuiEdMenuItem[%id,%itemId++] = "Quit" TAB %cmdCtrl SPC "Q" TAB "quit();";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Edit";
	$GuiEdMenuItem[%id,%itemId++] = "Undo" TAB %cmdCtrl SPC "Z" TAB "GuiEditor.undo();";
	$GuiEdMenuItem[%id,%itemId++] = "Redo" TAB %redoShortcut TAB "GuiEditor.redo();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Cut" TAB %cmdCtrl SPC "X" TAB "GuiEditor.saveSelection(); GuiEditor.deleteSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "Copy" TAB %cmdCtrl SPC "C" TAB "GuiEditor.saveSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "Paste" TAB %cmdCtrl SPC "V" TAB "GuiEditor.loadSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Select All" TAB %cmdCtrl SPC "A" TAB "GuiEditor.selectAll();";
	$GuiEdMenuItem[%id,%itemId++] = "Deselect All" TAB %cmdCtrl SPC "D" TAB "GuiEditor.clearSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "Select Parent(s)" TAB %cmdCtrl @ "-Alt Up" TAB "GuiEditor.selectParents();";
	$GuiEdMenuItem[%id,%itemId++] = "Select Children" TAB %cmdCtrl @ "-Alt Down" TAB "GuiEditor.selectChildren();";
	$GuiEdMenuItem[%id,%itemId++] = "Add Parent(s) to Selection" TAB %cmdCtrl @ "-Alt-Shift Up" TAB "GuiEditor.selectParents( true );";
	$GuiEdMenuItem[%id,%itemId++] = "Add Children to Selection" TAB %cmdCtrl @ "-Alt-Shift Down" TAB "GuiEditor.selectChildren( true );";
	$GuiEdMenuItem[%id,%itemId++] = "Select..." TAB "" TAB "GuiEditorSelectDlg.toggleVisibility();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Lock/Unlock Selection" TAB %cmdCtrl SPC "L" TAB "GuiEditor.toggleLockSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "Hide/Unhide Selection" TAB %cmdCtrl SPC "H" TAB "GuiEditor.toggleHideSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Group Selection" TAB %cmdCtrl SPC "G" TAB "GuiEditor.groupSelected();";
	$GuiEdMenuItem[%id,%itemId++] = "Ungroup Selection" TAB %cmdCtrl @ "-Shift G" TAB "GuiEditor.ungroupSelected();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Full Box Selection" TAB "" TAB "GuiEditor.toggleFullBoxSelection();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Grid Size" TAB %cmdCtrl SPC "," TAB "GuiEditor.showPrefsDialog();";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Layout";
	$GuiEdMenuItem[%id,%itemId++] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "GuiEditor.Justify(0);";
	$GuiEdMenuItem[%id,%itemId++] = "Center Horizontally" TAB "" TAB "GuiEditor.Justify(1);";
	$GuiEdMenuItem[%id,%itemId++] = "Align Right" TAB %cmdCtrl SPC "Right" TAB "GuiEditor.Justify(2);";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Align Top" TAB %cmdCtrl SPC "Up" TAB "GuiEditor.Justify(3);";
	$GuiEdMenuItem[%id,%itemId++] = "Center Vertically" TAB "" TAB "GuiEditor.Justify(7);";
	$GuiEdMenuItem[%id,%itemId++] = "Align Bottom" TAB %cmdCtrl SPC "Down" TAB "GuiEditor.Justify(4);";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Space Vertically" TAB "" TAB "GuiEditor.Justify(5);";
	$GuiEdMenuItem[%id,%itemId++] = "Space Horizontally" TAB "" TAB "GuiEditor.Justify(6);";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Fit into Parent(s)" TAB "Alt f" TAB "GuiEditor.fitIntoParents();";
	$GuiEdMenuItem[%id,%itemId++] = "Fit Width to Parent(s)" TAB "Alt w" TAB "GuiEditor.fitIntoParents( true, false );";
	$GuiEdMenuItem[%id,%itemId++] = "Fit Height to Parent(s)" TAB "Alt h" TAB "GuiEditor.fitIntoParents( false, true );";
	$GuiEdMenuItem[%id,%itemId++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Bring to Front" TAB "" TAB "GuiEditor.BringToFront();";
	$GuiEdMenuItem[%id,%itemId++] = "Send to Back" TAB "" TAB "GuiEditor.PushToBack();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenu[%id++] = "LabLayout";
	$GuiEdMenuItem[%id,%itemId = 0] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "Lab.AlignCtrlToParent(\"left\");";
	$GuiEdMenuItem[%id,%itemId++] = "Align Right" TAB %cmdCtrl SPC "Left" TAB "Lab.AlignCtrlToParent(\"right\");";
	$GuiEdMenuItem[%id,%itemId++] = "Align Top" TAB %cmdCtrl SPC "Left" TAB "Lab.AlignCtrlToParent(\"top\");";
	$GuiEdMenuItem[%id,%itemId++] = "Align Bottom" TAB %cmdCtrl SPC "Left" TAB "Lab.AlignCtrlToParent(\"bottom\");";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Set control as reference-" TAB "Alt r" TAB "Lab.setSelectedControlAsReference( );";
	$GuiEdMenuItem[%id,%itemId++] = "Set profile from reference-" TAB "Shift 1" TAB "Lab.setControlReferenceField(\" Profile \");";
	$GuiEdMenuItem[%id,%itemId++] = "Set position from reference-" TAB "Shift 2" TAB "Lab.setControlReferenceField(\" position \");";
	$GuiEdMenuItem[%id,%itemId++] = "Set extent from reference-" TAB "Shift 3" TAB "Lab.setControlReferenceField(\" extent \");";
	$GuiEdMenuItem[%id,%itemId++] = "Set empty name tp selection-" TAB "Shift n" TAB "Lab.setControlReferenceField(\" name \");";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Toggle auto load last GUI" TAB "" TAB "Lab.toggleAutoLoadLastGui();";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Move";
	$GuiEdMenuItem[%id,%itemId++] = "Nudge Left" TAB "Left" TAB "GuiEditor.move( -1, 0);";
	$GuiEdMenuItem[%id,%itemId++] = "Nudge Right" TAB "Right" TAB "GuiEditor.move( 1, 0);";
	$GuiEdMenuItem[%id,%itemId++] = "Nudge Up" TAB "Up" TAB "GuiEditor.move( 0, -1);";
	$GuiEdMenuItem[%id,%itemId++] = "Nudge Down" TAB "Down" TAB "GuiEditor.move( 0, 1 );";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Big Nudge Left" TAB "Shift Left" TAB "GuiEditor.move( - GuiEditor.snap2gridsize, 0 );";
	$GuiEdMenuItem[%id,%itemId++] = "Big Nudge Right" TAB "Shift Right" TAB "GuiEditor.move( GuiEditor.snap2gridsize, 0 );";
	$GuiEdMenuItem[%id,%itemId++] = "Big Nudge Up" TAB "Shift Up" TAB "GuiEditor.move( 0, - GuiEditor.snap2gridsize );";
	$GuiEdMenuItem[%id,%itemId++] = "Big Nudge Down" TAB "Shift Down" TAB "GuiEditor.move( 0, GuiEditor.snap2gridsize );";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Snap";
	$GuiEdMenuItem[%id,%itemId++] = "Snap Edges" TAB "Alt-Shift E" TAB "GuiEditor.toggleEdgeSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "Snap Centers" TAB "Alt-Shift C" TAB "GuiEditor.toggleCenterSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Snap to Guides" TAB "Alt-Shift G" TAB "GuiEditor.toggleGuideSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "Snap to Controls" TAB "Alt-Shift T" TAB "GuiEditor.toggleControlSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "Snap to Canvas" TAB "" TAB "GuiEditor.toggleCanvasSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "Snap to Grid" TAB "" TAB "GuiEditor.toggleGridSnap();";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Show Guides" TAB "" TAB "GuiEditor.toggleDrawGuides();";
	$GuiEdMenuItem[%id,%itemId++] = "Clear Guides" TAB "" TAB "GuiEditor.clearGuides();";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Lab Menu";
	$GuiEdMenuItem[%id,%itemId++] = "-> Lab Editors GUIs <-";
	$GuiEdMenuItem[%id,%itemId++] = "Remove from EditorGui" TAB "" TAB "Lab.setGuisToDefault();";
	$GuiEdMenuItem[%id,%itemId++] = "Add to EditorGui" TAB "" TAB "Lab.setGuisToEditor();";
	$GuiEdMenuItem[%id,%itemId++] = "-> General Editors GUIs <-";
	$GuiEdMenuItem[%id,%itemId++] = "Toggle Editors GUI listing" TAB "" TAB "Lab.toggleEditorGuiListing();";
	$GuiEdMenuItem[%id,%itemId++] = "Detach the Editor GUIs" TAB "" TAB "Lab.detachAllEditorGuis();";
	$GuiEdMenuItem[%id,%itemId++] = "Attach the Editor GUIs" TAB "" TAB "Lab.attachAllEditorGuis();";
	$GuiEdMenuItem[%id,%itemId++] = "Toggle Lab Editor Settings" TAB "" TAB "toggleDlg(LabEditorSettings);";
	$GuiEdMenuItem[%id,%itemId++] = "-> Special Editors GUIs <-";
	$GuiEdMenuItem[%id,%itemId++] = "Toggle Field Duplicator" TAB  "Alt-Shift D" TAB "Lab.toggleDuplicator();";
	$GuiEdMenuItem[%id,%itemId++] = "-> Special TorqueLab functions <-";
	$GuiEdMenuItem[%id,%itemId++] = "Save all toolbars" TAB  "" TAB "Lab.saveToolbar();";
	$GuiEdMenuItem[%id,%itemId++] = "-> Editing the GuiEditor GUI <-";
	$GuiEdMenuItem[%id,%itemId++] = "Clone GuiEditorGui" TAB  "" TAB "Lab.cloneGuiEditor();";
	$GuiEdMenuItem[%id,%itemId++] = "Apply GuiEditorGui Clone" TAB  "" TAB "Lab.convertClonedGuiEditor();";
	%itemId = -1;
	$GuiEdMenu[%id++] = "Help";
	$GuiEdMenuItem[%id,%itemId++] = "Online Documentation..." TAB "Alt F1" TAB "gotoWebPage( GuiEditor.documentationURL );";
	$GuiEdMenuItem[%id,%itemId++] = "Offline User Guid..." TAB "" TAB "gotoWebPage( GuiEditor.documentationLocal );";
	$GuiEdMenuItem[%id,%itemId++] = "Offline Reference Guide..." TAB "" TAB "shellExecute( GuiEditor.documentationReference );";
	$GuiEdMenuItem[%id,%itemId++] = "-";
	$GuiEdMenuItem[%id,%itemId++] = "Torque 3D Public Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/73\" );";
	$GuiEdMenuItem[%id,%itemId++] = "Torque 3D Private Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/63\" );";
}
//------------------------------------------------------------------------------
