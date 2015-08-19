//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
//GuiEditCanvas.createLabMenu();
function GuiEditCanvas::createLabMenu(%this) {
	Lab.setActiveMenu(GuiEd_MenuBar);
	Lab.clearMenus();
	%menuId = 0;

	while($GuiEdMenu[%menuId] !$= "") {
		GuiEditCanvas.addMenu( $GuiEdMenu[%menuId],%menuId);
		%menuItemId = 0;

		while($GuiEdMenuItem[%menuId,%menuItemId] !$= "") {
			%item = $GuiEdMenuItem[%menuId,%menuItemId];
			GuiEditCanvas.addMenuItem( %menuId, getField(%item,0),%menuItemId,getField(%item,1),-1);
			%subMenuItemId = 0;

			while($GuiEdMenuSubMenuItem[%menuId,%menuItemId,%subMenuItemId] !$= "") {
				%subitem = $GuiEdMenuSubMenuItem[%menuId,%menuItemId,%subMenuItemId] ;
				GuiEditCanvas.addSubmenuItem(%menuId,%menuItemId,getField(%subitem,0),%subMenuItemId,getField(%subitem,1),-1);
				%subMenuItemId++;
			}

			%menuItemId++;
		}

		%menuId++;
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function GuiEditCanvas::addMenuBind(%this,%bind,%command) {
	GuiEdMap.bindCmd(keyboard, %bind, %command, "");
}
//------------------------------------------------------------------------------
//==============================================================================
function GuiEditCanvas::addMenu(%this,%group,%id) {
	if (%id $= "") %id = 1;

	GuiEd_MenuBar.addMenu(%group,%id);
	GuiEd_MenuBar.menuList = trim(GuiEd_MenuBar.menuList SPC %group);
}
//------------------------------------------------------------------------------

//==============================================================================
function GuiEditCanvas::addMenuItem(%this,%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup) {
	/*Adds a menu item to the specified menu. The menu argument can be either the text of a menu or its id.

	Parameters:
	%menu 	Menu name or menu Id to add the new item to.
	%menuItemText 	Text for the new menu item.
	%menuItemId 	Id for the new menu item.
	%accelerator 	Accelerator key for the new menu item.
	%checkGroup 	Check group to include this menu item in.*/
	$GuiEdMenuCallback[%menu,%menuItemId,%menuItemText] = getField($GuiEdMenuItem[%menu,%menuItemId],2);

	if (%accelerator !$="") {
		%callBack =  getField($GuiEdMenuItem[%menu,%menuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
	}

	GuiEd_MenuBar.addMenuItem(%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup);
	GuiEd_MenuBar.menuItemList[%menu] = trim(GuiEd_MenuBar.menuItemList[%menu] SPC %menuItemText);
}
//------------------------------------------------------------------------------

//==============================================================================
function GuiEditCanvas::addSubmenuItem(%this,%menuTarget,%menuItem,%submenuItemText,%submenuItemId,%accelerator,%checkGroup) {
	/*Adds a menu item to the specified menu. The menu argument can be either the text of a menu or its id.

	   Parameters:
	   %menuTarget 	Menu to affect a submenu in
	   %menuItem 	Menu item to affect
	   %submenuItemText 	Text to show for the new submenu
	   %submenuItemId 	Id for the new submenu
	   %accelerator 	Accelerator key for the new submenu
	   %checkGroup 	Which check group the new submenu should be in, or -1 for none.
	   GuiEd_MenuBar.setMenuItemSubmenuState(1,1,true);
	    GuiEd_MenuBar.addSubmenuItem(1,1,"Sub",0,"",-1);
	*/
	$GuiEdMenuCallback[%menuTarget,%submenuItemId,%submenuItemText] = getField($GuiEdMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);

	if (%accelerator !$="") {
		%callBack =  getField( $GuiEdMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
	}

	GuiEd_MenuBar.addSubmenuItem(%menuTarget,%menuItem,%submenuItemText,%submenuItemId,%accelerator,%checkGroup);
}
//------------------------------------------------------------------------------




//==============================================================================
// LabMenu Callbacks
//==============================================================================

//==============================================================================
/// GuiEd_MenuBar::onMenuItemSelect Called whenever an item in a menu is selected.
/// Parameters:
/// %menuId 	Index id of the menu which contains the selected menu item
/// %menuText 	Text of the menu which contains the selected menu item
/// %menuItemId 	Index id of the selected menu item
/// %menuItemText 	Text of the selected menu item
function GuiEd_MenuBar::onMenuItemSelect(%this,%menuId,%menuText,%menuItemId,%menuItemText) {
	logd("GuiEd_MenuBar::onMenuItemSelect(%this,%menuId,%menuText,%menuItemId,%menuItemText)",%this,%menuId,%menuText,%menuItemId,%menuItemText);		
	%callBack =  $GuiEdMenuCallback[%menuId,%menuItemId,%menuItemText];
	
	eval(%callBack);
}
//------------------------------------------------------------------------------

//==============================================================================
/// GuiEd_MenuBar::onMenuSelect Called whenever a menu is selected.
/// Parameters:
/// menuId 	Index id of the clicked menu
/// menuText 	Text of the clicked menu
function GuiEd_MenuBar::onMenuSelect(%this,%menuId,%menuText) {	
	//devLog("GuiEd_MenuBar::onMenuSelect(%this,%menuId,%menuText)",%this,%menuId,%menuText);
}
//------------------------------------------------------------------------------
//==============================================================================
function GuiEd_MenuBar::onMouseInMenu(%this,%isInMenu) {
	/* Called whenever the mouse enters, or persists is in the menu.

	Parameters:
	isInMenu 	True if the mouse has entered the menu, otherwise is false.
	Note:
	To receive this callback, call setProcessTicks(true) on the menu bar.

	*/
}
//------------------------------------------------------------------------------

//==============================================================================
/// GuiEd_MenuBar::onSubmenuSelect Called whenever a submenu is selected.
/// Parameters:
/// submenuId 	Index id of the clicked submenu
/// submenuText 	Text of the clicked submenu
function GuiEd_MenuBar::onSubmenuSelect(%this,%submenuId,%submenuText) {
	//devLog("GuiEd_MenuBar::onSubmenuSelect(%this,%submenuId,%submenuText)",%this,%submenuId,%submenuText);
}
//------------------------------------------------------------------------------
