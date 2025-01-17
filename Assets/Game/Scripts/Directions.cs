using System;
using UnityEngine;

public enum Directions : int
{
    None = -1,
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}

public static class DirectionsExtensions
{
    
    public static Vector2 ToVector2(this Directions dir)
    {
        switch (dir)
        {
            case Directions.None:
                return Vector2.zero;
            case Directions.Up:
                return Vector2.up;
            case Directions.Down:
                return Vector2.down;
            case Directions.Left:
                return Vector2.left;
            case Directions.Right:
                return Vector2.down;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
        }
    }

    public static Directions ToDirection(this Vector2 dir)
    {
        if (dir.IsNearZero())
        {
            return Directions.None;
        }
        
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? Directions.Right : Directions.Left;
        }
        
        return dir.y > 0 ? Directions.Up : Directions.Down;
    }
    
    public static Directions ToDirection(this Vector3 dir)
    {
        if (dir.IsNearZero())
        {
            return Directions.None;
        }
        
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? Directions.Right : Directions.Left;
        }
        
        return dir.y > 0 ? Directions.Up : Directions.Down;
    }

    public static bool IsNearZero(this Vector2 dir) => Mathf.Approximately(dir.x, 0) && Mathf.Approximately(dir.y, 0);
    
    public static bool IsNearZero(this Vector3 dir) => Mathf.Approximately(dir.x, 0) && Mathf.Approximately(dir.y, 0);
    
}
