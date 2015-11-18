//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$MatEdRollOutList = "TextureMaps PBR Tranparency MaterialDamage Accumulation lighting animation advanced";

//==============================================================================
// Helper functions to help load and update the preview and active material
//------------------------------------------------------------------------------
//==============================================================================
// Finds the selected line in the material list, then makes it active in the editor.
function MaterialEditorGui::initGui(%this) {
	foreach$(%rollout in $MatEdRollOutList){
		%gui = MatEd_Rollouts.findObjectByInternalName(%rollout);
		if (!isObject(%gui))
			continue;
		
		%gui.expanded = $MatEd_RolloutExpanded[%rollout];
	}
}
//------------------------------------------------------------------------------
