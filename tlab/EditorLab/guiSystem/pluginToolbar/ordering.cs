//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Plugin Tool Bar Ordering Management (Sort and Store)
//==============================================================================

//==============================================================================
// Reorder the Plugins Bar from the store settings (If Default true, default will be used)
function Lab::sortPluginsBar(%this,%default) {
	if (%default)
		ToolsToolbarArray.sort("sortPluginByDefaultOrder");
	else
		ToolsToolbarArray.sort("sortPluginByOrder");
	ToolsToolbarArray.refresh();
	%this.updatePluginIconOrder();
}
//------------------------------------------------------------------------------
//Compare plugin order A and B and return result
function sortPluginByOrder(%objA,%objB) {		
	if ( %objA.pluginObj.pluginOrder > %objB.pluginObj.pluginOrder)
		return "1";

	if ( %objA.pluginObj.pluginOrder < %objB.pluginObj.pluginOrder)
		return "-1";

	return "0";
}
//------------------------------------------------------------------------------
//Compare plugin order A and B and return result
function sortPluginByDefaultOrder(%objA,%objB) {	
		if ( %objA.pluginObj.getCfg("pluginOrderDefault") > %objB.pluginObj.getCfg("pluginOrderDefault"))
			return "1";

		if ( %objA.pluginObj.getCfg("pluginOrderDefault") < %objB.pluginObj.getCfg("pluginOrderDefault"))
			return "-1";

		return "0";	
}
//------------------------------------------------------------------------------

//==============================================================================
//Store the current plugins order to setting (isDefault true will store defaults)
function Lab::updatePluginIconOrder(%this,%isDefault) {
	%count = ToolsToolbarArray.getCount();

	for ( %i = 0; %i < %count; %i++ ) {
		%icon = ToolsToolbarArray.getObject(%i);
		%icon.pluginObj.pluginOrder = %i+1;
		if (%isDefault)
			%icon.pluginObj.setCfg("pluginOrderDefault",%icon.pluginObj.pluginOrder);			
		else
			%icon.pluginObj.setCfg("pluginOrder",%icon.pluginObj.pluginOrder);			
	}
}
//------------------------------------------------------------------------------
