//==============================================================================
// TorqueLab -> MeshRoadEditor - Road Manager - NodeData
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// TerrainObject Functions
//==============================================================================
//MRoadManager.updateRoadData();
function MRoadManager::updateRoadData(%this){
	%road = MRoadManager.currentRoad;
	if (!isObject(%road))
		return;
	%id = 0;
	while( true ) {
		MeshRoadEditorGui.setSelectedNode(%id);
		%nodeWidth = MeshRoadEditorGui.getNodeWidth();
		
		if (%nodeWidth $= "" || %nodeWidth $= "-1")
			break;
			
		%nodePos = MeshRoadEditorGui.getNodePosition();		
		%nodeDepth = MeshRoadEditorGui.getNodeDepth();		
		%nodeNormal = MeshRoadEditorGui.getNodeNormal();
		
		devLog("Node:",%id,"Pos",%nodePos,"Normal",%nodeNormal,"Width",%nodeWidth,"Depth",%nodeDepth);
		
		MRoadManager.nodeData[%id] = %nodePos TAB %nodeNormal TAB %nodeWidth TAB %nodeDepth;
		%id++;
	}
	MRoadManager.nodeCount = %id;
	%this.updateNodeStack();
	
	MeshRoadEditorOptionsWindow-->topMaterial.setText(%road.topMaterial);
	MeshRoadEditorOptionsWindow-->bottomMaterial.setText(%road.bottomMaterial);
	MeshRoadEditorOptionsWindow-->sideMaterial.setText(%road.sideMaterial);

}

//==============================================================================
function MRoadManager::updateNodeStack(%this){
	MREP_NodePillSample.visible = 0;
	MREP_NodePillSample_v3.visible = 0;
	%stack = MREP_NodePillStack;
	%stack.visible = 1;
	%stack.clear();
	for(%i=0;%i<MRoadManager.nodeCount;%i++){
		%this.addNodePillToStack(%i);
	}
}

function MRoadManager::addNodePillToStack(%this,%nodeId){
	%fieldData = MRoadManager.nodeData[%nodeId];
	%pos = getField(%fieldData,0);
	%normal = getField(%fieldData,1);
	%width = getField(%fieldData,2);
	%depth = getField(%fieldData,3);
	
	
	%pill = cloneObject(MREP_NodePillSample_v3);
	%pill.superClass = "MREP_NodePill";
	%pill.internalName = "NodePill_"@%nodeId;
	%pill.nodeId = %nodeId;
	%pill-->Linked.setStateOn(false);
	%pill.linkCheck = %pill-->Linked;
	//%pill-->nodeIdText.text = "Node #\c2"@%nodeId;
	%pill.caption = "Node #\c4"@%nodeId;
	%pill-->toggleNorm.command = "toggleVisible("@%pill-->normCtrl.getId()@");";
	%pill-->togglePos.command = "toggleVisible("@%pill-->posCtrl.getId()@");";
	%pill-->deleteButton.command = "MRoadManager.deleteNodeId("@%nodeId@");";
	%pill-->PosX.text = %pos.x;
	%pill-->PosY.text = %pos.y;
	%pill-->PosZ.text = %pos.z;
	%pill-->Normal.text = %normal;
	%pill-->Width.setText(%width);
	%pill-->Width.updateFriends();
	%pill-->Depth.setText(%depth);
	%pill-->Depth.updateFriends();
	
	%pill-->Width.pill = %pill;
	
	
	MREP_NodePillStack.add(%pill);
}
//==============================================================================
// Update Node
//==============================================================================
//==============================================================================
function MRoadManager::updateNodeSetting(%this,%node,%field,%value){
	devLog("Node:",%node,"Field",%field,"Value",%value);
	MeshRoadEditorGui.setSelectedNode(%node);
	switch$(%field){
		case "width":
			MeshRoadEditorGui.setNodeWidth(%value);
		case "depth":
			MeshRoadEditorGui.setNodeDepth(%value);
		case "normal":
			MeshRoadEditorGui.setNodeNormal(%value);
		case "PosX":
			%position = MeshRoadEditorGui.getNodePosition();
			%position.x = %value;
			MeshRoadEditorGui.setNodePosition(%position);
			devLog("PosX",%value,"Full",%position);
		case "PosY":
			%position = MeshRoadEditorGui.getNodePosition();
			%position.y = %value;
			MeshRoadEditorGui.setNodePosition(%position);
			devLog("PosY",%value,"Full",%position);
		case "PosZ":
			%position = MeshRoadEditorGui.getNodePosition();
			%position.z = %value;
			MeshRoadEditorGui.setNodePosition(%position);
			devLog("PosZ",%value,"Full",%position);
	}
	
}
//------------------------------------------------------------------------------
//==============================================================================
function MRoadManager::updateNodeCtrlSetting(%this,%ctrl){
	%pill = MRoadManager.getParentPill(%ctrl);
	%node = %pill.nodeId;
	%fields = strreplace(%ctrl.internalName,"_"," ");
	%field = getWord(%fields,0);
	%value = %ctrl.getTypeValue();
	%ctrl.updateFriends();
	%this.updateNodeSetting(%node,%field,%value);
	
}
//------------------------------------------------------------------------------
//==============================================================================
function MRoadManager::getParentPill(%this,%ctrl){
	%attempt = 0;
	while(%attempt < 10){
		%parent = %ctrl.parentGroup;
		if (%parent.superClass $= "MREP_NodePill")
			return %parent;
		
		%ctrl = %parent;
		%attempt++;
	}
	return "";
}
//------------------------------------------------------------------------------

//==============================================================================
function MREP_NodeSlider::onMouseDragged(%this){
	%value = mFloatLength(%this.getValue(),3);
	%this.setValue(%value);		
	MRoadManager.updateNodeCtrlSetting(%this);	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function MREP_NodeEdit::onValidate(%this){
	MRoadManager.updateNodeCtrlSetting(%this);	
}
//------------------------------------------------------------------------------
//==============================================================================
function MREP_MaterialSelect::setMaterial(%this,%materialName,%a1,%a2){
	devLog("MREP_MaterialSelect::setMaterial(%this,%materialName,%a1,%a2)",%this,%materialName,%a1,%a2);
	%type = %this.internalName;
	
	%road = MRoadManager.currentRoad;
	if (!isObject(%road))
		return;
	MeshRoadInspector.inspect(%road);
	%road.setFieldValue(%type@"Material",%materialName);
	MeshRoadInspector.refresh();
	
	%textEdit = MeshRoadEditorOptionsWindow.findObjectByInternalName(%type@"Material",true);
	%textEdit.setText(%materialName);
	
	devLog("Select Material for:",%type,"Is:",%materialName);
}
//------------------------------------------------------------------------------
//==============================================================================
function MREP_MaterialEdit::onValidate(%this){
	%type = %this.internalName;
	devLog("Select Material for:",%type,"Is:",%this.getText());
	
}
//------------------------------------------------------------------------------
