using Godot;
using System;

namespace MySystems
{
    /// <summary>
    /// The main node for <see cref="System_Manager"/>
    /// We use the node to call physics, process and to access the tree without 
    /// the need of another node and so on.
    /// Used on autoload to, so we don't have to bother to put on every
    /// single test scene and creates the <see cref="SystemManager"/>
    /// <para>
    /// Plus, we have a custom timer runned by the update delta.
    /// </para>
    /// </summary>
    public class AppManager_GO : Node
    {

        /// <summary>
        /// The <see cref="SystemManager"/> for the application
        /// </summary>
        public SystemManager Manager { get; set; }        
       
        /// <summary>
        /// Total time of the application.
        /// </summary>
        public static float Time { get; private set; }
        public static float Delta;
        public override void _EnterTree()
        {
            GD.Print("Instance made to the tree");
            // call for the instance
            this.Manager = SystemManager.GetInstance(this);
        }

        public override void _Process(float delta)
        {
            Manager.MyUpdate(delta);
            Time += delta;
            Delta = delta;
        }

        public override void _PhysicsProcess(float delta)
        {
            Manager.MyPhysic(delta);
        }


    }
}
