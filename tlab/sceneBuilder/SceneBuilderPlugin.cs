//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



//==============================================================================
// Scene Editor Params - Used set default settings and build plugins options GUI
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SceneBuilderPlugin::initParamsArray( %this,%array ) {
	$SBPCfg = newScriptObject("SceneBuilderCfg");
	%array.group[%groupId++] = "Objects and Prefabs settings";

	%array.setVal("DropLocation",       "10" TAB "Drop object location" TAB "Dropdown"  TAB "itemList>>$TLab_Object_DropTypes" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("AutoCreatePrefab",       "1" TAB "Create prefab automatically" TAB "Checkbox"  TAB "" TAB "$SBP::AutoCreatePrefab" TAB %groupId);
	$TLab_PrefabAutoMode = "Level Object Folder";
	%array.setVal("AutoPrefabMode",       "1" TAB "Automatic prefab mode" TAB "DropDown"  TAB "itemList>>$TLab_PrefabAutoMode" TAB "$SBP::AutoPrefabMode" TAB %groupId);
	%array.setVal("AutoPrefabFolder",       "art/models/prefabs/" TAB "Folder for auto prefab mode" TAB "TextEdit"  TAB "" TAB "$SBP::AutoPrefabFolder" TAB %groupId);
	
	%array.group[%groupId++] = "MissionGroup Organizer";
	%array.setVal("CoreGroup",       "mgCore" TAB "Core Objects Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("SceneObjectsGroup",       "mgSceneObjects" TAB "Ambient Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("EnvironmentGroup",       "mgEnvironment" TAB "Environment Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("TSStaticGroup",       "mgMapModels" TAB "TSStatic Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("SpawnGroup",       "PlayerDropPoints" TAB "Spawn Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("Vehicle",       "Vehicle" TAB "Vehicle Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("MiscObjectGroup",       "mgMiscObject" TAB "Misc. Objects Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("ShapeGroup",       "mgShapeGroup" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("NavAIGroup",       "NavAI" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("NavMeshGroup",       "NavMesh" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("NavPathGroup",       "NavPath" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	%array.setVal("CoverPointGroup",       "CoverPoint" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "SceneBuilderCfg" TAB %groupId);
	
	%array.group[%groupId++] = "Scene tree list options";
	%array.setVal("renameInternal",       "0" TAB "Rename internal name" TAB "Checkbox"  TAB "" TAB "SceneBuilderTree" TAB %groupId);
	%array.setVal("showObjectNames",       "0" TAB "Show object name" TAB "Checkbox"  TAB "" TAB "SceneBuilderTree" TAB %groupId);
	%array.setVal("showInternalNames",       "0" TAB "Show object internalName" TAB "Checkbox"  TAB "" TAB "SceneBuilderTree" TAB %groupId);
	%array.setVal("showObjectIds",       "0" TAB "Show object IDs" TAB "Checkbox"  TAB "" TAB "SceneBuilderTree" TAB %groupId);
	%array.setVal("showClassNames",       "0" TAB "Show object Class" TAB "Checkbox"  TAB "" TAB "SceneBuilderTree" TAB %groupId);
	
	%array.group[%groupId++] = "Objects Creator Options";
	%array.setVal("IconWidth",       "120" TAB "icon width" TAB "SliderEdit"  TAB "range>>30 300;;tickAt>>1" TAB "SceneBuilderCfg" TAB %groupId);
	
	%array.group[%groupId++] = "Special tools and managers";
	%array.setVal("GroundCoverDefaultMaterial",       "grass1" TAB "Shape Group" TAB "TextEdit"  TAB "" TAB "$SBP_GroundCoverDefault_Material" TAB %groupId);
}
//------------------------------------------------------------------------------

//==============================================================================
// Plugin Object Callbacks - Called from TLab plugin management scripts
//==============================================================================

//==============================================================================
// Called when TorqueLab is launched for first time
function SceneBuilderPlugin::onWorldEditorStartup( %this ) {
	Parent::onWorldEditorStartup( %this );
	%map = new ActionMap();
	%map.bindCmd( keyboard, "1", "EWorldEditorNoneModeBtn.performClick();", "" );  // Select
	%map.bindCmd( keyboard, "2", "EWorldEditorMoveModeBtn.performClick();", "" );  // Move
	%map.bindCmd( keyboard, "3", "EWorldEditorRotateModeBtn.performClick();", "" );  // Rotate
	%map.bindCmd( keyboard, "4", "EWorldEditorScaleModeBtn.performClick();", "" );  // Scale
	%map.bindCmd( keyboard, "f", "FitToSelectionBtn.performClick();", "" );// Fit Camera to Selection
	%map.bindCmd( keyboard, "z", "EditorGuiStatusBar.setCamera(\"Standard Camera\");", "" );// Free camera
	%map.bindCmd( keyboard, "n", "ToggleNodeBar->renderHandleBtn.performClick();", "" );// Render Node
	%map.bindCmd( keyboard, "shift n", "ToggleNodeBar->renderTextBtn.performClick();", "" );// Render Node Text
	%map.bindCmd( keyboard, "g", "ESnapOptions-->GridSnapButton.performClick();" ); // Grid Snappping
	%map.bindCmd( keyboard, "t", "SnapToBar->objectSnapDownBtn.performClick();", "" );// Terrain Snapping
	%map.bindCmd( keyboard, "b", "SnapToBar-->objectSnapBtn.performClick();" ); // Soft Snappping
	%map.bindCmd( keyboard, "v", "SceneBuilderToolbar->boundingBoxColBtn.performClick();", "" );// Bounds Selection
	%map.bindCmd( keyboard, "o", "EToolbarObjectCenterDropdown->objectBoxBtn.performClick(); objectCenterDropdown.toggle();", "" );// Object Center
	%map.bindCmd( keyboard, "p", "EToolbarObjectCenterDropdown->objectBoundsBtn.performClick(); objectCenterDropdown.toggle();", "" );// Bounds Center
	%map.bindCmd( keyboard, "k", "EToolbarObjectTransformDropdown->objectTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// Object Transform
	%map.bindCmd( keyboard, "l", "EToolbarObjectTransformDropdown->worldTransformBtn.performClick(); EToolbarObjectTransformDropdown.toggle();", "" );// World Transform
	SceneBuilderPlugin.map = %map;

	if (SceneBuilderPlugin.getCfg("DropType") !$= "")
		EWorldEditor.dropType = %this.getCfg("DropType");
	
	SBP.initPageBuilder();
	

	//SBP_Creator.initArrayCfg();
	
	ETransformBox.deactivate();
	
	SceneBuilderTools.getToolClones();
	
	hide(SBP_ScenePageSettings);
	
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is activated (Active TorqueLab plugin)
function SceneBuilderPlugin::onActivated( %this ) {
	Parent::onActivated( %this );
	SceneBuilderToolbar-->groundCoverToolbar.visible = 0;
	%this.initToolBar();
	SceneBuilderTreeFilter.extent.x = SceneBuilderTreeTabBook.extent.x -  56;
	SceneBuilderTreeTabBook.selectPage($SBP_TreePage);
	SceneBuilderModeTab.selectPage($SBP_ModePage);
	SBP_Creator.init();
	hide(SBP_CreatorSettings);	
	
	if ($SBP_CreatorTypeActive $= "")
		$SBP_CreatorTypeActive = "Meshes";
		
	%button = SBP_CreatorTypeStack.findObjectByInternalName($SBP_CreatorTypeActive,true);
	if (isObject(%button))
		%button.performClick();
		
	%dropMenu = SceneBuilderTools-->dropTypeMenu;
	%dropId = 0;
	%selDrop = 0;
	%dropMenu.clear();
	foreach$(%dropType in $Scene_AllDropTypes){
		%text = $Scene_DropTypeDisplay[%dropType];
		if (Scene.dropMode $= %dropType)
			%selDrop = %dropId;
		if (%text $= "")
			continue;
		%dropMenu.typeId[%dropId] = %dropType;
		%dropMenu.add("Drop> "@%text,%dropId);
		%dropId++;
	}
	%dropMenu.setSelected(%selDrop);	
	Scene.dropTypeMenus = strAddWord(Scene.dropTypeMenus,%dropMenu.getId(),true);

	if (SceneBuilderPlugin.getCfg("DropType") !$= "")
		EWorldEditor.dropType = %this.getCfg("DropType");
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the Plugin is deactivated (active to inactive transition)
function SceneBuilderPlugin::onDeactivated( %this,%newPlugin ) {	
	//Set last Vis preset file manually loaded	
	Parent::onDeactivated( %this );
}
//------------------------------------------------------------------------------
//==============================================================================
// Called from TorqueLab after plugin is initialize to set needed settings
function SceneBuilderPlugin::onPluginCreated( %this ) {
	EWorldEditor.dropType = SceneBuilderPlugin.getCfg("DropType");
}
//------------------------------------------------------------------------------

//==============================================================================
// Called when the mission file has been saved
function SceneBuilderPlugin::onSaveMission( %this, %file ) {

}
//------------------------------------------------------------------------------
//==============================================================================
// Called when the mission file has been saved
function SceneBuilderPlugin::onExitMission( %this ) {
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when TorqueLab is closed
function SceneBuilderPlugin::onEditorSleep( %this ) {
	%this.setCfg("renameInternal",SceneBuilderTree.renameInternal);
	%this.setCfg("showObjectNames",SceneBuilderTree.showObjectNames);
	%this.setCfg("showInternalNames",SceneBuilderTree.showInternalNames);
	%this.setCfg("showObjectIds",  SceneBuilderTree.showObjectIds);
	%this.setCfg("showClassNames",SceneBuilderTree.showClassNames);
}
//------------------------------------------------------------------------------
//==============================================================================
//Called when editor is selected from menu
function SceneBuilderPlugin::onEditMenuSelect( %this, %editMenu ) {
	%canCutCopy = EWorldEditor.getSelectionSize() > 0;
	%editMenu.enableItem( 3, %canCutCopy ); // Cut
	%editMenu.enableItem( 4, %canCutCopy ); // Copy
	%editMenu.enableItem( 5, EWorldEditor.canPasteSelection() ); // Paste
	%selSize = EWorldEditor.getSelectionSize();
	%lockCount = EWorldEditor.getSelectionLockCount();
	%hideCount = EWorldEditor.getSelectionHiddenCount();
	%editMenu.enableItem( 6, %selSize > 0 && %lockCount != %selSize ); // Delete Selection
	%editMenu.enableItem( 8, %canCutCopy ); // Deselect
}
//------------------------------------------------------------------------------

//==============================================================================
// Callbacks Handlers - Called on specific editor actions
//==============================================================================
$QuickDeleteMode = true;
//==============================================================================
//
function SceneBuilderPlugin::handleDelete( %this ) {	
	// The tree handles deletion and notifies the
	// world editor to clear its selection.
	//
	// This is because non-SceneObject elements like
	// SimGroups also need to be destroyed.
	//
	// See EditorTree::onObjectDeleteCompleted().
	if ($QuickDeleteMode){
		delSelect();
		return;
	}
	
	%selSize = EWorldEditor.getSelectionSize();

	if( %selSize > 0 )
		SceneBuilderTree.deleteSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::handleDeselect() {
	EWorldEditor.clearSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::handleCut() {
	EWorldEditor.cutSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::handleCopy() {
	EWorldEditor.copySelection();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::handlePaste() {
	EWorldEditor.pasteSelection();
}
//------------------------------------------------------------------------------
//==============================================================================
$SceneBuilderPluginToolModes = "Inspector Builder";
//SceneBuilderPlugin.toggleToolMode();
function SceneBuilderPlugin::toggleToolMode( %this ) {
	%lastTool = SceneBuilderTools.getObject(SceneBuilderTools.getCount() -1);
	%currentTool = SceneBuilderTools.getObject(1);
	SceneBuilderTools.reorderChild(%lastTool,%currentTool);
	SceneBuilderTools.updateSizes();
}
//------------------------------------------------------------------------------
//==============================================================================
//SceneBuilderPlugin.setToolMode("Builder");
function SceneBuilderPlugin::setToolMode( %this,%mode ) {
	%toolCtrl = "SceneBuilder"@%mode@"Gui";
	%currentTool = SceneBuilderTools.getObject(1);
	SceneBuilderTools.reorderChild(%toolCtrl,%currentTool);
	SceneBuilderTools.updateSizes();
}
//------------------------------------------------------------------------------
