//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function TMG::setActiveTerrain(%this,%terrainId) {
	info("Active terrain is:",%terrainId.getName());
	if (!isObject(%terrainId)){
		TMG.activeTerrain = "";
		TMG.activeHeightInfo = "";
	}
	TMG.activeTerrain = %terrainId;
	
	%this.getActiveFolders();
	
	TMG_PageMaterialLayers-->heightmapModeStack-->Current.visible = !%terrainId.isNew;
	//TMG_MaterialLayersNewTerrain.visible = %terrainId.isNew;
	if (%terrainId.isNew){		
		if (TMG.heightmapMode $= "Current")
			TMG.changeHeightmapMode("","Source");
		
		%terrainName = getUniqueName("theTerrain");
		
	}
	else {	
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
	
		TMG.infoActions1 = "Totals actions:\c2" SPC ETerrainEditor.getActionName(1);
		syncParamArray(TMG.terrainArray);	
	}
	%this.validateImportTerrainName(%terrainName);
	%this.updateTerrainLayers();
	TMG.updateMaterialLayers();
	
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::updateTerrainList(%this) {
	TMG_ActiveTerrainMenu.clear();
	
	%list = getMissionObjectClassList("TerrainBlock");
	foreach$(%terrain in %list){
		TMG_ActiveTerrainMenu.add(%terrain.getName(),%terrain.getId());
	}
	TMG_ActiveTerrainMenu.add("New terrain",0);
	if (TMG.activeTerrain $= "" || !isObject(TMG.activeTerrain))
		TMG.activeTerrain = getWord(%list,0).getId();
	
	TMG_ActiveTerrainMenu.setSelected(TMG.activeTerrain);
	
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