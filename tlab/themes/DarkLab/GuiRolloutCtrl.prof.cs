//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================





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
    bitmap = "tlab/themes/DarkLab/assets/container-assets/GuiRolloutProfile.png";
    textoffset = "24 -1";
    fontType = "Aileron Bold";
    fontSize = "16";
    fontColors[0] = "253 253 253 255";
    fontColor = "253 253 253 255";
   fontColors[7] = "255 0 255 255";
   opaque = "0";
   fillColor = "242 241 240 255";
   fillColorHL = "228 228 235 255";
   fillColorNA = "White";
   fillColorSEL = "98 100 137 255";
   category = "ToolsContainers";

};

//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsRolloutProfile_S1 : ToolsRolloutProfile)
{
   bitmap = "tlab/themes/DarkLab/assets/container-assets/GuiRolloutProfile_S1.png";
   fontColors[4] = "Magenta";
   fontColorLink = "Magenta";
   fontType = "Lato";
   fontSize = "14";
   fontColors[0] = "254 220 152 255";
   fontColors[9] = "Magenta";
   fontColor = "254 220 152 255";
};
//------------------------------------------------------------------------------



