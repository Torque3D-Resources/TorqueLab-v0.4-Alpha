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
function ToolbarPluginIcon::onMouseDragged( %this,%a1,%a2,%a3 ) {	
	if (!isObject(%this)) {
		devLog("ToolbarPluginIcon class is corrupted! Fix it!");
		return;
	}

	Lab.startToolbarDrag(%this,%a1,%a2,%a3);
}
//------------------------------------------------------------------------------
//==============================================================================
// Start dragging a toolbar icon
function ToolbarIcon::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("PluginIcon class is corrupted! Fix it!");
		return;
	}
	Lab.startToolbarDrag(%this,%a1,%a2,%a3);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Start dragging a toolbar icons group
function ToolbarBoxChild::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("ToolbarBoxChild class is corrupted! Fix it!");
		return;
	}
	%src = %this.parentGroup;
	Lab.startToolbarDrag(%src,%a1,%a2,%a3);
	
}
//------------------------------------------------------------------------------



//==============================================================================
// Start dragging a toolbar icons group
function ToolbarPluginGroup::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("ToolbarGroup class is corrupted! Fix it!");
		return;
	}

	%group = %this.parentGroup.parentGroup;

	if (%group.getClassName() !$= "GuiStackControl")
		return;
	Lab.startToolbarDrag(%group,%a1,%a2,%a3);

}
//------------------------------------------------------------------------------
//==============================================================================
// Start dragging a toolbar icons group
function ToolbarGroup::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("ToolbarGroup class is corrupted! Fix it!");
		return;
	}

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
	
	
	startDragAndDropCtrl(%ctrl,"Toolbar","Lab.onToolbarIconDroppedDefault");	
	hide(%ctrl);
	Lab.showDisabledToolbarDropArea(	%ctrl);
}
//------------------------------------------------------------------------------
//==============================================================================
// Toolbar Dropping a dragged item callbacks
//==============================================================================

//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function EToolbarIconTrash::onControlDropped(%this, %control, %dropPoint) {
	%droppedCtrl = %control.dragSourceControl;
Lab.hideDisabledToolbarDropArea(	);
	if (%control.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		show(%droppedCtrl);
		return;
	}
	if(isObject(%droppedCtrl.srcCtrl))
		return;
	if (%droppedCtrl.getClassName() $= "GuiStackControl") {
		show(%droppedCtrl);

		for(%i = %droppedCtrl.getCount()-1; %i >= 0; %i--) {
			%obj = %droppedCtrl.getObject(%i);

			if (%obj.internalName $= "GroupDivider")
				continue;

			%this.add(%obj);
		}

		%droppedCtrl.delete();
		return;
	}
	
	
	Lab.setToolbarIconDisabled(%droppedCtrl,true);
	%clone = %droppedCtrl.deepClone();
	%clone.srcCtrl = %droppedCtrl;
	
	show(%clone);
	%this.add(%clone);
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over a toolbar icon
function ToolbarIcon::onControlDropped(%this, %control, %dropPoint) {

	%droppedCtrl = %control.dragSourceControl;
Lab.hideDisabledToolbarDropArea(	);
	if (%control.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		show(%droppedCtrl);
		return;
	}
	
	
		if(%droppedCtrl.pluginName !$= %this.pluginName){			
			warnLog("Can't drop plugin icon on something not related to this plugin. Dropped Plugin",%droppedCtrl.pluginName,"On",%this.pluginName);			
			show(%droppedCtrl);
			return;
		}
	
	
	if(isObject(%droppedCtrl.srcCtrl))
	{
	
		%clone = %droppedCtrl;
		%droppedCtrl = %droppedCtrl.srcCtrl;
		delObj(%clone);
	}
	%addToThis = %this.parentGroup;
	%addBefore = %this;
	show(%droppedCtrl);
	
	if (%droppedCtrl.getClassName() $= "GuiStackControl") {
		%stackAndChild = Lab.findObjectToolbarStack(%this,true);
		%addToThis = getWord(%stackAndChild,0);
		%addBefore =  getWord(%stackAndChild,1);
	} else if (%droppedCtrl.isNewGroup) {
		show(%droppedCtrl);
		%newGroup = Lab.createNewToolbarIconGroup();
		%droppedCtrl = %newGroup;
		%stackAndChild = Lab.findObjectToolbarStack(%this,true);
		%addToThis = getWord(%stackAndChild,0);
		%addBefore =  getWord(%stackAndChild,1);
	}

	if (!isObject( %addToThis )) {
		warnLog("Something went wrong, can't find a valid stack for the dropped on icon",%this);
		return;
	}

	Lab.addToolbarItemToGroup(%droppedCtrl,%addToThis,%addBefore);
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over a toolbar icon
function PluginToolbarEnd::onControlDropped(%this, %control, %dropPoint) {
	%droppedCtrl = %control.dragSourceControl;
	Lab.hideDisabledToolbarDropArea(	);
	if (%control.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		show(%droppedCtrl);
		return;
	}
	if(isObject(%droppedCtrl.srcCtrl))
	{
		
		%clone = %droppedCtrl;
		%droppedCtrl = %droppedCtrl.srcCtrl;
		delObj(%clone);
	}
	show(%droppedCtrl);
	%stackAndChild = Lab.findObjectToolbarStack(%this,true);
	%addToThis = getWord(%stackAndChild,0);
	%addBefore =  getWord(%stackAndChild,1);

	if (%droppedCtrl.isNewGroup ) {
		%newGroup = Lab.createNewToolbarIconGroup();
		%droppedCtrl = %newGroup;
	}

	Lab.addToolbarItemToGroup(%droppedCtrl,%addToThis,%addBefore);
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::onToolbarIconDroppedDefault(%this,%dropOnCtrl, %draggedControl, %dropPoint) {
	%droppedCtrl = %draggedControl.dragSourceControl;
	Lab.hideDisabledToolbarDropArea(	);
	if (%draggedControl.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		show(%droppedCtrl);
		return;
	}

	if (%dropOnCtrl.superClass $= "ToolbarIconNoDrop"){
		warnLog("Can't drop on locked icons");
		show(%droppedCtrl);
		return;
	}

		if(%draggedControl.pluginName !$= %dropOnCtrl.pluginName){	
			devLog("Dropped Plugin Name:",%draggedControl.pluginName);
			devLog("Dropped on Plugin Name:",%dropOnCtrl.pluginName);			
			warnLog("Can't drop plugin icon on something not related to this plugin. Dropped Plugin",%draggedControl.pluginName,"On",%dropOnCtrl.pluginName);			
			show(%droppedCtrl);
			return;
		}
	devLog("Dropped on InternalName:",%dropOnCtrl.internalName);
	if (%dropOnCtrl.internalName $= "SubStackEnd"){
		warnLog("Dropped on Sub Stack End");
		%addToThis = %dropOnCtrl.parentGroup;
		
	}
	if (%dropOnCtrl.internalName $= "StackEnd"){
		warnLog("Dropped on Stack End");
		%addToThis = %dropOnCtrl.parentGroup;
		
	}
	if (%dropOnCtrl.internalName $= "StackEndImg"){
		warnLog("Dropped on Stack End Img");
		%addToThis = %dropOnCtrl.parentGroup.parentGroup;
		
	}
	show(%droppedCtrl);
	%addBefore = %dropOnCtrl;
	if (isObject(%addToThis)){
		devLog("Premature dropping!");
		%this.addToolbarItemToGroup(%droppedCtrl,%addToThis,%addBefore);
		return;
	}

	if (%droppedCtrl.isNewGroup || %droppedCtrl.getClassName() $= "GuiStackControl") {
		%stackAndChild = Lab.findObjectToolbarStack(%dropOnCtrl,true);
		%addToThis = getWord(%stackAndChild,0);
		%addBefore =  getWord(%stackAndChild,1);
	} else
		%addToThis = Lab.findObjectToolbarStack(%dropOnCtrl,%lookForDefaultStack);

	if (!isObject(%addToThis))
		return;

	if (%droppedCtrl.isNewGroup) {
		%newGroup = Lab.createNewToolbarIconGroup();
		%droppedCtrl = %newGroup;
	}

	%this.addToolbarItemToGroup(%droppedCtrl,%addToThis,%addBefore);
}
//------------------------------------------------------------------------------
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
// Create a new Icon Group to be dragged in Toolbar
//==============================================================================
//==============================================================================
// Start dragging a new icons group to be added to toolbar
function NewToolbarGroup::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("ToolbarGroup class is corrupted! Fix it!");
		return;
	}

	%group = %this.parentGroup;
	%group.isNewGroup = true;
	startDragAndDropCtrl(%group,"Toolbar","Lab.onToolbarIconDroppedDefault");
}
//------------------------------------------------------------------------------

//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::createNewToolbarIconGroup(%this) {
	%newGroup = new GuiStackControl() {
		stackingType = "Horizontal";
		horizStacking = "Left to Right";
		vertStacking = "Top to Bottom";
		padding = "2";
		dynamicSize = "1";
		dynamicNonStackExtent = "0";
		dynamicPos = "0";
		changeChildSizeToFit = "1";
		changeChildPosition = "1";
		position = "472 0";
		extent = "68 30";
		minExtent = "4 16";
		profile = "ToolsPanelDarkB";
		visible = "1";
		active = "1";
		internalName = "ToolbarGroup";
		superClass = "ToolbarContainer";
		new GuiContainer() {
			position = "0 0";
			extent = "16 30";
			minExtent = "4 2";
			profile = "ToolsPanelDarkB";
			visible = "1";
			active = "1";
			tooltipProfile = "ToolsGuiToolTipProfile";
			internalName = "GroupDivider";
			new GuiBitmapCtrl() {
				bitmap = "tlab/gui/icons/toolbar-assets/GroupDivider.png";
				wrap = "0";
				position = "1 6";
				extent = "4 18";
				minExtent = "4 2";
				profile = "ToolsDefaultProfile";
				visible = "1";
				active = "1";
			};
			new GuiMouseEventCtrl() {
				extent = "16 30";
				minExtent = "8 2";
				visible = "1";
				active = "1";
				superClass = "ToolbarGroup";
			};
		};
	};
	return %newGroup;
}
//==============================================================================
function Lab::showDisabledToolbarDropArea(%this,%dragControl) {
	%isIcon = false;
	if (%dragControl.isMemberOfClass("GuiIconButtonCtrl"))
		%isIcon = true;
	EToolbarDisabledDrop.visible = 1;
	
	//Only show StackEnd for groups
	if(%isIcon){
		foreach(%gui in LabToolbarGuiSet) {
			foreach$(%stack in %gui.stackLists){
				%stackEnd = %stack-->SubStackEnd;			
				if (!isObject(%stackEnd))
					continue;				
				
				show(%stackEnd);
				%stack.pushToBack(%stackEnd);
				%stackEnd.profile = "ToolsBoxDarkC";
				%stackEnd.setExtent(18,%gui.parentGroup.extent.y);		
			}
		}
	}
	else {
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
		if (%gui.pluginName !$= %dragControl.pluginName){
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
		foreach$(%stack in %gui.stackLists){
			%subStackEnd = %stack-->SubStackEnd;			
			if (isObject(%subStackEnd))
					hide(%subStackEnd);				
		}
	}

	foreach(%gui in EToolbarDisabledDrop)
			%gui.visible = 0;				
	EToolbarDisabledDrop.visible = 0;		
}
//------------------------------------------------------------------------------