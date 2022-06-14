using BehaviorTree.Base;
using Godot;
using MySystems;
using System;

namespace Entities.Components
{
    public class ActionComponent : Node
    {
        // para esto miraremos el hijo para el tipo de acción. Si queremos algo 
        // super-chungo, podemos meter un super behaviour tree
        [Export]
        private string test = "La vaca lechera tiene ubres que le llegan hasta la teta\n" +
                              "Caía la noche mientras lloraba por la leche derramada\n" +
                              "Y ahora esta debe ser la frase final..." ;

        [Export]
        private readonly string PATH;

        public void DoAction(in Entity entity){
            Messages.Print($"an action was done by {entity.Name}");
            this.ShowText();
        }

        private void ShowText(){
            DialogSystem dial;

            SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true);
            // cargamos la cosa
            try{
                IBehaviorNode sc = GD.Load<PackedScene>(PATH).Instance<IBehaviorNode>();
                dial.DisplayTree(sc);
            }catch(Exception e){
                Messages.Print(e.Message, Messages.MessageType.ERROR);
            }
            
            
            
        }

    }
}

