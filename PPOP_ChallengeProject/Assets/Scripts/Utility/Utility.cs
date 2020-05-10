using UnityEngine;

public static class Utility
{
    public struct Constants
    {
        public struct Layers
        {
            public const int Tile = 8;
        }

        public struct MaterialReferences
        {
            public const string MainTexture = "_MainTex";
            public const string AlbedoColor = "_Color";
        }
    }


    /// <summary>
    /// Returns the Pythagorean distance between 2 points on the same plane.
    /// </summary>
    public static float PythagoreanDistance(Vector2 origin, Vector2 destination)
    {
        Vector3 h = destination - origin;

        return h.x * h.x + h.y * h.y;
    }

    /// <summary>
    /// Returns the Manhattan distance between 2 grid coordinates. Space position won't work
    /// </summary>
    public static float ManhattanDistance(Vector2 origin, Vector2 destination)
    {
        Vector3 h = destination - origin;

        return Mathf.Abs(h.x) + Mathf.Abs(h.y);
    }
}