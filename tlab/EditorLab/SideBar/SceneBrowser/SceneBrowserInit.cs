//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$SceneBrowserMenu_ActivePage = 0;
//==============================================================================
//FileBrowserTree.initFiles
function Lab::initSceneBrowser( %this ) {
	
	SceneBrowserMenu.clear();
	SceneBrowserMenu.add("Show plugin objects",0);
	SceneBrowserMenu.add("Show all objects",1);
	SceneBrowserMenu.add("Show custom classes",2);
	SceneBrowserMenu.setSelected($SceneBrowserMenu_ActivePage);
	SceneBrowserTree.rebuild();
}
function SceneBrowserMenu::onSelect( %this,%id,%text ) {
	devLog("SceneBrowserMenu::onSelect( %this,%id,%text )",%this,%id,%text );	
	$SceneBrowserMenu_ActivePage = %id;
	switch$(%id){
		case "2":
			ETools.showTool("SceneTreeClasses");
			
	}
	
}



