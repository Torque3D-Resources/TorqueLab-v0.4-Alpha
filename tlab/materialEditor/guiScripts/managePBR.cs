//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// MaterialEditorGui.activatePBR();
function MaterialEditorGui::togglePBR(%this ) {
	%this.activatePBR(!MatEd.PBRenabled);
}
//------------------------------------------------------------------------------
//==============================================================================
// MaterialEditorGui.activatePBR();
function MaterialEditorGui::activatePBR(%this,%activate ) {
	if (%activate $= "")
		%activate = true;

	MatEd.PBRenabled = %activate;
	pbr_lightInfuenceProperties.visible = %activate;
	pbr_materialDamageProperties.visible = %activate;
	pbr_lightingProperties.visible = %activate;
	pbr_accumulationProperties.visible = %activate;
	MEP_SpecularContainer.visible = !%activate;
}
//------------------------------------------------------------------------------
//==============================================================================
// Mode : 0 = all maps >> 1 = Comp only >> 2 = No COmp
function MatEd::pbrMapMode(%this,%mode ) {
	switch$(%mode) {
	case "0":
		pbr_lightInfuenceProperties-->compMap.visible = 1;
		pbr_lightInfuenceProperties-->smoothMap.visible = 1;
		pbr_lightInfuenceProperties-->aoMap.visible = 1;
		pbr_lightInfuenceProperties-->metalMap.visible = 1;

	case "1":
		pbr_lightInfuenceProperties-->compMap.visible = 1;
		pbr_lightInfuenceProperties-->smoothMap.visible = 0;
		pbr_lightInfuenceProperties-->aoMap.visible = 0;
		pbr_lightInfuenceProperties-->metalMap.visible = 0;

	case "2":
		pbr_lightInfuenceProperties-->compMap.visible = 0;
		pbr_lightInfuenceProperties-->smoothMap.visible = 1;
		pbr_lightInfuenceProperties-->aoMap.visible = 1;
		pbr_lightInfuenceProperties-->metalMap.visible = 1;
	}
}
//------------------------------------------------------------------------------