using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// ��� ��ư�� ������ Ŭ���� �̱� ������, ������ ������ �ϴ� ���� �����ϸ� �ȵȴ�.
public class ButtonEvent : MonoBehaviour
{
    private Button button;

    [SerializeField] private string buttonName;

    private int buttonIndex = -1;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        //button.onClick.AddListener(() => event2(5)); // ���ڰ� ������ ��ư�� �������� �Ҵ��ϴ� ��. with ���ٽ�

        if(buttonName == "makeJelly")
        {
            buttonIndex = 0;
        }
        else if(buttonName == "upgrade")
        {
            buttonIndex = 1;
        }
        else if(buttonName == "sell")
        {
            buttonIndex = 2;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("�߸��� button�̸��� ȣ���մϴ�.");
#endif
        }
    }

    private void OnButtonClick()
    {
        CallButtonEvent();
    }

    private void CallButtonEvent()
    {
        switch (buttonIndex)
        {
            case 0:
                ButtonCall.instance.CallEventMethodByIndex(buttonIndex);
                break;
            case 1:
                Debug.Log("upgrade");
                break;
             case 2:
                Debug.Log("sell");
                break;

        }
    }
}
