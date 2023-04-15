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

// Float �� Int���� ���� �����ִ� �����̳� �ڽ��� ����
// editor���� �����̳� �ڽ��� ���� �����ϸ� �ش� �����̳ʰ� ���ϴ� ���� �������ִ� ���

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

        EditorGUILayout.LabelField("���� ���� �ѹ�");

        selectJellyNum = EditorGUILayout.IntField(selectJellyNum);

        EditorGUILayout.EndHorizontal();

        //

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("���� ���� : 1���� 3���� ������ ��");

        selectJellyLevel = EditorGUILayout.IntField(selectJellyLevel);

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("���� ����"))
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
