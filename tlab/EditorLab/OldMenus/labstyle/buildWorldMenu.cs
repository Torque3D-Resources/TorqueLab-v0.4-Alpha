//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function Lab::buildMenu(%this) {
	Lab.setActiveMenu(LabMenu);
	Lab.clearMenus();
	%menuId = 0;
	devLog("Building world menu");
	while($LabMenu[%menuId] !$= "") {
		Lab.addMenu( $LabMenu[%menuId],%menuId);
		%menuItemId = 0;

		while($LabMenuItem[%menuId,%menuItemId] !$= "") {
			%itemData = $LabMenuItem[%menuId,%menuItemId];
			
			Lab.addMenuItemData( %menuId,%menuItemId,%itemData);
			//Lab.addMenuItem( %menuId, getField(%item,0),%menuItemId,getField(%item,1),%checkId);
			%subMenuItemId = 0;

			while($LabMenuSubMenuItem[%menuId,%menuItemId,%subMenuItemId] !$= "") {
				%subItemData = $LabMenuSubMenuItem[%menuId,%menuItemId,%subMenuItemId];								
				Lab.addSubMenuItemData( %menuId,%menuItemId,%subMenuItemId,%subItemData);
				//Lab.addSubmenuItem(%menuId,%menuItemId,getField(%subitem,0),%subMenuItemId,getField(%subitem,1),%subCheckId);
				%subMenuItemId++;
			}

			%menuItemId++;
		}

		%menuId++;
	}
	LabMenu.setCheckmarkBitmapIndex(2);
	LabMenu.setMenuMargins("0","0","0");
}
//------------------------------------------------------------------------------