//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TMG_Init = false;
//==============================================================================
function TerrainManagerGui::onWake(%this) {
	if (!TMG.initialized)
		TMG.init();
	
	TMG.refreshData();
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainManagerGui::onSleep(%this) {
	
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------

//==============================================================================
function TerrainManagerGui::onPreEditorSave(%this) {	
	devLog("TerrainManagerGui::onPreEditorSave",TMG_GroundCoverClone-->MainContainer);
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainManagerGui::onPostEditorSave(%this) {
	devLog("TerrainManagerGui::onPostEditorSave",TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::init(%this) {
	
	TMG.initMaterialLayersPage();
	
	TerrainManagerGui-->dataFolder.setText("");	
	TMG.initialized = true;
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::refreshData(%this) {
	hide(SEP_GroundCover);
	TMG_GroundCoverClone.add(SEP_GroundCover-->MainContainer);
	TMG.updateTerrainList();
	%this.refreshMaterialLayersPage();
	%this.buildTerrainInfoParams();
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::getActiveFolders(%this) {
	%dataFolder = MissionGroup.dataFolder;
	if (%dataFolder $= "")
		%dataFolder = filePath(TMG.activeTerrain.terrainFile);
	
	TMG.setFolder("data",%dataFolder);
	
	%sourceFolder = MissionGroup.sourceFolder;
	if (%sourceFolder $= "")
		%sourceFolder = %dataFolder;	
	TMG.setFolder("source",%sourceFolder);
	
	%targetFolder = MissionGroup.targetFolder;
	if (%targetFolder $= "")
		%targetFolder = %dataFolder;	
	TMG.setFolder("target",%targetFolder);
	
	TMG.setFolder("source",MissionGroup.sourceSubFolder,true);
	TMG.setFolder("target",MissionGroup.targetSubFolder,true);	
	
	TerrainManagerGui-->dataFolder.setText(TMG.dataFolder);	
	TMG_PageMaterialLayers-->textureMapFolder.setText(TMG.sourceFolder);
	TMG_PageMaterialLayers-->relativeMapFolder.setText(TMG.sourceSubFolder);
	TMG_PageMaterialLayers-->textureTargetFolder.setText(TMG.targetFolder);
	TMG_PageMaterialLayers-->relativeTargetFolder.setText(TMG.targetSubFolder);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setFolder(%this,%type,%folder,%relativeToData,%onlyTMG) {
	%oldSource = TMG.sourceFolder;
	
	if (%relativeToData){
		devLog("Relative folder:",%type,"Folder",%folder);
		%subFolder = %folder;
		%folder = TMG.dataFolder@"/"@%subFolder;
		%subField = %type@"SubFolder";
		eval("TMG."@%subField@" = %subFolder;");
		if (!%onlyTMG){
			MissionGroup.setFieldValue(%subField,%subFolder);
			TMG.activeTerrain.setFieldValue(%subField,%subFolder);
		}
		
	}	
	
	%field = %type@"Folder";
	eval("TMG."@%field@" = %folder;");
	
	if (!%onlyTMG){
		MissionGroup.setFieldValue(%field,%folder);
		TMG.activeTerrain.setFieldValue(%field,%folder);
	}

	devLog("Old source:",%oldSource,"New source",TMG.sourceFolder);
	//Update map layers data if source changed
	if (%type $= "Source" && %oldSource !$= TMG.sourceFolder)
		TMG.updateMaterialLayers();
}
//------------------------------------------------------------------------------


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
		TMG.activeHeightRange = TMG.activeHeightInfo.x SPC "\c1(\c2"@TMG.activeHeightInfo.y@"\c1/\c2"@TMG.activeHeightInfo.z@"\c1)";
		
		TMG_HeightmapOptions-->heightScale.setText(TMG.activeHeightInfo.x);
		TMG_HeightmapOptions-->squareSize.setText(%terrainId.squareSize);
		
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

//==============================================================================
// Terrain Data Folder (used as base for exporting)
//==============================================================================
//==============================================================================
function TMG::selectDataFolder( %this, %filter) {
	
	%currentFile = MissionGroup.getFilename();
	%dlg = new OpenFolderDialog() {
		Title = "Select Export Folder";
		Filters = %filter;
		DefaultFile = %currentFile;
		ChangePath = false;
		MustExist = true;
		MultipleFiles = false;
	};

	if(filePath( %currentFile ) !$= "")
		%dlg.DefaultPath = filePath(%currentFile);
	else
		%dlg.DefaultPath = getMainDotCSDir();

	if(%dlg.Execute())
		TMG.setDataFolder(%dlg.FileName);
		
	%dlg.delete();
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setDataFolder( %this, %path ) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Not active terrain detected. Please select one before setting data folder:",%terObj);
		return;
	}
	%path =  makeRelativePath( %path, getMainDotCsDir() );
	TerrainManagerGui-->dataFolder.setText(%path);	
	%this.setFolder("data",%path);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::getDefaultFolder(%this) {
	if (isObject(TMG.activeTerrain)){
		if (TMG.activeTerrain.dataFolder !$= "")
			return TMG.activeTerrain.dataFolder;
	}
	
	if (TerrainManagerGui-->dataFolder.text !$= "")		
		return TerrainManagerGui-->dataFolder.text;
	
	%folder = filePath(MissionGroup.getFilename());
		return %folder;	
	
}
//------------------------------------------------------------------------------
