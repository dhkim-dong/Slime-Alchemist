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

    // 테스트 2 : 코드 간결화를 위해 반복을 쓰고 싶다. 반복문에서 생성된 데이터를 자료구조를 담는 리스트형으로 만들어서 쓰기? 구현해볼 것 = 리스트를 리스트로 가지는
    // ex) List<List<int>> doubleList = new List<List<int>>();
    private void Start()
    {
     
    }
}
