using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCall : MonoBehaviour
{
    public static ButtonCall instance;

    [SerializeField] private GameObject makeJellyPanel;
    [SerializeField] private GameObject upgradeJellyPanel;
    private void Start()
    {
        instance= this;
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
                break;
            case 1:upgradeJellyPanel.SetActive(true);
                break;
        }
    }

    public void ExitEventMethodByIndex(int index)
    {
        switch (index)
        {
            case 0:
                makeJellyPanel.SetActive(false);
                break;
            case 3:
                upgradeJellyPanel.SetActive(false);
                break;
        }
    }
}
