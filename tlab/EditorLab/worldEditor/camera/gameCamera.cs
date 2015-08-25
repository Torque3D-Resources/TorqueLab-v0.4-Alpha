//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function Lab::storeClientCameraState(%this,%client) {
	Lab.initialControlObject = %client.getControlObject();
	Lab.initialCameraObject = %client.getCameraObject();
	
	if (!isObject(%client.camera)) {	
		Lab.initialCamera = %client.camera;	
		Lab.initialCameraControlMode = %client.camera.controlMode;
		Lab.initialCameraDatablock = %client.camera.dataBlock;
		Lab.initialCameraPosition = %client.camera.position;
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::restoreClientCameraState(%this) {
	%client = LocalClientConnection;
	%client.setControlObject(Lab.initialControlObject);
	%client.setCameraObject(Lab.initialCameraObject);
	if (isObject(%client.camera)) {	
		%client.camera = Lab.initialCamera;
		%client.camera.controlMode = Lab.initialCameraControlMode;
		%client.camera.dataBlock = Lab.initialCameraDatablock;
		%client.camera.position = Lab.initialCameraPosition;		
	}
}
//------------------------------------------------------------------------------