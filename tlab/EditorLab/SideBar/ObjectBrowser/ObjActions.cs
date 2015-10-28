//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function ObjectBrowserIcon::onClick(%this) {
	devLog("ObjectBrowserIcon OnClick",%this);
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::iconFolderAlt(%this,%icon) {
	devLog("iconFolderAlt",%icon);
	ObjectBrowser.navigateDown(%icon.text);
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::iconMeshAlt(%this,%icon) {
	devLog("iconMeshAlt",%icon);
	if (Lab.currentEditor.isMethod("addObjectBrowserMesh"))
		Lab.currentEditor.addObjectBrowserMesh(%icon.fullPath,%icon.createCmd);
	else if (EWorldEditor.visible)
		ColladaImportDlg.showDialog(%icon.fullPath,%icon.createCmd);

}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::iconImageAlt(%this,%icon) {
	devLog("iconImageAlt",%icon);
	

}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::iconUnknownAlt(%this,%icon) {
	devLog("iconUnknownAlt",%icon);
	info("Unknown file clicked! TorqueLab doesn't compute this file...",%icon.fullPath);

}
//------------------------------------------------------------------------------