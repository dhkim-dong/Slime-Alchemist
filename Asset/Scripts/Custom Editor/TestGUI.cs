using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

public class TestGUI : EditorWindow
{
    [MenuItem("Window/Example")]
    static void Open()
    {
        GetWindow<TestGUI>();
    }

    bool toggleValue;

    private void OnGUI()
    {
        Display();

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);

        Display();

        EditorGUI.EndDisabledGroup();

        bool on = animFloat.value == 1;

        if(GUILayout.Button(on ? "Close" : "Open", GUILayout.Width(64)))
        {
            animFloat.target = on ? 0.0001f : 1;
            animFloat.speed = 0.5f;

            var env = new UnityEvent();
            env.AddListener(() => Repaint());
            animFloat.valueChanged = env;
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginFadeGroup(animFloat.value);
        Display();
        EditorGUILayout.EndFadeGroup();
        Display();
        EditorGUILayout.EndHorizontal();
    }

    void Display()
    {
        EditorGUILayout.ToggleLeft("Toggle", false);
        EditorGUILayout.IntSlider(0, 10, 0);
        GUILayout.Button("Button");
    }

    AnimFloat animFloat = new AnimFloat(0.001f);
    Texture tex;
}
