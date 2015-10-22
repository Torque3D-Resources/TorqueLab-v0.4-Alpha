//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// GuiScrollCtrl Profiles
//==============================================================================
//==============================================================================
//ToolsScrollProfile Style - With Default Dark Background
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsScrollProfile : ToolsDefaultProfile ) {
	bitmap = "tlab/themes/DarkBlue/assets/container-assets/GuiScrollProfile.png";
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
singleton GuiControlProfile( ToolsScrollProfile_T50 : ToolsScrollProfile ) {  
   fillColor = "34 34 34 128"; 
};
//------------------------------------------------------------------------------
