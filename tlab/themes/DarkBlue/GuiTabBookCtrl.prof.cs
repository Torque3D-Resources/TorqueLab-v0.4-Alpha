//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// GuiTabBookCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsTabBookMain Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTabBookMain : ToolsDefaultProfile ) {
	hasBitmapArray = true;
	category = "ToolsContainers";
	fontColors[9] = "255 0 255 255";
	autoSizeWidth = "1";
	autoSizeHeight = "1";
	profileForChildren = "ToolsBoxDarkC_Top";
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiTabBookMain.png";
	border = "5";
	borderThickness = "14";
	fontType = "Lato bold";
	fontSize = "16";
	justify = "Bottom";
	fontColors[0] = "220 219 213 255";
	fontColor = "220 219 213 255";
	textOffset = "0 0";
	fontColors[3] = "83 83 83 255";
	fontColorSEL = "83 83 83 255";
	opaque = "1";
	fillColor = "32 80 18 0";
	borderColor = "118 118 118 0";
};
//------------------------------------------------------------------------------




singleton GuiControlProfile(ToolsTabBookRound : ToolsTabBookMain) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiTabBookRound.png";
};
