//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function MaterialLabGui::prepareActiveObject( %this, %override ) {
	%obj = $Lab::materialLabList;
	
	$MLP_BaseObjectPath = "";
	$MLP_BaseObject = "";
	if (isObject(MaterialLabGui.currentObject)){
		$MLP_BaseObject = MaterialLabGui.currentObject;
		$MLP_BaseObjectPath  = MaterialLabGui.currentObject.shapeName;
	}
	
	if( MaterialLabGui.currentObject == %obj && !%override)
		return;

	// TSStatics and ShapeBase objects should have getModelFile methods
	if( %obj.isMethod( "getModelFile" ) ) {
		MaterialLabGui.currentObject = %obj;
		SubMaterialSelector.clear();
		MaterialLabGui.currentMeshMode = "Model";
		MaterialLabGui.setMode();

		for(%j = 0; %j < MaterialLabGui.currentObject.getTargetCount(); %j++) {
			%target = MaterialLabGui.currentObject.getTargetName(%j);
			%count = SubMaterialSelector.getCount();
			SubMaterialSelector.add(%target);
		}
	} else { // Other classes that support materials if possible
		%canSupportMaterial = false;

		for( %i = 0; %i < %obj.getFieldCount(); %i++ ) {
			%fieldName = %obj.getField(%i);

			if( %obj.getFieldType(%fieldName) !$= "TypeMaterialName" )
				continue;

			if( !%canSupportMaterial ) {
				MaterialLabGui.currentObject = %obj;
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

		MaterialLabGui.currentMeshMode = "EditorShape";
		MaterialLabGui.setMode();
	}

	%id = SubMaterialSelector.findText( MaterialLabGui.currentMaterial.mapTo );

	if( %id != -1 )
		SubMaterialSelector.setSelected( %id );
	else
		SubMaterialSelector.setSelected(0);
}
//------------------------------------------------------------------------------
