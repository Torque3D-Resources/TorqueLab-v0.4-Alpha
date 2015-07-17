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
//==============================================================================
function TMG::exportHeightMap( %this,%folder) {
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
	
	%ret = %terObj.exportHeightMap( %folder @ "/" @ %filePrefix,"png" );
	
	devLog("Terrain HeightMap exported to:",%folder @ "/" @ %filePrefix,"Result=",%ret);
	if (%ret)
		return %folder @ "/" @ %filePrefix;
	
	return "";
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