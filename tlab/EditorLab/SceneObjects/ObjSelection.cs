//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$WorldEditor_DropTypes = "toTerrain atOrigin atCamera atCameraRot belowCamera screenCenter atCentroid belowSelection";
$Scene_DropTypes = "currentSel currentSelZ";
$Scene_AllDropTypes = $WorldEditor_DropTypes SPC $Scene_DropTypes;
$Scene_DropTypeDisplay["atOrigin"] = "at origin";
$Scene_DropTypeDisplay["atCamera"] = "at camera";
$Scene_DropTypeDisplay["atCameraRot"] = "at camera+Rot.";
$Scene_DropTypeDisplay["belowCamera"] = "below camera";
$Scene_DropTypeDisplay["atCentroid"] = "at centroid";
$Scene_DropTypeDisplay["toTerrain"] = "to terrain";
$Scene_DropTypeDisplay["belowSelection"] = "below sel.";
$Scene_DropTypeDisplay["currentSel"] = "at current sel.";
$Scene_DropTypeDisplay["currentSelZ"] = "at current sel. Z";
function SceneDropTypeMenu::onSelect(%this, %id,%text) {
	%dropType = %this.typeId[%id];
	devLog("Selected drop type=",%dropType,"Id",%id,"Text",%text);
	Scene.setDropType(%dropType);
	devLog("Coonfirm drop type=",Scene.dropMode,"Editor",EWorldEditor.dropType);
}	

function Scene::setDropType(%this, %dropType) {
	
	foreach$(%menu in Scene.dropTypeMenus){
		%menu.setText($Scene_DropTypeDisplay[%dropType]);
	}
	if (strFind($WorldEditor_DropTypes,%dropType))
		EWorldEditor.dropType = %dropType;
	
	Scene.dropMode = %dropType;
}	

//==============================================================================
// Set object selected in scene (Trees and WorldEditor)
//==============================================================================

function Scene::selectObject(%this, %obj,%isLast) {
	if (%isLast $= "")
		%isLast = true;
		EWorldEditor.selectObject(%obj);
		
		foreach$(%tree in Scene.sceneTrees)
			%tree.treeAddSelection(%obj,%isLast);
	
		
}

//==============================================================================
// Set object selected in scene (Trees and WorldEditor)
//==============================================================================

function Scene::onSelect(%this, %obj) {
	if (%obj.getClassName() $= "SimGroup") {		
		SceneEd.setActiveSimGroup(%obj);
	}

	if (%obj.getClassName() $= "GroundCover") {	
		SEP_GroundCover.onGroundCoverSelected(%obj);
	}
	%plugin = Lab.currentEditor;
	if (%plugin.isMethod("setSelectedObject"))
		%plugin.setSelectedObject(%obj);
	
	if (%plugin.isMethod("onSelectObject"))
		%plugin.onSelectObject(%obj);
		
	if (%plugin.isMethod("on"@%obj.getClassName()@"Selected"))
		eval("%plugin.on"@%obj.getClassName()@"Selected(%obj);");
		
	
		
}
function Scene::onUnselect(%this, %obj) {
	if (%obj.getClassName() $= "GroundCover") {		
		SEP_GroundCover.onGroundCoverUnselected(%obj);
	}

	EWorldEditor.unselectObject(%obj);
	
	if (%plugin.isMethod("on"@%obj.getClassName()@"Unselected"))
		eval("%plugin.on"@%obj.getClassName()@"Unselected(%obj);");
		
	if (%plugin.isMethod("onUnselectObject"))
		%plugin.onUnselectObject(%obj);
}

function Scene::onRemoveSelection(%this, %obj) {
	EWorldEditor.unselectObject(%obj);
	Scene.onRemoveInspect(%obj);
	//SceneBrowserInspector.removeInspect( %obj );
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


function Scene::AddObjectToSelection(%this, %obj, %isLastSelection) {	
	EWorldEditor.selectObject( %obj );
	%this.onAddInspect(%obj,%isLastSelection);
	
	//if( %isLastSelection )
		//SceneBrowserInspector.addInspect( %obj );
	//else
		//SceneBrowserInspector.addInspect( %obj, false );
}

$QuickDeleteMode = "1";
//==============================================================================
// Remove selected objects from their group (delete group if empty)
function Scene::DeleteSelection(%this) {
	if ($QuickDeleteMode $= "1"){
		%this.quickDeleteSelection();
		return;
	}
	else if($QuickDeleteMode $= "2"){
	
	%selSize = EWorldEditor.getSelectionSize();

	if( %selSize > 0 )
		SceneEditorTree.deleteSelection();
		return;
	}
	
	%count = EWorldEditor.getSelectionSize();

	EWorldEditor.clearSelection();

		
	if (%count <= 0)
		return;
	Scene.setDirty();
	foreach$(%tree in Scene.sceneTrees)
		%tree.deleteSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
// Remove selected objects from their group (delete group if empty)
function Scene::quickDeleteSelection(%this) {	
	if (!isObject("WorldEditorQuickGroup"))
		$QuickGroup = newSimSet("WEditorQuickGroup");
	
	WEditorQuickGroup.clear();
	%count = EWorldEditor.getSelectionSize();
	
	for( %i=0; %i<%count; %i++) {
		%obj = EWorldEditor.getSelectedObject( %i );
		WEditorQuickGroup.add(%obj);		
	}
	
	WEditorQuickGroup.deleteAllObjects();	
}
//------------------------------------------------------------------------------
//==============================================================================
// Remove selected objects from their group (delete group if empty)
function delSelect(%this) {	
	if (!isObject("WorldEditorQuickGroup"))
		$QuickGroup = newSimSet("WEditorQuickGroup");
	
	WEditorQuickGroup.clear();
	%count = EWorldEditor.getSelectionSize();
	
	for( %i=0; %i<%count; %i++) {
		%obj = EWorldEditor.getSelectedObject( %i );
		WEditorQuickGroup.add(%obj);		
	}

	EWorldEditor.clearSelection();
	WEditorQuickGroup.deleteAllObjects();
}
//------------------------------------------------------------------------------
