using Godot;
using MySystems;
using System;

namespace Entities.Components
{
    public sealed class MovementComponent : KinematicBody2D, IComponentNode
    {
        [Export]
        public Vector2 Velocity{ get; private set; }

        public Vector2 Acceleration{ get; set; }

        private MovementSystem movSys;

        public Entity MyEntity { get; set; }

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

