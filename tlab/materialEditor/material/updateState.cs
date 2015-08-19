//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Clear and Refresh Material
function MaterialEditorGui::clearMaterial(%this) {
	%action = %this.createUndo(ActionClearMaterial, "Clear Material");
	%action.material = MaterialEditorGui.currentMaterial;
	%action.object = MaterialEditorGui.currentObject;
	pushInstantGroup();
	%action.oldMaterial = new Material();
	%action.newMaterial = new Material();
	popInstantGroup();
	MaterialEditorGui.submitUndo( %action );
	MaterialEditorGui.copyMaterials( MaterialEditorGui.currentMaterial, %action.oldMaterial );
	%tempMat = new Material() {
		name = "tempMaterial";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	MaterialEditorGui.copyMaterials( %tempMat, materialEd_previewMaterial );
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
	materialEd_previewMaterial.flush();
	materialEd_previewMaterial.reload();

	if (MaterialEditorGui.livePreview == true) {
		MaterialEditorGui.copyMaterials( %tempMat, MaterialEditorGui.currentMaterial );
		MaterialEditorGui.currentMaterial.flush();
		MaterialEditorGui.currentMaterial.reload();
	}

	MaterialEditorGui.setMaterialDirty();
	%tempMat.delete();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::refreshMaterial(%this) {
	%action = %this.createUndo(ActionRefreshMaterial, "Refresh Material");
	%action.material = MaterialEditorGui.currentMaterial;
	%action.object = MaterialEditorGui.currentObject;
	pushInstantGroup();
	%action.oldMaterial = new Material();
	%action.newMaterial = new Material();
	popInstantGroup();
	MaterialEditorGui.copyMaterials( MaterialEditorGui.currentMaterial, %action.oldMaterial );
	MaterialEditorGui.copyMaterials( notDirtyMaterial, %action.newMaterial );
	%action.oldName = MaterialEditorGui.currentMaterial.getName();
	%action.newName = %this.originalName;
	MaterialEditorGui.submitUndo( %action );
	MaterialEditorGui.currentMaterial.setName( %this.originalName );
	MaterialEditorGui.copyMaterials( notDirtyMaterial, materialEd_previewMaterial );
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
	materialEd_previewMaterial.flush();
	materialEd_previewMaterial.reload();

	if (MaterialEditorGui.livePreview == true) {
		MaterialEditorGui.copyMaterials( notDirtyMaterial, MaterialEditorGui.currentMaterial );
		MaterialEditorGui.currentMaterial.flush();
		MaterialEditorGui.currentMaterial.reload();
	}

	MaterialEditorGui.setMaterialNotDirty();
}
//------------------------------------------------------------------------------
//==============================================================================
// Switching and Changing Materials
function MaterialEditorGui::switchMaterial( %this, %material ) {
	//MaterialEditorGui.currentMaterial = %material.getId();
	MaterialEditorGui.currentObject = "";
	MaterialEditorGui.setMode();
	MaterialEditorGui.prepareActiveMaterial( %material.getId(), true );
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
function MaterialEditorGui::changeMaterial(%this, %fromMaterial, %toMaterial) {
	%action = %this.createUndo(ActionChangeMaterial, "Change Material");
	%action.object = MaterialEditorGui.currentObject;
	%materialTarget = SubMaterialSelector.text;
	%action.materialTarget = %materialTarget;
	%action.fromMaterial = %fromMaterial;
	%action.toMaterial = %toMaterial;
	%action.toMaterialOldFname = %toMaterial.getFilename();
	%action.object =  MaterialEditorGui.currentObject;

	if( MaterialEditorGui.currentMeshMode $= "Model" ) { // Models
		%action.mode = "model";
		MaterialEditorGui.currentObject.changeMaterial( %materialTarget, %fromMaterial.getName(), %toMaterial.getName() );

		if( MaterialEditorGui.currentObject.shapeName !$= "" )
			%sourcePath = MaterialEditorGui.currentObject.shapeName;
		else if( MaterialEditorGui.currentObject.isMethod("getDatablock") ) {
			if( MaterialEditorGui.currentObject.getDatablock().shapeFile !$= "" )
				%sourcePath = MaterialEditorGui.currentObject.getDatablock().shapeFile;
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
		MaterialEditorGui.prepareActiveMaterial( %toMaterial, true );

		if( !MaterialEditorGui.isMatEditorMaterial( %toMaterial ) ) {
			matEd_PersistMan.removeObjectFromFile(%toMaterial);
		}

		matEd_PersistMan.setDirty(%fromMaterial);
		matEd_PersistMan.setDirty(%toMaterial, %fileName);
		matEd_PersistMan.saveDirty();
		matEd_PersistMan.removeDirty(%fromMaterial);
		matEd_PersistMan.removeDirty(%toMaterial);
	} else { // EditorShapes
		%action.mode = "editorShapes";
		eval("MaterialEditorGui.currentObject." @ SubMaterialSelector.getText() @ " = " @ %toMaterial.getName() @ ";");

		if( MaterialEditorGui.currentObject.isMethod("postApply") )
			MaterialEditorGui.currentObject.postApply();

		MaterialEditorGui.prepareActiveMaterial( %toMaterial, true );
	}

	MaterialEditorGui.submitUndo( %action );
}
//------------------------------------------------------------------------------