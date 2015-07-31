//==============================================================================
// Castle Blasters ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// Lab ColorPicker Common updates and Picked function
//==============================================================================
//==============================================================================
// Called when a color is confirmed and colorPicker is closing
function GuiColorPickerCtrl::doCommonUpdate(%this,%color,%isPicked) {	
	
	
		
	//Check if the Picker have an assigned slider for alpha
	if (isObject(%this.alphaSlider)) {		
		%this.alphaSlider.setValue(getWord(%color,3));
	}
	
	
		
	//Check if the Picker have a assigned ColorEditCtrl
	if (isObject(%this.colorEditCtrl)) {
		%editColor = %color;
		if (%this.noAlpha)
			%editColor = removeWord(%editColor,3);
		%this.colorEditCtrl.setText(%editColor);
		//%command = %this.colorEditCtrl.command;
		//strreplace(%command,"$ThisControl",%this.colorEditCtrl.getId());
		//devLog("About to eval:",%command);
		//eval(%command);
	}
	
	if (%isPicked){
		if (isObject(%this.colorEditCtrl)) {		
			%command = %this.colorEditCtrl.command;
			%command = strreplace(%command,"$ThisControl",%this.colorEditCtrl.getId());
			devLog("About to eval:",%command);
			eval(%command);
			
		}
		%srcObj = %this.sourceObject;
		%srcField = %this.sourceField;
		
		//Check if a SourceObject exist and set Color.
		if (isObject(%srcObj))
			%srcObj.setFieldValue(%srcField,%color);		
	}
	//if (%this.updateCommand !$= "")
		//eval(%this.updateCommand);
}
//------------------------------------------------------------------------------

//==============================================================================
// Lab ColorPicker Callbacks (For Integer Colors: 255 255 255 255)
//==============================================================================
//==============================================================================
// Called when a color is confirmed and colorPicker is closing
function GuiColorPickerCtrl::ColorPickedI(%this,%color) {	
	if (%this.noAlpha)
		%color.a = 255;
		
	//Convert the Int Color to float for store as Base Color
	%baseColor = ColorIntToFloat(%color);		
	%this.baseColor = %baseColor;
	
	%alpha = mCeil(getWord(%color,3));
	%color = setWord(%color,3,%alpha);
	
	
	
	%this.doCommonUpdate(%color,true);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// Called when a color is piicked inside the colorPicker
function GuiColorPickerCtrl::ColorUpdatedI(%this,%color) {
	if (%this.noAlpha)
		%color.a = 255;
	//Convert the Int Color to float for store as Base Color
	%baseColor = ColorIntToFloat(%color);		
	%this.baseColor = %baseColor;	
	%this.updateColor();
		
	%this.doCommonUpdate(%color,false);
	}
//------------------------------------------------------------------------------

//==============================================================================
// Float Color Type Callbacks
//==============================================================================
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorPicked(%this,%color) {
	if (%this.noAlpha)
		%color.a = 1;	
		
	%baseColor = %color;
	if (%this.isIntColor)
		%baseColor = ColorIntToFloat(%color);
		
	%this.baseColor = %baseColor;
	if (%this.floatLength > 0 && !%this.isIntColor)
		%color = ColorFloatLength(%color,%this.floatLength);		
		
	%this.doCommonUpdate(%color,true);	
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorUpdated(%this,%color) {

	if (%this.noAlpha)
		%color.a = 1;		
	
	%baseColor = %color;
	if (%this.isIntColor)
		%baseColor = ColorIntToFloat(%color);
	
	%this.baseColor = %baseColor;		
	%this.updateColor();
	
	if (%this.floatLength > 0 && !%this.isIntColor)
		%color = ColorFloatLength(%color,%this.floatLength);		
	
	%this.doCommonUpdate(%color,false);	
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::AlphaChanged(%this,%sourceCtrl) {	
	%alpha = %sourceCtrl.getValue();	
	%this.baseColor.a = %alpha;
	%color = %this.baseColor;
	%this.updateColor();

	devLog("AlphaChanged color = ",%this.baseColor,"IsIntCOlor",%this.isIntColor);	
	if (%this.updateCommand !$= "")
		eval(%this.updateCommand);
	
}
//------------------------------------------------------------------------------
//==============================================================================
// GuiColorEditCtrl is a TextEdit linked to Picker and update it when changed
function GuiColorEditCtrl::onValidate(%this) {	
	devLog("GuiColorEditCtrl::onValidate");
	
	
	%max = 1;
	%min = 0;
	if (%this.isIntColor)
		%max = 255;
		
	%color = "0 0 0 0";
	%value = %this.getValue();
	%pickerValue = %this.colorPickerCtrl.baseColor;
	%curWord = 0;
	//Clamp all words to range (Float:0 to 1 -- Int:0 to 255)
	foreach$(%word in %value){
		if (!strIsNumeric(%word))
			%word = getWord(%pickerValue,%curWord);
					
		%word = mClamp(%word,%min,%max);
		
		%color = setWord(%color,%curWord,%word);
		%curWord++;
		if (%curWord > 3)
			break;
	}
	
	for(%i = %curWord;%i<=3;%i++){
		%word = getWord(%pickerValue,%i);
		%color = setWord(%color,%i,%word);
	}		
	
	//Called the common colorPicker update function
	%this.colorPickerCtrl.doCommonUpdate(%color,true);
	
	//If Int COlor, Convert to Float for baseColor
	if (%this.isIntColor)
		%color = ColorIntToFloat(%color);
		
	%this.colorPickerCtrl.baseColor = %color;
	%this.colorPickerCtrl.pickColor = %color;		
	
	%this.colorPickerCtrl.updateColor();
}
//------------------------------------------------------------------------------

//==============================================================================
// Old System (Might be use, delete when confirmed not used)
//==============================================================================
function GuiColorPickerCtrl::pickColorF( %this, %updateFunc,%arg1,%arg2,%arg3) {
  devLog("SHOULD BE DELETED!!!!!! GuiColorPickerCtrl::pickColorF"); 
   %ctrl.updateCommand = %updateFunc@"(%this.internalName,%color,\""@%arg1@"\",\""@%arg2@"\",\""@%arg3@"\");";
      
   %currentColor =   %this.baseColor;
   %callBack = %this@".ColorPicked";
   %updateCallback = %this@".ColorUpdated";       
      devLog("GuiColorPickerCtrl::pickColorF",%this,"Color",%currentColor,"Func",%updateFunc); 
   GetColorF( %currentColor, %callback, %this.getRoot(), %updateCallback, %cancelCallback );     
   
}
function GuiColorPickerCtrl::pickColorI( %this, %updateFunc,%arg1,%arg2,%arg3) {
    devLog("SHOULD BE DELETED!!!!!! GuiColorPickerCtrl::pickColorI"); 
   %ctrl.updateCommand = %updateFunc@"(%this.internalName,%color,\""@%arg1@"\",\""@%arg2@"\",\""@%arg3@"\");";
      
   %currentColor =   ColorFloatToInt(%this.baseColor);
   %callBack = %this@".ColorPicked";
   %updateCallback = %this@".ColorUpdated";       
       devLog("GuiColorPickerCtrl::pickColorI",%this,"Color",%currentColor,"Func",%updateFunc);

   
}