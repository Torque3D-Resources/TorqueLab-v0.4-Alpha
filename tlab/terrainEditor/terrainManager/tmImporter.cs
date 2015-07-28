//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Prepare and import the terrain HeightMap (Manager Import Terrain Button)
//==============================================================================

//==============================================================================
function TMG::prepareAllLayers(%this,%doImport) {
	%folder = TMG.SourceFolder;
	%this.exportTerrainLayersToPath(%folder,"png");

	foreach(%pill in TMG_MaterialLayersStack) {
		if (!isFile(%pill.file)) {			
			%pill.file = %folder@"/"@%pill-->mapMenu.getText()@".png";
			warnLog(%pill.layerId," Layer file created from menu text:",%pill.file);
		
		}
		else if (isFile(%pill.file)) {
			warnLog(%pill.layerId," Layer file was stored in pill:",%pill.file);
		}
			

		%pill.activeChannels = "";
		%stack = %pill-->channelStack;

		foreach$(%chan in %pill.channelRadios) {
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
//------------------------------------------------------------------------------

//==============================================================================
// Import Terrain Heightmap + Layers
//==============================================================================
//==============================================================================
function TMG::reimportTerrain(%this) {
	%terObj = %this.activeTerrain;
	%folder = TMG_PageMaterialLayers-->textureSourceFolder.text;
	%hmMenu = TMG_PageMaterialLayers-->heightMapMenu;

	if (%hmMenu.getSelected() !$= "0") {
		%file = %folder @"/"@%hmMenu.getText();
		%file = strreplace(%file,"//","/");
		TMG.currentHeightMap = %file;
	}

	%heightmapFile = TMG.currentHeightMap;

	if (TMG.heightmapMode $= "Source") {
		%heightmapSrc = %hmMenu.getText();
		%heightmapFile = TMG.SourceFolder@"/"@%heightmapSrc;
		devLog("Reimporting with heightmap:",%heightmapSrc,"File:",%heightmapFile);
	}

	if(!isFile(%heightmapFile)) {
		warnLog("Invalid file or terrainObj terrain:",	%terObj);
		warnLog("Invalid file or terrainObj file:",	TMG.currentHeightMap);
		return;
	}

	%metersPerPixel = TMG_HeightmapOptions-->squareSize.getText();
	%heightScale = TMG_HeightmapOptions-->heightScale.getText();
	%flipYAxis = TMG_HeightmapOptions-->flipAxisCheck.isStateOn();

	foreach(%pill in TMG_MaterialLayersStack) {
		%fixFile = %pill.file;
		if (!isFile(%fixFile))
			%preImportFailed = true;
		%opacityNames = strAddRecord(%opacityNames,%fixFile TAB %pill.activeChannels);
		%materialNames = strAddRecord(%materialNames,%pill.matInternalName);
	}
	if (%preImportFailed){
		devLog("Pre heightmap import failed! Importing heightmap with",getRecordCount(%opacityNames)," opacity maps and ",getRecordCount(%materialNames),"Materials.");
		devLog("Opacity list:",%opacityNames);
		devLog("Material list:",%materialNames);
		return;
	}
	%name = TMG_PageMaterialLayers-->importTerrainName.getText();
	%obj = TerrainBlock::import(  %name,
											%heightmapFile,
											%metersPerPixel,
											%heightScale,
											%opacityNames,
											%materialNames,
											%flipYAxis );
	%obj.terrainHeight = %heightScale;
	%obj.dataFolder = TMG.dataFolder;
	%obj.sourceFolder = TMG.sourceFolder;
	%obj.targetFolder = TMG.targetFolder;

	if (isObject(%terObj)) {
		%obj.position = %terObj.position;
	}

	if ( isObject( %obj ) ) {
		// Select it in the editor.
		EWorldEditor.clearSelection();
		EWorldEditor.selectObject(%obj);
		// When we drop the selection don't store undo
		// state for it... the creation deals with it.
		EWorldEditor.dropSelection( true );
		ETerrainEditor.isDirty = true;
		EPainter.updateLayers();
		TMG.activeTerrain = %obj;
		TMG.updateTerrainList(true);
		//TMG.setActiveTerrain(%obj);
	} else {
		warnLog("Something bad happen and heightmap import failed!");
		devLog("Importing heightmap with",getRecordCount(%opacityNames)," opacity maps and ",getRecordCount(%materialNames),"Materials.");
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
	if (isObject(%name)) {
		%info = "\c2Terrain exist and will be overridden with heightmap";
	} else {
		%info = "\c1New terrain will be created from heightmap";
	}

	TMG_PageMaterialLayers-->importTerrainNameStatus.setText(%info);
	TMG_PageMaterialLayers-->importTerrainName.setText(%name);
}
//------------------------------------------------------------------------------