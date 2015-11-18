//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$LDG_ProfileSetupLoaded = false;
function Lab::initProfileSetupSystem(%this) {
	Lab.loadProfileSetupDefault();
	Lab.buildProfileSetupData();
	Lab.buildSetupTypesList();
	Lab.initProfilesSetupData(true);
	Lab.getSetupProfilesList();
	initProfileSetupParams();
	LDG_ProfilesSetupTree.init();
	Lab.scanProfilePresetData();
	$LDG_ProfileSetupLoaded = true;
}

function Lab::loadProfileSetupSystem(%this) {
	Lab.initProfileSetupSystem();
}
function Lab::rebuilProfileSetupParams(%this) {
	exec("tlab/EditorLab/gui/LabDevGui/profileSetupParams.cs");
	initProfileSetupParams();
}
function Lab::loadProfileSetupDefault(%this) {
	exec("tlab/gui/profiles/profileDefaults.cs");
}
function Lab::loadProfileSetupPreset(%this,%file) {
	if (%file $= "")
		%file = "tlab/gui/profiles/profileDefaults.cs";

	devLog("Loading file preset:",%file);

	if (!isFile(%file))
		return;

	exec(%file);
	initProfileSetupParams();
	Lab.applyProfilesSetupData();
}

//==============================================================================
// Build the Game related Profiles data list
function Lab::getSetupProfilesList(%this) {
	newSimSet("LabProfilesGroup");

	foreach( %obj in GuiDataGroup ) {
		if( !%obj.isMemberOfClass( "GuiControlProfile" ) )
			continue;

		%startCat = getSubStr(%obj.category,0,3);
		$GLab_IsGameProfile[%obj.getName()] = "";
		$GLab_IsToolProfile[%obj.getName()] = "";

		if (%startCat !$= "Gam") {
			$GLab_GameProfileList = strAddWord($GLab_GameProfileList,%obj.getName(),true);
			$GLab_IsGameProfile[%obj.getName()] = true;
		} else if (strFind(%obj.getName(),"Tools") || strFind(%obj.getName(),"Lab") || %obj.category $= "Tools" || strFind(%obj.getName(),"Inspector")) {
			$GLab_IsToolProfile[%obj.getName()] = true;
			$GLab_ToolsProfileList = strAddWord($GLab_ToolsProfileList,%obj.getName(),true);
		} else {
			$GLab_UnlistedProfileList = strAddWord($GLab_UnlistedProfileList,%obj.getName(),true);
		}

		LabProfilesGroup.add( %obj);
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// Profile Setup Preset/Save/Load Functions
//==============================================================================
//==============================================================================
function Lab::scanProfilePresetData(%this) {
	LDG_ProfilePresetMenu.clear();
	%id = 0;
	%searchFolder = "tlab/gui/profiles/presetData/*.profData.cs";
	LDG_ProfilePresetMenu.add("[Default Data]",%id);

	for(%file = findFirstFile(%searchFolder); %file !$= ""; %file = findNextFile(%searchFolder)) {
		%name = fileBase(%file);
		%name2 = fileBase(%name);
		devLog("Name:",%name,"Name2",%name2);
		LDG_ProfilePresetMenu.add(%name2,%id++);
	}

	%currentId = LDG_ProfilePresetMenu.findText(Lab.profileSetupPreset);

	if (%currentId $= "-1")
		%currentId = 0;

	LDG_ProfilePresetMenu.setSelected(%currentId,true);
}
//------------------------------------------------------------------------------
//==============================================================================
function LDG_ProfilePresetMenu::onSelect(%this,%id,%name) {
	%file = "tlab/gui/profiles/presetData/"@%name@".profData.cs";

	if (%id $= "0") {
		%file = "";
	}

	%this.updateFriends();

	if (%name $= Lab.profileSetupPreset)
		return;

	Lab.profileSetupPreset = %name;
	Lab.loadProfileSetupPreset(%file);
}

//------------------------------------------------------------------------------
//==============================================================================
function Lab::exportProfilePresetData(%this,%name) {
	if (%name $= "") {
		%name = LDG_ProfilePresetNameEdit.getText();

		if (%name $= "")
			return;

		Lab.profileSetupPreset = %name;
	}

	%file = "tlab/gui/profiles/presetData/"@%name@".profData.cs";
	export("$LabColor_*",%file);
	export("$LabFont_*",%file,true);
	Lab.scanProfilePresetData();
}
//------------------------------------------------------------------------------

//==============================================================================
// DEVELOPMENT STUFF
//==============================================================================
function exportProfileSetupStuff(%this,%file) {
	%file = "tlab/EditorLab/LabDevGui/fullExportStuff.cs";
	export("$LabColor*",%file);
	export("$LabFont*",%file,true);
}