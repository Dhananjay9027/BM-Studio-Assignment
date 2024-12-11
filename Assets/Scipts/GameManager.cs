
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Path pathfinding;
    [SerializeField] private float tileSpacing;

    private Vector2Int playerGridPosition;

    private void Start()
    {
        Vector2Int enemyStartPosition = new Vector2Int(0, 0); 
        enemyAI.Initialize(enemyStartPosition);
    }

    public void FollowPlayer()
    {
        Vector2Int currentPlayerPosition = new Vector2Int(Mathf.RoundToInt(playerTransform.position.x / tileSpacing), Mathf.RoundToInt(playerTransform.position.z / tileSpacing));

        if (playerGridPosition != currentPlayerPosition) // player has moved
        {
            playerGridPosition = currentPlayerPosition;

            
            List<Vector2Int> adjacentTiles = GetAdjacentTiles(playerGridPosition);
            List<Vector2Int> walkableTiles = adjacentTiles.FindAll(tile => pathfinding.IsWalkable(tile));
            Vector2Int targetTile = GetClosestTile(walkableTiles, enemyAI.CurrentPosition);
           
             Debug.Log(targetTile);
           
            enemyAI.MoveTowardsTarget(targetTile);
        }
    }

    private List<Vector2Int> GetAdjacentTiles(Vector2Int position)
    {
        List<Vector2Int> adjacentTiles = new List<Vector2Int>
    {
        new Vector2Int(position.x + 1, position.y),
        new Vector2Int(position.x - 1, position.y),
        new Vector2Int(position.x, position.y + 1),
        new Vector2Int(position.x, position.y - 1)
    };

        // filter out of bounfd tiles
        return adjacentTiles.FindAll(tile =>
            tile.x >= 0 && tile.x < 10* tileSpacing && 
            tile.y >= 0 && tile.y < 10* tileSpacing    
        );
    }

    private Vector2Int GetClosestTile(List<Vector2Int> tiles, Vector2Int enemyPosition)
    {
        Vector2Int closestTile = tiles[0];
        float minDistance = float.MaxValue;

        foreach (var tile in tiles)
        {
            float distance = Vector2.Distance(new Vector2(tile.x, tile.y), new Vector2(enemyPosition.x, enemyPosition.y));
            if (distance < minDistance)
            {
                closestTile = tile;
                minDistance = distance;
            }
        }

        return closestTile;
    }
}
