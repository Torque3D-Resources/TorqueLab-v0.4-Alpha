//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Toolbar drag controls callbacks
//==============================================================================

//==============================================================================
// Start dragging a toolbar icon
function ToolbarIcon::onMouseDragged( %this,%a1,%a2,%a3 ) {	
	Lab.startToolbarDrag(%this,%a1,%a2,%a3);
}
//------------------------------------------------------------------------------
//==============================================================================
// Start dragging a toolbar icons group
function ToolbarBoxChild::onMouseDragged( %this,%a1,%a2,%a3 ) {	
	%src = %this.parentGroup;
	Lab.startToolbarDrag(%src,%a1,%a2,%a3);
}
//------------------------------------------------------------------------------

//==============================================================================
// Start dragging a toolbar icons group
function MouseStart::onMouseDragged( %this,%a1,%a2,%a3 ) {
	
	%group = %this.parentGroup.parentGroup;

	if (%group.getClassName() !$= "GuiStackControl")
		return;

	Lab.startToolbarDrag(%group,%a1,%a2,%a3);
}
//------------------------------------------------------------------------------
//==============================================================================
// OLD: Replaced with MouseStart
function ToolbarGroup::onMouseDragged( %this,%a1,%a2,%a3 ) {
	
	%group = %this.parentGroup.parentGroup;

	if (%group.getClassName() !$= "GuiStackControl")
		return;

	Lab.startToolbarDrag(%group,%a1,%a2,%a3);
}
//------------------------------------------------------------------------------
//==============================================================================
// Start dragging a toolbar icons group
function Lab::startToolbarDrag( %this,%ctrl,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("ToolbarBoxChild class is corrupted! Fix it!");
		return;
	}

	%callback = "Lab.onToolbarIconDroppedDefault";

	if (%ctrl.getClassName() $= "GuiStackControl")
		%callback = "Lab.onToolbarGroupDroppedDefault";

	startDragAndDropCtrl(%ctrl,"Toolbar",%callback);
	hide(%ctrl);
	Lab.showDisabledToolbarDropArea(	%ctrl);
}
//------------------------------------------------------------------------------


//==============================================================================

//==============================================================================
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::findObjectToolbarStack(%this,%checkObj,%defaultPluginStack) {
	%checkId = 0;

	while(%checkId < 5) {
		%result = %this.checkObjForToolbarStack(%checkObj,%defaultPluginStack);

		if (%result && %defaultPluginStack)
			return %checkObj SPC %previousObj;
		else if (%result)
			return %checkObj;

		%previousObj = %checkObj;
		%checkObj = %checkObj.parentGroup;
		%checkId++;
	}

	return false;
}
//------------------------------------------------------------------------------

//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::checkObjForToolbarStack(%this,%object,%defaultStack) {
	if (!isObject(%object))
		return false;

	if (%object.getClassname() $= "GuiStackControl") {
		if (%defaultStack && %object.superClass $= "ToolbarPluginStack")
			return true;
		else if (!%defaultStack && %object.superClass $= "ToolbarContainer")
			return true;
		else if (!%defaultStack && %object.superClass $= "PluginToolbarEnd")
			return true;
	}

	return false;
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::addToolbarItemToGroup(%this,%item,%group,%addBefore) {
	%divider = %group-->GroupDivider;

	if (isObject(%divider)) {
		%divider.extent.x = "6";

		foreach(%obj in %divider) {
			if (%obj.getClassName() $= "GuiMouseEventCtrl")
				%obj.extent.x = "6";
		}
	}

	%this.setToolbarIconDisabled(%item,false);
	%group.add(%item);
	%group.updateStack();
	%group.reorderChild(%item,%addBefore);
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::showDisabledToolbarDropArea(%this,%dragControl) {
	%isIcon = false;

	if (%dragControl.isMemberOfClass("GuiIconButtonCtrl"))
		%isIcon = true;

	EToolbarDisabledDrop.visible = 1;

	//Only show StackEnd for groups
	if(%isIcon) {
		foreach(%gui in LabToolbarGuiSet) {
			foreach$(%stack in %gui.stackLists) {
				%stackEnd = %stack-->SubStackEnd;

				if (!isObject(%stackEnd))
					continue;

				show(%stackEnd);
				%stack.pushToBack(%stackEnd);
				%stackEnd.profile = "ToolsBoxDarkC";
				%stackEnd.setExtent(18,%gui.parentGroup.extent.y);
			}
		}
	} else {
		foreach(%gui in LabToolbarGuiSet) {
			%stackEnd = %gui-->StackEnd;

			if (!isObject(%stackEnd))
				continue;

			%gui.pushToBack(%stackEnd);
			show(%stackEnd);
			%stackEnd.profile = "ToolsBoxDarkC";
			%stackEnd.setExtent(27,%gui.parentGroup.extent.y);
		}
	}

	foreach(%gui in EToolbarDisabledDrop) {
		if (%gui.pluginName !$= %dragControl.pluginName) {
			%src = %gui.internalName;

			if (%src.visible $= "0")
				continue;

			%gui.visible = 1;
			%gui.setExtent(%src.extent.x,EditorGuiToolbar.extent.y);
			%gui.setPosition(%src.position.x,0);
		}
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::hideDisabledToolbarDropArea(%this) {
	foreach(%gui in LabToolbarGuiSet) {
		%stackEnd = %gui-->StackEnd;

		if (isObject(%stackEnd))
			hide(%stackEnd);
		
		//foreach$(%stack in %gui.stackLists) {
		foreach(%subCtrl in %gui) {
			%subStackEnd = %subCtrl-->SubStackEnd;

			if (isObject(%subStackEnd))
				hide(%subStackEnd);
		}
	}

	foreach(%gui in EToolbarDisabledDrop)
		%gui.visible = 0;

	EToolbarDisabledDrop.visible = 0;
}
//------------------------------------------------------------------------------