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
singleton GuiControlProfile(ToolsPanelColorA : ToolsDefaultProfile) {
    fillColor = "14 151 226 255";
    opaque = "1";
    bevelColorHL = "Fuchsia";
    fontType = "Aileron";
    fontSize = "16";
    fontColors[4] = "255 0 255 255";
    fontColorLink = "255 0 255 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    category = "ToolsPanels";
    fillColorNA = "255 255 255 255";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelColorB : ToolsPanelColorA) {
    opaque = "1";
    fillColor = "17 45 58 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "0 0 0 255";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "0 0 0 255";
    fillColorNA = "255 255 255 255";
    fontColors[9] = "Magenta";
   bevelColorHL = "255 0 255 255";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelColorC : ToolsPanelColorA) {
    opaque = "1";
    fillColor = "101 136 166 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "0 0 0 255";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "0 0 0 255";
   bevelColorHL = "Magenta";
};
//------------------------------------------------------------------------------

//==============================================================================
// Dark Color Panels -> Dark theme colors (A-B-C)
//==============================================================================

//==============================================================================
singleton GuiControlProfile(ToolsPanelDarkB : ToolsDefaultProfile) {
    opaque = "1";
    fillColor = "34 35 35 255";
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
   category = "ToolsPanels";
   fontColors[5] = "Fuchsia";
   fontColorLinkHL = "Fuchsia";
};
singleton GuiControlProfile(ToolsPanelDarkB : ToolsPanelDarkA) {
    fillColor = "58 64 68 255";
    opaque = "1";
    bevelColorHL = "Fuchsia";
    fontType = "Aileron";
    fontSize = "16";
    fontColors[4] = "255 0 255 255";
    fontColorLink = "255 0 255 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    category = "ToolsPanels";
    fillColorNA = "255 255 255 255";
    fontColors[9] = "255 0 255 255";
    fontColors[7] = "255 0 255 255";
    fillColorHL = "228 228 235 255";
    fontColors[1] = "0 0 0 255";
    fontColorHL = "0 0 0 255";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelDarkC : ToolsPanelDarkA) {
    opaque = "1";
    fillColor = "19 40 55 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "Black";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "Black";
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
singleton GuiControlProfile(ToolsPanelLightB : ToolsDefaultProfile) {
    opaque = "1";
    fillColor = "239 246 248 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "Black";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "Black";
    fontType = "Gotham Black";
   fillColorNA = "255 255 255 255";
   category = "ToolsPanels";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelLightC : ToolsPanelLightA) {
    fillColor = "216 216 216 255";
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
singleton GuiControlProfile(ToolsPanelLightC : ToolsPanelLightA) {
    opaque = "1";
    fillColor = "101 136 166 255";
    fontColors[0] = "88 1 192 255";
    fontColor = "88 1 192 255";
    fontColors[1] = "Black";
    fontColors[6] = "255 0 255 255";
    fontColorHL = "Black";
};
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------




singleton GuiControlProfile(ToolsPanelDarkA_T50 : ToolsPanelDarkA)
{
   fillColor = "1 1 1 131";
};


singleton GuiControlProfile(ToolsPanelDarkA : ToolsPanelLightA)
{
   fillColor = "21 26 32 255";
   fillColorHL = "228 228 235 255";
   fontColors[0] = "88 1 192 255";
   fontColor = "88 1 192 255";
};

singleton GuiControlProfile(ToolsPanelLightA : ToolsDefaultProfile)
{
   fillColor = "242 242 242 255";
   fillColorHL = "229 229 236 0";
   fillColorNA = "TransparentWhite";
   fontColors[0] = "88 1 192 255";
   fontColor = "88 1 192 255";
   fontColors[6] = "Magenta";
   fontType = "Gotham Black";
};

singleton GuiControlProfile(ToolsPanelLightA : ToolsDefaultProfile)
{
   fillColor = "242 242 242 255";
   fontType = "Gotham Black";
   fontColors[0] = "88 1 192 255";
   fontColor = "88 1 192 255";
};
