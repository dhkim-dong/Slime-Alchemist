using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroApplication : MonoBehaviour
{

    public void StartGameButton()
    {
        SoundManager.instance.Clear();
        SoundManager.instance.Play("100101", SoundManager.Sound.Bgm);
        SceneManager.LoadScene("1_GameScene");
    }
}
