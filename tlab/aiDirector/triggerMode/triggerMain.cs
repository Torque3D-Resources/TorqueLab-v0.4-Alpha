//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$ADE_TriggerTypeText["1"] = "Once per game";
$ADE_TriggerTypeText["2"] = "Always - All bots";
$ADE_TriggerTypeText["3"] = "Always - Dead only";
$ADE_TriggerTypeText["4"] = "Once per player";

$ADE_TriggerType["1"] = "Once";
$ADE_TriggerType["2"] = "AlwaysAll";
$ADE_TriggerType["3"] = "AlwaysDead";
$ADE_TriggerType["4"] = "OncePlayer";
$ADE_DefaultTriggerType = "1";

//==============================================================================
function ADE::initTriggerPage(%this) {
	ADE_BotTriggerList.clearItems();
	%triggerList = getMissionObjectClassList("Trigger");

	foreach$(%trigger in %triggerList) {
		if (%trigger.dataBlock !$= "AiTrigger")
			continue;

		%name = %trigger.internalName;

		if (%name $= "")
			%name=%trigger.getName();

		ADE_BotTriggerList.insertItem(%name,%trigger.getId());
	}

	%list = getMissionObjectClassList("BotSpawnSphere");
	%spawnMenu = ADE_TriggerSettings-->spawnObj;
	%spawnMenu.clear();
	%spawnMenu.add("None",0);

	foreach$(%spawn in %list) {
		%name = %spawn.internalName;

		if (%name $= "")
			%name = %spawn.getName();

		%spawnMenu.add(%name,%spawn.getId());
	}

	%spawnMenu.setText("None");
	//---------------------------------------------------------
	// BotGroups Menu
	%botMenu = ADE_TriggerSettings-->botGroup;
	%botMenu.clear();
	%botMenu.add("None",0);

	foreach(%group in MissionBotGroups) {
		%name = %group.internalName;

		if (%name $= "")
			%name = %group.getName();

		%botMenu.add(%name,%group.getId());
	}

	%botMenu.setText("None");
	//---------------------------------------------------------
	// TriggerTypes Menu
	%typeMenu = ADE_TriggerSettings-->triggerType;
	%typeMenu.clear();
	$ADE_TriggerTypeText["0"] = "Once per player";
	%typeMenu.add(	"Default(Once)",0);
	%typeMenu.add(	$ADE_TriggerTypeText["1"],1);
	%typeMenu.add(	$ADE_TriggerTypeText["2"],2);
	%typeMenu.add(	$ADE_TriggerTypeText["3"],3);
	%typeMenu.add(	$ADE_TriggerTypeText["4"],4);
	%typeMenu.setText($ADE_TriggerType["1"]);
}
//------------------------------------------------------------------------------


