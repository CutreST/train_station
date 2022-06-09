using Godot;
using System;
using Entities.Components.States;

namespace Entities.Components
{
    public class ActionDoerComponent : Area2D, IComponentNode
    {
        public Entity MyEntity { get; set; }

        [Export]
        private readonly Vector2 _center;

        [Export]
        private readonly Vector2 _separation;

        [Export]
        private bool _connectToChangeDirection;

        private const string SIGNAL_BODY_ENTERED = "body_entered";
        private const string SIGNAL_BODY_EXITED = "body_exited";

        private MoveState.Direction _currentDirection = MoveState.Direction.RIGHT;

        private ActionComponent _actionable;
        public override void _Ready()
        {
            this.OnStart();
        }


        #region events reactions
        private void OnBodyEntered(Node other)
        {
            Messages.Print("Entered " + other.Name);

            _actionable = other as ActionComponent;
        }

        private void OnBodyExited(Node other)
        {
            ActionComponent temp = other as ActionComponent;

            if (_actionable != null && temp != null && _actionable == other)
            {
                Messages.Print("Exited " + other.Name);
                _actionable = null;
            }
        }

        private void OnChangeDirection(MoveState.Direction newDirection){
            // primero centramos
            base.Position -= DirectionToVector(_currentDirection) * _separation;

            // ahora vamos desde el centro
            base.Position += DirectionToVector(newDirection) * _separation;

            _currentDirection = newDirection;
        }

        private Vector2 DirectionToVector(MoveState.Direction direction) => direction switch
        {
            MoveState.Direction.DOWN => new Vector2(0, 1),
            MoveState.Direction.UP => new Vector2(0, -1),
            MoveState.Direction.LEFT => new Vector2(-1, 0),
            MoveState.Direction.RIGHT => new Vector2(1,0),
            _ => new Vector2(0,0),
        };


        #endregion


        public void DoAction()
        {
            if (_actionable != null)
            {
                _actionable.DoAction(this.MyEntity);
            }
        }

        public void OnAwake()
        {

        }

        public void OnSetFree()
        {
            this.DisconnectToEnterBody();
            this.DisconnectToExitBody();

            if(_connectToChangeDirection){
                this.DisconnectToChangeDirection();
            }
        }

        public void OnStart()
        {
            this.ConnectToEnterBody();
            this.ConnectToExitBody();

            if(_connectToChangeDirection){
                this.ConnectToChangeDirection();
            }
        }

        #region Connecting-Disconecting signals

        // enterbody
        private void ConnectToEnterBody()
        {
            base.Connect(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            bool conn = base.IsConnected(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            Messages.Print("Signal Connected: " + conn.ToString());
        }

        private void DisconnectToEnterBody()
        {
            base.Disconnect(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            bool conn = base.IsConnected(SIGNAL_BODY_ENTERED, this, nameof(this.OnBodyEntered));
            Messages.Print("Signal Disconnected: " + conn.ToString());
        }

        // exit body
        private void ConnectToExitBody()
        {
            base.Connect(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            bool conn = base.IsConnected(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("Signal Connected: " + conn.ToString());
        }

        private void DisconnectToExitBody()
        {
            base.Disconnect(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            bool conn = base.IsConnected(SIGNAL_BODY_EXITED, this, nameof(this.OnBodyExited));
            Messages.Print("Signal Connected: " + conn.ToString());
        }

        private void ConnectToChangeDirection(){
            MoveState st;
            if(this.MyEntity.TryGetIComponentNode<MoveState>(out st) == false){
                Messages.Print("Move State Not Found for " + this.MyEntity.Name, Messages.MessageType.ERROR);
                return;
            }

            st.SubrscribeToChangeDirection(this.OnChangeDirection);
        }

        private void DisconnectToChangeDirection(){
            MoveState st;
            if(this.MyEntity.TryGetIComponentNode<MoveState>(out st) == false){
                Messages.Print("Move State Not Found for " + this.MyEntity.Name, Messages.MessageType.ERROR);
                return;
            }

            st.UnSubscriteToChangeDirection(this.OnChangeDirection);
        }
        #endregion


        public void Reset()
        {

        }
    }
}

