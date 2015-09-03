//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function MatLab::prepareActiveObject( %this, %override ) {
	%obj = $Lab::materialLabList;
	
	$MLP_BaseObjectPath = "";
	$MLP_BaseObject = "";
	if (isObject(MatLab.currentObject)){
		$MLP_BaseObject = MatLab.currentObject;
		$MLP_BaseObjectPath  = MatLab.currentObject.shapeName;
	}
	
	if( MatLab.currentObject == %obj && !%override)
		return;

	// TSStatics and ShapeBase objects should have getModelFile methods
	if( %obj.isMethod( "getModelFile" ) ) {
		MatLab.currentObject = %obj;
		SubMaterialSelector.clear();
		MatLab.currentMeshMode = "Model";
		MatLab.setMode();

		for(%j = 0; %j < MatLab.currentObject.getTargetCount(); %j++) {
			%target = MatLab.currentObject.getTargetName(%j);
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
				MatLab.currentObject = %obj;
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

		MatLab.currentMeshMode = "EditorShape";
		MatLab.setMode();
	}

	%id = SubMaterialSelector.findText( MatLab.currentMaterial.mapTo );

	if( %id != -1 )
		SubMaterialSelector.setSelected( %id );
	else
		SubMaterialSelector.setSelected(0);
}
//------------------------------------------------------------------------------
