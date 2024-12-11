using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrigManager : MonoBehaviour
{
    public GameObject tilePrefab; 
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float tileSpacing = 1.1f; 

    void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
               
                Vector3 position = new Vector3(x * tileSpacing, 0, y * tileSpacing);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);

                tile.name = $"Tile ({x}, {y})";

                // attach tyileinfo script 
                TileInfo tileInfo = tile.AddComponent<TileInfo>();
                tileInfo.gridPosition = new Vector2Int(x, y);
            }
        }
    }
}
