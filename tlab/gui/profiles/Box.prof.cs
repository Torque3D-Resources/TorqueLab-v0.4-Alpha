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
    bitmap = "tlab/gui/assets/container-assets/GuiBoxDarkA.png";
    fontColors[2] = "0 0 0 255";
    fontColorNA = "0 0 0 255";
   hasBitmapArray = "1";
   fillColor = "19 40 55 255";
   fontType = "Anson Regular";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkB : ToolsBoxDarkA ) {
     opaque = false;
    border = -2;
    category = "ToolsContainers";
    bitmap = "tlab/gui/assets/container-assets/GuiBoxDarkB.png";
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
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkC : ToolsBoxDarkA ) {
     opaque = "1";
    border = -2;
    category = "ToolsContainers";
    bitmap = "tlab/gui/assets/container-assets/GuiBoxDarkC.png";
    fontColors[2] = "0 0 0 255";
    fontColorNA = "0 0 0 255";
   hasBitmapArray = "1";
   fillColor = "19 40 55 255";
   fontType = "Anson Regular";
};
//------------------------------------------------------------------------------

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
    bitmap = "tlab/gui/assets/container-assets/GuiBoxTitleDark";
    fontColors[2] = "0 0 0 255";
    fontColorNA = "0 0 0 255";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsBoxTitleBar Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsBoxTitleBar : ToolsDefaultProfile ) {
    opaque = false;
    border = -2;
    category = "ToolsContainers";
    bitmap = "tlab/gui/assets/container-assets/GuiBoxTitleBar.png";
    fontColors[2] = "0 0 0 255";
    fontColorNA = "0 0 0 255";
   fontType = "Gotham Black";
   fontSize = "17";
   fontColors[0] = "222 222 222 255";
   fontColor = "222 222 222 255";
};
//------------------------------------------------------------------------------

singleton GuiControlProfile(ToolsBoxTitleBar_Thin : ToolsBoxTitleBar)
{
   bitmap = "tlab/gui/assets/container-assets/GuiBoxTitleBar_Thin.png";
};

singleton GuiControlProfile(ToolsPaneProfile : ToolsBoxDarkA)
{
   bevelColorLL = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiPaneProfile.png";
   opaque = "1";
   fillColor = "2 2 2 255";
   fillColorHL = "2 2 2 255";
   fillColorNA = "2 2 2 255";
   border = "2";
   borderThickness = "5";
   fontType = "Gotham Bold";
   fontSize = "20";
   fontColors[0] = "254 254 254 255";
   fontColor = "254 254 254 255";
   textOffset = "2 6";
};

singleton GuiControlProfile(ToolsBoxDarkC_Top : ToolsBoxDarkC)
{
   bitmap = "tlab/gui/assets/container-assets/GuiBoxDarkA_Top.png";
};

singleton GuiControlProfile(ToolsPaneDarkA : ToolsPaneProfile)
{
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
   bitmap = "tlab/gui/assets/container-assets/GuiPaneDarkA.png";
};

singleton GuiControlProfile(ToolsPaneDarkC : ToolsPaneDarkA)
{
   fontColors[4] = "255 0 255 255";
   fontColorLink = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiPaneDarkC.png";
};
