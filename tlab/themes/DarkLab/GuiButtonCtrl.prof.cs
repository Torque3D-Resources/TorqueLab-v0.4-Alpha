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
    fontType = "Lato  Bold";
    fontColor = "254 254 254 255";
    justify = "center";
    category = "ToolsButtons";
    opaque = "1";
    border = "-2";
    fontColors[0] = "254 254 254 255";
    fontColors[2] = "200 200 200 255";
    fontColorNA = "200 200 200 255";
    bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiButtonProfile.png";
    hasBitmapArray = "1";
    fixedExtent = "0";
   bevelColorLL = "255 0 255 255";
   textOffset = "0 0";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   borderThickness = "0";
   fontColors[9] = "255 0 255 255";
   fontColors[6] = "Fuchsia";
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
   justify = "Top";
   textOffset = "0 -2";
   fontType = "Lato";
   fillColor = "254 254 254 255";
   fillColorHL = "229 229 236 255";
   opaque = "0";
};

//==============================================================================
//ToolsButtonDark Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsButtonDark : ToolsButtonProfile ) {
    bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiButtonAlt.png";
     border = "-2";
   fontType = "Lato bold";
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
   fontSize = "16";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsButtonDark_L : ToolsButtonDark)
{
   justify = "left";    
};

//==============================================================================
//ToolsButtonHighlight Style - Special style for highlighting stuff
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsButtonArray : ToolsButtonProfile ) {
    bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiButtonAlt.png";
   fillColor = "225 243 252 255";
   fillColorHL = "225 243 252 0";
   fillColorNA = "225 243 252 0";
   fillColorSEL = "225 243 252 0";
   borderColor = "153 222 253 255";
   borderColorHL = "156 156 156 255";
   borderColorNA = "153 222 253 0";
   fontType = "Gotham Book";
   fontSize = "14";
   fontColors[0] = "250 250 247 255";
   fontColors[1] = "244 244 244 255";
   fontColors[2] = "100 100 100 255";
   fontColors[3] = "43 107 206 255";
   fontColor = "250 250 247 255";
   fontColorHL = "244 244 244 255";
   fontColorNA = "100 100 100 255";
   fontColorSEL = "43 107 206 255";
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
   bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiButtonAlt.png";
};
//------------------------------------------------------------------------------

//==============================================================================
// GuiCheckboxCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsCheckBoxProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsCheckBoxProfile : ToolsDefaultProfile ) {
     opaque = false;
    fillColor = "232 232 232";
    border = false;
    borderColor = "100 100 100";
    fontSize = "14";
    fontColor = "250 220 171 255";
    fontColorHL = "80 80 80";
    fontColorNA = "200 200 200";
    fixedExtent = 1;
    justify = "left";
    bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiCheckboxProfile.png";
    hasBitmapArray = true;
    category = "Tools";
    fontType = "Lato";
    fontColors[0] = "250 220 171 255";
    fontColors[1] = "80 80 80 255";
    fontColors[2] = "200 200 200 255";
   fontColors[4] = "Fuchsia";
   fontColorLink = "Fuchsia";
   textOffset = "0 -3";
};


//------------------------------------------------------------------------------
//ToolsCheckBoxProfile Variation for list
singleton GuiControlProfile( ToolsCheckBoxProfile_List : ToolsCheckBoxProfile ) {
	bitmap = "tlab/themes/DarkBlue/assets/button-assets/GuiCheckBoxProfile_List.png";
   fontSize = "14";
   fontColors[8] = "Fuchsia";
   
};
//------------------------------------------------------------------------------
//ToolsCheckBoxProfile Variation for cancel
singleton GuiControlProfile( ToolsCheckBoxProfile_Cancel : ToolsCheckBoxProfile ) {
	bitmap = "tlab/themes/DarkBlue/assets/button-assets/GuiCheckBoxProfile_Cancel.png";
   fontSize = "22";
   
};
//------------------------------------------------------------------------------

//==============================================================================
// GuiRadioCtrl Profiles
//==============================================================================

//==============================================================================
//ToolsRadioProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsRadioProfile : ToolsDefaultProfile)
{
	fillColor = "254 253 253 255";
	fillColorHL = "221 221 221 255";
	fillColorNA = "200 200 200 255";
	fontSize = "14";
	textOffset = "16 10";
	bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiRadioProfile.png";
	hasBitmapArray = "1";
	fontColors[0] = "250 250 250 255";
	fontColor = "250 250 250 255";
   border = "0";
   fontColors[2] = "Black";
   fontColorNA = "Black";
   justify = "Left";
   fontType = "Lato";
   category = "Tools";
   fontColors[8] = "Magenta";
   fontColors[7] = "255 0 255 255";
   autoSizeHeight = "1";
};

//==============================================================================
// Swatch Button Profile -> Used in stock code (Do not remove)
//==============================================================================
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsSwatchButtonProfile : ToolsDefaultProfile)
{
	fillColor = "254 254 254 255";
	fillColorHL = "221 221 221 255";
	fillColorNA = "200 200 200 255";
	fontSize = "24";
	textOffset = "16 10";
	bitmap = "tlab/themes/DarkLab/assets/button-assets/GuiButtonAlt.png";
	hasBitmapArray = "0";
	fontColors[0] = "253 253 253 255";
	fontColor = "253 253 253 255";
   border = "0";
   fontColors[2] = "Black";
   fontColorNA = "Black";
    category = "Tools";
   modal = "0";
   opaque = "1";
   fillColorSEL = "99 101 138 156";
   borderColor = "100 100 100 255";
   cursorColor = "0 0 0 79";
};





