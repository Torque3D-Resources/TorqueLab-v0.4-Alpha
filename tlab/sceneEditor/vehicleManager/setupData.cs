//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function sepVM::updateParam( %this,%field,%value,%ctrl,%array,%arg1,%arg2 ) {
	logd("sepVM::updateParam( %this,%field,%value,%ctrl,%array,%arg1,%arg2 )",%field,%value,%ctrl,%array,%arg1,%arg2);
	
	%data = %array.getVal(%field);	
	%updObj = getField(%data,%array.syncObjsField);	
	%isDirty = LabObj.set(%updObj,%field,%value);
	if (%isDirty){
		%menu = $sepVM_DataClassMenu[%updObj.getName()];
		%menu.setText(%updObj.internalName @ "*");		
		sepVM_WheeledApplyButton.active = 1;		
	}	
}
//------------------------------------------------------------------------------


//==============================================================================
//%field,%value,%ctrl,%array,%arg1,%arg2
function sepVM::cloneDatablock( %this,%class,%type,%obj ) {
	logd("sepVM::cloneDatablock( %this,%class,%type,%obj )",%class,%type,%obj);
	
	
	%name = %class@"_"@%type@"_vm_clone";
	delObj(%name);	
	%newObj = %obj.deepClone();
	%newObj.setName(%name);	
	%newObj.internalName = %obj.getName();	
	//eval("sepVM."@%class@%type@" = %newObj;");	
	switch$(%type){
		case "Vehicle":
			sepVM_WheeledCloneCtrl-->cloneTextVehicle.text = "Target:\c2" SPC %obj.getName();
			syncParamArray(arsepVM_WheeledVehicleParam);		
		case "tire1":
			Wheeled_Vehicle_vm_clone.tireData1 = %obj.getName();
			sepVM.setDirty(Wheeled_Vehicle_vm_clone);
			syncParamArray(arsepVM_WheeledWheels1Param);
		case "spring1":
			Wheeled_Vehicle_vm_clone.springData1 = %obj.getName();
			sepVM.setDirty(Wheeled_Vehicle_vm_clone);
			syncParamArray(arsepVM_WheeledWheels1Param);
		case "tire2":
		Wheeled_Vehicle_vm_clone.tireData2 = %obj.getName();
			sepVM.setDirty(Wheeled_Vehicle_vm_clone);
			syncParamArray(arsepVM_WheeledWheels2Param);	
		case "spring2":
		Wheeled_Vehicle_vm_clone.springData2 = %obj.getName();
			sepVM.setDirty(Wheeled_Vehicle_vm_clone);
			syncParamArray(arsepVM_WheeledWheels2Param);	
	}	
	
	$sepVM_DataClassMenu[%name].setText(%obj.getName());
	%newObj.setFileName("art/datablocks/vehicles/devonly/tmpData.cs");
	sepVM_TempDatablocks.add(%newObj);	
}
//------------------------------------------------------------------------------
//==============================================================================
//sepVM.applyChanges
function sepVM::applyChanges( %this ) {
	%dirtyCount = 0;
	foreach(%obj in sepVM_TempDatablocks){
		%isDirty = Lab_PM.isDirty(%obj);
		if (!%isDirty)
			continue;
		
		%dirtyCount++;
		%srcData = %obj.internalName;	
		%srcData.assignFieldsFrom(%obj);
		
		Lab_PM.removeField(%srcData,"test");
		Lab_PM.removeField(%srcData,"internalName");
		LabObj.save(%srcData,true);
		Lab_PM.removeDirty(%obj);
		
		%menu = $sepVM_DataClassMenu[%obj.getName()];
		%menu.setText(%obj.internalName);
	}
	sepVM_WheeledApplyButton.active = 0;
	info("Changes applied for wheeled datablocks!",%dirtyCount,"Object saved");
}
//------------------------------------------------------------------------------

//==============================================================================
//sepVM.applyChanges
function sepVM::cloneData( %this,%type ) {
	%target = %type@"_vm_clone";
	%menu = "seVM_CloneMenu_"@%type;
	%source = %menu.getText();
	if (!isObject(%source)){
		warnLog("Trying to clone invalid source:",%source);
		return;
	}
	if (!isObject(%target)){
		warnLog("Trying to clone invalid target:",%target);
		return;
	}
	%target.assignFieldsFrom(%source);
	Lab_PM.setDirty(%target);
	sepVM_WheeledApplyButton.active = 1;
	info(%source.getName(),"have been cloned into:",%target.internalName);
}
//------------------------------------------------------------------------------

//==============================================================================
//sepVM.applyChanges
function sepVM::newDataFromSource( %this,%type ) {
	%textEdit = "seVM_NewName_"@%type;
	%name = %textEdit.getText();
	%unique = getUniqueName(%name);
	
	%menu = "seVM_CloneMenu_"@%type;
	%source = %menu.getText();
	if (!isObject(%source)){
		warnLog("Trying to clone invalid source:",%source);
		return;
	}
	%newObj = %source.deepClone();
	%newObj.setName(%unique);	
	%newObj.setFilename(%source.getFilename());
	DataBlockGroup.add(%newObj);
	
	
	Lab_PM.setDirty(%newObj);
	sepVM_WheeledApplyButton.active = 1;
	
	%field = getWord(strreplace(%type,"_"," "),1);
	
	%this.selectWheeledData(%newObj,%field);
	
}
//------------------------------------------------------------------------------
//==============================================================================
//sepVM.applyChanges
function sepVM::setDirty( %this,%obj,%notDirty ) {
	if (%notDirty){
		Lab_PM.removeDirty(%obj);
		return;
	}
	Lab_PM.setDirty(%obj);
	sepVM_WheeledApplyButton.active = 1;
}
//------------------------------------------------------------------------------