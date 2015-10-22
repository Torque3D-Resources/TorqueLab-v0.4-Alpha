//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Add the various TorqueLab GUIs to the container and set they belong
function Lab::addGui(%this,%gui,%type,%noHide) {
	%parent = %gui.parentGroup;

	switch$(%type) {
	case "Gui":
		%container = $LabSettingContainer;
		LabSettingGuiSet.add(%gui);

	case "EditorGui":
		%container = $LabEditorContainer;
		LabEditorGuiSet.add(%gui);

	case "ExtraGui":
		%container = $LabExtraContainer;
		LabExtraGuiSet.add(%gui);

	case "Toolbar":
		%container = $LabToolbarContainer;
		LabToolbarGuiSet.add(%gui);
	
	case "SideBar":
		%container = $LabSideBarContainer;
		LabSideBarGuiSet.add(%gui);

	case "Dialog":
		%container = $LabDialogContainer;
		LabDialogGuiSet.add(%gui);

	case "Palette":
		LabPaletteGuiSet.add(%gui);

	case "Overlay":
		%container = EditorGui;
		LabDialogGuiSet.add(%gui);

	case "EditorDlg":
		%container = EditorDialogs;
		LabEditorDlgSet.add(%gui);

	case "":
		%container = $LabSettingContainer;
		LabSettingGuiSet.add(%gui);
	}

	%gui.defaultParent = %gui.parentGroup;
	LabGuiSet.add(%gui); // Simset Holding all Editor Guis

	if (!%noHide)
		hide(%gui);

	if (isObject(%container)) {
		%container.add(%gui);
		%gui.editorContainer = %container;
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::initCoreGuis(%this) {
	foreach(%dlg in ETools) {
		hide(%dlg);
	}
}


//==============================================================================
// Detach the GUIs not saved with EditorGui (For safely save EditorGui)
//==============================================================================
//==============================================================================
function Lab::detachAllEditorGuis(%this) {
	%this.editorGuisDetached = true;
	
	foreach(%gui in LabGeneratedSet)
		%this.detachEditorGui(%gui);

	foreach(%gui in LabGuiSet)
		%this.detachEditorGui(%gui);

	foreach(%gui in LabPaletteItemSet)
		%this.detachEditorGui(%gui,true);

	foreach(%obj in EWToolsPaletteArray)
		%paletteList = strAddWord(%paletteList,%obj.getId());

	foreach$(%id in %paletteList)
		%this.detachEditorGui(%id,true);
	
	foreach(%gui in LabSideBarGuiSet)
		%this.detachEditorGui(%gui);
	

	%this.resetPluginsBar();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::detachEditorGui(%this,%gui,%dontShow) {
	EditorDetachedGuis.add(%gui);
	%gui.editorParent = %gui.parentGroup;
	%parent = %gui.defaultParent;

	if (!isObject(%parent))
		%parent = GuiGroup;

	%parent.add(%gui);

	if (%dontShow)
		return;

	show(%gui);
}
//------------------------------------------------------------------------------
//==============================================================================
// Reattach the GUIs not saved with EditorGui (For safely save EditorGui)
//==============================================================================
//==============================================================================
function Lab::attachAllEditorGuis(%this) {
	if (!%this.editorGuisDetached)
		return;

	Lab.editorGuisDetached = false;
	
	foreach(%gui in EditorDetachedGuis) 
		%this.attachEditorGui(%gui);	
	

	//foreach(%gui in LabGuiSet) 
		//%this.attachEditorGui(%gui);	

	foreach(%item in LabPaletteItemSet) {
		$LabPalletteArray.add(%item);
	}

	Lab.updatePluginsBar();
	//Now make sure everything fit together
	%this.resizeEditorGui();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::attachEditorGui(%this,%gui,%dontShow) {
	%gui.parentGroup = %gui.editorParent;

		if (%gui.isCommon)
			show(%gui);
		else
			hide(%gui);
}
//------------------------------------------------------------------------------
//==============================================================================
// Clear the editors menu (for dev purpose only as now)
function Lab::resizeEditorGui( %this ) {
	//-----------------------------------------------------
	// ToolsToolbar
	EWToolsToolbar.position = "0 0";// SPC EditorGuiToolbar.extent.y;
	EWToolsToolbar.extent  = "43 33" ;
	EWToolsToolbar-->resizeArrow.position = getWord(EWToolsToolbar.Extent, 0) - 7 SPC "0";
	EWToolsToolbar.expand();
	EWToolsPaletteContainer.position.y =  EWToolsToolbar.extent.y;// + EditorGuiToolbar.extent.y;
	EWToolsPaletteContainer.position.x = "0";
	//-----------------------------------------------------
	// VisibilityLayerContainer
	EVisibility.Position = getWord(visibilityToggleBtn.position, 0) SPC getWord(EditorGuiToolbar.extent, 1);

	//-----------------------------------------------------
	// CameraSpeedDropdownCtrlContainer
	/*	CameraSpeedDropdownCtrlContainerA.position = firstWord(CameraSpeedDropdownContainer.position) + firstWord(EditorGuiToolbar.position) + -6 SPC
				(getWord(CameraSpeedDropdownContainer, 1)) + 31;
		softSnapSizeSliderCtrlContainer-->slider.position = firstWord(SceneEditorToolbar-->softSnapSizeTextEdit.getGlobalPosition()) - 12 SPC
				(getWord(SceneEditorToolbar-->softSnapSizeTextEdit.getGlobalPosition(), 1)) + 18;
	*/
	foreach(%gui in $LabEditorContainer)
		%gui.fitIntoParents();

	foreach(%gui in LabEditorGuiSet) {
		%gui.fitIntoParents();
	}

	foreach(%gui in LabPaletteGuiSet) {
		%gui.extent.x = EWToolsPaletteContainer.extent.x;
	}
}
//------------------------------------------------------------------------------

