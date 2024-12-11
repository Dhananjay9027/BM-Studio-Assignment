using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, AI
{
    [SerializeField] private Path pathfinding;
    [SerializeField] private Transform player;
    private Vector2Int currentGridPosition;
    public Vector2Int CurrentPosition => currentGridPosition;
    private bool isMoving = false;
    [SerializeField] private float tileSpacing;
    [SerializeField] private float moveSpeed;

    public void Initialize(Vector2Int startPosition)
    {
        currentGridPosition = startPosition;
        transform.position = new Vector3(startPosition.x*tileSpacing, 1.2f, startPosition.y*tileSpacing);
    }

    public void MoveTowardsTarget(Vector2Int target)
    {
        if (isMoving) return; 

        Vector2Int enemyPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / tileSpacing),
            Mathf.RoundToInt(transform.position.z / tileSpacing)
        );
        Vector2Int playerPosition= new Vector2Int(Mathf.RoundToInt(player.position.x / tileSpacing), Mathf.RoundToInt(player.position.z / tileSpacing));
        List<Vector2Int> path = pathfinding.FindPath(enemyPos, target,playerPosition);
        if (path != null)
        {
            //Debug.Log(target);
            StartCoroutine(MoveAlongPath(path));
        }
    }
    private IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        isMoving = true;

        foreach (Vector2Int step in path)
        {
            Vector3 targetPosition = new Vector3(step.x * tileSpacing, 1.2f, step.y * tileSpacing);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition,moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentGridPosition = step; // update the grid position after reaching the tile
        }

        isMoving = false;
    }
}
