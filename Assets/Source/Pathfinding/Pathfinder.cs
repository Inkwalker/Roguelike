using System.Collections.Generic;

namespace Roguelike.Pathfinding
{
    public static class Pathfinder
    {
        //If findClosestPath is true and if end node is unreachable then this method returns path to closest node.
        public static List<T> FindPath<T>(Node start, Node end, bool findColsestPath = false) where T : Node
        {
            var openList = new List<NodeInfo>();
            var closedList = new List<NodeInfo>();
            var nodesInfo = new Dictionary<Node, NodeInfo>();

            NodeInfo currentNode = AddToOpenList(start, openList);
            currentNode.UpdateH(end);
            nodesInfo.Add(start, currentNode);

            bool stopFlag = false;

            while (!stopFlag)
            {
                foreach (var nodeLink in currentNode.Node.Linked)
                {
                    Node linkedNode = nodeLink.Key;

                    if (!nodesInfo.ContainsKey(linkedNode))
                    {
                        NodeInfo info = AddToOpenList(linkedNode, openList);
                        info.Parent = currentNode;
                        info.UpdateH(end);
                        nodesInfo.Add(linkedNode, info);

                        stopFlag = linkedNode == end;
                    }
                    else
                    {
                        NodeInfo info = nodesInfo[linkedNode];
                        if (info.List == NodeInfo.NodeList.Open && info.Parent != currentNode)
                        {
                            int newG = currentNode.G + nodeLink.Value;
                            if (newG < info.G)
                            {
                                info.Parent = currentNode;
                            }
                        }
                    }
                }

                MoveToClosedList(currentNode, openList, closedList);

                currentNode = FindNextNode(openList);

                if (currentNode == null)
                {
                    stopFlag = true;
                }
            }

            if (nodesInfo.ContainsKey(end))
            {
                List<T> path = GetPathToNode<T>(nodesInfo[end]);
                return path;
            }
            else
            {
                //No path to target. Find closest node
                if (findColsestPath == true && nodesInfo.Count > 0)
                {
                    var info = new List<NodeInfo>(nodesInfo.Values);
                    NodeInfo minHNode = info[0];

                    foreach (var node in info)
                    {
                        if (node.H < minHNode.H)
                        {
                            minHNode = node;
                        }
                        else if (node.H == minHNode.H) //if nodes at same distance then take closest to start one
                        {
                            if (node.G < minHNode.G)
                            {
                                minHNode = node;
                            }
                        }
                    }

                    List<T> path = GetPathToNode<T>(minHNode);
                    return path;
                }
            }

            return new List<T>();
        }

        private static NodeInfo AddToOpenList(Node node, List<NodeInfo> openList)
        {
            NodeInfo info = new NodeInfo(node);

            openList.Add(info);

            info.List = NodeInfo.NodeList.Open;

            return info;
        }

        private static void MoveToClosedList(NodeInfo node, List<NodeInfo> openList, List<NodeInfo> closedList)
        {
            openList.Remove(node);
            closedList.Add(node);

            node.List = NodeInfo.NodeList.Closed;
        }

        private static NodeInfo FindNextNode(List<NodeInfo> openList)
        {
            if (openList.Count == 0)
                return null;

            NodeInfo bestNode = openList[0];

            foreach (var node in openList)
            {
                if (node.F < bestNode.F)
                {
                    bestNode = node;
                }
            }

            return bestNode;
        }

        private static List<T> GetPathToNode<T>(NodeInfo node) where T : Node
        {
            List<T> reversedPath = new List<T>();
            NodeInfo currentNodeInfo = node;

            while (currentNodeInfo != null)
            {
                reversedPath.Add(currentNodeInfo.Node as T);
                currentNodeInfo = currentNodeInfo.Parent;
            }

            List<T> path = new List<T>();
            for (int i = reversedPath.Count - 1; i >= 0; i--)
            {
                path.Add(reversedPath[i]);
            }

            return path;
        }

        private class NodeInfo
        {
            public Node Node { get; set; }

            private NodeInfo parent;
            public NodeInfo Parent
            {
                get { return parent; }
                set
                {
                    parent = value;
                    UpdateG();
                }
            }

            public int F { get; private set; }
            public int G { get; private set; }
            public int H { get; private set; }

            public NodeList List { get; set; }

            public NodeInfo(Node node)
            {
                Node = node;
            }

            public void UpdateG()
            {
                int parentTransition = Parent != null && Node.Linked.ContainsKey(Parent.Node) ? Node.Linked[Parent.Node] : 0;
                int parentG = Parent != null ? Parent.G : 0;

                G = parentTransition + parentG;

                F = G + H;
            }

            public void UpdateH(Node target)
            {
                H = (int)((target.Position - Node.Position).sqrMagnitude);

                F = G + H;
            }

            public enum NodeList
            {
                None,
                Open,
                Closed
            }
        }
    }
}