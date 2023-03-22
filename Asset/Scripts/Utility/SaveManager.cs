using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField] private string characterName;
    [SerializeField] private int level;
    [SerializeField] private float exp;
    [SerializeField] private List<string> itemsName;

    [SerializeField] private Vector3 lastPosition;

    public SaveData(string t_characterName, int t_level, float t_exp, List<string> t_itemsName, Vector3 t_lastPos)
    {
        characterName = t_characterName;
        level = t_level;
        exp = t_exp;
        itemsName = new List<string>();
        foreach(var n in t_itemsName)
        {
            itemsName.Add(n);
        }
        lastPosition = new Vector3(t_lastPos.x, t_lastPos.y, t_lastPos.z);
    }
}


public class SaveManager : MonoBehaviour
{
    private static readonly string privateKey = "15135dalfasdfap35038520asdlgam35918as";

    public static void Save()
    {
        SaveData sd = new SaveData(
            "동훈",
            10,
            0.1f,
            new List<string> { "포션", "단검" },
            new Vector3(1, 2, 3));

        string jsonString = DataToJson(sd);
        string encryptString = Encrypt(jsonString);
        SaveFile(encryptString);
    }

    private static void SaveFile(string encryptString)
    {
        throw new NotImplementedException();
    }

    private static string Encrypt(string jsonString)
    {
        throw new NotImplementedException();
    }

    private static string DataToJson(SaveData sd)
    {
        throw new NotImplementedException();
    }
}
