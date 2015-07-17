//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TMG_ExportLayerFormat = "png";
//==============================================================================
function TMG::initExporter( %this) {
	
}
//------------------------------------------------------------------------------



//==============================================================================
// Export Functions
//==============================================================================
function TMG::exportHeightMapToFile( %this) {
	getSaveFilename("Png Files|*.png","TMG.exportHeightMapFile",TMG.getDefaultFolder(),true);
}
//==============================================================================
function TMG::exportHeightMap( %this,%folder,%noOverwrite) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Trying to export layers for invalid terrain:",%terObj);
		return;
	}
	
	if ($TMG_ExportLayerFormat $= "")
		$TMG_ExportLayerFormat = "png";
	
	
	if (%folder $= "")
		%folder = %terObj.dataFolder;
	
	if (%folder $= ""){		
		%baseFile = MissionGroup.getFilename();
		%folder = filePath( MissionGroup.getFilename());		
	}
	if ($TMG_ExportFolderName $= "")
		$TMG_ExportFolderName = "Default";
		
	%exportPath = %folder@"/"@$TMG_ExportFolderName;
	
	%filePrefix = %terObj.getName() @ "_heightmap.png";	
	%exportFile = %folder @ "/" @ %filePrefix;
	%result = %this.exportHeightMapFile(%exportFile,"png",%noOverwrite);
	//%ret = %terObj.exportHeightMap( %folder @ "/" @ %filePrefix,"png" );
	
	//devLog("Terrain HeightMap exported to:",%folder @ "/" @ %filePrefix,"Result=",%ret);
	if (%result)
		return %folder @ "/" @ %filePrefix;
	
	return "";
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::exportHeightMapFile( %this,%file,%format,%noOverwrite) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Trying to export layers for invalid terrain:",%terObj);
		return;
	}
	if (%format $= "")
		%format = "png";
	
	if (%noOverwrite)
		devLog("No overwrite");
	if (isFile(%file))
		devLog("File exist");
	if (isFile(%file) && %noOverwrite){
		%newFile = filePath(%file)@"/"@fileBase(%file)@"_"@%inc++@fileExt(%file);
		while (isFile(%newFile)){
			%newFile = filePath(%file)@"/"@fileBase(%file)@"_"@%inc++@fileExt(%file);
			if (%inc > 20){
				warnLog("Couldn't find a unique file name after 20 attempts. Export aborted!");
				return false;
			}
		}
		%file = %newFile;
	}
		
	%ret = %terObj.exportHeightMap( %file,%format );
	if (%ret)
		info("Heightmap exported to:",%file);
	else
		info("Heightmap failed to export to:",%file,"See console for report");
	return %ret;
}
//------------------------------------------------------------------------------
//==============================================================================
//Tmg.exportTerrainLayers("","",2);
function TMG::exportTerrainLayers( %this,%folder,%addSubFolder,%layerId) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Trying to export layers for invalid terrain:",%terObj);
		return;
	}
	
	if ($TMG_ExportLayerFormat $= "")
		$TMG_ExportLayerFormat = "png";
	
	if (%folder $= "")
		%folder = %terObj.dataFolder;
		
	if (%folder $= ""){
		warnLog("Export failed because no folder set");
		return;	
		%baseFile = MissionGroup.getFilename();
		%basePath = filePath( MissionGroup.getFilename());
		%terObj.setFieldValue("dataFolder",%basePath@"Data");
	}
	if ($TMG_ExportFolderName $= "")
		$TMG_ExportFolderName = "Default";
		
	%exportPath = %terObj.dataFolder@"/"@$TMG_ExportFolderName;
	%this.exportTerrainLayersToPath(%exportPath,$TMG_ExportLayerFormat,%layerId);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::exportTerrainLayersToPath( %this,%exportPath,%format,%layerId) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Trying to export layers for invalid terrain:",%terObj);
		return;
	}
	
	if (%format $= "")
		%format = "png";
	
	if (%exportPath $= ""){
		warnLog("Export failed because no folder set");
		return;	
	}		
	
	
	%filePrefix = %terObj.getName() @ "_layerMap";	
	if (%layerId !$= ""){
		%ret = %terObj.exportSingleLayerMap( %layerId, %exportPath @ "/" @ %filePrefix, %format );
		devLog(%layerId," layer exported to:",%exportPath @ "/" @ %filePrefix,"Result=",%ret);
	}
	else{
		%ret = %terObj.exportLayerMaps( %exportPath @ "/" @ %filePrefix, "png" );
		devLog("Terrain exported to:",%exportPath @ "/" @ %filePrefix,"Result=",%ret);
	}	
}
//------------------------------------------------------------------------------