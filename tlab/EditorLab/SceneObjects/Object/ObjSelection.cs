//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Set object selected in scene (Trees and WorldEditor)
//==============================================================================
//==============================================================================
function Scene::selectObject(%this, %obj,%isLast,%source) {
		devLog("Scene::selectObject %obj,%isLast,%source",%obj,%isLast,%source);
	if (isObject(%source)){
		%sourceClass = %source.getClassName();
		devLog("Scene::selectObject Called from source obj",%source,"of class:",%sourceClass);
	}
		
	if (%isLast $= "")
		%isLast = true;
	
	if (%sourceClass !$= "WorldEditor")
		EWorldEditor.selectObject(%obj);
		
	if (%sourceClass !$= "GuiTreeViewCtrl")
		%ignoreTree = %source;
		
		foreach$(%tree in Scene.sceneTrees){
			if (%tree.getId() $= %ignoreTree.getId()){
				devLog("Scene::selectObject Ignoring tree source",%tree);
				continue;
			}
			%tree.treeAddSelection(%obj,%isLast);
		}
	
	Scene.onSelect(%obj);	
}
//------------------------------------------------------------------------------

function Scene::unselectObject(%this, %obj,%source) {
	if (isObject(%source)){
		%sourceClass = %source.getClassName();
		devLog("Scene::unselectObject Called from source obj",%source,"of class:",%sourceClass);
	}
	if (%sourceClass !$= "WorldEditor")
		EWorldEditor.unselectObject(%obj);
		
	Scene.onRemoveInspect(%obj);
	
	Scene.onUnSelect(%obj);	
	//SceneBrowserInspector.removeInspect( %obj );
}

//==============================================================================
// Scene OnSelect/OnUnselect Callbacks
//==============================================================================
//==============================================================================
function Scene::onSelect(%this, %obj) {
	devLog("Scene::onSelect",%obj);
	if (%obj.getClassName() $= "SimGroup") {		
		Scene.setActiveSimGroup( %obj );
	}
	Scene.treeMarkObject(%obj);
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
//------------------------------------------------------------------------------
//==============================================================================
function Scene::onUnselect(%this, %obj) {
	if (%obj.getClassName() $= "GroundCover") {		
		SEP_GroundCover.onGroundCoverUnselected(%obj);
	}

	//EWorldEditor.unselectObject(%obj);
	%plugin = Lab.currentEditor;
	if (%plugin.isMethod("on"@%obj.getClassName()@"Unselected"))
		eval("%plugin.on"@%obj.getClassName()@"Unselected(%obj);");
		
	if (%plugin.isMethod("onUnselectObject"))
		%plugin.onUnselectObject(%obj);
}
//------------------------------------------------------------------------------
//==============================================================================



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
