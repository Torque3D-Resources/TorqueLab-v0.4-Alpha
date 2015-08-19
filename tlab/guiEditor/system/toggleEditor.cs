//==============================================================================
// TorqueLab -> GuiEditor Toggle Functions (open and close)
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
$InGuiEditor = false;
$MLAAFxGuiEditorTemp = false;

//==============================================================================
// Toggle, Open amd Close
//==============================================================================

//==============================================================================
// Toggle the GuiEditor( Close if open and open if closed)
function ToggleGuiEdit( %loadLast ) {
	if (Canvas.isFullscreen()) {
		LabMsgOK("Windowed Mode Required", "Please switch to windowed mode to access the GUI Editor.");
		return;
	}

	if (!$InGuiEditor) {
		//Store current Content to return to it
		%initialContent = Canvas.getContent();

		if ( isObject(GuiEditor.forceContent)) 	
			%initialContent = GuiEditor.forceContent;	

		GuiEditor.initialContent =%initialContent;

		if (($pref::Editor::AutoLoadLastGui || %loadLast)&& isObject(GuiEditor.lastContent))
			%initialContent = GuiEditor.lastContent;

		GuiEditContent(%initialContent);

		//Temp fix to disable MLAA when in GUI editor
		if( isObject(MLAAFx) && MLAAFx.isEnabled==true ) {
			MLAAFx.isEnabled = false;
			$MLAAFxGuiEditorTemp = true;
		}
		GuiEdMap.push();
	} 
	else {
		Lab.lastGuiEditSource = GuiEditor.lastContent;
		GuiEditCanvas.quit();
		GuiEdMap.pop();
	}
}
//------------------------------------------------------------------------------
//==============================================================================
function ToggleGuiEditor(  ) {
	//if( %make ) {
		GuiEditor.forceContent = "";

		if( EditorIsActive() && !GuiEditor.toggleIntoEditorGui ) {
			if (EditorGui.isAwake()) {
				GuiEditor.forceContent = EditorGui;
			}

			toggleEditor( true );
		}

		ToggleGuiEdit();
		// Cancel the scheduled event to prevent
		// the level from cycling after it's duration
		// has elapsed.
		cancel($Game::Schedule);
	//}
}
function toggleLastGuiEditor( %make ) {
	if( %make ) {
		GuiEditor.forceContent = "";

		if( EditorIsActive() && !GuiEditor.toggleIntoEditorGui ) {
			if (EditorGui.isAwake()) {
				GuiEditor.forceContent = EditorGui;
			}

			toggleEditor( true );
		}

		ToggleGuiEdit(true);
		// Cancel the scheduled event to prevent
		// the level from cycling after it's duration
		// has elapsed.
		cancel($Game::Schedule);
	}
}
GlobalActionMap.bindCmd( keyboard, "f10", "toggleGuiEditor();","" );
GlobalActionMap.bind( keyboard, "ctrl f10", toggleLastGuiEditor );
//------------------------------------------------------------------------------


function GuiEditCanvas::close( %this ) {
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::quit( %this ) {
	%this.close();
	GuiGroup.add(GuiEditorGui);
	// we must not delete a window while in its event handler, or we foul the event dispatch mechanism
	%this.schedule(10, delete);
	
	$InGuiEditor = false;
	Canvas.setContent(GuiEditor.initialContent);

	// Canvas.setContent(GuiEditor.lastContent);

	//Temp fix to disable MLAA when in GUI editor
	if( isObject(MLAAFx) && $MLAAFxGuiEditorTemp==true ) {
		MLAAFx.isEnabled = true;
		$MLAAFxGuiEditorTemp = false;
	}
}
