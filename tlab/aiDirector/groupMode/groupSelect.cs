//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::selectBotGroup(%this,%obj) {
	devLog("Selecting Bot Group:",%obj);
	%this.selectedBotGroup = %obj;
	
	ADE.updateBotGroupIds();
	//syncParamArray(%this.botGroupArray,true);
}
//------------------------------------------------------------------------------

//==============================================================================
function ADE_BotGroupTree::onSelect(%this,%objID) {
	
	ADE.selectBotGroup(%objID);
	
}
//------------------------------------------------------------------------------


//==============================================================================
function ADE::addBotGroupData(%this,%obj) {
	
	
	ADE_BotGroupGeneralStack-->internalName.setText(%obj.internalName);
	ADE_BotGroupGeneralStack-->superClass.setText(%obj.superClass);
	hide(ADE_GroupSettingIDSample);
	ADE_BotGroupIDStack.clear();
	%i = 1;
	while(%obj.spawnDatablock[%i] !$= ""){
		%this.addBotGroupIdPill(%obj,%i);		
		%i++;
	}
	
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE::updateBotGroupIds(%this) {
	
	%obj = ADE.selectedBotGroup;
	ADE_BotGroupGeneralStack-->internalName.setText(%obj.internalName);
	ADE_BotGroupGeneralStack-->superClass.setText(%obj.superClass);
	ADE_BotGroupGeneralStack-->subGroupCount.setText(%obj.subGroupCount);
	hide(ADE_GroupSettingIDSample);
	ADE_BotGroupIDStack.clear();
	
	%count = %obj.subGroupCount;
	if (%count $= "" || %count < 1)
		%count = 1;
	
	for(%i=1;%i<=%count;%i++){	
		%this.addBotGroupIdPill(%obj,%i);
	}
	
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE::addBotGroupIdPill(%this,%obj,%id) {
	
	
		%datablock = %obj.spawnDatablock[%id];
		
		%pill = cloneObject(ADE_GroupSettingIDSample,"","Group_"@%id,ADE_BotGroupIDStack);
		%pill.caption = "Group #"@%id;
		
		//-------------------------------------------------------
		//Datablock MENU	
		
		%datablockMenu = %pill-->spawnDatablock;
		%datablockMenu.clear();
		foreach$(%data in AIManager.botDatablockList){
			if (!%data.isBot)
				continue;
			%datablockMenu.add(%data.getName(),%data.getId());	
			
			if (%data.getName() $= 	%datablock)
				%selected = %data.getId();
		}
		if (%datablock  $= "")
			%datablock  = "Select Bot Datablock";
		%datablockMenu.groupId = %id;
		%datablockMenu.setText(%datablock);
		
		
		//-------------------------------------------------------
		//Behavior MENU
		%behaviorMenu = %pill-->spawnBehavior;
		%behaviorMenu.clear();
		foreach(%behavior in BehaviorTreeGroup){
			%behaviorMenu.add(%behavior.getName(),%behavior.getId());
		}	
		
		
		%behaviorMenu.setText(%obj.spawnBehavior[%i]);
		%behaviorMenu.groupId = %id;
		//-------------------------------------------------------
		//Text Edits
		
		%pill-->spawnDelay.groupId = %id;
		%pill-->spawnDelay.setText(%obj.spawnDelay[%id]);
		%pill-->spawnDelay.updateFriends();
		%pill-->spawnCount.groupId = %id;
		%pill-->spawnCount.setText(%obj.spawnCount[%id]);
		%pill-->spawnCount.updateFriends();		
		
		%pill.expanded = false;
	
}
//------------------------------------------------------------------------------