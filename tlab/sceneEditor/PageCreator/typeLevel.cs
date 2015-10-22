//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Navigate through Creator Book Data
function SEP_Creator::navigateLevel( %this, %address ) {
	// Add groups to popup menu
	%array = Scene.array;
	%array.sortk();
	%count = %array.count();

	if ( %count > 0 ) {
		%lastGroup = "";

		for ( %i = 0; %i < %count; %i++ ) {
			%group = %array.getKey( %i );

			if ( %group !$= %lastGroup ) {
				CreatorPopupMenu.add( %group );

				if ( %address $= "" )
					%this.addFolderIcon( %group );
			}

			if ( %address $= %group ) {
				%args = %array.getValue( %i );
				%class = %args.val[0];
				%name = %args.val[1];
				%func = %args.val[2];
				%this.addMissionObjectIcon( %class, %name, %func );
			}

			%lastGroup = %group;
		}
	}
}