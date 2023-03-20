using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    #region 디버그 주석
    // Debug 001 : 싱글톤 instance 비할당 오류

    #endregion

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
}
