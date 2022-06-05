using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static readonly string PASSAGE_TAG = "Passage";
    public static readonly string WALL_TAG = "Wall";

    private static readonly Color _PASSAGE_SELECTED_COLOR = Color.blue;
    private static readonly Color _PASSAGE_MARKED_COLOR = Color.cyan;
    private static readonly Color _PASSAGE_UNMARKED_COLOR = Color.white;
    private static readonly Color _WALL_COLOR = Color.gray;

    public static Action<Vector2> OnTileSelectedAction = null;
    public static Action<Vector2> OnTileMarkedAction = null;

    private SpriteRenderer _spriteRenderer = null;
    private TileType _tileType = TileType.PASSAGE;

    private Vector2 Position => transform.position;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        OnTileSelectedAction += OnTileSelected;
        OnTileMarkedAction += OnTileMarked;
    }

    private void OnDestroy()
    {
        OnTileSelectedAction -= OnTileSelected;
        OnTileMarkedAction -= OnTileMarked;
    }

    private void OnMouseDown()
    {
        if (_tileType == TileType.PASSAGE) OnTileSelectedAction?.Invoke(Position);
    }

    private void OnTileSelected(Vector2 tilePosition)
    {
        if (_tileType == TileType.PASSAGE) _spriteRenderer.color = Position == tilePosition ? _PASSAGE_SELECTED_COLOR : _PASSAGE_UNMARKED_COLOR;
    }

    private void OnTileMarked(Vector2 tilePosition)
    {
        if (Position == tilePosition && _spriteRenderer.color != _PASSAGE_SELECTED_COLOR) _spriteRenderer.color = _PASSAGE_MARKED_COLOR;
    }

    public void InitializeTile(TileType tileType)
    {
        _tileType = tileType;
        transform.tag = tileType == TileType.PASSAGE ? PASSAGE_TAG : WALL_TAG;
        ResetTile();
    }

    public void ResetTile()
    {
        _spriteRenderer.color = _tileType == TileType.PASSAGE ? _PASSAGE_UNMARKED_COLOR : _WALL_COLOR;
    }
}
