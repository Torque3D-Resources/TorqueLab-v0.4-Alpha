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
function ADE::updateBotGroupIds(%this) {
	
	%obj = ADE.selectedBotGroup;
	ADE_BotGroupGeneralStack-->internalName.setText(%obj.internalName);
	ADE_BotGroupGeneralStack-->superClass.setText(%obj.superClass);
	ADE_BotGroupGeneralStack-->subGroupCount.setText(%obj.subGroupCount);
	hide(ADE_GroupSettingIDSample);
	ADE_BotGroupIDStack.clear();	
	
	
	%count = %obj.dataCount;	
	for(%i=1;%i<=%count;%i++){	
		%this.addBotSubGroupPill(%obj,%i);
	}
	%this.addSubGroup();
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE::addBotSubGroupPill(%this,%obj,%id) {
	if (%obj $= "")
		%obj = ADE.selectedBotGroup;
	
		%groupName = "botGroup_"@%id;
		
		%fields = %obj.botData[%id];
		%datablock = getField(%fields,0);
		%behavior = getField(%fields,1);
		%count = getField(%fields,2);
		%delay = getField(%fields,3);
		if (%datablock $= "")
			%datablock = "Select a datablock";
		if (%behavior $= "")
			%behavior = "Select a behavior";
		if (%count $= "")
			%count = "1";
		if (%delay $= "")
			%delay = "200";
		
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
		
		%datablockMenu.groupId = %id;
		%datablockMenu.setText(%datablock);
		
		
		//-------------------------------------------------------
		//Behavior MENU
		%behaviorMenu = %pill-->spawnBehavior;
		%behaviorMenu.clear();
		foreach(%behavior in BehaviorTreeGroup){
			%behaviorMenu.add(%behavior.getName(),%behavior.getId());
		}	
		
		
		%behaviorMenu.setText(%behavior);
		%behaviorMenu.groupId = %id;
		//-------------------------------------------------------
		//Text Edits
		
		%pill-->spawnDelay.groupId = %id;
		%pill-->spawnDelay.setText(%delay);
		%pill-->spawnDelay.updateFriends();
		%pill-->spawnCount.groupId = %id;
		%pill-->spawnCount.setText(%count);
		%pill-->spawnCount.updateFriends();		
		%pill.expanded = false;
	
}
//------------------------------------------------------------------------------
