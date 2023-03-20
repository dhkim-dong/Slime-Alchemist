using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private int gold;     
    private int jelatin;

    public int Gold
    {
        get { return gold; }
        set { gold = value;}
    }

    public int Jelatin
    {
        get { return jelatin; }
        set { jelatin = value; }
    }
}
