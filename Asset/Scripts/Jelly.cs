using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class JellyStat
{
    public string name;
    public int id;
    public int exp;
}

public class Jelly : MonoBehaviour
{
    public enum State { Idle, Walk};

    private State state; // 젤리는 자신의 상태를 갖는다.

    private JellyStat jellyStat = new JellyStat();

    public JellyStat JellyStat => jellyStat;

    [SerializeField] private float jellySpeed;

    private Vector3 moveVec;
    private Transform topLeft;
    private Transform bottomRight;

    [SerializeField] private bool isBorder;
    [SerializeField] private bool isSell;

    private SpriteRenderer spriteRenderer;

    private GameObject selectJelly;

    // 만약 젤리가 대기 상태에 진입한다면

    // 랜덤한 시간 동안 '대기'하는 기능이 필요하다. 

    // 만약 젤리가 걷는 상태에 진입한다면

    // 불값을 isWalk true로 변경하고

    // 랜덤한 시간 동안 '대기' 시킨다.

    // 자신의 상태를 파악하는 로직을 짜야한다.

    // 구현해야 할 기능 1. 젤리의 대기 상태

    // 구현해야 할 기능 2. 젤리의 걷기

    // 생성될 때 topLeft 와 bottomRight를 초기화 해줘야 한다. // 2023-03-20
    // 젤리의 데이터를 설정 해주어야 한다.
    private void OnEnable()
    {
        topLeft = GameObject.FindGameObjectWithTag("TopLeft").gameObject.transform;
        bottomRight = GameObject.FindGameObjectWithTag("BottomRight").gameObject.transform;

        JellyStat.name = "테스트 이름";
    }

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
        GameManager.instance.selectJelly = gameObject;
    }

    private void OnMouseDrag()
    {
        JellyMouseMove();
        // 선택한 젤리의 정보를 받아오는 메소드 필요
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

        if (GameManager.instance.isSell)
        {
            // 현재 젤리의 ID를 검색한다.
            // 젤리 ID에 해당하는 Gold text값을 가져온다.
            // 젤리의 Level * Gold 값을 골드에 더한다.
            // 최대 금액 조건에 부합하는지 확인한다.
            // 젤리의 정보에 해당하는 골드를 획득
            GameManager.instance.jellyList.Remove(jellyStat);
            Destroy(GameManager.instance.selectJelly);
            GameManager.instance.GetGold(jellyStat.id);
        }
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
