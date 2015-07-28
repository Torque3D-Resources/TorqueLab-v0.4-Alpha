//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function TMG::getActiveFolders(%this) {
	%dataFolder = TMG.activeTerrain.dataFolder;
	if (%dataFolder $= "")
		%dataFolder = MissionGroup.dataFolder;
	if (%dataFolder $= "")
		%dataFolder = filePath(TMG.activeTerrain.terrainFile);
	
	TMG.setFolder("data",%dataFolder);
	
	%sourceFolder = TMG.activeTerrain.sourceFolder;
	if (%sourceFolder $= "")
		%sourceFolder = MissionGroup.sourceFolder;
	if (%sourceFolder $= "")
		%sourceFolder = %dataFolder;	
	TMG.setFolder("source",%sourceFolder);
	
	%targetFolder = TMG.activeTerrain.targetFolder;
	if (%targetFolder $= "")
		%targetFolder = MissionGroup.targetFolder;
	if (%targetFolder $= "")
		%targetFolder = %dataFolder;	
	TMG.setFolder("target",%targetFolder);
	
	//TMG.setFolder("source",MissionGroup.sourceSubFolder,true);
	//TMG.setFolder("target",MissionGroup.targetSubFolder,true);	
	
	
	//%relativeSourceFolder = strreplace(%sourceFolder,%dataFolder,"");
	//TerrainManagerGui-->subFolderSourceTitle.text = "Target folder:\c2" SPC %relativeSourceFolder;
	
	//TerrainManagerGui-->dataFolder.setText(TMG.dataFolder);	
	//TMG_PageMaterialLayers-->textureSourceFolder.setText(TMG.sourceFolder);
	//TMG_PageMaterialLayers-->relativeSourceFolder.setText(TMG.sourceSubFolder);
	//TMG_PageMaterialLayers-->textureTargetFolder.setText(TMG.targetFolder);
	//TMG_PageMaterialLayers-->relativeTargetFolder.setText(TMG.targetSubFolder);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setFolder(%this,%type,%folder,%relativeToData,%onlyTMG) {	
	if (%relativeToData){	
		%subFolder = %folder;
		%folder = TMG.dataFolder@"/"@%subFolder;
		%folder = strreplace(%folder,"//","/");
		%subField = %type@"SubFolder";
		eval("TMG."@%subField@" = %subFolder;");
		//if (!%onlyTMG){
		//	MissionGroup.setFieldValue(%subField,%subFolder);
		//	TMG.activeTerrain.setFieldValue(%subField,%subFolder);
		//}
		
	}	
	if (%type !$= "data"){		
		%relativeFolder = strreplace(%folder,TMG.dataFolder,"\c2[Data]\c0");
		eval("%editCtrl = TerrainManagerGui-->relative"@%type@"Folder;");
		%editCtrl.setText(%relativeFolder);
		
		
		eval("%editFullCtrl = TerrainManagerGui-->texture"@%type@"Folder;");
		%editFullCtrl.setText(%relativeFolder);
		
		eval("%subEdit = TerrainManagerGui-->"@%type@"_FolderEdit;");
		%subEdit.setText(%relativeFolder);
	}
	else {
		TerrainManagerGui-->dataFolder.setText(TMG.dataFolder);	
	}
	
	
	%field = %type@"Folder";
	eval("TMG."@%field@" = %folder;");
	
	if (!%onlyTMG){
		MissionGroup.setFieldValue(%field,%folder);		
	}
	if (isObject(TMG.activeTerrain)){
		if (TMG.activeTerrain.storeFolders)
			TMG.activeTerrain.setFieldValue(%field,%folder);
	}
	//Update map layers data if source changed
	if (%type $= "Source" && %oldSource !$= TMG.sourceFolder)
		TMG.updateMaterialLayers();
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

//==============================================================================
function RelativeFolderEdit::onValidate(%this) {
	%data = strreplace(%this.internalName,"_"," ");
	%type = getWord(%data,0);
	%text = strreplace(%this.getText(),"[Data]","");
	TMG.setFolder(%type,%text,true);
}
//------------------------------------------------------------------------------