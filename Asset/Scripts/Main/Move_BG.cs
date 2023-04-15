using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_BG : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private float moveXVec;
    private Vector3 moveVec;


    private void Update()
    {
        BackGroundScroll();
        ResetScrolling();  // BackGroundScroll�� ���ؼ� �ִ� X���� ���� �� Event�� ������ ������� ����� ���� �� ����.
    }

    void BackGroundScroll()
    {
        moveXVec = transform.position.x;
        moveVec = transform.position;
        moveVec.x = 1f;
        transform.Translate(moveVec.normalized * moveSpeed * Time.deltaTime);
    }

    void ResetScrolling()
    {
        if(transform.position.x >= maxX)
        {
            transform.position = new Vector3(minX, 0, 0);
        }
    }
}
