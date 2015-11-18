//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
// Handle the escape bind
function scSceneAutoSaveDelayEdit::onValidate( %this ) {
	%time = %this.getText();

	if (!strIsNumeric(%time))
		return;

	%newDelay = mFloatLength(%time,1);

	if (%newDelay $= $TLab_AutoSaveDelay)
		return;

	$TLab_AutoSaveDelay = %newDelay;
	devLog("Autosaving delay changed to",$TLab_AutoSaveDelay,"minutes");
	Lab.SetAutoSave();
}
//------------------------------------------------------------------------------

//==============================================================================
// Handle the escape bind
function scSceneAutoSaveCheck::onClick( %this ) {
	Lab.SetAutoSave();
}
//------------------------------------------------------------------------------
//==============================================================================
// Handle the escape bind
function Lab::SetAutoSave( %this ) {
	if ($TLab_AutoSaveEnabled) {
		if ($TLab_AutoSaveDelay $= "" || !strIsNumeric($TLab_AutoSaveDelay))
			$TLab_AutoSaveDelay = "1";

		cancel($LabAutoSaveSchedule);
		$LabAutoSaveSchedule = Lab.schedule($TLab_AutoSaveDelay * 60000,"AutoSaveScene");
		//$LabAutoSaveSchedule = Lab.schedule(Lab.autoSaveDelay * 60000,"AutoSaveScene");
		devLog("Autosaving enabled! Saving each",$TLab_AutoSaveDelay,"minutes!");
	} else {
		//cancel($LabAutoSaveSchedule);
		//delObj($LabAutoSaveSchedule);
		devLog("Autosaving disabled");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Handle the escape bind
function Lab::AutoSaveScene( %this ) {
	if (!EditorGui.isAwake())
		return;

	Lab.SaveMission();
	devLog("Scene has been autosaved",$TLab_AutoSaveDelay,"minutes!");
	Lab.SetAutoSave();
}
//------------------------------------------------------------------------------