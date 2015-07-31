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
				
		%pill = LabDevGui.addSetupProfilePill(%prof,%field,%sourceData,%sourceType);
		%pill.key = %key;		
		%pill.data = %data;		
		devLog("New pill id:",%pill);
	}	
}
//------------------------------------------------------------------------------
//==============================================================================
function LabDevGui::addSetupProfilePill( %this,%profile,%fieldData,%sourceData,%sourceType ) {	
	%stack = LDG_ProfileSetupSelectedCtrl-->dataStack;
	%pillSrc = LDG_ProfileSetupSelectedCtrl-->dataPillSrc;	
	hide(%pillSrc);
	%pill = cloneObject(%pillSrc,"",%fieldData);
	%pill.internalName = "";
	%pill.profile = %profile;	
	%pill.field = getWord(%fieldData,0);
	%pill.fieldId = getWord(%fieldData,1);	
	%pill.sourceType = %sourceType;
	
	%pill-->fieldData.setText(%pill.field);
	%pill-->sourceData.setText(%sourceData);		
	%pill-->sourceData.pill = %pill;	
	%pill-->deleteBtn.pill = %pill;
	%pill-->deleteBtn.command = "Lab.removeProfileSetupPill("@%pill.getId()@");";
	
		
	%menu = %pill-->sourceMenu;
	%menu.pill = %pill;
	%menu.clear();
	foreach$(%color in $LDG_TypeList[%sourceType])
		%menu.add(%color,%id++);
	%menu.setText(%sourceData);	
		
	%stack.add(%pill);
	
	devLog("New pill id:",%pill);
	return %pill;
	
}
//------------------------------------------------------------------------------

//==============================================================================
function LabDevGui::newSetupProfilePill( %this ) {	
	
	
	%field = LDG_ProfileSetupTypeMenu.getText();
	%sourceType = $LabDataType_Field[%field];
	%profile = LabDevGui.setupProfile;
	%pill = LabDevGui.addSetupProfilePill(%profile,%field,"",%sourceType);
	%nextId = arGuiProfilesSetupData.count();
	%pill.key = %sourceType@"_"@%nextId;
	%pill.data = %profile.getName() NL %field NL "" NL %sourceType;
}
//------------------------------------------------------------------------------

//==============================================================================
function LDG_ProfileSourceEdit::onValidate( %this ) {	
	devLog("LDG_ProfileSourceEdit Validate:",%this.getText(),"Pill",%this.pill);	

	%this.pill-->sourceMenu.setText(%this.getText());
	Lab.ApplyProfileSetupPill(%this.pill,%this.getText());
}
//------------------------------------------------------------------------------
//==============================================================================
function LDG_ProfileSourceMenu::onSelect( %this,%id,%text ) {	
	devLog("LDG_ProfileSourceMenu onSelect: ID",%id,"Text",%text,"Pill",%this.pill);
	%this.pill-->sourceData.setText(%text);
		Lab.ApplyProfileSetupPill(%this.pill,%text);
	
	
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::ApplyProfileSetupPill( %this,%pill,%value ) {
	%newData = setField(%pill.data,2,%value);	
	arGuiProfilesSetupData.setVal(%pill.key,%newData);
	Lab.applySingleProfileSetupData(%pill.profile,%pill.field,%pill.sourceType,%value,%pill.fieldId);
	
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::removeProfileSetupPill( %this,%pill ) {	
	
	%arrayIndex = arGuiProfilesSetupData.getIndexFromKey(%pill.key);
	 arGuiProfilesSetupData.erase(%arrayIndex);	
	 
	 delObj(%pill);

	//arGuiProfilesSetupData.setVal(%this.pill.key,"DELETED");
	//delObj(%this.pill);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Profiles Setup Tree Functions and Callbacks
//==============================================================================

//==============================================================================
function LDG_ProfilesSetupTree::init( %this ) {	
	if (!isObject(LabProfilesGroup))
		Lab.getSetupProfilesList();
		
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
   %profile = LDG_ProfilesSetupTree.getItemValue( %itemId );
   //Check if we click a profile and not a group
   if (!isObject(%profile)){
   	return;
   }
      
   LabDevGui.selectSetupProfile(%profile);   
  
}
//------------------------------------------------------------------------------