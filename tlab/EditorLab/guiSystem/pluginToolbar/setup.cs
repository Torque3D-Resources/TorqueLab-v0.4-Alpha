//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function Lab::updatePluginsBar(%this,%reset) {
	if (%reset)
		%this.resetPluginsBar();

	foreach(%pluginObj in LabPluginGroup) {	
		Lab.addPluginToBar( %pluginObj );
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::resetPluginsBar(%this) {
	ToolsToolbarArray.deleteAllObjects();
	Lab.clearDisabledPluginsBin();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::updatePluginsMenu(%this,%reset) {
	foreach(%pluginObj in LabPluginGroup) 
		%this.addToEditorsMenu(%pluginObj);
}
//------------------------------------------------------------------------------

//==============================================================================
// Add a plugin icon to the plugin bar
//==============================================================================

//==============================================================================
function Lab::addPluginToBar( %this, %pluginObj ) {	
	if (%pluginObj.isHidden)
		return;

	//First check if the Icon object is in on control
	
	
	%enabled =  %pluginObj.isEnabled;
	%containerEnabled = ToolsToolbarArray;
	%containerDisabled = EditorGui-->DisabledPluginsBox;
	%toolArrayEnabled = %containerEnabled.findObjectByInternalName(%pluginObj.plugin);
	%toolArrayDisabled = %containerDisabled.findObjectByInternalName(%pluginObj.plugin);

	if (isObject(%toolArrayEnabled)) {
		if (%enabled)
			%alreadyExists = true;
		else
			delObj(%toolArrayEnabled);
	}

	if (isObject(%toolArrayDisabled)) {
		if (!%enabled)
			%alreadyExists = true;
		else
			delObj(%toolArrayDisabled);
	}

	//If the Plugin Icon already exist, exit now
	if(%alreadyExists)
		return;

	%icon = %this.createPluginIcon(%pluginObj);

	if (%enabled){
		ToolsToolbarArray.add(%icon);
	}
	else{	
		EditorGui-->DisabledPluginsBox.add(%icon);
	}

	Lab.sortPluginsBar();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::createPluginIcon( %this, %pluginObj ) {
	%icon = "tlab/themes/DarkBlue/buttons/plugin-assets/"@%pluginObj.plugin@"Icon";

	if (!isFile(%icon@"_n.png"))
		%icon = "tlab/themes/DarkBlue/buttons/plugin-assets/TerrainEditorIcon";

	%button = cloneObject(EditorGui-->PluginIconSrc);
	%button.internalName = %pluginObj.plugin;
	%button.superClass = "";
	%button.command = "Lab.setEditor(" @ %pluginObj.getName()@ ");";
	%button.bitmap = %icon;
	%button.tooltip = %pluginObj.tooltip;
	%button.visible = true;
	%button.useMouseEvents = true;
	%button.pluginObj = %pluginObj;
	%button.class = "PluginIcon";
	return %button;
}
//------------------------------------------------------------------------------



//==============================================================================
function Lab::togglePluginBarSize( %this ) {	
	%collapsed = ToolsToolbarArray.isCollapsed;
	if (%collapsed)
		Lab.expandPluginBar();
	else
		Lab.collapsePluginBar();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::expandPluginBar( %this ) {		
	ToolsToolbarArray.isCollapsed = false;
	foreach(%icon in ToolsToolbarArray){
		%enabled = %icon.pluginObj.isEnabled;
		%icon.visible = %enabled;
	}	
	%this.refreshPluginToolbar();	
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::collapsePluginBar( %this ) {		
	ToolsToolbarArray.isCollapsed = true;
	foreach(%icon in ToolsToolbarArray){
		if (%icon.pluginObj.getId() $= Lab.currentEditor.getId())
			%icon.visible = true;
		else
			%icon.visible = false;			
	}
	%this.refreshPluginToolbar();	
}
//------------------------------------------------------------------------------
$PluginToolbarBitmap[0] = "tlab/gui/icons/default/collapse-toolbar";
$PluginToolbarBitmap[1] = "tlab/gui/icons/default/expand-toolbar";
//==============================================================================
function Lab::refreshPluginToolbar( %this ) {	
	%collapsed = ToolsToolbarArray.isCollapsed;
	ToolsToolbarArray.refresh();
	EWToolsToolbar.position = "0 -2";
	EWToolsToolbar.extent.y = "42";
	ToolsToolbarArray.position.y = 2;
	EWToolsToolbar.extent.x = ToolsToolbarArray.extent.x + 10;
	EWToolsToolbar-->resizeArrow.AlignCtrlToParent("right");
	EWToolsToolbar-->resizeArrow.setBitmap($PluginToolbarBitmap[%collapsed]);
	hide(EWToolsToolbarDecoy);
}
//------------------------------------------------------------------------------
//==============================================================================
// Plugin Toolbar GuiControl Functions
//==============================================================================

//==============================================================================
function EWToolsToolbar::reset( %this ) {
	devLog("EWToolsToolbar::reset is calling expandPluginBar instead");
	
	Lab.expandPluginBar();
	return;
	%count = ToolsToolbarArray.getCount();

	for( %i = 0 ; %i < %count; %i++ )
		ToolsToolbarArray.getObject(%i).setVisible(true);

	%this.setExtent((29 + 4) * %count + 12, 33);
	%this.isClosed = 0;
	EWToolsToolbar.isDynamic = 0;
	EWToolsToolbarDecoy.setVisible(false);
	EWToolsToolbarDecoy.setExtent((29 + 4) * %count + 4, 31);
	%this-->resizeArrow.setBitmap( "tlab/gui/icons/default/collapse-toolbar" );
}
//------------------------------------------------------------------------------
//==============================================================================
function EWToolsToolbar::expand( %this, %close ) {
	devLog("EWToolsToolbar::expand is calling expandPluginBar instead");
	
	Lab.expandPluginBar();
	return;
	%this.isClosed = !%close;
	%this.toggleSize();
}
//------------------------------------------------------------------------------
//==============================================================================
function EWToolsToolbar::resize( %this ) {
	devLog("EWToolsToolbar::resize is calling refreshPluginToolbar instead");
	
	Lab.refreshPluginToolbar();
	return;
	%this.isClosed = ! %this.isClosed ;
	%this.toggleSize();
}
//------------------------------------------------------------------------------
//==============================================================================
function EWToolsToolbar::toggleSize( %this, %useDynamics ) {
	devLog("EWToolsToolbar::toggleSize is calling togglePluginBarSize instead");
	Lab.togglePluginBarSize();
	return;
	// toggles the size of the tooltoolbar. also goes through
	// and hides each control not currently selected. we hide the controls
	// in a very neat, spiffy way
	EWToolsToolbar-->resizeArrow.setExtent("10","40");

	if ( %this.isClosed == 0 ) {
		%image = "tlab/gui/icons/default/expand-toolbar";

		for( %i = 0 ; %i < ToolsToolbarArray.getCount(); %i++ ) {
			%object = ToolsToolbarArray.getObject(%i);
			%plugin = %object.pluginObj;
			%enabled =%plugin.isEnabled;
			%plugin.pluginOrder = %i+1;
			$LabData_PluginOrder[%plugin.plugin] = %i+1;
			//if( %plugin.getName() !$= Lab.currentEditor.getName())
			%object.setVisible(%enabled);
		}

		//%this.setExtent(45, 33);
		%this.extent = "54 40";
		%this-->resizeArrow.position = "44 0";
		%this.isClosed = 1;

		if(!%useDynamics) {
			EWToolsToolbarDecoy.setVisible(true);
			EWToolsToolbar.isDynamic = 1;
		}

		EWToolsToolbarDecoy.extent ="22 31";
	} else {
		%image = "tlab/gui/icons/default/collapse-toolbar";
		%count = ToolsToolbarArray.getCount();

		for( %i = 0 ; %i < %count; %i++ ){
			%object = ToolsToolbarArray.getObject(%i);
			%plugin = %object.pluginObj;
			%enabled =%plugin.isEnabled;			
			%object.setVisible(%enabled);
			if (%enabled)
				%enableCount++;
		}

		%extentY = 37 * %enableCount + 14;
		%this.setExtent(%extentY, 40);
		%this.isClosed = 0;

		if(!%useDynamics) {
			EWToolsToolbarDecoy.setVisible(false);
			EWToolsToolbar.isDynamic = 0;
		}

		%extentY = 40 * %enableCount + 34;
		EWToolsToolbarDecoy.extent =%extentY SPC"31";
	}

	EWToolsToolbar-->resizeArrow.setBitmap( %image );
	ToolsToolbarArray.refresh();
}
//------------------------------------------------------------------------------
//==============================================================================
function EWToolsToolbarDecoy::onMouseEnter( %this ) {
	EWToolsToolbar.toggleSize(true);
}
//------------------------------------------------------------------------------
//==============================================================================
function EWToolsToolbarDecoy::onMouseLeave( %this ) {
	EWToolsToolbar.toggleSize(true);
}
//------------------------------------------------------------------------------