//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Initialize the Toolbar
//==============================================================================

//==============================================================================
function Lab::initAllToolbarGroups(%this) {
	EGuiCustomizer-->saveToolbar.active = false;

	if (!isObject(EToolbarDisabledDrop)) {
		%new =  new GuiContainer(EToolbarDisabledDrop) {
			horizSizing = "width";
			vertSizing = "height";
			profile = "ToolsDefaultProfile_NoModal";
		};
		EditorGuiToolbar.add(%new);
	}

	EditorGuiToolbar.pushToBack(EToolbarDisabledDrop);
	EToolbarDisabledDrop.fitIntoParents();
	hide(EToolbarDisabledDrop);

	foreach(%gui in LabToolbarGuiSet) {
		Lab.initToolbarGroup(%gui);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::initToolbarGroup(%this,%gui) {
	if (%gui.plugin $= "")
		%gui.plugin = strreplace(%gui.getName(),"Toolbar","");

	%pluginObj = %gui.plugin@"Plugin";
	%pluginObj.iconList = "";
	%pluginObj.toolbarGui = %gui;
	%pluginName = %pluginObj.plugin;

	if (%gui.plugin !$= %pluginName && %gui.plugin !$= "")
		%gui.plugin = "";

	//if (!isObject(%pluginObj))
	//warnLog("Can't find the plugin obj for gui:",%gui,%gui.getName());
	%defaultStack = %gui;

	if (%gui.getClassName() !$= "GuiStackControl")
		%defaultStack = %gui.getObject(0);

	%defaultStack.superClass = "ToolbarPluginStack";
	%gui.defaultStack = %defaultStack;
	%disabledDropContainer = EToolbarDisabledDrop.findObjectByInternalName(%gui.getName());

	if (!isObject(%disabledDropContainer)) {
		%disabledDropContainer =  new GuiContainer() {
			extent = %gui.extent;
			horizSizing = "width";
			profile = "ToolsFillDarkA";
			internalName = %gui.getName();
			superClass = "PluginToolbarEnd";
		};
		EToolbarDisabledDrop.add(%disabledDropContainer);
	}

	LabGeneratedSet.add(EToolbarDisabledDrop); //Dont save those with EditorGui
	%disabledDropContainer.profile = "ToolsFillDarkA_T50";
	%disabledDropContainer.pluginName = %pluginName;
	%disabledDropContainer.setPosition(%gui.position.x,%gui.position.y);
	hide(%disabledDropContainer);
	Lab.getStackEndGui(%gui,false);
	%disabled = %gui.defaultStack-->DisabledIcons;

	if (!isObject(%disabled)) {
		%disabled =  new GuiContainer() {
			extent = "84 30";
			profile = "ToolsDefaultProfile";
			internalName = "DisabledIcons";
			visible = 0;
		};
		%defaultStack.add(%disabled);
	}

	%gui.disabledGroup = %disabled;
	%defaultStack.pushToBack(%end);
	%pluginObj.toolbarGroups = "";

	foreach(%ctrl in %defaultStack)
		if (%ctrl.getClassName() $= "GuiStackControl")
			%pluginObj.toolbarGroups = strAddWord(%pluginObj.toolbarGroups,%ctrl.getId());

	%gui.stackLists = "";
	%this.scanPluginToolbarGroup(%defaultStack,"",%pluginObj,%gui);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::scanPluginToolbarGroup(%this,%group,%level,%pluginObj,%gui) {
	%pluginText = "";

	if (isObject(%pluginObj)) {
		%pluginName = %pluginObj.plugin;
	}

	foreach(%ctrl in %group) {
		if (%pluginName !$= "")
			%ctrl.pluginName = %pluginName;

		if (%ctrl.isMemberOfClass("GuiStackControl")) {
			Lab.getStackEndGui(%ctrl,true);
			%gui.stackLists = strAddWord(%gui.stackLists,%ctrl.getId(),true);
			%this.scanPluginToolbarStack(%ctrl,%pluginObj,%gui);
			//%this.scanPluginToolbarGroup(%ctrl,%level++,%pluginObj,%gui);
			continue;
		}

		if (%ctrl.isMemberOfClass("GuiIconButtonCtrl")) {
			%ctrl.superClass = "ToolbarIcon";
			%ctrl.useMouseEvents = true;
			%ctrl.toolbar = %pluginObj.toolbarGui;
			%pluginObj.iconList = strAddWord(%pluginObj.iconList,%ctrl.getId());
		}

		continue;

		if (%ctrl.isMemberOfClass("GuiContainer")) {
			%this.scanPluginToolbarGroup(%ctrl,%level++,%pluginObj,%gui);
			continue;
		}

		if (%ctrl.isMemberOfClass("GuiMouseEventCtrl")) {
			%ctrl.superClass = "ToolbarGroup";
			%this.scanPluginToolbarGroup(%ctrl,%level++,%pluginObj,%gui);
			continue;
		} else if (%group.isMemberOfClass("GuiStackControl")) {
			%ctrl.toolbar = %pluginObj.toolbarGui;
			%pluginObj.iconList = strAddWord(%pluginObj.iconList,%ctrl.getId());
		}
	}
}

//------------------------------------------------------------------------------
//==============================================================================
function Lab::scanPluginToolbarStack(%this,%stack,%pluginObj,%gui) {
	%pluginText = "";

	if (isObject(%pluginObj)) {
		%pluginName = %pluginObj.plugin;
	}

	foreach(%ctrl in %stack) {
		if (%pluginName !$= "")
			%ctrl.pluginName = %pluginName;

		%ctrl.toolbar = %gui;

		if (%ctrl.isMemberOfClass("GuiContainer")) {
			%dropTarget = %stack.getObjectIndex(%ctrl);

			foreach(%subCtrl in %ctrl) {
				if (%subCtrl.isMemberOfClass("GuiMouseEventCtrl")) {
					%subCtrl.internalName = "MouseStart";
					%subCtrl.superClass = "MouseBox";
					%subCtrl.dropTarget = %dropTarget;
					%subCtrl.pluginName = %pluginName;
				}
			}

			continue;
		}

		if (%ctrl.isMemberOfClass("GuiIconButtonCtrl")) {
			%ctrl.superClass = "ToolbarIcon";
			%ctrl.useMouseEvents = true;

			//%ctrl.toolbar = %gui;
			if (isObject(%pluginObj)) {
				%pluginObj.iconList = strAddWord(%pluginObj.iconList,%ctrl.getId());
			}
		}
	}
}

//------------------------------------------------------------------------------
//==============================================================================
function Lab::getStackEndGui(%this,%gui,%isSubStack) {
	if (%isSubStack) {
		%end = %gui-->StackEnd;
		delObj(%end);
		%end = %gui-->SubStackEnd;
		delObj(%end);

		if (!isObject(%end)) {
			%end = new GuiContainer() {
				extent = "18 32";
				profile = "ToolsBoxDarkC";
				isContainer = "1";
				internalName = "SubStackEnd";
				superClass = "SubStackEnd";
				new GuiBitmapCtrl() {
					bitmap = "tlab/gui/icons/toolbar-assets/DropLeft.png";
					wrap = "0";
					position = "4 7";
					extent = "12 16";
					internalName = "SubStackEndImg";
					superClass = "SubStackEndImg";
				};
			};
			%gui.add(%end);
		}

		%end.setExtent(18,32);
		hide(%end);
	} else {
		%end = %gui-->StackEnd;
		delObj(%end);

		if (!isObject(%end)) {
			%end = new GuiContainer() {
				extent = "27 32";
				profile = "ToolsBoxDarkC";
				isContainer = "1";
				internalName = "StackEnd";
				superClass = "StackEnd";
				new GuiBitmapCtrl() {
					bitmap = "tlab/gui/icons/toolbar-assets/DropLeft.png";
					wrap = "0";
					position = "6 5";
					extent = "17 22";
					internalName = "StackEndImg";
					superClass = "StackEndImg";
				};
			};
			%gui.add(%end);
		}

		%end.setExtent(27,32);
		hide(%end);
	}
}
//------------------------------------------------------------------------------

//==============================================================================
//==============================================================================
//==============================================================================
function Lab::toggleToolbarGroupVisible(%this,%groupId,%itemId) {
	%menu = Lab.currentEditor.toolbarMenu;
	%checked = %menu.isItemChecked(%itemId);
	toggleVisible(%groupId);
	%groupId.visible = !%checked;
	%menu.checkItem(%itemId,!%checked);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setToolbarDirty(%this,%isDirty) {
	EGuiCustomizer-->saveToolbar.active = %isDirty;
	Lab.toolbarIsDirty = %isDirty;
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::saveToolbar(%this,%icon,%disabled) {
	//EGuiCustomizer-->saveToolbar.active = false;
	//Lab.toolbarIsDirty = true;
	Lab.storePluginsToolbarState();
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::storePluginsToolbarState(%this) {
	if (!Lab.toolbarIsDirty)
		return;
	foreach(%gui in EditorGuiToolbarStack) {
		%file = %gui.getFilename();

		if (strFind(%file,"EditorGui.gui")) {
			warnLog("EditorGui found in file name:",%file);
			continue;
		}

		if (isFile(	%file))
			%gui.save(%file);
		else
			warnLog(%gui.getName()," saving skipped because file don't exist:",%file);
	}

	Lab.setToolbarDirty(false);
}
//------------------------------------------------------------------------------
//==============================================================================
// Show Popup Options
//==============================================================================
//==============================================================================
function Lab::rebuildAllToolbarPluginMenus(%this) {
	foreach(%gui in LabToolbarGuiSet) {
		%pluginObj = %gui.plugin@"Plugin";
		delObj(%pluginObj.toolbarMenu);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::ShowToolbarOptionMenu(%this) {
	%plugObj = Lab.currentEditor;

	if (!isObject(%plugObj.toolbarMenu)) {
		%menu = new PopupMenu() {
			superClass = "ContextMenu";
			isPopup = true;
			object = -1;
			profile = $LabCfg_ContextMenuProfile;
		};
		%itemId = 0;

		foreach$(%toolbarGroup in %plugObj.toolbarGroups) {
			%line = "Toggle" SPC %toolbarGroup.internalName TAB "" TAB "Lab.toggleToolbarGroupVisible("@%toolbarGroup.getId()@","@%itemId@");";
			%menu.addItem(%itemId,%line);
			%menu.groupItem[%itemId] = %toolbarGroup.getId();
			%menu.itemGroup[%toolbarGroup.getId()] = %itemId;

			if (%toolbarGroup.isVisible())
				%menu.checkItem(%itemId,true);
			else
				%menu.checkItem(%itemId,false);

			%itemId++;
		}

		%plugObj.toolbarMenu = %menu;
	}

	%plugObj.toolbarMenu.showPopup(Canvas);
}
//------------------------------------------------------------------------------
//==============================================================================
function ToolbarArea::onRightMouseDown(%this,%modifier,%point,%clicks) {
	devLog("ToolbarArea::onRightMouseDown(%this,%modifier,%point,%clicks) ",%this,%modifier,%point,%clicks);
	Lab.ShowToolbarOptionMenu();
}
//------------------------------------------------------------------------------
//==============================================================================
function ToolbarBoxChild::onRightMouseDown(%this,%modifier,%point,%clicks) {
	devLog("ToolbarBoxChild::onRightMouseDown(%this,%modifier,%point,%clicks) ",%this,%modifier,%point,%clicks);
	Lab.ShowToolbarOptionMenu();
}
//------------------------------------------------------------------------------
//==============================================================================
function ToolbarIcon::onRightClick(%this,%modifier,%point,%clicks) {
	devLog("ToolbarIcon::onRightClick(%this,%modifier,%point,%clicks) ",%this,%modifier,%point,%clicks);
	Lab.ShowToolbarOptionMenu();
}
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function Lab::resetAllDisabledIcons(%this) {
	foreach(%gui in EditorGuiToolbarStack) {
		%addToStack = "";
		%break = false;

		foreach(%ctrl in %gui) {
			if(%break)
				continue;

			if (%ctrl.getClassName() $= "GuiStackControl") {
				%addToStack = %ctrl;
				%break = true;
			}

			//if(%break)
			//break;
		}

		%disabledGroup = %gui-->DisabledIcons;

		if (!isObject(%disabledGroup) || !isObject(%addToStack))
			continue;

		for(%i=%disabledGroup.getCount()-1; %i >=0; %i--) {
			%ctrl = %disabledGroup.getObject(%i);
			%ctrl.locked = 0;
			%addToStack.add(%ctrl);
		}
	}
}
//------------------------------------------------------------------------------


//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function Lab::getIconRootStack(%this, %ctrl) {
	%test = %ctrl;
	for(%i=0;%i<8;%i++){
	
		%parent = %test.parentGroup;
		devLog("Test Parent Name = ",%parent.getName());

		if (%parent.getName() $= "EditorGuiToolbarStack" ) {
			%toolbar = %test;
			%i = 99;
		} else
			%test = %parent;	
	}

	if (isObject(%toolbar))
		return %toolbar;
	
	return false;
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::updateAllToolbarStacks(%this) {
	foreach(%gui in EditorGuiToolbarStack) {
		if (!%gui.isMemberOfClass("GuiStackControl")) 
			continue;
		
		%this.updateToolbarStack(%gui,true);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::updateToolbarStack(%this,%stack,%fixStructure) {	
		%stack.updateStack();
		
		if(!%fixStructure)
			return;
		
		%DisabledGroup = %stack->DisabledIcons;
		if (isObject(%DisabledGroup)){
			%stack.bringToFront(%DisabledGroup);
		}
		%StackEnd = %stack->StackEnd;
		if (isObject(%StackEnd)){
			%stack.pushToBack(%StackEnd);
		}
}
//------------------------------------------------------------------------------