//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function MaterialLabGui::openFile( %this, %fileType,%defaultFileName ) {
	devLog("MaterialLabGui::openFile", %fileType,%defaultFileName );
	switch$(%fileType) {
	case "Texture":
		%filters = MaterialLabGui.textureFormats;
		
		if (%defaultFileName $= "" || !isFile(%defaultFileName)) {
			if(MaterialLabGui.lastTextureFile !$= "")
				%defaultFileName = MaterialLabGui.lastTextureFile;
			else if (isFile($MLP_BaseObjectPath)){
				%defaultFileName = $MLP_BaseObjectPath;
			}
			else
				%defaultFileName = "art/*.*";
		}

		%defaultPath = MaterialLabGui.lastTexturePath;

	case "Model":
		%filters = MaterialLabGui.modelFormats;

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MaterialLabGui.lastModelPath;

	case "File":
		%filters = "TorqueScript Files (*.cs)|*.cs";

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MaterialLabGui.lastModelPath;
	}

	%dlg = new OpenFileDialog() {
		Filters        = %filters;
		DefaultPath    = %defaultPath;
		DefaultFile    = %defaultFileName;
		ChangePath     = false;
		MustExist      = true;
	};
	%ret = %dlg.Execute();

	if(%ret) {
		switch$(%fileType) {
		case "Texture":
			MaterialLabGui.lastTexturePath = filePath( %dlg.FileName );
			MaterialLabGui.lastTextureFile = %filename = %dlg.FileName;

		case "Model":
			MaterialLabGui.lastModelPath = filePath( %dlg.FileName );
			MaterialLabGui.lastModelFile = %filename = %dlg.FileName;

		case "File":
			MaterialLabGui.lastScriptPath = filePath( %dlg.FileName );
			MaterialLabGui.lastScriptFile = %filename = %dlg.FileName;
		}
	}

	%dlg.delete();

	if(!%ret)
		return;
	else
		return makeRelativePath( %filename, getMainDotCsDir() );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::copyMaterials( %this, %copyFrom, %copyTo) {
	// Make sure we copy and restore the map to.
	%mapTo = %copyTo.mapTo;
	%copyTo.assignFieldsFrom( %copyFrom );
	%copyTo.mapTo = %mapTo;
}
//------------------------------------------------------------------------------
//==============================================================================
// still needs to be optimized further
function MaterialLabGui::searchForTexture(%this,%material, %texture) {
	if( %texture !$= "" ) {
		// set the find signal as false to start out with
		%isFile = false;
		// sete the formats we're going to be looping through if need be
		%formats = ".png .jpg .dds .bmp .gif .jng .tga";

		// if the texture contains the correct filepath and name right off the bat, lets use it
		if( isFile(%texture) )
			%isFile = true;
		else {
			for( %i = 0; %i < getWordCount(%formats); %i++) {
				%testFileName = %texture @ getWord( %formats, %i );

				if(isFile(%testFileName)) {
					%isFile = true;
					break;
				}
			}
		}

		// if we didn't grab a proper name, lets use a string logarithm
		if( !%isFile ) {
			%materialDiffuse = %texture;
			%materialDiffuse2 = %texture;
			%materialPath = %material.getFilename();

			if( strchr( %materialDiffuse, "/") $= "" ) {
				%k = 0;

				while( strpos( %materialPath, "/", %k ) != -1 ) {
					%count = strpos( %materialPath, "/", %k );
					%k = %count + 1;
				}

				%materialsCs = getSubStr( %materialPath , %k , 99 );
				%texture =  strreplace( %materialPath, %materialsCs, %texture );
			} else
				%texture =  strreplace( %materialPath, %materialPath, %texture );

			// lets test the pathing we came up with
			if( isFile(%texture) )
				%isFile = true;
			else {
				for( %i = 0; %i < getWordCount(%formats); %i++) {
					%testFileName = %texture @ getWord( %formats, %i );

					if(isFile(%testFileName)) {
						%isFile = true;
						break;
					}
				}
			}

			// as a last resort to find the proper name
			// we have to resolve using find first file functions very very slow
			if( !%isFile ) {
				%k = 0;

				while( strpos( %materialDiffuse2, "/", %k ) != -1 ) {
					%count = strpos( %materialDiffuse2, "/", %k );
					%k = %count + 1;
				}

				%texture =  getSubStr( %materialDiffuse2 , %k , 99 );

				for( %i = 0; %i < getWordCount(%formats); %i++) {
					%searchString = "*" @ %texture @ getWord( %formats, %i );
					%testFileName = findFirstFile( %searchString );

					if( isFile(%testFileName) ) {
						%texture = %testFileName;
						%isFile = true;
						break;
					}
				}
			}

			return %texture;
		} else
			return %texture; //Texture exists and can be found - just return the input argument.
	}

	return ""; //No texture associated with this property.
}
//------------------------------------------------------------------------------

//==============================================================================
function MaterialLabGui::convertTextureFields(%this) {
	// Find the absolute paths for the texture filenames so that
	// we can properly wire up the preview materials and controls.
	for(%diffuseI = 0; %diffuseI < 4; %diffuseI++) {
		%diffuseMap = MaterialLabGui.currentMaterial.diffuseMap[%diffuseI];
		%diffuseMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %diffuseMap);
		MaterialLabGui.currentMaterial.diffuseMap[%diffuseI] = %diffuseMap;
	}

	for(%normalI = 0; %normalI < 4; %normalI++) {
		%normalMap = MaterialLabGui.currentMaterial.normalMap[%normalI];
		%normalMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %normalMap);
		MaterialLabGui.currentMaterial.normalMap[%normalI] = %normalMap;
	}

	for(%overlayI = 0; %overlayI < 4; %overlayI++) {
		%overlayMap = MaterialLabGui.currentMaterial.overlayMap[%overlayI];
		%overlayMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %overlayMap);
		MaterialLabGui.currentMaterial.overlayMap[%overlayI] = %overlayMap;
	}

	for(%detailI = 0; %detailI < 4; %detailI++) {
		%detailMap = MaterialLabGui.currentMaterial.detailMap[%detailI];
		%detailMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %detailMap);
		MaterialLabGui.currentMaterial.detailMap[%detailI] = %detailMap;
	}

	for(%detailNormalI = 0; %detailNormalI < 4; %detailNormalI++) {
		%detailNormalMap = MaterialLabGui.currentMaterial.detailNormalMap[%detailNormalI];
		%detailNormalMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %detailNormalMap);
		MaterialLabGui.currentMaterial.detailNormalMap[%detailNormalI] = %detailNormalMap;
	}

	for(%lightI = 0; %lightI < 4; %lightI++) {
		%lightMap = MaterialLabGui.currentMaterial.lightMap[%lightI];
		%lightMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %lightMap);
		MaterialLabGui.currentMaterial.lightMap[%lightI] = %lightMap;
	}

	for(%toneI = 0; %toneI < 4; %toneI++) {
		%toneMap = MaterialLabGui.currentMaterial.toneMap[%toneI];
		%toneMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %toneMap);
		MaterialLabGui.currentMaterial.toneMap[%toneI] = %toneMap;
	}

	for(%specI = 0; %specI < 4; %specI++) {
		%specMap = MaterialLabGui.currentMaterial.specularMap[%specI];
		%specMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %specMap);
		MaterialLabGui.currentMaterial.specularMap[%specI] = %specMap;
	}
	//PBR Script   
   for(%roughI = 0; %roughI < 4; %roughI++)
   {
      %roughMap = MaterialLabGui.currentMaterial.roughMap[%roughI];      
      %roughMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %roughMap);
      MaterialLabGui.currentMaterial.roughMap[%specI] = %roughMap;
   }
   
   for(%aoI = 0; %aoI < 4; %aoI++)
   {
      %aoMap = MaterialLabGui.currentMaterial.aoMap[%aoI];      
      %aoMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %aoMap);
      MaterialLabGui.currentMaterial.aoMap[%specI] = %aoMap;
   }
   
   for(%metalI = 0; %metalI < 4; %metalI++)
   {
      %metalMap = MaterialLabGui.currentMaterial.metalMap[%metalI];      
      %metalMap = MaterialLabGui.searchForTexture(MaterialLabGui.currentMaterial, %metalMap);
      MaterialLabGui.currentMaterial.metalMap[%metalI] = %metalMap;
   }
   //PBR ScriptEnd

}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::isMatLabitorMaterial(%this, %material) {
	return ( %material.getFilename() $= "" ||
													%material.getFilename() $= "tlab/gui/materialSelector.ed.gui" ||
															%material.getFilename() $= "tlab/materialLab/scripts/materialLab.ed.cs" );
}
//------------------------------------------------------------------------------