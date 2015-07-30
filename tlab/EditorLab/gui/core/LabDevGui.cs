//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
// Activate the interface for a plugin
//==============================================================================
$LabDevGui_StackList = "ToolsTextBase ToolsTextFX";
$LabDevGui_SampleText = "Sample text - éç@#$%?&*";


EditorMap.bindCmd( keyboard, "ctrl 0", "LabDevGui.toggleMe();","" );
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::onWake(%this) {
	
	LabDevGui.generateSamples();
	EditorMap.push();
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::toggleMe(%this) {
	devLog("ToggleMe");
	if (Canvas.getContent().getName() !$= "LabDevGui"){
		devLog("LabDevGui called:",Canvas.getContent().getName() );
		LabDevGui.previousGui = Canvas.getContent();
		setGui(LabDevGui);		
	}
	else {
		setGui(%this.previousGui);
	}
	
}
//------------------------------------------------------------------------------

//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::generateSamples(%this) {
	
	foreach$(%stackName in $LabDevGui_StackList){
		%stack = %this.findObjectByInternalName(%stackName@"Stack",true);
		if (!isObject(%stack))
			continue;
		devLog("Clearing stack for:",%stackName);
		%stack.clear();
	}
		
		
	foreach( %obj in GuiDataGroup ) {       
      if( !%obj.isMemberOfClass( "GuiControlProfile" ) )
         continue;
		
		if (strFind(%obj.getName(),"ToolsTextBase")){
			%this.generateTextSample(%obj.getName(),"ToolsTextBase");
		}
      if (strFind(%obj.getName(),"ToolsTextFX")){
			%this.generateTextSample(%obj.getName(),"ToolsTextFX");
		}   
	}
}
//------------------------------------------------------------------------------




//==============================================================================
function LabDevGui::generateTextSample(%this,%profileName,%type) {
	
	%stack = %this.findObjectByInternalName(%type@"Stack",true);
	if (!isObject(%stack)){
		warnLog("Invalid stack to add sample to. Profile:",%profileName,"Type",%type);
		return;
	}
	%pill = cloneObject(LabDev_TextSample);
	%pill.text = %profileName @ " - " @ $LabDevGui_SampleText;
	%pill.profile = %profileName;
	%stack.add(%pill);
	
	%pill = cloneObject(LabDev_TextSample);
	%pill.text = %profileName @ " - Colors \c0000 \c1111 \c2222";
	%pill.profile = %profileName;
	%stack.add(%pill);
}
//------------------------------------------------------------------------------