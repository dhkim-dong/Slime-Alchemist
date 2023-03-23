using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform piecePrefab; // 룰렛에 표시되는 정보 프리펩
    [SerializeField] private Transform linePrefab; // 정보들을 구분하는 선 프리펩
    [SerializeField] private Transform pieceParent; // 정보들이 배치되는 부모의 Transform
    [SerializeField] private Transform lineParent; // 선들이 배치되는 부모 Transform
    [SerializeField] RoulettePieceData[] roulettePieceData; // 룰렛에 표시되는 정보 배열

    [SerializeField] private int spinDuration; // 회전시간
    [SerializeField] private Transform spinningRoulette; // 실제 회전하는 회전판 Transform
    [SerializeField] private AnimationCurve spinningCurve; // 회적 속도 제어를 위한 그래프

    private float pieceAngle; // 정보 하나가 배치되는 각도
    private float halfPieceAngle; // 정보 하나가 배치되는 각도의 절반 크기
    private float halfPieceAngleWithPaddings; // 선의 굵기를 고려한 Padding이 포함된 절반 크기

    private int accumulatedWeight; // 가중치 계산을 위한 변수
    private bool isSpinning = false; // 현재 회전중인지
    private int selectedIndex = 0; // 룰렛에선 선택된 아이템

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

            // 예외처리. 혹시라도 chance값이 0 이하이면 1로 설정
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
            // 생성한 룰렛 조각의 정보 설정
            piece.GetComponent<Roulettepiece>().Setup(roulettePieceData[i]);
            // 생성한 룰렛 조각 회전
            piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));

            Transform line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);
            // 생성한 선 회전 (룰렛 조각 사이를 구분하는 용도)
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

    public struct RouletteResult // 구조체를 사용 하는 이유? 선언과 동시에 생성된다. Struct는 값타입이다.
    {
        private Sprite resultIcon;

        public RouletteResult(Sprite icon)
        {
            resultIcon = icon;
        }
    }
}
