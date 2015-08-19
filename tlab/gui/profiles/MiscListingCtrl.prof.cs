//==============================================================================
// TorqueLab -> Default Containers Profiles
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



//==============================================================================
// GuiTreeViewCtrl
//==============================================================================

//==============================================================================
// ToolsTreeViewProfile Style
//------------------------------------------------------------------------------
singleton GuiControlProfile( ToolsTreeViewProfile : ToolsDefaultProfile ) {
    bitmap = "tlab/gui/assets/element-assets/GuiTreeViewProfile.png";
    autoSizeHeight = true;
    canKeyFocus = true;
   fillColor = "255 255 255";
    fillColorHL = "228 228 235";
    fillColorSEL = "98 100 137";
    fillColorNA = "255 255 255";
   fontColor = "254 238 192 255";
    fontColorHL = "0 0 0";
    fontColorSEL= "255 255 255";
    fontColorNA = "200 200 200";   
    borderColor = "128 000 000";
    borderColorHL = "255 228 235";
   
    opaque = false;
    border = false;
    category = "Tools";
    fontColors[2] = "200 200 200 255";
   fontType = "Lato bold";
   fontSize = "17";
   fontColors[0] = "254 238 192 255";
};
//------------------------------------------------------------------------------

singleton GuiControlProfile(ToolsTreeViewProfile_S1 : ToolsTreeViewProfile)
{
   fontSize = "14";
};
