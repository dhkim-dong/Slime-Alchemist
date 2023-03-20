using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// 모든 버튼이 소유할 클래스 이기 때문에, 개인이 가져야 하는 값은 설정하면 안된다.
public class ButtonEvent : MonoBehaviour
{
    private Button button;

    [SerializeField] private string buttonName;

    private int buttonIndex = -1;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        //button.onClick.AddListener(() => event2(5)); // 인자가 있을때 버튼을 동적으로 할당하는 법. with 람다식

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
            Debug.Log("잘못된 button이름을 호출합니다.");
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
