//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================

function Scene::hideGroupChilds( %this, %simGroup ) {
	devLog("hideGroupChilds",%simGroup);
	foreach( %child in %simGroup ) {
		if( %child.isMemberOfClass( "SimGroup" ) )
			%this.hideGroupChilds( %child );
		else if (!%child.hidden)
			%child.setHidden( true );
	}
}
//------------------------------------------------------------------------------
function Scene::showGroupChilds( %this, %simGroup ) {
	devLog("showGroupChilds",%simGroup);
	foreach( %child in %simGroup ) {
		if( %child.isMemberOfClass( "SimGroup" ) )
			%this.showGroupChilds( %child );
		else if (%child.hidden)
			%child.setHidden( false );

	}
}
//------------------------------------------------------------------------------

function Scene::toggleHideGroupChilds( %this, %simGroup ) {
	devLog("toggleHideGroupChilds",%simGroup);
	foreach( %child in %simGroup ) {
		if( %child.isMemberOfClass( "SimGroup" ) )
			%this.toggleHideGroupChilds( %child );
		else
			%child.setHidden( !%child.hidden );
	}
}
//------------------------------------------------------------------------------