using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    private int gold;     
    private int jelatin;

    public int Gold => gold;

    public int Jelatin
    {
        get { return jelatin; }
        set { jelatin = value; }
    }
}
