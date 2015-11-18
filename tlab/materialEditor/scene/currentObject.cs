//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function MaterialEditorGui::prepareActiveObject( %this, %override ) {
	%obj = $Lab::materialEditorList;
	$MEP_BaseObjectPath = "";
	$MEP_BaseObject = "";

	if (isObject(MaterialEditorGui.currentObject)) {
		$MEP_BaseObject = MaterialEditorGui.currentObject;
		$MEP_BaseObjectPath  = MaterialEditorGui.currentObject.shapeName;
	}

	if( MaterialEditorGui.currentObject == %obj && !%override)
		return;

	// TSStatics and ShapeBase objects should have getModelFile methods
	if( %obj.isMethod( "getModelFile" ) ) {
		MaterialEditorGui.currentObject = %obj;
		SubMaterialSelector.clear();
		MaterialEditorGui.currentMeshMode = "Model";
		MaterialEditorGui.setMode();

		for(%j = 0; %j < MaterialEditorGui.currentObject.getTargetCount(); %j++) {
			%target = MaterialEditorGui.currentObject.getTargetName(%j);
			%count = SubMaterialSelector.getCount();
			if (strFind(%target,"ColorEffectR") && $MatEd_HideColorMaterial)
				continue;
				
			SubMaterialSelector.add(%target);
		}
	} else { // Other classes that support materials if possible
		%canSupportMaterial = false;

		for( %i = 0; %i < %obj.getFieldCount(); %i++ ) {
			%fieldName = %obj.getField(%i);

			if( %obj.getFieldType(%fieldName) !$= "TypeMaterialName" )
				continue;

			if( !%canSupportMaterial ) {
				MaterialEditorGui.currentObject = %obj;
				SubMaterialSelector.clear();
				SubMaterialSelector.add(%fieldName, 0);
			} else {
				%count = SubMaterialSelector.getCount();
				SubMaterialSelector.add(%fieldName, %count);
			}

			%canSupportMaterial = true;
		}

		if( !%canSupportMaterial ) // Non-relevant classes get returned
			return;

		MaterialEditorGui.currentMeshMode = "EditorShape";
		MaterialEditorGui.setMode();
	}

	%id = SubMaterialSelector.findText( MaterialEditorGui.currentMaterial.mapTo );

	if( %id != -1 )
		SubMaterialSelector.setSelected( %id );
	else
		SubMaterialSelector.setSelected(0);
}
//------------------------------------------------------------------------------
