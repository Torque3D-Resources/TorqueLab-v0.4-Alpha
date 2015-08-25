//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function Lab::toggleControlObject(%this) {
	if (!isObject(%this.gameControlObject)) {
		warnLog("There's no Game control object stored:",%this.gameControlObject);
		return;
	}

	//If Client is controlling game object, set control camera, else do contrary...
	if (%this.gameControlObject == LocalClientConnection.getControlObject())
		LocalClientConnection.setCOntrolObject(LocalClientConnection.camera);
	else
		LocalClientConnection.setCOntrolObject(%this.gameControlObject);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setCameraPlayerMode(%this) {
	devLog("Lab::setCameraPlayerMode");
	if (!isObject( LocalClientConnection.player)) {
		warnLog("You don't have a player assigned, set spawnPlayer true to spawn one automatically");
		return;
	}

	%player = LocalClientConnection.player;
	%player.setVelocity("0 0 0");
	if (LocalClientConnection.getControlObject() != LocalClientConnection.player)
		LocalClientConnection.setControlObject(%player);
	%this.syncCameraGui();
}
//------------------------------------------------------------------------------


//==============================================================================
function Lab::fitCameraToSelection( %this,%orbit ) {
	if (%orbit) {
		Lab.setCameraViewMode("Orbit Camera",false);
	}

	//GuiShapeEdPreview have it's own function
	if (isObject(Lab.fitCameraGui)) {
		Lab.fitCameraGui.fitToShape();
		return;
	}

	%radius = EWorldEditor.getSelectionRadius()+1;
	LocalClientConnection.camera.autoFitRadius(%radius);
	LocalClientConnection.setControlObject(LocalClientConnection.camera);
	%this.syncCameraGui();
}
//------------------------------------------------------------------------------
//==============================================================================

function Lab::EditorOrbitCameraChange(%this,%client, %size, %center) {
	if(%size > 0) {
		%client.camera.setValidEditOrbitPoint(true);
		%client.camera.setEditOrbitPoint(%center);
	} else {
		%client.camera.setValidEditOrbitPoint(false);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::EditorCameraFitRadius(%this,%client, %radius) {
	%client.camera.autoFitRadius(%radius);
	%client.setControlObject(%client.camera);
	Lab.syncCameraGui();
}
//------------------------------------------------------------------------------


function serverCmdDropPlayerAtCamera(%client) {
	// If the player is mounted to something (like a vehicle) drop that at the
	// camera instead. The player will remain mounted.
	%obj = %client.player.getObjectMount();

	if (!isObject(%obj))
		%obj = %client.player;

	%obj.setTransform(%client.camera.getTransform());
	%obj.setVelocity("0 0 0");
	%client.setControlObject(%client.player);
	Lab.syncCameraGui();
}

function serverCmdDropCameraAtPlayer(%client) {
	%client.camera.setTransform(%client.player.getEyeTransform());
	%client.camera.setVelocity("0 0 0");
	%client.setControlObject(%client.camera);
	Lab.syncCameraGui();
}



function Lab::CycleCameraFlyType(%this,%client) {
	if (%client $= "")
		%client = LocalClientConnection;
	if(%client.camera.getMode() $= "Fly") {
		if(%client.camera.newtonMode == false) { // Fly Camera
			// Switch to Newton Fly Mode without rotation damping
			%client.camera.newtonMode = "1";
			%client.camera.newtonRotation = "0";
			%client.camera.setVelocity("0 0 0");
		} else if(%client.camera.newtonRotation == false) { // Newton Camera without rotation damping
			// Switch to Newton Fly Mode with damped rotation
			%client.camera.newtonMode = "1";
			%client.camera.newtonRotation = "1";
			%client.camera.setAngularVelocity("0 0 0");
		} else { // Newton Camera with rotation damping
			// Switch to Fly Mode
			%client.camera.newtonMode = "0";
			%client.camera.newtonRotation = "0";
		}

		%client.setControlObject(%client.camera);
		Lab.syncCameraGui();
	}
}

