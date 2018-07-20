using UnityEngine;

namespace Roguelike.Dungeon
{
    public class VisibilityMap
    {
        private const int PixelsPerTile = 2;

        private VisibilityState[,] data;
        private Color exploredMapColor;
        public Texture2D texturePrev;
        public Texture2D textureCur;

        public VisibilityMap(int width, int height)
        {
            data = new VisibilityState[width, height];

            exploredMapColor = new Color(0.4f, 0.4f, 0.4f, 1);

            int tWidth = width * PixelsPerTile;
            int tHeight = height * PixelsPerTile;

            textureCur = new Texture2D(tWidth, tHeight, TextureFormat.RGBA32, false);
            textureCur.filterMode = FilterMode.Trilinear;

            texturePrev = new Texture2D(tWidth, tHeight, TextureFormat.RGBA32, false);
            texturePrev.filterMode = FilterMode.Trilinear;

            Debug.Log("Field of View texture size: " + tWidth);

            Clear();
        }

        public void Set(int x, int y, VisibilityState state)
        {
            data[x, y] = state;

            Color color = Color.black;
            switch (state)
            {
                case VisibilityState.Explored:
                    color = exploredMapColor;
                    break;
                case VisibilityState.Invisible:
                    color = new Color(0, 0, 0);
                    break;
                case VisibilityState.Visible:
                    color = new Color(1, 1, 1);
                    break;
            }

            for (int pX = x * PixelsPerTile; pX < x * PixelsPerTile + PixelsPerTile; pX++)
            {
                for (int pY = y * PixelsPerTile; pY < y * PixelsPerTile + PixelsPerTile; pY++)
                {
                    textureCur.SetPixel(pX, pY, color);
                }
            }

            textureCur.Apply();
        }

        public void Set(Vector2Int pos, VisibilityState state)
        {
            Set(pos.x, pos.y, state);
        }

        public VisibilityState Get(int x, int y)
        {
            return data[x, y];
        }

        public VisibilityState Get(Vector2Int pos)
        {
            return Get(pos.x, pos.y);
        }

        /// <summary>
        /// Sets all visible cells to Explored
        /// </summary>
        public void Reset()
        {
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (data[x,y] == VisibilityState.Visible)
                    {
                        Set(x, y, VisibilityState.Explored);
                    }
                }
            }
        }

        /// <summary>
        /// Sets all cells to Invisible
        /// </summary>
        public void Clear()
        {
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    Set(x, y, VisibilityState.Invisible);
                }
            }
        }

        public void CopyToTexturePrev()
        {
            texturePrev.SetPixels(textureCur.GetPixels());
            texturePrev.Apply();
        }

        public enum VisibilityState
        {
            Invisible,
            Visible,
            Explored,
        }
    }
}
