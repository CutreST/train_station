using Godot;
using MySystems;
using System;
using Base.Interfaces;
using MySystems.Inputs;
using Entities.Components.States;

namespace Entities.Components
{
    public sealed class MovementComponent : KinematicBody2D, IComponentNode, IPhysic
    {
        [Export]
        public Vector2 Speed { get; private set; }

        [Export]
        private bool connectToOnMove;

        private Vector2 _velocity;

        public Vector2 Acceleration { get; set; }

        private MovementSystem movSys;

        public Entity MyEntity { get; set; }
        private Vector2 _direction;

        

        #region Godot methods
        public override void _EnterTree()
        {
            this.OnAwake();

        }

        public override void _Ready()
        {
            this.OnStart();
        }


        public override void _ExitTree()
        {
            this.OnSetFree();
        }
        #endregion

        private void SubscribeToOnMove()
        {
            MoveState m;
            if(this.MyEntity.TryGetIComponentNode<MoveState>(out m) == false){
                Messages.Print("MoveState not found on " + base.Name, Messages.MessageType.ERROR);
                connectToOnMove = false;
                return;
            }
            m.SubscribeToOnMove(this.Move);
        }

        private void UnsubscribeToOnMove()
        {
            MoveState m;
            if(this.MyEntity.TryGetIComponentNode<MoveState>(out m) == false){
                Messages.Print("MoveState not found on " + base.Name, Messages.MessageType.ERROR);
                connectToOnMove = false;
                return;
            }
            m.UnsubscribeToOnMove(this.Move);
        }

        private void Move(Vector2 direction){
            base.MoveAndSlide(Speed * direction);
        }

        #region IComponent

        public void OnAwake()
        {
            InGameSystem game = new InGameSystem();
            SystemManager manager = SystemManager.GetInstance(this);

            if (manager.TryAddSystem(game) == false)
            {
                Messages.Print("Error", Messages.MessageType.ERROR);
            }
            manager.AddToStack(game);
        }

        public void OnSetFree()
        {
            this.MyEntity = null;

            if(connectToOnMove){
                this.UnsubscribeToOnMove();
            }

        }

        public void OnStart()
        {
            if(connectToOnMove){
                this.SubscribeToOnMove();
            }

        }

        public void Reset()
        {

        }

        public void MyPhysic(in float delta)
        {

        }

        #endregion
    }
}

