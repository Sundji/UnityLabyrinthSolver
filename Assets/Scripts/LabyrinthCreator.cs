using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    private const char _PASSAGE_CHARACTER = '0';

    [SerializeField] private Transform _tileContainer = null;
    [SerializeField] private Tile _tilePrefab = null;

    private List<Tile> _tiles = new List<Tile>();

    public void CreateLabyrinthFromFile(string fileName)
    {
        ResetLabyrinth(shouldClear: true);

        TextAsset labyrinthFile = Resources.Load(fileName) as TextAsset;
        if (labyrinthFile == null)
        {
            Debug.LogError("There is no file under file name " + fileName + "!");
            return;
        }

        int counter = 0;
        int tileCount = _tiles.Count;
        Vector2 currentPosition = Vector2.zero;
        foreach (string line in labyrinthFile.text.Split("\n"))
        {
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
                counter++;
                currentPosition.x++;
            }
            currentPosition.x = 0;
            currentPosition.y--;
        }

        for (; counter < tileCount; counter++) _tiles[counter].gameObject.SetActive(false);
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
