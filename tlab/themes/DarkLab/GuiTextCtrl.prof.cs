//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================

//==============================================================================
// Standard Text Profiles
//==============================================================================
singleton GuiControlProfile(ToolsTextBase : ToolsDefaultProfile) {
    fontColor = "252 254 252 255";
    fontType = "Gotham Book";
    fillColor = "238 236 240 255";
    bevelColorHL = "255 0 255 255";
    justify = "Left";
    category = "ToolsText";
    fontColors[0] = "252 254 252 255";
    fontColors[1] = "252 189 81 255";
    fontColors[2] = "254 227 83 255";
    fontColorHL = "252 189 81 255";
    fontColorNA = "254 227 83 255";
    fontSize = "15";
   fontColors[4] = "238 255 0 255";
   fontColorLink = "238 255 0 255";
   opaque = "0";
   fontColors[3] = "37 194 120 255";
   fontColors[5] = "3 254 148 255";
   fontColors[6] = "3 21 254 255";
   fontColors[7] = "254 236 3 255";
   fontColors[8] = "254 3 43 255";
   fontColors[9] = "3 48 248 255";
   fontColorSEL = "37 194 120 255";
   fontColorLinkHL = "3 254 148 255";
   colorFont = "DefaultFontA";

};
singleton GuiControlProfile(ToolsTextBase_C : ToolsTextBase) {
    locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_R : ToolsTextBase) {
    locked = true;
    justify = "Right";
};
singleton GuiControlProfile (ToolsTextBase_Auto : ToolsTextBase) {    
    autoSizeWidth = true;
    autoSizeHeight = true;   
};

singleton GuiControlProfile( ToolsTextBase_ML : ToolsTextBase ) {   
    autoSizeWidth = true;
    autoSizeHeight = true;
    border = false;    
};
singleton GuiControlProfile( ToolsTextBase_List : ToolsTextBase ) {
    tab = "0";
    canKeyFocus = "0";
    category = "Tools";
   mouseOverSelected = "0";
   modal = "0";
};
//------------------------------------------------------------------------------
//==============================================================================
singleton GuiControlProfile(ToolsTextBase_S1 : ToolsTextBase) {
    fontSize = "14";
    fontType = "Gotham";
   fontColors[7] = "255 0 255 255";
};
singleton GuiControlProfile(ToolsTextBase_S1_C : ToolsTextBase_S1) {
    locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_S1_R : ToolsTextBase_S1) {
    locked = true;
    justify = "Right";
};
//------------------------------------------------------------------------------
//==============================================================================
singleton GuiControlProfile(ToolsTextBase_L1 : ToolsTextBase) {
    fontSize = "20";
};
singleton GuiControlProfile(ToolsTextBase_L1_C : ToolsTextBase_L1) {
    locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_L1_R : ToolsTextBase_L1) {
    locked = true;
    justify = "Right";
};
//------------------------------------------------------------------------------


//==============================================================================
// Standard Text Header Profiles
//==============================================================================
//==============================================================================
singleton GuiControlProfile(ToolsTextBase_H1 : ToolsTextBase) {
    fontType = "Lato bold";
    fillColor = "238 236 240 255";
    bevelColorHL = "255 0 255 255";
    justify = "Left";
    category = "ToolsText";
    fontSize = "18";
   fontColors[0] = "254 222 155 255";
   fontColors[1] = "252 189 81 255";
   fontColors[2] = "254 227 83 255";
   fontColors[3] = "254 3 62 255";
   fontColors[4] = "238 255 0 255";
   fontColors[5] = "3 254 148 255";
   fontColors[6] = "3 21 254 255";
   fontColors[7] = "254 236 3 255";
   fontColors[8] = "254 3 43 255";
   fontColors[9] = "3 48 248 255";
   fontColor = "254 222 155 255";
   fontColorHL = "252 189 81 255";
   fontColorNA = "254 227 83 255";
   fontColorSEL = "254 3 62 255";
   fontColorLink = "238 255 0 255";
   fontColorLinkHL = "3 254 148 255";
   cursorColor = "Black";
   bevelColorLL = "Magenta";

};
singleton GuiControlProfile(ToolsTextBase_H1_C : ToolsTextBase_H1) {
    locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_H1_R : ToolsTextBase_H1) {
    locked = true;
    justify = "Right";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsTextBase_H2 : ToolsTextBase_H1) {   
    fontSize = "16";
};
singleton GuiControlProfile(ToolsTextBase_H2_C : ToolsTextBase_H2) {   
	locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_H2_R : ToolsTextBase_H2) {
    locked = true;
    justify = "Right";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsTextBase_H3 : ToolsTextBase_H1) {   
    fontSize = "14";
};
singleton GuiControlProfile(ToolsTextBase_H3_C : ToolsTextBase_H3) {   
	locked = true;
    justify = "Center";
};
singleton GuiControlProfile(ToolsTextBase_H3_R : ToolsTextBase_H3) {
    locked = true;
    justify = "Right";
};
//==============================================================================
// TextBase Color A Variation  Profiles
singleton GuiControlProfile(ToolsTextColA : ToolsTextBase)
{
   fontColors[0] = "254 222 155 255";
   fontColors[7] = "255 0 255 255";
   fontColor = "254 222 155 255";
   fontType = "Gotham Bold";
};


singleton GuiControlProfile(ToolsTextColA_S1 : ToolsTextColA)
{
   fontType = "Gotham Book";
};
//------------------------------------------------------------------------------






