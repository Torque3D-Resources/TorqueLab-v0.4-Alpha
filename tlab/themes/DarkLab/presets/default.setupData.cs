//==============================================================================
// TorqueLab -> GUI Profiles Setup (Colors, Fonts, etc)
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
delObj(LabProfileDataList);

%fontData = new ScriptObject(LabProfileDataList) {
	font[%fid++] = "Gotham" NL "" TAB "Black" TAB "Book";
	font[%fid++] = "Aileron" NL "" TAB "Bold";
	font[%fid++] = "Gafata" NL "Std";
	font[%fid++] = "Lato" NL "";
	font[%fid++] = "Davidan" NL "Std";
	dataType[%dtid++] = "fillColor" TAB "Color" TAB "HL SEL NA";
	dataType[%dtid++] = "fontColor" TAB "Color" TAB "HL NA SEL Link LinkHL";
	dataType[%dtid++] = "fontType" TAB "FontType" TAB "";
	dataType[%dtid++] = "fontColors" TAB "ColorIds" TAB "";
};

%happy = "YES";