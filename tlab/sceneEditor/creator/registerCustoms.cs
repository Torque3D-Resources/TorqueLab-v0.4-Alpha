//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function SceneCreatorWindow::registerCustomsObjectsGroup( %this,%group ) {
	
	switch$(%group){
		case "Environment":				
			return;
		
		case "Level":			
			return;
	}
	
}

function SceneCreatorWindow::registerCustomsGroups( %this ) {	
	
}

function ObjectBuilderGui::buildBoostZone(%this)
{
   %this.objectClassName = "BoostZone";
   %this.addField( "BoostZoneBase_A", "TypeFile", "Shape File", "", "*.*");
   %this.process();
}
