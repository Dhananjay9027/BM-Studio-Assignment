using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private Transform Spaw_location; 
    public Vector2Int playerPosition = new Vector2Int(0, 0); //starting position on the grid
    [SerializeField] private float tileSpacing;
    [SerializeField] private GameManager GameManager;
    public Path Path;
    [SerializeField] private float moveSpeed;
    private bool inputEnabled = true;
    void Start()
    {
        PlacePlayer(playerPosition);
        Path.InitializeGrid();
    }

    void Update()
    {
      //  Debug.Log(inputEnabled);
        if (inputEnabled)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
                    if (tileInfo != null)
                    {
                        
                        Vector2Int targetPosition = tileInfo.gridPosition;

                        MovePlayer(targetPosition);
                    }
                }              
            }
        }
       
    }

    void PlacePlayer(Vector2Int position)
    {
        transform.position = new Vector3(position.x * tileSpacing, 1.2f, position.y * tileSpacing);
    }

    IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        foreach (Vector2Int step in path)
        {
            Vector3 targetPosition = new Vector3(step.x * tileSpacing, 1.2f, step.y * tileSpacing);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        inputEnabled = true; 
      //  Debug.Log("---");
        GameManager.FollowPlayer();
    }

    void MovePlayer(Vector2Int target)
    {
        if (target == null)
        {
            inputEnabled = true;
            return;  
        }
        inputEnabled = false;
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(transform.position.x/tileSpacing), Mathf.RoundToInt(transform.position.z/tileSpacing));
        List<Vector2Int> path = Path.FindPath(playerPos, target);
        if (path != null)
        {
           // Debug.Log(playerPos);
          //  Debug.Log(target);
            StartCoroutine(MoveAlongPath(path));
        }
        else
        {
            inputEnabled = true;
        }
    }
}
