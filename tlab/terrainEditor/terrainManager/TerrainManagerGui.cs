//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TMG_Init = false;
//==============================================================================
function TerrainManagerGui::onWake(%this) {
	if (!$TMG_Init)
		TMG.init();
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainManagerGui::onSleep(%this) {
	
}
//------------------------------------------------------------------------------


//==============================================================================
function TMG::init(%this) {
	TMG.updateTerrainList();
	%this.initMaterialLayersPage();
	%this.buildTerrainInfoParams();
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
	
	TMG.activeHeightInfo = ETerrainEditor.getHeightRange();
	TMG.activeHeightRange = TMG.activeHeightInfo.x SPC "\c1(\c2"@TMG.activeHeightInfo.y@"\c1/\c2"@TMG.activeHeightInfo.z@"\c1)";
	%this.updateTerrainLayers();
	TMG.updateMaterialLayers();
	/*
	%terrainSettingObj = MissionGroup.findObjectByInternalName(%terrainId.getName()@"_Settings",true);
	if (!isObject(%terrainSettingObj)){
		%terrainSettingObj = newScriptObject(%terrainId.getName()@"_Settings",%terrainId.parentGroup);
		%terrainSettingObj.terrainName = %terrainId.getName();		
	}
	*/
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::updateTerrainList(%this) {
	TMG_ActiveTerrainMenu.clear();
	
	%list = getMissionObjectClassList("TerrainBlock");
	foreach$(%terrain in %list){
		TMG_ActiveTerrainMenu.add(%terrain.getName(),%terrain.getId());
	}
	if (TMG.activeTerrain $= "" || !isObject(TMG.activeTerrain))
		TMG.activeTerrain = getWord(%list,0).getId();
	
	TMG_ActiveTerrainMenu.setSelected(TMG.activeTerrain);
	
}
//------------------------------------------------------------------------------


//==============================================================================
function TMG_ActiveTerrainMenu::onSelect(%this,%id,%text) {
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
	%terObj.setFieldValue("dataFolder",%path);
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
