using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExampleClass
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}

// Float 나 Int값을 연결 시켜주는 컨테이너 박스를 생성
// editor에서 컨테이너 박스의 값을 조절하면 해당 컨테이너가 원하는 값을 전달해주는 방식

public class TestEditor : EditorWindow
{
    private List<ExampleClass> examples = new List<ExampleClass>();
    private ExampleClass selectedExampleClass;

    [MenuItem("Window/TestEdit")]
    static void Open()
    {
        GetWindow<TestEditor>();
    }

    int selected;

    int buyCost = GameManager.instance.rouletteCost;

    int costIncrement;

    int selectJellyNum;
    int selectJellyLevel;

    string insertValue;

    Transform[] Container;


    private void OnEnable()
    {
        UpdateEditorString();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10);

        GUILayout.Toolbar(selected, new string[] { "VIEW" });

        EditorGUILayout.Space(10);

        costIncrement = EditorGUILayout.IntSlider(costIncrement,0,90);

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Purchase Cost");

        EditorGUILayout.LabelField(buyCost.ToString());

        EditorGUILayout.EndHorizontal();


        if(GUILayout.Button("Purchase Cost Up"))
        {
            buyCost += costIncrement;
            GameManager.instance.rouletteCost = buyCost;
        }

        if(GUILayout.Button("Purchase Cost Down"))
        {
            buyCost -= costIncrement;
            GameManager.instance.rouletteCost = buyCost;
        }

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("젤리 선택 넘버");

        selectJellyNum = EditorGUILayout.IntField(selectJellyNum);

        EditorGUILayout.EndHorizontal();

        //

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("젤리 레벨 : 1부터 3까지 지정할 것");

        selectJellyLevel = EditorGUILayout.IntField(selectJellyLevel);

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("젤리 생성"))
        {
            if (selectJellyNum < 0) return;
            if (selectJellyNum > 11) return;

            if (selectJellyLevel <= 0) return;
            if (selectJellyLevel > 3) return;

            ButtonCall.instance.CallEventMethodByIndex(1);

            UpgradePanelView.editJellyTarget(selectJellyNum,selectJellyLevel);
            UpgradePanelView.target();
        }
    }

    private void UpdateEditorString()
    {
        buyCost = 150;
    }
}
