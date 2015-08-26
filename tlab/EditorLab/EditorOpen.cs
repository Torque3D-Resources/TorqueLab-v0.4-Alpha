//==============================================================================
// TorqueLab -> Editor Gui Open and Closing States
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Initial Editor launch call from EditorManager
function Editor::open(%this) {
	logb("Editor::open( %this )");
	// prevent the mission editor from opening while the GuiEditor is open.
	if(Canvas.getContent() == GuiEditorGui.getId())
		return;
		
	$EClient = LocalClientConnection;

	

	if( !LabEditor.isInitialized )
		Lab.onInitialEditorLaunch();
	
	//EditManager call to set the Editor Enabled
	%this.editorEnabled();
	
	Lab.setEditor( Lab.currentEditor, true );
	
	//Store current content and set EditorGui as content 
	EditorGui.previousGui = Canvas.getContent(); 
	Canvas.setContent(EditorGui);

	//The default menu seem to be create somewhere between setCOntent and here
	if(!$Cfg_UseCoreMenubar && isObject(Lab.menuBar)){
		flog("Editor::open removeMenu");
		Lab.menuBar.removeFromCanvas();
	}

	Lab.EditorLaunchGuiSetup();
	Lab.onEditorOpen();
}
//------------------------------------------------------------------------------
//==============================================================================
// EditorGui OnWake -> When the EditorGui is rendered
function EditorGui::onWake( %this ) {
	logb("EditorGui::onWake( %this )");
	
	Lab.setInitialCamera();
	
	//EHWorldEditor.setStateOn( 1 );
	startFileChangeNotifications();
	// Notify the editor plugins that the editor has started.

	Lab.onEditorWake();
	
	
	//Reset the TLabGameGui to default state
	TLabGameGui.reset();

	if(Canvas.getContent() == GuiEditorGui.getId())
		return;
	
	// Push the ActionMaps in the order that we want to have them
	// before activating an editor plugin, so that if the plugin
	// installs an ActionMap, it will be highest on the stack.
	MoveMap.push();
	EditorMap.push();

	// Active the current editor plugin.
	if( !Lab.currentEditor.isActivated )
		Lab.currentEditor.onActivated();

	%slashPos = 0;
	while( strpos( $Server::MissionFile, "/", %slashPos ) != -1 ) 
		%slashPos = strpos( $Server::MissionFile, "/", %slashPos ) + 1;	

	%levelName = getSubStr( $Server::MissionFile , %slashPos , 99 );

	if( %levelName !$= Lab.levelName )
		%this.onNewLevelLoaded( %levelName );
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when we have been set as the content and onWake has been called
function EditorGui::onSetContent(%this, %oldContent) {
	flog("EditorGui::onSetContent");
	Lab.attachMenus();
}
//------------------------------------------------------------------------------
//==============================================================================
//EditManager Functions
//==============================================================================


//==============================================================================
function Lab::onInitialEditorLaunch( %this ) {
	logc("Lab::onInitialEditorLaunch( %this )",%this);	
	%this.initialPluginsSetup();
	%this.InitialGuiSetup();
	
	
	EWorldEditor.isDirty = false;
	ETerrainEditor.isDirty = false;
	ETerrainEditor.isMissionDirty = false;
	//Get up-to-date config
	Lab.readAllConfigArray();
	

	if( LabEditor.isInitialized )
		return;

	$SelectedOperation = -1;
	$NextOperationId   = 1;
	$HeightfieldDirtyRow = -1;
	
	//-----------------------------------------------------
	
	EWorldEditor.init();
	EWorldEditor.setDisplayType($EditTsCtrl::DisplayTypePerspective);
	ETerrainEditor.init();
	//Creator.init();
	SceneCreatorWindow.init();
	ObjectBuilderGui.init();
	Lab.setMenuDefaultState();
	
	
	Lab.initEditorCamera();
	// sync camera gui
	Lab.syncCameraGui();
	
	// dropdowns out so that they display correctly in editor gui
	// make sure to show the default world editor guis
	EditorGui.bringToFront( EWorldEditor );
	EWorldEditor.setVisible( false );

	// Call the startup callback on the editor plugins.	
	for ( %i = 0; %i < EditorPluginSet.getCount(); %i++ ) {
		%obj = EditorPluginSet.getObject( %i );
		%obj.onWorldEditorStartup();
	}

	Lab.AddSelectionCallback("ETransformBox.updateSource","Transform");	
}
//------------------------------------------------------------------------------


//-----------------------------------------------------------------------------

//==============================================================================
// Called when we have been set as the content and onWake has been called
function worldStart() {
	logb("worldStart");
	// Call the startup callback on the editor plugins.
	for ( %i = 0; %i < EditorPluginSet.getCount(); %i++ ) {
		%obj = EditorPluginSet.getObject( %i );
		%obj.onWorldEditorStartup();
	}
}
//------------------------------------------------------------------------------


//-----------------------------------------------------------------------------


//------------------------------------------------------------------------------

