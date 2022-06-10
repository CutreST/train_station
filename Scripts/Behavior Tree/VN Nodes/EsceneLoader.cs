using Godot;
using System;
using BehaviorTree.Base;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class EsceneLoader : Node, IBehaviorNode
    {
        [Export]
        private readonly string SCENE_PATH;

        private BaseNode newRoot;

        public States NodeState { get; set; }

        public void InitNode(in TreeController controller)
        {
            //check if the scene is a base node
            PackedScene temp = GD.Load<PackedScene>(SCENE_PATH);
            newRoot = temp.Instance() as BaseNode;

            if (newRoot == null)
            {
                Messages.Print($"OJU!!! EsceneLoader{base.Name} found an incompatible scene.\n", Messages.MessageType.ERROR);
                this.NodeState = States.INOPERATIVE;
                return;
            }

        }

        public States Tick(in TreeController controller)
        {
            //change root            
            if (newRoot != null)
            {
                controller.Root = newRoot;
                controller.AddChild(newRoot);
            }

            return NodeState = States.SUCCESS;
        }
    }
}
