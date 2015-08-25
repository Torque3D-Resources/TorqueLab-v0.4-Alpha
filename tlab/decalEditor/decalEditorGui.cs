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

function DecalEditorGui::onWake( %this ) {
}
function DecalEditorGui::onSleep( %this ) {
}


// Stores the information when the gizmo is first used
function DecalEditorGui::prepGizmoTransform( %this, %decalId, %nodeDetails ) {
	DecalEditorGui.gizmoDetails = %nodeDetails;
}

// Activated in onMouseUp while gizmo is dirty
function DecalEditorGui::completeGizmoTransform( %this, %decalId, %nodeDetails ) {
	DecalEditorGui.doEditNodeDetails( %decalId, %nodeDetails, true );
}



function DecalEditorGui::paletteSync( %this, %mode ) {
	%evalShortcut = "EWToolsPaletteArray-->" @ %mode @ ".setStateOn(1);";
	eval(%evalShortcut);
}



function DecalEditorTabBook::onTabSelected( %this, %text, %idx ) {
	if( %idx == 0)
		%showInstance = true;
	else
		%showInstance = false;
	if( %idx == 0) {
		DecalPreviewWindow.text = "Instance Properties";		
		DeleteDecalButton.tabSelected = %idx;
	} else {
		DecalPreviewWindow.text = "Template Properties";		
		DeleteDecalButton.tabSelected = %idx;
	}
	DecalEditorTools-->TemplateProperties.setVisible(!%showInstance);
	DecalEditorTools-->TemplatePreview.setVisible(!%showInstance);
	DecalEditorWindow-->libraryStack.visible = !%showInstance;
	
	DecalEditorTools-->InstanceProperties.setVisible(%showInstance);
	DecalEditorTools-->InstancePreview.setVisible(%showInstance);
	DecalEditorWindow-->instanceStack.visible = %showInstance;
		
}






