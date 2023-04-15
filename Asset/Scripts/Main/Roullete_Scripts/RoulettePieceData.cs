using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class RoulettePieceData
{
    public Sprite icon;
    public string description;
    public int id;

    [Range(1, 100)]
    public int chance = 100; // 등장 확률. 모든 Card의  자신의 Chance / Chance값의 합 * 100%로 확률이 계산된다.

    [HideInInspector]
    public int index; // 아이템 순번

    [HideInInspector]
    public int weight; // 가중치
}
