using Godot;
using MySystems;
using System;
using BehaviorTree.Base;
namespace Entities.BehaviourTree.VN_Nodes
{

    public class ReferenceNode_U : DecoratorNode
    {
        [Export]
        private readonly string NODE_PATH;


        public override void InitNode(in TreeController controller)
        {
            //base.InitNode(controller);
            //this.Children = new System.Collections.Generic.List<BaseNode>();
            Messages.Print("Initialized " + base.Name);

            //look for the node
            Node root = controller.Root as Node;

            if (root == null)
            {
                Messages.Print($"Node null at {base.Name}", Messages.MessageType.ERROR);
                this.NodeState = States.INOPERATIVE;
                return;
            }
            root = root.GetNode(NODE_PATH);
            Child = (IBehaviorNode)root;
        }



        public override States Tick(in TreeController controller)
        {
            // this node is succesful if theres some child to put on running            
            //Child.ChangeNodeStatus(controller, States.RUNNING);
            //return base.ChangeNodeStatus(controller, States.SUCCESS);

            // basicamente lo que hacemos es poner el hijo como runeador
            // así lo cargará en el siguiente frame
            controller.ExitNode(this, States.SUCCESS);
            controller.ExitNode(Child, States.RUNNING);

            return this.NodeState;

        }      
    }
}
