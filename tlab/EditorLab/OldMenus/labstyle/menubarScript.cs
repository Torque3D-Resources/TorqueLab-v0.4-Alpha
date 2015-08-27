//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
/*
 Lab.ActiveMenu.addMenu("MyFirstMenu",0);
   Lab.ActiveMenu.addMenuItem(0,"MyFirstItem",0,"Ctrl numpad0",-1);
   Lab.ActiveMenu.addSubmenuItem("MyFirstMenu","MyFirstItem","MyFirstSubItem",0,"",-1);
   */

//==============================================================================
function Lab::setActiveMenu(%this,%menu) {
	Lab.activeMenu = %menu;
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::addMenuBind(%this,%bind,%command) {
	EditorMap.bindCmd(keyboard, %bind, %command, "");
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::addMenu(%this,%group,%id) {
	if (%id $= "") %id = 1;

	Lab.ActiveMenu.addMenu(%group,%id);
	Lab.ActiveMenu.menuList = trim(Lab.ActiveMenu.menuList SPC %group);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::addMenuItemData(%this,%menuId,%menuItemId,%data) {
	
	%menuItemText = getField(%data,0);
	%accelerator = getField(%data,1);
	%callback = getField(%data,2);
	%toggler = getField(%data,3);
	%checkGroup = getField(%data,4);
	if (%checkGroup $= "")
		%checkGroup = "-1";
	
	devLog(%menuId SPC %menuItemId,"Text=",%menuItemText,"%accelerator=",%accelerator,"%callback=",%callback,"%toggler=",%toggler);	
	$LabMenuCallback[%menuId,%menuItemId,%menuItemText] = %callback;
	$LabMenuIsSubMenu[%menuId,%menuItemId,%menuItemText] = false;
	if (%accelerator !$="") {		
		%this.addMenuBind(%accelerator,%callBack);
	}

	Lab.addMenuItem(%menuId,%menuItemText,%menuItemId,%accelerator,%checkGroup);
	//Lab.ActiveMenu.menuItemList[%menuId] = trim(GuiEd_MenuBar.menuItemList[%menuId] SPC %menuItemText);	
	
	if ( %toggler !$= ""){
		$LabMenuToggle[%menuId,%menuItemId,%menuItemText] = %toggler;
		eval("%checked = "@%toggler);
		Lab.ActiveMenu.setMenuItemChecked(%menuId,%menuItemId,%checked);
	}
	
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::addSubMenuItemData(%this,%menuId,%menuItemId,%submenuItemId,%data) {
	
	%submenuItemText = getField(%data,0);
	%accelerator = getField(%data,1);
	%callback = getField(%data,2);
	%toggler = getField(%data,3);
	%checkGroup = getField(%data,4);
	if (%checkGroup $= "")
		%checkGroup = "-1";
	
	devLog(%menuId SPC %menuItemId,"Text=",%submenuItemText,"%accelerator=",%accelerator,"%callback=",%callback,"%toggler=",%toggler);	
	devLog("%callback=",%callback,"%toggler=",%toggler,"Group",%checkGroup);	
	$LabMenuCallback[%menuId,%submenuItemId,%submenuItemText] = %callback;
	$LabMenuIsSubMenu[%menuId,%submenuItemId,%submenuItemText] = true;
	
	$LabSubMenuItemId[%menuId,%submenuItemId,%submenuItemText] = %menuItemId;
	
	if (%accelerator !$="") {		
		%this.addMenuBind(%accelerator,%callBack);
	}
	//Lab.setMenuItemSubmenuState(%menuId,%menuItemId,true);
	devLog("SubmenuId",%submenuItemId,"Text",%submenuItemText);
	Lab.addSubmenuItem(%menuId,%menuItemId,%submenuItemText,%submenuItemId,%accelerator,%checkGroup);
	Lab.setMenuItemBitmap(%menuId,%menuItemId,3);
	//Lab.subMenuItemList[%menuId,%menuItemId] = trim(GuiEd_MenuBar.subMenuItemList[%menuId,%menuItemId] SPC %submenuItemText);	
	
	if ( %toggler !$= ""){
		$LabMenuToggle[%menuId,%submenuItemId,%submenuItemText] = %toggler;
		eval("%checked = "@%toggler);
		devLog("Toggler MenuId:",%menuId,"ItemId",%menuItemId,"%menuItemText",%submenuItemText,"%newMenuItemId",%checked);	
		Lab.ActiveMenu.setSubmenuItemChecked(%menuId,%menuItemId,%submenuItemText,%checked);
	}
	
}
//------------------------------------------------------------------------------

//==============================================================================
// LabMenu Callbacks
//==============================================================================

//==============================================================================
/// LabMenu::onMenuItemSelect Called whenever an item in a menu is selected.
/// Parameters:
/// %menuId 	Index id of the menu which contains the selected menu item
/// %menuText 	Text of the menu which contains the selected menu item
/// %menuItemId 	Index id of the selected menu item
/// %menuItemText 	Text of the selected menu item
function LabMenu::onMenuItemSelect(%this,%menuId,%menuText,%menuItemId,%menuItemText) {
	//devLog("LabMenu::onMenuItemSelect(%this,%menuId,%menuText,%menuItemId,%menuItemText)",%this,%menuId,%menuText,%menuItemId,%menuItemText);
	

	%callBack =  $LabMenuCallback[%menuId,%menuItemId,%menuItemText];
	eval(%callBack);
	
	%isSubmenu = $LabMenuIsSubMenu[%menuId,%menuItemId,%menuItemText];
	if (%isSubmenu)
		devLog("This a subgfdgmenu of",%this.SubMenuText);
	

	%toggler = $LabMenuToggle[%menuId,%menuItemId,%menuItemText];	
	if (%toggler !$= ""){
		eval("%checked = "@%toggler);
		devLog("%menuId,%menuText,%menuItemId,%menuItemText:",%menuId,%menuText,%menuItemId,%menuItemText);
		//Lab.setMenuItemBitmap(%menuId,%menuItemId,3);
		if (%isSubmenu){
			%newMenuItemId = $LabSubMenuItemId[%menuId,%menuItemId,%menuItemText];
			devLog("Toggler MenuId:",%menuId,"ItemId",%menuItemId,"%menuItemText",%menuItemText,"%newMenuItemId",%newMenuItemId);	
			Lab.ActiveMenu.setSubMenuItemChecked(%menuId,%newMenuItemId,%menuItemText,%checked);
			return;
		}
		
		Lab.setMenuItemChecked(%menuId,%menuItemId,%checked);
		
		if (!%checked)
			Lab.ActiveMenu.setMenuItemBitmap(%menuId,%menuItemId,1);
		else
			Lab.ActiveMenu.setMenuItemBitmap(%menuId,%menuItemId,2);
	}
			
	
}
//------------------------------------------------------------------------------
//Lab.ActiveMenu.setSubMenuItemChecked(6,0,"DatablockEditor",true);
//==============================================================================
/// LabMenu::onMenuSelect Called whenever a menu is selected.
/// Parameters:
/// menuId 	Index id of the clicked menu
/// menuText 	Text of the clicked menu
function LabMenu::onMenuSelect(%this,%menuId,%menuText) {	
	//devLog("LabMenu::onMenuSelect(%this,%menuId,%menuText)",%this,%menuId,%menuText);
	%this.SubMenuText = "";
}
//------------------------------------------------------------------------------
//==============================================================================
function LabMenu::onMouseInMenu(%this,%isInMenu) {
	/* Called whenever the mouse enters, or persists is in the menu.

	Parameters:
	isInMenu 	True if the mouse has entered the menu, otherwise is false.
	Note:
	To receive this callback, call setProcessTicks(true) on the menu bar.

	*/
}
//------------------------------------------------------------------------------

//==============================================================================
/// LabMenu::onSubmenuSelect Called whenever a submenu is selected.
/// Parameters:
/// submenuId 	Index id of the clicked submenu
/// submenuText 	Text of the clicked submenu
function LabMenu::onSubmenuSelect(%this,%submenuId,%submenuText) {
	//devLog("LabMenu::onSubmenuSelect(%this,%submenuId,%submenuText)",%this,%submenuId,%submenuText);
	%this.SubMenuText = %submenuText;
}
//------------------------------------------------------------------------------
//==============================================================================
// LabMenu Cloned Functions
//==============================================================================
//==============================================================================
function Lab::addMenuItem(%this,%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup) {
	/*Adds a menu item to the specified menu. The menu argument can be either the text of a menu or its id.

	Parameters:
	%menu 	Menu name or menu Id to add the new item to.
	%menuItemText 	Text for the new menu item.
	%menuItemId 	Id for the new menu item.
	%accelerator 	Accelerator key for the new menu item.
	%checkGroup 	Check group to include this menu item in.*/
	/*$LabMenuCallback[%menu,%menuItemId,%menuItemText] = getField($LabMenuItem[%menu,%menuItemId],2);

	if (%accelerator !$="") {
		%callBack =  getField($LabMenuItem[%menu,%menuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
		Lab.ActiveMenu.setMenuItemBitmap
	}
*/
	Lab.ActiveMenu.addMenuItem(%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup);
	Lab.ActiveMenu.menuItemList[%menu] = strAddField(Lab.ActiveMenu.menuItemList[%menu],%menuItemText,true);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::addSubmenuItem(%this,%menuTarget,%menuItem,%submenuItemText,%submenuItemId,%accelerator,%checkGroup) {
	/*Adds a menu item to the specified menu. The menu argument can be either the text of a menu or its id.

	   Parameters:
	   %menuTarget 	Menu to affect a submenu in
	   %menuItem 	Menu item to affect
	   %submenuItemText 	Text to show for the new submenu
	   %submenuItemId 	Id for the new submenu
	   %accelerator 	Accelerator key for the new submenu
	   %checkGroup 	Which check group the new submenu should be in, or -1 for none.
	   Lab.ActiveMenu.setMenuItemSubmenuState(1,1,true);
	    Lab.ActiveMenu.addSubmenuItem(1,1,"Sub",0,"",-1);
	*/
	/*$LabMenuCallback[%menuTarget,%submenuItemId,%submenuItemText] = getField($LabMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);

	if (%accelerator !$="") {
		%callBack =  getField( $LabMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
	}
*/
	Lab.ActiveMenu.addSubmenuItem(%menuTarget,%menuItem,%submenuItemText,%submenuItemId,%accelerator,%checkGroup);
	Lab.ActiveMenu.subMenuItemList[%menuTarget,%menuItem] = strAddField(Lab.ActiveMenu.subMenuItemList[%menuTarget,%menuItem],%submenuItemText,true);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::ClearMenus(%this) {
	Lab.ActiveMenu.clearMenus("","");
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::ClearMenusItems(%this,%menu) {
	Lab.ActiveMenu.ClearMenusItems(%menu);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::removeMenu(%this,%menu) {
	Lab.ActiveMenu.removeMenu(%menu);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::removeMenuItem(%this,%menu,%item) {
	Lab.ActiveMenu.removeMenuItem(%menu,%item);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setCheckmarkBitmapIndex(%this,%index) {
	Lab.ActiveMenu.setCheckmarkBitmapIndex(%index);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuBitmapIndex(%this,%menuTarget,%bitmapindex,%bitmaponly,%drawborder) {
	/* Sets the bitmap index for the menu and toggles rendering only the bitmap.

	Parameters:
	%menuTarget 	Menu to affect
	%bitmapindex 	Bitmap index to set for the menu
	%bitmaponly 	If true, only the bitmap will be rendered
	%drawborder 	If true, a border will be drawn around the menu.*/
	Lab.ActiveMenu.setMenuBitmapIndex(%menuTarget,%bitmapindex,%bitmaponly,%drawborder);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuItemBitmap(%this,%menuTarget,%menuItem,%bitmapIndex) {
	/* Sets the specified menu item bitmap index in the bitmap array. Setting the item's index to -1 will remove any bitmap.

	Parameters:
	%menuTarget 	Menu to affect the menuItem in
	%menuItem 	Menu item to affect
	%bitmapIndex 	Bitmap index to set the menu item to
	*/
	Lab.ActiveMenu.setMenuItemBitmap(%menuTarget,%menuItem,%bitmapIndex);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setMenuItemChecked(%this,%menuTarget,%menuItem,%checked) {
	/* Sets the menu item bitmap to a check mark, which by default is the first element in the bitmap array (although this may be changed with setCheckmarkBitmapIndex()). Any other menu items in the menu with the same check group become unchecked if they are checked.

	Parameters:
	%menuTarget 	Menu to work in
	%menuItem 	Menu item to affect
	%checked 	Whether we are setting it to checked or not

	*/
	Lab.ActiveMenu.setMenuItemChecked(%menuTarget,%menuItem,%checked);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setMenuItemEnable(%this,%menuTarget,%menuItemTarget,%enabled) {
	/* sets the menu item to enabled or disabled based on the enable parameter. The specified menu and menu item can either be text or ids.

	Detailed description

	Parameters:
	%menuTarget 	Menu to work in
	%menuItemTarget 	The menu item inside of the menu to enable or disable
	%enabled 	Boolean enable / disable value.

	*/
	Lab.ActiveMenu.setMenuItemEnable(%menuTarget,%menuItemTarget,%enabled);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuItemSubmenuState(%this,%menuTarget,%menuItem,%isSubmenu) {
	/* Sets the given menu item to be a submenu.

	Parameters:
	%menuTarget 	Menu to affect a submenu in
	%menuItem 	Menu item to affect
	%isSubmenu 	Whether or not the menuItem will become a subMenu or not
	Lab.ActiveMenu.setMenuItemSubmenuState(0,2,true);
	*/
	Lab.ActiveMenu.setMenuItemSubmenuState(%menuTarget,%menuItem,%isSubmenu);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuItemText(%this,%menuTarget,%menuItem,%newMenuItemText) {
	/* Sets the text of the specified menu item to the new string.

	Parameters:
	%menuTarget 	Menu to affect
	%menuItem 	Menu item in the menu to change the text at
	%newMenuItemText 	New menu text

	*/
	Lab.ActiveMenu.setMenuItemText(%menuTarget,%menuItem,%newMenuItemText);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuItemVisible(%this,%menuTarget,%menuItem,%isVisible) {
	/* Brief Description.

	Detailed description

	Parameters:
	menuTarget 	Menu to affect the menu item in
	menuItem 	Menu item to affect
	%isVisible 	Visible state to set the menu item to.

	*/
	Lab.ActiveMenu.setMenuItemVisible(%menuTarget,%menuItem,%isVisible);
}
//------------------------------------------------------------------------------


//==============================================================================
function Lab::setMenuMargins(%this,%horizontalMargin,%verticalMargin,%bitmapToTextSpacing) {
	/*
	setMenuMargins(%this,%horizontalMargin,%verticalMargin,%bitmapToTextSpacing)
	Sets the menu rendering margins: horizontal, vertical, bitmap spacing.

	Detailed description

	Parameters:
	%horizontalMargin 	Number of pixels on the left and right side of a menu's text.
	%verticalMargin 	Number of pixels on the top and bottom of a menu's text.
	%bitmapToTextSpacing 	Number of pixels between a menu's bitmap and text.

	*/
	Lab.ActiveMenu.setMenuMargins(%horizontalMargin,%verticalMargin,%bitmapToTextSpacing);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setMenuText(%this,%menuTarget,%newMenuText) {
	/* Sets the text of the specified menu to the new string.

	Parameters:
	menuTarget 	Menu to affect
	%newMenuText 	New menu text


	*/
	Lab.ActiveMenu.setMenuText(%menuTarget,%newMenuText);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setMenuVisible(%this,%menuTarget,%visible) {
	/* Sets the whether or not to display the specified menu.

	Parameters:
	menuTarget 	Menu item to affect
	%visible 	Whether the menu item will be visible or not



	*/
	Lab.ActiveMenu.setMenuVisible(%menuTarget,%visible);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setSubmenuItemChecked(%this,%menuTarget,%menuItem,%submenuItemText,%checked) {
	/* Sets the menu item bitmap to a check mark, which by default is the first element in the bitmap array (although this may be changed with setCheckmarkBitmapIndex()). Any other menu items in the menu with the same check group become unchecked if they are checked.

	Parameters:
	%menuTarget 	Menu to affect a submenu in
	%menuItem 	Menu item to affect
	%submenuItemText 	Text to show for submenu
	%checked 	Whether or not this submenu item will be checked.



	*/
	Lab.ActiveMenu.setSubmenuItemChecked(%menuTarget,%menuItem,%submenuItemText,%checked);
}
//------------------------------------------------------------------------------



