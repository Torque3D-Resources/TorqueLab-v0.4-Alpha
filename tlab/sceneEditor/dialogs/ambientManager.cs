//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$SEP_AmbientBook_PageId = 0;
$SEP_PrecipitationBook_PageId = 0;
$SEP_ScatterSkyBook_PageId = 0;
$SEP_CloudsBook_PageId = 0;
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SceneEditorDialogs::onActivated( %this ) {		
	if (SceneEditorDialogs.selectedPage $= "")
		SceneEditorDialogs.selectedPage = "0";

	SEP_SkySystemCreator-->newScatterSkyName.setText("envScatterSky");
	SEP_SkySystemCreator-->newSunName.setText("envSun");
	SEP_SkySystemCreator-->newSkyName.setText("envSkyBox");
}
//------------------------------------------------------------------------------

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::onShow( %this ) {
	devLog("SEP_AmbientManager::onShow(%this)");
	
	SEP_PostFXManager_Clone.add(EPostFxManager-->MainContainer);	
	
	hide(EPostFxManager);
	
	hide(SEP_SkySystemCreator);	
	
	SEP_AmbientManager.updateSkySystemData(true);
	syncParamObj(SEP_AmbientManager.FogParamArray);
	
	//EPostFxManager.init("1");
	EPostFxManager.schedule(1000,"init","1");
	
	//SEP_AmbientBook.selectPage(0);
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::onHide( %this ) {
	devLog("SEP_AmbientManager::onHide(%this)");
	
	if (isObject(SEP_PostFXManager_Clone-->MainContainer)){
		EPostFxManager.add(SEP_PostFXManager_Clone-->MainContainer);
		
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_AmbientManager::onPreEditorSave(%this) {	
	devLog("SEP_AmbientManager::onPreEditorSave",TMG_GroundCoverClone-->MainContainer);
	if (isObject(SEP_PostFXManager_Clone-->MainContainer))
		EPostFxManager.add(EPostFxManager_Main);
}
//------------------------------------------------------------------------------
//==============================================================================
function SEP_AmbientManager::onPostEditorSave(%this) {
	devLog("SEP_AmbientManager::onPostEditorSave",TMG_GroundCoverClone-->MainContainer);
	SEP_PostFXManager_Clone.add(EPostFxManager-->MainContainer);
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::initDialog( %this ) {
	logd("SEP_AmbientManager::initDialog(%this)");

	if (!isObject(SEP_AmbientManager_PM))
		new PersistenceManager(SEP_AmbientManager_PM);
		
	SEP_AmbientBook.selectPage($SEP_AmbientBook_PageId);
	
	%this.getSkySystemObject();
	
	

	SEP_AmbientManager.initSkySystemData();
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::onActivated( %this ) {
	logd("SEP_AmbientManager::onActivated(%this)");
	SEP_AmbientBook.selectPage($SEP_AmbientBook_PageId);
	SEP_ScatterSkyBook.selectPage($SEP_ScatterSkyBook_PageId);
	SEP_PrecipitationBook.selectPage($SEP_PrecipitationBook_PageId);
	SEP_CloudsBook.selectPage($SEP_CloudsBook_PageId);
	
	SEP_ScatterSkySystemMenu.clear();
	SEP_ScatterSkySystemMenu.add("New Scatter Sky",0);
	SEP_ScatterSkySystemMenu.add("New Sun (+SkyBox)",1);
	SEP_ScatterSkySystemMenu.setSelected(0);
	//%this.getSkySystemObject();
	SEP_AmbientManager.buildFogParams(true);
	SEP_AmbientManager.buildBasicCloudsParams();
	SEP_AmbientManager.initBasicCloudsData();
	SEP_AmbientManager.buildCloudLayerParams();
	SEP_AmbientManager.initCloudLayerData();
	SEP_ScatterSkyManager.initData();
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::onWake( %this ) {
	SEP_PrecipitationManager.initData();
	SEP_PrecipitationBook.selectPage(0);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::onSleep( %this ) {
	logd("SEP_AmbientManager::onSleep( %this )");
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientBook::onTabSelected( %this,%text,%index ) {
	logd("SEP_AmbientManager::onTabSelected( %this,%text,%index )");
	
	$SEP_AmbientBook_PageId = %index;
}
//------------------------------------------------------------------------------
//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_CloudsBook::onTabSelected( %this,%text,%index ) {	
	
	$SEP_CloudsBook_PageId = %index;
}
//------------------------------------------------------------------------------

//==============================================================================
// Prepare the default config array for the Scene Editor Plugin
function SEP_AmbientManager::setObjectDirty( %this,%obj,%isDirty ) {
	logd("SEP_AmbientManager::setObjectDirty( %this,%obj,%isDirty )");
	
	if (!isObject(%obj)){
		warnLog("Trying to set dirty for invalid object in AmbienManager:",%obj);
		return;
	}
	if (%isDirty $="")
		%isDirty = SEP_AmbientManager_PM.isDirty(%obj);		
	else if ( !SEP_AmbientManager_PM.isDirty(%obj) && %isDirty)
		SEP_AmbientManager_PM.setDirty( %obj );
	else if ( SEP_AmbientManager_PM.isDirty(%obj) && !%isDirty)
		SEP_AmbientManager_PM.removeDirty( %obj );
}
//------------------------------------------------------------------------------