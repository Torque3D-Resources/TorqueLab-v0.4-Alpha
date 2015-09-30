//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$VisibilityOptionsLoaded = false;
$VisibilityClassLoaded = false;
$EVisibilityLayers_Initialized = false;
//==============================================================================
function EVisibilityLayers::toggleVisibility(%this) {
	devLog("EVisibilityLayersvisibility is",%dlg.visible);
	ETools.toggleTool("VisibilityLayers");
	visibilityToggleBtn.setStateOn(%this.visible);

	if ( %this.visible  ) {
		%this.onShow();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function EVisibilityLayers::onShow( %this ) {
	// Create the array if it doesn't already exist.
	if ( !isObject( ar_EVisibilityLayers ) )
		%array = newArrayObject("ar_EVisibilityLayers");

	// Create the array if it doesn't already exist.
	if ( !isObject( ar_EVisibilityLayersClass ) )
		%classArray = newArrayObject("ar_EVisibilityLayersClass");

	%this.array = ar_EVisibilityLayers;
	%this.classArray = ar_EVisibilityLayersClass;
	
	%this.updatePresetMenu();


	if(!$EVisibilityLayers_Initialized) {
		//EVisibilityLayers.position = visibilityToggleBtn.position;
		%this.initOptions();
		%this.addClassOptions();
		$EVisibilityLayers_Initialized = true;
	}

	%this.updateOptions();
}
//------------------------------------------------------------------------------
function EVisibilityLayers::updateOptions( %this ) {
	// First clear the stack control.
	%this-->theVisOptionsList.clear();

	// Go through all the
	// parameters in our array and
	// create a check box for each.
	for ( %i = 0; %i < %this.array.count(); %i++ ) {
		%text = "  " @ %this.array.getValue( %i );
		%val = %this.array.getKey( %i );
		%var = getWord( %val, 0 );
		%toggleFunction = getWord( %val, 1 );
		%textLength = strlen( %text );
		%cmd = "";

		if ( %toggleFunction !$= "" )
			%cmd = %toggleFunction @ "( $thisControl.getValue() );";

		%checkBox = new GuiCheckBoxCtrl() {
			canSaveDynamicFields = "0";
			isContainer = "0";
			Profile = "ToolsCheckBoxProfile_S1";
			HorizSizing = "right";
			VertSizing = "bottom";
			Position = "0 0";
			Extent = (%textLength * 4) @ " 18";
			MinExtent = "8 2";
			canSave = "1";
			Visible = "1";
			Variable = %var;
			tooltipprofile = "ToolsToolTipProfile";
			hovertime = "1000";
			text = %text;
			groupNum = "-1";
			buttonType = "ToggleButton";
			useMouseEvents = "0";
			useInactiveState = "0";
			Command = %cmd;
		};
		%this-->theVisOptionsList.addGuiControl( %checkBox );
	}
}

function EVisibilityLayers::initOptions( %this ) {
// Expose stock visibility/debug options.
	%this.addOption( "Render: Zones", "$Zone::isRenderable", "" );
	%this.addOption( "Render: Portals", "$Portal::isRenderable", "" );
	%this.addOption( "Render: Occlusion Volumes", "$OcclusionVolume::isRenderable", "" );
	%this.addOption( "Render: Triggers", "$Trigger::renderTriggers", "" );
	%this.addOption( "Render: PhysicalZones", "$PhysicalZone::renderZones", "" );
	%this.addOption( "Render: Sound Emitters", "$SFXEmitter::renderEmitters", "" );
	%this.addOption( "Render: Mission Area", "EWorldEditor.renderMissionArea", "" );
	%this.addOption( "Render: Sound Spaces", "$SFXSpace::isRenderable", "" );
	%this.addOption( "Wireframe Mode", "$gfx::wireframe", "" );
	%this.addOption( "Debug Render: Player Collision", "$Player::renderCollision", "" );
	%this.addOption( "Debug Render: Terrain", "TerrainBlock::debugRender", "" );
	%this.addOption( "Debug Render: Decals", "$Decals::debugRender", "" );
	%this.addOption( "Debug Render: Light Frustums", "$Light::renderLightFrustums", "" );
	%this.addOption( "Debug Render: Bounding Boxes", "$Scene::renderBoundingBoxes", "" );
	%this.addOption( "AL: Disable Shadows", "$Shadows::disable", "" );
	%this.addOption( "AL: Light Color Viz", "$AL_LightColorVisualizeVar", "toggleLightColorViz" );
	%this.addOption( "AL: Light Specular Viz", "$AL_LightSpecularVisualizeVar", "toggleLightSpecularViz" );
	%this.addOption( "AL: Normals Viz", "$AL_NormalsVisualizeVar", "toggleNormalsViz" );
	%this.addOption( "AL: Depth Viz", "$AL_DepthVisualizeVar", "toggleDepthViz" );
	%this.addOption( "Frustum Lock", "$Scene::lockCull", "" );
	%this.addOption( "Disable Zone Culling", "$Scene::disableZoneCulling", "" );
	%this.addOption( "Disable Terrain Occlusion", "$Scene::disableTerrainOcclusion", "" );
	
	//PBR Script
	%this.addOption( "Debug Render: Physics World", "$PhysicsWorld::render", "togglePhysicsDebugViz" );
  
   %this.addOption( "AL: Environment Light", "$AL_LightMapShaderVar", "toggleLightMapViz" );
  
   %this.addOption( "AL: Color Buffer", "$AL_ColorBufferShaderVar", "toggleColorBufferViz" );
   %this.addOption( "AL: Spec Map(Rough)", "$AL_RoughMapShaderVar", "toggleRoughMapViz");
   %this.addOption( "AL: Spec Map(Metal)", "$AL_MetalMapShaderVar", "toggleMetalMapViz");
   %this.addOption( "AL: Backbuffer", "$AL_BackbufferVisualizeVar", "toggleBackbufferViz" );
	//PBR Script End
	$VisibilityOptionsLoaded = true;
}
function EVisibilityLayers::addOption( %this, %text, %varName, %toggleFunction ) {
	// Create the array if it
	// doesn't already exist.
	if ( !isObject( %this.array ) )
		%this.array = new ArrayObject();

	%this.array.push_back( %varName @ " " @ %toggleFunction, %text );
	%this.array.uniqueKey();
	%this.array.sortd();
	//%this.updateOptions();
}
//EVisibilityLayers.addClassOptions
function EVisibilityLayers::addClassOptions( %this ) {
	%visList = %this-->theClassVisList;
	%selList = %this-->theClassSelList;
	// First clear the stack control.
	%visList.clear();
	%selList.clear();
	%classList = enumerateConsoleClasses( "SceneObject" );
	%classCount = getFieldCount( %classList );

	for ( %i = 0; %i < %classCount; %i++ ) {
		%className = getField( %classList, %i );
		%this.classArray.push_back( %className );
	}

	// Remove duplicates and sort by key.
	%this.classArray.uniqueKey();
	%this.classArray.sortkd();

	// Go through all the
	// parameters in our array and
	// create a check box for each.
	for ( %i = 0; %i < %this.classArray.count(); %i++ ) {
		%class = %this.classArray.getKey( %i );
		%visVar = "$" @ %class @ "::isRenderable";
		%selVar = "$" @ %class @ "::isSelectable";
		
		%textLength = strlen( %class );
		%text = "  " @ %class;
		// Add visibility toggle.
		%visCheckBox = new GuiCheckBoxCtrl() {
			canSaveDynamicFields = "0";
			isContainer = "0";
			Profile = "ToolsCheckBoxProfile_S1";
			HorizSizing = "right";
			VertSizing = "bottom";
			Position = "0 0";
			Extent = (%textLength * 4) @ " 18";
			MinExtent = "8 2";
			canSave = "1";
			Visible = "1";
			Variable = %visVar;
			command = "EVisibilityLayers.toggleRenderable(\""@%class@"\");";
			tooltipprofile = "ToolsToolTipProfile";
			hovertime = "1000";
			tooltip = "Show/hide all " @ %class @ " objects.";
			text = %text;
			groupNum = "-1";
			buttonType = "ToggleButton";
			useMouseEvents = "0";
			useInactiveState = "0";
		};
		
		//Variable = %visVar;
		%visList.addGuiControl( %visCheckBox );
		// Add selectability toggle.
		%selCheckBox = new GuiCheckBoxCtrl() {
			canSaveDynamicFields = "0";
			isContainer = "0";
			Profile = "ToolsCheckBoxProfile_S1";
			HorizSizing = "right";
			VertSizing = "bottom";
			Position = "0 0";
			Extent = (%textLength * 4) @ " 18";
			MinExtent = "8 2";
			canSave = "1";
			Visible = "1";
			command = "EVisibilityLayers.toggleSelectable(\""@%class@"\");";
			Variable = %selVar;
			tooltipprofile = "ToolsToolTipProfile";
			hovertime = "1000";
			tooltip = "Enable/disable selection of all " @ %class @ " objects.";
			text = %text;
			groupNum = "-1";
			buttonType = "ToggleButton";
			useMouseEvents = "0";
			useInactiveState = "0";
		};
		//Variable = %selVar;
		%selList.addGuiControl( %selCheckBox );
		//eval("%visON = $" @ %class @ "::isRenderable;");
		//eval("%selON = $" @ %class @ "::isSelectable;");
		//%visCheckBox.setStateOn(%visON);
		//%selCheckBox.setStateOn(%selON);
	}

	$VisibilityClassLoaded = true;
}
//==============================================================================

function EVisibilityLayers::toggleRenderable( %this,%class ) {
	
	//eval("$" @ %class @ "::isRenderable = !$"@ %class @ "::isRenderable;");
	%visVar = "$" @ %class @ "::isRenderable";
	//%selVar = "$" @ %class @ "::isSelectable";
	if (!%visVar)
		eval("$" @ %class @ "::isSelectable = \"0\";");
	else
		eval("$" @ %class @ "::isSelectable = \"1\";");
}
function EVisibilityLayers::toggleSelectable( %this,%class ) {
	//eval("$" @ %class @ "::isRenderable = !$"@ %class @ "::isRenderable;");
	%selVar = "$" @ %class @ "::isSelectable";
	if (%selVar)
		eval("$" @ %class @ "::isRenderable = \"1\";");
}		