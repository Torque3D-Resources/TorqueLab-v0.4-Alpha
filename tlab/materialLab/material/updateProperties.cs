//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

// Material Editor originally created by Dave Calabrese and Travis Vroman of Gaslight Studios

//==============================================================================
// Helper functions to help create categories and manage category lists
function MaterialLabGui::updateAllFields(%this) {
	matLab_cubemapEd_availableCubemapList.clear();
}
//------------------------------------------------------------------------------

//==============================================================================
// Select the file in which the material will be saved
function MaterialLabGui::selectMatFile( %this ) {
	%file = %this.openFile("File",MaterialLabGui.currentMaterial.getFileName());

	if (!isFile(%file)) {
		warnLog("Invalid file selected:",%file);
		return;
	}

	if (MaterialLabGui.currentMaterial.getFileName() $= %file) {
		warnLog("File is the same as current:",%file,"Current:",MaterialLabGui.currentMaterial.getFileName());
		return;
	}

	MaterialLabGui.currentMaterial.setFilename(%file);
	MaterialLabGui.setMaterialDirty();
	%this.setActiveMaterialFile(MaterialLabGui.currentMaterial);
}
//------------------------------------------------------------------------------

//==============================================================================
function MaterialLabGui::changeLayer( %this, %layer ) {
	if( MaterialLabGui.currentLayer == getWord(%layer, 1) )
		return;

	MaterialLabGui.currentLayer = getWord(%layer, 1);
	MaterialLabGui.guiSync( materialLab_previewMaterial );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::updateMaterialReferences( %this, %obj, %oldName, %newName ) {
	if ( %obj.isMemberOfClass( "SimSet" ) ) {
		// invoke on children
		%count = %obj.getCount();

		for ( %i = 0; %i < %count; %i++ )
			%this.updateMaterialReferences( %obj.getObject( %i ), %oldName, %newName );
	} else {
		%objChanged = false;
		// Change all material fields that use the old material name
		%count = %obj.getFieldCount();

		for( %i = 0; %i < %count; %i++ ) {
			%fieldName = %obj.getField( %i );

			if ( ( %obj.getFieldType( %fieldName ) $= "TypeMaterialName" ) && ( %obj.getFieldValue( %fieldName ) $= %oldName ) ) {
				eval( %obj @ "." @ %fieldName @ " = " @ %newName @ ";" );
				%objChanged = true;
			}
		}

		EWorldEditor.isDirty |= %objChanged;

		if ( %objChanged && %obj.isMethod( "postApply" ) )
			%obj.postApply();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Global Material Options

function MaterialLabGui::updateReflectionType( %this, %type ) {
	if( %type $= "None" ) {
		MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(0);
		//Reset material reflection settings on the preview materials
		MaterialLabGui.updateActiveMaterial( "cubeMap", "" );
		MaterialLabGui.updateActiveMaterial( "dynamicCubemap" , false );
		MaterialLabGui.updateActiveMaterial( "planarReflection", false );
	} else {
		if(%type $= "cubeMap") {
			devLog("ShowCubemap button");
			MaterialLabPropertiesWindow-->matLab_cubemapEditBtn.setVisible(1);
			MaterialLabGui.updateActiveMaterial( %type, materialLab_previewMaterial.cubemap );
		} else {
			MaterialLabGui.updateActiveMaterial( %type, true );
		}
	}
}
//------------------------------------------------------------------------------