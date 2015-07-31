//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// ->Added Gui style support
// -->Delete Profiles when reloaded
// -->Image array store in Global
//==============================================================================

//==============================================================================
// GuiTextEditCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsTextEditProfile
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTextEditProfile : ToolsDefaultProfile ) {
    opaque = true;
    bitmap = "tlab/gui/assets/element-assets/GuiTextEditBorder.png";

    hasBitmapArray = true;
    border = -2; // fix to display textEdit img
    //borderWidth = "1";  // fix to display textEdit img
    //borderColor = "100 100 100";
    fillColor = "242 241 240 0";
    fillColorHL = "255 255 255 255";
    fontColor = "0 0 0";
    fontColorHL = "64 50 0 255";
    fontColorSEL = "98 100 137";
    fontColorNA = "6 19 76 255";
    textOffset = "4 2";
    autoSizeWidth = false;
    autoSizeHeight = true;
    justify = "left";
    tab = true;
    canKeyFocus = true;
    category = "Tools";
    fontType = "Gotham Book";
    fontSize = "15";
    fontColors[1] = "64 50 0 255";
    fontColors[2] = "6 19 76 255";
    fontColors[3] = "98 100 137 255";
};
//------------------------------------------------------------------------------
//ToolsTextEditDark Blue border variation
singleton GuiControlProfile( ToolsTextEditProfile_Num : ToolsTextEditProfile ) {  
    numbersOnly = true;
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsTextEditDark
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
//ToolsTextEditDark Blue border variation
singleton GuiControlProfile( ToolsTextEditDark : ToolsTextEditProfile ) {  
    bitmap = "tlab/gui/assets/element-assets/GuiTextEditDark_Blue.png";
   bevelColorLL = "Magenta";
   fontSize = "15";
   textOffset = "0 0";
   justify = "Top";
   fontColors[0] = "252 254 252 255";
   fontColors[1] = "34 102 132 150";
   fontColors[2] = "254 227 83 255";
   fontColors[3] = "254 185 5 255";
   fontColors[9] = "255 0 255 255";
   fontColor = "252 254 252 255";
   fontColorHL = "34 102 132 150";
   fontColorNA = "254 227 83 255";
   fontColorSEL = "254 185 5 255";
   fontColors[7] = "255 0 255 255";
};



singleton GuiControlProfile(ToolsTextEditDark_S1 : ToolsTextEditDark)
{
   bevelColorLL = "255 0 255 255";
   fontSize = "14";
   fontType = "Arial";
};

//------------------------------------------------------------------------------
//ToolsTextEditDark Blue border variation
singleton GuiControlProfile( ToolsTextEditDark_Num : ToolsTextEditDark ) {  
    numbersOnly = true;
};
//------------------------------------------------------------------------------

