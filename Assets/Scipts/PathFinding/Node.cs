using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Vector2Int position;
    public Node parent;
    public int gCost, hCost, fCost;

    public Node(Vector2Int pos, Node parentNode)
    {
        position = pos;
        parent = parentNode;
    }
}
