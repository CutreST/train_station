using Godot;
using System;

namespace MySystems.Inputs{
    public class InDialogInput : Input_Base
    {

        public bool IsNext{ get; private set; }

        private const string DIAL_NAME = "dl_next";
        public override void GetInputs()
        {
            IsNext = false;

            if(Input.IsActionJustPressed(DIAL_NAME)){
                IsNext = true;
            }
        }
    }
}

