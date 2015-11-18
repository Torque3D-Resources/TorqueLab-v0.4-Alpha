//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$SceneBrowserMenu_ActivePage = 0;
$SceneBrowserClassesId["1"] = "Zone Portal OcclusionVolume";
//==============================================================================
//FileBrowserTree.initFiles
function Lab::initSceneBrowser( %this ) {
	SceneBrowserTree.currentSet = "";
	SceneCustomPresetMenu.clear();
	SceneCustomPresetMenu.add("Custom Classes Filter",0);
	SceneCustomPresetMenu.add("Occluders",1);
	SceneCustomPresetMenu.setSelected($SceneBrowserMenu_ActivePage);
	SceneBrowserTree.rebuild();
}
function SceneCustomPresetMenu::onSelect( %this,%id,%text ) {	
	$SceneBrowserMenu_ActivePage = %id;
	
	%classes = $SceneBrowserClassesId[%id];
	SceneBrowserTreeFilter.setText(%classes);
	
	SceneBrowserTreeFilter.onValidate();
	
}

function SceneBrowserTreeFilter::onValidate( %this ) {	
	devLog("SceneBrowserTreeFilter onValidate",%this.getText());
	%classes =%this.getText();	
	SceneEd.filterClasses(%classes);	
	
}

