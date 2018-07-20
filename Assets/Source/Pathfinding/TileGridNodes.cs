using Roguelike.Dungeon;
using UnityEngine;

namespace Roguelike.Pathfinding
{
    //NOTE: don't use movement cost less then 10
    public class TileGridNodes
    {
        private NodeGroup<Tile> nodes;
        private GameMap map;

        public TileGridNodes(GameMap map)
        {
            nodes = new NodeGroup<Tile>();
            this.map = map;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Tile tile = map.GetTile(x, y);
                    TileNode newNode = new TileNode(tile);

                    nodes.AddNode(tile, newNode);
                }
            }
        }

        public void UpdateLinksForTile(Tile tile)
        {
            CreateLinks(tile, false);
            Tile[] neighbors = GetNeighbors(tile, false);

            //update non diagonal neighbors' links
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] != null)
                {
                    CreateLinks(neighbors[i], false);
                }
            }
        }

        private void CreateLinks(Tile tile, bool allowDiagonal)
        {
            Node node = GetNode(tile);

            Tile[] neighbors = GetNeighbors(tile, allowDiagonal);

            int cost = map.IsBlocked(tile.Position.x, tile.Position.y) ? 0 : 10;

            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] != null)
                {
                    Node neighborNode = GetNode(neighbors[i]);

                    if (neighborNode != null)
                    {
                        bool diagonal = Mathf.Abs(tile.Position.x - neighbors[i].Position.x) +
                            Mathf.Abs(tile.Position.y - neighbors[i].Position.y) == 2;
                        int linkCost = diagonal ? (int)(cost * 1.4f) : cost;

                        bool validLink = cost > 0;

                        if (diagonal)
                        {
                            Tile n_1 = map.GetTile(tile.Position.x, neighbors[i].Position.y);
                            Tile n_2 = map.GetTile(neighbors[i].Position.x, tile.Position.y);

                            validLink = n_1 != null && n_2 != null && n_1.Blocked == false && n_2.Blocked == false;
                        }

                        if (validLink)
                        {
                            neighborNode.LinkToNode(node, cost + linkCost);
                        }
                        else
                        {
                            neighborNode.RemoveLinkToNode(node);
                        }

                    }
                }
            }
        }

        public Node GetNode(Tile tile)
        {
            return nodes.GetNode(tile);
        }

        private Tile[] GetNeighbors(Tile tile, bool returnDiagonal)
        {
            int x = tile.Position.x;
            int y = tile.Position.y;

            Tile[] neighbours;

            if (returnDiagonal)
                neighbours = new Tile[8];
            else
                neighbours = new Tile[4];

            //N E S W
            neighbours[0] = map.GetTile(x, y + 1);
            neighbours[1] = map.GetTile(x + 1, y);
            neighbours[2] = map.GetTile(x, y - 1);
            neighbours[3] = map.GetTile(x - 1, y);

            if (returnDiagonal)
            {
                //NE SE SW NW
                neighbours[4] = map.GetTile(x + 1, y + 1);
                neighbours[5] = map.GetTile(x + 1, y - 1);
                neighbours[6] = map.GetTile(x - 1, y - 1);
                neighbours[7] = map.GetTile(x - 1, y + 1);
            }

            return neighbours;
        }
    }
}