//==============================================================================
// TorqueLab -> ShapeEditor -> Sequence Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
function ShapeEd::onAddSequence( %this, %name ) {
	if ( %name $= "" )
		%name = ShapeEditor.getUniqueName( "sequence", "mySequence" );

	// Use the currently selected sequence as the base
	%curSeqName = %this.selectedSequence;
	
	if ( %curSeqName $= "" ) {
		// No sequence selected => open dialog to browse for one
		getLoadFilename( "COLLADA Files|*.dae|DSQ Files|*.dsq|Google Earth Files|*.kmz", %this @ ".onAddSequenceFromBrowse", ShapeEdFromMenu.lastPath );
		return;
	} else {
		%sourceData = ShapeEditor.getSequenceSource( %curSeqName );
		%from = rtrim( getFields( %sourceData, 0, 1 ) );
		%start = getField( %sourceData, 2 );
		%end = getField( %sourceData, 3 );
		%frameCount = getField( %sourceData, 4 );
		
		// Add the new sequence
		
		ShapeEditor.doAddSequence( %name, %curSeqName, %start, %end );
	}
}
function ShapeEd::onAddSequenceFromBrowse( %this, %path ) {
	// Add a new sequence from the browse path
	%path = makeRelativePath( %path, getMainDotCSDir() );
	ShapeEdFromMenu.lastPath = %path;
	%name = ShapeEditor.getUniqueName( "sequence", "mySequence" );
	ShapeEditor.doAddSequence( %name, %path, 0, -1 );
}


//==============================================================================
// ShapeEditor Sequence Update -> Name
//==============================================================================
//==============================================================================
function ShapeEd::onEditSequenceName( %this,%seqName,%newName ) {	
	if ( %seqName !$= "" && %newName !$= "") 	
			ShapeEditor.doRenameSequence( %seqName, %newName );
	
}

//==============================================================================
// ShapeEditor Sequence Update -> Cyclic
//==============================================================================
//==============================================================================
function ShapeEd::onToggleCyclic( %this,%seqName ) {
	%seqName = ShapeEdSequenceList.getSelectedName();

	if ( %seqName !$= "" ) {
		%cyclic = %this-->cyclicFlag.getValue();
		ShapeEditor.doEditCyclic( %seqName, %cyclic );
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// ShapeEditor Sequence Update -> Priority
//==============================================================================
//==============================================================================
function ShapeEd::onEditPriority( %this,%seqName ) {
	//%seqName = ShapeEdSequenceList.getSelectedName();

	if ( %seqName !$= "" ) {
		%newPriority = %this-->priority.getText();

		if ( %newPriority !$= "" )
			ShapeEditor.doEditSequencePriority( %seqName, %newPriority );
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// ShapeEditor Sequence Update -> Belnding
//==============================================================================
//==============================================================================
function ShapeEd::onEditBlend( %this,%seqName,%pill ) {
	if (%seqName $= "")
		%seqName = ShapeEd.selectedSequence;

	if ( %seqName !$= "" ) {
		// Get the blend flags (current and new)
		%oldBlendData = ShapeEditor.shape.getSequenceBlend( %seqName );
		%oldBlend = getField( %oldBlendData, 0 );
		%blend = %pill-->blend.isStateOn();
		//%blend = %this-->blendFlag.getValue();
		if (%blend $= "")
			%blend = %oldBlend;
		// Ignore changes to the blend reference for non-blend sequences
		if ( !%oldBlend && !%blend )
			return;

		// OK - we're trying to change the blend properties of this sequence. The
		// new reference sequence and frame must be set.
		%blendSeq = %pill-->blendSeq.getText();
		%blendFrame = %pill-->blendFrame.getText();

		if ( ( %blendSeq $= "" ) || ( %blendFrame $= "" ) ) {
			LabMsgOK( "Blend reference not set", "The blend reference sequence and " @
						 "frame must be set before changing the blend flag or frame." );
			%pill-->blend.setStateOn( %oldBlend );
			return;
		}

		// Get the current blend properties (use new values if not specified)
		%oldBlendSeq = getField( %oldBlendData, 1 );

		if ( %oldBlendSeq $= "" )
			%oldBlendSeq = %blendSeq;

		%oldBlendFrame = getField( %oldBlendData, 2 );

		if ( %oldBlendFrame $= "" )
			%oldBlendFrame = %blendFrame;

		// Check if there is anything to do
		if ( ( %oldBlend TAB %oldBlendSeq TAB %oldBlendFrame ) !$= ( %blend TAB %blendSeq TAB %blendFrame ) )
			ShapeEditor.doEditBlend( %seqName, %blend, %blendSeq, %blendFrame );
	}
}
//------------------------------------------------------------------------------

//==============================================================================
// ShapeEditor Sequence Update -> Belnding
//==============================================================================
//==============================================================================
function ShapeEd::onEditSequenceSource( %this, %from ,%pill ) {
	// ignore for shapes without sequences
	if (ShapeEditor.shape.getSequenceCount() == 0)
		return;

	%start = %pill-->frameIn.getText();
	%end = %pill-->frameOut.getText();
	%seqName = %pill.seqName;
	if ( ( %start !$= "" ) && ( %end !$= "" ) ) {
		
		%oldSource = ShapeEditor.getSequenceSource( %seqName );

		if ( %from $= "" )
			%from = rtrim( getFields( %oldSource, 0, 0 ) );

		if ( getFields( %oldSource, 0, 3 ) !$= ( %from TAB "" TAB %start TAB %end ) )
			ShapeEditor.doEditSeqSource( %seqName, %from, %start, %end );
	}
}
