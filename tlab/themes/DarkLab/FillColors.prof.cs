//==============================================================================
// TorqueLab -> Panels Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Special Panels which simply set predefined fill color and appropriate text
//==============================================================================

//==============================================================================
// Color Panels -> Main theme colors (A-B-C)
//==============================================================================

//==============================================================================
singleton GuiControlProfile(ToolsFillAccentA : ToolsDefaultProfile) {
	fillColor = "17 45 58 255";
	opaque = "1";
	bevelColorHL = "Fuchsia";
	fontType = "Aileron";
	fontSize = "16";
	fontColors[4] = "255 0 255 255";
	fontColorLink = "255 0 255 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	category = "ToolsFills";
	fillColorNA = "255 255 255 255";
	fontColors[3] = "255 255 255 255";
	fontColorSEL = "255 255 255 255";
	fontColors[9] = "Fuchsia";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsFillAccentB : ToolsFillAccentA) {
	opaque = "1";
	fillColor = "101 136 166 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "0 0 0 255";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "0 0 0 255";
	fillColorNA = "255 255 255 255";
	fontColors[9] = "Magenta";
	bevelColorHL = "255 0 255 255";
	fontColors[8] = "Magenta";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsFillAccentC : ToolsFillAccentA) {
	opaque = "1";
	fillColor = "101 136 166 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "0 0 0 255";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "0 0 0 255";
	bevelColorHL = "Magenta";
	bevelColorLL = "Fuchsia";
};
//------------------------------------------------------------------------------

//==============================================================================
// Dark Color Panels -> Dark theme colors (A-B-C)
//==============================================================================

//==============================================================================

singleton GuiControlProfile(ToolsFillDarkA : ToolsDefaultProfile) {
	opaque = "1";
	fillColor = "28 28 28 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "Black";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "Black";
	fontColors[9] = "Magenta";
	fontColors[7] = "255 0 255 255";
	fillColorHL = "228 228 235 255";
	fillColorNA = "255 255 255 255";
	fontType = "Aileron";
	category = "ToolsFills";
	fontColors[5] = "Fuchsia";
	fontColorLinkHL = "Fuchsia";
	fontColors[4] = "255 0 255 255";
	fontColorLink = "255 0 255 255";
	fontColors[3] = "255 255 255 255";
	fontColorSEL = "255 255 255 255";
	bevelColorLL = "Fuchsia";
	fontColors[8] = "Magenta";
};
singleton GuiControlProfile(ToolsFillDarkB : ToolsFillDarkA) {
	fillColor = "35 34 34 255";
};

singleton GuiControlProfile(ToolsFillDarkC : ToolsFillDarkA) {
	fillColor = "43 43 43 255";
	opaque = "1";
	bevelColorHL = "Fuchsia";
	fontType = "Aileron";
	fontSize = "16";
	fontColors[4] = "Fuchsia";
	fontColorLink = "Fuchsia";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	category = "ToolsFills";
	fillColorNA = "255 255 255 0";
	fontColors[9] = "255 0 255 255";
	fontColors[7] = "255 0 255 255";
	fillColorHL = "228 228 235 255";
	fontColors[1] = "0 0 0 255";
	fontColorHL = "0 0 0 255";
	fontColors[5] = "Magenta";
	fontColorLinkHL = "Magenta";
	cursorColor = "0 0 0 255";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsFillDarkC : ToolsFillDarkA) {
	opaque = "1";
	fillColor = "19 40 55 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "0 0 0 255";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "0 0 0 255";
	fontColors[9] = "Magenta";
	fontColors[7] = "255 0 255 255";
	fillColorHL = "228 228 235 255";
	fillColorNA = "255 255 255 0";
};
//------------------------------------------------------------------------------

//==============================================================================
// Light Color Panels -> Light theme colors (A-B-C)
//==============================================================================
//==============================================================================
singleton GuiControlProfile(ToolsFillLightA : ToolsDefaultProfile) {
	opaque = "1";
	fillColor = "48 48 48 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "Black";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "Black";
	fontType = "Gotham Black";
	fillColorNA = "255 255 255 255";
	category = "ToolsFills";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsFillLightB : ToolsFillLightA) {
	fillColor = "239 246 248 255";
	opaque = "1";
	bevelColorHL = "Magenta";
	fontType = "Gotham Black";
	fontSize = "16";
	fontColors[4] = "255 0 255 255";
	fontColorLink = "255 0 255 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	category = "Tools";
	fillColorNA = "255 255 255 0";
	fontColors[3] = "White";
	fontColorSEL = "White";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsFillLightC : ToolsFillLightA) {
	opaque = "1";
	fillColor = "216 216 216 255";
	fontColors[0] = "88 1 192 255";
	fontColor = "88 1 192 255";
	fontColors[1] = "Black";
	fontColors[6] = "255 0 255 255";
	fontColorHL = "Black";
	fillColorNA = "White";
};
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------



singleton GuiControlProfile(ToolsFillDarkA_T50 : ToolsFillDarkA) {
	fillColor = "34 36 36 128";
};
