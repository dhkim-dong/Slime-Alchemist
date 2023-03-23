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
    public int chance = 100; // ���� Ȯ��. ��� Card��  �ڽ��� Chance / Chance���� �� * 100%�� Ȯ���� ���ȴ�.

    [HideInInspector]
    public int index; // ������ ����

    [HideInInspector]
    public int weight; // ����ġ
}
