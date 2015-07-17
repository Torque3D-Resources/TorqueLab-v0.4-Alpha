//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// TerrainObject Functions
//==============================================================================

//==============================================================================
function TMG::updateTerrainField(%this,%field,%value) {
	%terrain = TMG.activeTerrain;
	%terrain.setFieldValue(%field,%value);
	
}
//------------------------------------------------------------------------------

//==============================================================================
// Sync the current profile values into the params objects
function TMG::getTerrainFile( %this ) { 	
	
   %currentFile = TMG.activeTerrain.terrainFile;
   //Canvas.cursorOff();
   getLoadFilename("*.*|*.*", "TMG.setTerrainFile", %currentFile);
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function TMG::setTerrainFile( %this,%file ) { 
   
  %filename = makeRelativePath( %file, getMainDotCsDir() );     
   %this.updateTerrainField("terrainFile",%filename);

}
//------------------------------------------------------------------------------



//==============================================================================
// Terrain Layers Functions
//==============================================================================
//==============================================================================
function TMG::updateTerrainLayers(%this,%clearOnly) {
	TMG_TerrainLayerStack.clear();
	if (%clearOnly)
		return;
	%mats = ETerrainEditor.getMaterials();
	for( %i = 0; %i < getRecordCount( %mats ); %i++ ) {
		%matInternalName = getRecord( %mats, %i );
		%mat = TerrainMaterialSet.findObjectByInternalName( %matInternalName );
		%pill = cloneObject(TMG_TerrainLayerPill);
		%pill-->matInternalName.text = %matInternalName;
		%pill-->textureMap.text = "Not implemented";
		%pill-->textureMap.importTextureMapBtn.command = "TMG.importLayerTextureMap(\""@%matInternalName@"\");";
		%pill-->textureMap.exportTextureMapBtn.command = "TMG.exportLayerTextureMap(\""@%matInternalName@"\");";
		TMG_TerrainLayerStack.add(%pill);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::importLayerTextureMap(%this,%matInternalName) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::exportLayerTextureMap(%this,%matInternalName) {
	
}
//------------------------------------------------------------------------------

//==============================================================================
// TMG.getTerrainHeightRange
function TMG::getTerrainHeightRange( %this ) { 
  %heightRange = ETerrainEditor.getHeightRange();
  return %heightRange;

}
//------------------------------------------------------------------------------
//==============================================================================
/*
new TerrainBlock(TerrainTile_x0y0) {
         terrainFile = "levels/Demo/MiniTerrain/MiniTerrainDemo.ter";
         castShadows = "1";
         squareSize = "2";
         baseTexSize = "256";
         baseTexFormat = "JPG";
         lightMapSize = "256";
         screenError = "16";
         position = "-256 -256 0";
         rotation = "1 0 0 0";
         canSave = "1";
         canSaveDynamicFields = "1";
            scale = "1 1 1";
            tile = "0";
      };
*/