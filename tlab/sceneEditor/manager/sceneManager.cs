//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================



function SceneAddSimGroupButton::onClick( %this ) {
	devLog("SceneAddSimGroupButton::onClick??");
	EWorldEditor.addSimGroup();
}
function SceneAddSimGroupButton::onDefaultClick( %this ) {
	devLog("SceneAddSimGroupButton::onDefaultClick??");
	EWorldEditor.addSimGroup();
}

function SceneAddSimGroupButton::onCtrlClick( %this ) {
	devLog("SceneAddSimGroupButton::onCtrlClick??");
	EWorldEditor.addSimGroup( true );
}


function SceneEditorPlugin::toggleBuildMode( %this ) {
	%rowCount = getWordCount(SceneEditorTools.rows);

	if (%rowCount > 1){		
		SceneEditorTools.lastRows = SceneEditorTools.rows;
		
		SceneEditorTools.rows = "0";
		SceneEditorTools.updateSizes();
	}
	else {
		if (getWordCount(SceneEditorTools.lastRows) <= 1)
			SceneEditorTools.lastRows = "0 200";
		
		SceneEditorTools.rows = SceneEditorTools.lastRows;
		SceneEditorTools.updateSizes();
	}
}
