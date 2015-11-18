//==============================================================================
// TorqueLab -> Default ToolBoxes Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Box Dark Profiles
//==============================================================================

//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkA : ToolsDefaultProfile ) {
	opaque = false;
	border = -2;
	category = "ToolsContainers";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkA.png";
	fontColors[2] = "0 0 0 255";
	fontColorNA = "0 0 0 255";
	hasBitmapArray = "1";
	fillColor = "19 40 55 255";
	fontType = "Anson Regular";
	fontColors[4] = "Magenta";
	fontColorLink = "Magenta";
	bevelColorLL = "Magenta";
	fontColors[3] = "255 255 255 255";
	fontColorSEL = "255 255 255 255";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkB : ToolsBoxDarkA ) {
	opaque = false;
	border = -2;
	category = "ToolsContainers";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkB.png";
	fontColors[2] = "254 227 83 255";
	fontColorNA = "254 227 83 255";
	hasBitmapArray = "1";
	fillColor = "14 151 226 255";
	fillColorSEL = "101 136 166 255";
	fontType = "Lato";
	fontColors[0] = "252 254 252 255";
	fontColors[1] = "252 189 81 255";
	fontColors[3] = "29 104 143 255";
	fontColors[4] = "238 255 0 255";
	fontColors[5] = "252 189 81 255";
	fontColors[6] = "3 21 254 255";
	fontColors[7] = "254 236 3 255";
	fontColors[8] = "254 3 43 255";
	fontColors[9] = "3 48 248 255";
	fontColor = "252 254 252 255";
	fontColorHL = "252 189 81 255";
	fontColorSEL = "29 104 143 255";
	fontColorLink = "238 255 0 255";
	fontColorLinkHL = "252 189 81 255";
	bevelColorLL = "Fuchsia";
	bevelColorHL = "Magenta";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkC : ToolsBoxDarkA ) {
	opaque = "1";
	border = -2;
	category = "ToolsContainers";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkC.png";
	fontColors[2] = "0 0 0 255";
	fontColorNA = "0 0 0 255";
	hasBitmapArray = "1";
	fillColor = "19 40 55 255";
	fontType = "Anson Regular";
	fontColors[7] = "Fuchsia";
	fontColors[5] = "Fuchsia";
	fontColorLinkHL = "Fuchsia";
	bevelColorLL = "255 0 255 255";
	fontColors[4] = "255 0 255 255";
	fontColorLink = "255 0 255 255";
	bevelColorHL = "Magenta";
};
//------------------------------------------------------------------------------

singleton GuiControlProfile(ToolsBoxDarkC_Top : ToolsBoxDarkC) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkC_Top.png";
	fontColors[5] = "Magenta";
	fontColorLinkHL = "Magenta";
};


//==============================================================================
// GuiContainer Profiles
//==============================================================================

//==============================================================================
//ToolsBoxTitleDark Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsBoxTitleDark : ToolsDefaultProfile ) {
	opaque = false;
	border = -2;
	category = "ToolsContainers";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxTitleDark";
	fontColors[2] = "0 0 0 255";
	fontColorNA = "0 0 0 255";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsBoxTitle Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsBoxTitle : ToolsDefaultProfile ) {
	opaque = false;
	border = -2;
	category = "ToolsContainers";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxTitleBar.png";
	fontColors[2] = "0 0 0 255";
	fontColorNA = "0 0 0 255";
	fontType = "Gotham Black";
	fontSize = "17";
	fontColors[0] = "222 222 222 255";
	fontColor = "222 222 222 255";
	fontColors[4] = "Magenta";
	fontColorLink = "Magenta";
};
//------------------------------------------------------------------------------

singleton GuiControlProfile(ToolsBoxTitle_Thin : ToolsBoxTitle) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxTitleBar_Thin.png";
};

//------------------------------------------------------------------------------

singleton GuiControlProfile(ToolsBoxDarkB_Top : ToolsBoxDarkB) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkB_Top.png";
};

singleton GuiControlProfile(ToolsBoxDarkA_Top : ToolsBoxDarkA) {
	bevelColorHL = "Fuchsia";
	fontColors[0] = "Black";
	fontColor = "Black";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiBoxDarkA_Top.png";
};
