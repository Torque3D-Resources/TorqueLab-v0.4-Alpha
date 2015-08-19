//==============================================================================
// TorqueLab -> Default Style setup
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Font Setup
//==============================================================================

$LabFonts["Default"] = "Gotham Book";
$LabFonts["Bold"] = "Gotham Bold";
$LabFonts["Thick"] = "Gotham Black";

$LabStyleFontColor[1] = "238 238 238 255";
$LabStyleFontColor[2] = "231 224 178 255";
$LabStyleFontColor[3] = "101 136 166 255";
$LabStyleFontDarkColor[1] = "14 151 226 255";
$LabStyleFontDarkColor[2] = "17 45 58 255";
$LabStyleFontDarkColor[3] = "101 136 166 255";

//==============================================================================
// Text Profiles
//==============================================================================
$LabStyleColor[1] = "14 151 226 255";
$LabStyleColor[2] = "17 45 58 255";
$LabStyleColor[3] = "101 136 166 255";

$LabStyleBackground[1] = "51 51 51 255";
$LabStyleBackground[2] = "57 74 86 255";
$LabStyleBackground[3] = "101 136 166 255";

$LabStyleDarkColor[1] = "51 51 51 255";
$LabStyleDarkColor[2] = "57 74 86 255";
$LabStyleDarkColor[3] = "101 136 166 255";

$LabStyleLightColor[1] = "127 143 154 255";
$LabStyleLightColor[2] = "101 136 166 255";
$LabStyleLightColor[3] = "101 136 166 255";

$LabStyleDarkHL = "32 32 34 255";
%list = "ToolsFillDarkA GuiInspectorFieldProfile GuiInspectorStackProfile GuiInspectorDynamicFieldProfile";
$StyleColorGroup["DarkColor1"] = "fillColor Background1\tfillColorHL DarkHL\tfontColorHL FontColor2\tfontColor FontColor1";
$StyleColorGroupProfiles["DarkColor1"] = %list;

%list = "GuiInspectorTextEditProfile";
$StyleColorGroup["DarkEdit"] = "fontColor FontColor2";
$StyleColorGroupProfiles["DarkEdit"] = %list;


$LabStyleGroups["Default"] = "DarkColor1 DarkEdit";
%list = "ToolsFillAccentA";
$LabProfilesStyleColor[1] = %list;
%list = "ToolsFillAccentB";
$LabProfilesStyleColor[2] = %list;
%list = "ToolsFillAccentC";
$LabProfilesStyleColor[3] = %list;

%list = "ToolsFillDarkA";
$LabProfilesStyleBackground[1] = %list;
%list = "ToolsFillDarkB GuiInspectorStackProfile";
$LabProfilesStyleBackground[2] = %list;
%list = "ToolsFillDarkC";
$LabProfilesStyleBackground[3] = %list;

%list = "ToolsFillDarkA";
$LabProfilesStyleDarkColor[1] = %list;
%list = "ToolsFillDarkB";
$LabProfilesStyleDarkColor[2] = %list;
%list = "ToolsFillDarkC";
$LabProfilesStyleDarkColor[3] = %list;

%list = "ToolsFillLightA";
$LabProfilesStyleLightColor[1] = %list;
%list = "ToolsFillLightB";
$LabProfilesStyleLightColor[2] = %list;
%list = "ToolsFillLightC";
$LabProfilesStyleLightColor[3] = %list;