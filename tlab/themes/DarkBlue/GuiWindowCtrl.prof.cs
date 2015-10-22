//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// GuiWindowCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsWindowProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsWindowProfile : ToolsDefaultProfile ) {
    opaque = "0";
    border = "0";
     fillColor = "2 2 2 255";
    fillColorHL = "221 221 221";
    fillColorNA = "200 200 200";
    fontColor = "236 236 236 255";
    fontColorHL = "0 0 0 255";
    bevelColorHL = "255 255 255";
    bevelColorLL = "Black";
    text = "untitled";
    bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiWindowProfile_Dark.png";
    textOffset = "8 2";
    hasBitmapArray = true;
    justify = "left";
    category = "ToolsContainers";
    fontType = "Gotham Bold";
    fontSize = "19";
    fontColors[0] = "236 236 236 255";
    cursorColor = "0 0 0 255";
   fontColors[7] = "255 0 255 255";
   fontColors[5] = "Fuchsia";
   fontColorLinkHL = "Fuchsia";
};
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
singleton GuiControlProfile( LabWindowProfile : ToolsDefaultProfile ) {
    opaque = "0";
    border = "0";
     fillColor = "2 2 2 255";
    fillColorHL = "221 221 221";
    fillColorNA = "200 200 200";
    fontColor = "236 236 236 255";
    fontColorHL = "0 0 0 255";
    bevelColorHL = "255 255 255";
    bevelColorLL = "Black";
    text = "untitled";
    bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiWindowProfile_Dark.png";
    textOffset = "8 2";
    hasBitmapArray = true;
    justify = "left";
    category = "ToolsContainers";
    fontType = "Gotham Bold";
    fontSize = "19";
    fontColors[0] = "236 236 236 255";
    cursorColor = "0 0 0 255";
   fontColors[7] = "255 0 255 255";
   fontColors[5] = "Fuchsia";
   fontColorLinkHL = "Fuchsia";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsWindowPanel Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowPanel : ToolsWindowProfile)
{
   bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiWindowPanel.png";
   fillColor = "21 26 32 255";
   fontColors[0] = "245 245 245 255";
   fontColor = "245 245 245 255";
   textOffset = "8 1";
   fontColors[3] = "255 255 255 255";
   fontColorSEL = "255 255 255 255";
   fontColors[9] = "255 0 255 255";
   fillColorHL = "228 228 235 255";
   fillColorNA = "255 255 255 255";
   fillColorSEL = "98 100 137 255";
   borderColor = "200 200 200 255";
   fontType = "Aileron Bold";
   fontSize = "16";
   fontColors[7] = "Fuchsia";
   bevelColorLL = "Black";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsWindowBox Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowBox : ToolsWindowPanel)
{
   bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiWindowBox.png";
   fontColors[0] = "233 233 233 255";
   fontColor = "233 233 233 255";
   textOffset = "8 3";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowBox_Alt : ToolsWindowBox)
{
   bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiWindowBox_Alt.png";
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
   opaque = "1";
   fillColor = "254 254 254 255";
   fontColors[1] = "0 0 0 255";
   fontColorHL = "0 0 0 255";
};
//------------------------------------------------------------------------------
