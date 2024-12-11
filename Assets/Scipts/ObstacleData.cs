using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Grid/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    [Tooltip("10x10 grid to store obstacle information")]
    public bool[] obstacles = new bool[100]; // array to store obstacle info (10x10 grid)
}
