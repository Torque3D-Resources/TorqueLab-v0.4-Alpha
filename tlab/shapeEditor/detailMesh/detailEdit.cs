//==============================================================================
// TorqueLab -> ShapeEditor -> Detail/Mesh Editing
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================

//==============================================================================
// ShapeEditor -> Detail/Mesh Editing
//==============================================================================



function ShapeEdDetails::onWake( %this ) {
	// Initialise popup menus
	%this-->bbType.clear();
	%this-->bbType.add( "None", 0 );
	%this-->bbType.add( "Billboard", 1 );
	%this-->bbType.add( "Z Billboard", 2 );
	%this-->addGeomTo.clear();
	%this-->addGeomTo.add( "current detail", 0 );
	%this-->addGeomTo.add( "new detail", 1 );
	%this-->addGeomTo.setSelected( 0, false );
	ShapeEd_DetailTree.onDefineIcons();
}






function ShapeEditor::autoAddDetails( %this, %dest ) {
	// Sets of LOD files are named like:
	//
	// MyShape_LOD200.dae
	// MyShape_LOD64.dae
	// MyShape_LOD2.dae
	//
	// Determine the base name of the input file (MyShape_LOD in the example above)
	// and use that to find any other shapes in the set.
	%base = fileBase( %dest.baseShape );
	%pos = strstr( %base, "_LOD" );

	if ( %pos < 0 ) {
		echo( "Not an LOD shape file" );
		return;
	}

	%base = getSubStr( %base, 0, %pos + 4 );
	echo( "Base is: " @ %base );
	%filePatterns = filePath( %dest.baseShape ) @ "/" @ %base @ "*" @ fileExt( %dest.baseShape );
	echo( "Pattern is: " @ %filePatterns );
	%fullPath = findFirstFileMultiExpr( %filePatterns );

	while ( %fullPath !$= "" ) {
		%fullPath = makeRelativePath( %fullPath, getMainDotCSDir() );

		if ( %fullPath !$= %dest.baseShape ) {
			echo( "Found LOD shape file: " @ %fullPath );
			// Determine the detail size ( number after the base name ), then add the
			// new mesh
			%size = strreplace( fileBase( %fullPath ), %base, "" );
			ShapeEditor.addLODFromFile( %dest, %fullPath, %size, 0 );
		}

		%fullPath = findNextFileMultiExpr( %filePatterns );
	}

	if ( %this.shape == %dest ) {
		ShapeEdShapeView.refreshShape();
		ShapeEd.onDetailsChanged();
	}
}

function ShapeEditor::addLODFromFile( %this, %dest, %filename, %size, %allowUnmatched ) {
	// Get (or create) a TSShapeConstructor object for the source shape. Need to
	// exec the script manually as the resource may not have been loaded yet
	%csPath = filePath( %filename ) @ "/" @ fileBase( %filename ) @ ".cs";

	if ( isFile( %csPath ) )
		exec( %csPath );

	%source = ShapeEditor.findConstructor( %filename );

	if ( %source == -1 )
		%source = ShapeEditor.createConstructor( %filename );

	%source.lodType = "SingleSize";
	%source.singleDetailSize = %size;
	// Create a temporary TSStatic to ensure the resource is loaded
	%temp = new TSStatic() {
		shapeName = %filename;
		collisionType = "None";
	};
	%meshList = "";

	if ( isObject( %temp ) ) {
		// Add a new mesh for each object in the source shape
		%objCount = %source.getObjectCount();

		for ( %i = 0; %i < %objCount; %i++ ) {
			%objName = %source.getObjectName( %i );
			echo( "Checking for object " @ %objName );

			if ( %allowUnmatched || ( %dest.getObjectIndex( %objName ) != -1 ) ) {
				// Add the source object's highest LOD mesh to the destination shape
				echo( "Adding detail size" SPC %size SPC "for object" SPC %objName );
				%srcName = %source.getMeshName( %objName, 0 );
				%destName = %objName SPC %size;
				%dest.addMesh( %destName, %filename, %srcName );
				%meshList = %meshList TAB %destName;
			}
		}

		%temp.delete();
	}

	return trim( %meshList );
}