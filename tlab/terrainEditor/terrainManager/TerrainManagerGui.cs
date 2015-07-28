//==============================================================================
// TorqueLab -> TerrainMaterialManager
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$TMG_Init = false;
$TMG_CurrentPage = 0;
//==============================================================================
function TerrainManagerGui::onWake(%this) {
	if (!TMG.initialized)
		TMG.init();
	
	TMG_MainBook.selectPage($TMG_CurrentPage);
	TMG.refreshData();
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainManagerGui::onSleep(%this) {
	
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------

//==============================================================================
function TerrainManagerGui::onPreEditorSave(%this) {	
	devLog("TerrainManagerGui::onPreEditorSave",TMG_GroundCoverClone-->MainContainer);
	if (isObject(TMG_GroundCoverClone-->MainContainer))
		SEP_GroundCover.add(TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------
//==============================================================================
function TerrainManagerGui::onPostEditorSave(%this) {
	devLog("TerrainManagerGui::onPostEditorSave",TMG_GroundCoverClone-->MainContainer);
}
//------------------------------------------------------------------------------
//==============================================================================
function TMG::init(%this) {
	
	TMG.initMaterialLayersPage();
	
	TerrainManagerGui-->dataFolder.setText("");	
	TMG.initialized = true;
}
//------------------------------------------------------------------------------

//==============================================================================
function TMG::refreshData(%this) {
	hide(SEP_GroundCover);
	TMG_GroundCoverClone.add(SEP_GroundCover-->MainContainer);
	TMG.updateTerrainList(true);
	%this.refreshMaterialLayersPage();
	%this.buildTerrainInfoParams();
}
//------------------------------------------------------------------------------







//==============================================================================
function TMG_MainBook::onTabSelected(%this,%text,%id) {
	$TMG_CurrentPage = %id;
	
}
//------------------------------------------------------------------------------
