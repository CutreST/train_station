using Godot;
using System;
using BehaviorTree.Base;
using MySystems.Inputs;
using MySystems;
using Entities.Components.States;
using Entities;

namespace BehaviorTree.InputNodes
{
    public class ActionInput : Node, IBehaviorNode
    {
        public States NodeState { get; set; }

        // pillamos el action state, que se ocupe él de las cosas, o no?
        private InputInGame input;
        private ActionState action;


        // y ale

        // oju que también podemos unir este nodo el de moverse y demás con todos los inputs (optimización)
        // Por alguna razón, TreeController NNO ES UN IComponent, creo que como lo he usado para todo no lo hace bien...
        // Como es interfaz, creo que debería serlo, pero lo dejamos como algo para TODO
        public void InitNode(in TreeController controller)
        {   
            Entity ent = controller.TryGetFromParent_Rec<Entity>();

            if (ent == null)
            {
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("No found entity on: " + base.Name, Messages.MessageType.ERROR);
                return;
            }

            if (ent.TryGetIComponentNode<ActionState>(out action) == false)
            {
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("No found ActionState on: " + base.Name, Messages.MessageType.ERROR);
                return;
            }

            InGameSystem game;
            if (SystemManager.GetInstance(this).TryGetSystem<InGameSystem>(out game) == false)
            {
                Messages.Print($"No Game system found on {base.Name}", Messages.MessageType.ERROR);
                return;
            }

            input = game.Input;



        }

        public States Tick(in TreeController controller)
        {
            if (input.IsAction)
            {
                Messages.Print("Got Action!!!");
                action.DoAction();
            }

            return controller.ExitNode(this, States.SUCCESS);
        }
    }
}

