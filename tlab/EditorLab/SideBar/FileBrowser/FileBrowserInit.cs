//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$FileBrowser_View[0] = "1 Column" TAB "Col 1";
$FileBrowser_View[1] = "2 Column" TAB "Col 2";
$FileBrowser_View[2] = "3 Column" TAB "Col 3";
$FileBrowser_ViewId = 0;

$FileBrowser_FavRow = "600";
//SceneBrowserTree.rebuild
function Lab::initFileBrowser( %this ) {
	FileBrowser.initFiles();
	FileBrowser.currentViewId = "";
	FileBrowserViewMenu.clear();
	%i = 0;

	while($FileBrowser_View[%i] !$= "") {
		%text = getField($FileBrowser_View[%i],0);
		FileBrowserViewMenu.add(%text,%i);
		%i++;
	}

	FileBrowser.setViewId($FileBrowser_ViewId);
	//FileBrowserViewMenu.setSelected($FileBrowser_ViewId);
}
function FileBrowserViewMenu::onSelect( %this,%id,%text ) {
	$FileBrowser_ViewId = %id;
	FileBrowser.setViewId($FileBrowser_ViewId);
}

function FileBrowser::toggleFavorites( %this ) {
	%currentFavRow = getWord(FileBrowserFrameSet.rows,1);

	if (%currentFavRow $= "") {
		%lastFavRow = FileBrowser.lastFavRow;

		if (%lastFavRow $= "")
			%lastFavRow = $FileBrowser_FavRow;

		FileBrowserFrameSet.rows = "0" SPC %lastFavRow;
	} else {
		FileBrowser.lastFavRow = %currentFavRow;
		FileBrowserFrameSet.rows = "0";
	}

	FileBrowserFrameSet.updateSizes();
}
