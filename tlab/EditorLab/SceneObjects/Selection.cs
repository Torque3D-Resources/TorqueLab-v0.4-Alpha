//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function Scene::onSelect(%this, %obj) {
	if (%obj.getClassName() $= "SimGroup") {		
		SceneEd.setActiveSimGroup(%obj);
	}

	if (%obj.getClassName() $= "GroundCover") {	
		SEP_GroundCover.onGroundCoverSelected(%obj);
	}
}
function Scene::onUnselect(%this, %obj) {
	if (%obj.getClassName() $= "GroundCover") {		
		SEP_GroundCover.onGroundCoverUnselected(%obj);
	}

	EWorldEditor.unselectObject(%obj);
}

function Scene::onRemoveSelection(%this, %obj) {
	EWorldEditor.unselectObject(%obj);
	SceneBrowserInspector.removeInspect( %obj );
}

//==============================================================================
// Detach the GUIs not saved with EditorGui (For safely save EditorGui)
//==============================================================================
//==============================================================================

function Scene::onObjectDeleteCompleted(%this) {
	// Let the world editor know to
	// clear its selection.
	EWorldEditor.clearSelection();
	EWorldEditor.isDirty = true;
}
function Scene::onClearSelected(%this) {	
	WorldEditor.clearSelection();
}

function Scene::setDirty(%this) {	
	EWorldEditor.isDirty = true;
}

//------------------------------------------------------------------------------

function Scene::onInspect(%this, %obj) {
	SceneBrowserInspector.inspect(%obj);
}
function Scene::onAddSelection(%this, %obj, %isLastSelection) {	
	EWorldEditor.selectObject( %obj );
	
	if( %isLastSelection )
		SceneBrowserInspector.addInspect( %obj );
	else
		SceneBrowserInspector.addInspect( %obj, false );
}