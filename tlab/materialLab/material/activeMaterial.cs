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
function MaterialLabGui::prepareActiveMaterial(%this, %material, %override) {
	// If were not valid, grab the first valid material out of the materialSet
	if( !isObject(%material) )
		%material = MaterialSet.getObject(0);

	// Check made in order to avoid loading the same material. Overriding
	// made in special cases
	if(%material $= MaterialLabGui.lastMaterial && !%override) {
		return;
	} else {
		if(MaterialLabGui.materialDirty ) {
			MaterialLabGui.showSaveDialog( %material );
			return;
		}

		MaterialLabGui.setActiveMaterial(%material);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Updates the preview material to use the same properties as the selected material,
// and makes that material active in the editor.
function MaterialLabGui::setActiveMaterial( %this, %material ) {
	// Warn if selecting a CustomMaterial (they can't be properly previewed or edited)
	if ( isObject( %material ) && %material.isMemberOfClass( "CustomMaterial" ) ) {
		LabMsgOK( "Warning", "The selected Material (" @ %material.getName() @
					 ") is a CustomMaterial, and cannot be edited using the Material Editor." );
		return;
	}

	if (strFind(%material.getFilename(),"tlab") || %material.getFilename() $= "") {
		devlog("Changind material filename from:",%material.getFilename(),"To",$Pref::MaterialLabGui::DefaultMaterialFile);
		%material.setFilename($Pref::MaterialLabGui::DefaultMaterialFile);
	}
	
	MaterialLabGui.currentMaterial = %material;
	MaterialLabGui.lastMaterial = %material;
	%this.setActiveMaterialFile(%material);
	// we create or recreate a material to hold in a pristine state
	singleton Material(notDirtyMaterial) {
		mapTo = "matLab_previewMat";
		diffuseMap[0] = "tlab/materialLab/assets/matLab_mappedMat";
	};
	// Converts the texture files into absolute paths.
	MaterialLabGui.convertTextureFields();
	// If we're allowing for name changes, make sure to save the name seperately
	%this.originalName = MaterialLabGui.currentMaterial.name;
	// Copy materials over to other references
	MaterialLabGui.copyMaterials( MaterialLabGui.currentMaterial, materialLab_previewMaterial );
	MaterialLabGui.copyMaterials( MaterialLabGui.currentMaterial, notDirtyMaterial );
	MaterialLabGui.guiSync( materialLab_previewMaterial );
	// Necessary functionality in order to render correctly
	materialLab_previewMaterial.flush();
	materialLab_previewMaterial.reload();
	MaterialLabGui.currentMaterial.flush();
	MaterialLabGui.currentMaterial.reload();
	MaterialLabGui.setMaterialNotDirty();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::setActiveMaterialFile( %this, %material ) {
	MatLabTargetMode-->selMaterialFile.setText(%material.getFilename());
	MatLabMaterialMode-->selMaterialFile.setText(%material.getFilename());
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateActiveMaterial(%this, %propertyField, %value, %isSlider, %onMouseUp) {	
	logc(" MaterialLabGui::updateActiveMaterial(%this, %propertyField, %value, %isSlider, %onMouseUp)", %this, %propertyField, %value, %isSlider, %onMouseUp);
	MaterialLabGui.setMaterialDirty();

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
		%action.material = MaterialLabGui.currentMaterial;
		%action.object = MaterialLabGui.currentObject;
		%action.field = %propertyField;
		%action.isSlider = %isSlider;
		%action.onMouseUp = %onMouseUp;
		%action.newValue = %value;
		eval( "%action.oldValue = " @ MaterialLabGui.currentMaterial @ "." @ %propertyField @ ";");
		%action.oldValue = "\"" @ %action.oldValue @ "\"";
		MaterialLabGui.submitUndo( %action );
	}

	eval("materialLab_previewMaterial." @ %propertyField @ " = " @ %value @ ";");
	materialLab_previewMaterial.flush();
	materialLab_previewMaterial.reload();

	if (MaterialLabGui.livePreview == true) {
		eval("MaterialLabGui.currentMaterial." @ %propertyField @ " = " @ %value @ ";");
		MaterialLabGui.currentMaterial.flush();
		MaterialLabGui.currentMaterial.reload();
	}

	if(strFind(%propertyField,"diffuseMap") && %autoUpdateNormal) {
		%diffuseSuffix = MaterialLabPlugin.getCfg("DiffuseSuffix");
		%cleanValue = strreplace(%value,%diffuseSuffix,"");
		%autoUpdateNormal = MaterialLabPlugin.getCfg("AutoAddNormal");

		if (%autoUpdateNormal) {
			%normalMap = strreplace(%propertyField,"diffuse","normal");
			%normalFile = MaterialLabGui.currentMaterial.getFieldValue(%normalMap);
			%isFileNormal = isImageFile(%normalFile);
			devLog("Updating texture map:",%cleanValue,"Current Normal:",%normalFile,"IsFile",%isFileNormal);

			if (!%isFileNormal) {
				%normalSuffix = MaterialLabPlugin.getCfg("NormalSuffix");
				%fileBase = fileBase(%cleanValue);
				%filePath = filePath(%cleanValue);
				%testNormal = %filePath@"/"@%fileBase@%normalSuffix;
				%isFileNew = isImageFile(%testNormal);

				if (%isFileNew)
					%this.updateTextureMapImage("normal",%testNormal,0);
			}
		}

		%autoUpdateSpecular = MaterialLabPlugin.getCfg("AutoAddSpecular");

		if (%autoUpdateSpecular) {
			%specMap = strreplace(%propertyField,"diffuse","specular");
			%specFile = MaterialLabGui.currentMaterial.getFieldValue(%specMap);
			%isFileSpec = isImageFile(%specFile);
			devLog("Updating texture map:",%cleanValue,"Current Specular:",%specFile,"IsFile",%isFileSpec);

			if (!%isFileSpec) {
				%specSuffix = MaterialLabPlugin.getCfg("SpecularSuffix");
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
function MaterialLabGui::updateActiveMaterialName(%this, %name) {
	%action = %this.createUndo(ActionUpdateActiveMaterialName, "Update Active Material Name");
	%action.material =  MaterialLabGui.currentMaterial;
	%action.object = MaterialLabGui.currentObject;
	%action.oldName = MaterialLabGui.currentMaterial.getName();
	%action.newName = %name;
	MaterialLabGui.submitUndo( %action );
	MaterialLabGui.currentMaterial.setName(%name);
	// Some objects (ConvexShape, DecalRoad etc) reference Materials by name => need
	// to find and update all these references so they don't break when we rename the
	// Material.
	MaterialLabGui.updateMaterialReferences( MissionGroup, %action.oldName, %action.newName );
}


