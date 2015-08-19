//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// GuiTabBookCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsTabBookProfile_S1 Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTabBookProfile : ToolsDefaultProfile ) {
    border = "0";
    opaque = false;
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiTabBookProfile.png";

    category = "Tools";
   fontType = "Gotham Book";
   fontSize = "17";
   fontColors[0] = "254 254 254 255";
   fontColor = "254 254 254 255";
   textOffset = "8 8";
   justify = "Center";
   fontColors[9] = "255 0 255 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   profileForChildren = "ToolsBoxDarkC_Top";
   bevelColorHL = "Magenta";
   fontColors[3] = "White";
   fontColors[7] = "255 0 255 255";
   fontColorSEL = "White";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTabBookProfile_S1 : ToolsTabBookProfile ) {
    border = "0";
    opaque = false;
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiTabBookProfile";

    category = "Tools";
   fontType = "Gotham Book";
   fontSize = "15";
   fontColors[0] = "254 254 254 255";
   fontColor = "254 254 254 255";
   textOffset = "8 0";
   justify = "Center";
   fontColors[9] = "255 0 255 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   bevelColorHL = "255 0 255 255";
   fontColors[3] = "255 255 255 255";
   fontColors[5] = "Fuchsia";
   fontColorSEL = "255 255 255 255";
   fontColorLinkHL = "Fuchsia";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsTabBookBasic Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTabBookBasic : ToolsDefaultProfile ) {
    border = "0";
    opaque = false;
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiTabBookBasic";

    category = "Tools";
   fontType = "Gotham Book";
   fontSize = "15";
   fontColors[0] = "254 254 254 255";
   fontColor = "254 254 254 255";
   textOffset = "8 0";
   justify = "Top";
   fontColors[9] = "255 0 255 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsTabBookColor Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTabBookColor : ToolsDefaultProfile ) {
    border = "0";
    opaque = false;
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiTabBookColor";

    category = "Tools";
   fontType = "Gotham Book";
   fontSize = "15";
   fontColors[0] = "254 254 254 255";
   fontColor = "254 254 254 255";
   textOffset = "8 0";
   justify = "Top";
   fontColors[9] = "255 0 255 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsTabBookCurve Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsTabBookCurve : ToolsTabBookProfile)
{
   bitmap = "tlab/gui/assets/container-assets/GuiTabBookCurve.png";
   fontSize = "15";
   justify = "Bottom";
   textOffset = "0 0";
   fillColorSEL = "99 101 138 0";
};
//------------------------------------------------------------------------------