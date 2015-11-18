//==============================================================================
// TorqueLab -> ShapeEditor -> Material Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Edit the selected object material
//==============================================================================
function ShapeEdMaterials::editSelectedMaterial( %this ) {
	if ( isObject( %this.selectedMaterial ) ) {
		%this.updateSelectedMaterial( false );
		%this.editMaterial(%this.selectedMaterial);
	}
}
function ShapeEdMaterials::editMaterial( %this,%material ) {
	if ( !isObject(%material ))
		return;

	// Remove the highlight effect from the selected material, then switch
	// to the Material Editor
	// Create a temporary TSStatic so the MaterialEditor can query the model's
	// materials.
	pushInstantGroup();
	%this.tempShape = new TSStatic() {
		shapeName = ShapeEditor.shape.baseShape;
		collisionType = "None";
	};
	popInstantGroup();
	MaterialEditorGui.currentMaterial = %material;
	MaterialEditorGui.currentObject = $Lab::materialEditorList = %this.tempShape;
	Lab.setEditor(MaterialEditorPlugin);
	show(MEP_CallbackArea);
	MEP_CallbackArea-->callbackButton.text = "Return to ShapeEditor";
	MEP_CallbackArea-->callbackButton.command = "ShapeEdMaterials.editMaterialEnd();";
	//ShapeEdSelectWindow.setVisible( false );
	//ShapeEdPropWindow.setVisible( false );
	//EditorGui-->MatEdPropertiesWindow.setVisible( true );
	//EditorGui-->MatEdPreviewWindow.setVisible( true );
	//MatEd_phoBreadcrumb.setVisible( true );
	//MatEd_phoBreadcrumb.command = "ShapeEdMaterials.editSelectedMaterialEnd();";
	//advancedTextureMapsRollout.Expanded = false;
	//materialAnimationPropertiesRollout.Expanded = false;
	//materialAdvancedPropertiesRollout.Expanded = false;
	//MaterialEditorGui.open();
	//MaterialEditorGui.setActiveMaterial( %this.selectedMaterial );
	%id = SubMaterialSelector.findText( %this.selectedMapTo );

	if( %id != -1 )
		SubMaterialSelector.setSelected( %id );

	Lab.setEditor(MaterialEditorPlugin);
}

function ShapeEdMaterials::editMaterialEnd( %this, %closeEditor ) {
	hide(MEP_CallbackArea);
	Lab.setEditor(ShapeEditorPlugin);
	//MaterialEditorGui.quit();
	//EditorGui-->MatEdPropertiesWindow.setVisible( false );
	//EditorGui-->MatEdPreviewWindow.setVisible( false );
	// Delete the temporary TSStatic
	%this.tempShape.delete();

	if( !%closeEditor ) {
		ShapeEdSelectWindow.setVisible( true );
		ShapeEdPropWindow.setVisible( true );
	}
}
//==============================================================================
// ShapeEditor -> Material Editing
//==============================================================================


function ShapeEd::updateMaterialList( %this ) {
	ShapeEd_MatPillStack.clear();
	// --- MATERIALS TAB ---
	/*ShapeEdMaterialList.clear();
	ShapeEdMaterialList.addRow( -2, "Name" TAB "Mapped" );
	ShapeEdMaterialList.setRowActive( -2, false );
	ShapeEdMaterialList.addRow( -1, "<none>" );*/
	%count = ShapeEditor.shape.getTargetCount();

	for ( %i = 0; %i < %count; %i++ ) {
		%matName = ShapeEditor.shape.getTargetName( %i );
		%mapped = getMaterialMapping( %matName );
		
		if (strFind(%matName,"ColorEffectR") && $ShapeEd_HideColorMaterial)
				continue;
		/*	if ( %mapped $= "" )
				ShapeEdMaterialList.addRow( WarningMaterial.getID(), %matName TAB "unmapped" );
			else
				ShapeEdMaterialList.addRow( %mapped.getID(), %matName TAB %mapped );*/
		%this.addMaterialPill(%matName,%mapped);
	}

	//ShapeEdMaterials-->materialListHeader.setExtent( getWord( ShapeEdMaterialList.extent, 0 ) SPC "19" );
}

function ShapeEd::addMaterialPill( %this,%matName,%mapped ) {
	if ( !isObject(%mapped)) {
		%mapped = "Unmapped";
		%mapId =  WarningMaterial.getID();
	} else
		%mapId =  %mapped.getID();

	
				
	hide(ShapeEd_MatPillSource);
	%pill = cloneObject(ShapeEd_MatPillSource,"",%matName,ShapeEd_MatPillStack);
	%pill-->materialName.setText(%matName);
	%pill-->mapName.setText(%mapped);
	%pill-->MouseArea.pill = %pill;
	%pill-->selectedCtrl.visible = 0;
	%pill-->editButton.command = "ShapeEdMaterials.editMaterial("@%mapId@");";
	%pill-->highlightCheck.visible = 0;
	%pill.mapID = %mapId;
	%pill.mat = %matName;
}

function ShapeEdMaterials::updateSelectedMaterial( %this, %highlight ) {
	// Remove the highlight effect from the old selection
	if ( isObject( %this.selectedMaterial ) ) {
		%this.selectedMaterial.diffuseMap[1] = %this.savedMap;
		%this.selectedMaterial.reload();
	}

	// Apply the highlight effect to the new selected material
	%this.selectedMapTo = ShapeEdMaterials.pendingMap;
	%this.selectedMaterial = ShapeEdMaterials.pendingMaterial;
	%this.savedMap = %this.selectedMaterial.diffuseMap[1];

	if ( %highlight && isObject( %this.selectedMaterial ) ) {
		%this.selectedMaterial.diffuseMap[1] = "tlab/shapeEditor/images/highlight_material";
		%this.selectedMaterial.reload();
	}
}

function ShapeEd_MatPillMouse::onMouseDown( %this, %mod,%point,%clicks ) {
	devLog("ShapeEd_MatPillMouse PILL:",%this.pill,"THIS",%this);

	foreach(%pill in ShapeEd_MatPillStack)
		%pill-->selectedCtrl.visible = 0;

	%this.pill-->selectedCtrl.visible = 1;
	ShapeEdMaterials.pendingMaterial = %this.pill.mapID;
	ShapeEdMaterials.pendingMap = %this.pill-->mapName.text;
	ShapeEdMaterials.updateSelectedMaterial(ShapeEdMaterials-->highlightMaterial.getValue());
}

