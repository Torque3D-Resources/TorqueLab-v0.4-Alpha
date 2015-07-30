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
