using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Pathfinding
{
    public class NodeGroup<T>
    {
        private Dictionary<T, Node> nodes;

        public NodeGroup()
        {
            nodes = new Dictionary<T, Node>();
        }

        public void AddNode(T key, Node node)
        {
            nodes.Add(key, node);
        }

        public Node GetNode(T key)
        {
            Node result = null;
            nodes.TryGetValue(key, out result);
            return result;
        }

        public void RemoveNode(T key)
        {
            Node nodeToRemove;

            if (nodes.TryGetValue(key, out nodeToRemove))
            {
                nodes.Remove(key);

                //Deleting all links to this node
                foreach (Node node in nodes.Values)
                {
                    node.Linked.Remove(nodeToRemove);
                }
            }
            else
            {
                Debug.LogError("NodeGroup doesn't contains node for key: " + key.ToString());
            }
        }

        public void SetLink(T from, T to, int cost)
        {
            Node fromNode;
            Node toNode;

            if (nodes.TryGetValue(from, out fromNode) && nodes.TryGetValue(to, out toNode))
            {
                fromNode.LinkToNode(toNode, cost);
            }
            else
            {
                Debug.LogError("NodeGroup doesn't contains one or bouth nodes for keys: " + from.ToString() + " " + to.ToString());
            }
        }

        public void RemoveLink(T from, T to)
        {
            Node fromNode;
            Node toNode;

            if (nodes.TryGetValue(from, out fromNode) && nodes.TryGetValue(to, out toNode))
            {
                fromNode.RemoveLinkToNode(toNode);
            }
            else
            {
                Debug.LogError("NodeGroup doesn't contains one or bouth nodes for keys: " + from.ToString() + " " + to.ToString());
            }
        }
    }
}