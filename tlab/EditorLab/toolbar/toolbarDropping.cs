//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Manage Toolbar Group Dropping
//==============================================================================
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::onToolbarGroupDroppedDefault(%this,%dropOnCtrl, %draggedControl, %dropPoint) {
	%droppedCtrl = %draggedControl.dragSourceControl;
	Lab.hideDisabledToolbarDropArea(	);
	show(%droppedCtrl);

	if (%draggedControl.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		return;
	}

	if(%droppedCtrl.pluginName !$= %dropOnCtrl.pluginName) {
		warnLog("Can't drop plugin icon on something not related to this plugin.");
		return;
	}
	%this.findToolbarGroupDropTarget(%dropOnCtrl,%droppedCtrl,%dropPoint);	
}
//------------------------------------------------------------------------------

//==============================================================================
// New toolbar group dropping
function Lab::onNewToolbarGroupDropped(%this,%dropOnCtrl, %draggedControl, %dropPoint) {
	%droppedCtrl = %draggedControl.dragSourceControl;
	Lab.hideDisabledToolbarDropArea();
	show(%droppedCtrl);
	if (!%droppedCtrl.isNewGroup)
		return;
		
	
	%newGroup = Lab.createNewToolbarIconGroup();	
	
	%newGroup.pluginName = %dropOnCtrl.pluginName;
	%newGroup-->MouseStart.pluginName = %dropOnCtrl.pluginName;
	%newGroup-->MouseStart.droptTarget = %newGroup.getId();

	%this.findToolbarGroupDropTarget(%dropOnCtrl,%newGroup,%dropPoint);	
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::findToolbarGroupDropTarget(%this,%dropOnCtrl, %droppedCtrl, %dropPoint) {	
	//devLog("findToolbarGroupDropTarget(%dropOnCtrl, %droppedCtrl, %dropPoint)",%dropOnCtrl, %droppedCtrl, %dropPoint);
	
	switch$(%dropOnCtrl.superClass) {
		case "StackEnd":
			if (%droppedCtrl.getClassName() !$= "GuiStackControl") {
				warnLog("Can't drop icon on a group stack",%droppedCtrl);
				return;
			}

			%addToThis = %dropOnCtrl.parentGroup;
			%addBefore = %dropOnCtrl;

		case "StackEndImg" or "ToolbarIcon":
			%addToThis = %dropOnCtrl.parentGroup.parentGroup;
			%addBefore = %dropOnCtrl.parentGroup;
	}
	
	%this.dropControlOnToolbar(%droppedCtrl,%addToThis,%addBefore);
	return;
	if (%droppedCtrl.isNewGroup ) {
		%stackAndChild = Lab.findObjectToolbarStack(%dropOnCtrl,true);
		%addToThis = getWord(%stackAndChild,0);
		%addBefore =  getWord(%stackAndChild,1);
	}

	
}
//------------------------------------------------------------------------------

//==============================================================================
// Manage Toolbar Icon Dropping
//==============================================================================

//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::onToolbarIconDroppedDefault(%this,%dropOnCtrl, %draggedControl, %dropPoint) {
	%droppedCtrl = %draggedControl.dragSourceControl;
	Lab.hideDisabledToolbarDropArea(	);
	show(%droppedCtrl);
	
	if (%draggedControl.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);
		return;
	}

	if(%draggedControl.pluginName !$= %dropOnCtrl.pluginName) {
		warnLog("Can't drop plugin icon on something not related to this plugin. Dropped Plugin",%draggedControl.pluginName,"On",%dropOnCtrl.pluginName);			
		return;
	}

	%this.findToolbarItemDropTarget(%dropOnCtrl,%droppedCtrl,%dropPoint);	
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::findToolbarItemDropTarget(%this,%dropOnCtrl, %droppedCtrl, %dropPoint) {
devLog("findToolbarItemDropTarget(%dropOnCtrl SuperClass",%dropOnCtrl.superClass);
	switch$(%dropOnCtrl.superClass) {
		case "MouseStart":
			%addToThis = %dropOnCtrl.parentGroup.parentGroup;
			%addBefore = %dropOnCtrl.parentGroup.parentGroup.getObject(1);
		case "SubStackEnd" or "ToolbarIcon":
			%addToThis = %dropOnCtrl.parentGroup;
			%addBefore = %dropOnCtrl;

		case "SubStackEndImg":
			%addToThis = %dropOnCtrl.parentGroup.parentGroup;
			%addBefore = %dropOnCtrl.parentGroup;
	}

	%this.dropControlOnToolbar(%droppedCtrl,%addToThis,%addBefore);
}
//------------------------------------------------------------------------------
//==============================================================================
// Add the new item to toolbard
//==============================================================================
//==============================================================================
// Dragged control dropped over undefined control (gonna check if it's droppable)
function Lab::dropControlOnToolbar(%this,%droppedCtrl, %addToThis, %addBefore) {
	if (!isObject(%addToThis)) {
		warnLog("Invalid target to add dropped ctrl to:",%addToThis);
		return;
	}

	if (!isObject(%addBefore))
		warnLog("Invalid target to add before:",%addBefore);

	%this.addToolbarItemToGroup(%droppedCtrl,%addToThis,%addBefore);
}
//------------------------------------------------------------------------------