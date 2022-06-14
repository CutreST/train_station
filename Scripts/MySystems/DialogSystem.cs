using BehaviorTree.Base;
using Godot;
using MySystems.Inputs;
using System;
using UI.Dialog;

namespace MySystems
{

    public class DialogSystem : VisualSystem_Base
    {
        private static string DIALOG_DISPLAY_PATH = "res://Scenes/UI/Dial_Cont.tscn";

        public bool IsRunning{ get; protected set; }

        // pillamos ref del Behaviour tree para meter nodos y eso
        private TreeController tree;

        public DialogSystem(in Node go, in SystemManager manager) : base(go, manager)
        {
        }

        public DialogSystem() : base (null, null){}

        public DialogDisplay DialDisp { get; set; }

        public InDialogInput DialInput{ get; set; }

        public void DisplayDialog(in string text){
            DialDisp.PutTextToDisplay(text);
            MyManager.AddToStack(this);
        }

        public void CloseDialog(){            
            tree.DisableTree();

            // sacamos el nodo, así no tenemos problemas luego
            Node n = tree.Root as Node;
            n.QueueFree();
            DialDisp.Hide();
            MyManager.RemoveFromStack(this);
            tree.Root = null;
            DialDisp.ClearEvents();
        }


        #region MySystem methods
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
            //dialog
            //first, cheeck if a console already exists
            Node n = MyManager.NodeManager.GetParent();
            DialDisp = n.TryGetFromChild_Rec<DialogDisplay>();

            if (DialDisp != null)
            {
                Messages.Print("yelooooow, is it me");
            }
            else
            {
                PackedScene sc = GD.Load<PackedScene>(DIALOG_DISPLAY_PATH);
                DialDisp = sc.Instance<DialogDisplay>();
                //n.CallDeferred("add_child", _console);
                //de momento lo metemos aquí, seguramente querremos un level controller o algu
                MyManager.NodeManager.AddChild(DialDisp);                
            }

            GO = DialDisp;
            DialDisp.DialogSystem = this;
            DialInput = new InDialogInput();

            //pillamos tree
            this.tree = DialDisp.TryGetFromChild_Rec<TreeController>();

            if(tree == null){
                Messages.Print("Tree not found at DialogSystem", Messages.MessageType.ERROR);
            }
        }

        public void DisplayTree(in IBehaviorNode root){
            tree.Root = root;
            //DialDisp.DialStatus = DialogDisplay.Status.WAITING;
            tree.ActivateTree();
            //tree.AddChild((Node)root);
            MyManager.AddToStack(this);
            
        }

        public void SubscribeOnEndText(in Action func){
            this.DialDisp.SubscribeOnEndText(func);
        }

        public void UnsubscriteOnEndText(in Action func){
            this.DialDisp.UnsubscriteOnEndText(func);
        }

        public override void MyUpdate(in float delta)
        {
            DialInput.GetInputs();
            base.MyUpdate(delta);
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }
        #endregion

        #region Stack Methods
        public override void OnEnterStack()
        {
            Messages.EnterStack(this);
        }


        public override void OnExitStack()
        {
            Messages.ExitStack(this);
        }

        

        public override void OnPauseStack()
        {
            Messages.PauseStack(this);
        }

        public override void OnResumeStack()
        {
            Messages.ResumeStack(this);
        }
        #endregion
    }
}
