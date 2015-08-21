//==============================================================================
// TorqueLab -> Initialize editor plugins
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TLab::DefaultPlugins = "SceneEditor";

//==============================================================================
// Create the Plugin object with initial data
function Lab::createPlugin(%this,%pluginName,%displayName,%alwaysEnable) {
	//Set plugin object name and verify if already existing
	%plugName = %pluginName@"Plugin";

	if (isObject( %plugName)) {
		warnLog("Plugin already created:",%pluginName);
		return %plugName;
	}

	if (%displayName $= "")
		%displayName = %pluginName;

	%pluginOrder = "99";

	if ($LabData_PluginOrder[%pluginName] !$= "")
		%pluginOrder = $LabData_PluginOrder[%pluginName];

	//Create the ScriptObject for the Plugin
	%pluginObj = new ScriptObject( %plugName ) {
		superClass = "EditorPlugin"; //Default to EditorPlugin class
		editorGui = EWorldEditor; //Default to EWorldEditor
		editorMode = "World";
		plugin = %pluginName;
		displayName = %displayName;
		toolTip = %displayName;
		alwaysOn = %alwaysEnable;
		pluginOrder = %pluginOrder;
		shortPlugin = %shortObjName;
		useTools = false;
	};
	LabPluginGroup.add(%pluginObj);

	if (strFind($TLab::DefaultPlugins,%pluginName))
		%pluginObj.isDefaultPlugin = true;

	if (%alwaysEnable)
		$PluginAlwaysOn[%pluginName] = true;

	if (%pluginObj.isMethod("onPluginCreated"))
		%pluginObj.onPluginCreated();

	//Lab.initPluginData(%pluginObj);
	return %pluginObj;
}
//------------------------------------------------------------------------------
//==============================================================================
//Reinitialize all plugin data
function Lab::initAllPluginConfig(%this) {
	foreach(%plugin  in LabPluginGroup)
		%this.initPluginConfig(%plugin);
}
//------------------------------------------------------------------------------
//==============================================================================
//Initialize plugin data
function Lab::initPluginConfig(%this,%pluginObj) {
	%pluginName = %pluginObj.plugin;

	//Moving toward new params array system
	%newArray = Lab.newParamsArray(%pluginName,"Plugins",%pluginObj);
	%newArray.displayName = %pluginObj.displayName;
	%pluginObj.paramArray = %newArray;
	%newArray.pluginObj = %pluginObj;
	%newArray.paramCallback = "Lab.onParamPluginBuild";
	
	//Default plugin settings
	%newArray.setVal("pluginOrder",      "99" TAB "pluginOrder" TAB "" TAB "" TAB %pluginObj.getName());
	%newArray.setVal("isEnabled",      "1" TAB "isEnabled" TAB "" TAB "" TAB %pluginObj.getName());

	if (%pluginObj.isMethod("initParamsArray"))
		%pluginObj.initParamsArray(%newArray);
}
//------------------------------------------------------------------------------

//==============================================================================
//Allow the plugin to be selected in editor
function Lab::enablePlugin(%this,%pluginObj,%enabled) {
	if (!isObject(%pluginObj)) {
		warnLog("Trying to enable invalid plugin:",%pluginObj);
		return;
	}
	
	if (%pluginObj.alwaysOn) 
		%enabled = "1";

	%name = %pluginObj.plugin;	

	%toolArray = ToolsToolbarArray.findObjectByInternalName(%pluginObj.getName());
	%toolDisabledArray = EditorGui-->DisabledPluginsBox.findObjectByInternalName(%pluginObj.getName());
	
	%pluginObj.isEnabled = %enabled;

	if (%enabled) {
		if (!isObject(%toolArray))
			%this.AddToEditorsMenu(%pluginObj);			
	} 
	else {
		hide(%toolArrayObj);
		%this.removeFromEditorsMenu(%pluginObj);		
	}
}
//------------------------------------------------------------------------------
//==============================================================================
//Call when Editor is open, check that all plugin are enabled correctly
function Lab::updateActivePlugins(%this) {
	foreach(%pluginObj in LabPluginGroup) {
		%enabled = %pluginObj.getCfg("isEnabled");	
		%this.enablePlugin(%pluginObj,%enabled);
	}
}
//------------------------------------------------------------------------------
/*//==============================================================================
function Lab::saveAllPluginData(%this) {
	//Update the PluginBar data (PluginOrder for now)
	Lab.updatePluginIconOrder();

	foreach(%pluginObj in LabPluginGroup) {
		%pluginObj.setCfg("pluginOrder",%pluginObj.pluginOrder);
	}
}
//------------------------------------------------------------------------------
*/
