//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// Allow to manage different GUI Styles without conflict
//==============================================================================
//==============================================================================
// Load the Specific UI Style settings
function Lab::exportCurrentProfilesStyle(%this,%style) {
	
	if (%style $= "")
		%style = "Default";
	
	%styleGroup = newSimGroup(%style@"_ProfStyleGroup");	
	%file = "tlab/gui/styles/"@%style@"/profile.styles.cs";
	foreach$(%profile in $LabProfileList) {
		%newStyle = newScriptObject(%profile.getName()@"_"@%style@"Style");
		
	
		foreach$(%field in $ProfileFieldList) {
			if (%field $= "name") 
				continue;

			%value = %profile.getFieldValue(%field);
			if (%value $= "") 
				continue;
				
			eval("%newStyle."@%field@" = %value;");
            //%newStyle.setFieldValue(%field, %value);
		}

		%newStyle.internalName = %profile.getName();
		%styleGroup.add(%newStyle);
	
	}
	%result = %styleGroup.save(%file,false,"$LabStyleCurrentGroup = ");
	if (%result) 
		info("Save to file success:",%file);
	else 
		info("Save to file failed:",%file); 

}
//------------------------------------------------------------------------------