//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function FileBrowser::goToCurrentFolder(%this) {
	%sel = EWorldEditor.getSelectedObject(0);

	if (!isObject(%sel))
		return;

	%path = "No path found";

	if (isFile(%sel.shapeName)) {
		%file = %sel.shapeName;
		%path = filePath(%file);
	} else if (isFile(%sel.fileName)) {
		%file = %sel.fileName;
		%path = filePath(%file);
	}

	%address = strReplace( %path,"/"," ");
	%this.navigate(%address);
	devLog("Sel =",%sel,"Class",%sel.getClassName(),"Path",%address);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowserIcon::onClick(%this) {
	devLog("FileBrowserIcon OnClick",%this);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::iconFolderAlt(%this,%icon) {
	devLog("iconFolderAlt",%icon);
	FileBrowser.navigateDown(%icon.text);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::iconMeshAlt(%this,%icon) {
	devLog("iconMeshAlt",%icon);

	if (Lab.currentEditor.isMethod("addFileBrowserMesh"))
		Lab.currentEditor.addFileBrowserMesh(%icon.fullPath,%icon.createCmd);
	else if (EWorldEditor.visible)
		ColladaImportDlg.showDialog(%icon.fullPath,%icon.createCmd);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::iconPrefabAlt(%this,%icon) {
	devLog("iconPrefabAlt",%icon);

	if (Lab.currentEditor.isMethod("addFileBrowserPrefab"))
		Lab.currentEditor.addFileBrowserPrefab(%icon.fullPath,%icon.createCmd);
	else if (EWorldEditor.visible)
		eval(%icon.createCmd);

	//ColladaImportDlg.showDialog(%icon.fullPath,%icon.createCmd);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::iconImageAlt(%this,%icon) {
	devLog("iconImageAlt",%icon);
}
//------------------------------------------------------------------------------
//==============================================================================
function FileBrowser::iconUnknownAlt(%this,%icon) {
	devLog("iconUnknownAlt",%icon);
	info("Unknown file clicked! TorqueLab doesn't compute this file...",%icon.fullPath);
}
//------------------------------------------------------------------------------