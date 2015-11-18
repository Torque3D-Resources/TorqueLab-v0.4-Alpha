//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Dark text profile for Light background
//==============================================================================
//==============================================================================
singleton GuiControlProfile(ToolsTextFX : ToolsDefaultProfile) {
	fontColor = "254 224 97 255";
	fontType = "Lato";
	fillColor = "238 236 240 255";
	bevelColorHL = "255 0 255 255";
	justify = "left";
	category = "ToolsText";
	fontSize = "15";
	fontColors[0] = "254 224 97 255";
};
singleton GuiControlProfile(ToolsTextFX_C : ToolsTextFX) {
	locked = true;
	justify = "Center";
	fontColors[0] = "0 30 255 255";
	fontColors[1] = "252 189 81 255";
	fontColors[2] = "254 227 83 255";
	fontColors[3] = "254 3 62 255";
	fontColors[4] = "238 255 0 255";
	fontColors[5] = "3 254 148 255";
	fontColors[6] = "3 21 254 255";
	fontColors[7] = "254 236 3 255";
	fontColors[8] = "254 3 43 255";
	fontColors[9] = "3 48 248 255";
	fontColor = "0 30 255 255";
	fontColorHL = "252 189 81 255";
	fontColorNA = "254 227 83 255";
	fontColorSEL = "254 3 62 255";
	fontColorLink = "238 255 0 255";
	fontColorLinkHL = "3 254 148 255";
	cursorColor = "Black";
};
singleton GuiControlProfile(ToolsTextFX_R : ToolsTextFX) {
	locked = true;
	justify = "Right";
};
//------------------------------------------------------------------------------
//==============================================================================
singleton GuiControlProfile(ToolsTextFX_S1 : ToolsTextFX) {
	fontSize = "18";
	cursorColor = "Black";
};
singleton GuiControlProfile(ToolsTextFX_S1_C : ToolsTextFX_S1) {
	locked = true;
	justify = "Center";
};
singleton GuiControlProfile(ToolsTextFX_S1_R : ToolsTextFX_S1) {
	locked = true;
	justify = "Right";
};
//------------------------------------------------------------------------------
//==============================================================================
singleton GuiControlProfile(ToolsTextFX_L1 : ToolsTextFX) {
	fontSize = "18";
	fontColors[0] = "222 175 51 255";
	fontColor = "222 175 51 255";
};
singleton GuiControlProfile(ToolsTextFX_L1_C : ToolsTextFX_L1) {
	locked = true;
	justify = "Center";
	fontColors[5] = "Magenta";
	fontColorLinkHL = "Magenta";
};
singleton GuiControlProfile(ToolsTextFX_L1_R : ToolsTextFX_L1) {
	locked = true;
	justify = "Right";
};
//------------------------------------------------------------------------------

//==============================================================================
// Standard Text Header Profiles
//==============================================================================
//==============================================================================
singleton GuiControlProfile(ToolsTextFX_H1 : ToolsTextFX) {
	fontColor = "0 255 231 208";
	fillColor = "238 236 240 255";
	bevelColorHL = "255 0 255 255";
	justify = "Left";
	category = "ToolsText";
	fontSize = "24";
	fontColors[0] = "0 255 231 208";
	fontColors[4] = "255 0 255 255";
	fontColorLink = "255 0 255 255";
};
singleton GuiControlProfile(ToolsTextFX_H1_C : ToolsTextFX_H1) {
	locked = true;
	justify = "Center";
};
singleton GuiControlProfile(ToolsTextFX_H1_R : ToolsTextFX_H1) {
	locked = true;
	justify = "Right";
	fontColors[3] = "White";
	fontColorSEL = "White";
};
//------------------------------------------------------------------------------