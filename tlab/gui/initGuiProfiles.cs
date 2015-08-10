//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// ->Added Gui style support
// -->Delete Profiles when reloaded
// -->Image array store in Global
//==============================================================================


//Lab.loadScriptsProfiles();
exec("tlab/gui/profiles/profileDefaults.cs");
exec("tlab/gui/profiles/baseProfiles.cs");

//exec("tlab/gui/profiles/defaultProfiles.cs");
%filePathScript = "tlab/gui/profiles/*.prof.cs";
for(%file = findFirstFile(%filePathScript); %file !$= ""; %file = findNextFile(%filePathScript)) {   
    exec( %file );
}
exec("tlab/gui/profiles/editorProfiles.cs");
exec("tlab/gui/profiles/inspectorProfiles.cs");

//execpattern("tlab/gui/profiles/styles/*.cs");
//Lab.initProfileStyleData();

//exec("tlab/gui/profiles/ColorPanel.prof.cs");





