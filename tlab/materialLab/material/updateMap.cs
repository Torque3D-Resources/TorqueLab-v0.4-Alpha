//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//------------------------------------------------------------------------------
//==============================================================================
//Select a texture for a material map type 
function MaterialLabTools::openMapFile( %this, %defaultFileName ) {
	logc("MaterialLabTools::openMapFile", %defaultFileName );
	
		%filters = MaterialLabTools.textureFormats;
		
		if (%defaultFileName $= "" || !isFile(%defaultFileName)) {
			if(MaterialLabTools.lastTextureFile !$= "")
				%defaultFileName = MaterialLabTools.lastTextureFile;
			else if (isFile($MLP_BaseObjectPath)){
				%defaultFileName = $MLP_BaseObjectPath;
			}
			else
				%defaultFileName = "art/*.*";
		}

		%defaultPath = MaterialLabTools.lastTexturePath;

	

	%dlg = new OpenFileDialog() {
		Filters        = %filters;
		DefaultPath    = %defaultPath;
		DefaultFile    = %defaultFileName;
		ChangePath     = false;
		MustExist      = true;
	};
	%ret = %dlg.Execute();

	if(%ret) {		
		MaterialLabTools.lastTexturePath = filePath( %dlg.FileName );
		MaterialLabTools.lastTextureFile = %filename = %dlg.FileName;
		
		%dlg.delete();
		return makeRelativePath( %filename, getMainDotCsDir() );
	}
	
	%dlg.delete();
	return;
}
//------------------------------------------------------------------------------


//==============================================================================
// Per-Layer Material Options

// For update maps
// %action : 1 = change map
// %action : 0 = remove map

function MaterialLabTools::updateMaterialMap( %this, %type, %action ) {
	%layer = MaterialLabTools.currentLayer;
	%bitmapCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapDisplayBitmap", true );
	%textCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapNameText", true );

	if( %action ) {
		%texture = MaterialLabTools.openMapFile(MaterialLabTools.currentMaterial.diffuseMap[0]);

		if( %texture !$= "" ) {
			%this.updateTextureMapImage(%type,%texture,%layer);
		}
	} else {
		%textCtrl.setText("None");
		%bitmapCtrl.setBitmap("tlab/materialLab/assets/unknownImage");
		MaterialLabTools.updateActiveMaterial(%type @ "Map[" @ %layer @ "]","");
	}
}
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// channel in selectors
function MaterialLabTools::setRoughChan(%this, %value)
{
   MaterialLabTools.updateActiveMaterial("SmoothnessChan[" @ MaterialLabTools.currentLayer @ "]", %value);   
   MaterialLabTools.guiSync( materialLab_previewMaterial );
}

function MaterialLabTools::setAOChan(%this, %value)
{
   MaterialLabTools.updateActiveMaterial("aoChan[" @ MaterialLabTools.currentLayer @ "]", %value);   
   MaterialLabTools.guiSync( materialLab_previewMaterial );
}

function MaterialLabTools::setMetalChan(%this, %value)
{
   MaterialLabTools.updateActiveMaterial("metalChan[" @ MaterialLabTools.currentLayer @ "]", %value);   
   MaterialLabTools.guiSync( materialLab_previewMaterial );
}
