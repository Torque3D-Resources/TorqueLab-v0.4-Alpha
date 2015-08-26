//==============================================================================
// TorqueLab -> Initialize editor plugins
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TLab::DefaultPlugins = "SceneEditor";

//==============================================================================
// Create the Plugin object with initial data
function Lab::createPlugin(%this,%pluginName,%displayName,%alwaysEnable,%isModule) {
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
		displayName = %displayName;
		toolTip = %displayName;
		alwaysOn = %alwaysEnable;		
		useTools = false;
	};
	
	if (%isModule){
		%pluginObj.module = %pluginName;
		return %pluginObj;
	}
	%pluginObj.plugin = %pluginName;
	%pluginObj.pluginOrder = %pluginOrder;
	%pluginObj.shortPlugin = %shortObjName;
	
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
// Module are mini plugin which don't need tools or icons
function Lab::createModule(%this,%pluginName,%displayName,%alwaysEnable) {
	%moduleObj = %this.createPlugin(%pluginName,%displayName,%alwaysEnable,true);
	LabModuleGroup.add(%moduleObj);
}
//------------------------------------------------------------------------------
//==============================================================================
// Plugins Initialization Scripts
//==============================================================================

//==============================================================================
// Called the first time the World Editor is launched
function Lab::initialPluginsSetup(%this) {
	//Prepare the plugins toolbars
	//%this.initAllToolbarGroups();
	%this.initAllPluginsDialogs();
}
//------------------------------------------------------------------------------


//==============================================================================
// Called just before the Plugins settings will be generated (Lab.initConfigSystem)
function Lab::initAllPluginConfig(%this) {
	foreach(%plugin  in LabPluginGroup)
		%this.initPluginConfig(%plugin);
}
//------------------------------------------------------------------------------
// Initialize the PluginObj configs
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
// Make the Active Plugins Enabled - Called from Editor::open
//==============================================================================
//==============================================================================
//Call when Editor is open, check that all plugin are enabled correctly
function Lab::updateActivePlugins(%this) {
	foreach(%pluginObj in LabPluginGroup) {
		%enabled = %pluginObj.getCfg("isEnabled");
		%pluginObj.setPluginEnable(%enabled);		
	}
}
//------------------------------------------------------------------------------
function Lab::enablePlugin(%this,%pluginObj,%enabled) {
	if (!isObject(%pluginObj)) {
		warnLog("Trying to enable invalid plugin:",%pluginObj);
		return;
	}	
	if (%pluginObj.isEnabled)
		warnLog(%pluginObj.plugin,"The plugin is already enabled...");
		
	%pluginObj.setPluginEnable(true);
}
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function Lab::disablePlugin(%this,%pluginObj) {
	if (!isObject(%pluginObj)) {
		warnLog("Trying to enable invalid plugin:",%pluginObj);
		return;
	}	

	if (!%pluginObj.isEnabled)
		warnLog(%pluginObj.plugin,"The plugin is already disabled...");
		
	%pluginObj.setPluginEnable(false);
}
//------------------------------------------------------------------------------
//==============================================================================
function EditorPlugin::setPluginEnable(%this,%enabled) {
	
	if (%this.alwaysOn) 
		%enabled = "1";

	%name = %this.plugin;	

	%toolArray = ToolsToolbarArray.findObjectByInternalName(%this.plugin);
	%toolDisabledArray = EditorGui-->DisabledPluginsBox.findObjectByInternalName(%this.plugin);
	
	%this.isEnabled = %enabled;

	if (%enabled) {
		if (!isObject(%toolArray))
			%this.AddToEditorsMenu(%this);
		show(%toolArray);
		info(%this.plugin,"plugin is enabled");
	} 
	else {
		hide(%toolArray);
		//%this.removeFromEditorsMenu(%this);	
		info(%this.plugin,"plugin is disabled");
	}
	//EWToolsToolbar.resize();
}
//------------------------------------------------------------------------------