//==============================================================================
// GameLab -> Interface Development Gui
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::profileIsFieldOwner(%this,%profileName,%field ) {
   %field = strreplace(%field,"[",""); 
   %field = strreplace(%field,"]","");        
   %ownList = $ProfOwnedFields[%profileName];
   %ownField = strFind(%ownList,%field);  
      return %ownField;
}
//------------------------------------------------------------------------------

//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::findParentFieldSource(%this,%profileName,%field ) {  
   %parentName = $ProfParent[%profileName];  
  %field = strreplace(%field,"[",""); 
   %field = strreplace(%field,"]","");        
   while(%parentName !$= "" ){
      %profileName = %parentName;
      if (%profileName $= ""){
         return false;
      }
      
      %ownList = $ProfOwnedFields[%profileName];
      %ownField = strFind(%ownList,%field);  
      if (%ownField)
         return %profileName; 
      
       %parentName = $ProfParent[%profileName];
       if (%parentName $= %profileName){
          return false;
       }
   }
   return false;
   
  
}
//------------------------------------------------------------------------------
//==============================================================================
// Profile fields update functions
//==============================================================================

//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::updateProfileField(%this,%profile,%field,%value,%notDirty ) {  
   %isDirty = !%notDirty;
   
  if (%field $= "colorFont" ||%field $= "colorFont"){
	 	warnLog("We are not saving color sets in profile anymore:",%field);
	 	return;
	 }
	 
   if (%isDirty && %field $= "fontSize"){
       if (strstr($ProfStoreFieldProfiles["fontSize"],%profile.getName()) !$= "-1")
      {
         $ProfStoreFieldDefault[%profile.getName(),"fontSize"] = %value;
      }
   }
   %profileName = %profile.getName(); 
   if ($GLab::SaveParentProfileField){
      %ownList = $ProfOwnedFields[%profile.getName()];
      %ownField = strFind(%ownList,%field);
      if (!%ownField){
         //This is a parent field         
         %parent = %this.findParentFieldSource(%profileName,%field);
         if (isObject(%parent)){             
            %profile = %parent;            
         }
         else{
         	warnLog("Couln't find a valid parent to save field to for profile:",%profileName);
         	return;
         }         
		}
   }
   
      //Check if parent field
  // %current =  %profile.getFieldValue(%field); 
   //if (%current $= %value && %notDirty){
   	//devLog(%profile.getName(),"Skipping, same value",%current,"New",%value);
       //GLab.updateProfileChildsField( %profile,%field,%value);
      //return;
   //}
	%this.setProfileFieldValue(%profile,%field,%value);
  // %profile.setFieldValue(%field,%value);  
   //Update the childs with new value so changes are applied to them also
   //GLab.updateProfileChildsField( %profile,%field,%value);
   //GLab.setProfileDirty( %profile, true );
   
  // if (%field $= "colorFont" ||%field $= "colorFont")
   //	GLab.updateSingleProfileColors(%profile,%field);
   
   if ($GLab_UpdateColorsOnSetChanged && strFind($ProfileUpdateColorList,%field))
   {
      devLog("UpdateColors on set changed:",%field,"Value",%value);
   }
}
//------------------------------------------------------------------------------

//==============================================================================
// Update a single profile field and set profile dirty if changed
function GLab::setProfileFieldValue(%this,%profile,%field,%value,%skipChildren ) {  
	%current =  %profile.getFieldValue(%field); 
   if (%current $= %value)
   	return;
   	
	%profile.setFieldValue(%field,%value);  
	GLab.setProfileDirty( %profile, true );
	
	if(!%skipChildren)
		GLab.updateProfileChildsField( %profile,%field,%value);
}
//------------------------------------------------------------------------------

//==============================================================================
//GLab.updateChildrensField("ToolsTextFX","fontType");
function GLab::updateProfileChildsField(%this,%profile,%field,%subLevel ) {
   if ($ProfChilds[%profile.getName()] $= "")
      return;
      
	 if (%field $= "colorFont" ||%field $= "colorFont"){
	 	warnLog("Trying to store invalid field to childrens:",%field);
	 	return;
	 }
      
	%value = %profile.getFieldValue(%field);

   foreach$(%child in $ProfChilds[%profile.getName()]){
      %ownField = strstr($ProfOwnedFields[%child],%field);
      if (%ownField !$= "-1" ){
         continue;
      }
      if (!isObject(%child)){
      	warnLog("SKIPPING Trying to update an invalid child:",%child);
         continue;
      }
      devLog(%child.getName(),"Child field:",%field,"Set to:",%value);
      %child.setFieldValue(%field,%value);
       GLab.updateProfileChildsField( %child,%field,%subLevel++);
       
      
   }  
}
//==============================================================================
function GLab::eraseSelectedField( %this ) {
   %profileName = $GLab_SelectedObject.getName(); 
  %text = "You are about to delete \c2"@ $GLab_SelectedField @"\c0 reference from profile:\c2"@ %profileName @"\c0.";
  %text = %text @ "This will remove the field from profile file and the profile will use the parent value.\n Proceed to field removal?";
  msgBoxYesNo("Erase field from profile",%text,"GLab.eraseProfileField();","");
 //  removeProfileField(%profileName,$GLab_SelectedField);
  // %this.eraseProfileField( %profileName, $GLab_SelectedField);
}
function GLab::eraseProfileField( %this,%profileName,%field ) {
   %profileName = $GLab_SelectedObject.getName(); 
   //  removeProfileField(%profileName,$GLab_SelectedField);
   GameProfilesPM.setDirty(%profileName);
    GameProfilesPM.removeField(%profileName,$GLab_SelectedField);
   
   
   GameProfilesPM.saveDirtyObject(%profileName);   
   
   //Now we should set the parent value right now
   %parent = GLab.findParentFieldSource(%profileName,%field);
   %parentValue = %parent.getFieldValue(%field);
              
   if (%parentValue $= "")
      return;
      
   GLab.updateProfileField(%profileName,%field,%parentValue,true);   
   %this.syncProfileField(%field);
}
// removeProfileFieldSet(GuiTextProfileA_C,"colorFont");
//------------------------------------------------------------------------------