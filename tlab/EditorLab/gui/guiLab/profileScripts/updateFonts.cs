//==============================================================================
// Lab GuiManager -> Init Lab GUI Manager System 
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Profile Fonts Update from font source
//==============================================================================

//==============================================================================
// Update all the Color Sets assigned profiles
function GLab::updateProfilesFonts(%this ) {   
      
   foreach$(%profile in $ProfileList["fontSource"]){
      %fontSrc = %profile.getFieldValue("fontSource");      
      %fontType = $GuiFont[%fontSrc];
      GLab.updateProfileField(%profile,"fontType",%fontType);   
      
   }
}
//------------------------------------------------------------------------------