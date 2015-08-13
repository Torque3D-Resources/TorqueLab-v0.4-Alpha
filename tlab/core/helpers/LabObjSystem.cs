//==============================================================================
// TorqueLab -> LabObj System
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// TorqueLab universal SimObjects update (GuiInspector) and save (PersistenceManager)
//==============================================================================


//==============================================================================
//Editor Initialization callbacks
//==============================================================================

//==============================================================================
function LabObj::update(%this,%obj,%field,%value,%fieldId) {
	%initialValue = %obj.getFieldValue(%field);
	LabInspect.inspect(%obj);
	%obj.setFieldValue(%field,%value,%fieldId);	
	LabInspect.apply();
	
	
}
//------------------------------------------------------------------------------

//==============================================================================
// LabObj.set is same as update but will also add the object to Lab_PM for future save
function LabObj::set(%this,%obj,%field,%value,%fieldId) {
	%initialValue = %obj.getFieldValue(%field);
	%this.update(%obj,%field,%value,%fieldId);	
	
	if (%initialValue !$= %value){
		Lab_PM.setDirty(%obj);
		info(Lab_PM.getDirtyObjectCount()," objects are dirty in Lab_PM");
	}	
}
//------------------------------------------------------------------------------

//==============================================================================
function LabObj::listDirty(%this) {
	Lab_PM.listDirty();
	
}
//------------------------------------------------------------------------------

//==============================================================================
function LabObj::saveAll(%this) {
	Lab_PM.saveDirty();
	
}
//------------------------------------------------------------------------------

//==============================================================================
function LabObj::save(%this,%obj) {
	if (!Lab_PM.isDirty(%obj)){
		if (%force){
			info(%obj,"Object is not dirty but we will force it");
			Lab_PM.setDirty(%obj);
		}
		else {
		info(%obj,"Object is not dirty use %force = true to force it");
		return;
		}
	}
		
	Lab_PM.saveDirtyObject(%obj);
	
}
//------------------------------------------------------------------------------