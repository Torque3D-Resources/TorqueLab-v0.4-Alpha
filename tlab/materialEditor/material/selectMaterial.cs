//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Set GUI depending of if we have a standard Material or a Mesh Material
function MaterialEditorGui::setMode( %this ) {
	MatEdMaterialMode.setVisible(0);
	MatEdTargetMode.setVisible(0);

	if( isObject(MaterialEditorGui.currentObject) ) {
		MaterialEditorGui.currentMode = "Mesh";
		MatEdTargetMode.setVisible(1);
	} else {
		MaterialEditorGui.currentMode = "Material";
		MatEdMaterialMode.setVisible(1);
		EWorldEditor.clearSelection();
	}
}
//------------------------------------------------------------------------------