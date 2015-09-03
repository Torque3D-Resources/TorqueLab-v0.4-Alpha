//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function MatEdDlg::changeMaterialName(%this,%name) {
	%mat = MaterialSelector.selectedMaterial;
	if (!isObject(%mat))
		return;
	
	if (isObject(%name)){
		labMsgOk("Object with same name exist","There's already an object using name:\c1" SPC	%name SPC "\c0. Please make sure to choose a unique name");
		return;
	}
	LabObj.set(%mat,"name",%name);
	LabObj.save(%mat);
}
//------------------------------------------------------------------------------

