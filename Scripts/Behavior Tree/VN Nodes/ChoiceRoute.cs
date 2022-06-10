using Godot;
using MySystems;
using System;
using BehaviorTree.Base;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ChoiceRoute : SequenceNode
    {
        [Export]
        public readonly string CHOICE_ID;

        [Export]
        private MySystems.LoadXML.TextType TO_LOAD;

        public string Text{ get; private set; }

        public override void InitNode(in TreeController controller)
        {
            base.InitNode(controller);
            Node n = controller.Root as Node;
            Text = MySystems.LoadXML.LoadXmlElement(n.Name, CHOICE_ID, TO_LOAD);
            Messages.Print("Initialized " + base.Name);
            //ConsoleSystem.Write("Text is: " + Text);
        }          
    }
}
