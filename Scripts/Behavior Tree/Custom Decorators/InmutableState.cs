using BehaviorTree.Base;
using Godot;
using System;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// A decorator that always return the same state
    /// </summary>
    public class InmutableState : DecoratorNode
    {
        /// <summary>
        /// The desired state to return
        /// </summary>
        [Export]
        private States _inmutableState;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public InmutableState()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state">Desired state</param>
        public InmutableState(in States state)
        {
            base.NodeState = state;
            if(this.Child == null){
                base.NodeState = States.INOPERATIVE;
                Messages.Print("The node " + base.Name + "has no child");
            }
        }

        public override void InitNode(in TreeController controller)
        {
            base.InitNode(controller);
            base.NodeState = _inmutableState;
        }

        public override States Tick(in TreeController controller)
        {          
            
            controller.EnterNode(Child);
            Child.Tick(controller);           

            return base.NodeState;
        }

    }
}
