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
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsBoxDarkA Style
singleton GuiControlProfile( ToolsBoxDarkB : ToolsBoxDarkA ) {
     opaque = false;
    border = -2;
    category = "ToolsContainers";
    bitmap = "tlab/gui/assets/container-assets/GuiBoxDarkB.png";
    fontColors[2] = "0 0 0 255";
    fontColorNA = "0 0 0 255";
   hasBitmapArray = "1";
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
    bitmap = "tlab/gui/assets/container/GuiBoxTitleDark";
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
    bitmap = "tlab/gui/assets/container/GuiBoxTitleBar.png";
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
