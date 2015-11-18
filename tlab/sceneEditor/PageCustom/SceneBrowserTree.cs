//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//SceneBrowserTree.rebuild
function SceneBrowserTree::rebuild( %this ) {
	%this.setRoot();
}

function SceneBrowserTree::setRoot( %this,%simSet ) {
	if (%simSet $= "")
		%simSet = MissionGroup;
	
	if (!isObject(%this.currentSet))
		%this.currentSet = %this;
		
	if (%simSet.getId() !$= %this.currentSet.getId()){
		%this.clear();
		%this.open(%simSet);
	}
	%this.currentSet = %simSet;
	%this.buildVisibleTree();	
}

