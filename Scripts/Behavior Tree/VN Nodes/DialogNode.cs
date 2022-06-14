using Godot;
using MySystems;
using BehaviorTree.Base;
using System;

namespace BehaviorTree.VN_Nodes
{

    public class DialogNode : Node, IBehaviorNode
    {
        [Export]
        private readonly string DIAL_ID;

        [Export]
        private MySystems.LoadXML.TextType TO_LOAD;

        [Export]
        private readonly string GROUP_ID;

        private string text;

        public States NodeState { get; set; }

        private TreeController cont;

        public void InitNode(in TreeController controller)
        {
            //cargamos mierdas
            text = MySystems.LoadXML.LoadXmlElement(GROUP_ID, DIAL_ID, TO_LOAD);
            Messages.Print("Initialized " + base.Name);
        }


        public States Tick(in TreeController controller)
        {
            //throw new NotImplementedException();

            Messages.Print("Tick on " + base.Name);
            DialogSystem dial;

            if (NodeState == States.PENDING_SUCCESS)
            {
                SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true);
                dial.UnsubscriteOnEndText(this.OnExitText);
                Messages.Print("Pending done");
                return cont.ExitNode(this, States.SUCCESS);
            }

            if (SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true))
            {
                dial.DisplayDialog(text);

                this.cont = controller;
                this.OnEnterText(cont);
                dial.SubscribeOnEndText(this.OnExitText);
                cont.ExitNode(this, States.RUNNING);
            }           

            return this.NodeState;
        }

        private void OnEnterText(in TreeController controller)
        {
            controller.DisableTree();
        }

        private void OnExitText()
        {
            this.cont.ActivateTree();
            this.cont.ExitNode(this, States.PENDING_SUCCESS);
            Messages.Print("On exit text!!!");
        }




    }
}
