//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
/// Callback when the tool is 'activated' by the WorldEditor
/// Push Gui's, stuff like that
function EditorPlugin::onActivated( %this,%simple ) {
	//if(isDemo())
	//startToolTime(%this.getName());
	//Reset some default Plugin values
	Lab.fitCameraGui = ""; //Used by GuiShapeEdPreview to Fit camera on object
	//Call the Plugin Object onActivated method if exist

	//%this.activateGui();

	if (%this.no3D)
		ECamViewGui.setState(false,true);
	else
		ECamViewGui.setState($Lab_CamViewEnabled);

	if (!%simple) {
		//Hide all the Guis for all plugins
		foreach(%gui in LabPluginGuiSet) {
			%gui.setVisible(false);
		}
	}

	EWToolsPaletteContainer.visible = !%this.noPalette;

	//Show only the Gui related to actiavted plugin
	if (!%simple) {
		%pluginGuiSet = %this.plugin@"_GuiSet";

		if (!isObject(%pluginGuiSet))
			warnLog("Invalid Plugin GuiSet for update in EditorPlugin::onActivated, tried:",%pluginGuiSet);
		else
			foreach(%gui in %pluginGuiSet) {
				//Don't show dialogs
				if (%gui.isDlg)
					continue;

				%gui.setVisible(true);
			}
	}

	%this.isActivated = true;

	if(isObject(%this.map))
		%this.map.push();

	if( isObject( %this.editorGui ) ) {
		show(%this.editorGui);
		%this.editorGui.setDisplayType( Lab.cameraDisplayType );
		%this.editorGui.setOrthoFOV( Lab.orthoFOV );
		// Lab.syncCameraGui();
	} else {
		warnLog("The plugin",%this.displayName,"have no editor GUI assigned. Using default World Editor GUI");
	}

	//Lab.activatePluginToolbar(%this);
	Lab.setToolbarPluginTrash(%this);

	if (isObject(%this.dialogs))
		%this.dialogs.onActivatedDialogs();

	Lab.checkPluginTools();
	//SceneBrowser.filterPlugin(%this);
}
//------------------------------------------------------------------------------
//==============================================================================
/// Callback when the tool is 'deactivated' / closed by the WorldEditor
/// Pop Gui's, stuff like that
function EditorPlugin::onDeactivated( %this,%newEditor ) {
	endToolTime(%this.getName());

	if(isObject(%this.map))
		%this.map.pop();

	hide(%this.editorGui);
	%this.isActivated = false;
	//Lab.deactivatePluginToolbar(%this);
}
//------------------------------------------------------------------------------