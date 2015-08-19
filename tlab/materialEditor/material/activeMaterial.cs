//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
// Helper functions to help load and update the preview and active material
//------------------------------------------------------------------------------
//==============================================================================
// Finds the selected line in the material list, then makes it active in the editor.
function MaterialEditorGui::prepareActiveMaterial(%this, %material, %override) {
	// If were not valid, grab the first valid material out of the materialSet
	if( !isObject(%material) )
		%material = MaterialSet.getObject(0);

	// Check made in order to avoid loading the same material. Overriding
	// made in special cases
	if(%material $= MaterialEditorGui.lastMaterial && !%override) {
		return;
	} else {
		if(MaterialEditorGui.materialDirty ) {
			MaterialEditorGui.showSaveDialog( %material );
			return;
		}

		MaterialEditorGui.setActiveMaterial(%material);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Updates the preview material to use the same properties as the selected material,
// and makes that material active in the editor.
function MaterialEditorGui::setActiveMaterial( %this, %material ) {
	// Warn if selecting a CustomMaterial (they can't be properly previewed or edited)
	if ( isObject( %material ) && %material.isMemberOfClass( "CustomMaterial" ) ) {
		LabMsgOK( "Warning", "The selected Material (" @ %material.getName() @
					 ") is a CustomMaterial, and cannot be edited using the Material Editor." );
		return;
	}

	if (strFind(%material.getFilename(),"tlab") || %material.getFilename() $= "") {
		devlog("Changind material filename from:",%material.getFilename(),"To",$Pref::MaterialEditorGui::DefaultMaterialFile);
		%material.setFilename($Pref::MaterialEditorGui::DefaultMaterialFile);
	}
	
	MaterialEditorGui.currentMaterial = %material;
	MaterialEditorGui.lastMaterial = %material;
	%this.setActiveMaterialFile(%material);
	// we create or recreate a material to hold in a pristine state
	singleton Material(notDirtyMaterial) {
		mapTo = "matEd_previewMat";
		diffuseMap[0] = "tlab/materialEditor/assets/matEd_mappedMat";
	};
	// Converts the texture files into absolute paths.
	MaterialEditorGui.convertTextureFields();
	// If we're allowing for name changes, make sure to save the name seperately
	%this.originalName = MaterialEditorGui.currentMaterial.name;
	// Copy materials over to other references
	MaterialEditorGui.copyMaterials( MaterialEditorGui.currentMaterial, materialEd_previewMaterial );
	MaterialEditorGui.copyMaterials( MaterialEditorGui.currentMaterial, notDirtyMaterial );
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
	// Necessary functionality in order to render correctly
	materialEd_previewMaterial.flush();
	materialEd_previewMaterial.reload();
	MaterialEditorGui.currentMaterial.flush();
	MaterialEditorGui.currentMaterial.reload();
	MaterialEditorGui.setMaterialNotDirty();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::setActiveMaterialFile( %this, %material ) {
	MatEdTargetMode-->selMaterialFile.setText(%material.getFilename());
	MatEdMaterialMode-->selMaterialFile.setText(%material.getFilename());
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateActiveMaterial(%this, %propertyField, %value, %isSlider, %onMouseUp) {	
	logc(" MaterialEditorGui::updateActiveMaterial(%this, %propertyField, %value, %isSlider, %onMouseUp)", %this, %propertyField, %value, %isSlider, %onMouseUp);
	MaterialEditorGui.setMaterialDirty();

	if(%value $= "")
		%value = "\"\"";

	// Here is where we handle undo actions with slider controls. We want to be able to
	// undo every onMouseUp; so we overrite the same undo action when necessary in order
	// to achieve this desired effect.
	%last = Editor.getUndoManager().getUndoAction(Editor.getUndoManager().getUndoCount() - 1);

	if((%last != -1) && (%last.isSlider) && (!%last.onMouseUp)) {
		%last.field = %propertyField;
		%last.isSlider = %isSlider;
		%last.onMouseUp = %onMouseUp;
		%last.newValue = %value;
	} else {
		%action = %this.createUndo(ActionUpdateActiveMaterial, "Update Active Material");
		%action.material = MaterialEditorGui.currentMaterial;
		%action.object = MaterialEditorGui.currentObject;
		%action.field = %propertyField;
		%action.isSlider = %isSlider;
		%action.onMouseUp = %onMouseUp;
		%action.newValue = %value;
		eval( "%action.oldValue = " @ MaterialEditorGui.currentMaterial @ "." @ %propertyField @ ";");
		%action.oldValue = "\"" @ %action.oldValue @ "\"";
		MaterialEditorGui.submitUndo( %action );
	}

	eval("materialEd_previewMaterial." @ %propertyField @ " = " @ %value @ ";");
	materialEd_previewMaterial.flush();
	materialEd_previewMaterial.reload();

	if (MaterialEditorGui.livePreview == true) {
		eval("MaterialEditorGui.currentMaterial." @ %propertyField @ " = " @ %value @ ";");
		MaterialEditorGui.currentMaterial.flush();
		MaterialEditorGui.currentMaterial.reload();
	}

	if(strFind(%propertyField,"diffuseMap") && %autoUpdateNormal) {
		%diffuseSuffix = MaterialEditorPlugin.getCfg("DiffuseSuffix");
		%cleanValue = strreplace(%value,%diffuseSuffix,"");
		%autoUpdateNormal = MaterialEditorPlugin.getCfg("AutoAddNormal");

		if (%autoUpdateNormal) {
			%normalMap = strreplace(%propertyField,"diffuse","normal");
			%normalFile = MaterialEditorGui.currentMaterial.getFieldValue(%normalMap);
			%isFileNormal = isImageFile(%normalFile);
			devLog("Updating texture map:",%cleanValue,"Current Normal:",%normalFile,"IsFile",%isFileNormal);

			if (!%isFileNormal) {
				%normalSuffix = MaterialEditorPlugin.getCfg("NormalSuffix");
				%fileBase = fileBase(%cleanValue);
				%filePath = filePath(%cleanValue);
				%testNormal = %filePath@"/"@%fileBase@%normalSuffix;
				%isFileNew = isImageFile(%testNormal);

				if (%isFileNew)
					%this.updateTextureMapImage("normal",%testNormal,0);
			}
		}

		%autoUpdateSpecular = MaterialEditorPlugin.getCfg("AutoAddSpecular");

		if (%autoUpdateSpecular) {
			%specMap = strreplace(%propertyField,"diffuse","specular");
			%specFile = MaterialEditorGui.currentMaterial.getFieldValue(%specMap);
			%isFileSpec = isImageFile(%specFile);
			devLog("Updating texture map:",%cleanValue,"Current Specular:",%specFile,"IsFile",%isFileSpec);

			if (!%isFileSpec) {
				%specSuffix = MaterialEditorPlugin.getCfg("SpecularSuffix");
				%fileBase = fileBase(%cleanValue);
				%filePath = filePath(%cleanValue);
				%testSpec = %filePath@"/"@%fileBase@%specSuffix;
				//%testSpec = strreplace(%testSpec,"\"","");
				%isFileNew = isImageFile(%testSpec);
				devLog("TestSpec = ",%testSpec);

				if (%isFileNew)
					%this.updateTextureMapImage("spec",%testSpec,0);
			}
		}
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateActiveMaterialName(%this, %name) {
	%action = %this.createUndo(ActionUpdateActiveMaterialName, "Update Active Material Name");
	%action.material =  MaterialEditorGui.currentMaterial;
	%action.object = MaterialEditorGui.currentObject;
	%action.oldName = MaterialEditorGui.currentMaterial.getName();
	%action.newName = %name;
	MaterialEditorGui.submitUndo( %action );
	MaterialEditorGui.currentMaterial.setName(%name);
	// Some objects (ConvexShape, DecalRoad etc) reference Materials by name => need
	// to find and update all these references so they don't break when we rename the
	// Material.
	MaterialEditorGui.updateMaterialReferences( MissionGroup, %action.oldName, %action.newName );
}


