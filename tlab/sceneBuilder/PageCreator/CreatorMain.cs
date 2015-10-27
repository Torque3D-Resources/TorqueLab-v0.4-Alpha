//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SBP_Creator::init( %this ) {
	// Just so we can recall this method for testing changes
	// without restarting.
	
	%this.setListView( true );
	
	Scene.registerObjects();
	SBP_Creator.contentCtrl = SBP_CreatorIconArray;
}
//==============================================================================
function SBP_Creator::onWake( %this ) {
	

	//SBP_CreatorTypeStack.getObject(0).performClick();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function CreatorTabBook::onTabSelected( %this, %text, %idx ) {
	if ( %this.isAwake() ) {
		%lastTabAdress = 	SBP_Creator.lastCreatorAdress[%text];
		//SBP_Creator.tab = %text;
		//SBP_Creator.navigate( %lastTabAdress );
		SBP_Creator.tab = %text;
		SBP_Creator.navigate( %lastTabAdress );
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function SBP_CreatorButton::onClick( %this ) {
	
	%type = %this.internalName;
	SBP_Creator.setType(%type,true);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
function SBP_Creator::setType( %this,%type,%fromButton ) {
	
	if (%type $= "Options"){
		show(SBP_CreatorOptionsArray);
		hide(SBP_CreatorIconArray);
		return;
	}
	hide(SBP_CreatorOptionsArray);
	show(SBP_CreatorIconArray);
	if ( SBP_Creator.isAwake() ) {
		%lastTabAdress = 	SBP_Creator.lastCreatorAdress[%type];
		SBP_Creator.tab = %type;
		SBP_Creator.navigate( %lastTabAdress );
	}
	%visPreset = "SBP_"@%type;

	if (isFile("tlab/EditorLab/editorTools/visibilityLayers/presets/"@%visPreset@".layers.ucs"))
		EVisibilityLayers.loadPresetFile(%visPreset,true);

	$SBP_CreatorTypeActive = %type;
}
//------------------------------------------------------------------------------
//==============================================================================
function SBP_CreatorPopupMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	SBP_Creator.navigate( %split );
}
//------------------------------------------------------------------------------




//==============================================================================
// Creator Objects Listing
//==============================================================================
//==============================================================================
function SBP_Creator::setListView( %this, %noupdate ) {
	//SBP_CreatorIconArray.clear();
	//SBP_CreatorIconArray.setVisible( false );
	SBP_CreatorIconArray.setVisible( true );
	SBP_Creator.contentCtrl = SBP_CreatorIconArray;
	%this.isList = true;

	if ( %noupdate == true )
		%this.navigate( %this.address );
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SBP_Creator.setDisplayMode("Detail");
function SBP_Creator::setDisplayMode( %this,%mode ) {
	%settingObj = $SBP_CreatorArray[%mode];
	%dynCount = %settingObj.getDynamicFieldCount();

	for(%i = 0; %i< %dynCount; %i++) {
		%fieldFull = %settingObj.getDynamicField(%i);
		%field = getField(%fieldFull,0);
		%value = getField(%fieldFull,1);
		SBP_CreatorIconArray.setFieldValue(%field,%value);
	}

	SBP_CreatorIconArray.refresh();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SBP_Creator.setDisplayMode("Detail");
function SBP_Creator::setIconWidth( %this,%ctrl ) {
	%width = %ctrl.getValue();
	if (%width < 10 || %width > 300)
		return;
		devLog("Width = ",%width);
	SBP_CreatorIconArray.setFieldValue("colSize",%width);
	SBP_CreatorIconArray.refresh();
	%ctrl.updateFriends();
}
//------------------------------------------------------------------------------
//==============================================================================
// Creator Book Tab Selected
//SBP_Creator.updatePrefabInMeshes("Detail");
function SBP_Creator::updatePrefabInMeshes( %this ) {
	if (%this.tab $= "Meshes")
		%this.navigate(%this.lastCreatorAdress[%this.tab]);
}
//------------------------------------------------------------------------------

//==============================================================================
// Creator Objects Navigation
//==============================================================================

//==============================================================================
function SBP_Creator::navigateDown( %this, %folder ) {
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
function SBP_Creator::navigateUp( %this ) {
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
function SBP_Creator::navigate( %this, %address ) {
	SBP_CreatorIconArray.frozen = true;
	SBP_CreatorIconArray.clear();
	SBP_CreatorPopupMenu.clear();
	%this.lastCreatorAdress[%this.tab] = %address;
	
	if (!%this.isMethod("navigate"@%this.tab)||%this.tab $= ""){
		warnLog("Couldn't find a scene creator navigating function for type:",%this.tab);
		return;
	}
	eval("%this.navigate"@%this.tab@"(%address);");

	SBP_CreatorIconArray.sort( "alphaIconCompare" );

	for ( %i = 0; %i < SBP_CreatorIconArray.getCount(); %i++ ) {
		SBP_CreatorIconArray.getObject(%i).autoSize = false;
	}

	SBP_CreatorIconArray.frozen = false;
	SBP_CreatorIconArray.refresh();
	// Recalculate the array for the parent guiScrollCtrl
	SBP_CreatorIconArray.getParent().computeSizes();
	%this.address = %address;
	SBP_CreatorPopupMenu.sort();
	%str = strreplace( %address, " ", "/" );
	%r = SBP_CreatorPopupMenu.findText( %str );

	if ( %r != -1 )
		SBP_CreatorPopupMenu.setSelected( %r, false );
	else
		SBP_CreatorPopupMenu.setText( %str );

	SBP_CreatorPopupMenu.tooltip = %str;
}

//------------------------------------------------------------------------------

