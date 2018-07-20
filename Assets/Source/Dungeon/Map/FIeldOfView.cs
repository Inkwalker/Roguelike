using UnityEngine;

namespace Roguelike.Dungeon
{
    public class FieldOfView
    {
        private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

        /// <summary>
        /// The plot function delegate
        /// </summary>
        /// <param name="x">The x co-ord being plotted</param>
        /// <param name="y">The y co-ord being plotted</param>
        /// <returns>True to continue, false to stop the algorithm</returns>
        private delegate bool PlotFunction(int x, int y);

        /// <summary>
        /// Plot the line from (x0, y0) to (x1, y1)
        /// </summary>
        /// <param name="x0">The start x</param>
        /// <param name="y0">The start y</param>
        /// <param name="x1">The end x</param>
        /// <param name="y1">The end y</param>
        /// <param name="plot">The plotting function (if this returns false, the algorithm stops early)</param>
        private static void Line(int x0, int y0, int x1, int y1, PlotFunction plot)
        {
            int w = x1 - x0;
            int h = y1 - y0;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Mathf.Abs(w);
            int shortest = Mathf.Abs(h);
            if (!(longest > shortest))
            {
                longest = Mathf.Abs(h);
                shortest = Mathf.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if (plot(x0, y0) == false) return;

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x0 += dx1;
                    y0 += dy1;
                }
                else
                {
                    x0 += dx2;
                    y0 += dy2;
                }
            }
        }

        public static bool IsVisible(GameMap map, Vector2Int from, Vector2Int to)
        {
            bool result = true;

            Line(from.x, from.y, to.x, to.y, (x, y) =>
            {
                var tile = map.GetTile(new Vector2Int(x, y));

                result = !tile.BlockSight;

                return tile.BlockSight == false;
            });

            return result;
        }

        public static void UpdateVisibilityMap(GameMap map, VisibilityMap visibility, Vector2Int pos, int distance)
        {
            var p0 = new Vector2Int(pos.x - distance, pos.y - distance);
            var p1 = new Vector2Int(pos.x + distance, pos.y + distance);

            for (int x = p0.x; x <= p1.x; x++)
            {
                for (int y = p0.y; y <= p1.y; y++)
                {
                    //for every tile on the perimeter
                    if ((x == p0.x || x == p1.x) || (y == p0.y || y == p1.y))
                    {
                        Line(pos.x, pos.y, x, y, (pX, pY) =>
                        {
                            if (pX < 0 || pX >= map.Width || pY < 0 || pY > map.Height) return false;

                            visibility.Set(pX, pY, VisibilityMap.VisibilityState.Visible);

                            var tile = map.GetTile(pX, pY);
                            return !tile.BlockSight;
                        });
                    }               
                }
            }
        }
    }
}
