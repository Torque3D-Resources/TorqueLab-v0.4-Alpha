//==============================================================================
// GuiLab -> Profiles Dirty management
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Profile saving functions
//==============================================================================
function initProfileSetupParams(%this) {
%gid = 0;
	%ar = createParamsArray("ProfileSetupParams",LDG_ProfileSetupStackParams,"$Lab");
	%ar.validateSubField = true;
	%ar.style = "ColStyleB";
	%ar.autoSyncPref = "1";
	%ar.useNewSystem = true;
	
	
	%ar.group[%gid++] = "Default color settings" TAB "Columns 0 130" ;
	%ar.setVal("Color_Dark_A",       "" TAB "Color Dark A" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Dark_B",       "" TAB "Color Dark B" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Dark_C",       "" TAB "Color Dark C" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	
	%ar.setVal("Color_Light_A",       "" TAB "Color Light A" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Light_B",       "" TAB "Color Light B" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Light_C",       "" TAB "Color Light C" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	
	%ar.setVal("Color_Accent_A",       "" TAB "Color Accent A" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Accent_B",       "" TAB "Color Accent B" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	%ar.setVal("Color_Accent_C",       "" TAB "Color Accent C" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);	
	

	%ar.group[%gid++] = "Custom color settings" TAB "Columns 0 130" ;
	%ar.setVal("Color_EditDark_HL",       "" TAB "DarkEdit Highlight" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);
	%ar.setVal("Color_EditDark_Sel",       "" TAB "DarkEdit Select" TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);
	
	foreach$( %data in $LabDataColorIdsTypes) {
		%ar.group[%gid++] = "Color Ids ->\c9 "@%data@" \c0Settings" TAB "Columns 0 130" ;
		for(%i=0; %i<10; %i++)
			%ar.setVal("Color_"@%data@"_"@%i,     "" TAB "Color "@%data@" Id:"@%i TAB "ColorEdit" TAB "mode>>int;;noAlpha>>1" TAB "" TAB %gid);
			
	}
	
	%ar.group[%gid++] = "Game font settings" TAB "Columns 0 130" ;
	%ar.setVal("Font_Game_Base_Std",     "" TAB "Font Game Base Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Base_Strong",  "" TAB "Font Game Base Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Base_Thin",    "" TAB "Font Game Base Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Fx_Std",       "" TAB "Font Game Fx Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Fx_Strong",    "" TAB "Font Game Fx Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Fx_Thin",      "" TAB "Font Game Fx Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Alt_Std",       "" TAB "Font Game Alt Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Alt_Strong",    "" TAB "Font Game Alt Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Game_Alt_Thin",      "" TAB "Font Game Alt Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	
	%ar.group[%gid++] = "Tools font settings" TAB "Columns 0 130" ;
	%ar.setVal("Font_Tools_Base_Std",     "" TAB "Font Tools Base Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Base_Strong",  "" TAB "Font Tools Base Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Base_Thin",    "" TAB "Font Tools Base Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Fx_Std",       "" TAB "Font Tools Fx Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Fx_Strong",    "" TAB "Font Tools Fx Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Fx_Thin",      "" TAB "Font Tools Fx Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Alt_Std",       "" TAB "Font Tools Alt Std" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Alt_Strong",    "" TAB "Font Tools Alt Strong" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);	
	%ar.setVal("Font_Tools_Alt_Thin",      "" TAB "Font Tools Alt Thin" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "" TAB %gid);		
	
	buildParamsArray(%ar,true);
	Lab.profileSetupArray = %ar;
}

function exportProfileSetupParams(%this,%file) {		
	export("$LabColor*","tlab/profDefaultTest.cs");	
	%fileWrite = getFileWriteObj("tlab/gui/profiles/presets/defaultProfile.arrayData.cs");	
	%array = arGuiProfilesSetupData;		
	for(%i = 0; %i< %array.count(); %i++) {
		%key = %array.getKey(%i);
		%data = %array.getValue(%i);
		%fileWrite.writeLine("$ProfSetupData_"@%i@" = \""@getRecord(%data,0)@"\" NL \""@getRecord(%data,1)@"\" NL \""@getRecord(%data,2)@"\" NL \""@getRecord(%data,3)@"\";");		
	}
	%fileWrite.writeLine("$ProfSetupData_Total = "@%array.count()@";");		
	closeFileObj(%fileWrite);	
}