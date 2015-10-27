//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$SceneBrowser_PluginClasses["ForestEditor"] = "Forest";
$SceneBrowser_PluginClasses["RoadEditor"] = "DecalRoad";
$SceneBrowser_PluginClasses["MeshRoadEditor"] = "MeshRoad";
$SceneBrowser_PluginClasses["RiverEditor"] = "River";
$SceneBrowser_PluginClasses["MaterialEditor"] = "TSStatic Player MeshRoad DecalRoad";
$SceneBrowser_PluginClasses["DatablockEditor"] = "All" TAB "ScatterSky";
$SceneBrowser_PluginClasses["NavEditor"] = "NavMesh";
$SceneBrowser_PluginClasses["TerrainEditor"] = "TerrainBlock";
$SceneBrowser_PluginClasses["TerrainPainter"] = "TerrainBlock";
$SceneBrowser_PluginClasses["ShapeEditor"] = "TSStatic MeshRoad DecalRoad";
//SceneBrowserTree.rebuild
function SceneBrowser::filterPlugin( %this,%pluginObj ) {
	if (!isObject(%pluginObj))
		%pluginObj = Lab.currentEditor;
	
	%classes = $SceneBrowser_PluginClasses[%pluginObj.plugin];	
	%this.filterClasses(%classes);
}



function SceneBrowser::filterClasses( %this,%classes ) {
	%set = "";
	if (%classes !$= "" && %classes !$= "All"){
			%set = SceneBrowserObjSet;
		if (!isObject(%set))
			%set = newSimSet("SceneBrowserObjSet");
		else 
			%set.clear();
			
		%objList = getMissionObjectClassList(%classes);
		
		foreach$(%obj in %objList)
			%set.add(%obj);
		
		devLog("ObjList added to set:",%objList);
		%set.outputlog();
	}
	
	SceneBrowserTree.setRoot(%set);
	
	/* Not Working, need to use a set
	SceneBrowserTree.noCallbacks = true;
	for(%i = 1; %i< SceneBrowserTree.getItemCount();%i++){
		%obj = SceneBrowserTree.getItemValue(%i);
		
		if (%classes $= "" || strFind(%classes,%obj.getClassName()))
			%showList = strAddWord(%showList,%i);
		else	
			%hideList = strAddWord(%hideList,%i);
	}
	SceneBrowserTree.clearSelection();
	%count = getWordCount(%showList);
	%last = false;
	foreach$(%item in %showList){
		%id++;
		if (%id >=%count){
			devLog("Last of",%showList,"Is",%item);
			%last = true;
		}
		SceneBrowserTree.addSelection(%item,%last);
	}
	devLog("Showing item count:",SceneBrowserTree.getSelectedItemsCount());
	SceneBrowserTree.hideSelection(false);
	
	SceneBrowserTree.clearSelection();
	%count = getWordCount(%hideList);
	%last = false;
	foreach$(%item in %hideList){
		%id++;
		if (%id >=%count)
			%last = true;
		SceneBrowserTree.addSelection(%item,%last);
	}
	devLog("Hiding item count:",SceneBrowserTree.getSelectedItemsCount());
	SceneBrowserTree.hideSelection(true);
	SceneBrowserTree.noCallbacks = false;*/
}