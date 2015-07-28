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
	hide(TMG_TerrainLayerPill);
	show(TMG_TerrainLayerStack);
	if (%clearOnly)
		return;
	%mats = ETerrainEditor.getMaterials();
	for( %i = 0; %i < getRecordCount( %mats ); %i++ ) {
		%matInternalName = getRecord( %mats, %i );
		%mat = TerrainMaterialSet.findObjectByInternalName( %matInternalName );		
		%pill = cloneObject(TMG_TerrainLayerPill);
		%pill.matObj = %mat;
		%pill.layerId = %i;
		%pill.internalName = "Layer_"@%i;
		%pill-->materialName.text = "Material:\c1" SPC %mat.internalName;
		%pill-->materialMouse.pill = %pill;
		%pill-->materialMouse.superClass = "TMG_GeneralMaterialMouse";
		%pill-->materialMouse.callback = "TMG_GeneralMaterialChangeCallback";
		
		TMG_TerrainLayerStack.add(%pill);
	}
	
}
//------------------------------------------------------------------------------
function TMG_GeneralMaterialMouse::onMouseDown(%this,%modifier,%mousePoint,%mouseClickCount) {
	if (%mouseClickCount > 1){
		TMG.showGeneralMaterialDlg(%this.pill);
	}
	
	
}
//==============================================================================
function TMG::showGeneralMaterialDlg( %this,%pill ) {	
	if (!isObject(%pill)) {
		warnLog("Invalid layer to change material");
		return;
	}
	if (%callback $= "")
		%callback = "TMG_LayerMaterialChangeCallback";
	%mat = %pill.matObj;	
	TMG.changeMaterialPill = %pill;
	TMG.changeMaterialLive = %directUpdate;
	TerrainMaterialDlg.show( %pill.layerId, %mat,TMG_GeneralMaterialChangeCallback );
}
//------------------------------------------------------------------------------
//==============================================================================
// Callback from TerrainMaterialDlg returning selected material info
function TMG_GeneralMaterialChangeCallback( %mat, %matIndex, %activeIdx ) {	
	TMG_LayerMaterialChangeCallback(%mat, %matIndex, %activeIdx );	
	EPainter_TerrainMaterialUpdateCallback(%mat, %matIndex);	
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