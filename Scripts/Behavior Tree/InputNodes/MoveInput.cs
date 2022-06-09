using Godot;
using System;
using BehaviorTree.Base;
using Entities.Components.States;
using Entities;
using MySystems.Inputs;
using MySystems;

namespace BehaviorTree.InputNodes
{
    public class MoveInput : Node, IBehaviorNode
    {
        public States NodeState { get; set; }

        private MoveState movement;
        private InputInGame input;

        public void InitNode(in TreeController controller)
        {
            Entity ent = controller.TryGetFromParent_Rec<Entity>();
            if (ent == null)
            {
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("No found entity on: " + base.Name, Messages.MessageType.ERROR);
                return;
            }

            if (ent.TryGetIComponentNode<MoveState>(out movement) == false)
            {
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("No found moveState on: " + base.Name, Messages.MessageType.ERROR);
                return;
            }

            InGameSystem game;

            if (SystemManager.GetInstance(this).TryGetSystem<InGameSystem>(out game) == false)
            {
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("Node " + base.Name + " set to inoperative");
                return;
            }

            this.input = game.Input;
        }

        public States Tick(in TreeController controller)
        {
            if (input.Direction.x != 0 || input.Direction.y != 0)
            {
                this.movement.Move(this.input.Direction);
                return controller.ExitNode(this, States.SUCCESS);
            }

            return controller.ExitNode(this, States.FAILURE);

        }

    }
}

