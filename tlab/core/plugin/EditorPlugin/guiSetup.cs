//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
/// Callback right before the editor is closed.
function EditorPlugin::activateGui( %this ) {
	
	Lab.checkPluginTools();	

	if (%this.no3D)
		ECamViewGui.setState(false,true);
	else
		ECamViewGui.setState($Lab_CamViewEnabled);

	//Hide all the Guis for all plugins
	foreach(%gui in LabPluginGuiSet)
		%gui.setVisible(false);

	//Show only the Gui related to actiavted plugin
	%pluginGuiSet = %this.plugin@"_GuiSet";

	foreach(%gui in %pluginGuiSet) {
		//Don't show dialogs
		if (%gui.isDlg)
			continue;

		%gui.setVisible(true);
	}
}
//------------------------------------------------------------------------------