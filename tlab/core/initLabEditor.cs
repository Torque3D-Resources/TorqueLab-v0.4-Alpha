//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function Lab::initLabEditor( %this ) {
	if( !isObject( "Lab_PM" ) )
		new PersistenceManager( Lab_PM );

	$LabObj = newScriptObject("LabObj");
	new SimGroup(ToolLabGuiGroup);
	$LabPluginGroup = newSimSet("LabPluginGroup");
	$LabModuleGroup = newSimSet("LabPluginModGroup");
	
	newSimSet( ToolGuiSet );
	newSimSet( EditorPluginSet );
	//Create a group to keep track of all objects set
	newSimGroup( LabSceneObjectGroups );
	//Create the ScriptObject for the Plugin
	new ScriptObject( WEditorPlugin ) {
		superClass = "EditorPlugin"; //Default to EditorPlugin class
		editorGui = EWorldEditor; //Default to EWorldEditor
		isHidden = true;
	};
	

	//Prepare the Settings
	%this.initEditorGui();
	
	//%this.initMenubar();
	%this.initParamsSystem();
	
	 exec("tlab/EditorLab/editorDialogs/guiLab/initGuiLab.cs");
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::initEditorGui( %this ) {
	
	newScriptObject("LabEditor");
	newSimSet("LabGuiSet");
	newSimSet("LabPluginGuiSet");
	newSimSet("LabActiveFrameSet");
	newSimSet("LabEditorGuiSet");
	newSimSet("LabExtraGuiSet");
	newSimSet("LabToolbarGuiSet");
	newSimSet("LabSideBarGuiSet");
	newSimSet("LabGeneratedSet");
	newSimSet("LabToolbarStartGuiSet");
	newSimSet("LabPaletteGuiSet");
	newSimSet("LabDialogGuiSet");
	newSimSet("LabSettingGuiSet");
	newSimSet("LabEditorDlgSet");
	newSimSet("EditorDetachedGuis");
	
	$LabPalletteContainer = EditorGui-->ToolsPaletteContainer;
	$LabPalletteArray = EditorGui-->ToolsPaletteArray;
	$LabPluginsArray = EditorGui-->PluginsArray;
	$LabWorldContainer = EditorGui-->WorldContainer;
	$LabSettingContainer = EditorGui-->SettingContainer;
	$LabToolbarContainer = EditorGui-->ToolbarContainer;
	$LabSideBarContainer = EditorGui-->SideBarContainer;
	$LabToolbarEndContainer = EditorGui-->ToolbarContainerEnd;
	$LabToolbarStartContainer = EditorGui-->ToolbarContainerStart;
	$LabDialogContainer = EditorGui-->ToolsContainer;
	$LabEditorContainer = EditorGui-->EditorContainer;
	$LabExtraContainer = EditorGui-->ExtraContainer;
	
}
//------------------------------------------------------------------------------

//==============================================================================
// All the plugins scripts have been loaded
function Lab::pluginInitCompleted( %this ) {
	//%this.prepareAllPluginsGui();
	//ETools.initTools();
	//Prepare the Settings
	Lab.BuildMenus();
	Lab.initConfigSystem();
	Lab.initAllConfigArray();
	
}
//------------------------------------------------------------------------------

