//==============================================================================
// TorqueLab -> DecalEditor Action Manager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function DecalEditorGui::createAction(%this, %class, %desc) {
	pushInstantGroup();
	%action = new UndoScriptAction() {
		class = %class;
		superClass = BaseDecalEdAction;
		actionName = %desc;
		tree = DecalEditorTreeView;
	};
	popInstantGroup();
	return %action;
}

function DecalEditorGui::doAction(%this, %action) {
	if (%action.doit())
		%action.addToManager(Editor.getUndoManager());
}

function BaseDecalEdAction::redo(%this) {
	// Default redo action is the same as the doit action
	%this.doit();
}

function BaseDecalEdAction::undo(%this) {
}


function ActionEditNodeDetails::doit(%this) {
	%count = getWordCount(%this.newTransformData);

	if(%this.instanceId !$= "" && %count == 7) {
		DecalEditorGui.editDecalDetails( %this.instanceId, %this.newTransformData );
		DecalEditorGui.syncNodeDetails();
		DecalEditorGui.selectDecal( %this.instanceId );
		return true;
	}

	return false;
}

function ActionEditNodeDetails::undo(%this) {
	%count = getWordCount(%this.oldTransformData);

	if(%this.instanceId !$= "" && %count == 7) {
		DecalEditorGui.editDecalDetails( %this.instanceId, %this.oldTransformData );
		DecalEditorGui.syncNodeDetails();
		DecalEditorGui.selectDecal( %this.instanceId );
	}
}
