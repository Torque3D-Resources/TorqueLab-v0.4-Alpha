//--- OBJECT WRITE BEGIN ---
$LabStyleCurrentGroup = new SimGroup(Default_ProfStyleGroup) {
   canSave = "1";
   canSaveDynamicFields = "1";

   new ScriptObject(SampleOnlyProfile_DefaultStyle) {
      internalName = "SampleOnlyProfile";
      canSave = "1";
      canSaveDynamicFields = "1";
         autoSizeHeight = "1";
         autoSizeWidth = "1";        
         border = "0";
         borderColor = "100 100 100 255";
         borderColorHL = "50 50 50 50";
         borderColorNA = "75 75 75 255";
         borderThickness = "3";
         canKeyFocus = "0";
         category = "Tools";
         cursorColor = "0 0 0 255";
         fillColor = "254 254 254 55";
         fillColorHL = "228 228 235 255";
         fillColorNA = "255 255 255 255";
         fillColorSEL = "98 100 137 255";
         fontCharset = "ANSI";
         fontColor = "0 0 0 255";
         fontColorHL = "150 150 150 255";
         fontColorLink = "255 0 255 255";
         fontColorLinkHL = "255 0 255 255";
         fontColorNA = "255 0 0 255";
         fontColorSEL = "255 255 255 255";
         fontSize = "12";
         fontType = "Lucida Console";
         hasBitmapArray = "1";
         justify = "Left";
         modal = "1";
         mouseOverSelected = "0";
         numbersOnly = "0";
         opaque = "1";
         returnTab = "0";
         tab = "0";
         textOffset = "0 0";
   };
   
};
//--- OBJECT WRITE END ---
