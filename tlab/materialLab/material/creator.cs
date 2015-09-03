//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Create New and Delete Material
//==============================================================================

//==============================================================================
function MaterialLabTools::createNewMaterial( %this ) {
	%action = %this.createUndo(ActionCreateNewMaterial, "Create New Material");
	%action.object = "";
	%material = getUniqueName( "newMaterial" );
	new Material(%material) {
		diffuseMap[0] = "art/textures/core/warnMat";
		mapTo = "unmapped_mat";
		parentGroup = RootGroup;
	};
	
	%material.setFileName(MaterialLabTools-->selMaterialFile.getText());
	%action.newMaterial = %material.getId();
	%action.oldMaterial = MaterialLabTools.currentMaterial;
	MaterialLabTools.submitUndo( %action );
	MaterialLabTools.currentObject = "";
	MaterialLabTools.setMode();
	MaterialLabTools.prepareActiveMaterial( %material.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
// Clone selected Material
function MaterialLabTools::cloneMaterial( %this ) {
	%srcMat = MaterialLabTools.currentMaterial;

	if (!isObject(%srcMat))
		return;

	%newMat = %srcMat.deepClone();
	%newName = getUniqueName(%srcMat.getName());
	%newMat.setName(%newName);
	%newMat.setFilename(%srcMat.getFilename());
	MaterialLabTools.currentObject = "";
	MaterialLabTools.setMode();
	MaterialLabTools.prepareActiveMaterial( %newMat.getId(), true );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabTools::deleteMaterial( %this ) {
	%action = %this.createUndo(ActionDeleteMaterial, "Delete Material");
	%action.object = MaterialLabTools.currentObject;
	%action.currentMode = MaterialLabTools.currentMode;
	/*
	if( MaterialLabTools.currentMode $= "Mesh" )
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
	   %action.fromMaterial = MaterialLabTools.currentMaterial;
	   %action.fromMaterialOldFname = MaterialLabTools.currentMaterial.getFilename();
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
	   %action.fromMaterial = MaterialLabTools.currentMaterial;
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
	%action.oldMaterial = MaterialLabTools.currentMaterial;
	%action.oldMaterialFname = MaterialLabTools.currentMaterial.getFilename();
	// Submit undo
	MaterialLabTools.submitUndo( %action );

	// Delete the material from file
	if( !MaterialLabTools.isMatLabitorMaterial( MaterialLabTools.currentMaterial ) ) {
		MatLab_PM.removeObjectFromFile(MaterialLabTools.currentMaterial);
		MatLab_PM.removeDirty(MaterialLabTools.currentMaterial);
	}

	// Delete the material as seen through the material selector.
	UnlistedMaterials.add( "unlistedMaterials", MaterialLabTools.currentMaterial.getName() );
	// Loadup another material
	MaterialLabTools.currentObject = "";
	MaterialLabTools.setMode();
	MaterialLabTools.prepareActiveMaterial( %newMaterial.getId(), true );
}
//------------------------------------------------------------------------------
