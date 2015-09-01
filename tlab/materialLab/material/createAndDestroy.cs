//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Create New and Delete Material
//==============================================================================

//==============================================================================
function MaterialLabGui::createNewMaterial( %this ) {
	%action = %this.createUndo(ActionCreateNewMaterial, "Create New Material");
	%action.object = "";
	%material = getUniqueName( "newMaterial" );
	new Material(%material) {
		diffuseMap[0] = "art/textures/core/warnMat";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	
	%material.setFileName(MaterialLabGui-->selMaterialFile.getText());
	%action.newMaterial = %material.getId();
	%action.oldMaterial = MaterialLabGui.currentMaterial;
	MaterialLabGui.submitUndo( %action );
	MaterialLabGui.currentObject = "";
	MaterialLabGui.setMode();
	MaterialLabGui.prepareActiveMaterial( %material.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
// Clone selected Material
function MaterialLabGui::cloneMaterial( %this ) {
	%srcMat = MaterialLabGui.currentMaterial;

	if (!isObject(%srcMat))
		return;

	%newMat = %srcMat.deepClone();
	%newName = getUniqueName(%srcMat.getName());
	%newMat.setName(%newName);
	%newMat.setFilename(%srcMat.getFilename());
	MaterialLabGui.currentObject = "";
	MaterialLabGui.setMode();
	MaterialLabGui.prepareActiveMaterial( %newMat.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::deleteMaterial( %this ) {
	%action = %this.createUndo(ActionDeleteMaterial, "Delete Material");
	%action.object = MaterialLabGui.currentObject;
	%action.currentMode = MaterialLabGui.currentMode;
	/*
	if( MaterialLabGui.currentMode $= "Mesh" )
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
	   %action.fromMaterial = MaterialLabGui.currentMaterial;
	   %action.fromMaterialOldFname = MaterialLabGui.currentMaterial.getFilename();
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
	   %action.fromMaterial = MaterialLabGui.currentMaterial;
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
	%action.oldMaterial = MaterialLabGui.currentMaterial;
	%action.oldMaterialFname = MaterialLabGui.currentMaterial.getFilename();
	// Submit undo
	MaterialLabGui.submitUndo( %action );

	// Delete the material from file
	if( !MaterialLabGui.isMatLabitorMaterial( MaterialLabGui.currentMaterial ) ) {
		matLab_PersistMan.removeObjectFromFile(MaterialLabGui.currentMaterial);
		matLab_PersistMan.removeDirty(MaterialLabGui.currentMaterial);
	}

	// Delete the material as seen through the material selector.
	UnlistedMaterials.add( "unlistedMaterials", MaterialLabGui.currentMaterial.getName() );
	// Loadup another material
	MaterialLabGui.currentObject = "";
	MaterialLabGui.setMode();
	MaterialLabGui.prepareActiveMaterial( %newMaterial.getId(), true );
}
//------------------------------------------------------------------------------
