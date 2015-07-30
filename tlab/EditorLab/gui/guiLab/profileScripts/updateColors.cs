//==============================================================================
// Lab GuiManager -> Profile Updater functions
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$ResizableProfiles = "GuiButtonText";

//==============================================================================
// Profile Colors Update from selected Color Set
//==============================================================================

//==============================================================================
// Update all the Color Sets assigned profiles
function GLab::updateProfilesColors(%this,%refreshList ) {
   if ($ProfileUpdateColorList $="" || %refreshList)
      updateAllProfileFiles();
      
   foreach$(%cType in $ProfileUpdateColorList){
      
     
      //------------------------------------------------------------------------
      //Check for Fill set which use single global object
      %isFill = strstr(%cType,"Fill");
      if (%isFill !$= "-1"){
         GLab.updateProfilesFillColor(%cType);
         continue;
      }
      
       	
      foreach$(%profile in $ProfileListColor[%cType]){
      	%this.updateSingleProfileColors(%profile,%cType);
         
      }      
   }
}
//------------------------------------------------------------------------------
//==============================================================================
// Update all the Color Sets assigned profiles
function GLab::updateSingleProfileColors(%this,%profile,%cType ) {
  info("Updating single profile colors",%profile.getName(),"Type",%cType);
  	%srcGroup = GuiColor_Group.findObjectByInternalName(%cType,true);
         %colorSet = %profile.getFieldValue(%cType);  
         
         %srcColor = %srcGroup.findObjectByInternalName(%colorSet,true);
         
         %count = %srcColor.getDynamicFieldCount();
         for(%i = 0; %i < %count;%i++){
            %fieldFull = %srcColor.getDynamicField(%i);
			   %field = getField(%fieldFull,0);
			   %value = getField(%fieldFull,1);
			   %current = %profile.getFieldValue(%field);
			   %isDirty = false;
			   if(%current !$= %value){
			   	%isDirty = true;
			   	warnLog("-> Should be set to dirty",%field,%value,%current);
			   }
			   %strlen = strLen(%field);
			   %lastChar = getSubStr(%field,%strlen-1);
			  
			   if (strIsNumeric(%lastChar))
			   {
			      %beforeLastChar = getSubStr(%field,0,%strlen-1); 
			      %field = %beforeLastChar@"["@%lastChar@"]";
			   }
			   GLab.updateProfileField(%profile,%field,%value,!%isDirty);			    
			  
         }      

}
//------------------------------------------------------------------------------
//==============================================================================
// Update ColorFill color set (use an object to hold all values)
function GLab::updateProfilesFillColor(%this,%cType ) {
   %srcColor = GuiColor_Group.findObjectByInternalName(%cType,true);       
   foreach$(%profile in $ProfileListColor[%cType]){
      %colorSet = %profile.getFieldValue(%cType);           
      %value = %srcColor.getFieldValue(%colorSet);     
      GLab.updateProfileField(%profile,"fillColor",%value);
   }      
}
//------------------------------------------------------------------------------
//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::updateFontSize(%this,%ratio,%all ) {
   if (!$GLab_FontResizeEnabled){
      //warnLog("Font Resizing mode is disabled, exiting updateFontSize function.");
      return;
   } 
   
   if (%ratio $=""){
     %ratio = %this.getGuiRatio();
   }
      
    %list = $ProfStoreFieldProfiles["fontSize"];
   if (!%all)
      %list = $ResizableProfiles;
   
  foreach$(%profileName in %list ){  
      
      devLog("RESIZING PROFILE:",%profileName);
      %value = $ProfStoreFieldDefault[%profileName,"fontSize"];
      %newValue = %value * %ratio;
      %newValue = mCeil(%newValue);
      %this.updateProfileField(%profileName,"fontSize",%newValue,true);
      
       foreach$(%child in $ProfChilds[%profileName]){
            %childValue = $ProfStoreFieldDefault[%profileName,"fontSize"];
            if (%childValue $= "")
               continue;
            %childValue = $ProfStoreFieldDefault[%child,"fontSize"];
            %newChildValue = %childValue * %ratio;
            %newChildValue = mCeil(%newChildValue);
            devLog(%child,"Updating a child with own value",%childValue,"Changed to:",%newChildValue);
            %this.updateProfileField(%child,"fontSize",%newChildValue,true);
       }
  }
  
  
}

//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::restoreFontSize(%this,%ratio,%all ) {
    %list = $ProfStoreFieldProfiles["fontSize"];
   if (!%all)
      %list = $ResizableProfiles;
   
  foreach$(%profileName in %list ){  
      
      devLog("RESIZING PROFILE:",%profileName);
      %value = $ProfStoreFieldDefault[%profileName,"fontSize"];
      //%newValue = %value * %ratio;
      //%newValue = mCeil(%newValue);
      %this.updateProfileField(%profileName,"fontSize",%value,true);
  }
}
//------------------------------------------------------------------------------
//devLog("Default:",$ProfStoreFieldDefault[GuiButtonText,"fontSize"],"MenuDefault:",$ProfStoreFieldDefault[GuiButtonText_Menu,"fontSize"]);