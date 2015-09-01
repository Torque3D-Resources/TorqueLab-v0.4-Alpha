//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Automated editor plugin setting interface
function MaterialLabGui::initGui(%this,%params ) {
	advancedTextureMapsRollout.Expanded = false;
	materialAnimationPropertiesRollout.Expanded = false;
	materialAdvancedPropertiesRollout.Expanded = false;
	%this-->previewOptions.expanded = false;
}
//------------------------------------------------------------------------------

//==============================================================================
function MaterialLabGui::updatePreviewObject(%this) {
	%newModel = matLab_quickPreview_Popup.getValue();

	switch$(%newModel) {
	case "sphere":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/spherePreview.dts");
		matLab_previewObjectView.setOrbitDistance(4);

	case "cube":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/cubePreview.dts");
		matLab_previewObjectView.setOrbitDistance(5);

	case "pyramid":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/pyramidPreview.dts");
		matLab_previewObjectView.setOrbitDistance(5);

	case "cylinder":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/cylinderPreview.dts");
		matLab_previewObjectView.setOrbitDistance(4.2);

	case "torus":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/torusPreview.dts");
		matLab_previewObjectView.setOrbitDistance(4.2);

	case "knot":
		matLab_quickPreview_Popup.selected = %newModel;
		matLab_previewObjectView.setModel("tlab/materialLab/assets/torusknotPreview.dts");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateLivePreview(%this,%preview) {
	// When checkbox is selected, preview the material in real time, if not; then don't
	if( %preview )
		MaterialLabGui.copyMaterials( materialLab_previewMaterial, MaterialLabGui.currentMaterial );
	else
		MaterialLabGui.copyMaterials( notDirtyMaterial, MaterialLabGui.currentMaterial );

	MaterialLabGui.currentMaterial.flush();
	MaterialLabGui.currentMaterial.reload();
}
//------------------------------------------------------------------------------
//==============================================================================

function MaterialLabGui::guiSync( %this, %material ) {
	%this.preventUndo = true;

	//Setup our headers
	if( MaterialLabGui.currentMode $= "material" ) {
		MatLabMaterialMode-->selMaterialName.setText(MaterialLabGui.currentMaterial.name);
		MatLabMaterialMode-->selMaterialMapTo.setText(MaterialLabGui.currentMaterial.mapTo);
	} else {
		if( MaterialLabGui.currentObject.isMethod("getModelFile") ) {
			%sourcePath = MaterialLabGui.currentObject.getModelFile();

			if( %sourcePath !$= "" ) {
				MatLabTargetMode-->selMaterialMapTo.ToolTip = %sourcePath;
				%sourceName = fileName(%sourcePath);
				MatLabTargetMode-->selMaterialMapTo.setText(%sourceName);
				MatLabTargetMode-->selMaterialName.setText(MaterialLabGui.currentMaterial.name);
			}
		} else {
			%info = MaterialLabGui.currentObject.getClassName();
			MatLabTargetMode-->selMaterialMapTo.ToolTip = %info;
			MatLabTargetMode-->selMaterialMapTo.setText(%info);
			MatLabTargetMode-->selMaterialName.setText(MaterialLabGui.currentMaterial.name);
		}
	}

	MaterialLabPropertiesWindow-->alphaRefTextEdit.setText((%material).alphaRef);
	MaterialLabPropertiesWindow-->alphaRefSlider.setValue((%material).alphaRef);
	MaterialLabPropertiesWindow-->doubleSidedCheckBox.setValue((%material).doubleSided);
	MaterialLabPropertiesWindow-->transZWriteCheckBox.setValue((%material).translucentZWrite);
	MaterialLabPropertiesWindow-->alphaTestCheckBox.setValue((%material).alphaTest);
	MaterialLabPropertiesWindow-->castShadows.setValue((%material).castShadows);
	
	MaterialLabPropertiesWindow-->castDynamicShadows.setValue((%material).castDynamicShadows);//PBR Scripts

	MaterialLabPropertiesWindow-->translucentCheckbox.setValue((%material).translucent);

	switch$((%material).translucentBlendOp) {
	case "None":
		%selectedNum = 0;

	case "Mul":
		%selectedNum = 1;

	case "Add":
		%selectedNum = 2;

	case "AddAlpha":
		%selectedNum = 3;

	case "Sub":
		%selectedNum = 4;

	case "LerpAlpha":
		%selectedNum = 5;
	}

	MaterialLabPropertiesWindow-->blendingTypePopUp.setSelected(%selectedNum);

	if((%material).cubemap !$= "") {
		MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(1);
		MaterialLabPropertiesWindow-->reflectionTypePopUp.setSelected(1);
	} else if((%material).dynamiccubemap) {
		MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(0);
		MaterialLabPropertiesWindow-->reflectionTypePopUp.setSelected(2);
	} else if((%material).planarReflection) {
		MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(0);
		MaterialLabPropertiesWindow-->reflectionTypePopUp.setSelected(3);
	} else {
		MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(0);
		MaterialLabPropertiesWindow-->reflectionTypePopUp.setSelected(0);
	}

	MaterialLabPropertiesWindow-->effectColor0Swatch.color = (%material).effectColor[0];
	MaterialLabPropertiesWindow-->effectColor1Swatch.color = (%material).effectColor[1];
	MaterialLabPropertiesWindow-->showFootprintsCheckbox.setValue((%material).showFootprints);
	MaterialLabPropertiesWindow-->showDustCheckbox.setValue((%material).showDust);
	MaterialLabGui.updateSoundPopup("Footstep", (%material).footstepSoundId, (%material).customFootstepSound);
	MaterialLabGui.updateSoundPopup("Impact", (%material).impactSoundId, (%material).customImpactSound);
	//layer specific controls are located here
	%layer = MaterialLabGui.currentLayer;

	if((%material).diffuseMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->diffuseMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->diffuseMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->diffuseMapNameText.setText( (%material).diffuseMap[%layer] );
		MaterialLabPropertiesWindow-->diffuseMapDisplayBitmap.setBitmap( (%material).diffuseMap[%layer] );
	}

	if((%material).normalMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->normalMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->normalMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->normalMapNameText.setText( (%material).normalMap[%layer] );
		MaterialLabPropertiesWindow-->normalMapDisplayBitmap.setBitmap( (%material).normalMap[%layer] );
	}

	if((%material).overlayMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->overlayMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->overlayMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->overlayMapNameText.setText( (%material).overlayMap[%layer] );
		MaterialLabPropertiesWindow-->overlayMapDisplayBitmap.setBitmap( (%material).overlayMap[%layer] );
	}

	if((%material).detailMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->detailMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->detailMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->detailMapNameText.setText( (%material).detailMap[%layer] );
		MaterialLabPropertiesWindow-->detailMapDisplayBitmap.setBitmap( (%material).detailMap[%layer] );
	}

	if((%material).detailNormalMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->detailNormalMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->detailNormalMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->detailNormalMapNameText.setText( (%material).detailNormalMap[%layer] );
		MaterialLabPropertiesWindow-->detailNormalMapDisplayBitmap.setBitmap( (%material).detailNormalMap[%layer] );
	}

	if((%material).lightMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->lightMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->lightMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->lightMapNameText.setText( (%material).lightMap[%layer] );
		MaterialLabPropertiesWindow-->lightMapDisplayBitmap.setBitmap( (%material).lightMap[%layer] );
	}

	if((%material).toneMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->toneMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->toneMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->toneMapNameText.setText( (%material).toneMap[%layer] );
		MaterialLabPropertiesWindow-->toneMapDisplayBitmap.setBitmap( (%material).toneMap[%layer] );
	}
	//PBR Scripts
	MaterialLabPropertiesWindow-->FlipRBCheckbox.setValue((%material).FlipRB[%layer]);
   MaterialLabPropertiesWindow-->invertSmoothnessCheckbox.setValue((%material).invertSmoothness[%layer]);

	if((%material).specularMap[%layer] $= "") {
		MaterialLabPropertiesWindow-->specMapNameText.setText( "None" );
		MaterialLabPropertiesWindow-->specMapDisplayBitmap.setBitmap( "tlab/materialLab/assets/unknownImage" );
	} else {
		MaterialLabPropertiesWindow-->specMapNameText.setText( (%material).specularMap[%layer] );
		MaterialLabPropertiesWindow-->specMapDisplayBitmap.setBitmap( (%material).specularMap[%layer] );
	}
	
	//PBR Script
	if((%material).roughMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->roughMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->roughMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->roughMapNameText.setText( (%material).roughMap[%layer] );
      MaterialLabPropertiesWindow-->roughMapDisplayBitmap.setBitmap( (%material).roughMap[%layer] );
   }
   
   if((%material).aoMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->aoMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->aoMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->aoMapNameText.setText( (%material).aoMap[%layer] );
      MaterialLabPropertiesWindow-->aoMapDisplayBitmap.setBitmap( (%material).aoMap[%layer] );
   }
   
   if((%material).metalMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->metalMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->metalMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->metalMapNameText.setText( (%material).metalMap[%layer] );
      MaterialLabPropertiesWindow-->metalMapDisplayBitmap.setBitmap( (%material).metalMap[%layer] );
   }
   
   // material damage
      
   if((%material).albedoDamageMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->albedoDamageMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->albedoDamageMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->albedoDamageMapNameText.setText( (%material).albedoDamageMap[%layer] );
      MaterialLabPropertiesWindow-->albedoDamageMapDisplayBitmap.setBitmap( (%material).albedoDamageMap[%layer] );
   }
   
   if((%material).normalDamageMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->normalDamageMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->normalDamageMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->normalDamageMapNameText.setText( (%material).normalDamageMap[%layer] );
      MaterialLabPropertiesWindow-->normalDamageMapDisplayBitmap.setBitmap( (%material).normalDamageMap[%layer] );
   }
   
   if((%material).compositeDamageMap[%layer] $= "") 
   {
      MaterialLabPropertiesWindow-->compositeDamageMapNameText.setText( "None" );
      MaterialLabPropertiesWindow-->compositeDamageMapDisplayBitmap.setBitmap( "tools/materialeditor/gui/unknownImage" );
   }
   else
   {
      MaterialLabPropertiesWindow-->compositeDamageMapNameText.setText( (%material).normalDamageMap[%layer] );
      MaterialLabPropertiesWindow-->compositeDamageMapDisplayBitmap.setBitmap( (%material).compositeDamageMap[%layer] );
   }
   
   MaterialLabPropertiesWindow-->minDamageTextEdit.setText((%material).minDamage[%layer]);
   MaterialLabPropertiesWindow-->minDamageSlider.setValue((%material).minDamage[%layer]);
	//PBR Script End   

	MaterialLabPropertiesWindow-->detailScaleTextEdit.setText( getWord((%material).detailScale[%layer], 0) );
	MaterialLabPropertiesWindow-->detailNormalStrengthTextEdit.setText( getWord((%material).detailNormalMapStrength[%layer], 0) );
	MaterialLabPropertiesWindow-->colorTintSwatch.color = (%material).diffuseColor[%layer];
	MaterialLabPropertiesWindow-->specularColorSwatch.color = (%material).specular[%layer];
	MaterialLabPropertiesWindow-->specularPowerTextEdit.setText((%material).specularPower[%layer]);
	MaterialLabPropertiesWindow-->specularPowerSlider.setValue((%material).specularPower[%layer]);
	MaterialLabPropertiesWindow-->specularStrengthTextEdit.setText((%material).specularStrength[%layer]);
	MaterialLabPropertiesWindow-->specularStrengthSlider.setValue((%material).specularStrength[%layer]);
	MaterialLabPropertiesWindow-->pixelSpecularCheckbox.setValue((%material).pixelSpecular[%layer]);
	
	//PBR Script
	MaterialLabPropertiesWindow-->SmoothnessTextEdit.setText((%material).Smoothness[%layer]);
   MaterialLabPropertiesWindow-->SmoothnessSlider.setValue((%material).Smoothness[%layer]);
   MaterialLabPropertiesWindow-->MetalnessTextEdit.setText((%material).Metalness[%layer]);
   MaterialLabPropertiesWindow-->MetalnessSlider.setValue((%material).Metalness[%layer]);
	//PBR Script End

	MaterialLabPropertiesWindow-->glowCheckbox.setValue((%material).glow[%layer]);
	MaterialLabPropertiesWindow-->emissiveCheckbox.setValue((%material).emissive[%layer]);
	MaterialLabPropertiesWindow-->parallaxTextEdit.setText((%material).parallaxScale[%layer]);
	MaterialLabPropertiesWindow-->parallaxSlider.setValue((%material).parallaxScale[%layer]);
	MaterialLabPropertiesWindow-->useAnisoCheckbox.setValue((%material).useAnisotropic[%layer]);
	MaterialLabPropertiesWindow-->vertLitCheckbox.setValue((%material).vertLit[%layer]);
	MaterialLabPropertiesWindow-->vertColorSwatch.color = (%material).vertColor[%layer];
	MaterialLabPropertiesWindow-->subSurfaceCheckbox.setValue((%material).subSurface[%layer]);
	MaterialLabPropertiesWindow-->subSurfaceColorSwatch.color = (%material).subSurfaceColor[%layer];
	MaterialLabPropertiesWindow-->subSurfaceRolloffTextEdit.setText((%material).subSurfaceRolloff[%layer]);
	MaterialLabPropertiesWindow-->minnaertTextEdit.setText((%material).minnaertConstant[%layer]);
	// Animation properties
	MaterialLabPropertiesWindow-->RotationAnimation.setValue(0);
	MaterialLabPropertiesWindow-->ScrollAnimation.setValue(0);
	MaterialLabPropertiesWindow-->WaveAnimation.setValue(0);
	MaterialLabPropertiesWindow-->ScaleAnimation.setValue(0);
	MaterialLabPropertiesWindow-->SequenceAnimation.setValue(0);
	%flags = (%material).getAnimFlags(%layer);
	%wordCount = getWordCount( %flags );

	for(%i = 0; %i != %wordCount; %i++) {
		switch$(getWord( %flags, %i)) {
		case "$rotate":
			MaterialLabPropertiesWindow-->RotationAnimation.setValue(1);

		case "$scroll":
			MaterialLabPropertiesWindow-->ScrollAnimation.setValue(1);

		case "$wave":
			MaterialLabPropertiesWindow-->WaveAnimation.setValue(1);

		case "$scale":
			MaterialLabPropertiesWindow-->ScaleAnimation.setValue(1);

		case "$sequence":
			MaterialLabPropertiesWindow-->SequenceAnimation.setValue(1);
		}
	}

	MaterialLabPropertiesWindow-->RotationTextEditU.setText( getWord((%material).rotPivotOffset[%layer], 0) );
	MaterialLabPropertiesWindow-->RotationTextEditV.setText( getWord((%material).rotPivotOffset[%layer], 1) );
	MaterialLabPropertiesWindow-->RotationSpeedTextEdit.setText( (%material).rotSpeed[%layer] );
	MaterialLabPropertiesWindow-->RotationSliderU.setValue( getWord((%material).rotPivotOffset[%layer], 0) );
	MaterialLabPropertiesWindow-->RotationSliderV.setValue( getWord((%material).rotPivotOffset[%layer], 1) );
	MaterialLabPropertiesWindow-->RotationSpeedSlider.setValue( (%material).rotSpeed[%layer] );
	MaterialLabPropertiesWindow-->RotationCrosshair.setPosition( 45*mAbs(getWord((%material).rotPivotOffset[%layer], 0))-2, 45*mAbs(getWord((%material).rotPivotOffset[%layer], 1))-2 );
	MaterialLabPropertiesWindow-->ScrollTextEditU.setText( getWord((%material).scrollDir[%layer], 0) );
	MaterialLabPropertiesWindow-->ScrollTextEditV.setText( getWord((%material).scrollDir[%layer], 1) );
	MaterialLabPropertiesWindow-->ScrollSpeedTextEdit.setText( (%material).scrollSpeed[%layer] );
	MaterialLabPropertiesWindow-->ScrollSliderU.setValue( getWord((%material).scrollDir[%layer], 0) );
	MaterialLabPropertiesWindow-->ScrollSliderV.setValue( getWord((%material).scrollDir[%layer], 1) );
	MaterialLabPropertiesWindow-->ScrollSpeedSlider.setValue( (%material).scrollSpeed[%layer] );
	MaterialLabPropertiesWindow-->ScrollCrosshair.setPosition( -(23 * getWord((%material).scrollDir[%layer], 0))+20, -(23 * getWord((%material).scrollDir[%layer], 1))+20);
	%waveType = (%material).waveType[%layer];

	for( %radioButton = 0; %radioButton < MaterialLabPropertiesWindow-->WaveButtonContainer.getCount(); %radioButton++ ) {
		if( %waveType $= MaterialLabPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).waveType )
			MaterialLabPropertiesWindow-->WaveButtonContainer.getObject(%radioButton).setStateOn(1);
	}

	MaterialLabPropertiesWindow-->WaveTextEditAmp.setText( (%material).waveAmp[%layer] );
	MaterialLabPropertiesWindow-->WaveTextEditFreq.setText( (%material).waveFreq[%layer] );
	MaterialLabPropertiesWindow-->WaveSliderAmp.setValue( (%material).waveAmp[%layer] );
	MaterialLabPropertiesWindow-->WaveSliderFreq.setValue( (%material).waveFreq[%layer] );
	%numFrames = mRound( 1 / (%material).sequenceSegmentSize[%layer] );
	MaterialLabPropertiesWindow-->SequenceTextEditFPS.setText( (%material).sequenceFramePerSec[%layer] );
	MaterialLabPropertiesWindow-->SequenceTextEditSSS.setText( %numFrames );
	MaterialLabPropertiesWindow-->SequenceSliderFPS.setValue( (%material).sequenceFramePerSec[%layer] );
	MaterialLabPropertiesWindow-->SequenceSliderSSS.setValue( %numFrames );
	
	
	// Accumulation PBR Script
   MaterialLabPropertiesWindow-->accuCheckbox.setValue((%material).accuEnabled[%layer]);  
     
   MaterialLabPropertiesWindow-->accuCheckbox.setValue((%material).accuEnabled[%layer]);
   
   %this.getRoughChan((%material).SmoothnessChan[%layer]);
   %this.getAOChan((%material).AOChan[%layer]);
   %this.getMetalChan((%material).metalChan[%layer]);
   //PBR Script End
	%this.preventUndo = false;
}

//------------------------------------------------------------------------------
//PBR Script Functions
//=======================================
function MaterialLabGui::getRoughChan(%this, %channel)
{
    %guiElement = roughChanBtn @ %channel;
    %guiElement.setStateOn(true);
}

function MaterialLabGui::getAOChan(%this, %channel)
{
    %guiElement = AOChanBtn @ %channel;
    %guiElement.setStateOn(true);
}

function MaterialLabGui::getMetalChan(%this, %channel)
{
    %guiElement = metalChanBtn @ %channel;
    %guiElement.setStateOn(true);
}

//=======================================
//PBR script end
//==============================================================================
// Color Picker Helpers - They are all using colorPicker.ed.gui in order to function
// These functions are mainly passed callbacks from getColorI/getColorF callbacks

function MaterialLabGui::syncGuiColor(%this, %guiCtrl, %propname, %color) {
	%layer = MaterialLabGui.currentLayer;
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	%a = getWord(%color,3);
	%colorSwatch = (%r SPC %g SPC %b SPC %a);
	%color = "\"" @ %r SPC %g SPC %b SPC %a @ "\"";
	%guiCtrl.color = %colorSwatch;
	MaterialLabGui.updateActiveMaterial(%propName, %color);
}
//------------------------------------------------------------------------------
