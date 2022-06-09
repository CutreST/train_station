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

        private Vector2 _velocity;

        public Vector2 Acceleration { get; set; }

        private MovementSystem movSys;

        public Entity MyEntity { get; set; }
        private Vector2 _direction;

        // borrar, sólo test
        private InputInGame input;

        #region Godot methods
        public override void _EnterTree()
        {
            this.OnAwake();

        }


        public override void _ExitTree()
        {
            this.OnSetFree();
        }

        public override void _PhysicsProcess(float delta)
        {
            try
            {
                input.GetInputs();

                // checkeamos la mierda de accion, algo básico
                // vale, hemos hecho una area 2d, sólo para probar
                // con raycast sería mejor, pero creo que será más sencillo así, no?
                if (input.IsAction)
                {
                    ActionDoerComponent act = this.MyEntity.TryGetFromChild_Rec<ActionDoerComponent>();
                    if (act != null)
                    {
                        act.DoAction();
                    }

                }
                if (input.IsMovement)
                {
                    //Messages.Print(input.Direction.ToString());
                    base.MoveAndSlide(Speed * input.Direction.Normalized());

                    MoveState st;

                    if (this.MyEntity.TryGetIComponentNode<MoveState>(out st) == false)
                    {
                        Messages.Print("Move State Not Found for " + this.MyEntity.Name, Messages.MessageType.ERROR);
                        return;
                    }

                    st.Move(input.Direction);
                }
            }
            catch (NullReferenceException e)
            {
                Messages.Print(e.Message, Messages.MessageType.ERROR);
            }
        }

        #endregion




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
            input = game.Input;
        }

        public void OnSetFree()
        {
            this.MyEntity = null;

        }

        public void OnStart()
        {
            MovementSystem temp;

            if (SystemManager.GetInstance(this).TryGetSystem<MovementSystem>(out temp, true))
            {
                Messages.Print("test para probar");
                Messages.Print("test para probar error", Messages.MessageType.ERROR);
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

