//==============================================================================
// GameLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

$LabParamsStyle = "StyleA";

//==============================================================================
//Initialize plugin data
function Lab::newParamsArray(%this,%nameFlds,%groupFlds,%cfgObject,%useLongName) {
	if (%nameFlds $= "") {
		warnLog("You need to specify a name for the settings which is unique in this type");
		return;
	}
	
	%name = getField(%nameFlds,0);
	%nameCode = getField(%nameFlds,1);
	if (%nameCode $= "")
		%nameCode = %name;
	
	%group = getField(%groupFlds,0);
	%groupCode = getField(%groupFlds,1);
	if (%groupCode $= "")
		%groupCode = %group;
	
	devLog("Name:",%name,%nameCode,"Group:",%group,%groupCode);
	if (%container $= "")
		%container = %name@"ParamStack";

	%name = strreplace(%name," ","_");
	%arrayName = %name;
	if (%useLongName)
		%arrayName = %groupCode@%nameCode;
		
	
	%fullName = %arrayName@"_Param";
	%array = newArrayObject(%fullName,LabParamsGroup,"ParamArray");
	%array.internalName = %name;
	%array.displayName = %name;
	%array.container = %container;
	%array.paramCallback = "Lab.onParamBuild";
	
	%array.group = %group;
	%array.useNewSystem = true;
	//If no cfgObject supplied, simply use the new array as object
	if (!isObject(%cfgObject))
		%cfgObject = %array;

	%array.cfgObject = %cfgObject;
	%array.groupLink = %group@"_"@%name;


	if (%prefGroup $= "")
		%prefGroup = %name;

	%array.prefGroup = %prefGroup;
	%array.updateFunc = "LabParams.updateParamArrayCtrl";
	//%array.common["command"] = "syncParamArrayCtrl($ThisControl,\"LabParams.updateParamArrayCtrl\",\""@%fullName@"\",\"\",\"\");";
	//%array.common["altCommand"] = "syncParamArrayCtrl($ThisControl,\"LabParams.updateParamArrayCtrl\",\""@%fullName@"\",\"true\",\"\");";
	return %array;
}
//------------------------------------------------------------------------------
//==============================================================================
//Initialize plugin data
function Lab::onParamBuild(%this,%array,%field,%paramData) {
	paramLog("Lab::onParamBuild(%this,%array,%field,%paramData)",%this,%array,%field,%paramData);
}
//------------------------------------------------------------------------------
//==============================================================================
//Initialize plugin data
function Lab::onParamPluginBuild(%this,%array,%field,%paramData) {
	paramLog("Lab::onParamPluginBuild(%this,%array,%field,%paramData)",%this,%array,%field,%paramData);
	%plugin = %array.pluginObj;
	%cfgValue = %plugin.getCfg(%field);

	if (%cfgValue $= "")
		%plugin.setCfg(%field,%paramData.Default);
}
//------------------------------------------------------------------------------