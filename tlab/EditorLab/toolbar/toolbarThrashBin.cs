//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Toolbar Dropping a dragged item callbacks
//==============================================================================

//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function EToolbarTrashClass::onControlDropped(%this, %control, %dropPoint) {	
	Lab.checkTrashDroppedCtrl(%control);
}
//------------------------------------------------------------------------------


//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function Lab::checkTrashDroppedCtrl(%this, %control) {
	devLog("Lab::checkTrashDroppedCtrl(%this, %control)",%this, %control);
	%droppedCtrl = %control.dragSourceControl;
	Lab.hideDisabledToolbarDropArea(	);
	show(%droppedCtrl);
	if (%control.dropType !$= "Toolbar") {
		warnLog("Toolbar thrash dropped invalid droptype ctrl:",%control);		
		return;
	}

	if (%droppedCtrl.isMemberOfClass("GuiStackControl"))
		%this.addStackToGuiTrash(%droppedCtrl); 
	else if (%droppedCtrl.isMemberOfClass("GuiIconButtonCtrl"))
		%this.addCtrlToGuiTrash("Icon",%droppedCtrl);
	else
		%this.addCtrlToGuiTrash("Box",%droppedCtrl);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function Lab::addStackToGuiTrash(%this,%ctrl) {
	for(%i = %ctrl.getCount()-1; %i >= 0; %i--) {
		%obj = %ctrl.getObject(%i);
		
		if (strFindWords(%obj.internalName,"DisabledIcons StackEnd GroupDivider"))
			continue;		
			
		if (%obj.isMemberOfClass("GuiIconButtonCtrl"))
			%this.addCtrlToGuiTrash("Icon",%obj);
		else
			%this.addCtrlToGuiTrash("Box",%obj);	
	}	
}
//------------------------------------------------------------------------------
//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function Lab::addCtrlToGuiTrash(%this, %type, %ctrl) {
	Lab.setToolbarIconDisabled(%ctrl,true);
	%container = "EToolbar"@%type@"Trash";
	if (!isObject(%container)){
		warnLog("Trying to add a control to trash using wrong type:",%type);
		return;
	}
	%container.add(%ctrl);
	
	return;
	%clone = %droppedCtrl.deepClone();
	%clone.srcCtrl = %droppedCtrl;
	show(%clone);
	%this.add(%clone);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setToolbarIconDisabled(%this,%icon,%disabled) {
	%icon.canSaveDynamicFields = true;

	if (%icon.locked $= %disabled)
		return;

	EGuiCustomizer-->saveToolbar.active = true;
	%icon.locked = %disabled;

	if (!%disabled)
		return;

	%disabledGroup = %icon.toolbar-->DisabledIcons;

	if (%disabled && isObject(%disabledGroup))
		%disabledGroup.add(%icon);
}
//------------------------------------------------------------------------------