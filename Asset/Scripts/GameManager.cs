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

    public List<int> jellyBoolList = new List<int>();

    public bool[] level1Groups;
    public bool[] level2Groups;
    

    private void Awake()
    {
        instance = this;
        level1Groups = new bool[12];
        level2Groups = new bool[12];
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
    // 젤리가 생성되거나 파괴 될 때 메서드 실행
    public void SearchDuplicate()
    {
        if (jellyIdList == null) return;
        
        // 중복 요소 체크
        var duplicate = jellyIdList.GroupBy(i => i)
            .Where(g => g.Count() > 2)
            .Select(g => g.Key)
            .ToList();

        // Bool List 초기화
        jellyBoolList.Clear();

        // 중복 값 추가
        foreach(var it in duplicate)
        {
            jellyBoolList.Add(it);
        }

        // 전부 초기화
        for (int i = 0; i < level1Groups.Length; i++)
        {
            level1Groups[i] = false;
        }

        // 중복 체크
        foreach (var a in jellyBoolList)
        {
            level1Groups[a] = true;
        }
    }

    // isGroups[i]가 true일 때 젤리 합치기 기능
    // isGroups[i]를 false로
    // 해당하는 젤리 오브젝트 3개를 파괴하고
    // 새로운 젤리를 생성한다.
    // 또는 2개를 파괴하고 1개를 변화시킨다.
}
