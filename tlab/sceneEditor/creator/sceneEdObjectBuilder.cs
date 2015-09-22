//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function ObjectBuilderGui::buildBotSpawn(%this)
{
   %this.objectClassName = "BotSpawnSphere";
   %this.addField("dataBlock",    "TypeDataBlock", "dataBlock",   "MissionMarkerData BotSpawnMarker");
   %this.addField("radius",       "TypeFloat",     "Radius",        1);
   %this.addField("sphereWeight", "TypeFloat",     "Sphere Weight", 1);

   %this.addField("spawnClass",     "TypeString",    "Spawn Class", "Player");
   %this.addField("spawnDatablock", "TypeDataBlock", "Spawn Data", "PlayerData DefaultPlayerData");

   if( SceneCreatorWindow.objectGroup.getID() == MissionGroup.getID() )
   {
   	if( !isObject("BotStuff") )
         MissionGroup.add( new SimGroup("BotStuff") );
         
      if( !isObject("BotSpawns") )
         BotsStuff.add( new SimGroup("BotSpawns") );
      %this.objectGroup = "BotSpawns";
   }

   %this.process();
}

function ObjectBuilderGui::buildBotGoal(%this)
{
    %this.objectClassName = "BotGoalPoint";
   %this.addField("dataBlock",    "TypeDataBlock", "dataBlock",   "MissionMarkerData BotGoalMarker");
 
	 if( SceneCreatorWindow.objectGroup.getID() == MissionGroup.getID() )
   {
   	if( !isObject("BotStuff") )
         MissionGroup.add( new SimGroup("BotStuff") );
         
      if( !isObject("BotGoals") )
         BotsStuff.add( new SimGroup("BotGoals") );
      %this.objectGroup = "BotSpawns";
   }
  

   %this.process();
}

function ObjectBuilderGui::buildPlayerDropPoint(%this)
{
   %this.objectClassName = "SpawnSphere";
   %this.addField("dataBlock",    "TypeDataBlock", "dataBlock",   "MissionMarkerData SpawnSphereMarker");
   %this.addField("radius",       "TypeFloat",     "Radius",        1);
   %this.addField("sphereWeight", "TypeFloat",     "Sphere Weight", 1);

   %this.addField("spawnClass",     "TypeString",    "Spawn Class", "Player");
   %this.addField("spawnDatablock", "TypeDataBlock", "Spawn Data", "PlayerData DefaultPlayerData");

   if( SceneCreatorWindow.objectGroup.getID() == MissionGroup.getID() )
   {
      if( !isObject("Spawnpoints") )
         MissionGroup.add( new SimGroup("Spawnpoints") );
      %this.objectGroup = "Spawnpoints";
   }

   %this.process();
}

function ObjectBuilderGui::buildObserverDropPoint(%this)
{
   %this.objectClassName = "SpawnSphere";
   %this.addField("dataBlock",    "TypeDataBlock", "dataBlock",   "MissionMarkerData SpawnSphereMarker");
   %this.addField("radius",       "TypeFloat",     "Radius",        1);
   %this.addField("sphereWeight", "TypeFloat",     "Sphere Weight", 1);

   %this.addField("spawnClass",     "TypeString",    "Spawn Class", "Camera");
   %this.addField("spawnDatablock", "TypeDataBlock", "Spawn Data", "CameraData Observer");

   if( SceneCreatorWindow.objectGroup.getID() == MissionGroup.getID() )
   {
      if( !isObject("ObserverDropPoints") )
         MissionGroup.add( new SimGroup("ObserverDropPoints") );
      %this.objectGroup = "ObserverDropPoints";
   }

   %this.process();
}