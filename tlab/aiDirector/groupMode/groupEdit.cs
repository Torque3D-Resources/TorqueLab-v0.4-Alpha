//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$ADE_BotGroupField["SpawnDatablock"] = 0;
$ADE_BotGroupField["SpawnBehavior"] = 1;
$ADE_BotGroupField["SpawnCount"] = 2;
$ADE_BotGroupField["SpawnDelay"] = 3;
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE::updateBotGroupField( %this,%field,%value,%groupId ) {
	devLog("ADE::updateBotGroupField( %this,%field,%value,%groupId)",%this,%field,%value,%groupId);
	
	%obj = ADE.selectedBotGroup;
	if (!isObject(%obj)){
		warnLog("Invalid BotGroup");
		return;
	}	
	if($ADE_BotGroupField[%field] !$= ""){ 
		%fieldId = $ADE_BotGroupField[%field];
		%initialData = %obj.botData[%groupId]; 
		%newData = setField(%initialData,%fieldId,%value);
		
		devLog("BotData changed from:",%initialData,"To",%newData);
		%isDirty = LabObj.set(%obj,"botData",%newData,%groupId);
	}
	else	
		%isDirty = LabObj.set(%obj,%field,%value,%groupId);
	
	if (!%isDirty)
		return;

	ADE_SaveGroupButton.active = 1;
	ADE_BotGroupsTitle.setText("Bot groups *");
	
	if (%field $= "subGroupCount"){
		ADE.updateBotGroupIds();
	}
		
}
//------------------------------------------------------------------------------

//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_GroupSettingEdit::onValidate( %this ) {
	devLog("ADE_GroupSettingEdit::onValidatefdf( %this)",%this);
	
	%field = getWord(%this.internalName,0);
	%groupId = %this.groupId;
	%value = %this.getText();
	
		ADE.updateBotGroupField(%field,%value,%groupId);
}
//------------------------------------------------------------------------------

//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_GroupSettingSlider::onMouseDragged( %this ) {
	logd("ADE_GroupSettingSlider::onMouseDragged( %this)",%this);
	%this.updateFriends();	

}
//------------------------------------------------------------------------------
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_GroupSettingSlider::onSliderChanged( %this ) {
	devLog("ADE_GroupSettingSlider::onSliderChanged( %this)",%this,%this.getValue());
	%field = getWord(%this.internalName,0);
	%groupId = %this.groupId;
	
	
		ADE.updateBotGroupField(%field,%this.getValue(),%groupId);
	
	%this.updateFriends();	
}
//------------------------------------------------------------------------------
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE_GroupSettingMenu::onSelect( %this,%id,%text ) {
	logd("ADE_GroupSettingMenu::onSelect( %this,%id,%text)",%this,%id,%text);
	%field = getWord(%this.internalName,0);
	%groupId = %this.groupId;
	%value = %this.getText();
	
		ADE.updateBotGroupField(%field,%value,%groupId);
}
//------------------------------------------------------------------------------

