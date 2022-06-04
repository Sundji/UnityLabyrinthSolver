using System.Collections.Generic;
using UnityEngine;

public class SolutionEntry
{
    public Vector2 TilePosition { get; private set; } = Vector2.zero;
    public HashSet<Direction> UsedDirections { get; } = new HashSet<Direction>();

    public SolutionEntry(Vector2 tilePosition)
    {
        TilePosition = tilePosition;
    }

    public override bool Equals(object other)
    {
        if (other is not SolutionEntry otherEntry) return false;
        return TilePosition == otherEntry.TilePosition;
    }

    public override int GetHashCode()
    {
        return TilePosition.GetHashCode();
    }
}
