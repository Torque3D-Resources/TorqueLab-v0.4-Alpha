//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Commands for the Cubemap Editor

//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::selectCubemap(%this) {
	%cubemap = MaterialLabGui.currentCubemap;

	if(!isObject(%cubemap))
		return;

	MaterialLabGui.updateActiveMaterial( "cubemap", %cubemap.name );
	MaterialLabGui.hideCubemapEditor();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::cancelCubemap(%this) {
	%cubemap = MaterialLabGui.currentCubemap;
	%idx = matLab_cubemapEd_availableCubemapList.findItemText( %cubemap.getName() );
	matLab_cubemapEd_availableCubemapList.setItemText( %idx, notDirtyCubemap.originalName );
	%cubemap.setName( notDirtyCubemap.originalName );
	MaterialLabGui.copyCubemaps( notDirtyCubemap, %cubemap );
	MaterialLabGui.copyCubemaps( notDirtyCubemap, matLabCubeMapPreviewMat);
	%cubemap.updateFaces();
	matLabCubeMapPreviewMat.updateFaces();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::showCubemapEditor(%this) {
	if (matLab_cubemapEditor.isVisible())
		return;

	MaterialLabGui.currentCubemap = "";
	matLab_cubemapEditor.setVisible(1);
	new PersistenceManager(matLab_cubemapEdPerMan);
	MaterialLabGui.setCubemapNotDirty();

	for( %i = 0; %i < RootGroup.getCount(); %i++ ) {
		if( RootGroup.getObject(%i).getClassName()!$= "CubemapData" )
			continue;

		for( %k = 0; %k < UnlistedCubemaps.count(); %k++ ) {
			%unlistedFound = 0;

			if( UnlistedCubemaps.getValue(%k) $= RootGroup.getObject(%i).name ) {
				%unlistedFound = 1;
				break;
			}
		}

		if( %unlistedFound )
			continue;

		matLab_cubemapEd_availableCubemapList.addItem( RootGroup.getObject(%i).name );
	}

	singleton CubemapData(notDirtyCubemap);

	// if there was no cubemap, pick the first, select, and bail, these are going to take
	// care of themselves in the selected function
	if( !isObject( MaterialLabGui.currentMaterial.cubemap ) ) {
		if( matLab_cubemapEd_availableCubemapList.getItemCount() > 0 ) {
			matLab_cubemapEd_availableCubemapList.setSelected(0, true);
			return;
		} else {
			// if there are no cubemaps, then create one, select, and bail
			%cubemap = MaterialLabGui.createNewCubemap();
			matLab_cubemapEd_availableCubemapList.addItem( %cubemap.name );
			matLab_cubemapEd_availableCubemapList.setSelected(0, true);
			return;
		}
	}

	// do not directly change activeMat!
	MaterialLabGui.currentCubemap = MaterialLabGui.currentMaterial.cubemap.getId();
	%cubemap = MaterialLabGui.currentCubemap;
	notDirtyCubemap.originalName = %cubemap.getName();
	MaterialLabGui.copyCubemaps( %cubemap, notDirtyCubemap);
	MaterialLabGui.copyCubemaps( %cubemap, matLabCubeMapPreviewMat);
	MaterialLabGui.syncCubemap( %cubemap );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::hideCubemapEditor(%this,%cancel) {
	if(%cancel)
		MaterialLabGui.cancelCubemap();

	matLab_cubemapEd_availableCubemapList.clearItems();
	matLab_cubemapEdPerMan.delete();
	matLab_cubemapEditor.setVisible(0);
}
//------------------------------------------------------------------------------
//==============================================================================
// create category and update current material if there is one
function MaterialLabGui::addCubemap( %this,%cubemapName ) {
	if( %cubemapName $= "" ) {
		LabMsgOK( "Error", "Can not create a cubemap without a valid name.");
		return;
	}

	for(%i = 0; %i < RootGroup.getCount(); %i++) {
		if( %cubemapName $= RootGroup.getObject(%i).getName() ) {
			LabMsgOK( "Error", "There is already an object with the same name.");
			return;
		}
	}

	// Create and select a new cubemap
	%cubemap = MaterialLabGui.createNewCubemap( %cubemapName );
	%idx = matLab_cubemapEd_availableCubemapList.addItem( %cubemap.name );
	matLab_cubemapEd_availableCubemapList.setSelected( %idx, true );
	// material category text field to blank
	matLab_addCubemapWindow-->cubemapName.setText("");
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::createNewCubemap( %this, %cubemap ) {
	if( %cubemap $= "" ) {
		for(%i = 0; ; %i++) {
			%cubemap = "newCubemap_" @ %i;

			if( !isObject(%cubemap) )
				break;
		}
	}

	new CubemapData(%cubemap) {
		cubeFace[0] = "tlab/materialLab/assets/cube_xNeg";
		cubeFace[1] = "tlab/materialLab/assets/cube_xPos";
		cubeFace[2] = "tlab/materialLab/assets/cube_ZNeg";
		cubeFace[3] = "tlab/materialLab/assets/cube_ZPos";
		cubeFace[4] = "tlab/materialLab/assets/cube_YNeg";
		cubeFace[5] = "tlab/materialLab/assets/cube_YPos";
		parentGroup = RootGroup;
	};
	matLab_cubemapEdPerMan.setDirty( %cubemap, "art/materials.cs" );
	matLab_cubemapEdPerMan.saveDirty();
	return %cubemap;
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::setCubemapDirty(%this) {
	%propertyText = "Create Cubemap *";
	matLab_cubemapEditor.text = %propertyText;
	matLab_cubemapEditor.dirty = true;
	matLab_cubemapEditor-->saveCubemap.setActive(true);
	%cubemap = MaterialLabGui.currentCubemap;

	// materials created in the materail selector are given that as its filename, so we run another check
	if( MaterialLabGui.isMatLabitorMaterial( %cubemap ) )
		matLab_cubemapEdPerMan.setDirty(%cubemap, "art/materials.cs");
	else
		matLab_cubemapEdPerMan.setDirty(%cubemap);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::setCubemapNotDirty(%this) {
	%propertyText= strreplace("Create Cubemap" , "*" , "");
	matLab_cubemapEditor.text = %propertyText;
	matLab_cubemapEditor.dirty = false;
	matLab_cubemapEditor-->saveCubemap.setActive(false);
	%cubemap = MaterialLabGui.currentCubemap;
	matLab_cubemapEdPerMan.removeDirty(%cubemap);
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::showDeleteCubemapDialog(%this) {
	%idx = matLab_cubemapEd_availableCubemapList.getSelectedItem();
	%cubemap = matLab_cubemapEd_availableCubemapList.getItemText( %idx );
	%cubemap = %cubemap.getId();

	if( %cubemap == -1 || !isObject(%cubemap) )
		return;

	if( isObject( %cubemap ) ) {
		LabMsgYesNoCancel("Delete Cubemap?",
								"Are you sure you want to delete<br><br>" @ %cubemap.getName() @ "<br><br> Cubemap deletion won't take affect until the engine is quit.",
								"MaterialLabGui.deleteCubemap( " @ %cubemap @ ", " @ %idx @ " );",
								"",
								"" );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::deleteCubemap( %this, %cubemap, %idx ) {
	matLab_cubemapEd_availableCubemapList.deleteItem( %idx );
	UnlistedCubemaps.add( "unlistedCubemaps", %cubemap.getName() );

	if( !MaterialLabGui.isMatLabitorMaterial( %cubemap ) ) {
		matLab_cubemapEdPerMan.removeDirty( %cubemap );
		matLab_cubemapEdPerMan.removeObjectFromFile( %cubemap );
	}

	if( matLab_cubemapEd_availableCubemapList.getItemCount() > 0 ) {
		matLab_cubemapEd_availableCubemapList.setSelected(0, true);
	} else {
		// if there are no cubemaps, then create one, select, and bail
		%cubemap = MaterialLabGui.createNewCubemap();
		matLab_cubemapEd_availableCubemapList.addItem( %cubemap.getName() );
		matLab_cubemapEd_availableCubemapList.setSelected(0, true);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function matLab_cubemapEd_availableCubemapList::onSelect( %this, %id, %cubemap ) {
	%cubemap = %cubemap.getId();

	if( MaterialLabGui.currentCubemap $= %cubemap )
		return;

	if( matLab_cubemapEditor.dirty ) {
		%savedCubemap = MaterialLabGui.currentCubemap;
		LabMsgYesNoCancel("Save Existing Cubemap?",
								"Do you want to save changes to <br><br>" @ %savedCubemap.getName(),
								"MaterialLabGui.saveCubemap(" @ true @ ");",
								"MaterialLabGui.saveCubemapDialogDontSave(" @ %cubemap @ ");",
								"MaterialLabGui.saveCubemapDialogCancel();" );
	} else
		MaterialLabGui.changeCubemap( %cubemap );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::showSaveCubemapDialog( %this ) {
	%cubemap = MaterialLabGui.currentCubemap;

	if( !isObject(%cubemap) )
		return;

	LabMsgYesNoCancel("Save Cubemap?",
							"Do you want to save changes to <br><br>" @ %cubemap.getName(),
							"MaterialLabGui.saveCubemap( " @ %cubemap @ " );",
							"",
							"" );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::saveCubemap( %this, %cubemap ) {
	notDirtyCubemap.originalName = %cubemap.getName();
	MaterialLabGui.copyCubemaps( %cubemap, notDirtyCubemap );
	MaterialLabGui.copyCubemaps( %cubemap, matLabCubeMapPreviewMat);
	matLab_cubemapEdPerMan.saveDirty();
	MaterialLabGui.setCubemapNotDirty();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::saveCubemapDialogDontSave( %this, %newCubemap) {
	//deal with old cubemap first
	%oldCubemap = MaterialLabGui.currentCubemap;
	%idx = matLab_cubemapEd_availableCubemapList.findItemText( %oldCubemap.getName() );
	matLab_cubemapEd_availableCubemapList.setItemText( %idx, notDirtyCubemap.originalName );
	%oldCubemap.setName( notDirtyCubemap.originalName );
	MaterialLabGui.copyCubemaps( notDirtyCubemap, %oldCubemap);
	MaterialLabGui.copyCubemaps( notDirtyCubemap, matLabCubeMapPreviewMat);
	MaterialLabGui.syncCubemap( %oldCubemap );
	MaterialLabGui.changeCubemap( %newCubemap );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::saveCubemapDialogCancel( %this ) {
	%cubemap = MaterialLabGui.currentCubemap;
	%idx = matLab_cubemapEd_availableCubemapList.findItemText( %cubemap.getName() );
	matLab_cubemapEd_availableCubemapList.clearSelection();
	matLab_cubemapEd_availableCubemapList.setSelected( %idx, true );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::changeCubemap( %this, %cubemap ) {
	MaterialLabGui.setCubemapNotDirty();
	MaterialLabGui.currentCubemap = %cubemap;
	notDirtyCubemap.originalName = %cubemap.getName();
	MaterialLabGui.copyCubemaps( %cubemap, notDirtyCubemap);
	MaterialLabGui.copyCubemaps( %cubemap, matLabCubeMapPreviewMat);
	MaterialLabGui.syncCubemap( %cubemap );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::editCubemapImage( %this, %face ) {
	MaterialLabGui.setCubemapDirty();
	%cubemap = MaterialLabGui.currentCubemap;
	%bitmap = MaterialLabGui.openFile("texture");

	if( %bitmap !$= "" && %bitmap !$= "tlab/materialLab/assets/cubemapBtnBorder" ) {
		%cubemap.cubeFace[%face] = %bitmap;
		MaterialLabGui.copyCubemaps( %cubemap, matLabCubeMapPreviewMat);
		MaterialLabGui.syncCubemap( %cubemap );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::editCubemapName( %this, %newName ) {
	MaterialLabGui.setCubemapDirty();
	%cubemap = MaterialLabGui.currentCubemap;
	%idx = matLab_cubemapEd_availableCubemapList.findItemText( %cubemap.getName() );
	matLab_cubemapEd_availableCubemapList.setItemText( %idx, %newName );
	%cubemap.setName(%newName);
	MaterialLabGui.syncCubemap( %cubemap );
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::syncCubemap( %this, %cubemap ) {
	%xpos = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[0]);

	if( %xpos !$= "" )
		matLab_cubemapEd_XPos.setBitmap( %xpos );

	%xneg = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[1]);

	if( %xneg !$= "" )
		matLab_cubemapEd_XNeg.setBitmap( %xneg );

	%yneg = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[2]);

	if( %yneg !$= "" )
		matLab_cubemapEd_YNeG.setBitmap( %yneg );

	%ypos = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[3]);

	if( %ypos !$= "" )
		matLab_cubemapEd_YPos.setBitmap( %ypos );

	%zpos = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[4]);

	if( %zpos !$= "" )
		matLab_cubemapEd_ZPos.setBitmap( %zpos );

	%zneg = MaterialLabGui.searchForTexture(%cubemap.getName(), %cubemap.cubeFace[5]);

	if( %zneg !$= "" )
		matLab_cubemapEd_ZNeg.setBitmap( %zneg );

	matLab_cubemapEd_activeCubemapNameTxt.setText(%cubemap.getName());
	%cubemap.updateFaces();
	matLabCubeMapPreviewMat.updateFaces();
}
//------------------------------------------------------------------------------
//==============================================================================
function MaterialLabGui::copyCubemaps( %this, %copyFrom, %copyTo) {
	%copyTo.cubeFace[0] = %copyFrom.cubeFace[0];
	%copyTo.cubeFace[1] = %copyFrom.cubeFace[1];
	%copyTo.cubeFace[2] = %copyFrom.cubeFace[2];
	%copyTo.cubeFace[3] = %copyFrom.cubeFace[3];
	%copyTo.cubeFace[4] = %copyFrom.cubeFace[4];
	%copyTo.cubeFace[5] = %copyFrom.cubeFace[5];
}
//------------------------------------------------------------------------------
