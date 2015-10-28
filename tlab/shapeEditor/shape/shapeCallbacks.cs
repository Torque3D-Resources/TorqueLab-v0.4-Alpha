//==============================================================================
// TorqueLab -> ShapeEditor -> Mounted Shapes
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================



function ShapeEd::onObjectRenamed( %this, %oldName, %newName ) {
	// --- DETAILS TAB ---
	// Rename tree entries for this object
	%count = ShapeEditor.shape.getMeshCount( %newName );

	for ( %i = 0; %i < %count; %i++ ) {
		%size = getTrailingNumber( ShapeEditor.shape.getMeshName( %newName, %i ) );
		%id = ShapeEd_DetailTree.findItemByName( %oldName SPC %size );

		if ( %id > 0 ) {
			ShapeEd_DetailTree.editItem( %id, %newName SPC %size, "" );

			// Sync text if item is selected
			if ( ShapeEd_DetailTree.isItemSelected( %id ) &&
					( ShapeEdDetails-->meshName.getText() !$= %newName ) )
				ShapeEdDetails-->meshName.setText( %newName );
		}
	}
}
