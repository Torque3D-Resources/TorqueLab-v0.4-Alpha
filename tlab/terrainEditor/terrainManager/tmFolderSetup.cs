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
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setFolder(%this,%type,%folder,%relativeToData,%onlyTMG) {		
	if (%folder $= "")
		return;	
	
		
	if (%relativeToData){	
		%subFolder = %folder;
		%folder = TMG.dataFolder@"/"@%subFolder;
		%folder = strreplace(%folder,"//","/");
		%subField = %type@"SubFolder";
		eval("TMG."@%subField@" = %subFolder;");		
		
	}	
	%field = %type@"Folder";
	eval("TMG."@%field@" = %folder;");	
	if (%type !$= "data"){
		
		%subText = %folder;
		if (strFind(%folder,TMG.dataFolder)){				
			%relativeFolder = strreplace(%folder,TMG.dataFolder,"");
			if (getSubStr(%relativeFolder,0,1) $= "/")
				%relativeFolder = getSubStr(%relativeFolder,1);
			%subText = "\c2[Data/]\c0"@%relativeFolder;
		}
		
		
		eval("%editCtrl = TerrainManagerGui-->"@%type@"_sideFolder;");
		%editCtrl.setText(%subText);			
		
		eval("%subEdit = TerrainManagerGui-->"@%type@"_FolderEdit;");
		%subEdit.setText(%subText);
	}
	else {
	
		TerrainManagerGui-->dataFolder.setText(TMG.dataFolder);
		TerrainManagerGui-->sideDataFolderText.text = TMG.dataFolder;		
		%this.setFolder("source",TMG.sourceSubFolder,true);
		%this.setFolder("target",TMG.targetSubFolder,true);
	}
	
	
	
	
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
// Select TerrainManager Data folder
//==============================================================================
//==============================================================================
function TMG::selectDataFolder( %this,%type) {
	if (%type $= "")
		%type = "data";
	eval("%currentFolder = TMG."@%type@"Folder;");	
	
	if (!isDirectory(%current))
		%currentFolder = MissionGroup.getFilename();
	
	getFolderName("","TMG.setDataFolder",%currentFolder,"Select Export Folder",%type);	
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::setDataFolder( %this, %path,%type ) {
	%terObj = %this.activeTerrain;
	if (!isObject(%terObj)){
		warnlog("Not active terrain detected. Please select one before setting data folder:",%terObj);
		return;
	}
	%path =  makeRelativePath( %path, getMainDotCsDir() );
	//TerrainManagerGui-->dataFolder.setText(%path);	
	%this.setFolder(%type,%path);
}
//------------------------------------------------------------------------------

//==============================================================================
// Data/Source/Target TextEdit validations
//==============================================================================

//==============================================================================
function RelativeFolderEdit::onValidate(%this) {
	%data = strreplace(%this.internalName,"_"," ");
	%type = getWord(%data,0);
	%text = strreplace(%this.getText(),"[Data]","");
	TMG.setFolder(%type,%text,true);
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG_RelativeSourceFolderEdit::onValidate( %this ) {
	TMG.setFolder("source",%this.getText(),true);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG_RelativeTargetFolderEdit::onValidate( %this ) {
	TMG.setFolder("target",%this.getText(),true);
}
//------------------------------------------------------------------------------