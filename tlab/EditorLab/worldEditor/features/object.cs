//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================


function EditorMount() {
	echo( "EditorMount" );
	%size = EWorldEditor.getSelectionSize();

	if ( %size != 2 )
		return;

	%a = EWorldEditor.getSelectedObject(0);
	%b = EWorldEditor.getSelectedObject(1);
	//%a.mountObject( %b, 0 );
	EWorldEditor.mountRelative( %a, %b );
}

function EditorUnmount() {
	echo( "EditorUnmount" );
	%obj = EWorldEditor.getSelectedObject(0);
	%obj.unmount();
}
//------------------------------------------------------------------------------



//==============================================================================
// Remove selected objects from their group (delete group if empty)
function delSelect(%this) {	
	if (!isObject("WorldEditorQuickGroup"))
		$QuickGroup = newSimSet("WEditorQuickGroup");
	
	WEditorQuickGroup.clear();
	%count = EWorldEditor.getSelectionSize();
	
	for( %i=0; %i<%count; %i++) {
		%obj = EWorldEditor.getSelectedObject( %i );
		WEditorQuickGroup.add(%obj);		
	}

	EWorldEditor.clearSelection();
	WEditorQuickGroup.deleteAllObjects();
}
//------------------------------------------------------------------------------

//==============================================================================
// Remove selected objects from their group (delete group if empty)
function cloneSelect(%this) {	
	if (!isObject("WorldEditorQuickGroup"))
		$QuickGroup = newSimSet("WEditorQuickGroup");
	
	WEditorQuickGroup.clear();
	%count = EWorldEditor.getSelectionSize();
	
	for( %i=0; %i<%count; %i++) {
		%obj = EWorldEditor.getSelectedObject( %i );
		WEditorQuickGroup.add(%obj.deepClone());		
	}

	EWorldEditor.clearSelection();
	foreach(%obj in WEditorQuickGroup)	
		EWorldEditor.selectObject(%obj);
		
	WEditorQuickGroup.clear();
}
//------------------------------------------------------------------------------