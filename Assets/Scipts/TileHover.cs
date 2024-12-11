using UnityEngine;
using UnityEngine.UI;

public class TileHover : MonoBehaviour
{
    public Text infoText; 

    void Update()
    {
        DetectTile();
    }

    void DetectTile()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
       // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                infoText.text = $"Tile: ({tileInfo.gridPosition.x}, {tileInfo.gridPosition.y})";
                return;
            }
        }

        infoText.text = ""; 
    }
}
