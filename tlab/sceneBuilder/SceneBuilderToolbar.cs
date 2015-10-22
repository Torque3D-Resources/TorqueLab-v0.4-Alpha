//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
function SceneBuilderPlugin::initToolBar( %this ) {
	%menu = SceneBuilderToolbar-->DropTypeMenu;
	%menu.clear();
	%selected = 0;

	foreach$(%type in $TLab_Object_DropTypes) {
		%menu.add(%type,%id++);

		if (EWorldEditor.dropType $= %type)
			%selected = %id;
	}

	%menu.setSelected(%selected,false);
}

//==============================================================================
function Lab::toggleObjectCenter( %this,%updateOnly ) {
	if (!%updateOnly)
		EWorldEditor.objectsUseBoxCenter = !EWorldEditor.objectsUseBoxCenter;

	if( EWorldEditor.objectsUseBoxCenter ) {
		SceneBuilderToolbar-->centerObject.setBitmap("tlab/gui/icons/toolbar_assets/SelObjectCenter.png");
	} else {
		SceneBuilderToolbar-->centerObject.setBitmap("tlab/gui/icons/toolbar_assets/SelObjectBounds.png");
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::toggleObjectTransform( %this ) {
	if (!%updateOnly) {
		if( GlobalGizmoProfile.getFieldValue(alignment) $= "Object" )
			GlobalGizmoProfile.setFieldValue(alignment, World);
		else
			GlobalGizmoProfile.setFieldValue(alignment, Object);
	}

	if( GlobalGizmoProfile.getFieldValue(alignment) $= "Object" ) {
		SceneBuilderToolbar-->objectTransform.setBitmap("tlab/gui/icons/toolbar_assets/TransformObject.png");
		SBP_CreatorTools-->objectTransform.setBitmap("tlab/gui/icons/toolbar_assets/TransformObject.png");
	} else {
		SceneBuilderToolbar-->objectTransform.setBitmap("tlab/gui/icons/toolbar_assets/TransformWorld");
		SBP_CreatorTools-->objectTransform.setBitmap("tlab/gui/icons/toolbar_assets/TransformWorld");
	}
}
//------------------------------------------------------------------------------

//==============================================================================
function Lab::setSelectionLock( %this,%locked ) {
	EWorldEditor.lockSelection(%locked);
	EWorldEditor.syncGui();
}
//------------------------------------------------------------------------------
//==============================================================================
function Lab::setSelectionVisible( %this,%visible ) {
	EWorldEditor.hideSelection(!%visible);
	EWorldEditor.syncGui();
}
//------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::toggleSnapSlider( %this,%sourceObj ) {
//Canvas.pushDialog(softSnapSizeSliderCtrlContainer);
	%srcPos = %sourceObj.getRealPosition();
	%srcPos.y += %sourceObj.extent.y;
	EOverlay.toggleSlider("2",%srcPos,"range \t 0.1 10 \n altCommand \t SceneBuilderPlugin.onSnapSizeSliderChanged($ThisControl);");
}
//------------------------------------------------------------------------------
function SceneBuilderPlugin::onSnapSizeSliderChanged(%this,%slider) {
	%value = mFloatLength(%slider.value,1);
	EWorldEditor.setSoftSnapSize(%value );
	EWorldEditor.syncGui();
}
//------------------------------------------------------------------------------------
//==============================================================================
function SceneBuilderPlugin::toggleGridSizeSlider( %this,%sourceObj ) {
//Canvas.pushDialog(softSnapSizeSliderCtrlContainer);
	%srcPos = %sourceObj.getRealPosition();
	%srcPos.y += %sourceObj.extent.y;
	%step = $WEditor::gridStep;
	if (%step $= "")
		%step = "0.1";
	%range = %step SPC "10";
	
	%ticks = getTicksFromRange(%range,%step);
	EOverlay.toggleSlider("2",%srcPos,"range \t "@%range@" \n ticks \t "@%ticks@"\n altCommand \t SceneBuilderPlugin.onGridSizeSliderChanged($ThisControl);");
}
//------------------------------------------------------------------------------
function SceneBuilderPlugin::onGridSizeSliderChanged(%this,%slider) {
	%value = mFloatLength(%slider.value,1);
	EditorGuiToolbarStack-->WorldEditorGridSizeEdit.setText(%value);
	Lab.setGridSize(%value );
	EWorldEditor.syncGui();
}
//------------------------------------------------------------------------------------

