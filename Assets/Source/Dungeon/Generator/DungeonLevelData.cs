using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Dungeon.Generator
{
    public class DungeonLevelData
    {
        public List<RectInt> rooms;
        public List<DungeonEntityData> entities;
        public bool[,] floor;
        public Vector2Int playerPosition;

        public int Width { get { return floor.GetLength(0); } }
        public int Height { get { return floor.GetLength(1); } }

        public DungeonLevelData(int width, int height)
        {
            rooms = new List<RectInt>();
            entities = new List<DungeonEntityData>();

            floor = new bool[width, height];
        }
    }
}
