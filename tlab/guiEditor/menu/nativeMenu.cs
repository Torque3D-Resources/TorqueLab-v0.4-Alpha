//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------


//=============================================================================================
//    Event Handlers.
//=============================================================================================
function GuiEditCanvas::createAutoNativeMenu(%this) {
	delObj(%this.menuBar);

	

	%this.menuBar = new MenuBar(){
		dynamicItemInsertPos = 3;		
		};
		
	%menuId = 0;
	while($GuiEdMenu[%menuId] !$= ""){
		%menu = new PopupMenu() {
			superClass = "MenuBuilder";
			class = "EditorFileMenu";
			barTitle = $GuiEdMenu[%menuId];
		};
		
		devLog("---------",$GuiEdMenu[%menuId]);			
		%itemId = 0;
		while($GuiEdMenuItem[%menuId,%itemId] !$= ""){			
			%subId = 0;
			devLog("---------",$GuiEdMenuItem[%menuId,%itemId]);		
			if ($GuiEdMenuSubMenuItem[%menuId,%itemId,%subId] $= ""){
				%menu.appendItem($GuiEdMenuItem[%menuId,%itemId]);				
			}
			else {			
				%subMenu = new PopupMenu() {
					superClass = "MenuBuilder";
					class = "EditorFileMenu";				
				};	
				while($GuiEdMenuSubMenuItem[%menuId,%itemId,%subId] !$= ""){
					%subMenu.appendItem($GuiEdMenuSubMenuItem[%menuId,%itemId,%subId]);
					devLog("--------- --------",$GuiEdMenuSubMenuItem[%menuId,%itemId,%subId]);		
					%subId++;
				}
				%menu.appendItem($GuiEdMenuItem[%menuId,%itemId] TAB %subMenu);	
						
			}
			%itemId++;
		}		
		%menuId++;
		%this.menuBar.add(%menu);
	}	
	%this.menuBar.attachToCanvas( Canvas, 0 );
}

function GuiEditCanvas::createNativeMenu(%this) {
	devLog("CreateMenu");
	delObj(%this.menuBar);

	if(isObject(%this.menuBar))
		return;

	//set up %cmdctrl variable so that it matches OS standards
	if( $platform $= "macos" ) {
		%cmdCtrl = "cmd";
		%redoShortcut = "Cmd-Shift Z";
	} else {
		%cmdCtrl = "Ctrl";
		%redoShort = "Ctrl Y";
	}

	// Menu bar
	%this.menuBar = new MenuBar() {
		dynamicItemInsertPos = 3;
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "File";
			internalName = "FileMenu";
			item[0] = "New Gui..." TAB %cmdCtrl SPC "N" TAB %this @ ".create();";
			item[1] = "Open..." TAB %cmdCtrl SPC "O" TAB %this @ ".open();";
			item[2] = "Save" TAB %cmdCtrl SPC "S" TAB %this @ ".save( false, true );";
			item[3] = "Save As..." TAB %cmdCtrl @ "-Shift S" TAB %this @ ".save( false );";
			item[4] = "Save Selected As..." TAB %cmdCtrl @ "-Alt S" TAB %this @ ".save( true );";
			item[5] = "-";
			item[6] = "Revert Gui" TAB "" TAB %this @ ".revert();";
			item[7] = "Add Gui From File..." TAB "" TAB %this @ ".append();";
			item[8] = "-";
			item[9] = "Open Gui File in Torsion" TAB "" TAB %this @".openInTorsion();";
			item[10] = "-";
			item[11] = "Close Editor" TAB "F10" TAB %this @ ".quit();";
			item[12] = "Quit" TAB %cmdCtrl SPC "Q" TAB "quit();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Edit";
			internalName = "EditMenu";
			item[0] = "Undo" TAB %cmdCtrl SPC "Z" TAB "GuiEditor.undo();";
			item[1] = "Redo" TAB %redoShortcut TAB "GuiEditor.redo();";
			item[2] = "-";
			item[3] = "Cut" TAB %cmdCtrl SPC "X" TAB "GuiEditor.saveSelection(); GuiEditor.deleteSelection();";
			item[4] = "Copy" TAB %cmdCtrl SPC "C" TAB "GuiEditor.saveSelection();";
			item[5] = "Paste" TAB %cmdCtrl SPC "V" TAB "GuiEditor.loadSelection();";
			item[6] = "-";
			item[7] = "Select All" TAB %cmdCtrl SPC "A" TAB "GuiEditor.selectAll();";
			item[8] = "Deselect All" TAB %cmdCtrl SPC "D" TAB "GuiEditor.clearSelection();";
			item[9] = "Select Parent(s)" TAB %cmdCtrl @ "-Alt Up" TAB "GuiEditor.selectParents();";
			item[10] = "Select Children" TAB %cmdCtrl @ "-Alt Down" TAB "GuiEditor.selectChildren();";
			item[11] = "Add Parent(s) to Selection" TAB %cmdCtrl @ "-Alt-Shift Up" TAB "GuiEditor.selectParents( true );";
			item[12] = "Add Children to Selection" TAB %cmdCtrl @ "-Alt-Shift Down" TAB "GuiEditor.selectChildren( true );";
			item[13] = "Select..." TAB "" TAB "GuiEditorSelectDlg.toggleVisibility();";
			item[14] = "-";
			item[15] = "Lock/Unlock Selection" TAB %cmdCtrl SPC "L" TAB "GuiEditor.toggleLockSelection();";
			item[16] = "Hide/Unhide Selection" TAB %cmdCtrl SPC "H" TAB "GuiEditor.toggleHideSelection();";
			item[17] = "-";
			item[18] = "Group Selection" TAB %cmdCtrl SPC "G" TAB "GuiEditor.groupSelected();";
			item[19] = "Ungroup Selection" TAB %cmdCtrl @ "-Shift G" TAB "GuiEditor.ungroupSelected();";
			item[20] = "-";
			item[21] = "Full Box Selection" TAB "" TAB "GuiEditor.toggleFullBoxSelection();";
			item[22] = "-";
			item[23] = "Grid Size" TAB %cmdCtrl SPC "," TAB "GuiEditor.showPrefsDialog();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Layout";
			internalName = "LayoutMenu";
			item[%id = 0] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "GuiEditor.Justify(0);";
			item[%id++] = "Center Horizontally" TAB "" TAB "GuiEditor.Justify(1);";
			item[%id++] = "Align Right" TAB %cmdCtrl SPC "Right" TAB "GuiEditor.Justify(2);";
			item[%id++] = "-";
			item[%id++] = "Align Top" TAB %cmdCtrl SPC "Up" TAB "GuiEditor.Justify(3);";
			item[%id++] = "Center Vertically" TAB "" TAB "GuiEditor.Justify(7);";
			item[%id++] = "Align Bottom" TAB %cmdCtrl SPC "Down" TAB "GuiEditor.Justify(4);";
			item[%id++] = "-";
			item[%id++] = "Space Vertically" TAB "" TAB "GuiEditor.Justify(5);";
			item[%id++] = "Space Horizontally" TAB "" TAB "GuiEditor.Justify(6);";
			item[%id++] = "-";
			item[%id++] = "Fit into Parent(s)" TAB "Alt f" TAB "GuiEditor.fitIntoParents();";
			item[%id++] = "Fit Width to Parent(s)" TAB "Alt w" TAB "GuiEditor.fitIntoParents( true, false );";
			item[%id++] = "Fit Height to Parent(s)" TAB "Alt h" TAB "GuiEditor.fitIntoParents( false, true );";
			item[%id++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
			item[%id++] = "-";
			item[%id++] = "Bring to Front" TAB "" TAB "GuiEditor.BringToFront();";
			item[%id++] = "Send to Back" TAB "" TAB "GuiEditor.PushToBack();";
			item[%id++] = "-";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "LabLayout";
			internalName = "LabLayoutMenu";
			item[%id = 0] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"left\");";
			item[%id++] = "Align Right" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"right\");";
			item[%id++] = "Align Top" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"top\");";
			item[%id++] = "Align Bottom" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"bottom\");";
			item[%id++] = "-";
			item[%id++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
			item[%id++] = "-";
			item[%id++] = "Set control as reference-" TAB "Alt r" TAB "Lab.setSelectedControlAsReference( );";
			item[%id++] = "Set profile from reference-" TAB "Shift 1" TAB "Lab.setControlReferenceField(\" Profile \");";
			item[%id++] = "Set position from reference-" TAB "Shift 2" TAB "Lab.setControlReferenceField(\" position \");";
			item[%id++] = "Set extent from reference-" TAB "Shift 3" TAB "Lab.setControlReferenceField(\" extent \");";
			item[%id++] = "Set empty name tp selection-" TAB "Shift n" TAB "Lab.setControlReferenceField(\" name \");";
			item[%id++] = "-";
			item[%id++] = "Toggle auto load last GUI" TAB "" TAB "Lab.toggleAutoLoadLastGui();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Move";
			internalName = "MoveMenu";
			item[0] = "Nudge Left" TAB "Left" TAB "GuiEditor.move( -1, 0);";
			item[1] = "Nudge Right" TAB "Right" TAB "GuiEditor.move( 1, 0);";
			item[2] = "Nudge Up" TAB "Up" TAB "GuiEditor.move( 0, -1);";
			item[3] = "Nudge Down" TAB "Down" TAB "GuiEditor.move( 0, 1 );";
			item[4] = "-";
			item[5] = "Big Nudge Left" TAB "Shift Left" TAB "GuiEditor.move( - GuiEditor.snap2gridsize, 0 );";
			item[6] = "Big Nudge Right" TAB "Shift Right" TAB "GuiEditor.move( GuiEditor.snap2gridsize, 0 );";
			item[7] = "Big Nudge Up" TAB "Shift Up" TAB "GuiEditor.move( 0, - GuiEditor.snap2gridsize );";
			item[8] = "Big Nudge Down" TAB "Shift Down" TAB "GuiEditor.move( 0, GuiEditor.snap2gridsize );";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Snap";
			internalName = "SnapMenu";
			item[0] = "Snap Edges" TAB "Alt-Shift E" TAB "GuiEditor.toggleEdgeSnap();";
			item[1] = "Snap Centers" TAB "Alt-Shift C" TAB "GuiEditor.toggleCenterSnap();";
			item[2] = "-";
			item[3] = "Snap to Guides" TAB "Alt-Shift G" TAB "GuiEditor.toggleGuideSnap();";
			item[4] = "Snap to Controls" TAB "Alt-Shift T" TAB "GuiEditor.toggleControlSnap();";
			item[5] = "Snap to Canvas" TAB "" TAB "GuiEditor.toggleCanvasSnap();";
			item[6] = "Snap to Grid" TAB "" TAB "GuiEditor.toggleGridSnap();";
			item[7] = "-";
			item[8] = "Show Guides" TAB "" TAB "GuiEditor.toggleDrawGuides();";
			item[9] = "Clear Guides" TAB "" TAB "GuiEditor.clearGuides();";
		};
		
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Lab Menu";
			internalName = "LabMenu";

			item[%id = 0] = "-> Lab Editors GUIs <-";
			item[%id++] = "Remove from EditorGui" TAB "" TAB "Lab.setGuisToDefault();";
			item[%id++] = "Add to EditorGui" TAB "" TAB "Lab.setGuisToEditor();";
			item[%id++] = "-> General Editors GUIs <-";
			item[%id++] = "Toggle Editors GUI listing" TAB "" TAB "Lab.toggleEditorGuiListing();";
			item[%id++] = "Detach the Editor GUIs" TAB "" TAB "Lab.detachAllEditorGuis();";
			item[%id++] = "Attach the Editor GUIs" TAB "" TAB "Lab.attachAllEditorGuis();";
			item[%id++] = "Toggle Lab Editor Settings" TAB "" TAB "toggleDlg(LabEditorSettings);";
			item[%id++] = "-> Special Editors GUIs <-";
			item[%id++] = "Toggle Field Duplicator" TAB  "Alt-Shift D" TAB "Lab.toggleDuplicator();";
			item[%id++] = "-> Special TorqueLab functions <-";
			item[%id++] = "Save all toolbars" TAB  "" TAB "Lab.saveToolbar();";
			item[%id++] = "-> Editing the GuiEditor GUI <-";
			item[%id++] = "Clone GuiEditorGui" TAB  "" TAB "Lab.cloneGuiEditor();";
			item[%id++] = "Apply GuiEditorGui Clone" TAB  "" TAB "Lab.convertClonedGuiEditor();";
			
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			internalName = "HelpMenu";
			barTitle = "Help";
			item[0] = "Online Documentation..." TAB "Alt F1" TAB "gotoWebPage( GuiEditor.documentationURL );";
			item[1] = "Offline User Guid..." TAB "" TAB "gotoWebPage( GuiEditor.documentationLocal );";
			item[2] = "Offline Reference Guide..." TAB "" TAB "shellExecute( GuiEditor.documentationReference );";
			item[3] = "-";
			item[4] = "Torque 3D Public Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/73\" );";
			item[5] = "Torque 3D Private Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/63\" );";
		};
	};
	%this.menuBar.attachToCanvas( Canvas, 0 );
}

$GUI_EDITOR_MENU_EDGESNAP_INDEX = 0;
$GUI_EDITOR_MENU_CENTERSNAP_INDEX = 1;
$GUI_EDITOR_MENU_GUIDESNAP_INDEX = 3;
$GUI_EDITOR_MENU_CONTROLSNAP_INDEX = 4;
$GUI_EDITOR_MENU_CANVASSNAP_INDEX = 5;
$GUI_EDITOR_MENU_GRIDSNAP_INDEX = 6;
$GUI_EDITOR_MENU_DRAWGUIDES_INDEX = 8;
$GUI_EDITOR_MENU_FULLBOXSELECT_INDEX = 21;
/*

/// Create the Gui Editor menu bar.
function GuiEditCanvas::createNativeMenu(%this) {
	delObj(%this.menuBar);

	if(isObject(%this.menuBar))
		return;

	//set up %cmdctrl variable so that it matches OS standards
	if( $platform $= "macos" ) {
		%cmdCtrl = "cmd";
		%redoShortcut = "Cmd-Shift Z";
	} else {
		%cmdCtrl = "Ctrl";
		%redoShort = "Ctrl Y";
	}

	// Menu bar
	%this.menuBar = new MenuBar() {
		dynamicItemInsertPos = 3;
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "File";
			internalName = "FileMenu";
			item[0] = "New Gui..." TAB %cmdCtrl SPC "N" TAB %this @ ".create();";
			item[1] = "Open..." TAB %cmdCtrl SPC "O" TAB %this @ ".open();";
			item[2] = "Save" TAB %cmdCtrl SPC "S" TAB %this @ ".save( false, true );";
			item[3] = "Save As..." TAB %cmdCtrl @ "-Shift S" TAB %this @ ".save( false );";
			item[4] = "Save Selected As..." TAB %cmdCtrl @ "-Alt S" TAB %this @ ".save( true );";
			item[5] = "-";
			item[6] = "Revert Gui" TAB "" TAB %this @ ".revert();";
			item[7] = "Add Gui From File..." TAB "" TAB %this @ ".append();";
			item[8] = "-";
			item[9] = "Open Gui File in Torsion" TAB "" TAB %this @".openInTorsion();";
			item[10] = "-";
			item[11] = "Close Editor" TAB "F10" TAB %this @ ".quit();";
			item[12] = "Quit" TAB %cmdCtrl SPC "Q" TAB "quit();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Edit";
			internalName = "EditMenu";
			item[0] = "Undo" TAB %cmdCtrl SPC "Z" TAB "GuiEditor.undo();";
			item[1] = "Redo" TAB %redoShortcut TAB "GuiEditor.redo();";
			item[2] = "-";
			item[3] = "Cut" TAB %cmdCtrl SPC "X" TAB "GuiEditor.saveSelection(); GuiEditor.deleteSelection();";
			item[4] = "Copy" TAB %cmdCtrl SPC "C" TAB "GuiEditor.saveSelection();";
			item[5] = "Paste" TAB %cmdCtrl SPC "V" TAB "GuiEditor.loadSelection();";
			item[6] = "-";
			item[7] = "Select All" TAB %cmdCtrl SPC "A" TAB "GuiEditor.selectAll();";
			item[8] = "Deselect All" TAB %cmdCtrl SPC "D" TAB "GuiEditor.clearSelection();";
			item[9] = "Select Parent(s)" TAB %cmdCtrl @ "-Alt Up" TAB "GuiEditor.selectParents();";
			item[10] = "Select Children" TAB %cmdCtrl @ "-Alt Down" TAB "GuiEditor.selectChildren();";
			item[11] = "Add Parent(s) to Selection" TAB %cmdCtrl @ "-Alt-Shift Up" TAB "GuiEditor.selectParents( true );";
			item[12] = "Add Children to Selection" TAB %cmdCtrl @ "-Alt-Shift Down" TAB "GuiEditor.selectChildren( true );";
			item[13] = "Select..." TAB "" TAB "GuiEditorSelectDlg.toggleVisibility();";
			item[14] = "-";
			item[15] = "Lock/Unlock Selection" TAB %cmdCtrl SPC "L" TAB "GuiEditor.toggleLockSelection();";
			item[16] = "Hide/Unhide Selection" TAB %cmdCtrl SPC "H" TAB "GuiEditor.toggleHideSelection();";
			item[17] = "-";
			item[18] = "Group Selection" TAB %cmdCtrl SPC "G" TAB "GuiEditor.groupSelected();";
			item[19] = "Ungroup Selection" TAB %cmdCtrl @ "-Shift G" TAB "GuiEditor.ungroupSelected();";
			item[20] = "-";
			item[21] = "Full Box Selection" TAB "" TAB "GuiEditor.toggleFullBoxSelection();";
			item[22] = "-";
			item[23] = "Grid Size" TAB %cmdCtrl SPC "," TAB "GuiEditor.showPrefsDialog();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Layout";
			internalName = "LayoutMenu";
			item[%id = 0] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "GuiEditor.Justify(0);";
			item[%id++] = "Center Horizontally" TAB "" TAB "GuiEditor.Justify(1);";
			item[%id++] = "Align Right" TAB %cmdCtrl SPC "Right" TAB "GuiEditor.Justify(2);";
			item[%id++] = "-";
			item[%id++] = "Align Top" TAB %cmdCtrl SPC "Up" TAB "GuiEditor.Justify(3);";
			item[%id++] = "Center Vertically" TAB "" TAB "GuiEditor.Justify(7);";
			item[%id++] = "Align Bottom" TAB %cmdCtrl SPC "Down" TAB "GuiEditor.Justify(4);";
			item[%id++] = "-";
			item[%id++] = "Space Vertically" TAB "" TAB "GuiEditor.Justify(5);";
			item[%id++] = "Space Horizontally" TAB "" TAB "GuiEditor.Justify(6);";
			item[%id++] = "-";
			item[%id++] = "Fit into Parent(s)" TAB "Alt f" TAB "GuiEditor.fitIntoParents();";
			item[%id++] = "Fit Width to Parent(s)" TAB "Alt w" TAB "GuiEditor.fitIntoParents( true, false );";
			item[%id++] = "Fit Height to Parent(s)" TAB "Alt h" TAB "GuiEditor.fitIntoParents( false, true );";
			item[%id++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
			item[%id++] = "-";
			item[%id++] = "Bring to Front" TAB "" TAB "GuiEditor.BringToFront();";
			item[%id++] = "Send to Back" TAB "" TAB "GuiEditor.PushToBack();";
			item[%id++] = "-";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "LabLayout";
			internalName = "LabLayoutMenu";
			item[%id = 0] = "Align Left" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"left\");";
			item[%id++] = "Align Right" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"right\");";
			item[%id++] = "Align Top" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"top\");";
			item[%id++] = "Align Bottom" TAB %cmdCtrl SPC "Left" TAB "GuiEd.AlignCtrlToParent(\"bottom\");";
			item[%id++] = "-";
			item[%id++] = "Force into Parent-" TAB "Alt g" TAB "Lab.forceCtrlInsideParent( );";
			item[%id++] = "-";
			item[%id++] = "Set control as reference-" TAB "Alt r" TAB "Lab.setSelectedControlAsReference( );";
			item[%id++] = "Set profile from reference-" TAB "Shift 1" TAB "Lab.setControlReferenceField(\" Profile \");";
			item[%id++] = "Set position from reference-" TAB "Shift 2" TAB "Lab.setControlReferenceField(\" position \");";
			item[%id++] = "Set extent from reference-" TAB "Shift 3" TAB "Lab.setControlReferenceField(\" extent \");";
			item[%id++] = "Set empty name tp selection-" TAB "Shift n" TAB "Lab.setControlReferenceField(\" name \");";
			item[%id++] = "-";
			item[%id++] = "Toggle auto load last GUI" TAB "" TAB "Lab.toggleAutoLoadLastGui();";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Move";
			internalName = "MoveMenu";
			item[0] = "Nudge Left" TAB "Left" TAB "GuiEditor.move( -1, 0);";
			item[1] = "Nudge Right" TAB "Right" TAB "GuiEditor.move( 1, 0);";
			item[2] = "Nudge Up" TAB "Up" TAB "GuiEditor.move( 0, -1);";
			item[3] = "Nudge Down" TAB "Down" TAB "GuiEditor.move( 0, 1 );";
			item[4] = "-";
			item[5] = "Big Nudge Left" TAB "Shift Left" TAB "GuiEditor.move( - GuiEditor.snap2gridsize, 0 );";
			item[6] = "Big Nudge Right" TAB "Shift Right" TAB "GuiEditor.move( GuiEditor.snap2gridsize, 0 );";
			item[7] = "Big Nudge Up" TAB "Shift Up" TAB "GuiEditor.move( 0, - GuiEditor.snap2gridsize );";
			item[8] = "Big Nudge Down" TAB "Shift Down" TAB "GuiEditor.move( 0, GuiEditor.snap2gridsize );";
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Snap";
			internalName = "SnapMenu";
			item[0] = "Snap Edges" TAB "Alt-Shift E" TAB "GuiEditor.toggleEdgeSnap();";
			item[1] = "Snap Centers" TAB "Alt-Shift C" TAB "GuiEditor.toggleCenterSnap();";
			item[2] = "-";
			item[3] = "Snap to Guides" TAB "Alt-Shift G" TAB "GuiEditor.toggleGuideSnap();";
			item[4] = "Snap to Controls" TAB "Alt-Shift T" TAB "GuiEditor.toggleControlSnap();";
			item[5] = "Snap to Canvas" TAB "" TAB "GuiEditor.toggleCanvasSnap();";
			item[6] = "Snap to Grid" TAB "" TAB "GuiEditor.toggleGridSnap();";
			item[7] = "-";
			item[8] = "Show Guides" TAB "" TAB "GuiEditor.toggleDrawGuides();";
			item[9] = "Clear Guides" TAB "" TAB "GuiEditor.clearGuides();";
		};
		
		new PopupMenu() {
			superClass = "MenuBuilder";
			barTitle = "Lab Menu";
			internalName = "LabMenu";

			item[%id = 0] = "-> Lab Editors GUIs <-";
			item[%id++] = "Remove from EditorGui" TAB "" TAB "Lab.setGuisToDefault();";
			item[%id++] = "Add to EditorGui" TAB "" TAB "Lab.setGuisToEditor();";
			item[%id++] = "-> General Editors GUIs <-";
			item[%id++] = "Toggle Editors GUI listing" TAB "" TAB "Lab.toggleEditorGuiListing();";
			item[%id++] = "Detach the Editor GUIs" TAB "" TAB "Lab.detachAllEditorGuis();";
			item[%id++] = "Attach the Editor GUIs" TAB "" TAB "Lab.attachAllEditorGuis();";
			item[%id++] = "Toggle Lab Editor Settings" TAB "" TAB "toggleDlg(LabEditorSettings);";
			item[%id++] = "-> Special Editors GUIs <-";
			item[%id++] = "Toggle Field Duplicator" TAB  "Alt-Shift D" TAB "Lab.toggleDuplicator();";
			item[%id++] = "-> Special TorqueLab functions <-";
			item[%id++] = "Save all toolbars" TAB  "" TAB "Lab.saveToolbar();";
			item[%id++] = "-> Editing the GuiEditor GUI <-";
			item[%id++] = "Clone GuiEditorGui" TAB  "" TAB "Lab.cloneGuiEditor();";
			item[%id++] = "Apply GuiEditorGui Clone" TAB  "" TAB "Lab.convertClonedGuiEditor();";
			
		};
		new PopupMenu() {
			superClass = "MenuBuilder";
			internalName = "HelpMenu";
			barTitle = "Help";
			item[0] = "Online Documentation..." TAB "Alt F1" TAB "gotoWebPage( GuiEditor.documentationURL );";
			item[1] = "Offline User Guid..." TAB "" TAB "gotoWebPage( GuiEditor.documentationLocal );";
			item[2] = "Offline Reference Guide..." TAB "" TAB "shellExecute( GuiEditor.documentationReference );";
			item[3] = "-";
			item[4] = "Torque 3D Public Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/73\" );";
			item[5] = "Torque 3D Private Forums..." TAB "" TAB "gotoWebPage( \"http://www.garagegames.com/community/forums/63\" );";
		};
	};
	%this.menuBar.attachToCanvas( Canvas, 0 );
}

$GUI_EDITOR_MENU_EDGESNAP_INDEX = 0;
$GUI_EDITOR_MENU_CENTERSNAP_INDEX = 1;
$GUI_EDITOR_MENU_GUIDESNAP_INDEX = 3;
$GUI_EDITOR_MENU_CONTROLSNAP_INDEX = 4;
$GUI_EDITOR_MENU_CANVASSNAP_INDEX = 5;
$GUI_EDITOR_MENU_GRIDSNAP_INDEX = 6;
$GUI_EDITOR_MENU_DRAWGUIDES_INDEX = 8;
$GUI_EDITOR_MENU_FULLBOXSELECT_INDEX = 21;

*/