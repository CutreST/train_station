using Godot;
using System;

namespace Entities.Components
{
    public class ActionDoerComponent : Area2D, IComponentNode
    {
        public Entity MyEntity { get; set; }

        private const string SIGNAL_BODY_ENTERED = "body_entered";
        private const string SIGNAL_BODY_EXITED = "body_exited";

        private ActionComponent _actionable;
        public override void _Ready()
        {
            this.OnStart();
        }



        private void OnBodyEntered(Node other){
            Messages.Print("Entered " + other.Name);

            _actionable = other as ActionComponent;
        }

        private void OnBodyExited(Node other){
            Messages.Print("Exited " + other.Name);

            ActionComponent temp = other as ActionComponent;

            if(_actionable != null && temp != null && _actionable == other){
                _actionable = null;
            }
        }

        public void DoAction(){
            if(_actionable != null){
                _actionable.DoAction(this.MyEntity);
            }
        }

        public void OnAwake()
        {

        }

        public void OnSetFree()
        {
            this.DisconnectToEnterBody();
        }

        public void OnStart()
        {
            this.ConnectToEnterBody();
            this.ConnectToExitBody();
        }

        #region Connecting-Disconecting signals
        private void ConnectToEnterBody(){
            base.Connect(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            Messages.Print("No errors here");
            bool conn = base.IsConnected(SIGNAL_BODY_ENTERED, this, "OnBodyEntered");
            Messages.Print("Signal Connected: " + conn.ToString());
        }

        private void DisconnectToEnterBody(){
            base.Disconnect(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            bool conn = base.IsConnected(SIGNAL_BODY_ENTERED, this, "OnBodyEntered");
            Messages.Print("Signal Disconnected: " + conn.ToString());
        }

        private void ConnectToExitBody(){
            base.Connect(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("No errors here");
            bool conn = base.IsConnected(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("Signal Connected: " + conn.ToString());
        }

        private void DisconnectToExitBody(){
            base.Disconnect(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("No errors here");
            bool conn = base.IsConnected(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("Signal Connected: " + conn.ToString());
        }
        #endregion
        

        public void Reset()
        {

        }
    }
}

