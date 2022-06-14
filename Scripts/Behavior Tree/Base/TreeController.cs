
using Godot;
using System;
using System.Collections.Generic;
using Base.Interfaces;
using MySystems;

//using Entities.BehaviourTree.Loaders;

namespace BehaviorTree.Base
{
    /// <summary>
    /// The behaviour tree controller
    /// <remarks>
    /// There's work to do, i think. But it works for what we need.
    /// </remarks>
    /// </summary>
    public class TreeController : Node, IPhysic
    {
        /// <summary>
        /// The root node.
        /// </summary>
        private IBehaviorNode _root;

        public IBehaviorNode Root
        {
            get => _root;
            set
            {
                if (_root != null)
                {                        
                    Node godotNode = _root as Node;
                    if (godotNode != null)
                    {
                        godotNode.QueueFree();
                    }
                    else
                    {
                        GD.PrintErr("Tree Controller Says: the root is not a Godot node. Why?");
                    }

                }
                _root = value;
                _currentNode = _root;

                if (_root != null)
                {
                    this.InitRoot();
                    Messages.Print("New root on Behaviour Tree");
                }

            }
        }

        /// <summary>
        /// The current node. At some points this is going to be a stack, so, we 
        /// can keep track of the running onoes
        /// </summary>
        private IBehaviorNode _currentNode;

        #region Godot MEthods
        public override void _Ready()
        {
            // no root, no node, no error this way
            if (this.TrySetRoot() == false)
            {
                base.SetPhysicsProcess(false);
                GD.Print("Tree Controller says: There's no root node here");
            }

            // metemso en el physic
            this.ActivateTree();
        }

        public override void _ExitTree()
        {

        }
        #endregion

        /// <summary>
        /// Sets the root if it exists as a child of the controller and puts it to <see cref="TreeController._currentNode"
        /// OJU! The root has to be the first child (index = 0)!
        /// </summary>
        /// <returns>got the root?</returns>
        private bool TrySetRoot()
        {
            _root = base.GetChild(0) as IBehaviorNode;
            if (_root == null)
            {
                return false;
            }

            this.InitRoot();
            return true;
        }

        private void InitRoot()
        {
            _root.InitNode(this);
            _currentNode = _root;
        }

        [Obsolete]
        public void EnterNode(in IBehaviorNode node)
        {
            //_currentNode = node;                      
        }

        Queue<IBehaviorNode> Q;

        public States ExitNode(in IBehaviorNode node, in States state)
        {
            node.NodeState = state;

            if (node.NodeState == States.RUNNING)
            {
                _currentNode = node;
            }

            return state;
        }

        public void MyPhysic(in float delta)
        {
            _currentNode.Tick(this);
        }

        public void DisableTree()
        {
            SystemManager.GetInstance(this).currentSys.RemoveFromPhysic(this);
        }

        public void ActivateTree()
        {
            // metemso en el physic
            SystemManager.GetInstance(this).currentSys.AddToPhysic(this);
        }
    }
}
