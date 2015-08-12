//==============================================================================
// GameLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//-----------------------------------------------------------------------------
// Load up our main GUI which lets us see the game.

//============================================================================
//Taking screenshot
function ActionMap::OnAdd(%this) {
	devLog("ActionMap::OnAdd(%this)",%this);
}
//----------------------------------------------------------------------------

//============================================================================
//Taking screenshot
function getActionMapList(%filter) {
	foreach(%map in ActionMapGroup){
		if(%filter !$="" && !strFind(%map.getName(),%filter))
			continue;
		%mapList = strAddWord(%mapList,%map.getName());
	}
	return %mapList;
}
//----------------------------------------------------------------------------
