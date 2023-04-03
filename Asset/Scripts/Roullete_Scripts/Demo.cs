using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private Roulette roulette;
    [SerializeField] Button buttonSpin;
    [SerializeField] private GameObject jelly;

    private SpriteRenderer jellySprite;
    private JellyStat j_stat;

    private List<JellyStat> j_statList = new List<JellyStat>();

    private void Awake()
    {
        buttonSpin.onClick.AddListener(() =>
        {
            buttonSpin.interactable = false;

            GameManager.instance.UseGold(GameManager.instance.rouletteCost);
            if (GameManager.instance.canBuy)
            {
                roulette.Spin(EndOfSpin);
            }
            else
            {
                ButtonCall.instance.ExitEventMethodByIndex(0);
                buttonSpin.interactable = true;
            }
        }
        );

        j_statList = GameManager.instance.jellyList;
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        Debug.Log($"{selectedData.index} :{selectedData.description}");

        ButtonCall.instance.ExitEventMethodByIndex(0);

        buttonSpin.interactable = true;

        GameObject makeJelly = Instantiate(jelly, new Vector3(0, 0, 0), Quaternion.identity);

        j_stat = makeJelly.GetComponent<Jelly>().JellyStat;

        j_statList.Add(j_stat); ;

        j_stat.gameObject = makeJelly;
        j_stat.name = selectedData.description;
        j_stat.id = selectedData.id;
        j_stat.level = 1;
        j_stat.exp = 0;

        jellySprite = makeJelly.GetComponent<SpriteRenderer>();
        jellySprite.sprite = selectedData.icon;

        GameManager.instance.SearchDuplicate();
    }
}
