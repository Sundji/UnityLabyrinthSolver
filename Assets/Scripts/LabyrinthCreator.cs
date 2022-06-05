using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    private const char _PASSAGE_CHARACTER = '0';

    [SerializeField] private Transform _tileContainer = null;
    [SerializeField] private Tile _tilePrefab = null;

    private List<Tile> _tiles = new List<Tile>();
    private Camera _mainCamera = null;

    private Camera MainCamera
    {
        get
        {
            if (_mainCamera == null) _mainCamera = Camera.main;
            return _mainCamera;
        }
    }

    private void AdjustCamera(int columnCount, int rowCount)
    {
        MainCamera.transform.position = new Vector3(columnCount / 2.0f, -rowCount / 2.0f, MainCamera.transform.position.z);
        MainCamera.orthographicSize = columnCount > rowCount ? columnCount / 2.0f : rowCount * Screen.width / Screen.height / 2.0f;
    }

    public void CreateLabyrinthFromFile(string fileName)
    {
        ResetLabyrinth(shouldClear: true);

        TextAsset labyrinthFile = Resources.Load(fileName) as TextAsset;

        int columnCount = 0;
        int rowCount = 0;

        int counter = 0;
        int tileCount = _tiles.Count;
        Vector2 currentPosition = Vector2.zero;
        foreach (string line in labyrinthFile.text.Split("\n"))
        {
            columnCount = 0;
            foreach (char character in line.Trim())
            {
                Tile tile = null;
                if (counter < tileCount)
                {
                    tile = _tiles[counter];
                    tile.gameObject.SetActive(true);
                    tile.transform.position = currentPosition;
                }
                else
                {
                    tile = Instantiate(_tilePrefab, currentPosition, Quaternion.identity, _tileContainer);
                    _tiles.Add(tile);
                }
                tile.InitializeTile(character == _PASSAGE_CHARACTER ? TileType.PASSAGE : TileType.WALL);
                columnCount++;
                counter++;
                currentPosition.x++;
            }
            currentPosition.x = 0;
            currentPosition.y--;
            rowCount++;
        }

        for (; counter < tileCount; counter++) _tiles[counter].gameObject.SetActive(false);
        AdjustCamera(columnCount, rowCount);
    }

    public void ResetLabyrinth(bool shouldClear)
    {
        foreach (Tile tile in _tiles)
        {
            tile.ResetTile();
            if (shouldClear) tile.gameObject.SetActive(false);
        }
    }
}
