//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function MatLab::openFile( %this, %fileType,%defaultFileName ) {
	devLog("MatLab::openFile", %fileType,%defaultFileName );
	switch$(%fileType) {
	case "Texture":
		%filters = MatLab.textureFormats;
		
		if (%defaultFileName $= "" || !isFile(%defaultFileName)) {
			if(MatLab.lastTextureFile !$= "")
				%defaultFileName = MatLab.lastTextureFile;
			else if (isFile($MLP_BaseObjectPath)){
				%defaultFileName = $MLP_BaseObjectPath;
			}
			else
				%defaultFileName = "art/*.*";
		}

		%defaultPath = MatLab.lastTexturePath;

	case "Model":
		%filters = MatLab.modelFormats;

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MatLab.lastModelPath;

	case "File":
		%filters = "TorqueScript Files (*.cs)|*.cs";

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MatLab.lastModelPath;
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
			MatLab.lastTexturePath = filePath( %dlg.FileName );
			MatLab.lastTextureFile = %filename = %dlg.FileName;

		case "Model":
			MatLab.lastModelPath = filePath( %dlg.FileName );
			MatLab.lastModelFile = %filename = %dlg.FileName;

		case "File":
			MatLab.lastScriptPath = filePath( %dlg.FileName );
			MatLab.lastScriptFile = %filename = %dlg.FileName;
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
function MatLab::copyMaterials( %this, %copyFrom, %copyTo) {
	// Make sure we copy and restore the map to.
	%mapTo = %copyTo.mapTo;
	%copyTo.assignFieldsFrom( %copyFrom );
	%copyTo.mapTo = %mapTo;
}
//------------------------------------------------------------------------------
//==============================================================================
// still needs to be optimized further
function MatLab::searchForTexture(%this,%material, %texture) {
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
function MatLab::convertTextureFields(%this) {
	// Find the absolute paths for the texture filenames so that
	// we can properly wire up the preview materials and controls.
	for(%diffuseI = 0; %diffuseI < 4; %diffuseI++) {
		%diffuseMap = MatLab.currentMaterial.diffuseMap[%diffuseI];
		%diffuseMap = MatLab.searchForTexture(MatLab.currentMaterial, %diffuseMap);
		MatLab.currentMaterial.diffuseMap[%diffuseI] = %diffuseMap;
	}

	for(%normalI = 0; %normalI < 4; %normalI++) {
		%normalMap = MatLab.currentMaterial.normalMap[%normalI];
		%normalMap = MatLab.searchForTexture(MatLab.currentMaterial, %normalMap);
		MatLab.currentMaterial.normalMap[%normalI] = %normalMap;
	}

	for(%overlayI = 0; %overlayI < 4; %overlayI++) {
		%overlayMap = MatLab.currentMaterial.overlayMap[%overlayI];
		%overlayMap = MatLab.searchForTexture(MatLab.currentMaterial, %overlayMap);
		MatLab.currentMaterial.overlayMap[%overlayI] = %overlayMap;
	}

	for(%detailI = 0; %detailI < 4; %detailI++) {
		%detailMap = MatLab.currentMaterial.detailMap[%detailI];
		%detailMap = MatLab.searchForTexture(MatLab.currentMaterial, %detailMap);
		MatLab.currentMaterial.detailMap[%detailI] = %detailMap;
	}

	for(%detailNormalI = 0; %detailNormalI < 4; %detailNormalI++) {
		%detailNormalMap = MatLab.currentMaterial.detailNormalMap[%detailNormalI];
		%detailNormalMap = MatLab.searchForTexture(MatLab.currentMaterial, %detailNormalMap);
		MatLab.currentMaterial.detailNormalMap[%detailNormalI] = %detailNormalMap;
	}

	for(%lightI = 0; %lightI < 4; %lightI++) {
		%lightMap = MatLab.currentMaterial.lightMap[%lightI];
		%lightMap = MatLab.searchForTexture(MatLab.currentMaterial, %lightMap);
		MatLab.currentMaterial.lightMap[%lightI] = %lightMap;
	}

	for(%toneI = 0; %toneI < 4; %toneI++) {
		%toneMap = MatLab.currentMaterial.toneMap[%toneI];
		%toneMap = MatLab.searchForTexture(MatLab.currentMaterial, %toneMap);
		MatLab.currentMaterial.toneMap[%toneI] = %toneMap;
	}

	for(%specI = 0; %specI < 4; %specI++) {
		%specMap = MatLab.currentMaterial.specularMap[%specI];
		%specMap = MatLab.searchForTexture(MatLab.currentMaterial, %specMap);
		MatLab.currentMaterial.specularMap[%specI] = %specMap;
	}
	//PBR Script   
   for(%roughI = 0; %roughI < 4; %roughI++)
   {
      %roughMap = MatLab.currentMaterial.roughMap[%roughI];      
      %roughMap = MatLab.searchForTexture(MatLab.currentMaterial, %roughMap);
      MatLab.currentMaterial.roughMap[%specI] = %roughMap;
   }
   
   for(%aoI = 0; %aoI < 4; %aoI++)
   {
      %aoMap = MatLab.currentMaterial.aoMap[%aoI];      
      %aoMap = MatLab.searchForTexture(MatLab.currentMaterial, %aoMap);
      MatLab.currentMaterial.aoMap[%specI] = %aoMap;
   }
   
   for(%metalI = 0; %metalI < 4; %metalI++)
   {
      %metalMap = MatLab.currentMaterial.metalMap[%metalI];      
      %metalMap = MatLab.searchForTexture(MatLab.currentMaterial, %metalMap);
      MatLab.currentMaterial.metalMap[%metalI] = %metalMap;
   }
   //PBR ScriptEnd

}
//------------------------------------------------------------------------------
//==============================================================================
function MatLab::isMatLabitorMaterial(%this, %material) {
	return ( %material.getFilename() $= "" ||
													%material.getFilename() $= "tlab/gui/materialSelector.ed.gui" ||
															%material.getFilename() $= "tlab/materialLab/scripts/materialLab.ed.cs" );
}
//------------------------------------------------------------------------------