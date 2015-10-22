//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$SideBarMainBook_Page[0] = "FileBrowser";
$SideBarMainBook_Page[1] = "SceneBrowser";

//==============================================================================
// Activate the interface for a plugin
//==============================================================================
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function SideBarMainBook::onTabSelected(%this,%text,%id) {
	devLog("SideBarMainBook::onTabSelected",%text,%id);
	switch(%id){
		case 0:
			Lab.initFileBrowser();	
		case 1:
			SideBarSceneFrameSet.bringToFront(SideBarScene_Tree);
			Lab.schedule(200,"initSceneBrowser");	
			
	}
}
//------------------------------------------------------------------------------




//==============================================================================
function EToolDlgDecoyArea::onMouseLeave() {
	EToolDlgCameraTypesToggle();
}
//------------------------------------------------------------------------------