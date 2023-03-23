using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviour
{
    [SerializeField] private Sprite[] jellySprites;
    [SerializeField] private Image curJellyImage;

    private int currentPanelIndex = 0;
    const int JELLY_0 = 0;
    const int JELLY_1 = 1;
    const int JELLY_2 = 2;
    const int JELLY_3 = 3;
    const int JELLY_4 = 4;
    const int JELLY_5 = 5;
    const int JELLY_6 = 6;
    const int JELLY_7 = 7;
    const int JELLY_8 = 8;
    const int JELLY_9 = 9;
    const int JELLY_10 = 10;
    const int JELLY_11 = 11;
    const int MAX_JELLY_INDEX = 11;

    [SerializeField] private GameObject jelly;
    [SerializeField] private Button level2Btn;
    [SerializeField] private Button level3Btn;

    private JellyStat jellyStat;
    private JellyStat curStat;

    private List<JellyStat> j_statList = new List<JellyStat>();

    private void Awake()
    {
        curStat= new JellyStat();
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
    }

    private void SetJellyPanelStat()
    {
        switch (currentPanelIndex)
        {
            case JELLY_0:
                curStat.id = 0;
                curStat.level = 1;
                curStat.name = "1단계";
                break;
            case JELLY_1:
                curStat.id = 1;
                curStat.level = 1;
                curStat.name = "2단계";
                break;
            case JELLY_2:
                curStat.id = 2;
                curStat.level = 1;
                curStat.name = "3단계";
                break;
            case JELLY_3:
                curStat.id = 3;
                curStat.level = 1;
                curStat.name = "3단계";
                break;
            case JELLY_4:
                curStat.id = 4;
                curStat.level = 1;
                curStat.name = "4단계";
                break;
            case JELLY_5:
                curStat.id = 5;
                curStat.level = 1;
                curStat.name = "5단계";
                break;
            case JELLY_6:
                curStat.id = 6;
                curStat.level = 1;
                curStat.name = "6단계";
                break;
            case JELLY_7:
                curStat.id = 7;
                curStat.level = 1;
                curStat.name = "7단계";
                break;
            case JELLY_8:
                curStat.id = 8;
                curStat.level = 1;
                curStat.name = "8단계";
                break;
            case JELLY_9:
                curStat.id = 9;
                curStat.level = 1;
                curStat.name = "9단계";
                break;
            case JELLY_10:
                curStat.id = 10;
                curStat.level = 1;
                curStat.name = "10단계";
                break;
            case JELLY_11:
                curStat.id = 11;
                curStat.level = 1;
                curStat.name = "11단계";
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

    public void GenerateSecondLevelJelly()
    {
        GameObject makeJelly = Instantiate(jelly, new Vector3(0, 0, 0), Quaternion.identity);

        jellyStat = makeJelly.GetComponent<Jelly>().JellyStat;

        j_statList.Add(jellyStat); ;

        jellyStat.name = curStat.name;
        jellyStat.id = curStat.id;
        jellyStat.level = curStat.level + 1;
    }
}
