//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function SceneEditorTree::rebuild( %this ) {
	%this.clear();
	%this.open(MissionGroup);
	%this.buildVisibleTree();
}
function SceneEditorTreeTabBook::onTabSelected( %this ) {
	if( SceneEditorTreeTabBook.getSelectedPage() == 0) {
		SceneTreeWindow-->DeleteSelection.visible = true;
		SceneTreeWindow-->LockSelection.visible = true;
		SceneTreeWindow-->AddSimGroup.visible = true;
	} else {
		SceneTreeWindow-->DeleteSelection.visible = false;
		SceneTreeWindow-->LockSelection.visible = false;
		SceneTreeWindow-->AddSimGroup.visible = false;
	}
}
function SceneEditorTree::toggleLock( %this ) {
	if(  SceneTreeWindow-->LockSelection.command $= "EWorldEditor.lockSelection(true); SceneEditorTree.toggleLock();" ) {
		SceneTreeWindow-->LockSelection.command = "EWorldEditor.lockSelection(false); SceneEditorTree.toggleLock();";
		SceneTreeWindow-->DeleteSelection.command = "";
	} else {
		SceneTreeWindow-->LockSelection.command = "EWorldEditor.lockSelection(true); SceneEditorTree.toggleLock();";
		SceneTreeWindow-->DeleteSelection.command = "EditorMenuEditDelete();";
	}
}

function SceneEditorTree::onMouseUp( %this,%hitItemId, %mouseClickCount ) {
	devLog("SceneEditorTree::onMouseUp( %this,%hitItemId, %mouseClickCount )",%hitItemId, %mouseClickCount);
	%obj = %this.getItemValue(%hitItemId);

	if (!isObject(%obj))
		return;

	switch$(%obj.getClassName()) {
	case "SimGroup":
		if(%mouseClickCount > 1) {
			%obj.treeExpanded = !%obj.treeExpanded;
			%this.expandItem(%hitItemId,%obj.treeExpanded);
		}
	}
}

function SceneEditorTree::onAddSelection(%this, %obj, %isLastSelection) {
	Parent::onAddSelection(%this, %obj, %isLastSelection);
	//Scene.onAddSelection(%obj, %isLastSelection);
	//EWorldEditor.selectObject( %obj );
	%selSize = EWorldEditor.getSelectionSize();
	%lockCount = EWorldEditor.getSelectionLockCount();

	if( %lockCount < %selSize ) {
		SceneTreeWindow-->LockSelection.setStateOn(0);
		SceneTreeWindow-->LockSelection.command = "EWorldEditor.lockSelection(true); SceneEditorTree.toggleLock();";
	} else if ( %lockCount > 0 ) {
		SceneTreeWindow-->LockSelection.setStateOn(1);
		SceneTreeWindow-->LockSelection.command = "EWorldEditor.lockSelection(false); SceneEditorTree.toggleLock();";
	}

	if( %selSize > 0 && %lockCount == 0 )
		SceneTreeWindow-->DeleteSelection.command = "EditorMenuEditDelete();";
	else
		SceneTreeWindow-->DeleteSelection.command = "";

	//if( %isLastSelection )
	//SceneInspector.addInspect( %obj );
	//else
	//SceneInspector.addInspect( %obj, false );
}
//------------------------------------------------------------------------------------
function CameraSpeedDropdownCtrlContainer::onWake(%this) {
	%this-->slider.setValue(CameraSpeedDropdownContainer-->textEdit.getText());
}

//------------------------------------------------------------------------------------
// Callbacks to close the dropdown slider controls like the camera speed,
// that are marked with this class name.

function EditorDropdownSliderContainer::onMouseDown(%this) {
	Canvas.popDialog(%this);
}

function EditorDropdownSliderContainer::onRightMouseDown(%this) {
	Canvas.popDialog(%this);
}
