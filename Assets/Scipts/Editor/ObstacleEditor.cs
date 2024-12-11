using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    private ObstacleData obstacleData;

    private void OnEnable()
    {
        obstacleData = (ObstacleData)target;
    }

    public override void OnInspectorGUI()
    {
        if (obstacleData == null) return;

        EditorGUILayout.LabelField("Obstacle Grid Editor", EditorStyles.boldLabel);

        // a  grid of buttons
        for (int y = 0; y < 10; y++) 
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++) 
            {
                int index = y * 10 + x;
                obstacleData.obstacles[index] = EditorGUILayout.Toggle(obstacleData.obstacles[index], GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Data"))
        {
            EditorUtility.SetDirty(obstacleData);
            AssetDatabase.SaveAssets();
            Debug.Log("Obstacle data saved!");
        }
    }
}
