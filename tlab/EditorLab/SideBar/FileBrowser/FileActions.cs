//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
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