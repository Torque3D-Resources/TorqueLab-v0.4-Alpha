//==============================================================================
// TorqueLab -> Initialize editor plugins
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TLab::DefaultPlugins = "SceneEditor";

//==============================================================================
// Make sure all GUIs are fine once the editor is launched
function Lab::EditorLaunchGuiSetup(%this) {
	//Lab.attachAllEditorGuis();
	Lab.closeDisabledPluginsBin();
	
	show(EWToolsToolbar);
	show(EWToolsPaletteContainer);
	ToolsToolbarArray.reorderChild( ToolsToolbarArray-->SceneEditorPlugin,ToolsToolbarArray.getObject(0));
	ToolsToolbarArray.refresh();
	Lab.updateActivePlugins();
	
		EditorFrameWorld.pushToBack(EditorFrameTools);
	

	EditorGuiToolbarStack.bringToFront(EditorGuiToolbarStack-->FirstToolbarGroup);
	EditorGuiToolbarStack.pushToBack(EditorGuiToolbarStack-->LastToolbarGroup);
}
//------------------------------------------------------------------------------


//==============================================================================
// Make sure all GUIs are fine once the editor is launched
function Lab::InitialGuiSetup(%this,%pluginName,%displayName,%alwaysEnable) {
	Lab.updatePluginsBar();
	Lab.sortPluginsBar(true);
	ETools.initTools();
	
	//Store some dimension for future session update
	Lab.toolbarHeight = EditorGuiToolbar.y;
	Lab.pluginBarHeight = EWToolsToolbar.y;
	Lab.paletteBarWidth = EWToolsPaletteContainer.x;
	
	Lab.loadPluginsPalettes();
	
	ESnapOptions-->TabBook.selectPage(0);
	EVisibilityLayers-->TabBook.selectPage(0);
	//===========================================================================
	// Add the TorqueLab Universal GUIs to the editor
	Lab.addGui( EToolOverlayGui ,"Overlay");
	Lab.addGui( ETools ,"Dialog");	
	Lab.addGui( ESceneManager ,"Dialog");
	Lab.addGui( EManageBookmarks ,"Dialog");
	Lab.addGui( EManageSFXParameters ,"Dialog");
	Lab.addGui( ESelectObjects ,"Dialog");
	Lab.addGui( EPostFxManager ,"EditorDlg");	
	Lab.addGui( StackStartToolbar ,"Toolbar",true);
	Lab.addGui( StackEndToolbar ,"Toolbar",true);
	//---------------------------------------------------------------------------
	
	StackStartToolbar.isCommon = true;
	StackEndToolbar.isCommon = true;
	
	EWorldEditorAlignPopup.clear();
	EWorldEditorAlignPopup.add("World",0);
	EWorldEditorAlignPopup.add("Object",1);
	EWorldEditorAlignPopup.setSelected(0);
	
	// this will brind EToolDlgCameraTypes to front so that it goes over the menubar
	EditorGui.pushToBack(EToolDlgCameraTypes);
	EditorGui.pushToBack(VisibilityDropdown);
	
	
	Lab.initCoreGuis();
	Lab.resizeEditorGui();
	//Lab.initObjectConfigArray(EWorldEditor,"WorldEditor","General");
	
	Lab.initAllToolbarGroups();
	
	Lab.initToolbarTrash();
}
//------------------------------------------------------------------------------