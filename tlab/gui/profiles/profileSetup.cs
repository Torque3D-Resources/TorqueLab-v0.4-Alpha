//==============================================================================
// TorqueLab -> GUI Profiles Setup (Colors, Fonts, etc)
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------

function initProfileSetupParams(%this) {
%gid = 0;
	%ar = createParamsArray("ProfileSetupParams",LDG_ProfileSetupStackParams,"$Lab");
	%ar.validateSubField = true;
	%ar.group[%gid++] = "Mouse settings" TAB "Columns 0 130" ;
	%ar.style = "ColStyleB";
	%ar.autoSyncPref = "1";
	%ar.useNewSystem = true;
	%ar.setVal("Color_Dark_A",       "" TAB "Color Dark A" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Dark_B",       "" TAB "Color Dark B" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Dark_C",       "" TAB "Color Dark C" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	
	%ar.setVal("Color_Light_A",       "" TAB "Color Light A" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Light_B",       "" TAB "Color Light B" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Light_C",       "" TAB "Color Light C" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	
	%ar.setVal("Color_Accent_A",       "" TAB "Color Accent A" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Accent_B",       "" TAB "Color Accent B" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	%ar.setVal("Color_Accent_C",       "" TAB "Color Accent C" TAB "ColorEdit" TAB "mode>>int" TAB "" TAB %gid);	
	
	buildParamsArray(%ar,true);
	Lab.profileSetupArray = %ar;
}
//==============================================================================
// TorqueLab Colors Definition
//------------------------------------------------------------------------------
function initProfileSetupData(%this) {
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
 
}
initProfileSetupData();

//==============================================================================
// Related Color fields ID information
//------------------------------------------------------------------------------
// fontColor = fontColors[0]
// fontColorHL = fontColors[1]
// fontColorNA = fontColors[2]
// fontColorSEL = fontColors[3]
// fontColorLink = fontColors[4]
// fontColorLinkHL = fontColors[5]
  
//==============================================================================
function Lab::initProfilesSetupData(%this,%useDefault,%dontApply) {
	if (%useDefault)
		initProfileSetupData();
	
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
//LGM.eraseProfileField(