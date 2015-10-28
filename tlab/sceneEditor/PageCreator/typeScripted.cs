//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Navigate through Creator Book Data
function SEP_Creator::navigateScripted( %this, %address )
{
	%category = getWord( %address, 1 );
	%dataGroup = "DataBlockGroup";

	for ( %i = 0; %i < %dataGroup.getCount(); %i++ )
	{
		%obj = %dataGroup.getObject(%i);
		// echo ("Obj: " @ %obj.getName() @ " - " @ %obj.category );

		if ( %obj.category $= "" && %obj.category == 0 )
			continue;

		// Add category to popup menu if not there already
		if ( CreatorPopupMenu.findText( %obj.category ) == -1 )
			CreatorPopupMenu.add( %obj.category );

		if ( %address $= "" )
		{
			%ctrl = %this.findIconCtrl( %obj.category );

			if ( %ctrl == -1 )
			{
				%this.addFolderIcon( %obj.category );
			}
		}
		else if ( %address $= %obj.category )
		{
			%ctrl = %this.findIconCtrl( %obj.getName() );

			if ( %ctrl == -1 )
				%this.addShapeIcon( %obj );
		}
	}
}