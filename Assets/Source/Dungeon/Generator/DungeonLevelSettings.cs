using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Dungeon.Generator
{
    [CreateAssetMenu(menuName = "Dungeon/Dungeon Level Settings")]
    public class DungeonLevelSettings : ScriptableObject
    {
        public Vector2Int size;

        public int maxRoomSize = 10;
        public int minRoomSize = 6;
        public int maxRooms = 30;

        public int maxEntitiesPerRoom = 5;

        public Entity player;
        public Entity[] entities;
    }
}
