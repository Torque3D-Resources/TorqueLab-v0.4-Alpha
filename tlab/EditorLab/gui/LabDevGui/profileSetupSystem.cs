//==============================================================================
// Lab GuiManager -> Profile Color Management System
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

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
function Lab::buildProfileSetupData(%this) {
	exec("tlab/gui/profiles/presets/default.setupData.cs");
	devLog("Happy?",%happy);
	if (!isObject(LabProfileDataList))
		return;
	%data = LabProfileDataList;
	%i = -1;
	$LabFontData_FamilyList = "";
	while(%data.font[%i++] !$=""){
		//font[%fid++] = "Gotham" NL "Book" TAB "Black" TAB "Book";
		%fontData = %data.font[%i];		
		%fontFamily = getRecord(%fontData,0);
		$LabFontData_FamilyList = strAddWord($LabFontData_FamilyList,%fontFamily);		
		$LabFontData_Family[%fontFamily] = "";
		
		%fontVariation = getRecord(%fontData,1);
		devLog("%fontVariation=",%fontVariation);
		%varCount = getFieldCount(%fontVariation);
		if (%varCount < 1){
			$LabFontData_Family[%fontFamily] = %fontFamily;
			continue;
		}
		for(%j=0;%j<%varCount;%j++){
			%variation = getField(%fontVariation,%j);
			if (%variation $= "")
				%addFont = %fontFamily;
			else
				%addFont = %fontFamily SPC %variation;
			
			devLog("Add font=",%addFont,"Variation",%variation);
			$LabFontData_Family[%fontFamily] = strAddField($LabFontData_Family[%fontFamily],%addFont,true);	
		}
	}
	
}
//==============================================================================

  //==============================================================================
function Lab::buildSetupTypesList(%this) {
	%baseCol = "Dark_A Dark_B Dark_C Light_A Light_B Light_C Accent_A Accent_B Accent_C";
	%textIdsCol = "Text TextAlt";
	%textCol = "EditDark_Sel EditDark_HL";
	%textIdList = "";
	foreach$( %data in %textIdsCol){
		for(%i=0;%i<10;%i++)
			%textIdList = strAddWord(%textIdList,%data@"_"@%i);
	}
	$LDG_TypeColor["ColorIds"] = %textIdsCol;
	$LDG_TypeColor["Base"]= %baseCol;
	$LDG_TypeColor["Text"] = %textIdList;
	$LDG_TypeColor["Custom"] = %textCol;
	$LDG_TypeColor["Color"] = %baseCol SPC %textIdList SPC %textCol;

	
}
//==============================================================================

//==============================================================================
function Lab::initProfilesSetupData(%this,%useDefault,%dontApply) {
	if (%useDefault)
		%this.loadProfileSetupDefault();

	//===========================================================================
	// Profiles Setup Data is store in array with following structure
	//---------------------------------------------------------------------------
	// Key = Data type ending with unique inc ID
	// KeyValue = ProfileName NL FieldTarget TAB (opt)Field Index NL Value  NL Type
	//---------------------------------------------------------------------------
	// The default are store using $Lab["Type","Value"]; 
	//---------------------------------------------------------------------------
	
	%ar = newArrayObject("arGuiProfilesSetupData");	
	
	exec("tlab/gui/profiles/profDefaultArray.cs");
	for (%i=0;%i < $ProfSetupData_Total ;%i++){
		%data = $ProfSetupData_[%i];
		%ar.setVal("Color_"@%colorId++, %data);
	}	

	if (!%dontApply)
		Lab.applyProfilesSetupData();
	
	$LDG_ProfileSetupLoaded = true;
}

function Lab::loadProfileSetupSystem(%this) {
	exec("tlab/gui/profiles/profileDefaults.cs");
	Lab.buildSetupTypesList();
	Lab.initProfilesSetupData();
}

function Lab::loadProfileSetupDefault(%this) {
	exec("tlab/gui/profiles/profileDefaults.cs");
}
function Lab::applyProfilesSetupData(%this) {
	if (!isObject(arGuiProfilesSetupData))
		return;
		$ProfileSetupDirtyList = "";
	%array = arGuiProfilesSetupData;	
	%count = %array.count();
	for(%i = 0; %i< %count; %i++) {
		%key = %array.getKey(%i);
		%data = %array.getValue(%i);
		
		%profile = getRecord(%data,0);	
		%fieldData = getRecord(%data,1);
		%field = getField(%fieldData,0);
		%fieldId = getField(%fieldData,1);
		%sourceData = getRecord(%data,2);
		%sourceType = getRecord(%data,3);
		
		switch$(%sourceType){
			case "Color":
				%value = $Lab[%sourceType,%sourceData];
				if (%value $= "")
					continue;
					
				 Lab.applyProfileSetup(%profile,%field,%value,%fieldId);		
					
			case "ColorIds":
				for(%j=0;%j<10;%j++){
					%value = $Lab["Color",%sourceData,%j];					
					if (%value $= "")
						continue;
					 Lab.applyProfileSetup(%profile,%field,%value,%j);		
				}
		
		}		
			
		
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

//==============================================================================
// Build the Game related Profiles data list
function getSetupProfilesList() {
	newSimSet("LabProfilesGroup");

	foreach( %obj in GuiDataGroup ) {
		if( !%obj.isMemberOfClass( "GuiControlProfile" ) )
			continue;

		%startCat = getSubStr(%obj.category,0,3);
		$GLab_IsGameProfile[%obj.getName()] = "";
		$GLab_IsToolProfile[%obj.getName()] = "";

		if (%startCat !$= "Gam") {
			$GLab_GameProfileList = strAddWord($GLab_GameProfileList,%obj.getName(),true);
			$GLab_IsGameProfile[%obj.getName()] = true;
		} else if (strFind(%obj.getName(),"Tools") || strFind(%obj.getName(),"Lab") || %obj.category $= "Tools" || strFind(%obj.getName(),"Inspector")) {
			$GLab_IsToolProfile[%obj.getName()] = true;
			$GLab_ToolsProfileList = strAddWord($GLab_ToolsProfileList,%obj.getName(),true);
		} else {
			$GLab_UnlistedProfileList = strAddWord($GLab_UnlistedProfileList,%obj.getName(),true);
		}

		LabProfilesGroup.add( %obj);
	}
}
//------------------------------------------------------------------------------

function exportProfileSetupStuff(%this,%file) {		
	%file = "tlab/EditorLab/LabDevGui/fullExportStuff.cs";
	export("$LabColor*",%file);	
	export("$LabFont*",%file,true);	

}
/*
$Lab["Color","Dark","A"] = "21 26 32";
	$Lab["Color","Dark","B"] = "34 35 35";
	$Lab["Color","Dark","C"] = "19 40 55";

	$Lab["Color","Light","A"] = "242 242 242";
	$Lab["Color","Light","B"] = "239 246 248";
	$Lab["Color","Light","C"] = "216 216 216";	
	
	$Lab["Color","Accent","A"] = "14 151 226";
	$Lab["Color","Accent","B"] = "17 45 58";
	$Lab["Color","Accent","C"] = "101 136 166";
	
	$Lab["Color","Text","0"] = "252 254 252";
	$Lab["Color","Text","1"] = "252 189 81";
	$Lab["Color","Text","2"] = "254 227 83";
	$Lab["Color","Text","3"] = "37 194 120";
	$Lab["Color","Text","4"] = "238 255 0";
	$Lab["Color","Text","5"] = "3 254 148";
	$Lab["Color","Text","6"] = "3 21 254";
	$Lab["Color","Text","7"] = "254 236 3";
	$Lab["Color","Text","8"] = "254 3 43";
	$Lab["Color","Text","9"] = "3 48 248";
	
	
	$Lab["Color","TextAlt","0"] = "252 254 252";
	$Lab["Color","TextAlt","1"] = "252 189 81";
	$Lab["Color","TextAlt","2"] = "254 227 83";
	$Lab["Color","TextAlt","3"] = "29 104 143";
	$Lab["Color","TextAlt","4"] = "238 255 0";
	$Lab["Color","TextAlt","5"] = "3 254 148";
	$Lab["Color","TextAlt","6"] = "3 21 254";
	$Lab["Color","TextAlt","7"] = "254 236 3";
	$Lab["Color","TextAlt","8"] = "254 3 43";
	$Lab["Color","TextAlt","9"] = "3 48 248";	
	
	$Lab["Color","EditDark","3"] = "34 102 132 150";
 	$Lab["Color","EditDark","1"] = "254 185 5";
 	*/