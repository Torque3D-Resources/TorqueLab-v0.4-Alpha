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
    opaque = "1";
    border = "0";
    fillColor = "0 0 0 255";
    fillColorHL = "221 221 221";
    fillColorNA = "200 200 200";
    fontColor = "236 236 236 255";
    fontColorHL = "0 0 0";
    bevelColorHL = "255 255 255";
    bevelColorLL = "0 0 0";
    text = "untitled";
    bitmap = "tlab/gui/assets/container-assets/GuiWindowProfile_Dark.png";
    textOffset = "8 2";
    hasBitmapArray = true;
    justify = "left";
    category = "ToolsContainers";
    fontType = "Gotham Bold";
    fontSize = "19";
    fontColors[0] = "236 236 236 255";
    cursorColor = "0 0 0 255";
};
//------------------------------------------------------------------------------
//ToolsWindowProfile Dark center variation
singleton GuiControlProfile( ToolsWindowProfile_Dark : ToolsWindowProfile ) {
	bitmap = "tlab/gui/assets/container-assets/GuiWindowProfile_Dark.png";
   fillColor = "20 25 31 255";
   fontColors[1] = "0 0 0 255";
   fontColorHL = "0 0 0 255";
   fontSize = "17";
   textOffset = "6 3";
   category = "Tools";
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsWindowPanel Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowPanel : ToolsWindowProfile)
{
   bitmap = "tlab/gui/assets/container-assets/GuiWindowPanel.png";
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
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowPanelA : ToolsWindowPanel)
{
   fontColors[8] = "255 0 255 255";
   bitmap = "tlab/gui/assets/container-assets/GuiWindowPanelA.png";
   opaque = "1";
   fillColor = "19 40 55 255";
};
//------------------------------------------------------------------------------
//==============================================================================
//ToolsWindowBox Style
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowBox : ToolsWindowPanel)
{
   bitmap = "tlab/gui/assets/container-assets/GuiWindowBox.png";
   fontColors[0] = "233 233 233 255";
   fontColor = "233 233 233 255";
   textOffset = "8 3";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsWindowBox_Alt : ToolsWindowBox)
{
   bitmap = "tlab/gui/assets/container-assets/GuiWindowBox_Alt.png";
};
//------------------------------------------------------------------------------
//==============================================================================
// GuiScrollCtrl Profiles
//==============================================================================
//==============================================================================
//ToolsScrollProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsScrollProfile : ToolsDefaultProfile ) {
     opaque = "0";
    fillcolor = "254 254 254 55";
    fontColor = "0 0 0";
    fontColorHL = "150 150 150";
    border = "0";
    bitmap = "tlab/gui/assets/container-assets/GuiScrollProfile.png";
    hasBitmapArray = true;
    category = "Tools";
    fontColors[1] = "150 150 150 255";
    borderThickness = "3";
    bevelColorHL = "57 152 152 255";
    bevelColorLL = "150 196 201 255";  
};
//------------------------------------------------------------------------------
//ToolsScrollProfile Dark Fill 100%
singleton GuiControlProfile( ToolsScrollProfile_Dark : ToolsScrollProfile ) {
	bitmap = "tlab/gui/assets/container-assets/GuiScrollProfile.png";
   opaque = "1";
   fillColor = "34 34 34 255";
   category = "ToolsContainers";
   border = "4";
   borderThickness = "0";
   borderColor = "148 155 148 112";
   borderColorHL = "50 50 50 255";
   borderColorNA = "51 51 51 255";
   bevelColorHL = "3 3 3 255";
   bevelColorLL = "12 12 12 255";
};
//------------------------------------------------------------------------------
//ToolsScrollProfile Dark Fill 50%
singleton GuiControlProfile( ToolsScrollProfile_Dark_T50 : ToolsScrollProfile ) {
	bitmap = "tlab/gui/assets/container-assets/GuiScrollProfile.png";
   opaque = "1";
   fillColor = "21 21 21 128";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsScrollProfile_Black_T50 : ToolsScrollProfile_Dark)
{
   fillColor = "3 3 3 151";
};
//------------------------------------------------------------------------------


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
   fillColorNA = "255 255 255 255";
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
};
//------------------------------------------------------------------------------

//==============================================================================
//ToolsRolloutTitle Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsRolloutTitle : ToolsRolloutProfile ) {
    border = "0";
    borderColor = "200 200 200";
    hasBitmapArray = true;
    bitmap = "tlab/gui/assets/container-assets/GuiRolloutTitle.png";
    textoffset = "17 0";
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
//==============================================================================
// GuiFrameSetCtrl Profiles
//==============================================================================
//==============================================================================
singleton GuiControlProfile (ToolsGuiFrameSetProfile) {
    fillcolor = "255 255 255";
    borderColor = "246 245 244";
    border = 1;
    opaque = true;
    border = true;
    category = "Tools";
};
//------------------------------------------------------------------------------


