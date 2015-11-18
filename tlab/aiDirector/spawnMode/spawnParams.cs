//==============================================================================
// TorqueLab Editor ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function ADE::initSpawnSettingsParams(%this) {
	%arCfg = createParamsArray("ADE_Spawn",ADE_SpawnSettingsStack);
	%arCfg.updateFunc = "ADE.updateSpawnParam";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.noDefaults = true;
	//%arCfg.manualObj = Wheeled_Vehicle_vm_clone;
	//%arCfg.noDirectSync = true;
	%arCfg.group[%gid++] = "General";
	%arCfg.setVal("spawnDatablock",  "spawnDatablock" TAB "TextEdit" TAB "" TAB "ADE.selectedSpawn" TAB %gid);
	%arCfg.setVal("defaultBehavior",  "defaultBehavior" TAB "TextEdit" TAB "" TAB "ADE.selectedSpawn" TAB %gid);
	%arCfg.setVal("superClass",  "superClass" TAB "TextEdit" TAB "" TAB "ADE.selectedSpawn" TAB %gid);
	%arCfg.group[%gid++] = "Performance And Driving";
	%arCfg.setVal("spawnDelay",  "spawnDelay" TAB "SliderEdit" TAB "range>>0 1000;;tickAt>>10" TAB "ADE.selectedSpawn" TAB %gid);
	buildParamsArray(%arCfg,false);
	ADE.spawnArray = %arCfg;
}
//------------------------------------------------------------------------------
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function ADE::updateSpawnParam( %this,%field,%value,%ctrl,%array,%arg1,%arg2 ) {
	logd("ADE::updateSpawnParam( %this,%field,%value,%ctrl,%array,%arg1,%arg2 )",%field,%value,%ctrl,%array,%arg1,%arg2);
	%data = %array.getVal(%field);
	%updObj = getField(%data,%array.syncObjsField);
	%isDirty = LabObj.set(%updObj,%field,%value);
}
//------------------------------------------------------------------------------