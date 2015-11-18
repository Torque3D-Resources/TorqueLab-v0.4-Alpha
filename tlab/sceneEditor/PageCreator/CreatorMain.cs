//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SEP_Creator::init( %this ) {
	// Just so we can recall this method for testing changes
	// without restarting.
	%this.setListView( true );
	Scene.registerObjects();
	SEP_Creator.contentCtrl = CreatorIconArray;
}
//==============================================================================
function SEP_Creator::onWake( %this ) {
	if ($SEP_CreatorTypeActive $= "")
		$SEP_CreatorTypeActive = "0";

	CreatorTypeStack.getObject(0).performClick();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function CreatorTabBook::onTabSelected( %this, %text, %idx ) {
	if ( %this.isAwake() ) {
		%lastTabAdress = 	SEP_Creator.lastCreatorAdress[%text];
		//SEP_Creator.tab = %text;
		//SEP_Creator.navigate( %lastTabAdress );
		SEP_Creator.tab = %text;
		SEP_Creator.navigate( %lastTabAdress );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function SEP_CreatorButton::onClick( %this ) {
	%type = %this.text;

	if (%type $= "Options") {
		show(CreatorOptionsArray);
		hide(CreatorIconArray);
		return;
	}

	hide(CreatorOptionsArray);
	show(CreatorIconArray);

	if ( SEP_Creator.isAwake() ) {
		%lastTabAdress = 	SEP_Creator.lastCreatorAdress[%type];
		SEP_Creator.tab = %type;
		SEP_Creator.navigate( %lastTabAdress );
	}

	$SEP_CreatorTypeActive = %this.internalName;
}
//------------------------------------------------------------------------------
//==============================================================================
function CreatorPopupMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	SEP_Creator.navigate( %split );
}
//------------------------------------------------------------------------------




//==============================================================================
// Creator Objects Listing
//==============================================================================
//==============================================================================
function SEP_Creator::setListView( %this, %noupdate ) {
	//CreatorIconArray.clear();
	//CreatorIconArray.setVisible( false );
	CreatorIconArray.setVisible( true );
	SEP_Creator.contentCtrl = CreatorIconArray;
	%this.isList = true;

	if ( %noupdate == true )
		%this.navigate( %this.address );
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.setDisplayMode("Detail");
function SEP_Creator::setDisplayMode( %this,%mode ) {
	%settingObj = $SceneEd_CreatorArray[%mode];
	%dynCount = %settingObj.getDynamicFieldCount();

	for(%i = 0; %i< %dynCount; %i++) {
		%fieldFull = %settingObj.getDynamicField(%i);
		%field = getField(%fieldFull,0);
		%value = getField(%fieldFull,1);
		CreatorIconArray.setFieldValue(%field,%value);
	}

	CreatorIconArray.refresh();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.setDisplayMode("Detail");
function SEP_Creator::setIconWidth( %this,%ctrl ) {
	%width = %ctrl.getValue();

	if (%width < 10 || %width > 300)
		return;

	devLog("Width = ",%width);
	CreatorIconArray.setFieldValue("colSize",%width);
	CreatorIconArray.refresh();
	%ctrl.updateFriends();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SEP_Creator.updatePrefabInMeshes("Detail");
function SEP_Creator::updatePrefabInMeshes( %this ) {
	if (%this.tab $= "Meshes")
		%this.navigate(%this.lastCreatorAdress[%this.tab]);
}
//------------------------------------------------------------------------------

//==============================================================================
// Creator Objects Navigation
//==============================================================================

//==============================================================================
function SEP_Creator::navigateDown( %this, %folder ) {
	if ( %this.address $= "" )
		%address = %folder;
	else
		%address = %this.address SPC %folder;

	// Because this is called from an IconButton::onClick command
	// we have to wait a tick before actually calling navigate, else
	// we would delete the button out from under itself.
	%this.schedule( 1, "navigate", %address );
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_Creator::navigateUp( %this ) {
	%count = getWordCount( %this.address );

	if ( %count == 0 )
		return;

	if ( %count == 1 )
		%address = "";
	else
		%address = getWords( %this.address, 0, %count - 2 );

	%this.navigate( %address );
}
//------------------------------------------------------------------------------

//==============================================================================
// Navigate through Creator Book Data
function SEP_Creator::navigate( %this, %address ) {
	CreatorIconArray.frozen = true;
	CreatorIconArray.clear();
	CreatorPopupMenu.clear();
	%this.lastCreatorAdress[%this.tab] = %address;

	if (!%this.isMethod("navigate"@%this.tab)||%this.tab $= "") {
		warnLog("Couldn't find a scene creator navigating function for type:",%this.tab);
		return;
	}

	eval("%this.navigate"@%this.tab@"(%address);");
	CreatorIconArray.sort( "alphaIconCompare" );

	for ( %i = 0; %i < CreatorIconArray.getCount(); %i++ ) {
		CreatorIconArray.getObject(%i).autoSize = false;
	}

	CreatorIconArray.frozen = false;
	CreatorIconArray.refresh();
	// Recalculate the array for the parent guiScrollCtrl
	CreatorIconArray.getParent().computeSizes();
	%this.address = %address;
	CreatorPopupMenu.sort();
	%str = strreplace( %address, " ", "/" );
	%r = CreatorPopupMenu.findText( %str );

	if ( %r != -1 )
		CreatorPopupMenu.setSelected( %r, false );
	else
		CreatorPopupMenu.setText( %str );

	CreatorPopupMenu.tooltip = %str;
}

//------------------------------------------------------------------------------
