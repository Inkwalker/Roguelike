using UnityEngine;

namespace Roguelike.LoadSave
{
    [System.Serializable]
    public class GameMapData
    {
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] int[] tiles;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public GameMapData(int width, int height)
        {
            this.width = width;
            this.height = height;

            tiles = new int[width * height];
        }

        public void Set(int x, int y, int tile)
        {
            int index = x + y * width;
            tiles[index] = tile;
        }

        public int Get(int x, int y)
        {
            int index = x + y * width;
            return tiles[index];
        }
    }
}
