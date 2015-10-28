//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$ADE_MainPage = 0;
$ADE_PageName[0] = "Spawn";
$ADE_PageName[1] = "Group";
$ADE_PageName[2] = "Trigger";
//==============================================================================
function ADE::initTools(%this) {
	ADE_ModeBook.selectPage($ADE_MainPage);
	%this.initGroupPage();
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE_ModeBook::onTabSelected(%this,%text,%index) {
	%type = $ADE_PageName[%index];
	$ADE_MainPage = %index;	
	%settingContainer = ADE_SettingsContainer.findObjectByInternalName(%type,true);
	foreach(%gui in ADE_SettingsContainer)
		hide(%gui);
	
	
	show(%settingContainer);
	devLog("OnTabSelected",%text,%index,%type);
	if (ADE.isMethod("init"@%type@"Page"))
		eval("ADE.init"@%type@"Page();");
	
	switch$(%index){
		case "0":
			AED_SpawnSettingsBook.selectPage(0);
		case "1":
			AED_GroupSettingsBook.selectPage(0);
		case "2":
			AED_TriggerSettingsBook.selectPage(0);
	}
}
//------------------------------------------------------------------------------