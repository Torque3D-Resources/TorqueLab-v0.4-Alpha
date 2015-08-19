//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// GuiButtonCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsButtonProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsButtonProfile : ToolsDefaultProfile ) {
    fontSize = "16";
    fontType = "Gotham Bold";
    fontColor = "254 254 254 255";
    justify = "center";
    category = "ToolsButtons";
    opaque = "1";
    border = "-2";
    fontColors[0] = "254 254 254 255";
    fontColors[2] = "200 200 200 255";
    fontColorNA = "200 200 200 255";
    bitmap = "tlab/gui/assets/button-assets/GuiButtonProfile.png";
    hasBitmapArray = "1";
    fixedExtent = "0";
   bevelColorLL = "255 0 255 255";
   textOffset = "0 0";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   borderThickness = "0";
   fontColors[9] = "255 0 255 255";
   fontColors[6] = "255 0 255 255";
   fontColors[4] = "255 0 255 255";
   fontColorLink = "255 0 255 255";
   fontColors[3] = "255 255 255 255";
   fontColorSEL = "255 255 255 255";
   fontColors[7] = "Magenta";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsButtonProfile_S1 : ToolsButtonProfile)
{
   fontSize = "14";
   fontColors[9] = "Magenta";
};

//==============================================================================
//ToolsButtonDark Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsButtonDark : ToolsButtonProfile ) {
    bitmap = "tlab/gui/assets/button-assets/GuiButtonDark.png";
     border = "-2";
   fontType = "Anson Regular";
   fontColors[9] = "57 117 73 255";
   fontColors[6] = "255 0 255 255";
   fontColors[8] = "Fuchsia";
   fontColors[4] = "255 0 255 255";
   fontColorLink = "255 0 255 255";
   fontColors[0] = "254 254 254 255";
   fontColors[1] = "0 0 0 255";
   fontColors[2] = "200 200 200 255";
   fontColors[3] = "255 255 255 255";
   fontColor = "254 254 254 255";
   fontColorHL = "0 0 0 255";
   fontColorNA = "200 200 200 255";
   fontColorSEL = "255 255 255 255";
   fontColors1 = "255 0 0 255";
   fontColors_90 = "37 113 57 255";
   textOffset = "0 0";
   fontColors[7] = "255 0 255 255";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsButtonDark_L : ToolsButtonDark)
{
   justify = "left";    
};

//==============================================================================
//ToolsButtonHighlight Style - Special style for highlighting stuff
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsButtonHighlight : ToolsButtonProfile ) {
    bitmap = "tlab/gui/assets/button-assets/GuiButtonHighlight.png";
};
//------------------------------------------------------------------------------

singleton GuiControlProfile( ToolsButtonArray : ToolsButtonProfile) {   
    fillColor = "225 243 252 255";
    fillColorHL = "225 243 252 0";
    fillColorNA = "225 243 252 0";
    fillColorSEL = "225 243 252 0";

    //tab = true;
    //canKeyFocus = true;

    fontType = "Gotham Book";
    fontSize = "14";

    fontColor = "250 250 247 255";
    fontColorSEL = "43 107 206";
    fontColorHL = "244 244 244";
    fontColorNA = "100 100 100";

    border = "-2";
    borderColor   = "153 222 253 255";
    borderColorHL = "156 156 156";
    borderColorNA = "153 222 253 0";

    //bevelColorHL = "255 255 255";
    //bevelColorLL = "0 0 0";
  
    fontColors[1] = "244 244 244 255";
    fontColors[2] = "100 100 100 255";
    fontColors[3] = "43 107 206 255";
    fontColors[9] = "255 0 255 255";
   fontColors[0] = "250 250 247 255";
   
    modal = 1;
   bitmap = "tlab/gui/assets/button-assets/GuiButtonDark.png";
};
//------------------------------------------------------------------------------

//==============================================================================
// GuiCheckboxCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsCheckBoxProfile_S1 Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsCheckBoxProfile : ToolsDefaultProfile ) {
     opaque = false;
    fillColor = "232 232 232";
    border = false;
    borderColor = "100 100 100";
    fontSize = "18";
    fontColor = "250 220 171 255";
    fontColorHL = "80 80 80";
    fontColorNA = "200 200 200";
    fixedExtent = 1;
    justify = "left";
    bitmap = "tlab/gui/assets/button-assets/GuiCheckboxProfile.png";
    hasBitmapArray = true;
    category = "Tools";
    fontType = "Gotham Book";
    fontColors[0] = "250 220 171 255";
    fontColors[1] = "80 80 80 255";
    fontColors[2] = "200 200 200 255";
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
};
//------------------------------------------------------------------------------
//ToolsCheckBoxProfile Small #1
singleton GuiControlProfile( ToolsCheckBoxProfile_S1 : ToolsCheckBoxProfile ) {
	bitmap = "tlab/gui/assets/button-assets/GuiCheckboxProfile_S1.png";
   fontSize = "14";
   
};
//------------------------------------------------------------------------------
//ToolsCheckBoxProfile Large #1
singleton GuiControlProfile( ToolsCheckBoxProfile_L1 : ToolsCheckBoxProfile ) {
	bitmap = "tlab/gui/assets/button-assets/GuiCheckboxProfile_L1.png";
   fontSize = "22";
   
};
//------------------------------------------------------------------------------
//ToolsCheckBoxProfile_S1 Variation for list
singleton GuiControlProfile( ToolsCheckBoxProfile_List : ToolsCheckBoxProfile ) {
	bitmap = "tlab/gui/assets/button-assets/GuiCheckboxProfile_List.png";
   fontSize = "16";
   
};
//------------------------------------------------------------------------------
//ToolsCheckBoxProfile_S1 Variation for cancel
singleton GuiControlProfile( ToolsCheckBoxProfile_Cancel : ToolsCheckBoxProfile ) {
	bitmap = "tlab/gui/assets/button-assets/GuiCheckboxProfile_Cancel.png";
   fontSize = "22";
   
};
//------------------------------------------------------------------------------

//==============================================================================
// GuiRadioCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsRadioProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsRadioProfile_L1 : ToolsDefaultProfile)
{
	fillColor = "254 253 253 255";
	fillColorHL = "221 221 221 255";
	fillColorNA = "200 200 200 255";
	fontSize = "14";
	textOffset = "16 10";
	bitmap = "tlab/gui/assets/button-assets/GuiRadioProfile_L1.png";
	hasBitmapArray = "1";
	fontColors[0] = "250 250 250 255";
	fontColor = "250 250 250 255";
   border = "0";
   fontColors[2] = "Black";
   fontColorNA = "Black";
   justify = "Center";
   fontType = "Gotham Bold";
   category = "Tools";
   fontColors[8] = "Magenta";
   fontColors[7] = "255 0 255 255";
};
//------------------------------------------------------------------------------
//ToolsRadioProfile Large #1 
singleton GuiControlProfile( ToolsRadioProfile_L1 : ToolsRadioProfile ) {
	bitmap = "tlab/gui/assets/button-assets/GuiRadioProfile_L1.png";
   fontSize = "14";
   fontColors[8] = "255 0 255 255";
   cursorColor = "Black";
   
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsRadioProfile_S1 : ToolsRadioProfile)
{
   bitmap = "tlab/gui/assets/button-assets/GuiRadioProfile_S1.png";
   fontSize = "12";
   justify = "Left";
   textOffset = "16 5";
   fontColors[8] = "255 0 255 255";
};
//==============================================================================
// Swatch Button Profile -> Used in stock code (Do not remove)
//==============================================================================
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsSwatchButtonProfile : ToolsDefaultProfile)
{
	fillColor = "254 253 253 255";
	fillColorHL = "221 221 221 255";
	fillColorNA = "200 200 200 255";
	fontSize = "24";
	textOffset = "16 10";
	bitmap = "art/gui/Lab/assets/button-assets/GuiButton";
	hasBitmapArray = "1";
	fontColors[0] = "253 253 253 255";
	fontColor = "253 253 253 255";
   border = "-1";
   fontColors[2] = "Black";
   fontColorNA = "Black";
    category = "Tools";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsSwatchButtonProfile : GuiSwatchButtonProfile)
{
	 category = "Tools";
   fontColors[2] = "Black";
   fontColorNA = "Black";
};
//------------------------------------------------------------------------------




