using Godot;
using System;
using BehaviorTree.Base;
using MySystems.Inputs;
using MySystems;

namespace BehaviorTree.InputNodes
{
    public class GetInput : Node, IBehaviorNode
    {
        public States NodeState { get; set; }

        // lo hacemos hardcoded, pero podríamos usar otra manera
        // el visual_system tendría un Input_Base y ese nodo SOLO pillaría
        // inputs, luego el siguiente sería el que trataría de hacer sus mierdas
        // pero de momento lo dejo así
        private InputInGame input;

        public void InitNode(in TreeController controller)
        {
            InGameSystem game;
            
            if(SystemManager.GetInstance(this).TryGetSystem<InGameSystem>(out game) == false){
                controller.ExitNode(this, States.INOPERATIVE);
                Messages.Print("Node " + base.Name + " set to inoperative");
                return;
            }

            this.input = game.Input;
        }

        public States Tick(in TreeController controller)
        {
            input.GetInputs();
            controller.ExitNode(this, States.SUCCESS);
            return this.NodeState;
        }
    }
}

