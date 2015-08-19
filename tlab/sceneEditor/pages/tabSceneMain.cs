//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================




//------------------------------------------------------------------------------
//==============================================================================
function SEP_ScenePage::updateContent( %this ) {
}
//------------------------------------------------------------------------------
//==============================================================================
// AUTO MISSION GROUP ORGANIZER SYSTEM
//------------------------------------------------------------------------------
// Used to clean up MissionGroup object by assigning Object class to a default
// group naming as define in settings. The Sub groups will be reordered as the
// groups are listed in below autoGroups listing
//==============================================================================

//==============================================================================
// Define Automated MissionGroup SimGroups
$SceneEd_CheckGroups = "Core Environment SceneObjects Vehicle TSStatic Spawn";
$SceneEd_AllGroups = $SceneEd_CheckGroups SPC "MiscObject";

$SceneEd_RootGroups = "Core Environment SceneObjects Shape Spawn MiscObject";

$SceneEd_MultiGroups = "Shape";
$SceneEd_SubGroups["Shape"] = "Vehicle TSStatic";

//==============================================================================
// Define Automated MissionGroup SimGroups Objects Type
$SceneEd_GroupObjList["Core"] = "MissionArea LevelInfo";
$SceneEd_GroupObjList["Environment"] = "Water Terrain River GroundCover Forest Precipitation Sky Time Cloud";
$SceneEd_GroupObjList["SceneObjects"] = "Road";
$SceneEd_GroupObjList["TSStatic"] = "TSStatic Prefab";
$SceneEd_GroupObjList["Spawn"] = "Spawn";
$SceneEd_GroupObjList["Vehicle"] = "Vehicle";
//------------------------------------------------------------------------------
//==============================================================================
// Reorganize the Mission objects (only root items by default (%maxDepth = 1)
function SEP_ScenePage::organizeMissionGroup( %this,%maxDepth ) {
	if (%maxDepth $= "")
		%maxDepth = 1;

	%this.organizeGroupDepth = %maxDepth;

	//Go through all Scene objects and move them to define groups
	foreach$(%group in $SceneEd_AllGroups)
		%this.organizedGroupList[%group] = "";

	%this.findMissionObjectsGroups(MissionGroup,1);
	
	%this.addMissionObjectsToGroups();	
	%this.removeEmptyMissionGroup();		
		
	SEP_ScenePage.reorderMissionGroup();
}
//------------------------------------------------------------------------------


//==============================================================================
function SEP_ScenePage::findMissionObjectsGroups( %this,%group,%depth ) {
	//Go through all Scene objects and move them to define groups	
	foreach(%obj in %group) {		
		if (%obj.isMemberOfClass("SimGroup")) {
			if (%depth < SEP_ScenePage.organizeGroupDepth)
				%this.organizeGroup(%obj,%depth+1);				
			continue;
		}
		%class = %obj.getClassName();
	
		%groupFound = false;
		foreach$(%groupData in $SceneEd_CheckGroups){
			%list = $SceneEd_GroupObjList[%groupData];

			if (strFindWords(%class,%list)){ 
				%this.organizedGroupList[%groupData] = strAddWord(%this.organizedGroupList[%groupData],%obj);				
				%groupFound = true;
				break;				
			}
		}
		if (!%groupFound){			
			%this.organizedGroupList["MiscObject"] = strAddWord(%this.organizedGroupList["MiscObject"],%obj);
		}
	}
	
}
//------------------------------------------------------------------------------

//==============================================================================
function SEP_ScenePage::addMissionObjectsToGroups( %this,%group ) {
	foreach$(%group in $SceneEd_AllGroups){
		%list = %this.organizedGroupList[%group];
		foreach$(%obj in %list) 
			%this.addObjToGroup(%obj,%group);
		%this.organizedGroupList[%group] = "";		
	}
	foreach$(%mainGroup in $SceneEd_MultiGroups){
		%list = $SceneEd_SubGroups[%mainGroup];
		foreach$(%subgroup in %list){
			eval("%groupName = SceneEditorCfg."@%subgroup@"Group;");
			%this.addObjToGroup(%groupName,%mainGroup);
		}			
	}
}
//------------------------------------------------------------------------------



//==============================================================================
function SEP_ScenePage::addObjToGroup( %this,%obj,%group ) {
	//Go through all Scene objects and move them to define groups
	eval("%groupName = SceneEditorCfg."@%group@"Group;");	
	if (%groupName $= "")
		return;

	if (!isObject(%groupName))
		newSimGroup(%groupName,MissionGroup);
	%groupName.add(%obj);
}
//------------------------------------------------------------------------------
//==============================================================================
//SEP_ScenePage.removeEmptyMissionGroup
function SEP_ScenePage::removeEmptyMissionGroup( %this ) {
	foreach(%obj in MissionGroup) {
		if (%obj.isMemberOfClass("SimGroup")) {
			eval("%groupName = SceneEditorCfg."@%group@"Group;");
			if (%obj.getCount() <= 0){			
				%removeGroups = strAddWord(%removeGroups,%obj.getId());
				continue;
			}
			%somethingFound = false;
			foreach(%subObj in %obj) {	
				if (%subObj.isMemberOfClass("SimGroup")) {					
					if (%subObj.getCount() <= 0){			
						%removeGroups = strAddWord(%removeGroups,%subObj.getId());					
						continue;
					}
					%somethingFound = true;
				}				
				
			}		
			if (!%somethingFound)
				%removeGroups = strAddWord(%removeGroups,%obj.getId());	
			
		}
	}
	foreach$(%group in %removeGroups){
		if (%group.getCount() <= 0){				
				delObj(%group);				
			}
	}
}
//------------------------------------------------------------------------------
//==============================================================================
//SEP_ScenePage.reorderMissionGroup
function SEP_ScenePage::reorderMissionGroup( %this ) {
	%list = SEP_ScenePage.rootGroups;
	
	%id = 0;
	foreach$(%group in $SceneEd_RootGroups){
		%beforeObj = MissionGroup.getObject(%id);
		eval("%groupName = SceneEditorCfg."@%group@"Group;");
		%groupName.orderId = %id;		
		MissionGroup.reorderChild(	%groupName,%beforeObj);		
		%id++;
	}
	SceneEditorTree.open(MissionGroup);
	SceneEditorTree.buildVisibleTree();
	
}

