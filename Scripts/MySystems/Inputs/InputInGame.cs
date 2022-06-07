using Godot;
using System;


namespace MySystems.Inputs
{
    public class InputInGame : Input_Base
    {
        // pause
        private const string PAUSE = "gm_pause";
        // action
        private const string ACTION = "gm_action";

        // movement

        private const string MOVE_UP = "gm_up";
        private const string MOVE_DOWN = "gm_down";
        private const string MOVE_RIGHT = "gm_right";
        private const string MOVE_LEFT = "gm_left";

        public bool IsPause{ get; private set; }
        public bool IsAction{ get; private set; }

        public bool IsMovement{ get => this.Direction.x != 0 || this.Direction.y != 0; }

        private Vector2 _direction;
        public Vector2 Direction{ get => _direction; private set { _direction = value; } }

        public override void GetInputs()
        {
            // escape
            this.IsPause = Input.IsActionJustPressed(PAUSE);

            // action
            this.IsAction = Input.IsActionJustPressed(ACTION);

            // movement
            this.GetMovement();
        }

        private void GetMovement(){
            _direction = new Vector2();

            if(Input.IsActionPressed(MOVE_DOWN)){
                _direction.y = 1;
            }else if(Input.IsActionPressed(MOVE_UP)){
                _direction.y = -1;
            }

            if(Input.IsActionPressed(MOVE_LEFT)){
                _direction.x = -1;
            }else if(Input.IsActionPressed(MOVE_RIGHT)){
                _direction.x = 1;
            }            
        }

    
    }
}

