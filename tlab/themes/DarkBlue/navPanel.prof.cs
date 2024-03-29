//==============================================================================
// TorqueLab -> Core Nav Panel Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//==============================================================================


singleton GuiControlProfile (NavPanelProfile) {
	opaque = false;
	border = -2;
	category = "Editor";
};


singleton GuiControlProfile (NavPanel : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel";
	category = "Editor";
};

singleton GuiControlProfile (NavPanelBlue : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel_blue";
	category = "Editor";
};

singleton GuiControlProfile (NavPanelGreen : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel_green";
	category = "Editor";
};

singleton GuiControlProfile (NavPanelRed : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel_red";
	category = "Editor";
};

singleton GuiControlProfile (NavPanelWhite : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel_white";
	category = "Editor";
};

singleton GuiControlProfile (NavPanelYellow : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/navPanel_yellow";
	category = "Editor";
};
singleton GuiControlProfile (menubarProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/menubar";
	category = "Editor";
};
singleton GuiControlProfile (editorMenubarProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/editor-menubar";
	category = "Editor";
};
singleton GuiControlProfile (editorMenu_wBorderProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/menu-fullborder";
	category = "Editor";
};
singleton GuiControlProfile (inspectorStyleRolloutListProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/inspector-style-rollout-list";
	category = "Editor";
	hasBitmapArray = "1";
   fillColorNA = "White";
   fontColors[8] = "255 0 255 255";
};
singleton GuiControlProfile (inspectorStyleRolloutListProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/inspector-style-rollout-list";
	category = "Editor";
};
singleton GuiControlProfile (inspectorStyleRolloutInnerProfile : NavPanelProfile) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiRolloutTitle_S1.png";
	category = "Editor";
	fontColors[5] = "Fuchsia";
	fontColorLinkHL = "Fuchsia";
	hasBitmapArray = "1";
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
};
singleton GuiControlProfile (inspectorStyleRolloutInnerProfile : NavPanelProfile) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiRolloutTitle_S1.png";
	category = "Editor";
	bevelColorLL = "255 0 255 255";
	fontColors[1] = "0 0 0 255";
	fontColorHL = "0 0 0 255";
};
singleton GuiControlProfile (IconDropdownProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/icon-dropdownbar";
	category = "Editor";
   fontColors[8] = "255 0 255 255";
};
singleton GuiControlProfile (IconDropdownProfile : NavPanelProfile) {
	bitmap = "tlab/gui/icons/default/panels/icon-dropdownbar";
	category = "Editor";
   fontColors[7] = "Fuchsia";
   fontColors[8] = "255 0 255 255";
};
