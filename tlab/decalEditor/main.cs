//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function initializeDecalEditor() {
	echo(" % - Initializing Decal Editor");
	$decalDataFile = "art/decals/managedDecalData.cs";
	
	execDecalEd(true);
	
	Lab.createPlugin("DecalEditor");
	// Add ourselves to EditorGui, where all the other tools reside
	Lab.addPluginEditor("DecalEditor",   DecalEditorGui);
	Lab.addPluginGui("DecalEditor",DecalEditorTools);
	Lab.addPluginPalette("DecalEditor",   DecalEditorPalette);
	DecalEditorTabBook.selectPage( 0 );
	
	DecalEditorPlugin.editorGui = DecalEditorGui;
	%map = new ActionMap();
	%map.bindCmd( keyboard, "5", "EDecalEditorAddDecalBtn.performClick();", "" );
	%map.bindCmd( keyboard, "1", "EDecalEditorSelectDecalBtn.performClick();", "" );
	%map.bindCmd( keyboard, "2", "EDecalEditorMoveDecalBtn.performClick();", "" );
	%map.bindCmd( keyboard, "3", "EDecalEditorRotateDecalBtn.performClick();", "" );
	%map.bindCmd( keyboard, "4", "EDecalEditorScaleDecalBtn.performClick();", "" );
	DecalEditorPlugin.map = %map;
	new PersistenceManager( DecalPMan );
}

function execDecalEd(%loadGui) {

	if (%loadGui){
		exec( "tlab/decalEditor/gui/decalEditorGui.gui" );
	exec( "tlab/decalEditor/gui/decalEditorTools.gui" );
	exec( "tlab/decalEditor/gui/decalEditorPalette.gui" );
	}
	
	exec( "tlab/decalEditor/decalEditorGui.cs" );
	exec( "tlab/decalEditor/decalEditorActions.cs" );
	exec( "tlab/decalEditor/DecalEditorPlugin.cs" );
	exec( "tlab/decalEditor/DecalEditorParams.cs" );
	execPattern("tlab/decalEditor/instance/*.cs" );
	execPattern( "tlab/decalEditor/library/*.cs" );
}
function destroyDecalEditor() {
}

// JCF: helper for during development
function reinitDecalEditor() {
	exec( "./main.cs" );
	exec( "./decalEditor.cs" );
	exec( "./decalEditorGui.cs" );
}

