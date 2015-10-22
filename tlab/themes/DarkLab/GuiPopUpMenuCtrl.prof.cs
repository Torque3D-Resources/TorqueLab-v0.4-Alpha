//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// DropDown Profiles (GuiPopUpMenuCtrl)
//==============================================================================

//==============================================================================
// DropdownBasic Styles
//------------------------------------------------------------------------------
//DropdownBasic Item
singleton GuiControlProfile(ToolsDropdownProfile_Item : ToolsDefaultProfile)
{
   modal = "1";
   fontSize = "22";
   fontColors[1] = "255 160 0 255";
   fontColorHL = "255 160 0 255";
   autoSizeWidth = "0";
   autoSizeHeight = "0";
   fillColorSEL = "0 255 6 255";
   opaque = "1";
   bevelColorHL = "255 0 255 255";
   fontColors[0] = "0 0 0 255";
   fontColor = "0 0 0 255";
   category = "GameContainer";
   justify = "Center";
   textOffset = "4 0";
};
//------------------------------------------------------------------------------
//DropdownBasic List
singleton GuiControlProfile (ToolsDropdownProfile_List : ToolsDefaultProfile)
{   
   hasBitmapArray     = false;
   fontSize = "18";
   fontColors[1] = "255 160 0 255";
   fontColorHL = "255 160 0 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   modal = "1";
   fillColor = "25 25 25 237";
   fillColorHL = "180 113 18 169";
   fontColors[2] = "3 206 254 255";
   fontColorNA = "3 206 254 255";
   profileForChildren = "ToolsDropdownProfile_Item";
   fillColorSEL = "2 2 2 241";
   fontColors[3] = "15 243 48 255";
   fontColorSEL = "15 243 48 255";
   opaque = "1";
   bevelColorHL = "255 0 255 255";
   fontColors[0] = "0 0 0 255";
   fontColor = "0 0 0 255";
   tab = "1";
   canKeyFocus = "1";
   justify = "Center";
   textOffset = "4 0";
  
};
//------------------------------------------------------------------------------
//DropdownBasic Menu
singleton GuiControlProfile (ToolsDropdownProfile : ToolsDefaultProfile)
{   
  hasBitmapArray     = "1";
  profileForChildren = "ToolsDropdownProfile_List";
 	bitmap = "tlab/themes/DarkLab/assets/element-assets/GuiDropDownProfile.png";
   fillColor = "242 241 241 255";
   fontSize = "16";
   fillColorHL = "228 228 235 255";
   fontColors[1] = "255 160 0 255";
   fontColors[2] = "248 254 254 255";
   fontColorHL = "255 160 0 255";
   fontColorNA = "248 254 254 255";
   autoSizeWidth = "0";
   autoSizeHeight = "0";
   fontType = "Lato bold";
   modal = "1";
   fontColors[3] = "5 64 201 255";
   fontColorSEL = "5 64 201 255";
   bevelColorLL = "Magenta";
   opaque = "1";
   bevelColorHL = "255 0 255 255";
   fontColors[0] = "0 0 0 255";
   fontColor = "0 0 0 255";
   textOffset = "4 0";
};
//------------------------------------------------------------------------------
//==============================================================================
// DropdownBasic Thin Version
singleton GuiControlProfile(ToolsDropdownProfile_S1 : ToolsDropdownProfile)
{
   bitmap = "tlab/themes/DarkLab/assets/element-assets/GuiDropDownProfile.png";
   fontSize = "14";
   justify = "Left";
   autoSizeHeight = "0";
   fontColors[8] = "Magenta";
   fontType = "Lato bold";
   fontColors[2] = "248 254 254 255";
   fontColorNA = "248 254 254 255";
   textOffset = "4 1";
};
//------------------------------------------------------------------------------
//==============================================================================
// DropdownBasic Thin Version
singleton GuiControlProfile(ToolsDropdownProfile_L1 : ToolsDropdownProfile)
{
   bitmap = "tlab/themes/DarkBlue/assets/element-assets/GuiDropdownProfile_L1.png";
   fontSize = "15";
   justify = "Center";
   bevelColorLL = "255 0 255 255";
};
//------------------------------------------------------------------------------
//==============================================================================
// DropdownBasic Styles
//==============================================================================
//------------------------------------------------------------------------------
//DropdownBasic Item
singleton GuiControlProfile(ToolsDropdownBase_Item : ToolsDefaultProfile)
{
   modal = "1";
   fontSize = "22";
   fontColors[1] = "255 160 0 255";
   fontColorHL = "255 160 0 255";
   autoSizeWidth = "1";
   autoSizeHeight = "1";
   fillColorSEL = "0 255 6 255";
   fontColors[5] = "Magenta";
   fontColors[7] = "255 0 255 255";
   fontColorLinkHL = "Magenta";
};
//------------------------------------------------------------------------------
//DropdownBasic List
singleton GuiControlProfile (ToolsDropdownBase : ToolsDefaultProfile)
{   
   hasBitmapArray     = "1";
   fontSize = "17";
   fontColors[1] = "255 160 0 255";
   fontColorHL = "255 160 0 255";
   autoSizeWidth = "0";
   autoSizeHeight = "0";
   modal = "1";
   fillColor = "242 241 241 255";
   fillColorHL = "228 228 235 255";
   fontColors[2] = "3 206 254 255";
   fontColorNA = "3 206 254 255";
   profileForChildren = "ToolsDropdownBase_List";
   fillColorSEL = "99 101 138 156";
   fontColors[3] = "254 3 62 255";
   fontColorSEL = "254 3 62 255";
   opaque = "1";
   fontType = "Davidan";
   fontColors[7] = "255 0 255 255";
   bitmap = "tlab/themes/DarkBlue/assets/element-assets/GuiDropdownBase.png";
  
};
//------------------------------------------------------------------------------
//DropdownBasic Menu
singleton GuiControlProfile (ToolsDropdownBase : ToolsDefaultProfile)
{   
  hasBitmapArray     = "1";
  profileForChildren = "ToolsDropdownBase_List";
 	bitmap = "tlab/themes/DarkBlue/assets/element-assets/GuiDropdownBase.png";
   fillColor = "242 241 241 255";
   fontSize = "17";
   fillColorHL = "228 228 235 255";
   fontColors[1] = "255 160 0 255";
   fontColors[2] = "3 206 254 255";
   fontColorHL = "255 160 0 255";
   fontColorNA = "3 206 254 255";
   autoSizeWidth = "0";
   autoSizeHeight = "0";
   fontType = "Davidan";
   modal = "1";
   fontColors[3] = "254 3 62 255";
   fontColorSEL = "254 3 62 255";
   bevelColorLL = "Magenta";
   fontColors[7] = "255 0 255 255";
};
//------------------------------------------------------------------------------
//==============================================================================
// DropdownBasic Thin Version
singleton GuiControlProfile(ToolsDropdownBase_Thin : ToolsDropdownBase)
{
   bitmap = "tlab/themes/DarkBlue/assets/element-assets/GuiDropdownBase_S1.png";
};
//------------------------------------------------------------------------------
