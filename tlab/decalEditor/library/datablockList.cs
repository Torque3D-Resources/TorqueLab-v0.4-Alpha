//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function DecalDataList::onSelect( %this, %id, %text ) {
	%obj = %this.getItemObject( %id );
	DecalEditorGui.currentDecalData = %obj;
	%itemNum = DecalDataList.getSelectedItem();

	if ( %itemNum == -1 )
		return;

	%data = DecalDataList.getItemObject( %itemNum );
	// Update the materialEditorList
	$Lab::materialEditorList = %data.getId();
	//Canvas.pushDialog( DecalEditDlg );
	DecalInspector.inspect( %data );
	DecalEditorGui.updateDecalPreview( %data.material );
}
//------------------------------------------------------------------------------