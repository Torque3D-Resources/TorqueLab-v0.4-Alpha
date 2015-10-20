//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE_BotTriggerList::onSelect(%this,%triggerId,%text) {
	devLog("ADE_BotTriggerList",%triggerId,%text);
	%trigger = %text;
	if (!isObject(%trigger)){
		%trigger = MissionGroup.findObjectByInternalName(%text,true);
		if (!isObject(%trigger))
			return;
	}
	ADE.SelectTrigger(%trigger);
}
//------------------------------------------------------------------------------

//==============================================================================
function ADE::SelectTrigger(%this,%trigger) {
	ADE.selectedTrigger = %trigger;
	%botGroup = %trigger.botGroup;
	if (%botGroup $= "")
		%botGroup = "None";
	ADE_TriggerSettings-->botGroup.setText(%botGroup);
	
	%spawnObj = %trigger.spawnObj;
	if (%spawnObj $= "")
		%spawnObj = "None";
	ADE_TriggerSettings-->spawnObj.setText(%spawnObj);
	
	%triggerType = %trigger.triggerType;
	if (%triggerType $= "")
		%triggerType = $ADE_TriggerTypeText[$ADE_DefaultTriggerType];
	ADE_TriggerSettings-->triggerType.setText(%triggerType);
	ADE_TriggerSettings-->internalName.setText(%trigger.internalName);
	ADE_TriggerSettings-->groupName.setText(%trigger.groupName);
	ADE_TriggerSettings-->superClass.setText(%trigger.superClass);
}
//------------------------------------------------------------------------------
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_TriggerSettingMenu::onSelect( %this,%id,%text ) {
	logd("ADE_TriggerSettingMenu::onSelect( %this,%id,%text)",%this,%id,%text);
	%field = getWord(%this.internalName,0);
	%groupId = %this.groupId;
	%value = %this.getText();	
	if (%id $= "0")
		%value = "";
	
	if(%field $= "triggerType"){
		%value = $ADE_TriggerType[%id];
		devLog("TriggerType selected",%value);
	}
	
	if (!isObject(ADE.selectedTrigger))
		return;
	%isDirty = LabObj.set(ADE.selectedTrigger,%field,%value);
		
}
//------------------------------------------------------------------------------

//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_TriggerSettingEdit::onValidate( %this ) {
	logd("ADE_TriggerSettingMenu::onValidate( %this)",%this,%this.getText());
	%field = getWord(%this.internalName,0);
	%value = %this.getText();	
	
	if (!isObject(ADE.selectedTrigger))
		return;
	%isDirty = LabObj.set(ADE.selectedTrigger,%field,%value);
		
}
//------------------------------------------------------------------------------