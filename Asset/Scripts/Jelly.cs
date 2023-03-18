using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public enum State { Idle, Walk};

    private State state; // ������ �ڽ��� ���¸� ���´�.

    [SerializeField] private float jellySpeed;

    private Vector3 moveVec;
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;

    [SerializeField] private bool isBorder;

    private SpriteRenderer spriteRenderer;

    private int id;
    private int level;
    private int exp;

    // ���� ������ ��� ���¿� �����Ѵٸ�

    // ������ �ð� ���� '���'�ϴ� ����� �ʿ��ϴ�. 

    // ���� ������ �ȴ� ���¿� �����Ѵٸ�

    // �Ұ��� isWalk true�� �����ϰ�

    // ������ �ð� ���� '���' ��Ų��.

    // �ڽ��� ���¸� �ľ��ϴ� ������ ¥���Ѵ�.

    // �����ؾ� �� ��� 1. ������ ��� ����

    // �����ؾ� �� ��� 2. ������ �ȱ�

    private void Start()
    {
        JellyState();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckBorder();
        ReturnJellyPos();
        transform.Translate(moveVec.normalized * jellySpeed * Time.deltaTime);
    }

    #region ���� �̵�
    void JellyState()
    {
        switch (state)
        {
            case State.Idle: JellyStateIdle();
                break;
            case State.Walk: JellyStateWalk();
                break;
            default: Debug.Log("�ش��ϴ� ������ ���°� �����ϴ�");
                break;
        }
    }

    void JellyStateIdle()
    {
        StopAllCoroutines();
        StartCoroutine(JellyStateChangeWalk());

        // ������ ���¸� ���� �ð� �� Walk�� �ٲپ� �ش�.
    }

    void JellyStateWalk()
    {
        StopAllCoroutines();
        SetJellyWalk();
        StartCoroutine(JellyStateChangeIdle());
    }

    void SetJellyWalk()
    {
        float randomX = UnityEngine.Random.Range(-0.8f, 0.8f);
        float randomY = UnityEngine.Random.Range(-0.8f, 0.8f);

        if (randomX < 0) spriteRenderer.flipX = true;
        else if (randomX > 0) spriteRenderer.flipX = false;

        Vector3 newVec = transform.position;
        newVec.x = randomX;
        newVec.y = randomY;

        moveVec = newVec;
    }


    void CheckBorder() // ������ Walk���¿��� Bool�� ��輱�� ��츦 �ľ��ϰ� �ǵ��� ���� ����
    {
        if(transform.position.x < topLeft.position.x || transform.position.y > topLeft.position.y)
        {
            isBorder = true;           
        }
        else if(transform.position.x > bottomRight.position.x || transform.position.y < bottomRight.position.y)
        {
            isBorder = true;           
        }
    }

    void ReturnJellyPos()
    {
        if (isBorder)
        {
            StopAllCoroutines();
            moveVec *= -1;
            if (moveVec.x < 0) spriteRenderer.flipX = true;
            else if (moveVec.x > 0) spriteRenderer.flipX = false;

            StartCoroutine(JellyStateChangeIdle());
        }
    }

    IEnumerator JellyStateChangeWalk()
    {
        int random = UnityEngine.Random.Range(3, 5);
        yield return new WaitForSeconds(random);
        state = State.Walk;
        jellySpeed = 0.5f;
        JellyState();
        yield return null;
    }

    IEnumerator JellyStateChangeIdle()
    {
        isBorder = false;
        int random = UnityEngine.Random.Range(3, 5);
        yield return new WaitForSeconds(random);
        state = State.Idle;
        jellySpeed = 0f;
        JellyState();
        yield return null;
    }
    #endregion


    #region ���콺 �̺�Ʈ

    void OnMouseDown()
    {
        
    }

    private void OnMouseDrag()
    {
        JellyMouseMove();
    }

    private void OnMouseUp()
    {
        if (transform.position.x < topLeft.position.x || transform.position.y > topLeft.position.y)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > bottomRight.position.x || transform.position.y < bottomRight.position.y)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        JellyState();

        // �Ǹ� �������� üũ �� ��

        // ������ �Ǹ��Ѵ�.( ��带 ��´�) + �ش� ������Ʈ �ı�
    }

    void JellyMouseMove()
    {
        StopAllCoroutines();
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
        transform.position = mPos;
    }

    #endregion
}
