using Godot;
using System;
using System.Collections.Generic;

namespace Entities.Components.States
{
    // no, no, esta mierda se ocupa de todo.
    // es decir, el behaviour pilla el input (de todo)
    // B_Tree
    // ---Get input
    // ---Is_Direction?
    // ------MoveState.Move()
    // ----------MoveComponent.Move
    // ----------Changedirection
    // ----------Sprite, etc
    //
    // ahora la pregunta es, nos suscribimos uno a uno? o esto lo controla todo?
    public class MoveState : Node, IComponentNode
    {
        public Entity MyEntity { get; set; }

        public enum Direction : byte { UP, DOWN, LEFT, RIGHT}
        private Direction _currentDirection = Direction.RIGHT;

        HashSet<Action<Direction>> changeDirectionsList = new HashSet<Action<Direction>>();
        HashSet<Action<Vector2>> moveList = new HashSet<Action<Vector2>>();

        public override void _ExitTree()
        {
            this.OnSetFree();
        }

        public void Move(Vector2 direction)
        {
            this.OnMove(direction);
            this.ChangeDirection(direction);
        }

        private void ChangeDirection(Vector2 direction)
        {
            if ((_currentDirection == Direction.UP && direction.y < 0 == false) ||
               (_currentDirection == Direction.DOWN && direction.y > 0 == false) ||
               (_currentDirection == Direction.LEFT && direction.x < 0 == false) ||
               (_currentDirection == Direction.RIGHT && direction.x > 0 == false))
            {
                if (direction.x < 0)
                {
                    _currentDirection = Direction.LEFT;
                }else if(direction.x > 0){
                    _currentDirection = Direction.RIGHT;
                }else if(direction.y > 0){
                    _currentDirection = Direction.DOWN;
                }else{
                    _currentDirection = Direction.UP;
                }

                this.OnChangeDirection();
            }
        }



        #region subscripters
        public void SubrscribeToChangeDirection(Action<Direction> function){
            if(this.changeDirectionsList.Contains(function) == false){
                this.changeDirectionsList.Add(function);
            }
            
        }

        public void UnSubscriteToChangeDirection(Action<Direction> function){
            if(this.changeDirectionsList.Contains(function)){
                this.changeDirectionsList.Remove(function);
            }
        }

        public void SubscribeToOnMove(Action<Vector2> function){
            if(this.moveList.Contains(function) == false){
                this.moveList.Add(function);
            }
        }

        public void UnsubscribeToOnMove(Action<Vector2> function){
            if(this.moveList.Contains(function)){
                this.moveList.Remove(function);
            }
        }

        #endregion


        #region launchers
        private void OnMove(Vector2 direction)
        {
            foreach(Action<Vector2> move in moveList){
                move.Invoke(direction);
            }
        }

        private void OnChangeDirection()
        {
            foreach(Action<Direction> dir in changeDirectionsList){
                dir.Invoke(this._currentDirection);
            }
        }
        #endregion

        public void OnAwake()
        {

        }

        public void OnSetFree()
        {
            changeDirectionsList.Clear();
            changeDirectionsList = null;
            moveList.Clear();
            moveList = null;
        }

        public void OnStart()
        {

        }

        public void Reset()
        {

        }
    }
}

