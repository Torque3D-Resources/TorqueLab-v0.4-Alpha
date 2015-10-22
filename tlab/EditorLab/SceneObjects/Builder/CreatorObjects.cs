//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

$SceneEditor_AddObjectLastZ = true;
$SceneEditor_AddObjectCurrentSel = true;
//==============================================================================
function Scene::getCreateObjectPosition() {
	%focusPoint = LocalClientConnection.getControlObject().getLookAtPoint();
	$SceneEd_SkipEditorDrop = true;	
	if ($SceneEditor_AddObjectCurrentSel){
		%curPos = EWorldEditor.getSelectedObject(0).position;
		devLog("Adding at current pos",%curPos);
		if (%curPos !$= "")
			return %curPos;
	}
	if( %focusPoint $= "" )
		return "0 0 0";
	%position =  getWord( %focusPoint, 1 ) SPC getWord( %focusPoint, 2 ) SPC getWord( %focusPoint, 3 );	
	if ($SceneEditor_AddObjectLastZ){		
		if ($WorldEditor_LastSelZ !$= "")
			%position.z = $WorldEditor_LastSelZ;
		devLog("Using last Z:",	$WorldEditor_LastSelZ,"Pos",%position);
		return %position;
	}
	$SceneEd_SkipEditorDrop = false;	
	return %position;
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::onObjectCreated( %this, %objId,%noDrop ) {
	// Can we submit an undo action?
	if ( isObject( %objId ) )
		MECreateUndoAction::submit( %objId );

	SceneEditorTree.clearSelection();
	EWorldEditor.clearSelection();
	EWorldEditor.selectObject( %objId );
	// When we drop the selection don't store undo
	// state for it... the creation deals with it.
	if (!%noDrop)
		EWorldEditor.dropSelection( true );
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::onFinishCreateObject( %this, %objId ) {
	SceneEd.objectGroup.add( %objId );

	if( %objId.isMemberOfClass( "SceneObject" ) ) {
		%objId.position = %this.getCreateObjectPosition();
		//flush new position
		%objId.setTransform( %objId.getTransform() );
	}

	%this.onObjectCreated( %objId );
}
//------------------------------------------------------------------------------
//==============================================================================
//Scene.createSimGroup();
function Scene::createSimGroup( %this ) {
	if ( !$missionRunning )
		return;

	

	
	%addToGroup = SceneEd.getActiveSimGroup();
	%objId = new SimGroup() {
		internalName = getUniqueInternalName( %this.objectGroup.internalName, MissionGroup, true );
		position = %this.getCreateObjectPosition();
		parentGroup = %addToGroup;
	};
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::createStatic( %this, %file ) {
	if ( !$missionRunning )
		return;
	
	%addToGroup = SceneEd.getActiveSimGroup();
	%objId = new TSStatic() {
		shapeName = %file;
		position = %this.getCreateObjectPosition();
		parentGroup = %addToGroup;
	};
	devLog("SkipDrop",$SceneEd_SkipEditorDrop,"Pos",%objId.position);
	%this.onObjectCreated( %objId,$SceneEd_SkipEditorDrop );
	
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::createPrefab( %this, %file ) {
	if ( !$missionRunning )
		return;

	

	if( !isObject(%this.objectGroup) )
		SceneEd.setNewObjectGroup( MissionGroup );

	%objId = new Prefab() {
		filename = %file;
		position = %this.getCreateObjectPosition();
		parentGroup = %this.objectGroup;
	};
	devLog("SkipDrop",$SceneEd_SkipEditorDrop,"Pos",%objId.position);
	%this.onObjectCreated( %objId );
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::createObject( %this, %cmd ) {
	if ( !$missionRunning )
		return;

	

	if( !isObject(%this.objectGroup) )
		SceneEd.setNewObjectGroup( MissionGroup );
devLog("SkipDrop",$SceneEd_SkipEditorDrop,"Pos",%objId.position);
	pushInstantGroup();
	%objId = eval(%cmd);
	popInstantGroup();

	if( isObject( %objId ) )
		%this.onFinishCreateObject( %objId );

	return %objId;
}
//------------------------------------------------------------------------------
//==============================================================================
function Scene::createMesh( %this, %file ) {
	if ( !$missionRunning )
		return;
	
	%addToGroup = SceneEd.getActiveSimGroup();
	%objId = new TSStatic() {
		shapeName = %file;
		position = %this.getCreateObjectPosition();
		parentGroup = %addToGroup;
		internalName = fileBase(%file);
	};
	%this.onObjectCreated( %objId,$SceneEd_SkipEditorDrop );	
}
//------------------------------------------------------------------------------