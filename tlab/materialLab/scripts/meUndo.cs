//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

function MaterialLabGui::createUndo(%this, %class, %desc) {
	pushInstantGroup();
	%action = new UndoScriptAction() {
		class = %class;
		superClass = BaseMaterialEdAction;
		actionName = %desc;
	};
	popInstantGroup();
	return %action;
}

function MaterialLabGui::submitUndo(%this, %action) {
	if(!%this.preventUndo)
		%action.addToManager(Editor.getUndoManager());
}

function BaseMaterialEdAction::redo(%this) {
	%this.redo();
}

function BaseMaterialEdAction::undo(%this) {
}

// Generic updateActiveMaterial redo/undo

function ActionUpdateActiveMaterial::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		/*
		if( MaterialLabGui.currentMaterial != %this.material )
		{
		   MaterialLabGui.currentObject = %this.object;
		   MaterialLabGui.setMode();
		   MaterialLabGui.setActiveMaterial(%this.material);
		}
		*/
		eval("materialLab_previewMaterial." @ %this.field @ " = " @ %this.newValue @ ";");
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			eval("%this.material." @ %this.field @ " = " @ %this.newValue @ ";");
			MaterialLabGui.currentMaterial.flush();
			MaterialLabGui.currentMaterial.reload();
		}

		MaterialLabGui.preventUndo = true;
		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
		MaterialLabGui.preventUndo = false;
	} else {
		eval("%this.material." @ %this.field @ " = " @ %this.newValue @ ";");
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionUpdateActiveMaterial::undo(%this) {
	MaterialLabGui.preventUndo = true;

	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		/*
		if( MaterialLabGui.currentMaterial != %this.material )
		{
		   MaterialLabGui.currentObject = %this.object;
		   MaterialLabGui.setMode();
		   MaterialLabGui.setActiveMaterial(%this.material);
		}
		*/
		eval("materialLab_previewMaterial." @ %this.field @ " = " @ %this.oldValue @ ";");
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			eval("%this.material." @ %this.field @ " = " @ %this.oldValue @ ";");
			MaterialLabGui.currentMaterial.flush();
			MaterialLabGui.currentMaterial.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		eval("%this.material." @ %this.field @ " = " @ %this.oldValue @ ";");
		%this.material.flush();
		%this.material.reload();
	}

	MaterialLabGui.preventUndo = false;
}

// Special case updateActiveMaterial redo/undo

function ActionUpdateActiveMaterialAnimationFlags::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		/*
		if( MaterialLabGui.currentMaterial != %this.material )
		{
		   MaterialLabGui.currentObject = %this.object;
		   MaterialLabGui.setMode();
		   MaterialLabGui.setActiveMaterial(%this.material);
		}
		*/
		eval("materialLab_previewMaterial.animFlags[" @ %this.layer @ "] = " @ %this.newValue @ ";");
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			eval("%this.material.animFlags[" @ %this.layer @ "] = " @ %this.newValue @ ";");
			MaterialLabGui.currentMaterial.flush();
			MaterialLabGui.currentMaterial.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		eval("%this.material.animFlags[" @ %this.layer @ "] = " @ %this.newValue @ ";");
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionUpdateActiveMaterialAnimationFlags::undo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		eval("materialLab_previewMaterial.animFlags[" @ %this.layer @ "] = " @ %this.oldValue @ ";");
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			eval("%this.material.animFlags[" @ %this.layer @ "] = " @ %this.oldValue @ ";");
			MaterialLabGui.currentMaterial.flush();
			MaterialLabGui.currentMaterial.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		eval("%this.material.animFlags[" @ %this.layer @ "] = " @ %this.oldValue @ ";");
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionUpdateActiveMaterialName::redo(%this) {
	%this.material.setName(%this.newName);
	MaterialLabGui.updateMaterialReferences( MissionGroup, %this.oldName, %this.newName );

	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	}
}

function ActionUpdateActiveMaterialName::undo(%this) {
	%this.material.setName(%this.oldName);
	MaterialLabGui.updateMaterialReferences( MissionGroup, %this.newName, %this.oldName );

	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	}
}

function ActionRefreshMaterial::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		%this.material.setName( %this.newName );
		MaterialLabGui.copyMaterials( %this.newMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			MaterialLabGui.copyMaterials( %this.newMaterial , %this.material );
			%this.material.flush();
			%this.material.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialNotDirty();
	} else {
		MaterialLabGui.copyMaterials( %this.newMaterial, %this.material );
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionRefreshMaterial::undo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		%this.material.setName( %this.oldName );
		MaterialLabGui.copyMaterials( %this.oldMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			MaterialLabGui.copyMaterials( %this.oldMaterial, %this.material );
			%this.material.flush();
			%this.material.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		MaterialLabGui.copyMaterials( %this.oldMaterial, %this.material );
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionClearMaterial::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		MaterialLabGui.copyMaterials( %this.newMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			MaterialLabGui.copyMaterials( %this.newMaterial, %this.material );
			%this.material.flush();
			%this.material.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		MaterialLabGui.copyMaterials( %this.newMaterial, %this.material );
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionClearMaterial::undo(%this) {
	if( MaterialLabPreviewWindow.isVisible() && MaterialLabGui.currentMaterial == %this.material ) {
		MaterialLabGui.copyMaterials( %this.oldMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();

		if (MaterialLabGui.livePreview == true) {
			MaterialLabGui.copyMaterials( %this.oldMaterial, %this.material );
			%this.material.flush();
			%this.material.reload();
		}

		MaterialLabGui.guiSync( materialLab_previewMaterial );
		MaterialLabGui.setMaterialDirty();
	} else {
		MaterialLabGui.copyMaterials( %this.oldMaterial, %this.material );
		%this.material.flush();
		%this.material.reload();
	}
}

function ActionChangeMaterial::redo(%this) {
	if( %this.mode $= "model" ) {
		%this.object.changeMaterial( %this.materialTarget, %this.fromMaterial.getName(), %this.toMaterial.getName() );
		MaterialLabGui.currentObject = %this.object;

		if( %this.toMaterial.getFilename() !$= "tlab/gui/materialSelector.ed.gui" ||
															%this.toMaterial.getFilename() !$= "tlab/materialLab/scripts/materialLab.ed.cs") {
			matLab_PersistMan.removeObjectFromFile(%this.toMaterial);
		}

		matLab_PersistMan.setDirty(%this.fromMaterial);
		matLab_PersistMan.setDirty(%this.toMaterial, %this.toMaterialNewFname);
		matLab_PersistMan.saveDirty();
		matLab_PersistMan.removeDirty(%this.fromMaterial);
		matLab_PersistMan.removeDirty(%this.toMaterial);
	} else {
		eval("%this.object." @ %this.materialTarget @ " = " @ %this.toMaterial.getName() @ ";");
		MaterialLabGui.currentObject.postApply();
	}

	if( MaterialLabPreviewWindow.isVisible() )
		MaterialLabGui.setActiveMaterial( %this.toMaterial );
}

function ActionChangeMaterial::undo(%this) {
	if( %this.mode $= "model" ) {
		%this.object.changeMaterial( %this.materialTarget, %this.toMaterial.getName(), %this.fromMaterial.getName() );
		MaterialLabGui.currentObject = %this.object;

		if( %this.toMaterial.getFilename() !$= "tlab/gui/materialSelector.ed.gui" ||
															%this.toMaterial.getFilename() !$= "tlab/materialLab/scripts/materialLab.ed.cs") {
			matLab_PersistMan.removeObjectFromFile(%this.toMaterial);
		}

		matLab_PersistMan.setDirty(%this.fromMaterial);
		matLab_PersistMan.setDirty(%this.toMaterial, %this.toMaterialOldFname);
		matLab_PersistMan.saveDirty();
		matLab_PersistMan.removeDirty(%this.fromMaterial);
		matLab_PersistMan.removeDirty(%this.toMaterial);
	} else {
		eval("%this.object." @ %this.materialTarget @ " = " @ %this.fromMaterial.getName() @ ";");
		MaterialLabGui.currentObject.postApply();
	}

	if( MaterialLabPreviewWindow.isVisible() )
		MaterialLabGui.setActiveMaterial( %this.fromMaterial );
}

function ActionCreateNewMaterial::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() ) {
		if( MaterialLabGui.currentMaterial != %this.newMaterial ) {
			MaterialLabGui.currentObject = "";
			MaterialLabGui.setMode();
			MaterialLabGui.setActiveMaterial(%this.newMaterial);
		}

		MaterialLabGui.copyMaterials( %this.newMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();
		MaterialLabGui.guiSync( materialLab_previewMaterial );
	}

	%idx = UnlistedMaterials.getIndexFromValue( %this.newMaterial.getName() );
	UnlistedMaterials.erase( %idx );
}

function ActionCreateNewMaterial::undo(%this) {
	if( MaterialLabPreviewWindow.isVisible() ) {
		if( MaterialLabGui.currentMaterial != %this.oldMaterial ) {
			MaterialLabGui.currentObject = "";
			MaterialLabGui.setMode();
			MaterialLabGui.setActiveMaterial(%this.oldMaterial);
		}

		MaterialLabGui.copyMaterials( %this.oldMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();
		MaterialLabGui.guiSync( materialLab_previewMaterial );
	}

	UnlistedMaterials.add( "unlistedMaterials", %this.newMaterial.getName() );
}

function ActionDeleteMaterial::redo(%this) {
	if( MaterialLabPreviewWindow.isVisible() ) {
		if( MaterialLabGui.currentMaterial != %this.newMaterial ) {
			MaterialLabGui.currentObject = "";
			MaterialLabGui.setMode();
			MaterialLabGui.setActiveMaterial(%this.newMaterial);
		}

		MaterialLabGui.copyMaterials( %this.newMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();
		MaterialLabGui.guiSync( materialLab_previewMaterial );
	}

	if( %this.oldMaterial.getFilename() !$= "tlab/gui/materialSelector.ed.gui" ||
														 %this.oldMaterial.getFilename() !$= "tlab/materialLab/scripts/materialLab.ed.cs") {
		matLab_PersistMan.removeObjectFromFile(%this.oldMaterial);
	}

	UnlistedMaterials.add( "unlistedMaterials", %this.oldMaterial.getName() );
}

function ActionDeleteMaterial::undo(%this) {
	if( MaterialLabPreviewWindow.isVisible() ) {
		if( MaterialLabGui.currentMaterial != %this.oldMaterial ) {
			MaterialLabGui.currentObject = "";
			MaterialLabGui.setMode();
			MaterialLabGui.setActiveMaterial(%this.oldMaterial);
		}

		MaterialLabGui.copyMaterials( %this.oldMaterial, materialLab_previewMaterial );
		materialLab_previewMaterial.flush();
		materialLab_previewMaterial.reload();
		MaterialLabGui.guiSync( materialLab_previewMaterial );
	}

	matLab_PersistMan.setDirty(%this.oldMaterial, %this.oldMaterialFname);
	matLab_PersistMan.saveDirty();
	matLab_PersistMan.removeDirty(%this.oldMaterial);
	%idx = UnlistedMaterials.getIndexFromValue( %this.oldMaterial.getName() );
	UnlistedMaterials.erase( %idx );
}