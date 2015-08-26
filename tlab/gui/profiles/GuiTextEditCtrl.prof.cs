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
//ToolsTextEdit
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTextEdit : ToolsDefaultProfile ) {  
    bitmap = "tlab/gui/assets/element-assets/GuiTextEditDark_Blue.png";
   bevelColorLL = "255 0 255 255";
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
   fontColors[6] = "Magenta";
   hasBitmapArray = "1";
   border = "-2";
   tab = "1";
   canKeyFocus = "1";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsTextEdit_S1 : ToolsTextEdit)
{
   bevelColorLL = "255 0 255 255";
   fontSize = "14";
   fontType = "Arial";
};
//------------------------------------------------------------------------------
//ToolsTextEdit Blue border variation
singleton GuiControlProfile( ToolsTextEdit_Num : ToolsTextEdit ) {  
    numbersOnly = true;
   fillColorNA = "TransparentWhite";
   fontColors[9] = "Fuchsia";
   cursorColor = "Black";
};
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
//ToolsTextEdit Blue border variation
singleton GuiControlProfile( ToolsTextEdit_Disabled : ToolsTextEdit ) {  
    numbersOnly = true;
   fillColorNA = "TransparentWhite";
   fontColors[9] = "Fuchsia";
   cursorColor = "Black";
};
//------------------------------------------------------------------------------
