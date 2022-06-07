using Godot;
using System;
using MySystems.Inputs;

namespace MySystems
{
    public class InGameSystem : VisualSystem_Base
    {
        public InputInGame Input{ get; set; }


        public InGameSystem(in Node go, in SystemManager manager) : base(go, manager)
        {
            
        }

        public InGameSystem() : base(null, null){
            
        }

        public override void OnEnterStack()
        {
            Messages.EnterStack(this);
            this.Input = new InputInGame();
        }

        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem<InGameSystem>(this);
        }

        public override void OnExitStack()
        {
            Messages.ExitStack(this);
            this.Input = null;
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem<InGameSystem>(this);
        }

        public override void OnPauseStack()
        {
            Messages.PauseStack(this);
        }

        public override void OnResumeStack()
        {
            Messages.ResumeStack(this);
        }
    }
}

