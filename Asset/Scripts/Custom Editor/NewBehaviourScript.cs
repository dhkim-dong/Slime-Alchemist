using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : EditorWindow
{
    [MenuItem("Window/NewBehaviourScript")]
    static void Open()
    {
        GetWindow<NewBehaviourScript>();
    }
}
