using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public enum State { Idle, Walk};

    private State state; // 젤리는 자신의 상태를 갖는다.

    [SerializeField] private float jellySpeed;

    private Vector3 moveVec;
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;

    [SerializeField] private bool isBorder;

    private SpriteRenderer spriteRenderer;

    private int id;
    private int level;
    private int exp;

    // 만약 젤리가 대기 상태에 진입한다면

    // 랜덤한 시간 동안 '대기'하는 기능이 필요하다. 

    // 만약 젤리가 걷는 상태에 진입한다면

    // 불값을 isWalk true로 변경하고

    // 랜덤한 시간 동안 '대기' 시킨다.

    // 자신의 상태를 파악하는 로직을 짜야한다.

    // 구현해야 할 기능 1. 젤리의 대기 상태

    // 구현해야 할 기능 2. 젤리의 걷기

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

    #region 젤리 이동
    void JellyState()
    {
        switch (state)
        {
            case State.Idle: JellyStateIdle();
                break;
            case State.Walk: JellyStateWalk();
                break;
            default: Debug.Log("해당하는 젤리의 상태가 없습니다");
                break;
        }
    }

    void JellyStateIdle()
    {
        StopAllCoroutines();
        StartCoroutine(JellyStateChangeWalk());

        // 젤리의 상태를 랜덤 시간 후 Walk로 바꾸어 준다.
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


    void CheckBorder() // 젤리의 Walk상태에서 Bool이 경계선인 경우를 파악하고 되돌아 가는 로직
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


    #region 마우스 이벤트

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

        // 판매 가능한지 체크 한 후

        // 젤리를 판매한다.( 골드를 얻는다) + 해당 오브젝트 파괴
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
