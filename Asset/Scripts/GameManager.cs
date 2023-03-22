using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// UI 동기화
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

    public bool[] isGroups;
    

    private void Awake()
    {
        instance = this;
        isGroups = new bool[9];
    }

    // 젤리의 아이디 값(1부터 시작)을 int level 매개변수로 젤리의 진화를 표현하는 메서드
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

    #region 재화

    // 골드 획득

    public void GetGold(int id)
    {
        gameData.Gold += jellyGoldList[id];
    }

    // 젤라틴 획득
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

    // 젤라틴과 골드 UI 갱신
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

    // 중복된 데이터 값 찾기
    public void SearchDuplicate()
    {
        if (jellyIdList == null) return;

        var duplicate = jellyIdList.GroupBy(i => i)
            .Where(g => g.Count() > 2)
            .Select(g => g.Key)
            .ToList();
             
        foreach(var it in duplicate)
        {
            for (int i = 0; i < duplicate.Count; i++)
            {
                if(it == i)
                {
                    isGroups[i] = true;
                }
            }     
        }
    }

    // isGroups[i]가 true일 때 젤리 합치기 기능
    // isGroups[i]를 false로
    // 해당하는 젤리 오브젝트 3개를 파괴하고
    // 새로운 젤리를 생성한다.
    // 또는 2개를 파괴하고 1개를 변화시킨다.
}
