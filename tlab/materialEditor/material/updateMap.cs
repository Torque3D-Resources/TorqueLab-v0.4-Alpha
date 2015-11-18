//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$MaterialEditor_AutoUpdateMap = true;
//==============================================================================
//Select a texture for a material map type
function MaterialEditorGui::CheckAutoUpdateMap( %this,%type, %texture,%layer) {
	if (!$MaterialEditor_AutoUpdateMap)
		return;

	devLog("CheckAutoUpdateMap Type",%type,"Text",%texture);
	%fileName = fileBase(%texture);
	eval("%removeSuffix = SceneEditorCfg.DiffuseSuffix;");
	%fileName = strReplace(%fileName,%removeSuffix,"");
	%ext = fileExt(%texture);
	%folder = filePath(%texture)@"/";

	if(SceneEditorCfg.AutoAddNormal && %type !$="Normal") {
		%suffix = SceneEditorCfg.NormalSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;

		if (isImageFile(%testFile)) {
			devLog("Normal found:",%testFile);
			%this.updateTextureMapImage("Normal",%testFile,%layer,true);
		} else {
			devLog("Normal NOT found:",%testFile);
		}
	}

	if(SceneEditorCfg.AutoAddDiffuse && %type !$="Diffuse") {
		%suffix = SceneEditorCfg.DiffuseSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;

		if (isImageFile(%testFile))
			%this.updateTextureMapImage("Diffuse",%testFile,%layer,true);
	}

	if(SceneEditorCfg.AutoAddSpecular && %type !$="Specular") {
		%suffix = SceneEditorCfg.SpecularSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;

		if (isImageFile(%testFile))
			%this.updateSpecMap(true,%testFile,true);
	}

	if(SceneEditorCfg.AutoAddAO && %type !$="AO") {
		%suffix = SceneEditorCfg.AOSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;
		devLog("AO Check:",%testFile);

		if (isImageFile(%testFile))
			%this.updateAoMap(true,%testFile,true);
	}

	if(SceneEditorCfg.AutoAddSmoothness && %type !$="Smoothness") {
		%suffix = SceneEditorCfg.SmoothnessSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;
		devLog("Smoothness Check:",%testFile);

		if (isImageFile(%testFile))
			%this.updateRoughMap(true,%testFile,true);
	}

	if(SceneEditorCfg.AutoAddMetalness && %type !$="Metalness") {
		%suffix = SceneEditorCfg.MetalnessSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;
		devLog("Metalness Check:",%testFile);

		if (isImageFile(%testFile))
			%this.updateMetalMap(true,%testFile,true);
	}

	if(SceneEditorCfg.AutoAddComposite && %type !$="Composite") {
		%suffix = SceneEditorCfg.CompositeSuffix;
		%testFile = %folder @%fileName@%suffix@%ext;
		devLog("Composite Check:",%testFile);

		if (isImageFile(%testFile))
			%this.updateCompMap(true,%testFile,true);
	}
}

//------------------------------------------------------------------------------
$MEP_NoTextureImage = "tlab/materialEditor/assets/unavailable";
//==============================================================================
//Select a texture for a material map type
function MaterialEditorGui::openMapFile( %this, %defaultFileName ) {
	logc("MaterialEditorGui::openMapFile", %defaultFileName );
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
	%dlg = new OpenFileDialog() {
		Filters        = %filters;
		DefaultPath    = %defaultPath;
		DefaultFile    = %defaultFileName;
		ChangePath     = false;
		MustExist      = true;
	};
	%ret = %dlg.Execute();

	if(%ret) {
		MaterialEditorGui.lastTexturePath = filePath( %dlg.FileName );
		MaterialEditorGui.lastTextureFile = %filename = %dlg.FileName;
		%dlg.delete();
		return makeRelativePath( %filename, getMainDotCsDir() );
	}

	%dlg.delete();
	return;
}
//------------------------------------------------------------------------------

//==============================================================================

//------------------------------------------------------------------------------
//==============================================================================
// Per-Layer Material Options

// For update maps
// %action : 1 = change map
// %action : 0 = remove map

function MaterialEditorGui::updateTextureMap( %this, %type, %action ) {
	%layer = MaterialEditorGui.currentLayer;
	%bitmapCtrl = MaterialEditorPropertiesWindow.findObjectByInternalName( %type @ "MapDisplayBitmap", true );
	%textCtrl = MaterialEditorPropertiesWindow.findObjectByInternalName( %type @ "MapNameText", true );

	if( %action ) {
		%texture = MaterialEditorGui.openMapFile(MaterialEditorGui.currentMaterial.diffuseMap[0]);

		if( %texture !$= "" ) {
			%this.updateTextureMapImage(%type,%texture,%layer);
		}
	} else {
		%textCtrl.setText("None");
		%bitmapCtrl.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial(%type @ "Map[" @ %layer @ "]","");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateTextureMapImage( %this, %type, %texture,%layer,%skipAutoCheck ) {
	%bitmapCtrl = MaterialEditorPropertiesWindow.findObjectByInternalName( %type @ "MapDisplayBitmap", true );
	%textCtrl = MaterialEditorPropertiesWindow.findObjectByInternalName( %type @ "MapNameText", true );

	if (!%skipAutoCheck)
		%this.CheckAutoUpdateMap(%type, %texture,%layer);

	if (isImageFile(%texture))
		%bitmapCtrl.setBitmap(%texture);

	%bitmap = %bitmapCtrl.bitmap;
	%bitmap = strreplace(%bitmap,"tlab/materialEditor/scripts/","");
	%bitmapCtrl.setBitmap(%bitmap);
	%textCtrl.setText(%bitmap);
	MaterialEditorGui.updateActiveMaterial(%type @ "Map[" @ %layer @ "]","\"" @ %bitmap @ "\"");
}
//------------------------------------------------------------------------------
//==============================================================================

function MaterialEditorGui::updateDetailScale(%this,%newScale) {
	%layer = MaterialEditorGui.currentLayer;
	%detailScale = "\"" @ %newScale SPC %newScale @ "\"";
	MaterialEditorGui.updateActiveMaterial("detailScale[" @ %layer @ "]", %detailScale);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateDetailNormalStrength(%this,%newStrength) {
	%layer = MaterialEditorGui.currentLayer;
	%detailStrength = "\"" @ %newStrength @ "\"";
	MaterialEditorGui.updateActiveMaterial("detailNormalMapStrength[" @ %layer @ "]", %detailStrength);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateDiffuseMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;
	%this.updateTextureMapImage("Diffuse",%texture,%layer,%skipAutoCheck);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateNormalMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;
	%this.updateTextureMapImage("Normal",%texture,%layer,%skipAutoCheck);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSpecMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;

	if( %action ) {
		if (%texture $= "")
			%texture = MaterialEditorGui.openMapFile();

		if( %texture !$= "" ) {
			if (!%skipAutoCheck)
				%this.CheckAutoUpdateMap("Specular", %texture,%layer);

			MaterialEditorGui.updateActiveMaterial("pixelSpecular[" @ MaterialEditorGui.currentLayer @ "]", 0);
			MaterialEditorPropertiesWindow-->specMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialEditorPropertiesWindow-->specMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tlab/materialEditor/scripts/","");
			MaterialEditorPropertiesWindow-->specMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->specMapNameText.setText(%bitmap);
			MaterialEditorGui.updateActiveMaterial("specularMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialEditorPropertiesWindow-->specMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->specMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial("specularMap[" @ %layer @ "]","");
	}

	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
//PBR Script
//==============================================================================
function MaterialEditorGui::updateCompMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;

	if( %action ) {
		if (%texture $= "")
			%texture = MaterialEditorGui.openMapFile();

		if( %texture !$= "" ) {
			if (!%skipAutoCheck)
				%this.CheckAutoUpdateMap("Composite", %texture,%layer);

			MaterialEditorGui.updateActiveMaterial("pixelSpecular[" @ MaterialEditorGui.currentLayer @ "]", 0);
			MaterialEditorPropertiesWindow-->compMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialEditorPropertiesWindow-->compMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tlab/materialEditor/scripts/","");
			MaterialEditorPropertiesWindow-->compMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->compMapNameText.setText(%bitmap);
			MaterialEditorPropertiesWindow-->specMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->specMapNameText.setText(%bitmap);
			MaterialEditorGui.updateActiveMaterial("specularMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialEditorPropertiesWindow-->compMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->compMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorPropertiesWindow-->specMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->specMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial("specularMap[" @ %layer @ "]","");
	}

	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
function MaterialEditorGui::updateRoughMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;

	if( %action ) {
		if (%texture $= "")
			%texture = MaterialEditorGui.openFile("texture");

		if( %texture !$= "" ) {
			if (!%skipAutoCheck)
				%this.CheckAutoUpdateMap("Smoothness", %texture,%layer);

			MaterialEditorPropertiesWindow-->roughMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialEditorPropertiesWindow-->roughMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tools/materialEditor/scripts/","");
			MaterialEditorPropertiesWindow-->roughMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->roughMapNameText.setText(%bitmap);
			MaterialEditorGui.updateActiveMaterial("roughMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialEditorPropertiesWindow-->roughMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->roughMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial("roughMap[" @ %layer @ "]","");
	}

	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}

function MaterialEditorGui::updateaoMap(%this,%action,%texture,%skipAutoCheck) {
	%layer = MaterialEditorGui.currentLayer;

	if( %action ) {
		if (%texture $= "")
			%texture = MaterialEditorGui.openFile("texture");

		if( %texture !$= "" ) {
			if (!%skipAutoCheck)
				%this.CheckAutoUpdateMap("AO", %texture,%layer);

			MaterialEditorPropertiesWindow-->aoMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialEditorPropertiesWindow-->aoMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tools/materialEditor/scripts/","");
			MaterialEditorPropertiesWindow-->aoMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->aoMapNameText.setText(%bitmap);
			MaterialEditorGui.updateActiveMaterial("aoMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialEditorPropertiesWindow-->aoMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->aoMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial("aoMap[" @ %layer @ "]","");
	}

	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}

function MaterialEditorGui::updatemetalMap(%this,%action,%texture,%skipAutoCheck) {
	devLog("updatemetalMap:",%action,%texture,%skipAutoCheck);
	%layer = MaterialEditorGui.currentLayer;

	if( %action ) {
		if (%texture $= "")
			%texture = MaterialEditorGui.openFile("texture");

		if( %texture !$= "" ) {
			if (!%skipAutoCheck)
				%this.CheckAutoUpdateMap("Metalness", %texture,%layer);

			MaterialEditorPropertiesWindow-->metalMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialEditorPropertiesWindow-->metalMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tools/materialEditor/scripts/","");
			MaterialEditorPropertiesWindow-->metalMapDisplayBitmap.setBitmap(%bitmap);
			MaterialEditorPropertiesWindow-->metalMapNameText.setText(%bitmap);
			MaterialEditorGui.updateActiveMaterial("metalMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialEditorPropertiesWindow-->metalMapNameText.setText("None");
		MaterialEditorPropertiesWindow-->metalMapDisplayBitmap.setBitmap($MEP_NoTextureImage);
		MaterialEditorGui.updateActiveMaterial("metalMap[" @ %layer @ "]","");
	}

	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}

//PBR Script End
//==============================================================================
function MaterialEditorGui::updateRotationOffset(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%X = MaterialEditorPropertiesWindow-->RotationTextEditU.getText();
	%Y = MaterialEditorPropertiesWindow-->RotationTextEditV.getText();
	MaterialEditorPropertiesWindow-->RotationCrosshair.setPosition(45*mAbs(%X)-2, 45*mAbs(%Y)-2);
	MaterialEditorGui.updateActiveMaterial("rotPivotOffset[" @ %layer @ "]","\"" @ %X SPC %Y @ "\"",%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateRotationSpeed(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%speed = MaterialEditorPropertiesWindow-->RotationSpeedTextEdit.getText();
	MaterialEditorGui.updateActiveMaterial("rotSpeed[" @ %layer @ "]",%speed,%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateScrollOffset(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%X = MaterialEditorPropertiesWindow-->ScrollTextEditU.getText();
	%Y = MaterialEditorPropertiesWindow-->ScrollTextEditV.getText();
	MaterialEditorPropertiesWindow-->ScrollCrosshair.setPosition( -(23 * %X)+20, -(23 * %Y)+20);
	MaterialEditorGui.updateActiveMaterial("scrollDir[" @ %layer @ "]","\"" @ %X SPC %Y @ "\"",%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateScrollSpeed(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%speed = MaterialEditorPropertiesWindow-->ScrollSpeedTextEdit.getText();
	MaterialEditorGui.updateActiveMaterial("scrollSpeed[" @ %layer @ "]",%speed,%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateWaveType(%this) {
	for( %radioButton = 0; %radioButton < MaterialEditorPropertiesWindow-->WaveButtonContainer.getCount(); %radioButton++ ) {
		if( MaterialEditorPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).getValue() == 1 )
			%type = MaterialEditorPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).waveType;
	}

	%layer = MaterialEditorGui.currentLayer;
	MaterialEditorGui.updateActiveMaterial("waveType[" @ %layer @ "]", %type);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateWaveAmp(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%amp = MaterialEditorPropertiesWindow-->WaveTextEditAmp.getText();
	MaterialEditorGui.updateActiveMaterial("waveAmp[" @ %layer @ "]", %amp, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateWaveFreq(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%freq = MaterialEditorPropertiesWindow-->WaveTextEditFreq.getText();
	MaterialEditorGui.updateActiveMaterial("waveFreq[" @ %layer @ "]", %freq, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSequenceFPS(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%fps = MaterialEditorPropertiesWindow-->SequenceTextEditFPS.getText();
	MaterialEditorGui.updateActiveMaterial("sequenceFramePerSec[" @ %layer @ "]", %fps, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSequenceSSS(%this, %isSlider, %onMouseUp) {
	%layer = MaterialEditorGui.currentLayer;
	%sss = 1 / MaterialEditorPropertiesWindow-->SequenceTextEditSSS.getText();
	MaterialEditorGui.updateActiveMaterial("sequenceSegmentSize[" @ %layer @ "]", %sss, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateAnimationFlags(%this) {
	MaterialEditorGui.setMaterialDirty();
	%single = true;

	if(MaterialEditorPropertiesWindow-->RotationAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Rotate";
		else
			%flags = %flags @ " | $Rotate";

		%single = false;
	}

	if(MaterialEditorPropertiesWindow-->ScrollAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Scroll";
		else
			%flags = %flags @ " | $Scroll";

		%single = false;
	}

	if(MaterialEditorPropertiesWindow-->WaveAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Wave";
		else
			%flags = %flags @ " | $Wave";

		%single = false;
	}

	if(MaterialEditorPropertiesWindow-->ScaleAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Scale";
		else
			%flags = %flags @ " | $Scale";

		%single = false;
	}

	if(MaterialEditorPropertiesWindow-->SequenceAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Sequence";
		else
			%flags = %flags @ " | $Sequence";

		%single = false;
	}

	if(%flags $= "")
		%flags = "\"\"";

	%action = %this.createUndo(ActionUpdateActiveMaterialAnimationFlags, "Update Active Material");
	%action.material = MaterialEditorGui.currentMaterial;
	%action.object = MaterialEditorGui.currentObject;
	%action.layer = MaterialEditorGui.currentLayer;
	%action.newValue = %flags;
	%oldFlags = MaterialEditorGui.currentMaterial.getAnimFlags(MaterialEditorGui.currentLayer);

	if(%oldFlags $= "")
		%oldFlags = "\"\"";

	%action.oldValue = %oldFlags;
	MaterialEditorGui.submitUndo( %action );
	eval("materialEd_previewMaterial.animFlags[" @ MaterialEditorGui.currentLayer @ "] = " @ %flags @ ";");
	materialEd_previewMaterial.flush();
	materialEd_previewMaterial.reload();

	if (MaterialEditorGui.livePreview == true) {
		eval("MaterialEditorGui.currentMaterial.animFlags[" @ MaterialEditorGui.currentLayer @ "] = " @ %flags @ ";");
		MaterialEditorGui.currentMaterial.flush();
		MaterialEditorGui.currentMaterial.reload();
	}
}
//------------------------------------------------------------------------------
//==============================================================================

//These two functions are focused on object/layer specific functionality
function MaterialEditorGui::updateColorMultiply(%this,%color) {
	%propName = "diffuseColor[" @ MaterialEditorGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialEditorPropertiesWindow-->colorTintSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSpecularCheckbox(%this,%value) {
	MaterialEditorGui.updateActiveMaterial("pixelSpecular[" @ MaterialEditorGui.currentLayer @ "]", %value);
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSpecular(%this, %color) {
	%propName = "specular[" @ MaterialEditorGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialEditorPropertiesWindow-->specularColorSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSubSurfaceColor(%this, %color) {
	%propName = "subSurfaceColor[" @ MaterialEditorGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialEditorPropertiesWindow-->subSurfaceColorSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateEffectColor0(%this, %color) {
	%this.syncGuiColor(MaterialEditorPropertiesWindow-->effectColor0Swatch, "effectColor[0]", %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateEffectColor1(%this, %color) {
	%this.syncGuiColor(MaterialEditorPropertiesWindow-->effectColor1Swatch, "effectColor[1]", %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateBehaviorSound(%this, %type, %sound) {
	%defaultId = -1;
	%customName = "";

	switch$ (%sound) {
	case "<Soft>":
		%defaultId = 0;

	case "<Hard>":
		%defaultId = 1;

	case "<Metal>":
		%defaultId = 2;

	case "<Snow>":
		%defaultId = 3;

	default:
		%customName = %sound;
	}

	%this.updateActiveMaterial(%type @ "SoundId", %defaultId);
	%this.updateActiveMaterial("custom" @ %type @ "Sound", %customName);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateSoundPopup(%this, %type, %defaultId, %customName) {
	%ctrl = MaterialEditorPropertiesWindow.findObjectByInternalName( %type @ "SoundPopup", true );

	switch (%defaultId) {
	case 0:
		%name = "<Soft>";

	case 1:
		%name = "<Hard>";

	case 2:
		%name = "<Metal>";

	case 3:
		%name = "<Snow>";

	default:
		if (%customName $= "")
			%name = "<None>";
		else
			%name = %customName;
	}

	%r = %ctrl.findText(%name);

	if (%r != -1)
		%ctrl.setSelected(%r, false);
	else
		%ctrl.setText(%name);
}
//------------------------------------------------------------------------------
//==============================================================================
//These two functions are focused on environment specific functionality
function MaterialEditorGui::updateLightColor(%this, %color) {
	matEd_previewObjectView.setLightColor(%color);
	matEd_lightColorPicker.color = %color;
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updatePreviewBackground(%this,%color) {
	matEd_previewBackground.color = %color;
	MaterialPreviewBackgroundPicker.color = %color;
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateAmbientColor(%this,%color) {
	matEd_previewObjectView.setAmbientLightColor(%color);
	matEd_ambientLightColorPicker.color = %color;
}
//------------------------------------------------------------------------------
//==============================================================================
// PBR Script - Set/Get PBR CHannels
//==============================================================================
//==============================================================================
// Set PBR Channel in selectors
function MaterialEditorGui::setRoughChan(%this, %value) {
	MaterialEditorGui.updateActiveMaterial("SmoothnessChan[" @ MaterialEditorGui.currentLayer @ "]", %value);
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
function MaterialEditorGui::setAOChan(%this, %value) {
	MaterialEditorGui.updateActiveMaterial("aoChan[" @ MaterialEditorGui.currentLayer @ "]", %value);
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
function MaterialEditorGui::setMetalChan(%this, %value) {
	MaterialEditorGui.updateActiveMaterial("metalChan[" @ MaterialEditorGui.currentLayer @ "]", %value);
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
//==============================================================================
// Get PBR Channels
function MaterialEditorGui::getRoughChan(%this, %channel) {
	%guiElement = roughChanBtn @ %channel;
	%guiElement.setStateOn(true);
}
//------------------------------------------------------------------------------
function MaterialEditorGui::getAOChan(%this, %channel) {
	%guiElement = AOChanBtn @ %channel;
	%guiElement.setStateOn(true);
}
//------------------------------------------------------------------------------
function MaterialEditorGui::getMetalChan(%this, %channel) {
	%guiElement = metalChanBtn @ %channel;
	%guiElement.setStateOn(true);
}
//------------------------------------------------------------------------------
