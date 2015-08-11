//==============================================================================
// TorqueLab -> GuiPaneCtrl Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// ToolsPanelProfile Profiles
//==============================================================================
singleton GuiControlProfile(ToolsPanelProfile : ToolsDefaultProfile)
{
   bevelColorLL = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiPanelProfile.png";
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
//------------------------------------------------------------------------------

//==============================================================================
// ToolsPanelDark Profiles
//==============================================================================
singleton GuiControlProfile(ToolsPanelDarkA : ToolsPanelProfile)
{
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
   bitmap = "tlab/gui/assets/container-assets/GuiPanelDarkA.png";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelDarkB : ToolsPanelDarkA)
{
   fontColors[4] = "255 0 255 255";
   fontColorLink = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiPanelDarkC.png";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsPanelDarkC : ToolsPanelDarkA)
{
   fontColors[4] = "255 0 255 255";
   fontColorLink = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiPanelDarkC.png";
};
//------------------------------------------------------------------------------
