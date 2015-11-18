//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$LabProfileStyleActive = "";

//==============================================================================
function LDG::initProfileStyleParams(%this) {
	%gid = 0;
	%ar = createParamsArray("ProfileStyle",LDG_ProfileStyleStackParams,"$Lab");
	%ar.validateSubField = true;
	%ar.style = "ColStyleB";
	//%ar.autoSyncPref = "1";
	%ar.useNewSystem = true;
	%ar.group[%gid++] = "Fill Color settings" TAB "Columns 0 130" ;
	%ar.setVal("fillColor",       "" TAB "" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fillColorHL",       "" TAB "" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fillColorNA",       "" TAB "" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fillColorSEL",       "" TAB "" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.group[%gid++] = "Font Type and Color settings" TAB "Columns 0 130" ;
	%ar.setVal("fontType",     "" TAB "fontType" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "LDG.styleObj" TAB %gid);
	//%ar.setVal("fontSize",     "" TAB "fontType" TAB "DropdownEdit" TAB "fieldList>>$LabFontData_List" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColor",       "" TAB "fontColor (0)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColorHL",       "" TAB "fontColorHL (1)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColorNA",       "" TAB "fontColorNA (2)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColorSEL",       "" TAB "fontColorSEL (3)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColorLink",       "" TAB "fontColorLink (4)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.setVal("fontColorLinkHL",       "" TAB "fontColorLinkHL (5)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	%ar.group[%gid++] = "Style and layout settings" TAB "Columns 0 130";
	%ar.setVal("bitmap",     "" TAB "bitmap" TAB "FileSelect" TAB "file>>tlab/gui/styles/" TAB "LDG.styleObj" TAB %gid);
	//%ar.setVal("fontColors6",       "" TAB "fontColor (6)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	//%ar.setVal("fontColors7",       "" TAB "fontColor (7)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	//%ar.setVal("fontColors8",       "" TAB "fontColor (8)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	//%ar.setVal("fontColors9",       "" TAB "fontColor (9)" TAB "ColorEdit" TAB "mode>>int" TAB "LDG.styleObj" TAB %gid);
	/*
	 autoSizeHeight = "1";
	      autoSizeWidth = "1";
	      bitmap = "art/gui/DarkLab/container-assets/GuiScrollProfile.png";
	      border = "0";
	      borderColor = "100 100 100 255";
	      borderColorHL = "50 50 50 50";
	      borderColorNA = "75 75 75 255";
	      borderThickness = "3";
	      canKeyFocus = "0";
	      category = "Tools";
	      cursorColor = "0 0 0 255";

	      fontCharset = "ANSI";
	      fontColor = "0 0 0 255";
	      fontColorHL = "150 150 150 255";
	      fontColorLink = "255 0 255 255";
	      fontColorLinkHL = "255 0 255 255";
	      fontColorNA = "255 0 0 255";
	      fontColorSEL = "255 255 255 255";
	      fontSize = "12";
	      fontType = "Lucida Console";
	      hasBitmapArray = "1";
	      justify = "Left";
	      modal = "1";
	      mouseOverSelected = "0";
	      numbersOnly = "0";
	      opaque = "1";
	      returnTab = "0";
	      tab = "0";
	      textOffset = "0 0";
	      */
	buildParamsArray(%ar,false);
	Lab.profileStyleArray = %ar; ///arProfileStyleParam
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG::editProfileStyle( %this,%styleObj ) {
	devLog("Edit style ",%styleObj);
	LDG.styleObj = %styleObj;
	%profile = %styleObj.internalName();
	syncParamArray(arProfileStyleParam);
}
//------------------------------------------------------------------------------