//==============================================================================
// TorqueLab -> GuiEditor Template Manager - Editor
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function GuiEdTemplateEditor::onWake(%this) {
	devLog("GuiEdTemplateEditor::onWake(%this)");

	if (GuiEditor.lastContent.getName() $= "GuiEdTemplateEditor") {
		devLog("Not in editor");
		return;
	}

	if (!GuiEdTemplateManager.isAwake())
		TplManager.schedule(200,"showBrowser");
}
//==============================================================================
//==============================================================================
function GuiEdTemplateEditor::onSleep(%this) {
	devLog("GuiEdTemplateEditor::onSleep(%this)");

	if (GuiEdTemplateManager.isAwake())
		TplManager.hideBrowser();
}
//==============================================================================

//==============================================================================
function TplManager::initEditor(%this) {
}
//==============================================================================

