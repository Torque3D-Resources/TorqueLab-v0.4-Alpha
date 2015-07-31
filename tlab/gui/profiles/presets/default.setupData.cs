//==============================================================================
// TorqueLab -> GUI Profiles Setup (Colors, Fonts, etc)
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
delObj(LabProfileDataList);
%fid = -1;
%fontData = new ScriptObject(LabProfileDataList){
	font[%fid++] = "Gotham" NL "" TAB "Black" TAB "Book";
	font[%fid++] = "Aileron" NL "" TAB "Bold";
	font[%fid++] = "Gafata" NL "Std";
	font[%fid++] = "Lato" NL "";
	font[%fid++] = "Davidan" NL "Std";
};
	
%happy = "YES";