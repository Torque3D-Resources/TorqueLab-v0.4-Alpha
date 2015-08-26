//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

function SceneCreatorWindow::init( %this ) {
	// Just so we can recall this method for testing changes
	// without restarting.
	if ( isObject( %this.array ) )
		%this.array.delete();

	%this.array = new ArrayObject();
	%this.array.caseSensitive = true;
	%this.setListView( true );
	
	%this.registerObjects();
	SceneCreatorWindow.contentCtrl = CreatorIconArray;
}

//==============================================================================
function SceneCreatorWindow::onWake( %this ) {
	if ($SEP_CreatorTypeActive $= "")
		$SEP_CreatorTypeActive = "0";

	CreatorTypeStack.getObject(0).performClick();
}
//------------------------------------------------------------------------------

//==============================================================================
function SceneCreatorWindow::getCreateObjectPosition() {
	%focusPoint = LocalClientConnection.getControlObject().getLookAtPoint();

	if( %focusPoint $= "" )
		return "0 0 0";
	else
		return getWord( %focusPoint, 1 ) SPC getWord( %focusPoint, 2 ) SPC getWord( %focusPoint, 3 );
}
//------------------------------------------------------------------------------
//==============================================================================


function SceneCreatorWindow::navigateDown( %this, %folder ) {
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
function SceneCreatorWindow::navigateUp( %this ) {
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
function SceneCreatorWindow::setListView( %this, %noupdate ) {
	//CreatorIconArray.clear();
	//CreatorIconArray.setVisible( false );
	CreatorIconArray.setVisible( true );
	SceneCreatorWindow.contentCtrl = CreatorIconArray;
	%this.isList = true;

	if ( %noupdate == true )
		%this.navigate( %this.address );
}
//------------------------------------------------------------------------------
//==============================================================================
//function SceneCreatorWindow::setIconView( %this )
//{
//echo( "setIconView" );
//
//CreatorIconStack.clear();
//CreatorIconStack.setVisible( false );
//
//CreatorIconArray.setVisible( true );
//%this.contentCtrl = CreatorIconArray;
//%this.isList = false;
//
//%this.navigate( %this.address );
//}

//------------------------------------------------------------------------------
//==============================================================================
function CreatorPopupMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	SceneCreatorWindow.navigate( %split );
}
//------------------------------------------------------------------------------
