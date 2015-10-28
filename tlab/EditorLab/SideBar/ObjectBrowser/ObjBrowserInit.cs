//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$ObjectBrowser_View[0] = "1 Column" TAB "Col 1";
$ObjectBrowser_View[1] = "2 Column" TAB "Col 2";
$ObjectBrowser_View[2] = "3 Column" TAB "Col 3";
$ObjectBrowser_ViewId = 0;

$ObjectBrowser_FavRow = "600";

$ObjectBrowser_TypeId = 0;

//SceneBrowserTree.rebuild
function Lab::initObjectBrowser( %this ) {
	ObjectBrowser.initFiles();
	
	ObjectBrowser.currentViewId = "";
	ObjectBrowserViewMenu.clear();
	%i = 0;
	while($ObjectBrowser_View[%i] !$= ""){
		%text = getField($ObjectBrowser_View[%i],0); 
		ObjectBrowserViewMenu.add(%text,%i);
		%i++;
	}
	ObjectBrowser.setViewId($ObjectBrowser_ViewId);
	//ObjectBrowserViewMenu.setSelected($ObjectBrowser_ViewId);
	
	ObjectBrowserTypeMenu.clear();
	ObjectBrowserTypeMenu.add("Level",0);
	ObjectBrowserTypeMenu.add("Scripted",1);
	ObjectBrowserTypeMenu.setSelected($ObjectBrowser_TypeId);
}
function ObjectBrowserViewMenu::onSelect( %this,%id,%text ) {
	$ObjectBrowser_ViewId = %id;
	ObjectBrowser.setViewId($ObjectBrowser_ViewId);
}
function ObjectBrowserTypeMenu::onSelect( %this,%id,%text ) {
	$ObjectBrowser_TypeId = %id;
	
	ObjectBrowser.objType = %text;
	%lastTabAddress = ObjectBrowser.lastTypeAdress[%text];
	ObjectBrowser.navigate( %lastTabAddress );
}

function ObjectBrowser::toggleFavorites( %this ) {
	%currentFavRow = getWord(ObjectBrowserFrameSet.rows,1);
	if (%currentFavRow $= ""){
		%lastFavRow = ObjectBrowser.lastFavRow;
		if (%lastFavRow $= "")
			%lastFavRow = $ObjectBrowser_FavRow;
		
		ObjectBrowserFrameSet.rows = "0" SPC %lastFavRow;
	} else {
		ObjectBrowser.lastFavRow = %currentFavRow;
			ObjectBrowserFrameSet.rows = "0";
	}
	ObjectBrowserFrameSet.updateSizes();
	
}
