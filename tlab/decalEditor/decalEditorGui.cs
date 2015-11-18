//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function DecalEditorGui::onWake( %this ) {
}
function DecalEditorGui::onSleep( %this ) {
}


// Stores the information when the gizmo is first used
function DecalEditorGui::prepGizmoTransform( %this, %decalId, %nodeDetails ) {
	DecalEditorGui.gizmoDetails = %nodeDetails;
}

// Activated in onMouseUp while gizmo is dirty
function DecalEditorGui::completeGizmoTransform( %this, %decalId, %nodeDetails ) {
	DecalEditorGui.doEditNodeDetails( %decalId, %nodeDetails, true );
}



function DecalEditorGui::paletteSync( %this, %mode ) {
	%evalShortcut = "EWToolsPaletteArray-->" @ %mode @ ".setStateOn(1);";
	eval(%evalShortcut);
}



function DecalEditorTabBook::onTabSelected( %this, %text, %idx ) {
	if( %idx == 0)
		%showInstance = true;
	else
		%showInstance = false;

	if( %idx == 0) {
		DecalPreviewWindow.text = "Instance Properties";
		DeleteDecalButton.tabSelected = %idx;
	} else {
		DecalPreviewWindow.text = "Template Properties";
		DeleteDecalButton.tabSelected = %idx;
	}

	DecalEditorTools-->TemplateProperties.setVisible(!%showInstance);
	DecalEditorTools-->TemplatePreview.setVisible(!%showInstance);
	DecalEditorWindow-->libraryStack.visible = !%showInstance;
	DecalEditorTools-->InstanceProperties.setVisible(%showInstance);
	DecalEditorTools-->InstancePreview.setVisible(%showInstance);
	DecalEditorWindow-->instanceStack.visible = %showInstance;
}






