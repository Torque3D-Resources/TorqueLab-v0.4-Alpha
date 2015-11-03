//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function ObjectCreatorIcon::onClick(%this) {
	devLog("ObjectCreatorIcon OnClick",%this);
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectCreator::iconFolderAlt(%this,%icon) {
	devLog("iconFolderAlt",%icon);
	ObjectCreator.navigateDown(%icon.text);
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectCreator::iconMeshAlt(%this,%icon) {
	devLog("iconMeshAlt",%icon);
	if (Lab.currentEditor.isMethod("addObjectCreatorMesh"))
		Lab.currentEditor.addObjectCreatorMesh(%icon.fullPath,%icon.createCmd);
	else if (EWorldEditor.visible)
		ColladaImportDlg.showDialog(%icon.fullPath,%icon.createCmd);

}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectCreator::iconImageAlt(%this,%icon) {
	devLog("iconImageAlt",%icon);
	

}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectCreator::iconUnknownAlt(%this,%icon) {
	devLog("iconUnknownAlt",%icon);
	info("Unknown file clicked! TorqueLab doesn't compute this file...",%icon.fullPath);

}
//------------------------------------------------------------------------------