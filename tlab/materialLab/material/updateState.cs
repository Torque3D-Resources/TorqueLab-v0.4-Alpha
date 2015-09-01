//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Clear and Refresh Material
function MaterialLabGui::clearMaterial(%this) {
	%action = %this.createUndo(ActionClearMaterial, "Clear Material");
	%action.material = MaterialLabGui.currentMaterial;
	%action.object = MaterialLabGui.currentObject;
	pushInstantGroup();
	%action.oldMaterial = new Material();
	%action.newMaterial = new Material();
	popInstantGroup();
	MaterialLabGui.submitUndo( %action );
	MaterialLabGui.copyMaterials( MaterialLabGui.currentMaterial, %action.oldMaterial );
	%tempMat = new Material() {
		name = "tempMaterial";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	MaterialLabGui.copyMaterials( %tempMat, materialLab_previewMaterial );
	MaterialLabGui.guiSync( materialLab_previewMaterial );
	materialLab_previewMaterial.flush();
	materialLab_previewMaterial.reload();

	if (MaterialLabGui.livePreview == true) {
		MaterialLabGui.copyMaterials( %tempMat, MaterialLabGui.currentMaterial );
		MaterialLabGui.currentMaterial.flush();
		MaterialLabGui.currentMaterial.reload();
	}

	MaterialLabGui.setMaterialDirty();
	%tempMat.delete();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::refreshMaterial(%this) {
	%action = %this.createUndo(ActionRefreshMaterial, "Refresh Material");
	%action.material = MaterialLabGui.currentMaterial;
	%action.object = MaterialLabGui.currentObject;
	pushInstantGroup();
	%action.oldMaterial = new Material();
	%action.newMaterial = new Material();
	popInstantGroup();
	MaterialLabGui.copyMaterials( MaterialLabGui.currentMaterial, %action.oldMaterial );
	MaterialLabGui.copyMaterials( notDirtyMaterial, %action.newMaterial );
	%action.oldName = MaterialLabGui.currentMaterial.getName();
	%action.newName = %this.originalName;
	MaterialLabGui.submitUndo( %action );
	MaterialLabGui.currentMaterial.setName( %this.originalName );
	MaterialLabGui.copyMaterials( notDirtyMaterial, materialLab_previewMaterial );
	MaterialLabGui.guiSync( materialLab_previewMaterial );
	materialLab_previewMaterial.flush();
	materialLab_previewMaterial.reload();

	if (MaterialLabGui.livePreview == true) {
		MaterialLabGui.copyMaterials( notDirtyMaterial, MaterialLabGui.currentMaterial );
		MaterialLabGui.currentMaterial.flush();
		MaterialLabGui.currentMaterial.reload();
	}

	MaterialLabGui.setMaterialNotDirty();
}
//------------------------------------------------------------------------------
//==============================================================================
// Switching and Changing Materials
function MaterialLabGui::switchMaterial( %this, %material ) {
	//MaterialLabGui.currentMaterial = %material.getId();
	MaterialLabGui.currentObject = "";
	MaterialLabGui.setMode();
	MaterialLabGui.prepareActiveMaterial( %material.getId(), true );
}
/*------------------------------------------------------------------------------
 This changes the map to's of possibly two materials (%fromMaterial, %toMaterial)
 and updates the engines libraries accordingly in order to make this change per
 object/per objects instances/per target. Before this functionality is enacted,
 there is a popup beforehand that will ask if you are sure if you want to make
 this change. Making this change will physically alter possibly two materials.cs
 files in order to move the (%fromMaterial, %toMaterial), replacing the
 (%fromMaterials)'s mapTo to "unmapped_mat".
-------------------------------------------------------------------------------*/

//==============================================================================
function MaterialLabGui::changeMaterial(%this, %fromMaterial, %toMaterial) {
	%action = %this.createUndo(ActionChangeMaterial, "Change Material");
	%action.object = MaterialLabGui.currentObject;
	%materialTarget = SubMaterialSelector.text;
	%action.materialTarget = %materialTarget;
	%action.fromMaterial = %fromMaterial;
	%action.toMaterial = %toMaterial;
	%action.toMaterialOldFname = %toMaterial.getFilename();
	%action.object =  MaterialLabGui.currentObject;

	if( MaterialLabGui.currentMeshMode $= "Model" ) { // Models
		%action.mode = "model";
		MaterialLabGui.currentObject.changeMaterial( %materialTarget, %fromMaterial.getName(), %toMaterial.getName() );

		if( MaterialLabGui.currentObject.shapeName !$= "" )
			%sourcePath = MaterialLabGui.currentObject.shapeName;
		else if( MaterialLabGui.currentObject.isMethod("getDatablock") ) {
			if( MaterialLabGui.currentObject.getDatablock().shapeFile !$= "" )
				%sourcePath = MaterialLabGui.currentObject.getDatablock().shapeFile;
		}

		// Creating "to" path
		%k = 0;

		while( strpos( %sourcePath, "/", %k ) != -1 ) {
			%count = strpos( %sourcePath, "/", %k );
			%k = %count + 1;
		}

		%fileName = getSubStr( %sourcePath , 0 , %k );
		%fileName = %fileName @ "materials.cs";
		%action.toMaterialNewFname = %fileName;
		MaterialLabGui.prepareActiveMaterial( %toMaterial, true );

		if( !MaterialLabGui.isMatLabitorMaterial( %toMaterial ) ) {
			matLab_PersistMan.removeObjectFromFile(%toMaterial);
		}

		matLab_PersistMan.setDirty(%fromMaterial);
		matLab_PersistMan.setDirty(%toMaterial, %fileName);
		matLab_PersistMan.saveDirty();
		matLab_PersistMan.removeDirty(%fromMaterial);
		matLab_PersistMan.removeDirty(%toMaterial);
	} else { // EditorShapes
		%action.mode = "editorShapes";
		eval("MaterialLabGui.currentObject." @ SubMaterialSelector.getText() @ " = " @ %toMaterial.getName() @ ";");

		if( MaterialLabGui.currentObject.isMethod("postApply") )
			MaterialLabGui.currentObject.postApply();

		MaterialLabGui.prepareActiveMaterial( %toMaterial, true );
	}

	MaterialLabGui.submitUndo( %action );
}
//------------------------------------------------------------------------------