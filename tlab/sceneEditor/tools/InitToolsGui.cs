//==============================================================================
// TorqueLab -> SceneEditor Inspector script
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

//==============================================================================
// Prefabs
//==============================================================================
function SceneEd::initToolsGui( %this ) {
	SETools_CreateOptClassMenu.clear();
	SETools_CreateOptClassMenu.add("Zone",0);
	SETools_CreateOptClassMenu.add("Portal",1);
	SETools_CreateOptClassMenu.add("OcclusionVolume",2);
	SETools_CreateOptClassMenu.setText("Zone");
}

