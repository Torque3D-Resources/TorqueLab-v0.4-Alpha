//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



//==============================================================================
// Plugin Bar Drag and Drop
//==============================================================================

//==============================================================================
function PluginIcon::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("PluginIcon class is corrupted! Fix it!");
		return;
	}

	startDragAndDropCtrl(%this,"PluginIcon");
	hide(%this);
	Lab.openDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginIcon::DragFailed( %this ) {
	Lab.closeDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginIcon::DragSuccess( %this ) {
	Lab.updatePluginIconContainer();
	Lab.closeDisabledPluginsBin(true);

	if (!%this.pluginObj.isEnabled) {
		warnLog("Disabled plugin icon dropped, set it enabled:",%this.pluginObj.plugin);
		Lab.enablePlugin(%this.pluginObj);
	}

	EWToolsToolbar.resize();
}
//------------------------------------------------------------------------------

//==============================================================================
function PluginIconContainer::onControlDropped( %this,%ctrl,%position ) {
	%originalCtrl = %ctrl.dragSourceControl;

	if (%originalCtrl.class $= "PluginIconDisabled") {
		Lab.enablePlugin(	%originalCtrl.pluginObj);
	}

	if (%ctrl.parentGroup.dropType !$= "PluginIcon") {
		warnLog("Only plugins icons can be drop in the Plugin Bar");
		Parent::onControlDropped( %this,%ctrl,%position );
		return;
	}

	//Simply remove it and add it so it go to end
	%this.remove(%originalCtrl);
	%this.add(%originalCtrl);
	show(%originalCtrl);
	delObj(%ctrl);
	%originalCtrl.DragSuccess();
	EWToolsToolbar.resize();
}
//------------------------------------------------------------------------------

//==============================================================================
function PluginIcon::onControlDropped( %this,%ctrl,%position ) {
	%originalCtrl = %ctrl.dragSourceControl;

	if (%originalCtrl.class $= "PluginIconDisabled") {
		devLog(" PluginIcon  DisabledIconDropped");
		Lab.enablePlugin(	%originalCtrl.pluginObj);
	}

	if (%ctrl.parentGroup.dropType !$= "PluginIcon") {
		warnLog("Only plugins icons can be drop in the Plugin Bar");
		return;
	}

	//Let's add it just before this
	//%this.parentGroup.add(%ctrl);
	show(%originalCtrl);

	if (!ToolsToolbarArray.isMember(%originalCtrl))
		ToolsToolbarArray.add(%originalCtrl);

	ToolsToolbarArray.reorderChild(%originalCtrl,%this);
	delObj(%ctrl);
	%originalCtrl.DragSuccess();
	EWToolsToolbar.resize();
}
//------------------------------------------------------------------------------

//==============================================================================
function DisabledPluginIcon::onMouseDragged( %this,%a1,%a2,%a3 ) {
	startDragAndDropCtrl(%this,"PluginIcon");
	hide(%this);
	Lab.openDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------




//==============================================================================
// DisabledPlugin Bar Drag and Drop
//==============================================================================

//==============================================================================
function PluginIconDisabled::onMouseDragged( %this,%a1,%a2,%a3 ) {
	if (!isObject(%this)) {
		devLog("PluginIcon class is corrupted! Fix it!");
		return;
	}

	startDragAndDropCtrl(%this,"PluginIcon");
	hide(%this);
	Lab.openDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginIconDisabled::DragFailed( %this ) {
	Lab.closeDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginIconDisabled::DragSuccess( %this ) {
	Lab.updatePluginIconContainer();
	Lab.closeDisabledPluginsBin(true);
}
//------------------------------------------------------------------------------

//==============================================================================
function DisabledPluginsBox::onControlDropped( %this,%ctrl,%position ) {
	%originalIcon = %ctrl.dragSourceControl;

	if (%ctrl.parentGroup.dropType !$= "PluginIcon") {
		warnLog("Only plugins icons can be drop in the Plugin Bar");
		return;
	}

	show(%originalIcon);
	delObj(%trashedIcon);
	%this.add(%originalIcon);
	info(%originalIcon.internalName," dropped in DisabledPluginsBox and should be set disabled",%originalIcon.pluginObj);
	Lab.disablePlugin(%originalIcon.pluginObj);
	EWToolsToolbar.resize();
}
//------------------------------------------------------------------------------