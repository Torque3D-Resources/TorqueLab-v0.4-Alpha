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

	if (isObject(%name)) {
		labMsgOk("Object with same name exist","There's already an object using name:\c1" SPC	%name SPC "\c0. Please make sure to choose a unique name");
		return;
	}

	%mat.setName(%name);
	matEd_PersistMan.setDirty(%name);
	matEd_PersistMan.saveDirtyObject(%name);
}
//------------------------------------------------------------------------------

//==============================================================================
function MatEdDlg::setAsActive(%this,%name) {
	%mat = MaterialSelector.selectedMaterial;

	if (!isObject(%mat))
		return;

	MaterialEditorGui.setActiveMaterial(%mat);
}
//------------------------------------------------------------------------------
//==============================================================================
function MatEdDlg::setListFilterText(%this,%text) {
	%filterText = %text;

	if (%text $= "" || strFind(%text,"Filter...")) {
		%text = "\c1Filter...";
		MatSel_ListFilterText.setText(%text);
		%filterText = "";
	}

	MatEdDlg.filterText = %filterText;
	MaterialSelector.loadFilter( MaterialSelector.currentFilter, MaterialSelector.currentStaticFilter );
}
//------------------------------------------------------------------------------
//==============================================================================
function MatEdDlg::updateListFilteredText(%this,%filterText) {
}
//------------------------------------------------------------------------------