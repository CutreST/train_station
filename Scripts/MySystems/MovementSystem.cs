using Entities.Components;
using Godot;
using System;

namespace MySystems
{
    public class MovementSystem : System_Base
    {
        public void Move(in MovementComponent movComp){
            
        }

        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem<MovementSystem>(this);
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem<MovementSystem>(this);
        }
    }
}

