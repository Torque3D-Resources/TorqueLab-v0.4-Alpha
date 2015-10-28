//==============================================================================
// TorqueLab -> ShapeEditor -> Sequence Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// ShapeEditor -> Sequence Editing
//==============================================================================

function ShapeEdAnimWIndow::threadPosToKeyframe( %this, %pos ) {
	if ( %this.usingProxySeq ) {
		%start = getWord( ShapeEdSeqSlider.range, 0 );
		%end = getWord( ShapeEdSeqSlider.range, 1 );
	} else {
		%start = ShapeEdAnimWindow.seqStartFrame;
		%end = ShapeEdAnimWindow.seqEndFrame;
	}

	return %start + ( %end - %start ) * %pos;
}

function ShapeEdAnimWindow::keyframeToThreadPos( %this, %frame ) {
	if ( %this.usingProxySeq ) {
		%start = getWord( ShapeEdSeqSlider.range, 0 );
		%end = getWord( ShapeEdSeqSlider.range, 1 );
	} else {
		%start = ShapeEdAnimWindow.seqStartFrame;
		%end = ShapeEdAnimWindow.seqEndFrame;
	}

	return ( %frame - %start ) / ( %end - %start );
}

function ShapeEdAnimWindow::setKeyframe( %this, %frame ) {
	ShapeEdSeqSlider.setValue( %frame );

	if ( ShapeEdThreadViewer-->transitionTo.getText() $= "synched position" )
		ShapeEd_ThreadSlider.setValue( %frame );

	// Update the position of the active thread => if outside the in/out range,
	// need to switch to the proxy sequence
	if ( !%this.usingProxySeq ) {
		if ( ( %frame < %this.seqStartFrame ) || ( %frame > %this.seqEndFrame) ) {
			%this.usingProxySeq = true;
			%proxyName = ShapeEditor.getProxyName( ShapeEdShapeView.getThreadSequence() );
			ShapeEdShapeView.setThreadSequence( %proxyName, 0, 0, false );
		}
	}

	ShapeEdShapeView.threadPos = %this.keyframeToThreadPos( %frame );
}

function ShapeEdAnimWindow::setNoProxySequence( %this ) {
	// no need to use the proxy sequence during playback
	if ( %this.usingProxySeq ) {
		%this.usingProxySeq = false;
		%seqName = ShapeEditor.getUnproxyName( ShapeEdShapeView.getThreadSequence() );
		ShapeEdShapeView.setThreadSequence( %seqName, 0, 0, false );
		ShapeEdShapeView.threadPos = %this.keyframeToThreadPos( ShapeEdSeqSlider.getValue() );
	}
}

function ShapeEdAnimWindow::togglePause( %this ) {
	if ( %this-->pauseBtn.getValue() == 0 ) {
		%this.lastDirBkwd = %this-->playBkwdBtn.getValue();
		%this-->pauseBtn.performClick();
	} else {
		%this.setNoProxySequence();

		if ( %this.lastDirBkwd )
			%this-->playBkwdBtn.performClick();
		else
			%this-->playFwdBtn.performClick();
	}
}
// Set the direction of the current thread (-1: reverse, 0: paused, 1: forward)
function ShapeEdAnimWindow::setThreadDirection( %this, %dir ) {
	// Update thread direction
	ShapeEdShapeView.threadDirection = %dir;

	// Sync the controls in the thread window
	switch ( %dir ) {
	case -1:
		ShapeEdThreadViewer-->playBkwdBtn.setStateOn( 1 );

	case 0:
		ShapeEdThreadViewer-->pauseBtn.setStateOn( 1 );

	case 1:
		ShapeEdThreadViewer-->playFwdBtn.setStateOn( 1 );
	}
}
function ShapeEdAnimWindow::togglePingPong( %this ) {
	ShapeEdShapeView.threadPingPong = %this-->pingpong.getValue();

	if ( %this-->playFwdBtn.getValue() )
		%this-->playFwdBtn.performClick();
	else if ( %this-->playBkwdBtn.getValue() )
		%this-->playBkwdBtn.performClick();
}

//==============================================================================
// ShapeEditor -> Button commands
//==============================================================================
function ShapeEdThreadViewer::goToStart( %this ) {
	ShapeEdAnimWindow.setKeyframe( ShapeEdAnimWindow-->seqOut.getText() );
}

function ShapeEdThreadViewer::stepBkwd( %this ) {
	ShapeEdAnimWindow.setKeyframe( mCeil(ShapeEdSeqSlider.getValue() - 1) );
}
function ShapeEdThreadViewer::playBkwd( %this ) {
	ShapeEdAnimWindow.setNoProxySequence(); 
	ShapeEdAnimWindow.setThreadDirection( -1 );
}
function ShapeEdThreadViewer::playFwd( %this ) {
	ShapeEdAnimWindow.setNoProxySequence();
	 ShapeEdAnimWindow.setThreadDirection( 1 );
}

function ShapeEdThreadViewer::stepFwd( %this ) {
	ShapeEdAnimWindow.setKeyframe( mFloor(ShapeEdSeqSlider.getValue() + 1) );
}

function ShapeEdThreadViewer::pause( %this ) {
	ShapeEdAnimWindow.setThreadDirection( 0 );
}

function ShapeEdThreadViewer::goToEnd( %this ) {
	ShapeEdAnimWindow.setKeyframe( ShapeEdAnimWindow-->seqOut.getText() );
}




