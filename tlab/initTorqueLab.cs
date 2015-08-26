//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// TorqueLab Editor Initialization Function
//==============================================================================
$TorqueLabLoaded = false;
function initTorqueLab(%launchEditor) {
	if ($TorqueLabLoaded)
		return;
	
	//Load the Editor profile before loading the game onStart (Nooo...)
	exec("tlab/gui/initGuiProfiles.cs");
	exec( "tlab/EditorLab/gui/core/cursors.ed.cs" );
	
	//Start by loading the TorqueLab Loading Progress GUI to use it now
	exec("tlab/EditorLab/gui/core/EditorLoadingGui.gui");
	exec("tlab/EditorLab/gui/core/EditorLoadingGui.cs");
	EditorLoadingGui.startInit();
	
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
	if (%launchEditor $= "Gui")
		ToggleGuiEdit();
	else if (%launchEditor $= "World")
		toggleEditor( true );
	else
		EditorLoadingGui.endInit();
}