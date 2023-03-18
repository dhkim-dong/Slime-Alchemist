using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI ����ȭ
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

    // ����ƾ�� ��� UI ����
    private void UpdateGameResources()
    {
        jelatinText.text = string.Format("{0:#,###; -#,###;0}", gameData.Jelatin);
    }
}
