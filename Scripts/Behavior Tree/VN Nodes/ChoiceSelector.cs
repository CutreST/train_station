using BehaviorTree.Base;
using Godot;
using System.Collections.Generic;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ChoiceSelector : Node, IBehaviorNode
    {
        
        //private readonly string CHOICE_GROUP_ID -> esto no es necesario, solo opt
        public List<ChoiceRoute> Routes{ get; private set; }
        public States NodeState { get; set; }

        public int Selection;

        public ChoiceSelector()
        {
            Routes = new List<ChoiceRoute>();
        }

        
        public void InitNode(in TreeController controller)
        {
            //pillar children
            int count = base.GetChildCount();
            if (count == 0)
            {
                Messages.Print(base.Name + " didn't get any children and now is lonely :(.", Messages.MessageType.ERROR);
                return;
            }

            ChoiceRoute temp;
            for (int i = 0; i < count; i++)
            {
                temp = base.GetChild(i) as ChoiceRoute;

                if (temp != null)
                {
                    temp.InitNode(controller);
                    Routes.Add(temp);
                }
            }

            Selection = -1;
            //cargar el texto (lo hace esto?) 
            //|-> no, de momento lo hace el children, aquí hay optimización

        }

        public States Tick(in TreeController controller)
        {
            //cargamos el dialog system
            /*if (NodeState != States.RUNNING)
            {
                ChoiceSystem choice;
                System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out choice, true);
                choice.ShowSelection(this);
                ChangeNodeStatus(controller, States.RUNNING);
            }else{
                ChangeNodeStatus(controller, States.SUCCESS);
                Routes[Selection].Tick(controller);
                Selection = -1;
            }*/

            return this.NodeState;        

        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Routes.Clear();
        }
    }
}
