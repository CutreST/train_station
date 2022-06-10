using Godot;
using System;
using System.Collections.Generic;

namespace Entities.Components.States
{
    public class ActionState : Node, IComponentNode
    {
        public Entity MyEntity { get; set; }

        HashSet<Action> doAction = new HashSet<Action>();

        #region suscribers
        public void SubscriteToDoAction(in Action func){
            if(doAction.Contains(func) == false){
                doAction.Add(func);
            }
        }

        public void UnsubscriteToDoAction(in Action func){
            if(doAction.Contains(func)){
                doAction.Remove(func);
            }
        }

        #endregion

        public void DoAction(){
            foreach(Action act in doAction){
                act.Invoke();
            }
        }

        public void OnAwake()
        {
            
        }

        public void OnSetFree()
        {
        }

        public void OnStart()
        {
        }

        public void Reset()
        {
        }
    }

}
