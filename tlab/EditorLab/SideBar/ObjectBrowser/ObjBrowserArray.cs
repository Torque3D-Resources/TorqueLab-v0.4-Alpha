//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
//ObjectBrowser.initFiles
function ObjectBrowser::initFiles(%this,%path) {
	%lastAddress = ObjectBrowser.lastAddress;
	if (%lastAddress $= "")
		%lastAddress = "art";
	ObjectBrowser.navigate( %lastAddress );
}
//------------------------------------------------------------------------------
//==============================================================================
//ObjectBrowser.initFiles
function ObjectBrowser::setViewId(%this,%id,%force) {
	
	if (ObjectBrowser.currentViewId $= %id && !%force)
		return;
	
	%text = getField($ObjectBrowser_View[%i],%id);
		
	switch$(%id){
		case "0":
			%width = (ObjectBrowserArray.extent.x - 10);
		case "1":
			%width = (ObjectBrowserArray.extent.x - 10) /2;
		case "2":
			%width = (ObjectBrowserArray.extent.x - 10) /3;
			
	}
	if (%width !$= ""){
		ObjectBrowserArray.colSize = %width;
		ObjectBrowserArray.refresh();
		ObjectBrowser.currentViewId = %id;
		ObjectBrowserViewMenu.setText(getField($ObjectBrowser_View[%id],0));
	}

}
//------------------------------------------------------------------------------
if ( %this.isList ) {
		%width = ObjectBrowserArray.extent.x - 6;
		ObjectBrowserArray.colSize = %width;
		%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		//%ctrl.autoSize = true;
	} else {
		%width = (ObjectBrowserArray.extent.x - 10) /2;
		//%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		ObjectBrowserArray.colSize = %width;
	}
//==============================================================================
function ObjectBrowserMenu::onSelect( %this, %id, %text ) {
	%split = strreplace( %text, "/", " " );
	ObjectBrowser.navigate( %split );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::navigateDown( %this, %folder ) {
	if ( %this.address $= "" )
		%address = %folder;
	else
		%address = %this.address SPC %folder;

	// Because this is called from an IconButton::onClick command
	// we have to wait a tick before actually calling navigate, else
	// we would delete the button out from under itself.
	%this.schedule( 1, "navigate", %address );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::navigateUp( %this ) {
	%count = getWordCount( %this.address );

	if ( %count == 0 )
		return;

	if ( %count == 1 )
		%address = "";
	else
		%address = getWords( %this.address, 0, %count - 2 );

	%this.navigate( %address );
}
//------------------------------------------------------------------------------

//==============================================================================
// Navigate through Creator Book Data
function ObjectBrowser::navigate( %this, %address ) {
	ObjectBrowserArray.frozen = true;
	ObjectBrowserArray.clear();
	ObjectBrowserMenu.clear();
	show(ObjectBrowserArray);
	hide(ObjectBrowserIconSrc);
	%type = ObjectBrowser.objType;
	if (!%this.isMethod("navigate"@%type)||%type $= ""){
		warnLog("Couldn't find a scene creator navigating function for type:",%type);
		return;
	}
	eval("%this.navigate"@%type@"(%address);");

	
	ObjectBrowser.lastTypeAddress[%type] = %address;
	ObjectBrowserArray.sort( "alphaIconCompare" );

	for ( %i = 0; %i < ObjectBrowserArray.getCount(); %i++ ) {
		ObjectBrowserArray.getObject(%i).autoSize = false;
	}
	ObjectBrowser.addFolderUpIcon();
	%this.setViewId($ObjectBrowser_ViewId);
	ObjectBrowserArray.refresh();
	/*if ( %this.isList ) {
		%width = ObjectBrowserArray.extent.x - 6;
		ObjectBrowserArray.colSize = %width;
		%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		//%ctrl.autoSize = true;
	} else {
		%width = (ObjectBrowserArray.extent.x - 10) /2;
		//%extent = %width SPC "20";
		//%ctrl.extent = %extent;
		ObjectBrowserArray.colSize = %width;
	}*/
	
	//ObjectBrowserArray.frozen = false;
	//ObjectBrowserArray.refresh();
	// Recalculate the array for the parent guiScrollCtrl
	ObjectBrowserArray.getParent().computeSizes();
	%this.address = %address;
	ObjectBrowserMenu.sort();
	%str = strreplace( %address, " ", "/" );
	%r = ObjectBrowserMenu.findText( %str );

	if ( %r != -1 )
		ObjectBrowserMenu.setSelected( %r, false );
	else
		ObjectBrowserMenu.setText( %str );

	ObjectBrowserMenu.tooltip = %str;
}

//------------------------------------------------------------------------------
//==============================================================================
// Navigate through Creator Book Data
function ObjectBrowser::navigateLevel( %this, %address ) {
	// Add groups to popup menu
	%array = Scene.array;
	%array.sortk();
	%count = %array.count();

	if ( %count > 0 ) {
		%lastGroup = "";

		for ( %i = 0; %i < %count; %i++ ) {
			%group = %array.getKey( %i );

			if ( %group !$= %lastGroup ) {
				ObjectBrowserMenu.add( %group );

				if ( %address $= "" )
					%this.addFolderIcon( %group );
			}

			if ( %address $= %group ) {
				%args = %array.getValue( %i );
				%class = %args.val[0];
				%name = %args.val[1];
				%func = %args.val[2];
				%this.addMissionObjectIcon( %class, %name, %func );
			}

			%lastGroup = %group;
		}
	}
}
//==============================================================================
// Navigate through Creator Book Data
function ObjectBrowser::navigateScripted( %this, %address )
{
	%category = getWord( %address, 1 );
	%dataGroup = "DataBlockGroup";

	for ( %i = 0; %i < %dataGroup.getCount(); %i++ )
	{
		%obj = %dataGroup.getObject(%i);
		// echo ("Obj: " @ %obj.getName() @ " - " @ %obj.category );

		if ( %obj.category $= "" && %obj.category == 0 )
			continue;

		// Add category to popup menu if not there already
		if ( ObjectBrowserMenu.findText( %obj.category ) == -1 )
			ObjectBrowserMenu.add( %obj.category );

		if ( %address $= "" )
		{
			%ctrl = %this.findIconCtrl( %obj.category );

			if ( %ctrl == -1 )
			{
				%this.addFolderIcon( %obj.category );
			}
		}
		else if ( %address $= %obj.category )
		{
			%ctrl = %this.findIconCtrl( %obj.getName() );

			if ( %ctrl == -1 )
				%this.addShapeIcon( %obj );
		}
	}
}
//==============================================================================
function ObjectBrowser::createIcon( %this ) {
	%ctrl = cloneObject(ObjectBrowserIconSrc);
	%ctrl.profile = "ToolsButtonArray";
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%ctrl.superClass = "ObjectBrowserIcon";

	

	return %ctrl;
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::addFileIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );

	if (strFindWords(strlwr(%ext),"dae dts")) {
		%createCmd = "Scene.createMesh( \"" @ %fullPath @ "\" );";
		%type = "Mesh";
		%ctrl.createCmd = %createCmd;
		
	} else if (strFindWords(strlwr(%ext),"png tga jpg bmp")) {
		%type = "Image";
	} else	{
		%type = "Unknown";
	}

	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	
	%ctrl.altCommand = "ObjectBrowser.icon"@%type@"Alt($ThisControl);";
	%ctrl.iconBitmap = ( ( %ext $= ".dts" ) ? EditorIconRegistry::findIconByClassName( "TSStatic" ) : "tlab/gui/icons/default/iconCollada" );
	%ctrl.text = %file;
	%ctrl.type = %type;
	%ctrl.class = "ObjectBrowserIcon";
	//%ctrl.superClass = "ObjectBrowserIcon"@%type;
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	
	%ctrl.fullPath = %fullPath;
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//ObjectBrowser.addFolderUpIcon
function ObjectBrowser::addFolderUpIcon( %this ) {	
		
	%ctrl = %this.createIcon();
	%ctrl.command = "ObjectBrowser.navigateUp();";
	%ctrl.altCommand = "ObjectBrowser.navigateUp();";
	%ctrl.iconBitmap = "tlab/gui/icons/24-assets/folder_up.png";
	%ctrl.text = "..";
	%ctrl.tooltip = "Go to parent folder";
	%ctrl.buttonMargin = "8 1";
	%ctrl.sizeIconToButton = true;
	%ctrl.makeIconSquare = true;
	//%ctrl.class = "CreatorFolderIconBtn";
	%ctrl.buttonType = "PushButton";
	
	ObjectBrowserArray.addGuiControl( %ctrl );
	ObjectBrowserArray.bringToFront(%ctrl);
	
}
//------------------------------------------------------------------------------

//==============================================================================
function ObjectBrowser::addFolderIcon( %this, %text ) {	
		
	%ctrl = %this.createIcon();
	%ctrl.command = "ObjectBrowser.iconFolderAlt($ThisControl);";
	%ctrl.altCommand = "ObjectBrowser.iconFolderAlt($ThisControl);";
	%ctrl.iconBitmap = "tlab/gui/icons/24-assets/folder_open.png";
	%ctrl.text = %text;
	%ctrl.tooltip = %text;
	%ctrl.class = "CreatorFolderIconBtn";
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%ctrl.buttonMargin = "6 0";
	%ctrl.sizeIconToButton = true;
	%ctrl.makeIconSquare = true;
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::findIconCtrl( %this, %name ) {
	for ( %i = 0; %i < ObjectBrowserArray.getCount(); %i++ ) {
		%ctrl = ObjectBrowserArray.getObject( %i );

		if ( %ctrl.text $= %name )
			return %ctrl;
	}

	return -1;
}
//------------------------------------------------------------------------------

//==============================================================================
function ObjectBrowser::addMissionObjectIcon( %this, %class, %name, %buildfunc ) {
	%ctrl = %this.createIcon();
	// If we don't find a specific function for building an
	// object then fall back to the stock one
	%method = "build" @ %buildfunc;
	devLog("Method = ",%method);
	if( !ObjectBuilderGui.isMethod( %method ) )
		%method = "build" @ %class;
	
	if( !ObjectBuilderGui.isMethod( %method ) )
		%method = "build" @ %class;
		
	if( !ObjectBuilderGui.isMethod( %method ) ){
		%func = "build" @ %buildfunc;
		if (isFunction("build" @ %buildfunc))
			%cmd = "return build" @ %buildfunc @ "();";
		else
			%cmd = "return new " @ %class @ "();";
	}
	else{
		%cmd = "ObjectBuilderGui." @ %method @ "();";
	}

	%ctrl.altCommand = "ObjectBuilderGui.newObjectCallback = \"ObjectBrowser.onFinishCreateObject\"; Scene.createObject( \"" @ %cmd @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( %class );
	%ctrl.text = %name;
	%ctrl.class = "CreatorMissionObjectIconBtn";
	%ctrl.tooltip = %class;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	%ctrl.type = "MissionObject";
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::addShapeIcon( %this, %datablock ) {
	%ctrl = %this.createIcon();
	%name = %datablock.getName();
	%class = %datablock.getClassName();
	%cmd = %class @ "::create(" @ %name @ ");";
	%shapePath = ( %datablock.shapeFile !$= "" ) ? %datablock.shapeFile : %datablock.shapeName;
	%createCmd = "Scene.createObject( \\\"" @ %cmd @ "\\\" );";
	%ctrl.altCommand = "ColladaImportDlg.showDialog( \"" @ %shapePath @ "\", \"" @ %createCmd @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( %class );
	%ctrl.text = %name;
	%ctrl.class = "CreatorShapeIconBtn";
	%ctrl.tooltip = %name;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::addStaticIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%createCmd = "Scene.createStatic( \\\"" @ %fullPath @ "\\\" );";
	%ctrl.altCommand = "ColladaImportDlg.showDialog( \"" @ %fullPath @ "\", \"" @ %createCmd @ "\" );";
	%ctrl.iconBitmap = ( ( %ext $= ".dts" ) ? EditorIconRegistry::findIconByClassName( "TSStatic" ) : "tlab/gui/icons/default/iconCollada" );
	%ctrl.text = %file;
	%ctrl.class = "CreatorStaticIconBtn";
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------
//==============================================================================
function ObjectBrowser::addPrefabIcon( %this, %fullPath ) {
	%ctrl = %this.createIcon();
	%ext = fileExt( %fullPath );
	%file = fileBase( %fullPath );
	%fileLong = %file @ %ext;
	%tip = %fileLong NL
			 "Size: " @ fileSize( %fullPath ) / 1000.0 SPC "KB" NL
			 "Date Created: " @ fileCreatedTime( %fullPath ) NL
			 "Last Modified: " @ fileModifiedTime( %fullPath );
	%ctrl.altCommand = "Scene.createPrefab( \"" @ %fullPath @ "\" );";
	%ctrl.iconBitmap = EditorIconRegistry::findIconByClassName( "Prefab" );
	%ctrl.text = %file;
	%ctrl.class = "CreatorPrefabIconBtn";
	%ctrl.tooltip = %tip;
	%ctrl.buttonType = "radioButton";
	%ctrl.groupNum = "-1";
	ObjectBrowserArray.addGuiControl( %ctrl );
}
//------------------------------------------------------------------------------