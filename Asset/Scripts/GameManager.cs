using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 동기화
public class GameManager : MonoBehaviour
{
    [SerializeField] Text jelatinText;
    [SerializeField] Text goldText;

    [SerializeField] private int testValue;

    public RuntimeAnimatorController[] LevelAc;

    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }

    private GameData gameData;

    private void Start()
    {
        gameData = new GameData();
        gameData.Jelatin = 10;
    }

    private void Update()
    {
        UpdateGameResources();
        gameData.Jelatin = testValue;
    }

    // 젤라틴과 골드 UI 갱신
    private void UpdateGameResources()
    {
        jelatinText.text = string.Format("{0:#,###; -#,###;0}", gameData.Jelatin);
    }
}
