//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------


//=============================================================================================
//    Event Handlers.
//=============================================================================================

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::onAdd( %this ) {
	// %this.setWindowTitle("Torque Gui Editor");
	%this.onCreateMenu();
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::onRemove( %this ) {
	if( isObject( GuiEditorGui.menuGroup ) )
		GuiEditorGui.delete();

	// cleanup
	%this.onDestroyMenu();
}


//---------------------------------------------------------------------------------------------
/// Called before onSleep when the canvas content is changed
function GuiEditor::move(%this,%x,%y) {
	// GuiEditor.moveSelection(%x SPC %y);
	GuiEditor.moveSelection(%x ,%y);
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::onWindowClose(%this) {
	%this.quit();
}

//=============================================================================================
//    Menu Commands.
//=============================================================================================

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::create( %this ) {
	GuiEditorNewGuiDialog.init( "NewGui", "GuiControl" );
	Canvas.pushDialog( GuiEditorNewGuiDialog );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::load( %this, %filename ) {
	%newRedefineBehavior = "replaceExisting";

	if( isDefined( "$GuiEditor::loadRedefineBehavior" ) ) {
		// This trick allows to choose different redefineBehaviors when loading
		// GUIs.  This is useful, for example, when loading GUIs that would lead to
		// problems when loading with their correct names because script behavior
		// would immediately attach.
		//
		// This allows to also edit the GUI editor's own GUI inside itself.
		%newRedefineBehavior = $GuiEditor::loadRedefineBehavior;
	}

	// Allow stomping objects while exec'ing the GUI file as we want to
	// pull the file's objects even if we have another version of the GUI
	// already loaded.
	%oldRedefineBehavior = $Con::redefineBehavior;
	$Con::redefineBehavior = %newRedefineBehavior;
	// Load up the gui.
	exec( %fileName );
	$Con::redefineBehavior = %oldRedefineBehavior;

	// The GUI file should have contained a GUIControl which should now be in the instant
	// group. And, it should be the only thing in the group.
	if( !isObject( %guiContent ) ) {
		LabMsgOk( getEngineName(),
						 "You have loaded a Gui file that was created before this version.  It has been loaded but you must open it manually from the content list dropdown");
		return 0;
	}

	GuiEditor.openForEditing( %guiContent );
	GuiEditorStatusBar.print( "Loaded '" @ %filename @ "'" );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::openInTorsion( %this ) {
	if( !GuiEditorContent.getCount() )
		return;

	%guiObject = GuiEditorContent.getObject( 0 );
	EditorOpenDeclarationInTorsion( %guiObject );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::open( %this ) {
	devLog("Open!!");
	%openFileName = GuiBuilder::getOpenName();

	if( %openFileName $= "" )
		return;

	// Make sure the file is valid.
	if ((!isFile(%openFileName)) && (!isFile(%openFileName @ ".dso")))
		return;

	%this.load( %openFileName );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::save( %this, %selectedOnly, %noPrompt ) {
	// Get the control we should save.
	if( isObject(%selectedOnly) ) {
		%currentObject = %selectedOnly;
	}
	else if( %selectedOnly ) {
		%selected = GuiEditor.getSelection();

		if( !%selected.getCount() )
			return;
		else if( %selected.getCount() > 1 ) {
			LabMsgOk( "Invalid selection", "Only a single control hierarchy can be saved to a file.  Make sure you have selected only one control in the tree view." );
			return;
		}

		%currentObject = %selected.getObject( 0 );
	} else if( GuiEditorContent.getCount() > 0 )
		%currentObject = GuiEditorContent.getObject( 0 );
	else
		return;

	// Store the current guide set on the control.
	if (%currentObject.preSaveFunction !$= "")
		eval(%currentObject.preSaveFunction);

	GuiEditor.writeGuides( %currentObject );
	%currentObject.canSaveDynamicFields = true; // Make sure the guides get saved out.

	// Construct a base filename.

	if( %currentObject.getName() !$= "" )
		%name =  %currentObject.getName() @ ".gui";
	else
		%name = "Untitled.gui";

	// Construct a path.

	if( %selectedOnly
			&& %currentObject != GuiEditorContent.getObject( 0 )
			&& %currentObject.getFileName() $= GuiEditorContent.getObject( 0 ).getFileName() ) {
		// Selected child control that hasn't been yet saved to its own file.
		%currentFile = GuiEditor.LastPath @ "/" @ %name;
		%currentFile = makeRelativePath( %currentFile, getMainDotCsDir() );
	} else {
		%currentFile = %currentObject.getFileName();

		if( %currentFile $= "") {
			// No file name set on control.  Force a prompt.
			%noPrompt = false;

			if( GuiEditor.LastPath !$= "" ) {
				%currentFile = GuiEditor.LastPath @ "/" @ %name;
				%currentFile = makeRelativePath( %currentFile, getMainDotCsDir() );
			} else
				%currentFile = expandFileName( %name );
		} else
			%currentFile = expandFileName( %currentFile );
	}

	// Get the filename.

	if( !%noPrompt ) {
		%filename = GuiBuilder::getSaveName( %currentFile );

		if( %filename $= "" )
			return;
	} else
		%filename = %currentFile;

	// Save the Gui.
	if (%currentObject.getName() $= "EditorGui") {
		info("Calling the specific EditorGui save function");
		Lab.saveEditorGui(%filename);
		return;
	}

	if( isWriteableFileName( %filename ) ) {
		if (%currentObject.isMethod("onPreEditorSave"))
			%currentObject.onPreEditorSave();
		//
		// Extract any existent TorqueScript before writing out to disk
		//
		%fileObject = new FileObject();
		%fileObject.openForRead( %filename );
		%skipLines = true;
		%beforeObject = true;
		// %var++ does not post-increment %var, in torquescript, it pre-increments it,
		// because ++%var is illegal.
		%lines = -1;
		%beforeLines = -1;
		%skipLines = false;

		while( !%fileObject.isEOF() ) {
			%line = %fileObject.readLine();

			if( %line $= "//--- OBJECT WRITE BEGIN ---" )
				%skipLines = true;
			else if( %line $= "//--- OBJECT WRITE END ---" ) {
				%skipLines = false;
				%beforeObject = false;
			} else if( %skipLines == false ) {
				if(%beforeObject)
					%beforeNewFileLines[ %beforeLines++ ] = %line;
				else
					%newFileLines[ %lines++ ] = %line;
			}
		}

		%fileObject.close();
		%fileObject.delete();
		%fo = new FileObject();
		%fo.openForWrite(%filename);

		// Write out the captured TorqueScript that was before the object before the object
		for( %i = 0; %i <= %beforeLines; %i++)
			%fo.writeLine( %beforeNewFileLines[ %i ] );

		%fo.writeLine("//--- OBJECT WRITE BEGIN ---");
		%fo.writeObject(%currentObject, "%guiContent = ");
		%fo.writeLine("//--- OBJECT WRITE END ---");

		// Write out captured TorqueScript below Gui object
		for( %i = 0; %i <= %lines; %i++ )
			%fo.writeLine( %newFileLines[ %i ] );

		%fo.close();
		%fo.delete();
		%currentObject.setFileName( makeRelativePath( %filename, getMainDotCsDir() ) );
		GuiEditorStatusBar.print( "Saved file '" @ %currentObject.getFileName() @ "'" );
		if (%currentObject.isMethod("onPostEditorSave"))
			%currentObject.onPostEditorSave();
	} else
		LabMsgOk( "Error writing to file", "There was an error writing to file '" @ %currentFile @ "'. The file may be read-only." );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::append( %this ) {
	// Get filename.
	%openFileName = GuiBuilder::getOpenName();

	if( %openFileName $= ""
								|| ( !isFile( %openFileName )
									  && !isFile( %openFileName @ ".dso" ) ) )
		return;

	// Exec file.
	%oldRedefineBehavior = $Con::redefineBehavior;
	$Con::redefineBehavior = "renameNew";
	exec( %openFileName );
	$Con::redefineBehavior = %oldRedefineBehavior;

	// Find guiContent.

	if( !isObject( %guiContent ) ) {
		LabMsgOk( "Error loading GUI file", "The GUI content controls could not be found.  This function can only be used with files saved by the GUI editor." );
		return;
	}

	if( !GuiEditorContent.getCount() )
		GuiEditor.openForEditing( %guiContent );
	else {
		GuiEditor.getCurrentAddSet().add( %guiContent );
		GuiEditor.readGuides( %guiContent );
		GuiEditor.onAddNewCtrl( %guiContent );
		GuiEditor.onHierarchyChanged();
	}

	GuiEditorStatusBar.print( "Appended controls from '" @ %openFileName @ "'" );
}

//---------------------------------------------------------------------------------------------

function GuiEditCanvas::revert( %this ) {
	if( !GuiEditorContent.getCount() )
		return;

	%gui = GuiEditorContent.getObject( 0 );
	%filename = %gui.getFileName();

	if( %filename $= "" )
		return;

	if( LabMsgOkCancel( "Revert Gui", "Really revert the current Gui?  This cannot be undone.", "OkCancel", "Question" ) == $MROk )
		%this.load( %filename );
}

//---------------------------------------------------------------------------------------------
