//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$LabProfileStyleActive = "";



//==============================================================================
function LDG::initProfileStyles( %this ) {
	%this.updateProfileStylesList();
}
//------------------------------------------------------------------------------


//==============================================================================
function LDG::updateProfileStylesList( %this,%profile ) {
	LDG_ProfileStylesMenu.clear();
	Lab.scanProfileStyles();
	LDG_ProfileStylesMenu.add("Default",0);
	%selected = "0";

	foreach$(%style in $LabProfileStyles) {
		LDG_ProfileStylesMenu.add(%style,%sId++);

		if ($LabProfileStyleActive $= %style)
			%selected = %sId;
	}

	LDG_ProfileStylesMenu.setSelected(%selected,false);
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG_ProfileStylesMenu::onSelect( %this,%id,%text ) {
	LDG.selectProfileStyle(%text);
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG::selectProfileStyle( %this,%style,%apply ) {
	$LabProfileStyleActive = %style;
	LDG_ProfileStylesNameEdit.setText(%style);
	LDG_ProfileStyleDataTree.init();
	//if (%apply)
	Lab.importProfilesStyle(%style);
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG::importProfileStyle( %this,%style ) {
	if (%style $= "")
		%style = $LabProfileStyleActive;

	if (%style $= "")
		return;

	Lab.importProfilesStyle(%style);
}
//------------------------------------------------------------------------------
//==============================================================================
function LDG::saveProfileStyle( %this,%style ) {
	if (%style $= "")
		%style = LDG_ProfileStylesNameEdit.getText();

	if (%style $= "")
		return;

	Lab.exportCurrentProfilesStyle(%style);
	$LabProfileStyleActive = %style;
	%this.updateProfileStylesList();
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG::saveOriginalProfileFromStyle( %this ) {
	Lab.saveAllDirtyProfilesStyle();
}
//------------------------------------------------------------------------------

//==============================================================================
// Profiles Style Tree Functions and Callbacks
//==============================================================================

//==============================================================================
function LDG_ProfileStyleDataTree::init( %this ) {
	if (!isObject($LabStyleCurrentGroup))
		return;

	%this.clear();

	foreach( %obj in $LabStyleCurrentGroup ) {
		// Create a visible name.
		%profile = %obj.internalName;

		if( %profile $= "" )
			continue;

		%group = %this.findChildItemByName( 0, %profile.category );

		if( !%group )
			%group = %this.insertItem( 0, %profile.category );

		// Insert the item.
		%id = %this.insertItem( %group, %profile, %obj.getId(), "" );
	}

	%this.sort( 0, true, true, false );
	%this.schedule(50,"buildVisibleTree");
}
//==============================================================================
function LDG_ProfileStyleDataTree::onSelect( %this,%itemId ) {
	%profile = LDG_ProfileStyleDataTree.getItemText( %itemId );
	devLog("LDG_ProfileStyleDataTree onSelect Item:",%itemId,"Profile:",%profile);
	%style = $LabProfileStyleActive;
	%styleObj = %profile.getName()@"_"@%style@"Style";
	devLog("StyleObj=",%styleObj);

	//Check if we click a profile and not a group
	if (!isObject(%styleObj)) {
		return;
	}

	LDG.editProfileStyle(%styleObj);
}
//------------------------------------------------------------------------------