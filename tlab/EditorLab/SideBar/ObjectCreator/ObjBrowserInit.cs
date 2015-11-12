//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$ObjectCreator_View[0] = "1 Column" TAB "Col 1";
$ObjectCreator_View[1] = "2 Column" TAB "Col 2";
$ObjectCreator_View[2] = "3 Column" TAB "Col 3";
$ObjectCreator_ViewId = 0;

$ObjectCreator_FavRow = "600";

$ObjectCreator_TypeId = 0;

//SceneBrowserTree.rebuild
function Lab::initObjectCreator( %this ) {
	ObjectCreator.initFiles();
	
	ObjectCreator.currentViewId = "";
	ObjectCreatorViewMenu.clear();
	%i = 0;
	while($ObjectCreator_View[%i] !$= ""){
		%text = getField($ObjectCreator_View[%i],0); 
		ObjectCreatorViewMenu.add(%text,%i);
		%i++;
	}
	ObjectCreator.setViewId($ObjectCreator_ViewId);
	//ObjectCreatorViewMenu.setSelected($ObjectCreator_ViewId);
	
	ObjectCreatorTypeMenu.clear();
	ObjectCreatorTypeMenu.add("Level",0);
	ObjectCreatorTypeMenu.add("Scripted",1);
	ObjectCreatorTypeMenu.setSelected($ObjectCreator_TypeId);
}
function ObjectCreatorViewMenu::onSelect( %this,%id,%text ) {
	$ObjectCreator_ViewId = %id;
	ObjectCreator.setViewId($ObjectCreator_ViewId);
}
function ObjectCreatorTypeMenu::onSelect( %this,%id,%text ) {
	$ObjectCreator_TypeId = %id;
	
	ObjectCreator.objType = %text;
	%lastTabAddress = ObjectCreator.lastTypeAdress[%text];
	ObjectCreator.navigate( %lastTabAddress );
}

function ObjectCreatorTypeButton::onClick( %this ) {
	$ObjectCreator_TypeId = %id;
	
	ObjectCreator.objType = %this.internalName;
	%lastTabAddress = ObjectCreator.lastTypeAdress[%text];
	ObjectCreator.navigate( %lastTabAddress );
}

function ObjectCreator::toggleFavorites( %this ) {
	%currentFavRow = getWord(ObjectCreatorFrameSet.rows,1);
	if (%currentFavRow $= ""){
		%lastFavRow = ObjectCreator.lastFavRow;
		if (%lastFavRow $= "")
			%lastFavRow = $ObjectCreator_FavRow;
		
		ObjectCreatorFrameSet.rows = "0" SPC %lastFavRow;
	} else {
		ObjectCreator.lastFavRow = %currentFavRow;
			ObjectCreatorFrameSet.rows = "0";
	}
	ObjectCreatorFrameSet.updateSizes();
	
}
