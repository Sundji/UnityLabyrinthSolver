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
    public static Action<Vector2> OnTileUnmarkedAction = null;

    [SerializeField] private GameObject _selectionIndicator = null;

    private SpriteRenderer _spriteRenderer = null;

    private Vector2 _position = Vector2.zero;
    private TileType _tileType = TileType.PASSAGE;

    private void Awake()
    {
        _position = transform.position;
        _selectionIndicator.SetActive(false);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        OnTileSelectedAction += OnTileSelected;
        OnTileMarkedAction += OnTileMarked;
        OnTileUnmarkedAction += OnTileUnmarked;
    }

    private void OnDestroy()
    {
        OnTileSelectedAction -= OnTileSelected;
        OnTileMarkedAction -= OnTileMarked;
        OnTileUnmarkedAction -= OnTileUnmarked;
    }

    private void OnMouseDown()
    {
        if (_tileType == TileType.PASSAGE) OnTileSelectedAction?.Invoke(_position);
    }

    private void OnTileSelected(Vector2 tilePosition)
    {
        _selectionIndicator.SetActive(_position == tilePosition);
    }

    private void OnTileMarked(Vector2 tilePosition)
    {
        if (_position == tilePosition) _spriteRenderer.color = _PASSAGE_MARKED_COLOR;
    }

    private void OnTileUnmarked(Vector2 tilePosition)
    {
        if (_position == tilePosition) _spriteRenderer.color = _PASSAGE_UNMARKED_COLOR;
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
