//==============================================================================
// Lab GuiManager -> Profile Color Management System
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function Lab::loadProfileSetupDefault(%this) {
	exec("tlab/gui/profiles/profileDefaults.cs");
}
//==============================================================================
// Related Color fields ID information
//------------------------------------------------------------------------------
// fontColor = fontColors[0]
// fontColorHL = fontColors[1]
// fontColorNA = fontColors[2]
// fontColorSEL = fontColors[3]
// fontColorLink = fontColors[4]
// fontColorLinkHL = fontColors[5]
  $LDG_ProfileSetupLoaded = false;
//==============================================================================
function Lab::initProfilesSetupData(%this,%useDefault,%dontApply) {
	if (%useDefault)
		%this.loadProfileSetupDefault();
	
	%ar = newArrayObject("arGuiProfilesSetupData");	
	%ar.setVal("ToolsPanelDarkA",       "fillColor" TAB $Lab["Color","Dark","A"] SPC "255");
	%ar.setVal("ToolsPanelDarkB",       "fillColor" TAB $Lab["Color","Dark","B"] SPC "255");
	%ar.setVal("ToolsPanelDarkC",       "fillColor" TAB $Lab["Color","Dark","C"] SPC "255");
	
	%ar.setVal("ToolsPanelLightA",       "fillColor" TAB $Lab["Color","Light","A"] SPC "255");
	%ar.setVal("ToolsPanelLightB",       "fillColor" TAB $Lab["Color","Light","B"] SPC "255");
	%ar.setVal("ToolsPanelLightC",       "fillColor" TAB $Lab["Color","Light","C"] SPC "255");
	
	%ar.setVal("ToolsPanelColorA",       "fillColor" TAB $Lab["Color","Accent","A"] SPC "255");
	%ar.setVal("ToolsPanelColorB",       "fillColor" TAB $Lab["Color","Accent","B"] SPC "255");
	%ar.setVal("ToolsPanelColorC",       "fillColor" TAB $Lab["Color","Accent","C"] SPC "255");
	
	%ar.setVal("ToolsWindowPanel",       "fillColor" TAB $Lab["Color","Dark","A"] SPC "255");
	
	
	%ar.setVal("ToolsTextBase",       "fontColors" TAB "Ids Color Text" TAB "0>0;;1>1;;2>2;;3>3;;4>4;;5>5;;6>6;;7>7;;8>8;;9>9");
	%ar.setVal("ToolsTextEditDark",       "fontColors" TAB "Ids Color Text" TAB "0>0;;2>2");
	%ar.setVal("ToolsTextEditDark",       "fontColors" TAB "Ids Color EditDark" TAB "3>3;;1>1");

	if (!%dontApply)
		Lab.applyProfilesSetupData();
	
	$LDG_ProfileSetupLoaded = true;
}

function Lab::applyProfilesSetupData(%this) {
	if (!isObject(arGuiProfilesSetupData))
		return;
		$ProfileSetupDirtyList = "";
	%array = arGuiProfilesSetupData;	
	%count = %array.count();
	for(%i = 0; %i< %count; %i++) {
		%profile = %array.getKey(%i);
		%data = %array.getValue(%i);
		%field = getField(%data,0);
		%value = getField(%data,1);
		%extra = getField(%data,2);
		
		//Check if we want to update multiple IDs
		if (getWord(%value,0) $= "Ids"){
			%type = getWord(%value,1);
			%set = getWord(%value,2);
			%idsData = strreplace(%extra,";;","\t");
			devLog("Type:",%type,"Set",%set,"Data",%idsData);
			for(%j = 0;%j < getFieldCount(%idsData);%j++){
				%idData = getField(%idsData,%j);
				%idValues = strreplace(%idData,">"," ");
				%setSrc = getWord(%idValues,0);
				%id = getWord(%idValues,1);
				%realVal = $Lab[%type,%set,%setSrc];
				 Lab.applyProfileSetup(%profile,%field,%realVal,%id);				
				
			}
			continue;
		}
			
		 Lab.applyProfileSetup(%profile,%field,%value);		
	}
	%this.saveSetupDirtyProfiles();
}
function Lab::applyProfileSetup(%this,%profile,%field,%value,%id) {
	%profile.setFieldValue(%field,%value,%id);	
	%this.updateProfileChilds(%profile,%field,%value,%id);
	info(%profile, "field:",%field,"set to:",%value,"ID",%id);	
	$ProfileSetupDirtyList = strAddWord($ProfileSetupDirtyList,%profile,true);
}
function Lab::saveSetupDirtyProfiles(%this) {
	foreach$(%profile in $ProfileSetupDirtyList)
		Lab_PM.setDirty( %profile );
	
	//Save all dirty profiles using Persistence Manager
	Lab_PM.saveDirty();
}	


function Lab::updateProfileChilds(%this,%profile,%field,%value,%id,%subLevel ) {
   if ($ProfChilds[%profile.getName()] $= "")
      return;
	info(%profile.getName(),"childs update:",%field SPC %id,%value,"Level:",%subLevel);
   foreach$(%child in $ProfChilds[%profile.getName()]){
      %ownField = strstr($ProfOwnedFields[%child],%field);
      if (%ownField !$= "-1" ){
      	devLog("Skipping cause child have it own vavlue");
         continue;
      }
      if (!isObject(%child)){
      	warnLog("SKIPPING Trying to update an invalid child:",%child);
         continue;
      }
      info(%child.getName(), "field:",%field,"set to:",%value,"ID",%id);	
      %child.setFieldValue(%field,%value,%id);
		Lab.updateProfileChilds( %child,%field,%value,%id,%subLevel++);
       
      
   }  
}