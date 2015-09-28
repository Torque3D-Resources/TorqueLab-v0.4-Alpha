//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function ShapeEd::addSequencePill(%this,%seqName) {
	
	%cyclic = ShapeEditor.shape.getSequenceCyclic( %seqName );
	%blend = getField( ShapeEditor.shape.getSequenceBlend( %seqName ), 0 );
	%frameCount = ShapeEditor.shape.getSequenceFrameCount( %seqName );
	%priority = ShapeEditor.shape.getSequencePriority( %seqName );
	%sourceData = ShapeEditor.getSequenceSource( %seqName );
		%seqFrom = rtrim( getFields( %sourceData, 0, 1 ) );
		%seqStart = getField( %sourceData, 2 );
		%seqEnd = getField( %sourceData, 3 );
		%seqFromTotal = getField( %sourceData, 4 );
	
	
	hide(ShapeEd_SeqPillSource);
	%pill = cloneObject(ShapeEd_SeqPillSource,"",%seqName,ShapeEd_SeqPillStack);
	%pill-->Cyclic.setStateOn(%cyclic);
	%pill-->Blend.setStateOn(%blend);
	%pill-->frameCount.setText(%frameCount);
	%pill-->priority.setText(%priority);
	%pill-->seqName.setText(%seqName);
	%pill-->frameOut.setText(%seqEnd);
	%pill-->frameIn.setText(%seqStart);
	%pill-->sourceSeq.setText("Source");
	%pill-->blendSeq.setText("No blend");
	%pill-->Cyclic.pill = %pill;
	%pill-->Blend.pill = %pill;
	%pill-->frameCount.pill = %pill;
	%pill-->priority.pill = %pill;
	%pill-->seqName.pill = %pill;
	%pill-->sourceSeq.pill = %pill;
	%pill-->blendSeq.pill = %pill;
		%pill-->frameCount.active = 1;
	%pill-->priority.active = 1;
	%pill-->seqName.active = 1;
	%pill-->frameOut.active = 1;
	%pill-->frameIn.active = 1;
	%pill-->Blend.active = 1;
	%pill-->Cyclic.active = 1;
	%pill.seqName = %seqName;
	%pill.expanded = false;
	%pill.superClass = "ShapeEd_SeqPillRollout";
}
//------------------------------------------------------------------------------

//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function ShapeEdAnim_SeqPillEdit::onValidate(%this) {
	%type = %this.internalName;
	%pill = %this.pill;
	%seqName = %pill.seqName;
	switch$(%type){
		case "frameIn":
			%frameCount = getWord( ShapeEdSeqSlider.range, 1 );
			// Force value to a frame index within the slider range
			%val = mRound( %this.getText() );
			if ( %val < 0 ) %val = 0;
			if ( %val > %frameCount ) %val = %frameCount;		
			if ( %val >= %pill-->frameOut.getText() )
					%val = %pill-->frameOut.getText() - 1;

			%this.setText( %val );
				
			ShapeEd.onEditSequenceSource("",%pill);
		case "frameOut":
			%frameCount = getWord( ShapeEdSeqSlider.range, 1 );
			// Force value to a frame index within the slider range
			%val = mRound( %this.getText() );
			if ( %val < 0 ) %val = 0;
			if ( %val > %frameCount ) %val = %frameCount;		
			if ( %val <= %pill-->frameIn.getText() )
					%val = %pill-->frameIn.getText() + 1;

			%this.setText( %val );
			
				ShapeEd.onEditSequenceSource("",%pill);
		case "priority":
			%newPriority = %this.getText();
			if ( %newPriority !$= "" )
				ShapeEditor.doEditSequencePriority( %seqName, %newPriority );
			
		case "seqName":
		%newName = %this.getText();
		if (%newName !$= %seqName){			
			ShapeEd.onEditSequenceName(%seqName,%newName);
			%pill.seqName = %newName;
		}
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function ShapeEdAnim_SeqPillCheck::onClick(%this) {
	%type = %this.internalName;
	%pill = %this.pill;
	%seqName = %pill.seqName;
	switch$(%type){
		case "Cyclic":
		devLog("Set cyclic to",%this.isStateOn(),"For",%seqName);
			ShapeEditor.doEditCyclic( %seqName, %this.isStateOn() );
		case "Blend":
			ShapeEd.onEditBlend(%seqName,%pill );
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function ShapeEd_SeqPillRollout::onExpanded(%this) {
	ShapeEd.selectedSequence = %this.seqName;
	
	if (!isObject(ShapeEditor.shape)){
		ShapeEd.onSequenceObjectInvalid();
	}
	
	%seqName = %this.seqName;
	%sourceMenu = %this-->sourceSeq;
	%blendMenu = %this-->blendSeq;
	%sourceMenu.clear();
	%blendMenu.clear();
	%sourceMenu.add( "Browse..." );
	
	%count = ShapeEditor.shape.getSequenceCount();
	for ( %i = 0; %i < %count; %i++ ) {
		%name = ShapeEditor.shape.getSequenceName( %i );	

			if ( %name !$= %seqName ) {
				%sourceMenu.add( %name );
				%blendMenu.add( %name );
			}
		}
	%blendData = ShapeEditor.shape.getSequenceBlend( %seqName );
	%blendText = getField( %blendData, 1 );
	if (%blendText $= "")
		%blendText = "No blend";
	%blendMenu.setText( %blendText );		
	%curSource = getField(ShapeEditor.getSequenceSource( %seqName ),0);
	
	%sourceMenu.setText( %curSource );	
	devLog("Selected sequence is:",ShapeEd.selectedSequence);
}
//------------------------------------------------------------------------------
//==============================================================================
// Load the Scene Editor Plugin scripts, load Guis if %loadgui = true
function ShapeEdAnim_SeqPillMenu::onSelect(%this,%id,%text) {
	%type = %this.internalName;
	%pill = %this.pill;
	%seqName = %pill.seqName;
	switch$(%type){
		case "sourceSeq":
			if ( %text $= "Browse..." ) {				
				%seqFrom = rtrim( getFields( ShapeEditor.getSequenceSource( %seqName ), 0, 1 ) );
				%this.setText( %seqFrom );
				// Allow the user to browse for an external source of animation data
				getLoadFilename( "COLLADA Files|*.dae|DSQ Files|*.dsq|Google Earth Files|*.kmz", %this @ ".onBrowseSelect", %this.lastPath );
			} else {
				ShapeEd.onEditSequenceSource( %text,%pill );
			}
		
		case "blendSeq":
			ShapeEd.onEditBlend(%seqName,"" );
	}	
}
//------------------------------------------------------------------------------

