using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Path : MonoBehaviour
{
    bool[,] grid = new bool[10, 10];
    [SerializeField] private ObstacleData obstacleData;

    public void InitializeGrid()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x; 
                grid[x, y] = !obstacleData.obstacles[index]; 
            }
        }
        Debug.Log("grid iniliazed");
    }

    public  bool IsWalkable(Vector2Int position, Vector2Int? restrictedPosition = null)
    {
        return position.x >= 0 && position.x < 10 &&
               position.y >= 0 && position.y < 10 &&
               grid[position.x, position.y] &&
               (restrictedPosition == null || position != restrictedPosition); 
    }
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, Vector2Int restrictedPosition)
    {
        if(IsWalkable(target,restrictedPosition)==false)
        {
            return null;
        }

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        Node startNode = new Node(start, null);
        Node targetNode = new Node(target, null);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList.OrderBy(n => n.fCost).First();
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.position == target)
            {
                return ConstructPath(currentNode);
            }

            foreach (Vector2Int neighbor in GetNeighbors(currentNode.position))
            {
                if (!IsWalkable(neighbor, restrictedPosition) || closedList.Any(n => n.position == neighbor))
                    continue;

                int newMovementCost = currentNode.gCost + 1;
                Node neighborNode = new Node(neighbor, currentNode)
                {
                    gCost = newMovementCost,
                    hCost = GetHeuristic(neighbor, target),
                    fCost = newMovementCost + GetHeuristic(neighbor, target)
                };

                if (!openList.Any(n => n.position == neighbor))
                {
                    openList.Add(neighborNode);
                }
            }
        }

        return null;
    }

    List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        return new List<Vector2Int>
    {
        new Vector2Int(position.x + 1, position.y),  
        new Vector2Int(position.x - 1, position.y),  
        new Vector2Int(position.x, position.y + 1),  
        new Vector2Int(position.x, position.y - 1)  
    }.Where(p => IsWithinBounds(p)).ToList();
    }

    int GetHeuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < 10 && position.y >= 0 && position.y < 10;
    }
    List<Vector2Int> ConstructPath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse(); 
        return path;
    }





}
