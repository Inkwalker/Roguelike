using Roguelike.Dungeon;
using UnityEngine;

namespace Roguelike.Pathfinding
{
    public class TileNode : Node
    {
        public Tile Tile { get; private set; }

        public override Vector3 Position
        {
            get
            {
                return new Vector3(Tile.Position.x, Tile.Position.y);
            }
        }

        public TileNode(Tile tile)
        {
            Tile = tile;
        }
    }
}

