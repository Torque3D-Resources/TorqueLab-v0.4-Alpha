//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function TMG::initMaterialLayersPage(%this) {
	devLog("initMaterialLayersPage");
	TMG_PageMaterialLayers-->textureMapFolder.setText("");
	TMG_PageMaterialLayers-->relativeMapFolder.setText("");
	TMG_PageMaterialLayers-->textureTargetFolder.setText("");
	TMG_PageMaterialLayers-->relativeTargetFolder.setText("");
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::refreshMaterialLayersPage(%this) {
	//%mapFolder = TMG_PageMaterialLayers-->textureMapFolder.getText();
	//if (!isDirectory(%mapFolder))
		//TMG_PageMaterialLayers-->textureMapFolder.setText(%this.getDefaultFolder());
	
	%targetFolder = TMG_PageMaterialLayers-->textureTargetFolder.getText();
	if (!isDirectory(%targetFolder))
		TMG_PageMaterialLayers-->textureMapFolder.setText(%this.getDefaultFolder());
	
	%info = "The material layers page allow to simply reimport your terrain with current or new texture map." SPC
			"Those maps can be produced by external tools like L3TD or WorldMachine. Simply specify the folder containing" SPC
			"the sources texture map and select the map you want to use for each terrain layers. Then you select the color channel" SPC
			"you want to use. You can also assign a new heightmap which would be use to recreate the terrain (Similar to standard import" SPC
			"but you keep your current material layers information).";
	TMG_PageMaterialLayers-->materialLayersInfo.setText(%info);	

	
	//Change heightmap mode to current mode, else it will use the default current mode
	TMG.changeMapFolderMode(TMG_PageMaterialLayers-->mapModeStack-->relative,TMG.mapFolderMode);
	
	//Change heightmap mode to current mode, else it will use the default current mode
	TMG.changeHeightmapMode(TMG_PageMaterialLayers-->heightmapModeStack-->current,TMG.heightmapMode);
	
	TMG.updateMaterialLayers();
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::scanTextureMapFolder(%this) {
	TMG.textureMapList = "";
	if (TMG.mapFolderMode $= "relative"){
		%folder = TMG.sourceFolder;
		devLog("Scanning relative folder",%folder);
	}
	else
	{
		%folder = TMG_PageMaterialLayers-->textureMapFolder.text;
		devLog("Scanning browse folder",%folder);
	}
	
	if (!isDirectory(%folder)){
		warnLog("Invalid folder specified:",%folder," Make sure the folder exist and contain the images sources");
		TMG.textureMapList = "Nothing found in scanned folder." TAB "-" TAB "-";
		return;
	}

	%this.activeTerrain.setFieldValue("mapSourceFolder",%folder);
	%files = getMultiExtensionFileList(%folder,"png dds bmp tga jpg");
	
	for(%i = 0; %i < getRecordCount(%files); %i++) {
		%file = %folder@getRecord(%files,%i);		
		%bmpInfo = getBitmapinfo(%file );
		%fields = %file TAB getWord( %bmpInfo, 2 ) TAB getWord( %bmpInfo, 0 );
		TMG.textureMapList = strAddRecord(TMG.textureMapList,%fields);
	}

	
}
//------------------------------------------------------------------------------
//==============================================================================
// Terrain Layers Functions
//==============================================================================
//==============================================================================
function TMG::updateMaterialLayers(%this) {
	%terObj = %this.activeTerrain;
	%this.scanTextureMapFolder();
	
	
	
	TMG_PageMaterialLayers-->heightmapCurrentText.text = "Heightmap:\c1" SPC %terObj.getName()@"_heightmap";
	%hmMenu = TMG_PageMaterialLayers-->heightMapMenu;
	%hmMenu.clear();
	
	%hmMenu.add(%terObj.getName()@"_heightmap.png",0);
	for(%j=0;%j<getRecordCount(TMG.textureMapList);%j++){
			%record = getRecord(TMG.textureMapList,%j);
			%file = getField(%record,0);
			
			%onlyFile = fileBase(%file)@fileExt(%file);
			%hmMenu.add(%onlyFile,%hmid++);
			%hmMenu.file[%hmid++] = %file;
			
		}
		%hmMenu.setSelected(0,false);
	hide(TMG_MaterialLayersPill);
	show(TMG_MaterialLayersStack);
	TMG_MaterialLayersStack.clear();
	%mats = ETerrainEditor.getMaterials();
	for( %i = 0; %i < getRecordCount( %mats ); %i++ ) {
		%matInternalName = getRecord( %mats, %i );
		%mat = TerrainMaterialSet.findObjectByInternalName( %matInternalName );
		%pill = cloneObject(TMG_MaterialLayersPill);
		
		%pill.internalName = "Layer_"@%i;
		%pill.matInternalName = %matInternalName;
		%pill-->materialName.text = "Material:\c1" SPC %matInternalName;
		
		%texture = %terObj.getName()@"_layerMap_"@%i@"_"@%matInternalName;
		%pill-->exportMapBtn.command = "TMG.selectSingleTextureMapFolder("@%i@");";

		%pill-->removeMapBtn.command = "TMG.removeLayerMap("@%i@");";
		
		foreach(%radio in %pill-->channelStack){
			%radio.superClass = "TMG_MapChannelRadio";
			%radio.setStateOn(false);
		}
		%menu = %pill-->mapMenu;
		%menu.pill = %pill;
		%menu.layerId = %i;
		%menu.clear();
		%menu.add(%texture,0);
		%menu.channels[0] = "1";
		for(%j=0;%j<getRecordCount(TMG.textureMapList);%j++){
			%record = getRecord(TMG.textureMapList,%j);
			%file = getField(%record,0);
			
			%onlyFile = fileBase(%file)@fileExt(%file);
			%menu.add(%onlyFile,%id++);
			%menu.file[%id] = %file;
			%menu.channels[%id] = getField(%record,1);
		}
		%menu.command = "TMG.selectLayerMapMenu("@%menu@","@%i@");";
		%menu.setSelected(0,false);
		TMG_MaterialLayersStack.add(%pill);
		%this.selectLayerMapMenu(%menu,%i);
	}
	TMG_PageMaterialLayers-->reimportButton.active = 0;
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::selectLayerMapMenu(%this,%menu,%layerId,%channels) {
	%terObj = %this.activeTerrain;
	%layerId = %menu.layerId;
	%file = %menu.file[%menu.getSelected()];
	%channels = %menu.channels[%menu.getSelected()];
	%pill = %menu.pill;
	%pill.file = %file;
	%stack = %pill-->channelStack;
	foreach(%radio in %stack){
		%radio.visible = 0;
		%radio.setStateOn(false);
	}
	
	if (%channels $= "3"){		
		%pill.channelRadios = "R G B";		
	} else if (%channels $= "1"){		
		%pill.channelRadios = "R";		
	} else if (%channels $= "4"){		
		%pill.channelRadios = "R G B A";		
	}
	%set = false;
	foreach$(%chan in %pill.channelRadios){
		%radio = %stack.findObjectByInternalName(%chan);
		%radio.visible = 1;
		if (!%set)
			%radio.setStateOn(true);
		%set = true;
	}
	
}
//------------------------------------------------------------------------------

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
	%name = getUniqueName(%terObj.getName());
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
// Terrain Data Folder (used as base for exporting)
//==============================================================================
//==============================================================================
function TMG::removeLayerMap( %this,%layerId) {
	warnLog("Not yet");
}
//------------------------------------------------------------------------------
//==============================================================================
// Terrain Data Folder (used as base for exporting)
//==============================================================================
//==============================================================================
function TMG::changeMapFolderMode( %this, %ctrl,%mode) {
	TMG.mapFolderMode = %mode;
	if (TMG.mapFolderMode $= "")
		TMG.mapFolderMode = %ctrl.internalName;
		
	TMG_PageMaterialLayers-->relativeMapSource.visible = 0;
	TMG_PageMaterialLayers-->browseMapSource.visible = 0;
	eval("TMG_PageMaterialLayers-->"@TMG.mapFolderMode@"MapSource.visible = 1;");
	TMG.updateMaterialLayers();
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::selectTextureMapFolder( %this, %isTargetFolder) {
	
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
		TMG.setTextureMapFolder(%dlg.FileName,%isTargetFolder);
		
	%dlg.delete();
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setTextureMapFolder( %this, %path,%isTargetFolder ) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Not active terrain detected. Please select one before setting data folder:",%terObj);
		return;
	}
	%path =  makeRelativePath( %path, getMainDotCsDir() );
	if (%isTargetFolder){
		TMG_PageMaterialLayers-->textureTargetFolder.setText(%path);
		%this.setFolder("target",%path);		
		return;
	}
	TMG_PageMaterialLayers-->textureMapFolder.setText(%path);
	%this.setFolder("source",%path);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG_RelativeMapFolderEdit::onValidate( %this ) {	
	TMG.setFolder("source",%this.getText(),true);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG_RelativeTargetFolderEdit::onValidate( %this ) {		
	TMG.setFolder("target",%this.getText(),true);
}
//------------------------------------------------------------------------------
//==============================================================================
// Heightmap for re-importing
//==============================================================================
//==============================================================================
function TMG::changeHeightmapMode( %this, %ctrl, %mode) {
	TMG.heightmapMode = %mode;
	if (TMG.heightmapMode $= "")
		TMG.heightmapMode = %ctrl.internalName;
		
	TMG_PageMaterialLayers-->currentHeightmap.visible = 0;
	TMG_PageMaterialLayers-->browseHeightmap.visible = 0;
	TMG_PageMaterialLayers-->sourceHeightmap.visible = 0;
	eval("TMG_PageMaterialLayers-->"@TMG.heightmapMode@"Heightmap.visible = 1;");
	eval("TMG_PageMaterialLayers-->heightmapModeStack-->"@TMG.heightmapMode@".setStateOn(true);");
	
	
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