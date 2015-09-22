//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$ADE_MainPage = 0;

//==============================================================================
function ADE::initTools(%this) {
	ADE_ModeBook.selectPage($ADE_MainPage);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function ADE_ModeBook::onTabSelected(%this,%text,%index) {
	devLog("OnTabSelected",%text,%index);
	$ADE_MainPage = %index;	
	%settingContainer = ADE_SettingsContainer.findObjectByInternalName(%text,true);
	foreach(%gui in ADE_SettingsContainer)
		hide(%gui);
	
	show(%settingContainer);
	
	if (ADE.isMethod("init"@%text@"Page"))
		eval("ADE.init"@%text@"Page();");
	
	switch$(%text){
		case "Spawn":
			AED_SpawnSettingsBook.selectPage(0);
		case "Group":
			AED_GroupSettingsBook.selectPage(0);
	}
}
//------------------------------------------------------------------------------