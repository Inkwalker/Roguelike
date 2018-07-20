using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Pathfinding
{
    public abstract class Node
    {
        public abstract Vector3 Position { get; }

        public Dictionary<Node, int> Linked { get; private set; } //Node and transition cost

        public Node()
        {
            Linked = new Dictionary<Node, int>();
        }

        public void LinkToNode(Node node, int cost)
        {
            if (Linked.ContainsKey(node))
            {
                Linked[node] = cost;
            }
            else
            {
                Linked.Add(node, cost);
            }
        }

        public void RemoveLinkToNode(Node node)
        {
            Linked.Remove(node);
        }
    }
}