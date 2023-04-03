using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer instance;

    private void Awake()
    {
        instance = this;
    }

    public List<int> dataList = new List<int>();

    public void SetDataList(int value)
    {
        dataList.Add(value);
    }
}
