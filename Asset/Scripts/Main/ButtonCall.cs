using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCall : MonoBehaviour
{
    public static ButtonCall instance;

    [SerializeField] private GameObject makeJellyPanel;
    [SerializeField] private GameObject upgradeJellyPanel;
    [SerializeField] private GameObject[] missionPanel;
    private GameObject soundPanel;

    public bool isMission;
    private void Start()
    {
        instance = this;
    }

    public void PanelReset()
    {
        makeJellyPanel.SetActive(false);
        upgradeJellyPanel.SetActive(false);
    }

    public void CallEventMethodByIndex(int index)
    {
        switch (index)
        {
            case 0: makeJellyPanel.SetActive(true);
                SoundManager.instance.Play("Button", SoundManager.Sound.Effect);
                break;
            case 1:upgradeJellyPanel.SetActive(true);
                SoundManager.instance.Play("Button", SoundManager.Sound.Effect);
                break;
            case 2:
                isMission = !isMission;
                if (isMission)
                {

                    for (int i = 0; i < missionPanel.Length; i++)
                        missionPanel[i].SetActive(true);
               
                    FadeController.instance.ImageFadeIn(missionPanel);
                }
                else if(!isMission)
                {
                    FadeController.instance.ImageFadeOut(missionPanel);
                }
                break;
            case 4: SoundManager.instance.ControlSoundPanel();
                break;
        }
    }

    public void ExitEventMethodByIndex(int index)
    {
        switch (index)
        {
            case 0:
                makeJellyPanel.SetActive(false);
                SoundManager.instance.Play("Pause Out", SoundManager.Sound.Effect);
                break;
            case 3:
                upgradeJellyPanel.SetActive(false);
                SoundManager.instance.Play("Pause Out", SoundManager.Sound.Effect);
                break;
        }
    }
}
