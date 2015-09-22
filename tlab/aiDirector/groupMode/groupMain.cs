//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::initGroupPage(%this) {
	ADE_BotGroupTree.open(MissionBotGroups);
	ADE_BotGroupTree.buildVisibleTree();
	
	//%this.initGroupSettingsParams();
}
//------------------------------------------------------------------------------

//==============================================================================
//ADE.saveCurrentGroups
function ADE::saveCurrentGroups(%this) {
	%misFile = MissionGroup.getFileName();
	%groupFile = strreplace(%misFile,".mis",".bots.cs");
	MissionBotGroups.save(%groupFile);
	
	ADE_BotGroupsTitle.setText("Bot groups");
	ADE_SaveGroupButton.active = 0;
}
//------------------------------------------------------------------------------