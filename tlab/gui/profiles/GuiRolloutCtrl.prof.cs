//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================





//==============================================================================
// GuiRolloutCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsRolloutProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsRolloutProfile : ToolsDefaultProfile ) {
    border = "0";
    borderColor = "200 200 200";
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiRolloutProfile.png";
    textoffset = "24 1";
    fontType = "Aileron Bold";
    fontSize = "16";
    fontColors[0] = "253 253 253 255";
    fontColor = "253 253 253 255";
   fontColors[7] = "255 0 255 255";
   opaque = "0";
   fillColor = "242 241 240 255";
   fillColorHL = "228 228 235 255";
   fillColorNA = "White";
   fillColorSEL = "98 100 137 255";
   category = "ToolsContainers";

};
//------------------------------------------------------------------------------
//ToolsRolloutProfile Thin Version
singleton GuiControlProfile( ToolsRolloutProfile_S1 : ToolsRolloutProfile ) {
    bitmap = "tlab/gui/assets/container-assets/GuiRolloutProfile_S1.png";
    fontSize = "16";
   textOffset = "22 0";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsRolloutProfile_Alt : ToolsRolloutProfile)
{
   bitmap = "tlab/gui/assets/container-assets/GuiRolloutProfile_Alt.png";
   fontColors[4] = "Magenta";
   fontColorLink = "Magenta";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsRolloutTitle Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsRolloutTitle : ToolsRolloutProfile ) {
    border = "1";
    borderColor = "53 53 53 255";
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiRolloutTitle.png";
    textoffset = "22 0";
    fontType = "Aileron Bold";
    fontSize = "16";
    fontColors[0] = "253 253 253 255";
    fontColor = "253 253 253 255";
   fontColors[7] = "255 0 255 255";
   opaque = "0";
   fillColor = "242 241 240 255";
   fillColorHL = "228 228 235 255";
   fillColorNA = "255 255 255 255";
   fillColorSEL = "98 100 137 255";

};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsRolloutTitle Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsRolloutTitle : ToolsDefaultProfile ) {
    border = 1;
    borderColor = "200 200 200";
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiRolloutTitle.png";
    textoffset = "17 0";
    fontType = "Aileron Bold";
    fontSize = "16";
    fontColors[0] = "253 253 253 255";
    fontColor = "253 253 253 255";
   fontColors[7] = "255 0 255 255";

};
//------------------------------------------------------------------------------


