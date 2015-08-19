//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

// Material Editor originally created by Dave Calabrese and Travis Vroman of Gaslight Studios

//==============================================================================
// Helper functions to help create categories and manage category lists
function MaterialEditorGui::updateAllFields(%this) {
	matEd_cubemapEd_availableCubemapList.clear();
}
//------------------------------------------------------------------------------

//==============================================================================
// Select the file in which the material will be saved
function MaterialEditorGui::selectMatFile( %this ) {
	%file = %this.openFile("File",MaterialEditorGui.currentMaterial.getFileName());

	if (!isFile(%file)) {
		warnLog("Invalid file selected:",%file);
		return;
	}

	if (MaterialEditorGui.currentMaterial.getFileName() $= %file) {
		warnLog("File is the same as current:",%file,"Current:",MaterialEditorGui.currentMaterial.getFileName());
		return;
	}

	MaterialEditorGui.currentMaterial.setFilename(%file);
	MaterialEditorGui.setMaterialDirty();
	%this.setActiveMaterialFile(MaterialEditorGui.currentMaterial);
}
//------------------------------------------------------------------------------

//==============================================================================
function MaterialEditorGui::changeLayer( %this, %layer ) {
	if( MaterialEditorGui.currentLayer == getWord(%layer, 1) )
		return;

	MaterialEditorGui.currentLayer = getWord(%layer, 1);
	MaterialEditorGui.guiSync( materialEd_previewMaterial );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialEditorGui::updateMaterialReferences( %this, %obj, %oldName, %newName ) {
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

function MaterialEditorGui::updateReflectionType( %this, %type ) {
	if( %type $= "None" ) {
		MaterialEditorPropertiesWindow-->matEd_cubemapEditBtn.setVisible(0);
		//Reset material reflection settings on the preview materials
		MaterialEditorGui.updateActiveMaterial( "cubeMap", "" );
		MaterialEditorGui.updateActiveMaterial( "dynamicCubemap" , false );
		MaterialEditorGui.updateActiveMaterial( "planarReflection", false );
	} else {
		if(%type $= "cubeMap") {
			devLog("ShowCubemap button");
			MaterialEditorPropertiesWindow-->matEd_cubemapEditBtn.setVisible(1);
			MaterialEditorGui.updateActiveMaterial( %type, materialEd_previewMaterial.cubemap );
		} else {
			MaterialEditorGui.updateActiveMaterial( %type, true );
		}
	}
}
//------------------------------------------------------------------------------