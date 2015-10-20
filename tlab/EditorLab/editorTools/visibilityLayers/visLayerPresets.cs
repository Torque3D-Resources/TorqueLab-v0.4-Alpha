//==============================================================================
// TorqueLab -> Editor Gui Open and Closing States
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

function EVisibilityLayers::updatePresetMenu( %this ) {
		%searchFolder = "tlab/EditorLab/editorTools/visibilityLayers/presets/*.layers.ucs";
	//Now go through each files again to add a brush with latest items
	%creatorMenu = SEP_CreatorTools-->VisibilityPreset;
	%creatorMenu.clear();
	%creatorMenu.add("Select a preset",0);
	EVisibilityLayers_PresetMenu.clear();
	%selected = 0;
	EVisibilityLayers_PresetMenu.add("Select a preset",0);
	for(%presetFile = findFirstFile(%searchFolder); %presetFile !$= ""; %presetFile = findNextFile(%searchFolder)) {
		%presetName = strreplace(fileBase(%presetFile),".layers","");
		EVisibilityLayers_PresetMenu.add(%presetName,%pid++);
		%creatorMenu.add(%presetName,%pid);
	}
	EVisibilityLayers_PresetMenu.setSelected(%selected);
	%creatorMenu.setSelected(%selected);
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
function EVisibilityLayers::loadPresetFile( %this,%filename ) {
	%file = "tlab/EditorLab/editorTools/visibilityLayers/presets/"@%filename@".layers.ucs";
	
	if (!isFile(%file))
		return;
		
	exec(%file);
	
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
	EVisibilityLayers.currentPresetFile = %filename;
	
}

function EVisibilityLayers_Preset::onSelect( %this,%id,%text ) {	
	devLog("EVisibilityLayers_Preset onSelect",%id,%text);
	if (	%id $= "0")
		return;
	EVisibilityLayers.loadPresetFile(%text);
	
	
}
