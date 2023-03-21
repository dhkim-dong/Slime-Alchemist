using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<int> jellyIdList = new List<int>();

    public bool isGroup1;
    public bool isGroup2;
    public bool isGroup3;
    public bool isGroup4;

    private void Awake()
    {
        instance = this;
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

        SearchDuplicate();
    }

    #region ��ȭ

    // ��� ȹ��

    public void GetGold(int id)
    {
        gameData.Gold += jellyGoldList[id];
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
    public void SearchDuplicate()
    {
        var duplicate = jellyIdList.GroupBy(i => i)
            .Where(g => g.Count() > 2)
            .Select(g => g.Key)
            .ToList();
             
        foreach(var it in duplicate)
        {
            if(it == 0)
            {
                isGroup1 = true;
            }
            else if(it == 1)
            {
                isGroup2 = true;
            }
            else
            {
                Debug.Log("�ߺ� id�� �׿�");
            }
        }
    }
}
