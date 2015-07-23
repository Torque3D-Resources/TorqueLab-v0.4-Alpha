//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Manage Plugins Dialogs
//==============================================================================
//==============================================================================
function Lab::initAllPluginsDialogs( %this ) {
	%gameLabGuis = "";
	foreach(%gui in LabDialogGuiSet) {	
		foreach(%dlg in %gui){
			//If gameName is set, this gui is available in game (GameLabGui)			
			
			%dlg.visible = 0;
		}		
		%gui.initDialogs();
	}	
	Lab.initGameLabDialogs();
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::initGameLabDialogs( %this ) {
	GameLabGui.reset();
	%mainGui = GameLabGui;
	%menu = %mainGui-->dialogMenu;
	%menu.clear();
	%menu.add("Select a dialog" ,0);
	foreach(%gui in LabDialogGuiSet) {	
		foreach(%dlg in %gui){
			if (%dlg.gameName $="")
				continue;
			
			%menu.add(%dlg.gameName ,%dlg.getId());			
		}
	}
	%menu.setSelected(0);	
}
//------------------------------------------------------------------------------


//==============================================================================
// Manage Plugins Dialogs
//==============================================================================

//==============================================================================
function PluginDlg::initDialogs( %this ) {
	foreach(%obj in %this) {
		if (%obj.isMethod("initDialog"))
				%obj.initDialog();
	}

}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::onActivatedDialogs( %this ) {
	if (%this.isMethod("onActivated"))
			%this.onActivated();
				
	%this.visible = "0";			
	foreach(%obj in %this) {		
		
		if (%obj.isMethod("onActivated"))
				%obj.onActivated();
				
		if (%obj.alwaysOn $= "1" || %obj.alwaysOn){			
			%this.showDlg(%obj.internalName,true);
			%showThis = true;
			
		}		
		else {
			%obj.visible = "0";
		}
	}
	
	%this.checkState();
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::toggleDlg( %this, %dlg,%alwaysOn ) {
	%this.fitIntoParents();
	%dlgCtrl = %this.findObjectByInternalName(%dlg);
	
	if (!isObject(%dlgCtrl))
		return;

	if (%dlgCtrl.isVisible()) 		
		%this.hideDlg(%dlg);		
	else 
		%this.showDlg(%dlg,%alwaysOn);		
	
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::showDlg( %this, %dlg,%alwaysOn ) {
	%this.fitIntoParents();
	%dlgCtrl = %this.findObjectByInternalName(%dlg);
	%dlgCtrl.alwaysOn = %alwaysOn;
	if (!isObject(%dlgCtrl))
		return;
	if (!%dlgCtrl.isVisible()) 
		show(%dlgCtrl);
	
	show(%this);	
	if(%dlgCtrl.isMethod("onShow"))
		%dlgCtrl.onShow();
	
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::hideDlg( %this, %dlg ) {
	%this.fitIntoParents();
	%dlgCtrl = %this.findObjectByInternalName(%dlg);
	%this.hideDlgCtrl(%dlgCtrl);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::closeSelf( %this,%dlgCtrl ) {
	
	//Check is the dlg is used as GameLabGui
	if (%dlgCtrl.parentGroup $= GameLabGui){
		GameLabGui.closeAll();
	}
	%this.hideDlgCtrl(%dlgCtrl);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::hideDlgCtrl( %this, %dlgCtrl ) {
	if (!isObject(%dlgCtrl))
		return;
		
	%dlgCtrl.alwaysOn = false;	

	//if (!%dlgCtrl.isVisible()) 
		//return;
	hide(%dlgCtrl);
	if(%dlgCtrl.isMethod("onShow"))
		%dlgCtrl.onHide();	
	
	%this.checkState();
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::checkState( %this ) {
	%visibleCount = 0;
	%hiddenCount = 0;

	foreach(%ctrl in %this) {
		if (%ctrl.visible)
			%visibleCount++;
		else
			%hiddenCount++;
	}

	
	//If all are hidden, hide the dialog container
	if (%visibleCount == 0 && %this.visible)
		hide(%this);
	else if (!%this.visible)
		show(%this);
}
//------------------------------------------------------------------------------

//==============================================================================
function PluginDlg::onSleep( %this ) {
	foreach(%ctrl in %this)
		hide(%ctrl);
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::onPreEditorSave(%this) {	
	devLog("PluginDlg::onPreEditorSave",%this);
	//Check if a onPreEditorSave method is found in sub dialogs
	foreach(%ctrl in %this){
		if (%ctrl.isMethod("onPreEditorSave"))
			%ctrl.onPreEditorSave();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function PluginDlg::onPostEditorSave(%this) {	
	devLog("PluginDlg::onPostEditorSave",%this);
	//Check if a onPreEditorSave method is found in sub dialogs
	foreach(%ctrl in %this){
		if (%ctrl.isMethod("onPostEditorSave"))
			%ctrl.onPostEditorSave();
	}
}
//------------------------------------------------------------------------------
