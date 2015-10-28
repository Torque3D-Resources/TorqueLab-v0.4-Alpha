//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



function SceneInspectorBase::onAdd( %this ) {
	Scene.baseInspectors = strAddWord(Scene.baseInspectors,%this.getId(),true);
}

//==============================================================================
function SceneInspectorBase::onInspectorFieldModified( %this, %object, %fieldName, %arrayIndex, %oldValue, %newValue ) {
	
	// The instant group will try to add our
	// UndoAction if we don't disable it.
	pushInstantGroup();
	%nameOrClass = %object.getName();

	if ( %nameOrClass $= "" )
		%nameOrClass = %object.getClassname();

	%action = new InspectorFieldUndoAction() {
		actionName = %nameOrClass @ "." @ %fieldName @ " Change";
		objectId = %object.getId();
		fieldName = %fieldName;
		fieldValue = %oldValue;
		arrayIndex = %arrayIndex;
		inspectorGui = %this;
	};

	// If it's a datablock, initiate a retransmit.  Don't do so
	// immediately so as the actual field value will only be set
	// by the inspector code after this method has returned.

	if( %object.isMemberOfClass( "SimDataBlock" ) )
		%object.schedule( 1, "reloadOnLocalClient" ); 

	// Restore the instant group.
	popInstantGroup();
	%action.addToManager( Editor.getUndoManager() );
	EWorldEditor.isDirty = true;

	// Update the selection
	if(EWorldEditor.getSelectionSize() > 0 && (%fieldName $= "position" || %fieldName $= "rotation" || %fieldName $= "scale")) {
		EWorldEditor.invalidateSelectionCentroid();
	}
	if (%object.getClassName() $= "LevelInfo"){
		devLog("onInspectorFieldModified LevelInfo",%field);
		if (%field $= "LevelEnvMap")
			SceneInspectorBase.schedule(500,"envHack",true);
		
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::onFieldSelected( %this, %fieldName, %fieldTypeStr, %fieldDoc ) {
	SceneFieldInfoControl.setText( "<font:ArialBold:14>" @ %fieldName @ "<font:ArialItalic:14> (" @ %fieldTypeStr @ ") " NL "<font:Arial:14>" @ %fieldDoc );
}
//------------------------------------------------------------------------------
//==============================================================================
// The following three methods are for fields that edit field value live and thus cannot record
// undo information during edits.  For these fields, undo information is recorded in advance and
// then either queued or disarded when the field edit is finished.

function SceneInspectorBase::onInspectorPreFieldModification( %this, %fieldName, %arrayIndex ) {
	pushInstantGroup();
	%undoManager = Editor.getUndoManager();
	%numObjects = %this.getNumInspectObjects();

	if( %numObjects > 1 )
		%action = %undoManager.pushCompound( "Multiple Field Edit" );

	for( %i = 0; %i < %numObjects; %i ++ ) {
		%object = %this.getInspectObject( %i );
		%nameOrClass = %object.getName();

		if ( %nameOrClass $= "" )
			%nameOrClass = %object.getClassname();

		%undo = new InspectorFieldUndoAction() {
			actionName = %nameOrClass @ "." @ %fieldName @ " Change";
			objectId = %object.getId();
			fieldName = %fieldName;
			fieldValue = %object.getFieldValue( %fieldName, %arrayIndex );
			arrayIndex = %arrayIndex;
			inspectorGui = %this;
		};

		if( %numObjects > 1 )
			%undo.addToManager( %undoManager );
		else {
			%action = %undo;
			break;
		}
	}

	%this.currentFieldEditAction = %action;
	popInstantGroup();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::onInspectorPostFieldModification( %this ) {
	if( %this.currentFieldEditAction.isMemberOfClass( "CompoundUndoAction" ) ) {
		// Finish multiple field edit.
		Editor.getUndoManager().popCompound();
	} else {
		// Queue single field undo.
		%this.currentFieldEditAction.addToManager( Editor.getUndoManager() );
	}

	%this.currentFieldEditAction = "";
	EWorldEditor.isDirty = true;
	
	%obj = %this.getInspectObject( 0 );
	if (%obj.getClassName() $= "LevelInfo"){
		devLog("onInspectorPostFieldModification LevelInfo");
		%tmpObj = new EnvVolume("TmpEnvVolume");
		delObj(%tmpObj);
		
	}
}
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function SceneInspectorBase::onInspectorDiscardFieldModification( %this ) {
	%this.currentFieldEditAction.undo();

	if( %this.currentFieldEditAction.isMemberOfClass( "CompoundUndoAction" ) ) {
		// Multiple field editor.  Pop and discard.
		Editor.getUndoManager().popCompound( true );
	} else {
		// Single field edit.  Just kill undo action.
		%this.currentFieldEditAction.delete();
	}

	%this.currentFieldEditAction = "";
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::inspect( %this, %obj ) {
	//echo( "inspecting: " @ %obj );
	%name = "";

	if ( isObject( %obj ) )
		%name = %obj.getName();
	else
		SceneFieldInfoControl.setText( "" );
		
	if (%obj.getClassName() $= "LevelInfo"){
		devLog("Inspecting LevelInfo");
	}

	//InspectorNameEdit.setValue( %name );
	Parent::inspect( %this, %obj );
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::onBeginCompoundEdit( %this ) {
	Editor.getUndoManager().pushCompound( "Multiple Field Edit" );
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::onEndCompoundEdit( %this ) {
	Editor.getUndoManager().popCompound();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneInspectorBase::onCancelCompoundEdit( %this ) {
	Editor.getUndoManager().popCompound( true );
}
//------------------------------------------------------------------------------