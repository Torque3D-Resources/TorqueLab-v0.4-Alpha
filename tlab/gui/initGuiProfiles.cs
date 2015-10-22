//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
// ->Added Gui style support
// -->Delete Profiles when reloaded
// -->Image array store in Global
//==============================================================================
$TLab_Theme = "DarkLab";

//Lab.loadScriptsProfiles();
%profilesPath = "tlab/themes/"@$TLab_Theme@"/";
$TLab_ThemePath = %profilesPath;
exec(%profilesPath@"profileDefaults.cs");
exec(%profilesPath@"baseProfiles.cs");

//exec("tlab/gui/profiles/defaultProfiles.cs");
%filePathScript = %profilesPath@"*.prof.cs";
for(%file = findFirstFile(%filePathScript); %file !$= ""; %file = findNextFile(%filePathScript)) {   
    exec( %file );
}
exec(%profilesPath@"editorProfiles.cs");
exec(%profilesPath@"inspectorProfiles.cs");

//execpattern("tlab/gui/profiles/styles/*.cs");
//Lab.initProfileStyleData();

//exec("tlab/gui/profiles/ColorPanel.prof.cs");





