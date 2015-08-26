//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

%helpersLab = "tlab/helpers/initHelpers.cs";

if (isFile(%helpersLab))
	exec(%helpersLab);

//==============================================================================
//Do TorqueLab should be initialized after main onStart?
$TorqueLabInitOnStart = false; //Start, Custom, FunctionCall
//If $TorqueLabInitOnStart is set to false, you need to initialize it manually
//There's 3 options available for post start initialization
//1- It will initialized when you press F10 or F11 (Editors toggle binds)
//2- Add a initTorqueLab(); call anywhere in your scripts
//3- Use the custom function overide package at bottom (At end of Lab Package)
//   You simply set the function from which you want TorqueLab to be initialized.
//------------------------------------------------------------------------------

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

//Load the TorqueLab Main Initialization script.
exec("tlab/initTorqueLab.cs");

//==============================================================================
// Lab Package contain overide function that deal with TorqueLab
//==============================================================================
package Lab {
	//==============================================================================
	// onStart() - Called when the application is launched by the engine
	//------------------------------------------------------------------------------
	// If $TorqueLabInitOnStart is true, TorqueLab will be loaded at end of onStart
	// Else, it will bind the editor toggle keys to the init function and TorqueLab
	// will be initialized when any editor is toggled. It will launch the editor once
	// initialization is completed. If TLab is loaded manually from elsewhere, those
	// binds will be overiden with the normal toggle binds.
	//------------------------------------------------------------------------------
	function onStart() {
		if (!$TorqueLabInitOnStart) {
			Parent::onStart();
			GlobalActionMap.bindCmd(keyboard,"F10","initTorqueLab(\"Gui\");","");
			GlobalActionMap.bindCmd(keyboard,"F11","initTorqueLab(\"World\");","");
			return;
		}

		Parent::onStart();
		initTorqueLab();
	}
	//------------------------------------------------------------------------------

	//==============================================================================
	// onExit() - Call just before the application is shutted down
	//------------------------------------------------------------------------------
	// This will terminated some editor process and make sure everything is deleted
	//------------------------------------------------------------------------------
	function onExit() {
		if( LabEditor.isInitialized )
			EditorGui.shutdown();

		// Free all the icon images in the registry.
		EditorIconRegistry::clear();

		//Call destroy function of all editors
		for (%i = 0; %i < $editors[count]; %i++) {
			%destroyFunction = "destroy" @ $editors[%i];

			if( isFunction( %destroyFunction ) )
				call( %destroyFunction );
		}

		// Call Parent.
		Parent::onExit();
	}
	//------------------------------------------------------------------------------
	//==============================================================================
	//Custom Overide Function TorqueLab initialization (Example)
	//------------------------------------------------------------------------------
	// This is another way to initialize TorqueLab from anywhere, just set the function
	// information from which you want TLab to be loaded. If you want it to load at start
	// of the function, simply place initTorqueLab(); before the Parent:: call. The example
	// will make TorqueLab to load at the end of the StartupGui onWake function.
	//------------------------------------------------------------------------------
	// Uncomment and set on which function call you want to load TorqueLab
	function StartupGui::onWake(%this) {
		Parent::onWake(%this);
		//initTorqueLab();
		schedule(500,0,"initTorqueLab");
	}
	//------------------------------------------------------------------------------
};
//------------------------------------------------------------------------------
//==============================================================================
// Activate Package Lab
activatePackage(Lab);
//------------------------------------------------------------------------------
