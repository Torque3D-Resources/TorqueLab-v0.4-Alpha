//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Create New and Delete Material
//==============================================================================

//==============================================================================
function MaterialEditorGui::createNewMaterial( %this ) {
	%action = %this.createUndo(ActionCreateNewMaterial, "Create New Material");
	%action.object = "";
	%material = getUniqueName( "newMaterial" );
	new Material(%material) {
		diffuseMap[0] = "art/textures/core/warnMat";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	%material.setFileName(MaterialEditorGui-->selMaterialFile.getText());
	%action.newMaterial = %material.getId();
	%action.oldMaterial = MaterialEditorGui.currentMaterial;
	MaterialEditorGui.submitUndo( %action );
	MaterialEditorGui.currentObject = "";
	MaterialEditorGui.setMode();
	MaterialEditorGui.prepareActiveMaterial( %material.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
// Clone selected Material
function MaterialEditorGui::cloneMaterial( %this ) {
	%srcMat = MaterialEditorGui.currentMaterial;

	if (!isObject(%srcMat))
		return;

	%newMat = %srcMat.deepClone();
	%newName = getUniqueName(%srcMat.getName());
	%newMat.setName(%newName);
	%newMat.setFilename(%srcMat.getFilename());
	MaterialEditorGui.currentObject = "";
	MaterialEditorGui.setMode();
	MaterialEditorGui.prepareActiveMaterial( %newMat.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::deleteMaterial( %this ) {
	%action = %this.createUndo(ActionDeleteMaterial, "Delete Material");
	%action.object = MaterialEditorGui.currentObject;
	%action.currentMode = MaterialEditorGui.currentMode;
	/*
	if( MaterialEditorGui.currentMode $= "Mesh" )
	{
	   %materialTarget = SubMaterialSelector.text;
	   %action.materialTarget = %materialTarget;

	   //create the stub material
	   %toMaterial = getUniqueName( "newMaterial" );
	   new Material(%toMaterial)
	   {
	      diffuseMap[0] = "art/textures/core/warnMat";
	      mapTo = "unmapped_mat";
	      parentGroup = RootGroup;
	   };

	   %action.toMaterial = %toMaterial.getId();
	   %action.fromMaterial = MaterialEditorGui.currentMaterial;
	   %action.fromMaterialOldFname = MaterialEditorGui.currentMaterial.getFilename();
	}
	else
	{
	   // Grab first material we see; if theres not one, create one
	   %toMaterial = MaterialSet.getObject(0);
	   if( !isObject( %toMaterial ) )
	   {
	      %toMaterial = getUniqueName( "newMaterial" );
	      new Material(%toMaterial)
	      {
	         diffuseMap[0] = "art/textures/core/warnMat";
	         mapTo = "unmapped_mat";
	         parentGroup = RootGroup;
	      };
	   }

	   %action.toMaterial = %toMaterial.getId();
	   %action.fromMaterial = MaterialEditorGui.currentMaterial;
	}
	*/
	// Grab first material we see; if theres not one, create one
	%newMaterial = getUniqueName( "newMaterial" );
	new Material(%newMaterial) {
		diffuseMap[0] = "art/textures/core/warnMat";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	// Setup vars
	%action.newMaterial = %newMaterial.getId();
	%action.oldMaterial = MaterialEditorGui.currentMaterial;
	%action.oldMaterialFname = MaterialEditorGui.currentMaterial.getFilename();
	// Submit undo
	MaterialEditorGui.submitUndo( %action );

	// Delete the material from file
	if( !MaterialEditorGui.isMatEditorMaterial( MaterialEditorGui.currentMaterial ) ) {
		matEd_PersistMan.removeObjectFromFile(MaterialEditorGui.currentMaterial);
		matEd_PersistMan.removeDirty(MaterialEditorGui.currentMaterial);
	}

	// Delete the material as seen through the material selector.
	UnlistedMaterials.add( "unlistedMaterials", MaterialEditorGui.currentMaterial.getName() );
	// Loadup another material
	MaterialEditorGui.currentObject = "";
	MaterialEditorGui.setMode();
	MaterialEditorGui.prepareActiveMaterial( %newMaterial.getId(), true );
}
//------------------------------------------------------------------------------
