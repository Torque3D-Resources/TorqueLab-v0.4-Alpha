//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function TMG::setActiveTerrain(%this,%terrainId) {
	
	if (!isObject(%terrainId)){
		TMG.activeTerrain = "";
		TMG.activeHeightInfo = "";
	}
	TMG.activeTerrain = %terrainId;
	TMG_ActiveTerrainMenu.setText(TMG.activeTerrain.getName());
	%this.getActiveFolders();
	
	TMG_PageMaterialLayers-->heightmapModeStack-->Current.visible = !%terrainId.isNew;
	//TMG_MaterialLayersNewTerrain.visible = %terrainId.isNew;
	if (%terrainId.isNew){		
		if (TMG.heightmapMode $= "Current")
			TMG.changeHeightmapMode("","Source");	
				
		%terrainName = getUniqueName("theTerrain");		
	}
	else {
		TMG.changeHeightmapMode("","Current");	
		%terrainName = %terrainId.getName();	
		ETerrainEditor.attachTerrain(%terrainId);
		
		TMG.activeHeightInfo = ETerrainEditor.getHeightRange();
		TMG.activeHeightRangeInfo = TMG.activeHeightInfo.x SPC "\c1(\c2"@TMG.activeHeightInfo.y@"\c1/\c2"@TMG.activeHeightInfo.z@"\c1)";
		
		TMG.activeHeightRange = "\c2"@TMG.activeHeightInfo.x;
		TMG.activeHeightMin = "\c3"@TMG.activeHeightInfo.y;
		TMG.activeHeightMax = "\c4"@TMG.activeHeightInfo.z;
		TMG_HeightmapOptions-->heightScale.setText(TMG.activeHeightInfo.x);
		TMG_HeightmapOptions-->squareSize.setText(%terrainId.squareSize);
		
		TMG.activeTexturesCount = ETerrainEditor.getNumTextures();
		TMG.infoTexturesCount = "All terrains textures used:\c2" SPC TMG.activeTexturesCount;
		TMG.infoTexturesActive = "Active terrain textures used:\c2" SPC getRecordCount(ETerrainEditor.getMaterials());
		TMG.infoBlockCount = "Total terrain blocks:\c2" SPC ETerrainEditor.getTerrainBlockCount();
		TMG.infoBlockList = "Terrain blocks list:\c2" SPC ETerrainEditor.getTerrainBlocksMaterialList();
		
		TMG_PageGeneral-->storeFolders.setStateOn(%terrainId.storeFolders);
	
		TMG.infoActions1 = "Totals actions:\c2" SPC ETerrainEditor.getActionName(1);
		syncParamArray(TMG.terrainArray);	
		
		TMG_PageMaterialLayers-->terrainX.setText(%terrainId.position.x);
		TMG_PageMaterialLayers-->terrainY.setText(%terrainId.position.y);
		TMG_PageMaterialLayers-->terrainZ.setText(%terrainId.position.z);
	}
	%this.validateImportTerrainName(%terrainName);
	%this.updateTerrainLayers();
	if (ETerrainEditor.getMaterials() $= "")
		TMG.schedule(1000,"updateMaterialLayers");
	else
		%this.updateMaterialLayers();
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::updateTerrainList(%this,%selectCurrent) {
	TMG_ActiveTerrainMenu.clear();
	
	%list = getMissionObjectClassList("TerrainBlock");
	foreach$(%terrain in %list){
		TMG_ActiveTerrainMenu.add(%terrain.getName(),%terrain.getId());
	}
	TMG_ActiveTerrainMenu.add("New terrain",0);
	
	if (!%selectCurrent)
		return;
	if (!isObject(TMG.activeTerrain))
		TMG.activeTerrain = getWord(%list,0);	

	TMG.setActiveTerrain(TMG.activeTerrain);
	//TMG_ActiveTerrainMenu.setSelected(TMG.activeTerrain.getId());
	
}
//------------------------------------------------------------------------------


//==============================================================================
function TMG_ActiveTerrainMenu::onSelect(%this,%id,%text) {
	
	if (%id $= "0"){
		TMG.activeTerrain = newScriptObject("TMG_NewTerrain");
		TMG_NewTerrain.isNew = true;
		TMG_NewTerrain.dataFolder = 
		%id = TMG.activeTerrain.getId();
	}
		
	if (!isObject(%id))
	return;
	
	TMG.setActiveTerrain(%id);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::storeFolderToTerrain(%this,%storeToTerrain) {	
	if (!isObject(TMG.ActiveTerrain))
		return;

	if (!%storeToTerrain && TMG.ActiveTerrain.getFieldValue("storeFolders") $= "")
		return;		
		
	TMG.ActiveTerrain.setFieldValue("storeFolders",%storeToTerrain);	
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Export Single Layer Map
//==============================================================================

//==============================================================================
function TMG::saveTerrain(%this,%obj,%file) {	
	if (%obj $= "")
		%obj = TMG.activeTerrain;
		
	if (!isObject(%obj))
		return;
	if (%file $= "")
		%file = addFilenameToPath(filePath(MissionGroup.getFileName()),%obj.getName(),"ter");	
	
	%obj.save(%file);
	TMG.schedule(500,"updateTerrainFile",%obj,%file);
	devLog("Terrain:",%obj.getName(),"Saved to",%file);	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::updateTerrainFile(%this,%obj,%file) {	
	if (!isObject(%obj))
		return;
	%obj.setFieldValue("terrainFile",%file);
	devLog("Terrain:",%obj.getName(),"terrainFileSet to",%file);	
}
//------------------------------------------------------------------------------
