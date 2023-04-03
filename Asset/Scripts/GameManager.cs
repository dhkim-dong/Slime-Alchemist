using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// UI ����ȭ
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Text jelatinText;
    [SerializeField] Text goldText;

    [SerializeField] private int startGold;
    [SerializeField] private int startJelatin;

    public RuntimeAnimatorController[] LevelAc;
    public int[] jellyGoldList;
    public int[] jelatinList;

    public bool isSell;
    public bool canBuy;
    public GameObject selectJelly;

    public List<JellyStat> jellyList = new List<JellyStat>();

    public List<int> jellyBoolList = new List<int>();
    public List<int> jellyBool2List = new List<int>();

    public bool[] level1Groups;
    public bool[] level2Groups;

    public int rouletteCost;

    private void Awake()
    {
        instance = this;
        level1Groups = new bool[12];
        level2Groups = new bool[12];

        DataContainer.instance.SetDataList(rouletteCost);
    }

    // ������ ���̵� ��(1���� ����)�� int level �Ű������� ������ ��ȭ�� ǥ���ϴ� �޼���
    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }

    private GameData gameData;

    private void Start()
    {
        gameData = new GameData();
        gameData.Jelatin = 10;
        gameData.Gold = startGold;
    }

    private void Update()
    {
        UpdateGameResources();
        gameData.Jelatin = startJelatin;
    }

    #region ��ȭ

    // ��� ȹ��

    public void GetGold(JellyStat stat)
    {
        int totGold = jellyGoldList[stat.id] * stat.level;
        gameData.Gold += totGold;
    }

    // ����ƾ ȹ��
    public void GetJelatin(int id)
    {
        gameData.Jelatin += jelatinList[id];
    }

    public void UseGold(int value)
    {
        if (gameData.Gold < value)
        {
            canBuy = false;
            return;
        }
        canBuy = true;
        gameData.Gold -= value;
    }

    public void UseJelatin(int value)
    {
        gameData.Jelatin -= value;
    }
    #endregion

    // ����ƾ�� ��� UI ����
    private void UpdateGameResources()
    {
        jelatinText.text = string.Format("{0:#,###; -#,###;0}", gameData.Jelatin);
        goldText.text = string.Format("{0:#,###; -#,###;0}", gameData.Gold);
    }

    #region EventTrigger for sell btn
    public void PointEnterSellButton()
    {
        isSell = true;
    }
    
    public void PointExitSellButton()
    {
        isSell = false;
    }
    #endregion

    // �ߺ��� ������ �� ã��
    // ������ �����ǰų� �ı� �� �� �޼��� ����

    public void SearchDuplicate()
    {
        if (jellyList == null) return;

        // Bool List �ʱ�ȭ
        jellyBoolList.Clear();
        jellyBool2List.Clear();

        var same = jellyList.FindAll(i => i.level == 1);
        var duplicate = same.GroupBy(i => i.id)
        .Where(g => g.Count() > 2)
        .Select(g => g.Key)
        .ToList();

        var same2 = jellyList.FindAll(i => i.level == 2);
        var duplicate2 = same2.GroupBy(i => i.id)
        .Where(g => g.Count() > 2)
        .Select(g => g.Key)
        .ToList();

        // �ߺ� �� �߰�
        foreach (var it in duplicate)
        {
            jellyBoolList.Add(it);
        }

        foreach (var it in duplicate2)
        {
            jellyBool2List.Add(it);
        }

        // level 1 �׷� �ʱ�ȭ
        for (int i = 0; i < level1Groups.Length; i++)
        {
            level1Groups[i] = false;
        }

        // level 2 �׷� �ʱ�ȭ
        for (int i = 0; i < level2Groups.Length; i++)
        {
            level2Groups[i] = false;
        }

        // level 1 �ߺ� üũ
        foreach (var a in jellyBoolList)
        {
            level1Groups[a] = true;
        }


        // level 2 �ߺ� üũ
        foreach (var a in jellyBool2List)
        {
            level2Groups[a] = true;
        }
    }


    // �ߺ� üũ�� ���� ���� jellyidList�� �����Ͽ� �ϳ��� ����
    //public void SearchDuplicate()
    //{
    //    if (jellyIdList == null) return;

    //    // �ߺ� ��� üũ
    //    var duplicate = jellyIdList.GroupBy(i => i)
    //        .Where(g => g.Count() > 2)
    //        .Select(g => g.Key)
    //        .ToList();

    //    // Bool List �ʱ�ȭ
    //    jellyBoolList.Clear();

    //    // �ߺ� �� �߰�
    //    foreach(var it in duplicate)
    //    {
    //        jellyBoolList.Add(it);
    //    }

    //    // ���� �ʱ�ȭ
    //    for (int i = 0; i < level1Groups.Length; i++)
    //    {
    //        level1Groups[i] = false;
    //    }

    //    // �ߺ� üũ
    //    foreach (var a in jellyBoolList)
    //    {
    //        level1Groups[a] = true;
    //    }
    //}

    // isGroups[i]�� true�� �� ���� ��ġ�� ���
    // isGroups[i]�� false��
    // �ش��ϴ� ���� ������Ʈ 3���� �ı��ϰ�
    // ���ο� ������ �����Ѵ�.
    // �Ǵ� 2���� �ı��ϰ� 1���� ��ȭ��Ų��.

    //2023-03-24 ���� => JellyIdList ���ְ� jellyList�� ���� ���� �ϱ�

    // �ܼ� Ŀ�ǵ� ����� => �׽�Ʈ�� ���� 2����, 3���� ���� �����ϱ�

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 20, 30, 30), "GAMEINFO");

        GUI.TextArea(new Rect(10, 40, 200, 20), "�귿 ���� :    " + rouletteCost.ToString());

        //if(GUILayout.Button("���� ����"))
        //{
        //    UpgradePanelView.target();
        //}
    }
}
#endif
