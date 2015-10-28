//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::createBotGroup(%this) {
	%name = getUniqueName("NewBotGroup_");
	%botGroup = newScriptObject(%name,MissionBotGroups);
	%botGroup.internalName = "NewBotGroup";	
	//%botArray = newArrayObject(%name,MissionBotGroups);
	//%botArray.internalName = "NewBotArray";	
		//%botArray.push_back("botGroup_1", "" TAB "" TAB "1" TAB "200");
  
}
//------------------------------------------------------------------------------

//==============================================================================
function ADE::addSubGroup(%this) {
	%obj = ADE.selectedBotGroup;
	%obj.dataCount++;
	%obj.botData[%obj.dataCount] = "" TAB "" TAB "" TAB "1" TAB "200";
	
	
	%this.addBotSubGroupPill(%obj,%obj.dataCount);	
  
}
//------------------------------------------------------------------------------
