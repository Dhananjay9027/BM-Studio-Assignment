using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] private float tileSpacing;
    public ObstacleData obstacleData; // SO
    public GameObject obstaclePrefab; 
    private GameObject[,] obstacleInstances = new GameObject[10, 10]; 
    private void Start()
    {
        GenerateObstacles();
    }
    public void GenerateObstacles()
    {
        // remove existing obstacle
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (obstacleInstances[x, y] != null)
                {
                    Destroy(obstacleInstances[x, y]);
                }
            }
        }

        //new obstacles 
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                if (obstacleData.obstacles[index])
                {
                    Vector3 position = new Vector3(x * tileSpacing, 0.5f, y * tileSpacing);
                    obstacleInstances[x, y] = Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
