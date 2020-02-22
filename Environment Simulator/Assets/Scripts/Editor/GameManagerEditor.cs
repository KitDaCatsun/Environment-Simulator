using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerationManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GenerationManager gameManager = (GenerationManager)target;

        if (GUILayout.Button("Generate All")) {
            gameManager.GenerateWorld();
            gameManager.GenerateSun();
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate World")) {
            gameManager.GenerateWorld();
        }

        if (GUILayout.Button("Generate Sun")) {
            gameManager.GenerateSun();
        }

        GUILayout.EndHorizontal();
    }
}
