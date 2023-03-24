using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviour
{
    public static Action target;

    [SerializeField] private Sprite[] jellySprites;
    [SerializeField] private Image curJellyImage;

    private int currentPanelIndex = 0;
    const int MAX_JELLY_INDEX = 11;

    [SerializeField] private GameObject jelly;
    [SerializeField] private Button level2Btn;
    [SerializeField] private Button level3Btn;

    private JellyStat jellyStat;
    private JellyStat defaultStat;

    private Animator jellyAC;

    private List<JellyStat> j_statList = new List<JellyStat>();

    private List<GameObject> jellyList = new List<GameObject>();

    private void Awake()
    {
        target = () => { TestJellyMake(); };

        defaultStat= new JellyStat();
        level2Btn.interactable = false;
        level3Btn.interactable = false;
    }

    private void Start()
    {
        SetJellyPanelStat();
    }

    private void Update()
    {
        CheckEnableButton();
        CheckEnable2Button();
    }

    private void SetJellyPanelStat()
    {
        switch (currentPanelIndex)
        {
            case 0:
                defaultStat.id = 0;
                defaultStat.level = 1;
                defaultStat.name = "1�ܰ�";
                break;
            case 1:
                defaultStat.id = 1;
                defaultStat.level = 1;
                defaultStat.name = "2�ܰ�";
                break;
            case 2:
                defaultStat.id = 2;
                defaultStat.level = 1;
                defaultStat.name = "3�ܰ�";
                break;
            case 3:
                defaultStat.id = 3;
                defaultStat.level = 1;
                defaultStat.name = "3�ܰ�";
                break;
            case 4:
                defaultStat.id = 4;
                defaultStat.level = 1;
                defaultStat.name = "4�ܰ�";
                break;
            case 5:
                defaultStat.id = 5;
                defaultStat.level = 1;
                defaultStat.name = "5�ܰ�";
                break;
            case 6:
                defaultStat.id = 6;
                defaultStat.level = 1;
                defaultStat.name = "6�ܰ�";
                break;
            case 7:
                defaultStat.id = 7;
                defaultStat.level = 1;
                defaultStat.name = "7�ܰ�";
                break;
            case 8:
                defaultStat.id = 8;
                defaultStat.level = 1;
                defaultStat.name = "8�ܰ�";
                break;
            case 9:
                defaultStat.id = 9;
                defaultStat.level = 1;
                defaultStat.name = "9�ܰ�";
                break;
            case 10:
                defaultStat.id = 10;
                defaultStat.level = 1;
                defaultStat.name = "10�ܰ�";
                break;
            case 11:
                defaultStat.id = 11;
                defaultStat.level = 1;
                defaultStat.name = "11�ܰ�";
                break;
        }
    }

    private void CheckEnableButton()
    {
        if (GameManager.instance.level1Groups[currentPanelIndex])
        {
            level2Btn.interactable = true;
        }
        else
        {
            level2Btn.interactable = false;
        } 
    }

    private void CheckEnable2Button()
    {
        if (GameManager.instance.level2Groups[currentPanelIndex])
        {
            level3Btn.interactable = true;
        }
        else
        {
            level3Btn.interactable = false;
        }
    }

    private void SetJellySprite()
    {
        curJellyImage.sprite = jellySprites[currentPanelIndex];
    }

    public void JellyRightButton()
    {
        currentPanelIndex++;
        
        if(currentPanelIndex > MAX_JELLY_INDEX)
        {
            currentPanelIndex = 0;
        }
        SetJellyPanelStat();
        SetJellySprite();
    }

    public void JellyLeftButton()
    {
        currentPanelIndex--;

        if(currentPanelIndex < 0)
        {
            currentPanelIndex = MAX_JELLY_INDEX;
        }
        SetJellyPanelStat();
        SetJellySprite();
    }

    // 2023.03.24 �۾� ����
    // ������ 1���� ������ �ı��ؾ� �ϴµ� ������ ���� ���� ���Ѵ�.
    // ���� �����͸� ���� ���� �����Ѵ�?
    // �Ǵ� ���� �Ŵ����� jellyList�� id�� curPanelIndex�� ���� ������Ʈ�� �˻��Ѵ�.

    private void FindSameJelly(int findlevel)
    {
        var obj = GameManager.instance.jellyList.FindAll(a =>  a.id== currentPanelIndex);
        var obj2 = obj.FindAll(a => a.level == findlevel);

        List<GameObject> sameJellys = new List<GameObject>();

        foreach(var it in obj2)
        {
            sameJellys.Add(it.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            sameJellys[i].SetActive(false);
            GameManager.instance.jellyList.Remove(sameJellys[i].GetComponent<Jelly>().JellyStat); 
        }
    }

    // ����Ʈ �����ϰ� Act ���� �۾��� ��
    // �� �޼����� ������ �κ��� ������ �� �κ��� �ϳ��� ��ĥ ����� �����غ���
    // 2023-03-25 �۾�����
    public void GenerateSecondLevelJelly()
    {
        FindSameJelly(1);

        GameObject makeJelly = Instantiate(jelly, new Vector3(0, 0, 0), Quaternion.identity);

        jellyAC = makeJelly.GetComponent<Animator>();

        jellyStat = makeJelly.GetComponent<Jelly>().JellyStat;

        jellyStat.gameObject = makeJelly;
        jellyStat.name = defaultStat.name;
        jellyStat.id = defaultStat.id;
        jellyStat.level = defaultStat.level + 1;

        GameManager.instance.jellyList.Add(jellyStat);

        jellyAC.runtimeAnimatorController = GameManager.instance.LevelAc[1];
        SpriteRenderer jellySprite = makeJelly.gameObject.GetComponent<SpriteRenderer>();
        jellySprite.sprite = jellySprites[currentPanelIndex];

        GameManager.instance.SearchDuplicate();
    }

    public void GenerateThirdLevelJelly()
    {
        FindSameJelly(2);

        GameObject makeJelly = Instantiate(jelly, new Vector3(0, 0, 0), Quaternion.identity);

        jellyAC = makeJelly.GetComponent<Animator>();

        jellyStat = makeJelly.GetComponent<Jelly>().JellyStat;

        jellyStat.gameObject = makeJelly;
        jellyStat.name = defaultStat.name;
        jellyStat.id = defaultStat.id;
        jellyStat.level = defaultStat.level + 2;

        GameManager.instance.jellyList.Add(jellyStat);

        jellyAC.runtimeAnimatorController = GameManager.instance.LevelAc[2];
        SpriteRenderer jellySprite = makeJelly.gameObject.GetComponent<SpriteRenderer>();
        jellySprite.sprite = jellySprites[currentPanelIndex];

        GameManager.instance.SearchDuplicate();
    }

    private void TestJellyMake()
    {
        GameObject makeJelly = Instantiate(jelly, new Vector3(0, 0, 0), Quaternion.identity);

        jellyAC = makeJelly.GetComponent<Animator>();

        jellyStat = makeJelly.GetComponent<Jelly>().JellyStat;

        jellyStat.gameObject = makeJelly;
        jellyStat.name = defaultStat.name;
        jellyStat.id = defaultStat.id;
        jellyStat.level = defaultStat.level + 1;

        GameManager.instance.jellyList.Add(jellyStat);

        jellyAC.runtimeAnimatorController = GameManager.instance.LevelAc[1];
        SpriteRenderer jellySprite = makeJelly.gameObject.GetComponent<SpriteRenderer>();
        jellySprite.sprite = jellySprites[currentPanelIndex];
        GameManager.instance.SearchDuplicate();
    }
}
