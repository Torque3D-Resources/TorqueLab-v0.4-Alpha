//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ETools::initTools(%this) {
	foreach(%gui in %this) {
		if (%gui.isMethod("initTool"))
			%gui.initTool();
	}

	ETransformBoxGui.initTool();
}
//------------------------------------------------------------------------------

//==============================================================================
function ETools::toggleTool(%this,%tool) {
	if (isObject(%tool))
		%dlg = %tool;
	else
		%dlg = %this.findObjectByInternalName(%tool,true);

	if (!isObject(%dlg)) {
		warnLog("Trying to toggle invalid tool:",%tool);
		return;
	}

	%this.fitIntoParents();
	ETools.visible = true;

	if (%dlg.visible) {
		%this.hideTool(%tool);
		//%dlg.setVisible(false);
		//%position = getRealCursorPos();
		//%dlg.position = %position;
		//%dlg.position.x -= %dlg.extent.x/2;
		//%dlg.position.y -= EditorGuiMenubar.extent.y;
	} else {
		%this.showTool(%tool);
		//%dlg.setVisible(true);
	}

	return;
	%hideMe = true;

	foreach(%gui in %this)
		if (%gui.visible)
			%hideMe = false;

	if (%hideMe)
		%this.visible = 0;

	if (isObject(%dlg.linkedButton))
		%dlg.linkedButton.setStateOn(%dlg.visible);
}
//------------------------------------------------------------------------------

//==============================================================================
function ETools::showTool(%this,%tool) {
	if (isObject(%tool))
		%dlg = %tool;
	else
		%dlg = %this.findObjectByInternalName(%tool,true);

	if(!isObject(%dlg)) {
		warnLog("Trying to show invalid tools dialog for tool:",%tool,"Dlg",%dlg);
		return;
	}

	%this.fitIntoParents();
	%toggler = EditorGuiToolbarStack.findObjectByInternalName(%tool@"Toggle",true);

	if (isObject(%toggler))
		%toggler.setStateOn(true);

	ETools.visible = true;
	%dlg.setVisible(true);

	if (isObject(%dlg.linkedButton))
		%dlg.linkedButton.setStateOn(true);

	if (%dlg.isMethod("onShow"))
		%dlg.onShow();
}
//------------------------------------------------------------------------------

//==============================================================================
function ETools::hideTool(%this,%tool) {
	if (isObject(%tool))
		%dlg = %tool;
	else
		%dlg = %this.findObjectByInternalName(%tool,true);

	%toggler = EditorGuiToolbarStack.findObjectByInternalName(%tool@"Toggle",true);

	if (isObject(%toggler))
		%toggler.setStateOn(false);

	%dlg.setVisible(false);
	%hideMe = true;

	foreach(%gui in %this)
		if (%gui.visible)
			%hideMe = false;

	if (%hideMe)
		%this.visible = 0;

	if (isObject(%dlg.linkedButton))
		%dlg.linkedButton.setStateOn(false);

	if (%dlg.isMethod("onHide"))
		%dlg.onHide();
}
//------------------------------------------------------------------------------