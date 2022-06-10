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
        private string test = "La vaca lechera tiene ubres que le llegan hasta la teta";

        public void DoAction(in Entity entity){
            Messages.Print($"an action was done by {entity.Name}");
            this.ShowText();
        }

        private void ShowText(){
            DialogSystem dial;

            SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true);
            dial.DisplayDialog(test);
        }

    }
}

