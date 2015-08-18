//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
//SEP_ScatterSkyManager.buildParams();
function sepVM::buildWheeledParams( %this ) {
	%arCfg = createParamsArray("sepVM_WheeledVehicle",sepVM_WheeledVehicleData);
	%arCfg.updateFunc = "sepVM.updateParam";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.noDefaults = true;
	%arCfg.manualObj = Wheeled_Vehicle_vm_clone;
	%arCfg.noDirectSync = true;
		
	%arCfg.group[%gid++] = "General" TAB "Stack StackA";
	%arCfg.setVal("shapeFile",  "shapeFile" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("superClass",  "superClass" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("wheelDrive",  "wheelDrive" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);

	
	%arCfg.group[%gid++] = "Performance And Driving" TAB "Stack StackA";
	%arCfg.setVal("engineTorque",  "engineTorque" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("engineBrake",  "engineBrake" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("maxWheelSpeed",  "maxWheelSpeed" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("brakeTorque",  "brakeTorque" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	
	%arCfg.setVal("jetForce",  "jetForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("jetEnergyDrain",  "jetEnergyDrain" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("minJetEnergy",  "minJetEnergy" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("maxSteeringAngle",  "jetEnergyDrain" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	
	%arCfg.group[%gid++] = "Physic" TAB "Stack StackA";
	%arCfg.setVal("massCenter",  "massCenter" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("bodyRestitution",  "bodyRestitution" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("bodyFriction",  "bodyFriction" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("integration",  "integration" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("mass",  "mass" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("drag",  "drag" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);

	
	%arCfg.group[%gid++] = "Impact and damage" TAB "Stack StackB";
	%arCfg.setVal("minImpactSpeed",  "minImpactSpeed" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("softImpactSpeed",  "softImpactSpeed" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("hardImpactSpeed",  "hardImpactSpeed" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("contactTol",  "contactTol" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("maxEnergy",  "maxEnergy" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	
	
	%arCfg.group[%gid++] = "Sounds and FX" TAB "Stack StackB";	
	%arCfg.setVal("engineSound",  "engineSound" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("tireEmitter",  "tireEmitter" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("dustEmitter",  "dustEmitter" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("softImpactSound",  "softImpactSound" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("hardImpactSound",  "hardImpactSound" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	
  
 %arCfg.group[%gid++] = "Camera" TAB "Stack StackB";
	%arCfg.setVal("cameraRoll",  "cameraRoll" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("cameraLag",  "cameraLag" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
  %arCfg.setVal("cameraDecay",  "cameraDecay" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("cameraOffset",  "cameraOffset" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("cameraMaxDist",  "cameraMaxDist" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
	%arCfg.setVal("cameraMinDist",  "cameraMinDist" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Vehicle_vm_clone" TAB %gid);
   
   buildParamsArray(%arCfg,false);
	sepVM.wheeledParamVehicle = %arCfg;
  
  %arCfg = createParamsArray("sepVM_WheeledWheels1",sepVM_WheelAndSpringData1);
	%arCfg.updateFunc = "sepVM.updateParam";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.noDefaults = true;	
	%arCfg.noDirectSync = true;
	%gid = 0;
	%arCfg.group[%gid++] = "Spring1" TAB "StackB";	
	%arCfg.setVal("length",  "length" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring1_vm_clone" TAB %gid);
	%arCfg.setVal("force",  "force" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring1_vm_clone" TAB %gid);
	%arCfg.setVal("damping",  "damping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring1_vm_clone" TAB %gid);
	%arCfg.setVal("antiSwayForce",  "antiSwayForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring1_vm_clone" TAB %gid);


	%arCfg.group[%gid++] = "Tire1" TAB "StackA";	
	%arCfg.setVal("shapeFile",  "shapeFile" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("staticFriction",  "staticFriction" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("kineticFriction",  "kineticFriction" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("lateralForce",  "lateralForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("lateralDamping",  "lateralDamping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("lateralRelaxation",  "lateralRelaxation" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalForce",  "longitudinalForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalDamping",  "longitudinalDamping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalRelaxation",  "longitudinalRelaxation" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire1_vm_clone" TAB %gid);
	
	buildParamsArray(%arCfg,false);
	sepVM.wheeledParamWheel1 = %arCfg;
	
	%arCfg = createParamsArray("sepVM_WheeledWheels2",sepVM_WheelAndSpringData2);
	%arCfg.updateFunc = "sepVM.updateParam";
	%arCfg.style = "StyleA";
	%arCfg.useNewSystem = true;
	%arCfg.noDefaults = true;
	%arCfg.noDirectSync = true;
	%gid = 0;
	%arCfg.group[%gid++] = "Spring2" TAB "StackB";	
	%arCfg.setVal("length",  "length" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring2_vm_clone" TAB %gid);
	%arCfg.setVal("force",  "force" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring2_vm_clone" TAB %gid);
	%arCfg.setVal("damping",  "damping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring2_vm_clone" TAB %gid);
	%arCfg.setVal("antiSwayForce",  "antiSwayForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Spring2_vm_clone" TAB %gid);


	%arCfg.group[%gid++] = "Tire2" TAB "StackA";		
	%arCfg.setVal("shapeFile",  "shapeFile" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("staticFriction",  "staticFriction" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("kineticFriction",  "kineticFriction" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("lateralForce",  "lateralForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("lateralDamping",  "lateralDamping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("lateralRelaxation",  "lateralRelaxation" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalForce",  "longitudinalForce" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalDamping",  "longitudinalDamping" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
	%arCfg.setVal("longitudinalRelaxation",  "longitudinalRelaxation" TAB "SliderEdit" TAB "range>>0 50;;tickAt>>0.1" TAB "Wheeled_Tire2_vm_clone" TAB %gid);
  
	buildParamsArray(%arCfg,false);
	sepVM.wheeledParamWheel2 = %arCfg;
}
//------------------------------------------------------------------------------
