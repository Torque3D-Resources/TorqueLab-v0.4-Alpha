//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Import Terrain Heightmap + Layers
//==============================================================================

//==============================================================================
function TMG::prepareAllLayers(%this,%doImport) {
	%folder = TMG.SourceFolder;	
	%this.exportTerrainLayersToPath(%folder,"png");
	
	foreach(%pill in TMG_MaterialLayersStack){
		if (%pill-->mapMenu.getSelected() $= "0"){
			%pill.file = %folder@"/"@%pill-->mapMenu.getText()@".png";
			%isFile = isFile(%pill.file);			
		}
		%pill.activeChannels = "";
		%stack = %pill-->channelStack;
		foreach$(%chan in %pill.channelRadios){			
			%radio = %stack.findObjectByInternalName(%chan);
			if (%radio.isStateOn())
				%pill.activeChannels = strAddWord(%pill.activeChannels,%chan);
		}		
	}
	
	TMG.currentHeightMap = %this.exportHeightMap(%folder);
	
	TMG_PageMaterialLayers-->reimportButton.active = 1;
	if (%doImport)
		%this.reimportTerrain();
	
}
//==============================================================================
function TMG::reimportTerrain(%this) {
	%terObj = %this.activeTerrain;
	%folder = TMG_PageMaterialLayers-->textureMapFolder.text;	
	%hmMenu = TMG_PageMaterialLayers-->heightMapMenu;
	if (%hmMenu.getSelected() !$= "0"){
		%file = %folder @"/"@%hmMenu.getText();
		TMG.currentHeightMap = %file;		
	}
	if(!isObject(%terObj) || !isFile(TMG.currentHeightMap))
		return;
	
	
	%metersPerPixel = TMG_HeightmapOptions-->squareSize.getText();
	%heightScale = TMG_HeightmapOptions-->heightScale.getText();
	%flipYAxis = TMG_HeightmapOptions-->flipAxisCheck.isStateOn();
	
	foreach(%pill in TMG_MaterialLayersStack) {		
		%opacityNames = strAddRecord(%opacityNames,%pill.file TAB %pill.activeChannels);
		%materialNames = strAddRecord(%materialNames,%pill.matInternalName);		
	}	
	devLog("Importing heightmap with",getRecordCount(%opacityNames)," opacity maps and ",getRecordCount(%materialNames),"Materials.");
	%name = TMG_PageMaterialLayers-->importTerrainName.getText();	
	%obj = TerrainBlock::import(  %name,
											 TMG.currentHeightMap,
											 %metersPerPixel,
											 %heightScale,
											 %opacityNames,
											 %materialNames,
											 %flipYAxis );
	%obj.terrainHeight = %heightScale;
	%obj.position = %terObj.position;
	%obj.dataFolder = %terObj.dataFolder;
	%obj.sourceFolder = %terObj.sourceFolder;
	%obj.targetFolder = %terObj.targetFolder;
	if ( isObject( %obj ) ) {		
		// Select it in the editor.
		EWorldEditor.clearSelection();
		EWorldEditor.selectObject(%obj);
		// When we drop the selection don't store undo
		// state for it... the creation deals with it.
		EWorldEditor.dropSelection( true );
		ETerrainEditor.isDirty = true;
		EPainter.updateLayers();
	} else {
		warnLog("Something bad happen and heightmap import failed!");
	}	
}


//------------------------------------------------------------------------------
//==============================================================================
// Export Single Layer Map
//==============================================================================

//==============================================================================
function TMG::selectSingleTextureMapFolder( %this, %layerId) {
	
	%folder = TerrainManagerGui-->dataFolder.getText();
	%dlg = new OpenFolderDialog() {
		Title = "Select Export Folder";
		Filters = %filter;
		DefaultFile = %folder;
		ChangePath = false;
		MustExist = true;
		MultipleFiles = false;
	};

	if(filePath( %folder ) !$= "")
		%dlg.DefaultPath = filePath(%folder);
	else
		%dlg.DefaultPath = getMainDotCSDir();

	if(%dlg.Execute())
		TMG.setSingleTextureMapFolder(%dlg.FileName,%layerId);
		
	%dlg.delete();
}
//------------------------------------------------------------------------------
function TMG::setSingleTextureMapFolder( %this, %path,%layerId) {
	%this.exportTerrainLayersToPath(%path,"png",%layerId);
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG_ImportTerrainNameEdit::onValidate(%this) {
	%name = %this.getText();
	TMG.validateImportTerrainName(%name);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::validateImportTerrainName(%this,%name) {
	if (isObject(%name)){
		%info = "\c2Terrain exist and will be overridden with heightmap";
	} else {
		%info = "\c1New terrain will be created from heightmap";
	}
	TMG_PageMaterialLayers-->importTerrainNameStatus.setText(%info);
	TMG_PageMaterialLayers-->importTerrainName.setText(%name);
	
}
//------------------------------------------------------------------------------