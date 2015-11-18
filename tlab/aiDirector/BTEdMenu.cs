//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function BTEdMenu::initData(%this) {
	//set up $LabCmd variable so that it matches OS standards
	if( $platform $= "macos" ) {
		$LabCmd = "Cmd";
		$LabMenuCmd = "Cmd";
		$LabQuit = "Cmd Q";
		$LabRedo = "Cmd-Shift Z";
	} else {
		$LabCmd = "Ctrl";
		$LabMenuCmd = "Alt";
		$LabQuit = "Alt F4";
		$LabRedo = "Ctrl Y";
	}

	%id = -1;
	%itemId = -1;
	$LabMenuBT_[%id++] = "File";
	$LabMenuItemBT_[%id,%itemId++] = "New Tree..." TAB $LabCmd SPC "N" TAB "BTEditor.createTree();";
	$LabMenuItemBT_[%id,%itemId++] = "Open..." TAB $LabCmd SPC "O" TAB "BTEditor.open();";
	$LabMenuItemBT_[%id,%itemId++] = "Save Tree" TAB $LabCmd SPC "S" TAB "BTEditor.saveTree( BTEditor.getCurrentRootNode(), false );";
	$LabMenuItemBT_[%id,%itemId++] = "Save Tree As..." TAB $LabCmd @ "-Shift S" TAB "BTEditor.saveTree( BTEditor.getCurrentRootNode(), true );";
	$LabMenuItemBT_[%id,%itemId++] = "-";
	$LabMenuItemBT_[%id,%itemId++] = "Close Editor" TAB "F9" TAB %this @ ".quit();";
	$LabMenuItemBT_[%id,%itemId++]= "Quit" TAB $LabCmd SPC "Q" TAB "quit();";
	%itemId = -1;
	$LabMenuBT_[%id++] = "Edit";
	$LabMenuItemBT_[%id,%itemId++] = "Undo" TAB %cmdCtrl SPC "Z" TAB "BTEditor.undo();";
	$LabMenuItemBT_[%id,%itemId++] = "Redo" TAB %redoShortcut TAB "BTEditor.redo();";
	$LabMenuItemBT_[%id,%itemId++] = "-";
	$LabMenuItemBT_[%id,%itemId++] = "Delete node" TAB "" TAB "BTEditor.getCurrentViewCtrl().deleteSelection();";
	$LabMenuItemBT_[%id,%itemId++] = "Excise node" TAB "" TAB "BTEditor.getCurrentViewCtrl().exciseSelection();";
	%itemId = -1;
	$LabMenuBT_[%id++] = "View";
	$LabMenuItemBT_[%id,%itemId++] = "Expand All" TAB %cmdCtrl SPC "=" TAB "BTEditor.expandAll();";
	$LabMenuItemBT_[%id,%itemId++] = "Collapse All" TAB %cmdCtrl SPC "-" TAB "BTEditor.collapseAll();";
}
//------------------------------------------------------------------------------
