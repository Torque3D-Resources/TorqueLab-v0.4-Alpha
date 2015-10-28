//==============================================================================
// TorqueLab -> ShapeEditor -> Threads and Animation
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// ShapeEditor -> Threads and Animation
//==============================================================================


function ShapeEdThreadViewer::onWake( %this ) {
	%this-->useTransitions.setValue( 1 );
	%this-->transitionTime.setText( "0.5" );
	%this-->transitionTo.clear();
	%this-->transitionTo.add( "synched position", 0 );
	%this-->transitionTo.add( "slider position", 1 );
	%this-->transitionTo.setSelected( 0 );
	%this-->transitionTarget.clear();
	%this-->transitionTarget.add( "plays during transition", 0 );
	%this-->transitionTarget.add( "pauses during transition", 1 );
	%this-->transitionTarget.setSelected( 0 );
}



function ShapeEdThreadViewer::syncPlaybackDetails( %this ) {
	%seqName = ShapeEd.selectedSequence;

	if ( %seqName !$= "" ) {
		// Show sequence in/out bars
		ShapeEdAnimWindow-->seqInBar.setVisible( true );
		ShapeEdAnimWindow-->seqOutBar.setVisible( true );
		// Sync playback controls
		%sourceData = ShapeEditor.getSequenceSource( %seqName );
		%seqFrom = rtrim( getFields( %sourceData, 0, 1 ) );
		%seqStart = getField( %sourceData, 2 );
		%seqEnd = getField( %sourceData, 3 );
		%seqFromTotal = getField( %sourceData, 4 );

		// Display the original source for edited sequences
		if ( startswith( %seqFrom, "__backup__" ) ) {
			%backupData = ShapeEditor.getSequenceSource( getField( %seqFrom, 0 ) );
			%seqFrom = rtrim( getFields( %backupData, 0, 1 ) );
		}

		ShapeEdSeqFromMenu.setText( %seqFrom );
		ShapeEdSeqFromMenu.tooltip = ShapeEdSeqFromMenu.getText();   // use tooltip to show long names
		ShapeEdSequences-->startFrame.setText( %seqStart );
		ShapeEdSequences-->endFrame.setText( %seqEnd );
		%val = ShapeEdSeqSlider.getValue() / getWord( ShapeEdSeqSlider.range, 1 );
		ShapeEdSeqSlider.range = "0" SPC ( %seqFromTotal-1 );
		ShapeEdSeqSlider.setValue( %val * getWord( ShapeEdSeqSlider.range, 1 ) );
		ShapeEd_ThreadSlider.range = ShapeEdSeqSlider.range;
		ShapeEd_ThreadSlider.setValue( ShapeEdSeqSlider.value );
		ShapeEdAnimWindow.setSequence( %seqName );
		ShapeEdAnimWindow.setPlaybackLimit( "in", %seqStart );
		ShapeEdAnimWindow.setPlaybackLimit( "out", %seqEnd );
	} else {
		// Hide sequence in/out bars
		ShapeEdAnimWindow-->seqInBar.setVisible( false );
		ShapeEdAnimWindow-->seqOutBar.setVisible( false );
		ShapeEdSeqFromMenu.setText( "" );
		ShapeEdSeqFromMenu.tooltip = "";
		ShapeEdSequences-->startFrame.setText( 0 );
		ShapeEdSequences-->endFrame.setText( 0 );
		ShapeEdSeqSlider.range = "0 1";
		ShapeEdSeqSlider.setValue( 0 );
		ShapeEd_ThreadSlider.range = ShapeEdSeqSlider.range;
		ShapeEd_ThreadSlider.setValue( ShapeEdSeqSlider.value );
		ShapeEdAnimWindow.setPlaybackLimit( "in", 0 );
		ShapeEdAnimWindow.setPlaybackLimit( "out", 1 );
		ShapeEdAnimWindow.setSequence( "" );
	}
}

// Update the GUI in response to the shape selection changing
function ShapeEdThreadViewer::update_onShapeSelectionChanged( %this ) {
	
	ShapeEdThread_List.clear();
	ShapeEdThread_SeqList.clear();
	ShapeEdThread_SeqList.addRow( 0, "<rootpose>" );
}



function ShapeEdSeqSlider::onMouseDragged( %this ) {
	// Pause the active thread when the slider is dragged
	if ( ShapeEdAnimWindow-->pauseBtn.getValue() == 0 )
		ShapeEdAnimWindow-->pauseBtn.performClick();

	ShapeEdAnimWindow.setKeyframe( %this.getValue() );
}

function ShapeEd_ThreadSlider::onMouseDragged( %this ) {
	if ( ShapeEdThreadViewer-->transitionTo.getText() $= "synched position" ) {
		// Pause the active thread when the slider is dragged
		if ( ShapeEdAnimWindow-->pauseBtn.getValue() == 0 )
			ShapeEdAnimWindow-->pauseBtn.performClick();

		ShapeEdAnimWindow.setKeyframe( %this.getValue() );
	}
}

//==============================================================================
// GuiShapeEdPreview - Called when the position of the active thread has changed, such as during playback
function ShapeEdShapeView::onThreadPosChanged( %this, %pos, %inTransition ) {
	// Update sliders
	%frame = ShapeEdAnimWindow.threadPosToKeyframe( %pos );
	if (isObject(ShapeEdSeqSlider))
		ShapeEdSeqSlider.setValue( %frame );

	if ( ShapeEdThreadViewer-->transitionTo.getText() $= "synched position" ) {
		ShapeEd_ThreadSlider.setValue( %frame );

		// Highlight the slider during transitions
		if ( %inTransition )
			ShapeEd_ThreadSlider.profile = GuiShapeEdTransitionSliderProfile;
		else
			ShapeEd_ThreadSlider.profile = ToolsSliderProfile;
	}
}



// Set the sequence to play
function ShapeEdAnimWindow::setSequence( %this, %seqName ) {
	devLog("ShapeEdAnimWindow::setSequence",%seqName);
	%this.usingProxySeq = false;

	if ( ShapeEdThreadViewer-->useTransitions.getValue() ) {
		%transTime = ShapeEdThreadViewer-->transitionTime.getText();

		if ( ShapeEdThreadViewer-->transitionTo.getText() $= "synched position" )
			%transPos = -1;
		else
			%transPos = %this.keyframeToThreadPos( ShapeEd_ThreadSlider.getValue() );

		%transPlay = ( ShapeEdThreadViewer-->transitionTarget.getText() $= "plays during transition" );
	} else {
		%transTime = 0;
		%transPos = 0;
		%transPlay = 0;
	}

	// No transition when sequence is not changing
	if ( %seqName $= ShapeEdShapeView.getThreadSequence() )
		%transTime = 0;

	if ( %seqName !$= "" ) {
		// To be able to effectively scrub through the animation, we need to have all
		// frames available, even if it was added with only a subset. If that is the
		// case, then create a proxy sequence that has all the frames instead.
		%sourceData = ShapeEditor.getSequenceSource( %seqName );
		%from = rtrim( getFields( %sourceData, 0, 1 ) );
		%startFrame = getField( %sourceData, 2 );
		%endFrame = getField( %sourceData, 3 );
		%frameCount = getField( %sourceData, 4 );

		if ( ( %startFrame != 0 ) || ( %endFrame != ( %frameCount-1 ) ) ) {
			%proxyName = ShapeEditor.getProxyName( %seqName );

			if ( ShapeEditor.shape.getSequenceIndex( %proxyName ) != -1 ) {
				ShapeEditor.shape.removeSequence( %proxyName );
				ShapeEdShapeView.refreshThreadSequences();
			}

			ShapeEditor.shape.addSequence( %from, %proxyName );
			// Limit the transition position to the in/out range
			%transPos = mClamp( %transPos, 0, 1 );
		}
	}

	ShapeEdShapeView.setThreadSequence( %seqName, %transTime, %transPos, %transPlay );
}

function ShapeEdAnimWindow::getTimelineBitmapPos( %this, %val, %width ) {
	%frameCount = getWord( ShapeEdSeqSlider.range, 1 );
	%pos_x = getWord( ShapeEdSeqSlider.getPosition(), 0 );
	%len_x = getWord( ShapeEdSeqSlider.getExtent(), 0 ) - %width;
	return %pos_x + ( ( %len_x * %val / %frameCount ) );
}

// Set the in or out sequence limit
function ShapeEdAnimWindow::setPlaybackLimit( %this, %limit, %val ) {
	// Determine where to place the in/out bar on the slider
	%thumbWidth = 8;    // width of the thumb bitmap
	%pos_x = %this.getTimelineBitmapPos( %val, %thumbWidth );

	if ( %limit $= "in" ) {
		%this.seqStartFrame = %val;
		%this-->seqIn.setText( %val );
		%this-->seqInBar.setPosition( %pos_x, 0 );
	} else {
		%this.seqEndFrame = %val;
		%this-->seqOut.setText( %val );
		%this-->seqOutBar.setPosition( %pos_x, 0 );
	}
}


function ShapeEdThreadViewer::onAddThread( %this ) {
	ShapeEdShapeView.addThread();
	ShapeEdThread_List.addRow( %this.threadID++, ShapeEdThread_List.rowCount() );
	ShapeEdThread_List.setSelectedRow( ShapeEdThread_List.rowCount()-1 );
}

function ShapeEdThreadViewer::onRemoveThread( %this ) {
	if ( ShapeEdThread_List.rowCount() > 1 ) {
		// Remove the selected thread
		%row = ShapeEdThread_List.getSelectedRow();
		ShapeEdShapeView.removeThread( %row );
		ShapeEdThread_List.removeRow( %row );
		// Update list (threads are always numbered 0-N)
		%rowCount = ShapeEdThread_List.rowCount();

		for ( %i = %row; %i < %rowCount; %i++ )
			ShapeEdThread_List.setRowById( ShapeEdThreadList.getRowId( %i ), %i );

		// Select the next thread
		if ( %row >= %rowCount )
			%row = %rowCount - 1;

		ShapeEdThread_List.setSelectedRow( %row );
	}
}

function ShapeEdThread_List::onSelect( %this, %row, %text ) {
	ShapeEdShapeView.activeThread = ShapeEdThread_List.getSelectedRow();
	// Select the active thread's sequence in the list
	%seqName = ShapeEdShapeView.getThreadSequence();

	if ( %seqName $= "" )
		%seqName = "<rootpose>";
	else if ( startswith( %seqName, "__proxy__" ) )
		%seqName = ShapeEditor.getUnproxyName( %seqName );

	%seqIndex = ShapeEdSequenceList.getItemIndex( %seqName );
	ShapeEdSequenceList.setSelectedRow( %seqIndex );

	// Update the playback controls
	switch ( ShapeEdShapeView.threadDirection ) {
	case -1:
		ShapeEdAnimWindow-->playBkwdBtn.performClick();

	case 0:
		ShapeEdAnimWindow-->pauseBtn.performClick();

	case 1:
		ShapeEdAnimWindow-->playFwdBtn.performClick();
	}

	SetToggleButtonValue( ShapeEdAnimWindow-->pingpong, ShapeEdShapeView.threadPingPong );
}
