//==============================================================================
// TorqueLab -> Editor Gui General Scripts
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// Set object selected in scene (Trees and WorldEditor)
//==============================================================================

function Scene::selectObjectGroup(%this, %simGroup) {
	%isLast = false;
		foreach(%child in %simGroup){
			if (%simGroup.getObjectIndex(%child) >= %simGroup.getCount()-1)
				%isLast = true;
			
			Scene.SelectObject(%child,%isLast);
		}	
		
}
