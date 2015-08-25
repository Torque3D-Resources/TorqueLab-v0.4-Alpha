//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


function DecalEditorGui::editNodeDetails( %this ) {
	%decalId = DecalEditorGui.selDecalInstanceId;

	if( %decalId == -1 )
		return;

	%nodeDetails = DecalEditorDetailContainer-->nodePosition.getText();
	%nodeDetails = %nodeDetails @ " " @ DecalEditorDetailContainer-->nodeTangent.getText();
	%nodeDetails = %nodeDetails @ " " @ DecalEditorDetailContainer-->nodeSize.getText();

	if( getWordCount(%nodeDetails) == 7 )
		DecalEditorGui.doEditNodeDetails( %decalId, %nodeDetails, false );
}


function DecalEditorGui::syncNodeDetails( %this ) {
	%decalId = DecalEditorGui.selDecalInstanceId;

	if( %decalId == -1 )
		return;

	%lookupName = DecalEditorGui.getDecalLookupName( %decalId );
	DecalEditorGui.updateInstancePreview( %lookupName.material );
	DecalEd_InstanceProperties-->instanceId.setText(%decalId @ " " @ %lookupName);
	%transformData = DecalEditorGui.getDecalTransform(%decalId);
	DecalEd_InstanceProperties-->nodePosition.setText(getWords(%transformData, 0, 2));
	DecalEd_InstanceProperties-->nodeTangent.setText(getWords(%transformData, 3, 5));
	DecalEd_InstanceProperties-->nodeSize.setText(getWord(%transformData, 6));
}

//DecalEditorGui.saveAllInstances();
function DecalEditorGui::saveAllInstances( %this ) {
	warnLog("Not saving yet");
}