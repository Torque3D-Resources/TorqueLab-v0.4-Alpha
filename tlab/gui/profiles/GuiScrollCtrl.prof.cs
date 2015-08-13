//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

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
singleton GuiControlProfile( ToolsScrollProfile_Black_T50 : ToolsScrollProfile ) {
	bitmap = "tlab/gui/assets/container-assets/GuiScrollProfile.png";
   opaque = "1";
   fillColor = "3 3 3 151";
   border = "4";
   borderThickness = "0";
   borderColor = "148 155 148 112";
   borderColorHL = "50 50 50 255";
   borderColorNA = "51 51 51 255";
   category = "ToolsContainers";
};
//------------------------------------------------------------------------------
singleton GuiControlProfile(ToolsScrollProfile_Black_T50 : ToolsScrollProfile_Dark)
{
   fillColor = "3 3 3 151";
};
//------------------------------------------------------------------------------
