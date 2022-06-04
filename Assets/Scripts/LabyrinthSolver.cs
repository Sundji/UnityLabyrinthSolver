using System.Collections.Generic;
using UnityEngine;

public class LabyrinthSolver : MonoBehaviour
{
    private const float _COLLISION_RADIUS = 0.1f;
    private static readonly HashSet<Direction> _DIRECTIONS = new HashSet<Direction>() { Direction.LEFT, Direction.UP, Direction.RIGHT, Direction.DOWN };

    private static readonly Dictionary<Direction, Vector2> _DIRECTION_OFFSETS = new Dictionary<Direction, Vector2>()
    {
        { Direction.LEFT, Vector2.left },
        { Direction.UP,  Vector2.up },
        { Direction.RIGHT, Vector2.right },
        { Direction.DOWN, Vector2.down }
    };

    private bool _isTileSelected = false;
    private Vector2 _selectedTilePosition = Vector2.zero;

    private bool _exitFound = false;
    private HashSet<Vector2> _includedTilePositions = new HashSet<Vector2>();
    private Stack<SolutionEntry> _solutionEntries = new Stack<SolutionEntry>();

    private void Awake()
    {
        Tile.OnTileSelectedAction += OnTileSelected;
    }

    private void OnDestroy()
    {
        Tile.OnTileSelectedAction -= OnTileSelected;
    }

    private void OnTileSelected(Vector2 tilePosition)
    {
        _isTileSelected = true;
        _selectedTilePosition = tilePosition;
    }

    private bool CheckIfTileReachable(Vector2 tilePosition)
    {
        Collider2D collider = Physics2D.OverlapCircle(tilePosition, _COLLISION_RADIUS);

        if (collider != null) return collider.CompareTag(Tile.PASSAGE_TAG);

        _exitFound = true;
        return true;
    }

    private Direction FindNewDirection(SolutionEntry entry)
    {
        foreach (Direction direction in _DIRECTIONS)
        {
            if (entry.UsedDirections.Contains(direction)) continue;
            if (CheckIfTileReachable(entry.TilePosition + _DIRECTION_OFFSETS[direction])) return direction;
        }
        return Direction.DIRECTIONLESS;
    }

    public void SolveLabyrinth()
    {
        if (!_isTileSelected)
        {
            Debug.LogError("No tile selected!");
            return;
        }

        _isTileSelected = false;

        _exitFound = false;
        _includedTilePositions.Clear();
        _solutionEntries.Clear();

        Vector2 currentPosition = _selectedTilePosition;
        Direction currentDirection = Direction.LEFT;

        while (true)
        {
            SolutionEntry entry;

            if (_includedTilePositions.Contains(currentPosition))
            {
                SolutionEntry entryToCheck = _solutionEntries.Peek();
                currentDirection = FindNewDirection(entryToCheck);

                while (currentDirection == Direction.DIRECTIONLESS)
                {
                    _includedTilePositions.Remove(_solutionEntries.Pop().TilePosition);
                    if (_solutionEntries.Count == 0)
                    {
                        Debug.LogError("Cannot find a solution...");
                        return;
                    }
                    entryToCheck = _solutionEntries.Peek();
                    currentDirection = FindNewDirection(entryToCheck);
                }
                entry = entryToCheck;
            }
            else
            {
                entry = new SolutionEntry(currentPosition);
                _includedTilePositions.Add(currentPosition);
                _solutionEntries.Push(entry);

                if (!CheckIfTileReachable(currentPosition + _DIRECTION_OFFSETS[currentDirection])) currentDirection = FindNewDirection(entry);
                if (currentDirection == Direction.DIRECTIONLESS)
                {
                    Debug.LogError("Cannot find a solution...");
                    return;
                }
            }

            if (_exitFound) break;
            currentPosition = entry.TilePosition + _DIRECTION_OFFSETS[currentDirection];
            entry.UsedDirections.Add(currentDirection);
        }

        Debug.LogError("Exit found!");
        foreach (SolutionEntry entry in _solutionEntries) Tile.OnTileMarkedAction?.Invoke(entry.TilePosition);
    }
}
