//==============================================================================
// TorqueLab -> Editor Gui Open and Closing States
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//EVisibilityLayers.updatePresetMenu
function EVisibilityLayers::updatePresetMenu( %this ) {
		%searchFolder = "tlab/EditorLab/editorTools/visibilityLayers/presets/*.layers.ucs";
	//Now go through each files again to add a brush with latest items
	%menus = EVisibilityLayers_PresetMenu SPC SEP_CreatorTools-->VisibilityPreset SPC SBP_EVisibilityLayers_PresetMenu;
	%selected = 0;
	foreach$(%menu in %menus){
		%pid = 0;
		%menu.clear();
		%menu.add("Select a preset",0);
		for(%presetFile = findFirstFile(%searchFolder); %presetFile !$= ""; %presetFile = findNextFile(%searchFolder)) {
			%presetName = strreplace(fileBase(%presetFile),".layers","");
		
			%menu.add(%presetName,%pid++);
			if (EVisibilityLayers.currentPresetFile $= %presetName)
				%selected = %pid;
		}
		%menu.setSelected(%selected);
	}	
}
function EVisibilityLayers::toggleNewPresetCtrl( %this ) {
	if (EVisibilityLayers_NewPreset.visible){
		show(%this-->newPresetButton);
		EVisibilityLayers_NewPreset.visible = 0;
		return;
	}
	hide(%this-->newPresetButton);
		EVisibilityLayers_NewPreset.visible = 1;
}

//EVisibilityLayers.exportPresetSample
function EVisibilityLayers::savePresetToFile( %this ) {
	%name = EVisibilityLayers_NewPreset-->presetName.getText();
	
	if (strFind(%name,"[")){		
		return;
	}
		
	%layerExampleObj = newScriptObject();
	%layerExampleObj.internalName = %name;
	for ( %i = 0; %i < %this.classArray.count(); %i++ ) {
		%class = %this.classArray.getKey( %i );
		eval("%selectable = $" @ %class @ "::isSelectable;");
		eval("%renderable = $" @ %class @ "::isRenderable;");
		%layerExampleObj.selectable[%class] = %selectable;
		%layerExampleObj.visible[%class] = %renderable;
	}
	%layerExampleObj.save("tlab/EditorLab/editorTools/visibilityLayers/presets/"@%name@".layers.ucs",0,"%presetLayers = ");
	%this.toggleNewPresetCtrl();
	EVisibilityLayers.loadPresetFile(%name);
	%this.updatePresetMenu();
}
//EVisibilityLayers.exportPresetSample
function EVisibilityLayers::exportPresetSample( %this ) {
	%layerExampleObj = newScriptObject("EVisibilityLayerPresetExample");
	for ( %i = 0; %i < %this.classArray.count(); %i++ ) {
		%class = %this.classArray.getKey( %i );
		
		%layerExampleObj.selectable[%class] = 1;
		%layerExampleObj.visible[%class] = 1;
	}
	%layerExampleObj.save("tlab/EditorLab/editorTools/visibilityLayers/presetExample.layers.ucs",0,"%presetLayers = ");
}



//EVisibilityLayers.loadPresetFile("visBuilder");
function EVisibilityLayers::loadPresetFile( %this,%filename,%noStore ) {
	%file = "tlab/EditorLab/editorTools/visibilityLayers/presets/"@%filename@".layers.ucs";
	
	if (!isFile(%file))
		return;
		
	exec(%file);
	devLog("Loading Vis file:",%file,"NoStore",%noStore);
	for ( %i = 0; %i < %this.classArray.count(); %i++ ) {
		%class = %this.classArray.getKey( %i );
		
		%selectable = %presetLayers.selectable[%class];
		%renderable = %presetLayers.visible[%class];
		
		if (%selectable !$= ""){
			eval("$" @ %class @ "::isSelectable = \""@%selectable@"\";");
			info("Class:",%class,"isSelectable set to:",%selectable);
		}
		if (%renderable !$= ""){
			eval("$" @ %class @ "::isRenderable = \""@%renderable@"\";");
			info("Class:",%class,"isRenderable set to:",%renderable);
		}	
	}
	%presetName = %filename;
	if (!%noStore){
		EVisibilityLayers.currentPresetFile = %filename;		
	}else {
		%presetName = "*"@%presetName;
	}
	
	EVisibilityLayers.activePreset = %presetName;
}

function EVisibilityLayers_Preset::onSelect( %this,%id,%text ) {	
	devLog("EVisibilityLayers_Preset onSelect",%id,%text);
	if (	%id $= "0")
		return;
	EVisibilityLayers.loadPresetFile(%text);
	
	
}
