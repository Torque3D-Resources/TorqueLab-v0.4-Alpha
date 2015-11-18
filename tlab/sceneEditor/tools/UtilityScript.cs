//==============================================================================
// TorqueLab -> SceneEditor Inspector script
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================

//==============================================================================
// Prefabs
//==============================================================================



function SETools_CreateOptClassMargin::onValidate( %this ) {
	%text = %this.getText();
	if (!strIsNumeric(%text.x))
		%failed = true;
	if (!strIsNumeric(%text.y))
		%failed = true;
	if (!strIsNumeric(%text.z))
		%failed = true;
	
	if (%failed){
		SETools_CreateOptClassMargin.setText("0 0 0");
		return;
	}
	SETools_CreateOptClassMargin.setText(%text.x SPC %text.y SPC %text.z);
	
}