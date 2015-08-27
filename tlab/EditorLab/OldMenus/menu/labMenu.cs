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
			%itemData = $GuiEdMenuItem[%menuId,%menuItemId];
			
			GuiEditCanvas.addMenuItemData( %menuId,%menuItemId,%itemData);
		
			//GuiEditCanvas.addMenuItem( %menuId, getField(%item,0),%menuItemId,getField(%item,1),%checkId,%item);
			%subMenuItemId = 0;

			while($GuiEdSubMenuItem[%menuId,%menuItemId,%subMenuItemId] !$= "") {				
				%subItemData = $GuiEdSubMenuItem[%menuId,%menuItemId,%subMenuItemId] ;				
				GuiEditCanvas.addSubMenuItemData( %menuId,%menuItemId,%subMenuItemId,%subItemData);
				//GuiEditCanvas.addSubmenuItem(%menuId,%menuItemId,getField(%subitem,0),%subMenuItemId,getField(%subitem,1),-1);
				%subMenuItemId++;
			}

			%menuItemId++;
		}

		%menuId++;
	}
	GuiEd_MenuBar.setCheckmarkBitmapIndex(2);
	GuiEdMenu.setMenuMargins("7","0","18");
}
//------------------------------------------------------------------------------

//==============================================================================
function GuiEditCanvas::addMenuBind(%this,%bind,%command) {
	GuiEdMap.bindCmd(keyboard, %bind, %command, "");
}
//------------------------------------------------------------------------------
/*
	setMenuMargins(%this,%horizontalMargin,%verticalMargin,%bitmapToTextSpacing)
	Sets the menu rendering margins: horizontal, vertical, bitmap spacing.

	Detailed description

	Parameters:
	%horizontalMargin 	Number of pixels on the left and right side of a menu's text.
	%verticalMargin 	Number of pixels on the top and bottom of a menu's text.
	%bitmapToTextSpacing 	Number of pixels between a menu's bitmap and text.

	*/
//==============================================================================
function GuiEditCanvas::addMenu(%this,%group,%id) {
	if (%id $= "") %id = 1;

	GuiEd_MenuBar.addMenu(%group,%id);
	GuiEd_MenuBar.menuList = trim(GuiEd_MenuBar.menuList SPC %group);
	GuiEd_MenuBar.setMenuBitmapIndex(%id,3,false,false);
	
}
//------------------------------------------------------------------------------
//Menu 0 = normal 2 = hover 1 = open
//Item 0 = check 1 = hover
//setCheckmarkBitmapIndex (int bitmapindex)
 	//Sets the menu bitmap index for the check mark image. 
 	
//setMenuBitmapIndex (string menuTarget, int bitmapindex, bool bitmaponly, bool drawborder)
 	//Sets the bitmap index for the menu and toggles rendering only the bitmap. 
 	
//setMenuItemBitmap (string menuTarget, string menuItemTarget, int bitmapIndex)
	//Sets the specified menu item bitmap index in the bitmap array. Setting the item's index to -1 will remove any bitmap. 
//==============================================================================
function GuiEditCanvas::addMenuItemData(%this,%menuId,%menuItemId,%data) {
	
	%menuItemText = getField(%data,0);
	%accelerator = getField(%data,1);
	%callback = getField(%data,2);
	%toggler = getField(%data,3);
	%checkGroup = getField(%data,4);
	if (%checkGroup $= "")
		%checkGroup = "-1";
	

	//devLog(%menuId SPC %menuItemId,"Text=",%menuItemText,"%accelerator=",%accelerator,"%callback=",%callback,"%toggler=",%toggler);	
	$GuiEdMenuCallback[%menuId,%menuItemId,%menuItemText] = %callback;
	$GuiEdIsSubMenu[%menuId,%menuItemId,%menuItemText] = false;
	if (%accelerator !$="") {		
		%this.addMenuBind(%accelerator,%callBack);
	}

	GuiEd_MenuBar.addMenuItem(%menuId,%menuItemText,%menuItemId,%accelerator,%checkGroup);
	GuiEd_MenuBar.menuItemList[%menuId] = trim(GuiEd_MenuBar.menuItemList[%menuId] SPC %menuItemText);	
		GuiEd_MenuBar.setMenuItemBitmap(%menuId,%menuItemId,0);

	if ( %toggler !$= ""){		
		
		$GuiEdMenuToggle[%menuId,%menuItemId,%menuItemText] = %toggler;
		eval("%checked = "@%toggler);
		GuiEd_MenuBar.setMenuItemChecked(%menuId,%menuItemId,%checked);
	}
	else {		
		GuiEd_MenuBar.setMenuItemChecked(%menuId,%menuItemId,true);
	}
	
}
//------------------------------------------------------------------------------
//setMenuItemBitmap = menu item invidually 
//==============================================================================
function GuiEditCanvas::addSubMenuItemData(%this,%menuId,%menuItemId,%submenuItemId,%data) {
	
	%submenuItemText = getField(%data,0);
	%accelerator = getField(%data,1);
	%callback = getField(%data,2);
	%toggler = getField(%data,3);
	%checkGroup = getField(%data,4);
	if (%checkGroup $= "")
		%checkGroup = "-1";
	
	//GuiEd_MenuBar.setMenuItemSubmenuState(%menuId,%menuItemId,true);
	//devLog(%menuId SPC %menuItemId,"Text=",%menuItemText,"%accelerator=",%accelerator,"%callback=",%callback,"%toggler=",%toggler);	
	$GuiEdMenuCallback[%menuId,%submenuItemId,%submenuItemText] = %callback;
	$GuiEdIsSubMenu[%menuId,%submenuItemId,%submenuItemText] = true;
	if (%accelerator !$="") {		
		%this.addMenuBind(%accelerator,%callBack);
	}
	devLog("SubmenuId",%submenuItemId,"Text",%submenuItemText);
	GuiEd_MenuBar.addSubmenuItem(%menuId,%menuItemId,%submenuItemText,%submenuItemId,%accelerator,%checkGroup);
	GuiEd_MenuBar.subMenuItemList[%menuId,%menuItemId] = trim(GuiEd_MenuBar.subMenuItemList[%menuId,%menuItemId] SPC %submenuItemText);	
	GuiEd_MenuBar.setMenuItemBitmap(%menuId,%menuItemId,3);

	if ( %toggler !$= ""){
		$GuiEdMenuToggle[%menuId,%submenuItemId,%submenuItemText] = %toggler;
		eval("%checked = "@%toggler);		
		GuiEd_MenuBar.setSubmenuItemChecked(%menuId,%menuItemId,%submenuItemText,%checked);
		
	}
	else{
		GuiEd_MenuBar.setSubmenuItemChecked(%menuId,%menuItemId,%submenuItemText,true);
	}
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
	devLog("GuiEd_MenuBar::onMenuItemSelect(%this,%menuId,%menuText,%menuItemId,%menuItemText)",%this,%menuId,%menuText,%menuItemId,%menuItemText);		
	%callBack =  $GuiEdMenuCallback[%menuId,%menuItemId,%menuItemText];
	
	%isSubmenu = $GuiEdIsSubMenu[%menuId,%menuItemId,%menuItemText];
	if (%isSubmenu)
		devLog("This a submenu of",GuiEd_MenuBar.SubMenuText);
	devLog(GuiEd_MenuBar.menuType, "callback:",%callBack);
	
	eval(%callBack);
	
	%toggler = $GuiEdMenuToggle[%menuId,%menuItemId,%menuItemText];
	devLog(GuiEd_MenuBar.menuType, "%toggler:",%toggler);
	if (%toggler !$= ""){
		eval("%checked = "@%toggler);
		devLog("Check toggle source:",%toggler,"Result",%checked,"DoSub",%isSubmenu);
		if (%isSubmenu)
			GuiEd_MenuBar.setSubMenuItemChecked(%menuId,%menuItemId,%menuItemText,%checked);
		else
			GuiEd_MenuBar.setMenuItemChecked(%menuId,%menuItemId,%checked);
	}
}
//------------------------------------------------------------------------------

//==============================================================================
/// GuiEd_MenuBar::onMenuSelect Called whenever a èmenu is selected.
/// Parameters:
/// menuId 	Index id of the clicked menu
/// menuText 	Text of the clicked menu
function GuiEd_MenuBar::onMenuSelect(%this,%menuId,%menuText) {	
	devLog("GuiEd_MenuBar::onMenuSelect(%this,%menuId,%menuText)",%this,%menuId,%menuText);
	GuiEd_MenuBar.SubMenuText = "";
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
	devLog("GuiEd_MenuBar::onSubmenuSelect(%this,%submenuId,%submenuText)",%this,%submenuId,%submenuText);
	GuiEd_MenuBar.SubMenuText = %submenuText;
	
}
//------------------------------------------------------------------------------
//GuiEd_MenuBar.setMenuItemChecked(%menuTarget,%menuItem,%checked);
/*
//==============================================================================
function Lab::setMenuItemSubmenuState(%this,%menuTarget,%menuItem,%isSubmenu) {
 Sets the given menu item to be a submenu.

	Parameters:
	%menuTarget 	Menu to affect a submenu in
	%menuItem 	Menu item to affect
	%isSubmenu 	Whether or not the menuItem will become a subMenu or not

	
	GuiEd_MenuBar.setMenuItemSubmenuState(%menuTarget,%menuItem,%isSubmenu);
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
	/*$GuiEdMenuCallback[%menuTarget,%submenuItemId,%submenuItemText] = getField($GuiEdMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);

	if (%accelerator !$="") {
		%callBack =  getField( $GuiEdMenuSubMenuItem[%menuTarget,%menuItem,%submenuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
	}

	GuiEd_MenuBar.addSubmenuItem(%menuTarget,%menuItem,%submenuItemText,%submenuItemId,%accelerator,%checkGroup);
}
//------------------------------------------------------------------------------
//==============================================================================
function GuiEditCanvas::addMenuItem(%this,%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup,%data) {
	/*Adds a menu item to the specified menu. The menu argument can be either the text of a menu or its id.

	Parameters:
	%menu 	Menu name or menu Id to add the new item to.
	%menuItemText 	Text for the new menu item.
	%menuItemId 	Id for the new menu item.
	%accelerator 	Accelerator key for the new menu item.
	%checkGroup 	Check group to include this menu item in.*//*
	$GuiEdMenuCallback[%menu,%menuItemId,%menuItemText] = getField($GuiEdMenuItem[%menu,%menuItemId],2);
	devLog("FUllData=",$GuiEdMenuItem[%menu,%menuItemId]);
	
	if (%checkGroup !$= "-1")
	devLog("GuiEd_MenuBar::addMenuItem(%this,%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup,%data)",%this,%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup,%data);		
		

	if (%accelerator !$="") {
		%callBack =  getField($GuiEdMenuItem[%menu,%menuItemId],2);
		%this.addMenuBind(%accelerator,%callBack);
	}

	GuiEd_MenuBar.addMenuItem(%menu,%menuItemText,%menuItemId,%accelerator,%checkGroup);
	GuiEd_MenuBar.menuItemList[%menu] = trim(GuiEd_MenuBar.menuItemList[%menu] SPC %menuItemText);
	%toggleSource =  getField($GuiEdMenuItem[%menu,%menuItemId],3);
	if ( %toggleSource !$= ""){
		devLog("Menu Item use Toggle Check",%menuItemText);
		$GuiEdMenuToggle[%menu,%menuItemId] = %toggleSource;
		eval("%checked = "@%toggleSource);
		GuiEd_MenuBar.setMenuItemChecked(%menu,%menuItemId,%checked);
	}
}
//------------------------------------------------------------------------------

*/