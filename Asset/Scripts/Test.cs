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

    // �׽�Ʈ 2 

    private void Start()
    {
        PrintScriptableObject();
    }

    Dictionary<string, string> strings = new Dictionary<string, string>();

    private void PrintScriptableObject()
    {
        strings.Add("00001", "����");
        strings.Add("00002", "����");
        strings.Add("00003", "��");
        strings.Add("00004", "�ϴ�");
        strings.Add("00005", "����");
        strings.Add("00006", "�ٶ�");
        strings.Add("00007", "�¾�");
        strings.Add("00008", "��");
        strings.Add("00009", "����");
    } 
}
