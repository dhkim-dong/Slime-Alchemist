using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    #region 디버그 주석
    // Debug 001 : 싱글톤 instance 비할당 오류
    // Debug 002 : Scriptable Object 출력 테스트
    #endregion

    [SerializeField] LocalTable localTable;

    private void Update()
    {
        #region 테스트1 주석
        // Debug 내용 instance를 할당안해서 null오류가 뜸!

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ButtonCall.instance.CallEventMethodByIndex(0);
        //}
        #endregion
    }

    // 테스트 2 

    private void Start()
    {
        PrintScriptableObject();
    }

    Dictionary<string, string> strings = new Dictionary<string, string>();

    private void PrintScriptableObject()
    {
        strings.Add("00001", "가위");
        strings.Add("00002", "바위");
        strings.Add("00003", "보");
        strings.Add("00004", "하늘");
        strings.Add("00005", "구름");
        strings.Add("00006", "바람");
        strings.Add("00007", "태양");
        strings.Add("00008", "달");
        strings.Add("00009", "지구");
    } 
}
