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
singleton GuiControlProfile(ToolsFillAccentB : ToolsDefaultProfile) {
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
    fillColor = "21 26 32 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "0 0 0 255";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "0 0 0 255";
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
};
singleton GuiControlProfile(ToolsFillDarkB : ToolsFillDarkA)
{
   fillColor = "34 35 35 255";
};

singleton GuiControlProfile(ToolsFillDarkC : ToolsFillDarkA) {
    fillColor = "19 40 55 255";
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
    fillColor = "242 242 242 255";
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
};
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------


