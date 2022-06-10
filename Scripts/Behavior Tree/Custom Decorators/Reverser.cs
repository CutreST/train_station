using System;
using Godot;
using BehaviorTree.Base;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// A decorator node that reverse the success-failure
    /// </summary>
    public class Reverser : DecoratorNode
    {        
        
        public override States Tick(in TreeController controller)
        {   
            controller.EnterNode(Child);                     
            switch (base.Child.Tick(controller))
            {
                case States.FAILURE:
                    return controller.ExitNode(this, States.SUCCESS);
                case States.SUCCESS:
                    return controller.ExitNode(this, States.FAILURE);
            }
            
            
            return base.NodeState = States.RUNNING;
        }

       
    }
}
