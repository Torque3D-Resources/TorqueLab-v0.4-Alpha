//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function MaterialEditorGui::openFile( %this, %fileType,%defaultFileName ) {
	devLog("MaterialEditorGui::openFile", %fileType,%defaultFileName );

	switch$(%fileType) {
	case "Texture":
		%filters = MaterialEditorGui.textureFormats;

		if (%defaultFileName $= "" || !isFile(%defaultFileName)) {
			if(MaterialEditorGui.lastTextureFile !$= "")
				%defaultFileName = MaterialEditorGui.lastTextureFile;
			else if (isFile($MEP_BaseObjectPath)) {
				%defaultFileName = $MEP_BaseObjectPath;
			} else
				%defaultFileName = "art/*.*";
		}

		%defaultPath = MaterialEditorGui.lastTexturePath;

	case "Model":
		%filters = MaterialEditorGui.modelFormats;

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MaterialEditorGui.lastModelPath;

	case "File":
		%filters = "TorqueScript Files (*.cs)|*.cs";

		if (%defaultFileName $= "")
			%defaultFileName = "*.dts";

		%defaultPath = MaterialEditorGui.lastModelPath;
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
			MaterialEditorGui.lastTexturePath = filePath( %dlg.FileName );
			MaterialEditorGui.lastTextureFile = %filename = %dlg.FileName;

		case "Model":
			MaterialEditorGui.lastModelPath = filePath( %dlg.FileName );
			MaterialEditorGui.lastModelFile = %filename = %dlg.FileName;

		case "File":
			MaterialEditorGui.lastScriptPath = filePath( %dlg.FileName );
			MaterialEditorGui.lastScriptFile = %filename = %dlg.FileName;
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
function MaterialEditorGui::copyMaterials( %this, %copyFrom, %copyTo) {
	// Make sure we copy and restore the map to.
	if (!isObject(%copyFrom) || !isObject(%copyTo))
		return;

	%mapTo = %copyTo.mapTo;
	%copyTo.assignFieldsFrom( %copyFrom );
	%copyTo.mapTo = %mapTo;
}
//------------------------------------------------------------------------------
//==============================================================================
// still needs to be optimized further
function MaterialEditorGui::searchForTexture(%this,%material, %texture) {
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
function MaterialEditorGui::convertTextureFields(%this) {
	// Find the absolute paths for the texture filenames so that
	// we can properly wire up the preview materials and controls.
	for(%diffuseI = 0; %diffuseI < 4; %diffuseI++) {
		%diffuseMap = MaterialEditorGui.currentMaterial.diffuseMap[%diffuseI];
		%diffuseMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %diffuseMap);
		MaterialEditorGui.currentMaterial.diffuseMap[%diffuseI] = %diffuseMap;
	}

	for(%normalI = 0; %normalI < 4; %normalI++) {
		%normalMap = MaterialEditorGui.currentMaterial.normalMap[%normalI];
		%normalMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %normalMap);
		MaterialEditorGui.currentMaterial.normalMap[%normalI] = %normalMap;
	}

	for(%overlayI = 0; %overlayI < 4; %overlayI++) {
		%overlayMap = MaterialEditorGui.currentMaterial.overlayMap[%overlayI];
		%overlayMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %overlayMap);
		MaterialEditorGui.currentMaterial.overlayMap[%overlayI] = %overlayMap;
	}

	for(%detailI = 0; %detailI < 4; %detailI++) {
		%detailMap = MaterialEditorGui.currentMaterial.detailMap[%detailI];
		%detailMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %detailMap);
		MaterialEditorGui.currentMaterial.detailMap[%detailI] = %detailMap;
	}

	for(%detailNormalI = 0; %detailNormalI < 4; %detailNormalI++) {
		%detailNormalMap = MaterialEditorGui.currentMaterial.detailNormalMap[%detailNormalI];
		%detailNormalMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %detailNormalMap);
		MaterialEditorGui.currentMaterial.detailNormalMap[%detailNormalI] = %detailNormalMap;
	}

	for(%lightI = 0; %lightI < 4; %lightI++) {
		%lightMap = MaterialEditorGui.currentMaterial.lightMap[%lightI];
		%lightMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %lightMap);
		MaterialEditorGui.currentMaterial.lightMap[%lightI] = %lightMap;
	}

	for(%toneI = 0; %toneI < 4; %toneI++) {
		%toneMap = MaterialEditorGui.currentMaterial.toneMap[%toneI];
		%toneMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %toneMap);
		MaterialEditorGui.currentMaterial.toneMap[%toneI] = %toneMap;
	}

	for(%specI = 0; %specI < 4; %specI++) {
		%specMap = MaterialEditorGui.currentMaterial.specularMap[%specI];
		%specMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %specMap);
		MaterialEditorGui.currentMaterial.specularMap[%specI] = %specMap;
	}

	//PBR Script
	for(%roughI = 0; %roughI < 4; %roughI++) {
		%roughMap = MaterialEditorGui.currentMaterial.roughMap[%roughI];
		%roughMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %roughMap);
		MaterialEditorGui.currentMaterial.roughMap[%specI] = %roughMap;
	}

	for(%aoI = 0; %aoI < 4; %aoI++) {
		%aoMap = MaterialEditorGui.currentMaterial.aoMap[%aoI];
		%aoMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %aoMap);
		MaterialEditorGui.currentMaterial.aoMap[%specI] = %aoMap;
	}

	for(%metalI = 0; %metalI < 4; %metalI++) {
		%metalMap = MaterialEditorGui.currentMaterial.metalMap[%metalI];
		%metalMap = MaterialEditorGui.searchForTexture(MaterialEditorGui.currentMaterial, %metalMap);
		MaterialEditorGui.currentMaterial.metalMap[%metalI] = %metalMap;
	}

	//PBR ScriptEnd
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::isMatEditorMaterial(%this, %material) {
	return ( %material.getFilename() $= "" ||
													%material.getFilename() $= "tlab/gui/materialSelector.ed.gui" ||
															%material.getFilename() $= "tlab/materialEditor/scripts/materialEditor.ed.cs" );
}
//------------------------------------------------------------------------------