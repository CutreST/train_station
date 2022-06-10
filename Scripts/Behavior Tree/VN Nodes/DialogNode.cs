using Godot;
using MySystems;
using BehaviorTree.Base;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class DialogNode : Node, IBehaviorNode
    {
        [Export]
        private readonly string DIAL_ID;

        [Export]
        private MySystems.LoadXML.TextType TO_LOAD;

        private string text;

        public States NodeState { get; set; }

        public void InitNode(in TreeController controller)
        {
            //cargamos mierdas
            //text = MySystems.LoadXML.LoadXmlElement(controller.Root.Name, DIAL_ID, TO_LOAD);
            Messages.Print("Initialized " + base.Name);
        }

       
        public States Tick(in TreeController controller)
        {
            //throw new NotImplementedException();

            Messages.Print("Tick on " + base.Name);
            
            if(NodeState == States.RUNNING){
                controller.ExitNode(this, States.SUCCESS);
            }else{
                controller.ExitNode(this, States.RUNNING);

                DialogSystem dial;
                if(SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true)){
                    dial.DisplayDialog(text);
                }

            }

            return this.NodeState;
        }
    }
}
