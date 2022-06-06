using Godot;
using MySystems;
using System;

namespace Entities.Components
{
    public sealed class MovementComponent : KinematicBody2D, IComponentNode
    {
        [Export]
        public Vector2 Speed{ get; private set; }

        private Vector2 _velocity;

        public Vector2 Acceleration{ get; set; }

        private MovementSystem movSys;

        public Entity MyEntity { get; set; }
        private Vector2 _direction;

        // referencia al movement system

        #region Godot methods
        public override void _EnterTree()
        {
            this.OnAwake();

        }
        

        public override void _ExitTree()
        {
            this.OnSetFree();
        }

        public override void _PhysicsProcess(float delta){
            _direction = new Vector2();
            if(Input.IsActionPressed("ui_up")){
                _direction.y = -1;
            }else if(Input.IsActionPressed("ui_down")){
                _direction.y = 1;
            }

            if(Input.IsActionPressed("ui_left")){
                _direction.x = -1;
            }else if(Input.IsActionPressed("ui_right")){
                _direction.x = 1;
            }

            _velocity = this.Speed * _direction.Normalized();
            base.Position += _velocity;

            base.MoveAndSlide(_velocity, _direction.Normalized());        
        }

        #endregion


        

        #region IComponent

        public void OnAwake()
        {
            
        }

        public void OnSetFree()
        {
            this.MyEntity = null;        

        }

        public void OnStart()
        {
            MovementSystem temp;

            if(SystemManager.GetInstance(this).TryGetSystem<MovementSystem>(out temp, true)){
                Messages.Print("test para probar");
                 Messages.Print("test para probar error", Messages.MessageType.ERROR);
            }           
        }

        public void Reset()
        {
            
        }

        #endregion
    }
}

