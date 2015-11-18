//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::initGroupPage(%this) {
	ADE_BotGroupTree.open(MissionBotGroups);
	ADE_BotGroupTree.buildVisibleTree();
	//-------------------------------------------------------
	//Spwn MENU
	%spawnMenu = ADE_BotGroupGeneralStack-->defaultSpawn;
	%spawnMenu.clear();
	%spawnText = "None";
	%spawnMenu.add(%spawnText,0);
	%list = getMissionObjectClassList("BotSpawnSphere");

	foreach$(%spawn in %list) {
		%name = %spawn.internalName;

		if (%name $= "") {
			devLog("Spawn object skipped because no internalName set");
			continue;
		}

		%spawnMenu.add(%name,%spawn.getId());
	}

	%spawnMenu.setText(%spawnText);
	//%this.initGroupSettingsParams();
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE::setGroupDirty(%this) {
	ADE_SaveGroupButton.active = 1;
	ADE_SaveGroupButton2.active = 1;
	ADE_BotGroupsTitle.setText("Bot groups *");
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE::setGroupNotDirty(%this) {
	ADE_SaveGroupButton.active = 0;
	ADE_SaveGroupButton2.active = 0;
	ADE_BotGroupsTitle.setText("Bot groups");
}
//------------------------------------------------------------------------------
//==============================================================================
//ADE.saveCurrentGroups
function ADE::saveCurrentGroups(%this) {
	%misFile = MissionGroup.getFileName();
	%groupFile = strreplace(%misFile,".mis",".bots.cs");
	MissionBotGroups.save(%groupFile);
	ADE.setGroupNotDirty();
}
//------------------------------------------------------------------------------

//==============================================================================
//ADE.saveCurrentGroups
function ADE::deleteCurrentGroups(%this) {
	%selectList = ADE_BotGroupTree.getSelectedObjectList();

	if (%selectList $= "")
		return;

	foreach$(%obj in %selectList)
		delObj(%obj);

	ADE_SaveGroupButton.active = 1;
}
//------------------------------------------------------------------------------
//==============================================================================
//ADE.saveCurrentGroups
function ADE::deleteSubGroupId(%this,%id) {
	//labMsgok("Not supported yet","You can't delete sub group "@%id@" for now, just delete the group and start a new one... Or edit manually the file");
	//Simply rebuild the object
	%group = ADE.selectedBotGroup;
	%intName = %group.internalName;
	%name = %group.getName();
	%group.setName("");
	%newGroup = newScriptObject(%name);

	for(%i=1; %i<=%group.datacount; %i++) {
		if (%i $= %id) {
			devLog("Skip deleted data",%group.botData[%i]);
			continue;
		}

		%newGroup.botData[%newId++] = %group.botData[%i];
		devLog("New:",%newGroup,"Data",%newGroup.botData[%newId],"Old",%group,"Data",%group.botData[%i]);
	}

	%newGroup.datacount = %newId;
	MissionBotGroups.add(%newGroup);
	MissionBotGroups.reorderChild	(%newGroup,%group);
	delObj(%group);
	%newGroup.internalName = %intName;
	ADE.selectBotGroup(%newGroup);
	ADE.setGroupDirty();
}
//------------------------------------------------------------------------------
