using Godot;
using System;
using BehaviorTree.Base;
using MySystems;

namespace BehaviorTree.VN_Nodes
{
    public class EndDialog : Node, IBehaviorNode
    {
        public States NodeState { get; set; }

        public void InitNode(in TreeController controller)
        {
            
        }

        public States Tick(in TreeController controller)
        {
            DialogSystem dial;
            SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial);
            // devolvemos failure para acabar con la sequence
            controller.ExitNode(this, States.FAILURE);
            dial.CloseDialog();
            return this.NodeState;
        }
    }
}

