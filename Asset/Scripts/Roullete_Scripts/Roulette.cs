using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform piecePrefab; // �귿�� ǥ�õǴ� ���� ������
    [SerializeField] private Transform linePrefab; // �������� �����ϴ� �� ������
    [SerializeField] private Transform pieceParent; // �������� ��ġ�Ǵ� �θ��� Transform
    [SerializeField] private Transform lineParent; // ������ ��ġ�Ǵ� �θ� Transform
    [SerializeField] RoulettePieceData[] roulettePieceData; // �귿�� ǥ�õǴ� ���� �迭

    [SerializeField] private int spinDuration; // ȸ���ð�
    [SerializeField] private Transform spinningRoulette; // ���� ȸ���ϴ� ȸ���� Transform
    [SerializeField] private AnimationCurve spinningCurve; // ȸ�� �ӵ� ��� ���� �׷���

    private float pieceAngle; // ���� �ϳ��� ��ġ�Ǵ� ����
    private float halfPieceAngle; // ���� �ϳ��� ��ġ�Ǵ� ������ ���� ũ��
    private float halfPieceAngleWithPaddings; // ���� ���⸦ ������ Padding�� ���Ե� ���� ũ��

    private int accumulatedWeight; // ����ġ ����� ���� ����
    private bool isSpinning = false; // ���� ȸ��������
    private int selectedIndex = 0; // �귿���� ���õ� ������

    private void Awake()
    {
        pieceAngle = 360 / roulettePieceData.Length;
        halfPieceAngle = pieceAngle * 0.5f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

        SpawnPiecesAndLines();
        CalculateWeightsAndIndices();

        //Debug.Log($"Index : {GetRandomIndex()}");
    }

    private void CalculateWeightsAndIndices()
    {
        for (int i = 0; i < roulettePieceData.Length; ++i)
        {
            roulettePieceData[i].index = i;

            // ����ó��. Ȥ�ö� chance���� 0 �����̸� 1�� ����
            if(roulettePieceData[i].chance <= 0)
            {
                roulettePieceData[i].chance = 1;
            }

            accumulatedWeight += roulettePieceData[i].chance;
            roulettePieceData[i].weight = accumulatedWeight;

            Debug.Log($"({roulettePieceData[i].index}){roulettePieceData[i].description}:{roulettePieceData[i].weight}");
        }
    }

    private void SpawnPiecesAndLines()
    {
        for (int i = 0; i < roulettePieceData.Length; ++i)
        {
            Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
            // ������ �귿 ������ ���� ����
            piece.GetComponent<Roulettepiece>().Setup(roulettePieceData[i]);
            // ������ �귿 ���� ȸ��
            piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));

            Transform line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);
            // ������ �� ȸ�� (�귿 ���� ���̸� �����ϴ� �뵵)
            line.RotateAround(lineParent.position, Vector3.back, (pieceAngle * i) + halfPieceAngle);
        }
    }

    private int GetRandomIndex()
    {
        int weight = UnityEngine.Random.Range(0, accumulatedWeight);

        for (int i = 0; i < roulettePieceData.Length; ++i)
        {
            if(roulettePieceData[i].weight > weight)
            {
                return i;
            }
        }

        return 0;
    }

    public void Spin(UnityAction<RoulettePieceData> action = null)
    {
        if (isSpinning == true) return;

        selectedIndex = GetRandomIndex();
        //
        float angle = pieceAngle * selectedIndex;

        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360;
        float randomAngle = UnityEngine.Random.Range(leftOffset, rightOffset);

        int rotateSpeed = 2;
        float targetAngle = (randomAngle + 360 * spinDuration * rotateSpeed);

        Debug.Log($"SelectedIndex:{selectedIndex}, Angle : {angle}");
        Debug.Log($"left/right/random:{leftOffset}/{rightOffset}/{randomAngle}");
        Debug.Log($"targetAngle:{targetAngle}");

        isSpinning = true;
        StartCoroutine(OnSpin(targetAngle, action));
    }

    private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / spinDuration;

            float z = Mathf.Lerp(0, end, spinningCurve.Evaluate(percent));
            spinningRoulette.rotation = Quaternion.Euler(0, 0, z);

            yield return null;
        }

        isSpinning = false;

        if (action != null) action.Invoke(roulettePieceData[selectedIndex]);

        RouletteResult rResult = new RouletteResult(roulettePieceData[selectedIndex].icon);
      
    }

    public void ChangeIcon()
    {

    }

    public struct RouletteResult // ����ü�� ��� �ϴ� ����? ����� ���ÿ� �����ȴ�. Struct�� ��Ÿ���̴�.
    {
        private Sprite resultIcon;

        public RouletteResult(Sprite icon)
        {
            resultIcon = icon;
        }
    }
}