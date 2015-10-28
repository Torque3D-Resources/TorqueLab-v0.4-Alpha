//==============================================================================
// TorqueLab ->
// Copyright (c) 2015 All Right Reserved, http://nordiklab.com/
//------------------------------------------------------------------------------
//==============================================================================
//==============================================================================
// INIT
//==============================================================================

function BTEdit()
{
   if (!BTEditor.isAwake())
   {
      if(!isObject(BTEditCanvas))
         new GuiControl(BTEditCanvas, EditorGuiGroup);
      
      BTEditor.loadDialog();
      
      $InBehaviorTreeEditor = true;
      BehaviorTreeManager.onBehaviorTreeEditor(true);
   }
   else
   {
   	popDlg(BTEditor);
   	$InBehaviorTreeEditor = false;
   	 BehaviorTreeManager.onBehaviorTreeEditor(false);
      //BTEditCanvas.quit();
   }
}

function BTEditor::loadDialog(%this)
{
	pushDlg(BTEditor);
  // %this.lastContent=%content;
  // Canvas.setContent( BTEditor );
   
   if(!isObject(BehaviorTreeManager))
      // This isn't pretty, but we need to load up existing trees
      exec("lab/modules/ai/Behavior/behaviorTreeManager.cs");
   
   if(BehaviorTreeGroup.getCount() == 0)
   {
      %this.createTree();
   }
   else
   {
      BTEditorContentList.refresh();
      if(BTEditorTabBook.getCount() == 0)
         BTEditorContentList.setFirstSelected();
   }
   
   %this.updateUndoMenu();
   %this.updateNodeTypes();
}

function BTEditFull()
{
   if (!$InBehaviorTreeEditor)
   {
      if(!isObject(BTEditCanvas))
         new GuiControl(BTEditCanvas, EditorGuiGroup);
      
      BTEditor.startUp(Canvas.getContent());
      
      $InBehaviorTreeEditor = true;
      BehaviorTreeManager.onBehaviorTreeEditor(true);
   }
   else
   {
      BTEditCanvas.quit();
   }
}

function toggleBehaviorTreeEditor( %make )
{
   if( %make )
   {
      BTEdit();
      cancel($Game::Schedule);
   }
}

GlobalActionMap.bind( keyboard, "f9", toggleBehaviorTreeEditor );


function BTEditor::startUp(%this, %content)
{
   %this.lastContent=%content;
   Canvas.setContent( BTEditor );
   
   if(!isObject(BehaviorTreeManager))
      // This isn't pretty, but we need to load up existing trees
      exec("lab/modules/ai/Behavior/behaviorTreeManager.cs");
   
   if(BehaviorTreeGroup.getCount() == 0)
   {
      %this.createTree();
   }
   else
   {
      BTEditorContentList.refresh();
      if(BTEditorTabBook.getCount() == 0)
         BTEditorContentList.setFirstSelected();
   }
   
   %this.updateUndoMenu();
   %this.updateNodeTypes();
}

//==============================================================================
// INIT
//==============================================================================
function BTEditCanvas::onAdd( %this )
{
 //  %this.onCreateMenu();
   
   // close any invalid tab book pages
   for( %i=0; %i < BTEditorTabBook.getCount(); %i++)
   {
      %page = BTEditorTabBook.getObject(%i);
      if(!isObject(%page.rootNode) || %page.rootNode.getClassName() !$= "Root")
      {
         BTEditorTabBook.remove(%page);
         %page.delete();
         %i--;
      }           
   }
}

function BTEditCanvas::onRemove( %this )
{
   if( isObject( BehaviorTreeEditorGui.menuGroup ) )
      BehaviorTreeEditorGui.delete();

   // cleanup
   //%this.onDestroyMenu();
   
   //BTEditorTabBook.deleteAllObjects();
}

function BTEditCanvas::quit( %this )
{
   // we must not delete a window while in its event handler, or we foul the event dispatch mechanism
   %this.schedule(10, delete);
   
   Canvas.setContent(BTEditor.lastContent);
   $InBehaviorTreeEditor = false;
   BehaviorTreeManager.onBehaviorTreeEditor(false);
}
