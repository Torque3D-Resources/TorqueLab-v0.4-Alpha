//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Navigate through Creator Book Data
function SBP_Creator::navigatePrefabs( %this, %address ) {
	%expr = "*.prefab";
	%fullPath = findFirstFile( %expr );

	while ( %fullPath !$= "" ) {
		%fullPath = makeRelativePath( %fullPath, getMainDotCSDir() );
		%splitPath = strreplace( %fullPath, "/", " " );

		if( getWord(%splitPath, 0) $= "tools" ) {
			%fullPath = findNextFile( %expr );
			continue;
		}

		%dirCount = getWordCount( %splitPath ) - 1;
		%pathFolders = getWords( %splitPath, 0, %dirCount - 1 );
		// Add this file's path (parent folders) to the
		// popup menu if it isn't there yet.
		%temp = strreplace( %pathFolders, " ", "/" );
		%r = SBP_CreatorPopupMenu.findText( %temp );

		if ( %r == -1 ) {
			SBP_CreatorPopupMenu.add( %temp );
		}

		// Is this file in the current folder?
		if ( stricmp( %pathFolders, %address ) == 0 ) {
			%this.addPrefabIcon( %fullPath );
		}
		// Then is this file in a subfolder we need to add
		// a folder icon for?
		else {
			%wordIdx = 0;
			%add = false;

			if ( %address $= "" ) {
				%add = true;
				%wordIdx = 0;
			} else {
				for ( ; %wordIdx < %dirCount; %wordIdx++ ) {
					%temp = getWords( %splitPath, 0, %wordIdx );

					if ( stricmp( %temp, %address ) == 0 ) {
						%add = true;
						%wordIdx++;
						break;
					}
				}
			}

			if ( %add == true ) {
				%folder = getWord( %splitPath, %wordIdx );
				%ctrl = %this.findIconCtrl( %folder );

				if ( %ctrl == -1 )
					%this.addFolderIcon( %folder );
			}
		}

		%fullPath = findNextFile( %expr );
	}
}