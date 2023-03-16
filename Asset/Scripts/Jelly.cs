using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public enum State { Idle, Walk};

    private State state; // 젤리는 자신의 상태를 갖는다.

    [SerializeField] private float jellySpeed;

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
    }

    private void Update()
    {
        transform.position += Vector3.up * jellySpeed * Time.deltaTime;
    }

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
        StartCoroutine(JellyStateChangeIdle());
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
        int random = UnityEngine.Random.Range(3, 5);
        yield return new WaitForSeconds(random);
        state = State.Idle;
        jellySpeed = 0f;
        JellyState();
        yield return null;
    }
}
