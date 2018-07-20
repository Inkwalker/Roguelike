using UnityEngine;

namespace Roguelike.Dungeon
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] bool blocked;
        [SerializeField] bool blockSight;

        public bool Blocked { get { return blocked; } set { blocked = value; } }
        public bool BlockSight { get { return blockSight; } set { blockSight = value; } }

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int(
                Mathf.RoundToInt((transform.localPosition.x - GameMap.TileSize / 2f) / GameMap.TileSize),
                Mathf.RoundToInt((transform.localPosition.z - GameMap.TileSize / 2f) / GameMap.TileSize ));
            }
        }
    }
}
