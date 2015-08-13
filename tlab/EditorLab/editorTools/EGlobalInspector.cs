//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$VisibilityOptionsLoaded = false;
$VisibilityClassLoaded = false;
$EVisibilityLayers_Initialized = false;

//==============================================================================
function LabInspect::update(%this,%obj,%field,%value,%fieldId) {
	%this.set(%obj,%field,%value,%fieldId);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function LabInspect::set(%this,%obj,%field,%value,%fieldId) {
	LabInspect.inspect(%obj);
	%obj.setFieldValue(%field,%value,%fieldId);	
	LabInspect.apply();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::inspect(%this,%obj,%doApply) {
	LabInspect.inspect(%obj);
	if (%doApply)
		LabInspect.apply();
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::addInspect(%this,%obj) {
	LabInspect.addInspect(%obj);
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::apply(%this,%obj) {
	LabInspect.apply();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::getInspectObject(%this) {
	return LabInspect.getInspectObject();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::getNumInspectObjects(%this) {
	return LabInspect.getNumInspectObjects();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::refreshInspect(%this) {
	LabInspect.refresh();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::removeInspect(%this,%obj) {
	LabInspect.removeInspect(%obj);
}
//------------------------------------------------------------------------------
/*

virtual void GuiInspector::addInspect	(	(id object,(bool autoSync=true)) 		 ) 	[virtual]
Add the object to the list of objects being inspected.

virtual void GuiInspector::apply	(		 ) 	[virtual]
apply() - Force application of inspected object's attributes

virtual int GuiInspector::findByObject	(		 ) 	[virtual]
findByObject( SimObject ) - returns the id of an awake inspector that is inspecting the passed object if one exists.

virtual string GuiInspector::getInspectObject	(		 ) 	[virtual]
getInspectObject( int index=0 ) - Returns currently inspected object

virtual int GuiInspector::getNumInspectObjects	(	() 		 ) 	[virtual]
Return the number of objects currently being inspected.

virtual void GuiInspector::inspect	(		 ) 	[virtual]
Inspect(Object)

virtual void GuiInspector::refresh	(		 ) 	[virtual]
Reinspect the currently selected object.

virtual void GuiInspector::removeInspect	(	(id object) 		 ) 	[virtual]
Remove the object from the list of objects being inspected.

virtual Script GuiInspector::setAllGroupStateScript	(	(string this, string obj, string groupState) 		 ) 	[virtual]
virtual void GuiInspector::setName	(		 ) 	[virtual]
setName(NewObjectName)

virtual void GuiInspector::setObjectField	(		 ) 	[virtual]
setObjectField( fieldname, data ) - Set a named fields value on the inspected object if it exists. This triggers all the usual callbacks that would occur if the field had been changed through the gui.

virtual Script GuiInspector::toggleDynamicGroupScript	(	(string this, string obj) 		 ) 	[virtual]
virtual Script GuiInspector::toggleGroupScript	(	(string this, string obj, string fieldName) 		 ) 	[virtual]

*/