//==============================================================================
// TorqueLab -> SceneEditor Inspector script
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

//==============================================================================
// Hack to force LevelInfo update after Cubemap change...
//==============================================================================
//==============================================================================
function SceneEd::toggleSplatMapMode( %this ) {
	if (!isFile($ActiveSplatMap)){
		warnLog("No active splatmap found:",$ActiveSplatMap);
		Game.deactivateSplatMap();
		SceneEd_SplatMapCheck.active = false;
		SceneEd_SplatMapInfo.text = "No terrain splatmap found";
		return;		
	}
	SceneEd_SplatMapInfo.text = "Terrain SplatMap: \c1"@$ActiveSplatMap;
	Game.toggleSplatMapMode();
	
	
	
}