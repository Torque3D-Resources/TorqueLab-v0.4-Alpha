//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Set GUI depending of if we have a standard Material or a Mesh Material
function MaterialLabGui::setMode( %this ) {
	MatLabMaterialMode.setVisible(0);
	MatLabTargetMode.setVisible(0);

	if( isObject(MaterialLabGui.currentObject) ) {
		MaterialLabGui.currentMode = "Mesh";
		MatLabTargetMode.setVisible(1);
	} else {
		MaterialLabGui.currentMode = "Material";
		MatLabMaterialMode.setVisible(1);
		EWorldEditor.clearSelection();
	}
}
//------------------------------------------------------------------------------