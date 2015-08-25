//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

%helpersLab = "tlab/helpers/initHelpers.cs";

if (isFile(%helpersLab))
	exec(%helpersLab);

//If true , you need to add a call to initTorqueLab where you want
$TorqueLabManualInit = false;

//==============================================================================
// TorqueLab Core Global Settings
//==============================================================================
// Path to the folder that contains the editors we will load.
$Lab::resourcePath = "tlab/";

if (isObject($pref::UI::defaultGui))
	$TLab_defaultGui = $pref::UI::defaultGui;
else
	$TLab_defaultGui = "MainMenuGui";

$LabGameMap = "moveMap";
// Global holding material list for active simobject
$Lab::materialEditorList = "";

// These must be loaded first, in this order, before anything else is loaded
$Lab::loadFirst = "sceneEditor";
$Lab::loadLast = "";
// These folders must be skipped for initial load
$LabIgnoreEnableFolderList = "debugger forestEditor levels";

//==============================================================================
// TorqueLab Editor Initialization Function
//==============================================================================
$TorqueLabLoaded = false;
function initTorqueLab() {
	if ($TorqueLabLoaded)
		return;
	
	//Load the Editor profile before loading the game onStart (Nooo...)
	exec("tlab/gui/initGuiProfiles.cs");
	exec( "tlab/EditorLab/gui/core/cursors.ed.cs" );
	
	//loadTorqueLabProfiles();
	new Settings(EditorSettings) {
		file = "tlab/settings.xml";
	};
	EditorSettings.read();
	new Settings(LabCfg) {
		file = "tlab/core/configs/config.xml";
	};
	info( "Initializing TorqueLab" );

	// Default file path when saving from the editor (such as prefabs)
	if ($Pref::WorldEditor::LastPath $= "") {
		$Pref::WorldEditor::LastPath = getMainDotCsDir();
	}

	$Lab = new ScriptObject(Lab);
	exec("tlab/core/execScripts.cs");
	$LabGuiExeced = true;
	// Common GUI stuff.
	Lab.initLabEditor();
	//%toggle = $Scripts::ignoreDSOs;
	//$Scripts::ignoreDSOs = true;
	$ignoredDatablockSet = new SimSet();
	// fill the list of editors
	$editors[count] = getWordCount( $Lab::loadFirst );

	for ( %i = 0; %i < $editors[count]; %i++ ) {
		$editors[%i] = getWord( $Lab::loadFirst, %i );
	}

	%pattern = $Lab::resourcePath @ "/*/main.cs";
	%folder = findFirstFile( %pattern );

	if ( %folder $= "") {
		// if we have absolutely no matches for main.cs, we look for main.cs.dso
		%pattern = $Lab::resourcePath @ "/*/main.cs.dso";
		%folder = findFirstFile( %pattern );
	}

	while ( %folder !$= "" ) {
		if( filePath( %folder ) !$= "tools" && filePath( %folder ) !$= "tlab" ) { // Skip the actual 'tools' folder...we want the children
			%folder = filePath( %folder );
			%editor = fileName( %folder );

			if ( IsDirectory( %folder ) ) {
				// Yes, this sucks and should be done better
				if ( strstr( $Lab::loadFirst, %editor ) == -1 ) {
					$editors[$editors[count]] = %editor;
					$editors[count]++;
				}
			}
		}

		%folder = findNextFile( %pattern );
	}

	// initialize every editor
	%count = $editors[count];

	//  exec( "./worldEditor/main.cs" );
	foreach$(%tmpFolder in $LabIgnoreEnableFolderList)
		$ToolFolder[%tmpFolder] = "1";

	for ( %i = 0; %i < %count; %i++ ) {
		eval("%enabledEd = $pref::WorldEditor::"@$editors[%i]@"::Enabled;");

		if (!%enabledEd && !$ToolFolder[%tmpFolder]) {
			continue;
		}

		if (strFind($Lab::loadLast,$editors[%i])) {
			%finalLoadList = strAddWord(%finalLoadList,$editors[%i]);
			continue;
		}

		exec( "./" @ $editors[%i] @ "/main.cs" );
		%initializeFunction = "initialize" @ $editors[%i];

		if( isFunction( %initializeFunction ) )
			call( %initializeFunction );
	}

	foreach$(%editor in %finalLoadList) {
		devLog("Loading last editor:",%editor);
		exec( "./" @ %editor @ "/main.cs" );
		%initializeFunction = "initialize" @ %editor;

		if( isFunction( %initializeFunction ) )
			call( %initializeFunction );
	}

	// Popuplate the default SimObject icons that
	// are used by the various editors.
	EditorIconRegistry::loadFromPath( "tlab/gui/icons/class_assets/" );

	//$Scripts::ignoreDSOs = %toggle;
	Lab.pluginInitCompleted();
	$TorqueLabLoaded = true;
}
package Lab {

	// Start-up.
	function onStart() {
		if ($TorqueLabManualInit) {
			Parent::onStart();
			return;
		}

		Parent::onStart();
		initTorqueLab();
	}

	
	// Shutdown.
	function onExit() {
		if( LabEditor.isInitialized )
			EditorGui.shutdown();

		// Free all the icon images in the registry.
		EditorIconRegistry::clear();
		// Save any Layouts we might be using
		//GuiFormManager::SaveLayout(LevelBuilder, Default, User);
		%count = $editors[count];

		for (%i = 0; %i < %count; %i++) {
			%destroyFunction = "destroy" @ $editors[%i];

			if( isFunction( %destroyFunction ) )
				call( %destroyFunction );
		}

		// Call Parent.
		Parent::onExit();
		// write out our settings xml file
		//EditorSettings.write();
	}
};



//-----------------------------------------------------------------------------
// Activate Package.
//-----------------------------------------------------------------------------
activatePackage(Lab);

