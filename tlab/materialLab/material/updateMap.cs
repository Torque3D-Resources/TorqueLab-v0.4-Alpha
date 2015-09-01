//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//------------------------------------------------------------------------------
//==============================================================================
//Select a texture for a material map type 
function MaterialLabGui::openMapFile( %this, %defaultFileName ) {
	logc("MaterialLabGui::openMapFile", %defaultFileName );
	
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

	

	%dlg = new OpenFileDialog() {
		Filters        = %filters;
		DefaultPath    = %defaultPath;
		DefaultFile    = %defaultFileName;
		ChangePath     = false;
		MustExist      = true;
	};
	%ret = %dlg.Execute();

	if(%ret) {		
		MaterialLabGui.lastTexturePath = filePath( %dlg.FileName );
		MaterialLabGui.lastTextureFile = %filename = %dlg.FileName;
		
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

function MaterialLabGui::updateTextureMap( %this, %type, %action ) {
	%layer = MaterialLabGui.currentLayer;
	%bitmapCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapDisplayBitmap", true );
	%textCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapNameText", true );

	if( %action ) {
		%texture = MaterialLabGui.openMapFile(MaterialLabGui.currentMaterial.diffuseMap[0]);

		if( %texture !$= "" ) {
			%this.updateTextureMapImage(%type,%texture,%layer);
		}
	} else {
		%textCtrl.setText("None");
		%bitmapCtrl.setBitmap("tlab/materialLab/assets/unknownImage");
		MaterialLabGui.updateActiveMaterial(%type @ "Map[" @ %layer @ "]","");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateTextureMapImage( %this, %type, %texture,%layer ) {
	%bitmapCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapDisplayBitmap", true );
	%textCtrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "MapNameText", true );

	if (isImageFile(%texture))
		%bitmapCtrl.setBitmap(%texture);

	%bitmap = %bitmapCtrl.bitmap;
	%bitmap = strreplace(%bitmap,"tlab/materialLab/scripts/","");
	%bitmapCtrl.setBitmap(%bitmap);
	%textCtrl.setText(%bitmap);
	MaterialLabGui.updateActiveMaterial(%type @ "Map[" @ %layer @ "]","\"" @ %bitmap @ "\"");
}
//------------------------------------------------------------------------------
//==============================================================================

function MaterialLabGui::updateDetailScale(%this,%newScale) {
	%layer = MaterialLabGui.currentLayer;
	%detailScale = "\"" @ %newScale SPC %newScale @ "\"";
	MaterialLabGui.updateActiveMaterial("detailScale[" @ %layer @ "]", %detailScale);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateDetailNormalStrength(%this,%newStrength) {
	%layer = MaterialLabGui.currentLayer;
	%detailStrength = "\"" @ %newStrength @ "\"";
	MaterialLabGui.updateActiveMaterial("detailNormalMapStrength[" @ %layer @ "]", %detailStrength);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSpecMap(%this,%action) {
	%layer = MaterialLabGui.currentLayer;

	if( %action ) {
		%texture = MaterialLabGui.openMapFile();

		if( %texture !$= "" ) {
			MaterialLabGui.updateActiveMaterial("pixelSpecular[" @ MaterialLabGui.currentLayer @ "]", 0);
			MaterialLabPropertiesWindow-->specMapDisplayBitmap.setBitmap(%texture);
			%bitmap = MaterialLabPropertiesWindow-->specMapDisplayBitmap.bitmap;
			%bitmap = strreplace(%bitmap,"tlab/materialLab/scripts/","");
			MaterialLabPropertiesWindow-->specMapDisplayBitmap.setBitmap(%bitmap);
			MaterialLabPropertiesWindow-->specMapNameText.setText(%bitmap);
			MaterialLabGui.updateActiveMaterial("specularMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
		}
	} else {
		MaterialLabPropertiesWindow-->specMapNameText.setText("None");
		MaterialLabPropertiesWindow-->specMapDisplayBitmap.setBitmap("tlab/materialLab/assets/unknownImage");
		MaterialLabGui.updateActiveMaterial("specularMap[" @ %layer @ "]","");
	}

	MaterialLabGui.guiSync( materialLab_previewMaterial );
}
//------------------------------------------------------------------------------
//PBR Script
function MaterialLabGui::updateRoughMap(%this,%action)
{
   %layer = MaterialLabGui.currentLayer;
   
   if( %action )
   {
      %texture = MaterialLabGui.openFile("texture");
      if( %texture !$= "" )
      {         
         MaterialLabPropertiesWindow-->roughMapDisplayBitmap.setBitmap(%texture);
      
         %bitmap = MaterialLabPropertiesWindow-->roughMapDisplayBitmap.bitmap;
         %bitmap = strreplace(%bitmap,"tools/materialLab/scripts/","");
         MaterialLabPropertiesWindow-->roughMapDisplayBitmap.setBitmap(%bitmap);
         MaterialLabPropertiesWindow-->roughMapNameText.setText(%bitmap);
         MaterialLabGui.updateActiveMaterial("roughMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
      }
   }
   else
   {
      MaterialLabPropertiesWindow-->roughMapNameText.setText("None");
      MaterialLabPropertiesWindow-->roughMapDisplayBitmap.setBitmap("tools/materialeditor/gui/unknownImage");
      MaterialLabGui.updateActiveMaterial("roughMap[" @ %layer @ "]","");
   }
   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}

function MaterialLabGui::updateaoMap(%this,%action)
{
   %layer = MaterialLabGui.currentLayer;
   
   if( %action )
   {
      %texture = MaterialLabGui.openFile("texture");
      if( %texture !$= "" )
      {         
         MaterialLabPropertiesWindow-->aoMapDisplayBitmap.setBitmap(%texture);
      
         %bitmap = MaterialLabPropertiesWindow-->aoMapDisplayBitmap.bitmap;
         %bitmap = strreplace(%bitmap,"tools/materialLab/scripts/","");
         MaterialLabPropertiesWindow-->aoMapDisplayBitmap.setBitmap(%bitmap);
         MaterialLabPropertiesWindow-->aoMapNameText.setText(%bitmap);
         MaterialLabGui.updateActiveMaterial("aoMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
      }
   }
   else
   {
      MaterialLabPropertiesWindow-->aoMapNameText.setText("None");
      MaterialLabPropertiesWindow-->aoMapDisplayBitmap.setBitmap("tools/materialeditor/gui/unknownImage");
      MaterialLabGui.updateActiveMaterial("aoMap[" @ %layer @ "]","");
   }
   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}

function MaterialLabGui::updatemetalMap(%this,%action)
{
   %layer = MaterialLabGui.currentLayer;
   
   if( %action )
   {
      %texture = MaterialLabGui.openFile("texture");
      if( %texture !$= "" )
      {         
         MaterialLabPropertiesWindow-->metalMapDisplayBitmap.setBitmap(%texture);
      
         %bitmap = MaterialLabPropertiesWindow-->metalMapDisplayBitmap.bitmap;
         %bitmap = strreplace(%bitmap,"tools/materialLab/scripts/","");
         MaterialLabPropertiesWindow-->metalMapDisplayBitmap.setBitmap(%bitmap);
         MaterialLabPropertiesWindow-->metalMapNameText.setText(%bitmap);
         MaterialLabGui.updateActiveMaterial("metalMap[" @ %layer @ "]","\"" @ %bitmap @ "\"");
      }
   }
   else
   {
      MaterialLabPropertiesWindow-->metalMapNameText.setText("None");
      MaterialLabPropertiesWindow-->metalMapDisplayBitmap.setBitmap("tools/materialeditor/gui/unknownImage");
      MaterialLabGui.updateActiveMaterial("metalMap[" @ %layer @ "]","");
   }
   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}

//PBR Script End
//==============================================================================
function MaterialLabGui::updateRotationOffset(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%X = MaterialLabPropertiesWindow-->RotationTextEditU.getText();
	%Y = MaterialLabPropertiesWindow-->RotationTextEditV.getText();
	MaterialLabPropertiesWindow-->RotationCrosshair.setPosition(45*mAbs(%X)-2, 45*mAbs(%Y)-2);
	MaterialLabGui.updateActiveMaterial("rotPivotOffset[" @ %layer @ "]","\"" @ %X SPC %Y @ "\"",%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateRotationSpeed(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%speed = MaterialLabPropertiesWindow-->RotationSpeedTextEdit.getText();
	MaterialLabGui.updateActiveMaterial("rotSpeed[" @ %layer @ "]",%speed,%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateScrollOffset(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%X = MaterialLabPropertiesWindow-->ScrollTextEditU.getText();
	%Y = MaterialLabPropertiesWindow-->ScrollTextEditV.getText();
	MaterialLabPropertiesWindow-->ScrollCrosshair.setPosition( -(23 * %X)+20, -(23 * %Y)+20);
	MaterialLabGui.updateActiveMaterial("scrollDir[" @ %layer @ "]","\"" @ %X SPC %Y @ "\"",%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateScrollSpeed(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%speed = MaterialLabPropertiesWindow-->ScrollSpeedTextEdit.getText();
	MaterialLabGui.updateActiveMaterial("scrollSpeed[" @ %layer @ "]",%speed,%isSlider,%onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateWaveType(%this) {
	for( %radioButton = 0; %radioButton < MaterialLabPropertiesWindow-->WaveButtonContainer.getCount(); %radioButton++ ) {
		if( MaterialLabPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).getValue() == 1 )
			%type = MaterialLabPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).waveType;
	}

	%layer = MaterialLabGui.currentLayer;
	MaterialLabGui.updateActiveMaterial("waveType[" @ %layer @ "]", %type);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateWaveAmp(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%amp = MaterialLabPropertiesWindow-->WaveTextEditAmp.getText();
	MaterialLabGui.updateActiveMaterial("waveAmp[" @ %layer @ "]", %amp, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateWaveFreq(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%freq = MaterialLabPropertiesWindow-->WaveTextEditFreq.getText();
	MaterialLabGui.updateActiveMaterial("waveFreq[" @ %layer @ "]", %freq, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSequenceFPS(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%fps = MaterialLabPropertiesWindow-->SequenceTextEditFPS.getText();
	MaterialLabGui.updateActiveMaterial("sequenceFramePerSec[" @ %layer @ "]", %fps, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSequenceSSS(%this, %isSlider, %onMouseUp) {
	%layer = MaterialLabGui.currentLayer;
	%sss = 1 / MaterialLabPropertiesWindow-->SequenceTextEditSSS.getText();
	MaterialLabGui.updateActiveMaterial("sequenceSegmentSize[" @ %layer @ "]", %sss, %isSlider, %onMouseUp);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateAnimationFlags(%this) {
	MaterialLabGui.setMaterialDirty();
	%single = true;

	if(MaterialLabPropertiesWindow-->RotationAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Rotate";
		else
			%flags = %flags @ " | $Rotate";

		%single = false;
	}

	if(MaterialLabPropertiesWindow-->ScrollAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Scroll";
		else
			%flags = %flags @ " | $Scroll";

		%single = false;
	}

	if(MaterialLabPropertiesWindow-->WaveAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Wave";
		else
			%flags = %flags @ " | $Wave";

		%single = false;
	}

	if(MaterialLabPropertiesWindow-->ScaleAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Scale";
		else
			%flags = %flags @ " | $Scale";

		%single = false;
	}

	if(MaterialLabPropertiesWindow-->SequenceAnimation.getValue() == true) {
		if(%single == true)
			%flags = %flags @ "$Sequence";
		else
			%flags = %flags @ " | $Sequence";

		%single = false;
	}

	if(%flags $= "")
		%flags = "\"\"";

	%action = %this.createUndo(ActionUpdateActiveMaterialAnimationFlags, "Update Active Material");
	%action.material = MaterialLabGui.currentMaterial;
	%action.object = MaterialLabGui.currentObject;
	%action.layer = MaterialLabGui.currentLayer;
	%action.newValue = %flags;
	%oldFlags = MaterialLabGui.currentMaterial.getAnimFlags(MaterialLabGui.currentLayer);

	if(%oldFlags $= "")
		%oldFlags = "\"\"";

	%action.oldValue = %oldFlags;
	MaterialLabGui.submitUndo( %action );
	eval("materialLab_previewMaterial.animFlags[" @ MaterialLabGui.currentLayer @ "] = " @ %flags @ ";");
	materialLab_previewMaterial.flush();
	materialLab_previewMaterial.reload();

	if (MaterialLabGui.livePreview == true) {
		eval("MaterialLabGui.currentMaterial.animFlags[" @ MaterialLabGui.currentLayer @ "] = " @ %flags @ ";");
		MaterialLabGui.currentMaterial.flush();
		MaterialLabGui.currentMaterial.reload();
	}
}
//------------------------------------------------------------------------------
//==============================================================================

//These two functions are focused on object/layer specific functionality
function MaterialLabGui::updateColorMultiply(%this,%color) {
	%propName = "diffuseColor[" @ MaterialLabGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialLabPropertiesWindow-->colorTintSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSpecularCheckbox(%this,%value) {
	MaterialLabGui.updateActiveMaterial("pixelSpecular[" @ MaterialLabGui.currentLayer @ "]", %value);
	MaterialLabGui.guiSync( materialLab_previewMaterial );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSpecular(%this, %color) {
	%propName = "specular[" @ MaterialLabGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialLabPropertiesWindow-->specularColorSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateSubSurfaceColor(%this, %color) {
	%propName = "subSurfaceColor[" @ MaterialLabGui.currentLayer @ "]";
	%this.syncGuiColor(MaterialLabPropertiesWindow-->subSurfaceColorSwatch, %propName, %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateEffectColor0(%this, %color) {
	%this.syncGuiColor(MaterialLabPropertiesWindow-->effectColor0Swatch, "effectColor[0]", %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateEffectColor1(%this, %color) {
	%this.syncGuiColor(MaterialLabPropertiesWindow-->effectColor1Swatch, "effectColor[1]", %color);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateBehaviorSound(%this, %type, %sound) {
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
function MaterialLabGui::updateSoundPopup(%this, %type, %defaultId, %customName) {
	%ctrl = MaterialLabPropertiesWindow.findObjectByInternalName( %type @ "SoundPopup", true );

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
function MaterialLabGui::updateLightColor(%this, %color) {
	matLab_previewObjectView.setLightColor(%color);
	matLab_lightColorPicker.color = %color;
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updatePreviewBackground(%this,%color) {
	matLab_previewBackground.color = %color;
	MaterialPreviewBackgroundPicker.color = %color;
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateAmbientColor(%this,%color) {
	matLab_previewObjectView.setAmbientLightColor(%color);
	matLab_ambientLightColorPicker.color = %color;
}
//------------------------------------------------------------------------------
// channel in selectors
function MaterialLabGui::setRoughChan(%this, %value)
{
   MaterialLabGui.updateActiveMaterial("SmoothnessChan[" @ MaterialLabGui.currentLayer @ "]", %value);   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}

function MaterialLabGui::setAOChan(%this, %value)
{
   MaterialLabGui.updateActiveMaterial("aoChan[" @ MaterialLabGui.currentLayer @ "]", %value);   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}

function MaterialLabGui::setMetalChan(%this, %value)
{
   MaterialLabGui.updateActiveMaterial("metalChan[" @ MaterialLabGui.currentLayer @ "]", %value);   
   MaterialLabGui.guiSync( materialLab_previewMaterial );
}
