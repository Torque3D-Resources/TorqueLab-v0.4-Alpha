//==============================================================================
// TorqueLab -> VehicleEditorPlugin Initialization
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
function VEP::initDataTree(%this) {
	$VEP_DataGroup = newSimSet("VEP_DataGroup");
	foreach(%data in DataBlockGroup){
		if (!%data.isMemberOfClass("VehicleData"))
			continue;
		
		VEP_DataGroup.add(%data);
	}
	
	%this.buildDataTree();	
}
//------------------------------------------------------------------------------
//==============================================================================
function VEP::buildDataTree(%this) {
	if (!isObject(VEP_DataGroup)){
		%this.initDataTree();
		return;
	}
	
	VEP_DataTree.open(VEP_DataGroup);
	VEP_DataTree.buildVisibleTree();		
}
//------------------------------------------------------------------------------

//==============================================================================
function VEP_DataTree::onSelect(%this,%datablock) {
	VehicleEdInspector.inspect(%datablock);
}
//------------------------------------------------------------------------------
