//==============================================================================
// Lab GuiManager -> Profile Params System
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
// Profile params update and syncing
//==============================================================================
//==============================================================================
// Called when a field have been changed
function LGTools::updateProfileParam( %this, %field,%value,%paramArray,%noUpdate,%arg3,%ctrl ) { 
  devLog("LGTools updateProfileParam Ctrl",%ctrl,"Array",%paramArray,"field",%field);
   if (%value $= ""){
      warnLog("Can't set a profile field to empty yet");
      return;
   }
   %arIndex = %paramArray.getIndexFromKey(%field);
   %data = %paramArray.getValue(%arIndex);      
   %objGlobal = getField(%data,4);
   eval("%obj = "@%objGlobal@";");
   //As now, the %syncObj should be a valid object directly 
   if (!isObject(%obj)){
      warnlog("Profile param sync obj is invalid object!!");      
       return;
   }	
   if ((%field $= "colorFont" || %field $= "colorFill") && !$GLab_EmbedColorSetInProfile){
   	devLog("Color set changed for field:",%field,"Value",%value);
   	LGTools.setProfileColorFromSet(%value,%field);
   	return;
   }
   
   if (%noUpdate)
      return;
   %this.updateProfileField(%obj,%field,%value);
   
   %ctrl = %paramArray.container.findObjectByInternalName(%field,true);
   //if (isObject(%ctrl))
  	// %ctrl.updateFriends();
   //%obj.setFieldValue(%field,%value); 
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function LGTools::syncProfileParamArray( %this ) { 
   %paramArray = $GLab_ProfileParams;   
   for( ; %i < %paramArray.count() ; %i++) {
      %field = %paramArray.getKey(%i);
      %data = %paramArray.getValue(%i); 
      %this.syncProfileField(%field,%data);
      
   }	
   
  
}
//------------------------------------------------------------------------------
//==============================================================================
// Sync the current profile values into the params objects
function LGTools::syncProfileField( %this,%field,%data ) { 
    %paramArray = $GLab_ProfileParams;   
   if (%data $= ""){
      %index = %paramArray.getIndexFromKey(%field);
      %data = %paramArray.getValue(%index); 
   }
      
   %pData = %paramArray.pData[%field];
      %dataPill = %paramArray.pill[%field];      
      %title = %paramArray.title[%field]; 
      
     
     
     //%paramArray.pill[%field] = %pData;
      
      %value = "";
      %objGlobal = getField(%data,4);
      
      %dataPill-->fieldTitle.guiGroup = "GLab_FieldButtons";
      
      addCtrlToGuiGroup( %dataPill-->fieldTitle);
      eval("%syncObj = "@%objGlobal@";");
      
      if (!isObject(%syncObj)){
         warnlog("Invalid syncobj for field:",%field,"Data",%syncObj);      
         return;
      }	
      
       %ownList = $ProfOwnedFields[%syncObj.getName()];
      %ownField = strFind(%ownList,%field);
      
      
      %dataPill.fieldTitle = %dataPill-->fieldTitle;
      %titleText = %title;
      if (!%ownField){
         %titleText = "*" SPC %title;        
      }
      %dataPill-->fieldTitle.text = %titleText;
      
      %value = %syncObj.getFieldValue(%field);  
      
      
      
      if (%value $= "") return;
      %ctrl = %paramArray.container.findObjectByInternalName(%field,true);
      %ctrl.setTypeValue(%value); 
   	%ctrl.updateFriends();
}
//------------------------------------------------------------------------------


