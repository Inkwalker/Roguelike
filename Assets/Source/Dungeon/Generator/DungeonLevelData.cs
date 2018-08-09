using UnityEngine;
using System.Collections.Generic;
using Roguelike.LoadSave;

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

        public GameMapData GetGameMapData()
        {
            var data = new GameMapData(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int tileIndex = floor[x, y] ? 0 : 1; //TODO: add proper indices and tilesets.

                    data.Set(x, y, tileIndex);
                }
            }

            return data;
        }

        public List<EntityInstanceData> GetEntitiesData()
        {
            var result = new List<EntityInstanceData>();

            foreach (var entity in entities)
            {
                var instanceData = new EntityInstanceData(entity.prefab.PrefabID);

                instanceData.Position = new Vector2Int(entity.x, entity.y);

                result.Add(instanceData);
            }

            return result;
        }
    }
}
