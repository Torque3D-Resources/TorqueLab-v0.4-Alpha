//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================


//==============================================================================
// Activate the interface for a plugin
//==============================================================================
$LabDevGui_StackList = "ToolsTextBase ToolsTextFX ToolsTextAlt";
$LabDevGui_SampleText = "Sample text - éç@#$%?&*";


EditorMap.bindCmd( keyboard, "ctrl 0", "LabDevGui.toggleMe();","" );
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::onWake(%this) {
	if (!$LDG_ProfileSetupLoaded)
		Lab.initProfileSetupSystem();
		
	LDG_ProfilesSetupTree.init();
	LDG_WidgetsContainer.add(LWG_WidgetsTabBook);
	LabDevGui.generateSamples();
	EditorMap.push();
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::onSleep(%this) {	
	LWG_WidgetsContainer.add(LWG_WidgetsTabBook);	
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::onPreEditorSave(%this) {	
	LWG_WidgetsContainer.add(LWG_WidgetsTabBook);	
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabDevGui::onPostEditorSave(%this) {	
	GuiEditCanvas.save(LabWidgetsGui,true);
	LDG_WidgetsContainer.add(LWG_WidgetsTabBook);
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabWidgetsGui::onWake(%this) {	
	LWG_WidgetsContainer.add(LWG_WidgetsTabBook);	
}
//------------------------------------------------------------------------------
//==============================================================================
//Set a plugin as active (Selected Editor Plugin)
function LabWidgetsGui::onPreEditorSave(%this) {	
	LWG_WidgetsContainer.add(LWG_WidgetsTabBook);
	devLog("About to save LabWidgetsGui to:",LabWidgetsGui.getFilename());	
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
		if (strFind(%obj.getName(),"ToolsTextAlt")){
			%this.generateTextSample(%obj.getName(),"ToolsTextAlt");
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