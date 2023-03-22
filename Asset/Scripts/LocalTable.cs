using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;



[CreateAssetMenu(fileName = "Local Script", menuName = "Local")]
public class LocalTable : ScriptableObject
{
    public List<string> idList = new List<string>();
    public List<string> nameList = new List<string>();
}



