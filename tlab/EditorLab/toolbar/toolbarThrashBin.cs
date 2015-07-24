//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
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
//==============================================================================
// Toolbar Dropping a dragged item callbacks
//==============================================================================

//==============================================================================
// Dragged control dropped in Toolbar Trash Bin
function EToolbarIconTrash::onControlDropped(%this, %control, %dropPoint) {
	devLog("EToolbarIconTrash::onControlDropped(%this, %control, %dropPoint)",%this, %control, %dropPoint);
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

