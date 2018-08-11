using Roguelike.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Dungeon.Generator
{
    public static class DungeonGenerator
    {
        public static DungeonLevelData Generate(DungeonLevelSettings settings)
        {
            var levelData = new DungeonLevelData(settings.size.x, settings.size.y);

            for (int i = 0; i < settings.maxRooms; i++)
            {
                int w = Random.Range(settings.minRoomSize, settings.maxRoomSize + 1);
                int h = Random.Range(settings.minRoomSize, settings.maxRoomSize + 1);

                int x = Random.Range(0, settings.size.x - w - 1);
                int y = Random.Range(0, settings.size.y - h - 1);

                var newRoom = new RectInt(x, y, w, h);

                bool discardRoom = false;
                foreach (var room in levelData.rooms)
                {
                    if (Intersects(newRoom, room))
                    {
                        discardRoom = true;
                        break;
                    }
                } 
                
                if (discardRoom == false)
                {
                    CreateRoom(newRoom, levelData);
                    AddEntities(newRoom, settings.entities, settings.maxEntitiesPerRoom, levelData);

                    //put the player in the first room
                    if (i == 0)
                    {
                        var pos = Vector2Int.RoundToInt(newRoom.center);
                        levelData.entities.Add(new DungeonEntityData() { x = pos.x, y = pos.y, prefab = settings.player });
                    }

                    if (levelData.rooms.Count > 0)
                    {
                        Vector2Int c1 = Vector2Int.FloorToInt(levelData.rooms[levelData.rooms.Count - 1].center);
                        Vector2Int c2 = Vector2Int.FloorToInt(newRoom.center);

                        if (Random.Range(0, 2) == 0)
                        {
                            CreateHorizontalTunnel(c1.x, c2.x, c1.y, levelData);
                            CreateVerticalTunnel(c1.y, c2.y, c2.x, levelData);
                        }
                        else
                        {
                            CreateVerticalTunnel(c1.y, c2.y, c1.x, levelData);
                            CreateHorizontalTunnel(c1.x, c2.x, c2.y, levelData);                           
                        }
                    }

                    levelData.rooms.Add(newRoom);
                }
            }

            return levelData;
        }

        private static bool Intersects(RectInt rect1, RectInt rect2)
        {
            return (rect1.xMin <= rect2.xMax && rect2.xMin <= rect1.xMax &&
                    rect1.yMin <= rect2.yMax && rect2.yMin <= rect1.yMax);
        }

        private static void CreateRoom(RectInt rect, DungeonLevelData levelData)
        {
            for (int x = rect.xMin + 1; x < rect.xMax; x++)
            {
                for (int y = rect.yMin + 1; y < rect.yMax; y++)
                {
                    levelData.floor[x, y] = true;
                }
            }
        }

        private static void AddEntities(RectInt rect, Entity[] entities, int maxCount, DungeonLevelData levelData)
        {
            int numberOfEntities = Random.Range(0, maxCount + 1);

            for (int i = 0; i < numberOfEntities; i++)
            {
                int x = Random.Range(rect.xMin + 1, rect.xMax);
                int y = Random.Range(rect.yMin + 1, rect.yMax);

                bool validPos = true;

                foreach (var entity in levelData.entities)
                {
                    if (entity.x == x && entity.y == y)
                    {
                        validPos = false;
                        break;
                    }
                }

                if (validPos)
                {
                    var prefab = entities[Random.Range(0, entities.Length)];

                    levelData.entities.Add(new DungeonEntityData() {x = x, y = y, prefab = prefab });
                }
            }
        }

        private static void CreateHorizontalTunnel(int x1, int x2, int y, DungeonLevelData levelData)
        {
            for (int x = Mathf.Min(x1, x2); x <= Mathf.Max(x1, x2); x++)
            {
                levelData.floor[x, y] = true;
            }
        }

        private static void CreateVerticalTunnel(int y1, int y2, int x, DungeonLevelData levelData)
        {
            for (int y = Mathf.Min(y1, y2); y <= Mathf.Max(y1, y2); y++)
            {
                levelData.floor[x, y] = true;
            }
        }
    }
}
