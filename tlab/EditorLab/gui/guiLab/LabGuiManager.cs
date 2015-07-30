//==============================================================================
// GameLab -> Interface Development Gui
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
function LabGuiManager::onWake( %this ) {
  
   if( !isObject( "GameProfilesPM" ) )
      new PersistenceManager( GameProfilesPM ); 
   
   GLab_TabBook.selectPage($GLab::TabId);   
   GLab_SettingsBook.selectPage($GLab::TabId); 
   GLab.initProfileParams();
   exec("tlab/EditorLab/gui/guiLab/LabGuiManager.cs");
   $true = true;
   $Color = "12 88 133 254";
   
   $TextSample = "TextBaseMed sample text éç!? CPG cpg";   
   initGuiSystem();
   hide(GLab_NewProfileDlg);
   hide(wParams_ProfileSet);
   
  GLab.initColorManager();
  GLab.initProfileManager();
 
  GLab.updateFontTypeList();
  GLab_ActiveFieldRollout.expanded = false;
  GLab_ActiveFieldRollout.caption = $GLab_Caption_NoFieldSelected;
  GLab.setSelectedProfile($GLab_SelectedObject);
  
  if (GLab_MainWindow.extent.y >= LabGuiManager.extent.y){
     %pos = GLab_MainWindow.position;
     %extent = GLab_MainWindow.extent.x SPC LabGuiManager.extent.y-30;
     GLab_MainWindow.resize(%pos.x,%pos.y,%extent.x,%extent.y);
    // GLab_MainWindow.extent.y = LabGuiManager.extent.y;
  }
}
//------------------------------------------------------------------------------
//==============================================================================
function LabGuiManager::onSleep( %this ) {
  
   GLab.savePrefs();
}
//------------------------------------------------------------------------------
//==============================================================================
function LabGuiManager::onTabSelected( %this,%text,%id ) {
   $GLab::TabId = %id;  
}
//------------------------------------------------------------------------------
//==============================================================================
function GLab_SettingsBook::onTabSelected( %this,%text,%id ) {
   $GLab::TabId = %id;  
}
//------------------------------------------------------------------------------
//==============================================================================
function GLab::exportPrefs( %this ) {
   export("$GLab_pref_*", "tlab/EditorLab/gui/guiLab/prefs.cs", false);
}
//------------------------------------------------------------------------------


//==============================================================================
function GLab::initProfileManager( %this ) {
   GLab_ProfilesTree.init();
  //GLab_ProfilesTree.open( GameProfileGroup );
	//GLab_ProfilesTree.buildVisibleTree(true);
}
//------------------------------------------------------------------------------

//==============================================================================
function GLab::savePrefs( %this ) {
	%file = filePath(LabGuiManager.getfilename())@"/prefs.cs";
   export("$GLab::*", %file, false);
}
//------------------------------------------------------------------------------