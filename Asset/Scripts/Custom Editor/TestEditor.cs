using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditor : EditorWindow
{
    [MenuItem("Window/TestEdit")]
    static void Open()
    {
        GetWindow<TestEditor>();
    }

    int selected;

    Object selectedObject = null;

    string buyCost;

    private void OnEnable()
    {
        UpdateEditorString();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10);

        GUILayout.Toolbar(selected, new string[] { "VIEW" });

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Purchase Cost");

        EditorGUILayout.TextArea(buyCost);

        EditorGUILayout.EndHorizontal();

        selectedObject = EditorGUILayout.ObjectField("General Value", selectedObject, typeof(GameManager), true);
    }

    private void UpdateEditorString()
    {
        buyCost = "150";
    }
}
