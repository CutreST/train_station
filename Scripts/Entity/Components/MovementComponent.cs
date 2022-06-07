using Godot;
using MySystems;
using System;
using Base.Interfaces;
using MySystems.Inputs;

namespace Entities.Components
{
    public sealed class MovementComponent : KinematicBody2D, IComponentNode, IPhysic
    {
        [Export]
        public Vector2 Speed{ get; private set; }

        private Vector2 _velocity;

        public Vector2 Acceleration{ get; set; }

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

        public override void _PhysicsProcess(float delta){
            try{
                input.GetInputs();

                // checkeamos la mierda de accion, algo básico
                if(input.IsAction){
                    CollisionShape2D shape = new CollisionShape2D();
                    // luego lo hago, no sé si usar unos cuantos raycast y ale, acabamos antes
                    // bien, esto va con nodos, así que sólo usaremos uno y a partir de allí daremos las
                    // vueltas, no?
                    // sí, podría hacerlo con 20 de ellos, pero no tiene sentido, seria más fácil, eso sí.
                    
                }
                if(input.IsMovement){
                    //Messages.Print(input.Direction.ToString());
                    base.MoveAndSlide(Speed * input.Direction.Normalized());
                }
            }catch(NullReferenceException e){
                Messages.Print(e.Message, Messages.MessageType.ERROR);
            }
        }

        #endregion


        

        #region IComponent

        public void OnAwake()
        {
            InGameSystem game = new InGameSystem();
            SystemManager manager = SystemManager.GetInstance(this);

            if(manager.TryAddSystem(game) == false){
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

            if(SystemManager.GetInstance(this).TryGetSystem<MovementSystem>(out temp, true)){
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

