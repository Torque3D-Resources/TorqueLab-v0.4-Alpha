//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SceneCreatorWindow::beginGroup( %this, %group ) {
	%this.currentGroup = %group;
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneCreatorWindow::endGroup( %this, %group ) {
	%this.currentGroup = "";
}
//------------------------------------------------------------------------------
function SceneCreatorWindow::registerObjects( %this ) {

	%this.beginGroup( "Environment" );
	// Removed Prefab as there doesn't really seem to be a point in creating a blank one
	//%this.registerMissionObject( "Prefab",              "Prefab" );
	%this.registerMissionObject( "SkyBox",              "Sky Box" );
	%this.registerMissionObject( "CloudLayer",          "Cloud Layer" );
	%this.registerMissionObject( "BasicClouds",         "Basic Clouds" );
	%this.registerMissionObject( "ScatterSky",          "Scatter Sky" );
	%this.registerMissionObject( "Sun",                 "Basic Sun" );
	%this.registerMissionObject( "Lightning" );
	%this.registerMissionObject( "WaterBlock",          "Water Block" );
	%this.registerMissionObject( "SFXEmitter",          "Sound Emitter" );
	%this.registerMissionObject( "Precipitation" );
	%this.registerMissionObject( "ParticleEmitterNode", "Particle Emitter" );
	// Legacy features. Users should use Ground Cover and the Forest Editor.
	//%this.registerMissionObject( "fxShapeReplicator",   "Shape Replicator" );
	//%this.registerMissionObject( "fxFoliageReplicator", "Foliage Replicator" );
	%this.registerMissionObject( "PointLight",          "Point Light" );
	%this.registerMissionObject( "SpotLight",           "Spot Light" );
	%this.registerMissionObject( "GroundCover",         "Ground Cover" );
	%this.registerMissionObject( "TerrainBlock",        "Terrain Block" );
	%this.registerMissionObject( "GroundPlane",         "Ground Plane" );
	%this.registerMissionObject( "WaterPlane",          "Water Plane" );
	%this.registerMissionObject( "PxCloth",             "Cloth" );
	%this.registerMissionObject( "ForestWindEmitter",   "Wind Emitter" );
	%this.registerMissionObject( "DustEmitter", "Dust Emitter" );
	%this.registerMissionObject( "DustSimulation", "Dust Simulation" );
	%this.registerMissionObject( "DustEffecter", "Dust Effecter" );
	if (%this.isMethod("registerCustomsObjectsGroup"))
		%this.registerCustomsObjectsGroup("Environment");

	%this.endGroup();
	%this.beginGroup( "Level" );
	%this.registerMissionObject( "MissionArea",  "Mission Area" );
	%this.registerMissionObject( "Path" );
	%this.registerMissionObject( "Marker",       "Path Node" );
	%this.registerMissionObject( "Trigger" );
	%this.registerMissionObject( "PhysicalZone", "Physical Zone" );
	%this.registerMissionObject( "Camera" );
	%this.registerMissionObject( "LevelInfo",    "Level Info" );
	%this.registerMissionObject( "TimeOfDay",    "Time of Day" );
	%this.registerMissionObject( "Zone",         "Zone" );
	%this.registerMissionObject( "Portal",       "Zone Portal" );
	%this.registerMissionObject( "SpawnSphere",  "Player Spawn Sphere", "PlayerDropPoint" );
	%this.registerMissionObject( "SpawnSphere",  "Observer Spawn Sphere", "ObserverDropPoint" );
	%this.registerMissionObject( "SFXSpace",      "Sound Space" );
	%this.registerMissionObject( "OcclusionVolume", "Occlusion Volume" );
	%this.registerMissionObject("NavMesh", "Navigation mesh");
	%this.registerMissionObject("NavPath", "Path");
	
	if (%this.isMethod("registerCustomsObjectsGroup"))
		%this.registerCustomsObjectsGroup("Level");
	%this.endGroup();
	// andrewmac: PhysX 3.3
	%this.beginGroup( "PhysX 3.3" );
	%this.registerMissionObject( "Px3Cloth",        "Cloth Plane" );
	%this.registerMissionObject( "Px3Static",       "Static Shape" );
	%this.endGroup();
	%this.beginGroup( "System" );
	%this.registerMissionObject( "SimGroup" );
	%this.endGroup();
	%this.beginGroup( "ExampleObjects" );
	%this.registerMissionObject( "RenderObjectExample" );
	%this.registerMissionObject( "RenderMeshExample" );
	%this.registerMissionObject( "RenderShapeExample" );
	%this.endGroup();
	
	if (%this.isMethod("registerCustomsGroups"))
		%this.registerCustomsGroups();
}

function SceneCreatorWindow::registerMissionObject( %this, %class, %name, %buildfunc, %group ) {
	if( !isClass(%class) )
		return;

	if ( %name $= "" )
		%name = %class;

	if ( %this.currentGroup !$= "" && %group $= "" )
		%group = %this.currentGroup;

	if ( %class $= "" || %group $= "" ) {
		warn( "SceneCreatorWindow::registerMissionObject, invalid parameters!" );
		return;
	}

	%args = new ScriptObject();
	%args.val[0] = %class;
	%args.val[1] = %name;
	%args.val[2] = %buildfunc;
	%this.array.push_back( %group, %args );
}