//==============================================================================
// HelpersLab -> MissionGroup related helpers
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Update the GuiControl data field depending of the Class
//==============================================================================

//==============================================================================
// SEP_GroundCover.getMissionGroundCover();
function getMissionObjectClassList( %class ) {	
	%list = checkMissionSimGroupForClass(MissionGroup,%class);
	devLog("MissonObj List for class:",%class,"=",%list);
	return %list;
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function checkMissionSimGroupForClass(%group,%class ) {
	foreach(%obj in %group) {
		if (%obj.getClassname() $= %class) {
			%list = strAddWord(%list,%obj.getId());
		} else if (%obj.getClassname() $= "SimGroup") {
			%listAdd = checkMissionSimGroupForClass(%obj,%class);
			devLog("Adding SubList:",%listAdd,"To MainList",%list);
			%list = strAddWord(%list,%listAdd);
			
		}
	}
	return %list;
}
//------------------------------------------------------------------------------


//==============================================================================
// Update the GuiControl data field depending of the Class
//==============================================================================
//----------------------------------------------------------------------------
// A function used in order to easily parse the MissionGroup for classes . I'm pretty
// sure at this point the function can be easily modified to search the any group as well.
function parseMissionGroup( %className, %childGroup ) {
	if( getWordCount( %childGroup ) == 0)
		%currentGroup = "MissionGroup";
	else
		%currentGroup = %childGroup;

	for(%i = 0; %i < (%currentGroup).getCount(); %i++) {
		if( (%currentGroup).getObject(%i).getClassName() $= %className )
			return true;

		if( (%currentGroup).getObject(%i).getClassName() $= "SimGroup" ) {
			if( parseMissionGroup( %className, (%currentGroup).getObject(%i).getId() ) )
				return true;
		}
	}
}

// A variation of the above used to grab ids from the mission group based on classnames
function parseMissionGroupForIds( %className, %childGroup ) {
	if( getWordCount( %childGroup ) == 0)
		%currentGroup = "MissionGroup";
	else
		%currentGroup = %childGroup;

	for(%i = 0; %i < (%currentGroup).getCount(); %i++) {
		if( (%currentGroup).getObject(%i).getClassName() $= %className )
			%classIds = %classIds @ (%currentGroup).getObject(%i).getId() @ " ";

		if( (%currentGroup).getObject(%i).getClassName() $= "SimGroup" )
			%classIds = %classIds @ parseMissionGroupForIds( %className, (%currentGroup).getObject(%i).getId());
	}
	return %classIds;
}

