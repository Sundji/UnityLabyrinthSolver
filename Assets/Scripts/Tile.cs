using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static readonly string PASSAGE_TAG = "Passage";
    public static readonly string WALL_TAG = "Wall";

    private static readonly Color _PASSAGE_MARKED_COLOR = Color.white;
    private static readonly Color _PASSAGE_UNMARKED_COLOR = Color.gray;
    private static readonly Color _WALL_COLOR = Color.black;

    public static Action<Vector2> OnTileSelectedAction = null;
    public static Action<Vector2> OnTileMarkedAction = null;

    [SerializeField] private GameObject _selectionIndicator = null;

    private SpriteRenderer _spriteRenderer = null;

    private TileType _tileType = TileType.PASSAGE;

    private Vector2 Position => transform.position;

    private void Awake()
    {
        _selectionIndicator.SetActive(false);
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
        _selectionIndicator.SetActive(Position == tilePosition);
        if (_tileType == TileType.PASSAGE) _spriteRenderer.color = _PASSAGE_UNMARKED_COLOR;
    }

    private void OnTileMarked(Vector2 tilePosition)
    {
        if (Position == tilePosition) _spriteRenderer.color = _PASSAGE_MARKED_COLOR;
    }

    public void InitializeTile(TileType tileType)
    {
        _tileType = tileType;
        transform.tag = tileType == TileType.PASSAGE ? PASSAGE_TAG : WALL_TAG;
        ResetTile();
    }

    public void ResetTile()
    {
        _selectionIndicator.SetActive(false);
        _spriteRenderer.color = _tileType == TileType.PASSAGE ? _PASSAGE_UNMARKED_COLOR : _WALL_COLOR;
    }
}
