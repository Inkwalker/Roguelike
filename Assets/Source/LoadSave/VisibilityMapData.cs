using UnityEngine;

namespace Roguelike.LoadSave
{
    [System.Serializable]
    public class VisibilityMapData
    {
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] int[] exploredTiles;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public VisibilityMapData(int width, int height)
        {
            this.width = width;
            this.height = height;

            exploredTiles = new int[width * height];
        }

        public void Set(int x, int y, bool explored)
        {
            int index = x + y * width;
            exploredTiles[index] = explored ? 1 : 0;
        }

        public bool Get(int x, int y)
        {
            int index = x + y * width;
            return exploredTiles[index] == 1 ? true : false;
        }
    }
}
