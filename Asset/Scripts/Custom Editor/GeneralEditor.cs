using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
using System;
using System.Collections.Generic;

public class GeneralEditor : EditorWindow
{
	[MenuItem("Window/GeneralEditor")]
	static void Open( )
	{
		GetWindow<GeneralEditor>( );
	}

	int value;

	int costIncrement;

    void OnGUI ()
	{
		if (EditorApplication.isPlaying)
		{
            GUILayout.Label("Value: " + DataContainer.instance.dataList[0]);

			value = DataContainer.instance.dataList[0];
        }

        if (GUILayout.Button("Purchase Cost Up"))
        {
            value += costIncrement;
			DataContainer.instance.dataList[0] = value;
        }
    }
}