//==============================================================================
// Castle Blasters ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColor( %pData ) {
	%pData.pill-->field.text = %pData.Title;
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	%checkbox.variable = %pData.Variable;
	%noAlpha = %pData.Option[%pData.Setting,"noalpha"];
	%colorPicker.lockedAlpha = %noAlpha;

	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%colorPicker.isIntColor = true;
		
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColorInt( %pData ) {
	%pData.pill-->field.text = %pData.Title;
	%pData.pill-->field.internalName = "";
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	%checkbox.variable = %pData.Variable;
	%noAlpha = %pData.Option[%pData.Setting,"noalpha"];
	%colorPicker.lockedAlpha = %noAlpha;
	%colorPicker.isIntColor = true;
	
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColorAlpha( %pData ) {
	%pData.pill-->field.text = %pData.Title;
	%pData.pill-->field.internalName = "NoUpdate";
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	%colorPicker.variable = %pData.Variable;
	%colorPicker.alpha = "alphaSlider";
	%alphaSlider = %pData.pill-->slider;
	%alphaSlider.command = "updateParamColorAlpha($Me);";
	%alphaSlider.altCommand = "updateParamColorAlpha($Me);";
	%alphaSlider.internalName = "alphaSlider";
	%alphaSlider.dataType = %setting;
	%alphaSlider.range = "0 1";
	%alphaSlider.noFriends = true;
	%alphaSlider.variable = %pData.Variable;
	
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColorSlider( %pData ) {
	%isIntColor = false;
	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%isIntColor = true;
	%pData.pill-->field.text = %pData.Title;
	%alphaSlider = %pData.pill-->slider;
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	%colorPicker.alphaSlider = %alphaSlider;
	%checkbox.variable = %pData.Variable;
	%noAlpha = %pData.Option[%pData.Setting,"noalpha"];
	%colorPicker.lockedAlpha = %noAlpha;

	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%colorPicker.isIntColor = true;

	%alphaSlider.colorPicker = %colorPicker;
	%alphaSlider.fieldSource = %pData.Setting;
	%alphaSlider.command = %colorPicker@".AlphaChanged($ThisControl);";
	
	if (%isIntColor)
		%alphaSlider.range = "0 255";
	else
		%alphaSlider.range = "0 1";
		
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColorSliderEdit( %pData ) {
	%isIntColor = false;
	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%isIntColor = true;
	%pData.pill-->field.text = %pData.Title;
	%alphaSlider = %pData.pill-->slider;
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	%colorPicker.alphaSlider = %alphaSlider;
	%checkbox.variable = %pData.Variable;
	%noAlpha = %pData.Option[%pData.Setting,"noalpha"];
	%colorPicker.lockedAlpha = %noAlpha;
	
	

	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%colorPicker.isIntColor = true;

	%alphaSlider.colorPicker = %colorPicker;
	%alphaSlider.fieldSource = %pData.Setting;
	%alphaSlider.command = %colorPicker@".AlphaChanged($ThisControl);";
	
	if (%isIntColor)
		%alphaSlider.range = "0 255";
	else
		%alphaSlider.range = "0 1";
	
	%textEdit = %pData.pill-->TextEdit;
	%textEdit.command = %pData.Command;
	%textEdit.altCommand = %pData.AltCommand;
	%textEdit.internalName = %pData.Setting@"_ColorEdit";
	%textEdit.colorPickerCtrl = %colorPicker;
	%textEdit.superClass = "ParamColorEdit";
	%textEdit.isIntColor = %isIntColor;
	%textEdit.alphaSlider = %alphaSlider;
	
	%colorPicker.colorEditCtrl = %textEdit;
	%colorPicker.syncCtrls = %textEdit;
	
	%floatLength = %pData.Option[%pData.Setting,"flen"];
	if (%floatLength > 0){
		%colorPicker.floatLength = %floatLength;
		%textEdit.floatLength = %floatLength;
	}
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// RPE_DatablockEditor.buildInterface();
function buildParamColorEdit( %pData ) {
	%isIntColor = false;
	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%isIntColor = true;
	%pData.pill-->field.text = %pData.Title;
	
	//ColorPicker ctrl update
	%colorPicker = %pData.pill-->colorPicker;
	%colorPicker.command = %pData.Command;
	%colorPicker.altCommand = %pData.AltCommand;
	%colorPicker.internalName = %pData.InternalName;
	
	%checkbox.variable = %pData.Variable;
	%noAlpha = %pData.Option[%pData.Setting,"noalpha"];
	%colorPicker.lockedAlpha = %noAlpha;
	%colorPicker.noAlpha = %noAlpha;
	

	if(%pData.Option[%pData.Setting,"mode"] $= "int")
		%colorPicker.isIntColor = true;

	
	%textEdit = %pData.pill-->TextEdit;
	%textEdit.command = %pData.Command;
	%textEdit.altCommand = %pData.AltCommand;
	%textEdit.internalName = %pData.Setting@"_ColorEdit";
	%textEdit.colorPickerCtrl = %colorPicker;
	%textEdit.superClass = "ParamColorEdit";
	%textEdit.isIntColor = %isIntColor;
	
	
	%colorPicker.colorEditCtrl = %textEdit;
	%colorPicker.syncCtrls = %textEdit;
	
	%floatLength = %pData.Option[%pData.Setting,"flen"];
	if (%floatLength > 0){
		%colorPicker.floatLength = %floatLength;
		%textEdit.floatLength = %floatLength;
	}
	return %colorPicker;
}
//------------------------------------------------------------------------------
//==============================================================================
// Integer Color Type Callbacks
//==============================================================================
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorPickedI(%this,%color) {
	devLog("GuiColorPickerCtrl::ColorPickedI(%this,%color)",%this,%color);
	%srcObj = %this.sourceObject;
	%srcField = %this.sourceField;
	%alpha = mCeil(getWord(%color,3));
	%color = setWord(%color,3,%alpha);	

	%baseColor = ColorIntToFloat(%color);
	devLog("GuiColorPickerCtrl::ColorPicked(Converted)",%baseColor);
	%this.baseColor = %baseColor;
	if (isObject(%srcObj))
		%srcObj.setFieldValue(%srcField,%color);


		
	
	if (isObject(%this.alphaSlider)) {
		devLog("Color Picked, updating Slider to:",%alpha);
		%this.alphaSlider.setValue(%alpha);
	}
	
	if (isObject(%this.colorEditCtrl)) {
		devLog("Color Picked, updating TextEdit to:",%baseColor);
		%this.colorEditCtrl.setValue(%baseColor);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorUpdatedI(%this,%color) {
	devLog("GuiColorPickerCtrl::ColorUpdatedI(%this,%color)",%this,%color);
	%alpha = mCeil(getWord(%color,3));
	
	
	
	%baseColor = ColorIntToFloat(%color);
	

	devLog("GuiColorPickerCtrl::ColorUpdated(Converted)",%baseColor);
	// %this.sourceArray.setValue(%this.internalName,%color);
		%this.baseColor = %baseColor;	
	%this.updateColor();

	if (isObject(%this.alphaSlider)) {
		devLog("Color Updated, updating Slider to:",%alpha);
		%this.alphaSlider.setValue(%alpha);
	}
	if (isObject(%this.colorEditCtrl)) {
		devLog("Color Updated, updating TextEdit to:",%baseColor);
		%this.colorEditCtrl.setValue(%baseColor);
	}
		devLog("UpdateFunct:",%this.updateCommand);
	%ctrl = %this;
	eval(%ctrl.updateCommand);
}
//------------------------------------------------------------------------------

//==============================================================================
// Float Color Type Callbacks
//==============================================================================
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorPicked(%this,%color) {
	devLog("GuiColorPickerCtrl::ColorPicked(%this,%color)",%this,%color);
	%srcObj = %this.sourceObject;
	%srcField = %this.sourceField;
	%alpha = mCeil(getWord(%color,3));
	%alpha = getWord(%color,3);
	%color = setWord(%color,3,%alpha);
	%baseColor = %color;
	
	if (%this.noAlpha)
		%baseColor.a = 1;
	//if (%this.isIntColor)
	//	%baseColor = ColorIntToFloat(%color);
	
	devLog("GuiColorPickerCtrl::ColorPicked(Converted)",%baseColor);
	%this.baseColor = %baseColor;
	if (isObject(%srcObj))
		%srcObj.setFieldValue(%srcField,%color);

	if (%this.floatLength > 0){
		devLog("Starting float length",%this.floatLength,"Start color",%baseColor);
		%baseColor.r = mFloatLength(%baseColor.r,%this.floatLength);
		%baseColor.g = mFloatLength(%baseColor.g,%this.floatLength);
		%baseColor.b = mFloatLength(%baseColor.b,%this.floatLength);
		%baseColor.a = mFloatLength(%baseColor.a,%this.floatLength);
		devLog("Ending float length with color",%baseColor);
	}
			
	if (isObject(%this.alphaSlider)) {
		devLog("Color Picked, updating Slider to:",%alpha);
		%this.alphaSlider.setValue(%alpha);
	}
	if (isObject(%this.colorEditCtrl)) {		
		if (%this.noAlpha)
			%baseColor = removeWord(%baseColor,3);
		devLog("Color Float Picked, updating TextEdit to:",%baseColor);
		%this.colorEditCtrl.setValue(%baseColor);
	}
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::ColorUpdated(%this,%color) {
	devLog("GuiColorPickerCtrl::ColorUpdated(%this,%color)",%this,%color);
	%alpha = mCeil(getWord(%color,3));
	
	%alpha = getWord(%color,3);
	%baseColor = %color;
	if (%this.isIntColor)
		%baseColor = ColorIntToFloat(%color);
	
	%this.baseColor = %baseColor;	
	
	devLog("GuiColorPickerCtrl::ColorUpdated(Converted)",%baseColor);
	// %this.sourceArray.setValue(%this.internalName,%color);
	%this.updateColor();
	
	if (%this.floatLength > 0){
		devLog("Starting float length",%this.floatLength,"Start color",%baseColor);
		%baseColor.r = mFloatLength(%baseColor.r,%this.floatLength);
		%baseColor.g = mFloatLength(%baseColor.g,%this.floatLength);
		%baseColor.b = mFloatLength(%baseColor.b,%this.floatLength);
		%baseColor.a = mFloatLength(%baseColor.a,%this.floatLength);
		devLog("Ending float length with color",%baseColor);
	}
	
	if (isObject(%this.alphaSlider)) {
		devLog("Color Updated, updating Slider to:",%alpha);
		%this.alphaSlider.setValue(%baseColor.a);
	}
	
	if (isObject(%this.colorEditCtrl)) {
		if (%this.noAlpha)
			%baseColor = removeWord(%baseColor,3);
		devLog("Color Float Updated, updating TextEdit to:",%baseColor);
		%this.colorEditCtrl.setValue(%baseColor);
	}
		devLog("UpdateFunct:",%this.updateCommand);
	%ctrl = %this;
	eval(%ctrl.updateCommand);
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function GuiColorPickerCtrl::AlphaChanged(%this,%sourceCtrl) {
	devLog("GuiColorPickerCtrl::AlphaChanged(%this,%sourceCtrl)",%this,%sourceCtrl,"Value:",%sourceCtrl.getValue());
	%alpha = %sourceCtrl.getValue();
	
	devLog("Start color = ",%this.baseColor);
	%this.baseColor.a = %alpha;
	%color = %this.baseColor;
	%this.updateColor();
	devLog("UpdateFunct:",%this.updateCommand);
	devLog("End color = ",%this.baseColor);	
	%ctrl = %this;
	eval(%ctrl.updateCommand);
}
//------------------------------------------------------------------------------
//==============================================================================
// Empty Editor Gui
function ParamColorEdit::onValidate(%this) {
	devLog("ParamColorEdit::onValidate(%this)",%this);
	
	%max = 1;
	%min = 0;
	if (%this.isIntColor)
		%max = 255;
		
	%finalValue = "0 0 0 0";
	%value = %this.getValue();
	%pickerValue = %this.colorPickerCtrl.baseColor;
	%curWord = 0;
	foreach$(%word in %value){
		if (!strIsNumeric(%word))
			%word = getWord(%pickerValue,%curWord);
		
		
		%word = mClamp(%word,%min,%max);
		
		%finalValue = setWord(%finalValue,%curWord,%word);
		%curWord++;
		if (%curWord > 3)
			break;
	}
	
	for(%i = %curWord;%i<=3;%i++){
		%word = getWord(%pickerValue,%i);
		%finalValue = setWord(%finalValue,%i,%word);
	}
	
	devLog("Start Value = ",%value);	
	devLog("Picker Value = ",%pickerValue);		
	devLog("Final Value = ",%finalValue);	
	
	if (isObject(%this.alphaSlider))
		%this.alphaSlider.setValue(%finalValue.a);
		
	%intValue = ColorFloatToInt(%finalValue);
	
	devLog("Int Value = ",%intValue);	
	
	if (%this.isIntColor)
		%finalValue = %intValue;

	%this.colorPickerCtrl.baseColor = %finalValue;
	%this.colorPickerCtrl.pickColor = %finalValue;
	
	
	%this.colorPickerCtrl.updateColor();
	%this.command = strreplace(%this.command,"$ThisControl",%this.getId());
	
	eval(%this.command);
	devLog("BaseColor = ",%this.colorPickerCtrl.baseColor);	
	devLog("PickColor = ",%this.colorPickerCtrl.pickColor);	
}
//------------------------------------------------------------------------------
