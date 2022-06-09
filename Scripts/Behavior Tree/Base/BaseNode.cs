using Godot;
using System;

namespace BehaviorTree.Base{
    public abstract class BaseNode : Node, IBehaviorNode
    {
        public States NodeState { get ; set ; }

        public abstract void InitNode(in TreeController controller);


        public abstract States Tick(in TreeController controller);

    }
}

