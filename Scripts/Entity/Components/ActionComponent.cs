using Godot;
using System;

namespace Entities.Components
{
    public class ActionComponent : Node
    {   
        // para esto miraremos el hijo para el tipo de acción. Si queremos algo 
        // super-chungo, podemos meter un super behaviour tree

        public void DoAction(in Entity entity){
            Messages.Print($"an action was done by {entity.Name}");
        }

    }
}

