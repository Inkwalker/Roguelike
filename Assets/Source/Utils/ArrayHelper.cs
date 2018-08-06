namespace Roguelike.Utils
{
    public static class ArrayHelper
    {
        public static bool InBounds<T>(T[] array, int index)
        {
            return index > 0 && index < array.Length;
        }

        public static bool InBounds<T>(T[,] array, int x, int y)
        {
            return x > 0 && y > 0 && x < array.GetLength(0) && y < array.GetLength(1);
        }
    }
}
