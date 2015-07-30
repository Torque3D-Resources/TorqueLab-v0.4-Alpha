//==============================================================================
// GameLab -> Interface Development Gui
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Profile fields update functions
//==============================================================================

//==============================================================================
// Update a single profile field and set profile dirty if changed
function LGTools::updateProfileField(%this,%profile,%field,%value,%notDirty,%skipChildren ) {  
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
       //LGTools.updateProfileChildsField( %profile,%field,%value);
      //return;
   //}
	%this.setProfileFieldValue(%profile,%field,%value);
  // %profile.setFieldValue(%field,%value);  
   //Update the childs with new value so changes are applied to them also
   //LGTools.updateProfileChildsField( %profile,%field,%value);
   //LGTools.setProfileDirty( %profile, true );
   
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
function LGTools::setProfileFieldValue(%this,%profile,%field,%value,%skipChildren ) {  
	%current =  %profile.getFieldValue(%field); 
   if (%current $= %value)
   	return;
   	
	%profile.setFieldValue(%field,%value);  
	LGTools.setProfileDirty( %profile, true );
	
	if(!%skipChildren)
		LGTools.updateProfileChildsField( %profile,%field,%value);
}
//------------------------------------------------------------------------------

//==============================================================================
//LGTools.updateChildrensField("ToolsTextFX","fontType");
function LGTools::updateProfileChildsField(%this,%profile,%field,%subLevel ) {
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
       LGTools.updateChildrensField( %child,%field,%subLevel++);
       
      
   }  
}

//------------------------------------------------------------------------------
