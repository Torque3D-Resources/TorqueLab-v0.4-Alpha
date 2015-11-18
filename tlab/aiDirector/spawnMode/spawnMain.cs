//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::initSpawnPage(%this) {
	AIManager.getSpawnObjects();
	ADE_SpawnObjTree.open(MissionBotSpawnSet);
	ADE_SpawnObjTree.buildVisibleTree();
	%this.initSpawnSettingsParams();
}
//------------------------------------------------------------------------------


