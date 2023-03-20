using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCall : MonoBehaviour
{
    public static ButtonCall instance;

    [SerializeField] private GameObject makeJellyPanel;

    private void Start()
    {
        instance= this;
    }

    public void CallEventMethodByIndex(int index)
    {
        if(index == 0)
        {
            makeJellyPanel.SetActive(true);
        }
    }

    public void ExitEventMethodByIndex(int index)
    {
        if(index == 0)
        {
            makeJellyPanel.SetActive(false);
        }
    }
}
