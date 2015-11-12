//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================




//------------------------------------------------------------------------------
//Load GameLab system (In-Game Editor)
function Lab::ExportPrefs( %loadGui ) {
	export("$Tlab_*", "tlab/myPrefs.cs", false);
	
	foreach(%plugin in LabPluginGroup){
		if (%plugin.prefPrefix !$= ""){
		export("$"@%plugin.prefPrefix@"_*", "tlab/myPrefs.cs", true);	
		}
	}	
}
