//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Build the Game related Profiles data list
function getSetupProfilesList() {
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
function LDG_ProfilesSetupTree::init( %this ) {	
	if (!isObject(LabProfilesGroup))
		getSetupProfilesList();
		
	%this.clear();	

	foreach( %obj in LabProfilesGroup ) {
		// Create a visible name.
		%name = %obj.getName();

		if( %name $= "" )
			continue;		

		%group = %this.findChildItemByName( 0, %obj.category );

		if( !%group )
			%group = %this.insertItem( 0, %obj.category );

		// Insert the item.
		%id = %this.insertItem( %group, %name, %obj.getId(), "" );
	}

	%this.sort( 0, true, true, false );
	%this.schedule(50,"buildVisibleTree");
}
//==============================================================================
