using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public enum State { Idle, Walk};

    private State state; // ������ �ڽ��� ���¸� ���´�.

    [SerializeField] private float jellySpeed;

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
