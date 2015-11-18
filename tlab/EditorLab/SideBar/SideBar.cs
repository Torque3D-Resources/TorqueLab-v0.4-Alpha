//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function Lab::toggleSidebar(%this) {
	if (EditorSideBarCtrl.visible)
		Lab.closeSidebar();
	else
		Lab.openSidebar();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::closeSidebar(%this) {
	if (EditorSideBar.isOpen)
		EditorFrameContent.lastColumns = EditorFrameContent.columns;

	EditorSideBar.isOpen = false;
	EditorSideBarCtrl.visible = 0;
	EditorFrameContent.columns = "0 18";
	EditorFrameContent.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::openSidebar(%this) {
	EditorSideBar.isOpen = true;
	EditorSideBarCtrl.visible = 1;

	if (EditorFrameContent.lastColumns $= "")
		EditorFrameContent.lastColumns = "0 220";

	EditorFrameContent.columns = EditorFrameContent.lastColumns;
	EditorFrameContent.updateSizes();
	SideBarMainBook.selectPage($SideBarMainBook_CurrentPage);
}
//------------------------------------------------------------------------------

function EditorFrameContent::onWake(%this) {
	if (EditorSideBar.isOpen)
		Lab.schedule(500,"openSidebar");
	else
		Lab.schedule(500,"closeSidebar");
}
function EditorFrameContent::onSleep(%this) {
	if (EditorSideBar.isOpen) {
		$SideBar_CurentColumns = EditorFrameContent.columns;
		EditorFrameContent.lastColumns = EditorFrameContent.columns;
	}
}

