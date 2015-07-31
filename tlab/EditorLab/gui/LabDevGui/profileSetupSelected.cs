//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function LabDevGui::selectSetupProfile( %this,%profile ) {	
	LabDevGui.setupProfile = %profile;
	
	if (!isObject(arGuiProfilesSetupData))
		Lab.initProfilesSetupData(false,true);
	
	
	%stack = LDG_ProfileSetupSelectedCtrl-->dataStack;
	%pillSrc = LDG_ProfileSetupSelectedCtrl-->dataPillSrc;
	devLog("SelectProfile",%pillSrc);
	hide(%pillSrc);
	show(%stack);
	%stack.clear();
	%array = arGuiProfilesSetupData;
	for( ; %i < %array.count() ; %i++) {
		%key = %array.getKey(%i);
		%data = %array.getValue(%i);
		%prof = getRecord(%data,0);	
		%fieldData = getRecord(%data,1);
		%field = getField(%fieldData,0);
		%fieldId = getField(%fieldData,1);
		%sourceData = getRecord(%data,2);
		%sourceType = getRecord(%data,3);
		devLog("List profile",%prof);
		if (%prof !$= %profile.getName())
			continue;
		
		
		%pill = cloneObject(%pillSrc,"",%key@"__"@%field);
		%pill.profile = %prof;
		%pill.key = %key;
		%pill.field = %field;
		%pill.data = %data;
		%pill.sourceType = %sourceType;
		%pill-->fieldData.setText(%field);
		%pill-->sourceData.setText(%sourceData);		
		%pill-->sourceData.pill = %pill;		
		%menu = %pill-->sourceMenu;
		%menu.pill = %pill;
		%menu.clear();
		foreach$(%color in $LDG_TypeColor[%sourceType])
			%menu.add(%color,%id++);
		%menu.setText(%sourceData);	
		
		%stack.add(%pill);
	}	
}
//------------------------------------------------------------------------------


//==============================================================================
function LDG_ProfileSourceEdit::onValidate( %this ) {	
	devLog("LDG_ProfileSourceEdit Validate:",%this.getText(),"Pill",%this.pill);	
	%key = %this.pill.key;
	%data = %this.pill.data;
	%newData = setField(%data,2,%this.getText());
	%this.pill-->sourceMenu.setText(%this.getText());
	arGuiProfilesSetupData.setVal(%key,%newData);
	arGuiProfilesSetupData.dumpContent();
}
//------------------------------------------------------------------------------
//==============================================================================
function LDG_ProfileSourceMenu::onSelect( %this,%id,%text ) {	
	devLog("LDG_ProfileSourceMenu onSelect: ID",%id,"Text",%text,"Pill",%this.pill);
	%this.pill-->sourceData.setText(%text);
	%key = %this.pill.key;
	%data = %this.pill.data;
	%newData = setField(%data,2,%text);
	arGuiProfilesSetupData.setVal(%key,%newData);
	arGuiProfilesSetupData.dumpContent();
}
//------------------------------------------------------------------------------
//==============================================================================
// Profiles Setup Tree Functions and Callbacks
//==============================================================================

//==============================================================================
function LDG_ProfilesSetupTree::init( %this ) {	
	if (!isObject(LabProfilesGroup))
		getSetupProfilesList();
		
	%this.clear();	

	foreach( %obj in LabProfilesGroup ) {
		// Create a visible name.
		%name = %obj.getName();

		if( %name $= "" )
			continue;		

		%group = %this.findChildItemByName( 0, %obj.category );

		if( !%group )
			%group = %this.insertItem( 0, %obj.category );

		// Insert the item.
		%id = %this.insertItem( %group, %name, %obj.getId(), "" );
	}

	%this.sort( 0, true, true, false );
	%this.schedule(50,"buildVisibleTree");
}
//==============================================================================
function LDG_ProfilesSetupTree::onSelect( %this,%itemId ) {
   devLog("LDG_ProfilesSetupTree::onSelect",%itemId);
   %profile = LDG_ProfilesSetupTree.getItemValue( %itemId );
   if (!isObject(%profile))
      warnlog("Invalid profile selected from the tree:",%profile);
      
   LabDevGui.selectSetupProfile(%profile);   
  
}
//------------------------------------------------------------------------------