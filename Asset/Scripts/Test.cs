using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    #region ����� �ּ�
    // Debug 001 : �̱��� instance ���Ҵ� ����
    // Debug 002 : Scriptable Object ��� �׽�Ʈ
    #endregion

    [SerializeField] LocalTable localTable;

    private void Update()
    {
        #region �׽�Ʈ1 �ּ�
        // Debug ���� instance�� �Ҵ���ؼ� null������ ��!

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ButtonCall.instance.CallEventMethodByIndex(0);
        //}
        #endregion
    }

    // �׽�Ʈ 2 : �ڵ� ����ȭ�� ���� �ݺ��� ���� �ʹ�. �ݺ������� ������ �����͸� �ڷᱸ���� ��� ����Ʈ������ ���� ����? �����غ� �� = ����Ʈ�� ����Ʈ�� ������
    // ex) List<List<int>> doubleList = new List<List<int>>();
    private void Start()
    {
     
    }
}
