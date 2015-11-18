//==============================================================================
// TorqueLab GUI -> WidgetBuilder system
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Create quickly a set of GUI based on a template
//==============================================================================

//==============================================================================
function initializeGuiEditor() {
	echo( " % - Initializing Gui Editor" );

	if (!isObject(GuiLab))
		$GuiLab = new scriptObject("GuiLab");

	$GuiEd = new scriptObject("GuiEd");
	delObj(GuiEdMap);
	new ActionMap(GuiEdMap);
	// GUIs.
	execGuiEdit(true);
}
//------------------------------------------------------------------------------
function execGuiLab() {
	execPattern( "tlab/guiEditor/lab/*.cs" );
	execPattern( "tlab/guiEditor/system/*.cs" );
}
//==============================================================================
function execGuiEdit(%execGui,%execMainGui) {
	if (%execGui) {
		%execMainGui = true;
		exec( "./gui/guiEditorNewGuiDialog.ed.gui" );
		exec( "./gui/guiEditorPrefsDlg.ed.gui" );
		//exec( "./gui/guiEditorSelectDlg.ed.gui" );
		exec( "./gui/EditorChooseGUI.ed.gui" );
		exec( "./gui/LabWidgetBuilderDlg.gui" );
		exec( "tlab/guiEditor/gui/GuiEditFieldDuplicator.gui" );
		exec( "tlab/guiEditor/gui/GuiEdTemplateManager.gui" );
		exec( "tlab/guiEditor/gui/GuiEdTemplateEditor.gui" );
		exec( "tlab/guiEditor/gui/GuiEdTemplateGroup.gui" );
	}

	if (%execMainGui) {
		exec( "tlab/guiEditor/gui/guiEditor.ed.gui" );
		exec( "tlab/guiEditor/gui/CloneEditorGui.gui" );
	}

	if (!isObject(GuiEditor)) {
		addGuiEditorCtrl();
	}

	// Scripts.
	exec( "tlab/guiEditor/scripts/guiEditor.ed.cs" );
	exec( "tlab/guiEditor/scripts/guiEditorTreeView.ed.cs" );
	exec( "./scripts/guiEditorInspector.ed.cs" );
	exec( "tlab/guiEditor/scripts/guiEditorProfiles.ed.cs" );
	exec( "./scripts/guiEditorGroup.ed.cs" );
	exec( "tlab/guiEditor/scripts/guiEditorUndo.ed.cs" );
	exec( "tlab/guiEditor/scripts/guiEditorCanvas.ed.cs" );
	exec( "./scripts/guiEditorContentList.ed.cs" );
	exec( "./scripts/guiEditorStatusBar.ed.cs" );
	exec( "./scripts/guiEditorToolbox.ed.cs" );
	exec( "./scripts/guiEditorSelectDlg.ed.cs" );
	exec( "./scripts/guiEditorNewGuiDialog.ed.cs" );
	exec( "./scripts/fileDialogs.ed.cs" );
	exec( "./scripts/guiEditorPrefsDlg.ed.cs" );
	exec( "./scripts/EditorChooseGUI.ed.cs" );
	exec( "./gui/LabWidgetBuilderDlg.cs" );
	exec( "tlab/guiEditor/scripts/functionControls.cs" );
	execPattern( "tlab/guiEditor/lab/*.cs" );
	execPattern( "tlab/guiEditor/system/*.cs" );
	GuiEd.InitGuiEditor();
}
//------------------------------------------------------------------------------
//==============================================================================
function destroyGuiEditor() {
}
//------------------------------------------------------------------------------
