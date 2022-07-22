using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    [SerializeField] private GameObject dicePrefab;

    float firstPosX = -1.4f;    // 첫 주사위의 X 좌표
    float firstPosY = 0.35f;     // 첫 주사위의 Y 좌표
    float marginX = 0.7f;   // X 간격
    float marginY = 0.7f;   // Y 간격

    public GameObject [] diceArray = new GameObject[15];      // 주사위 배열

    void Start() {
        for (int i=0; i<15; i++) {
            diceArray[i] = null;
        }
    }

    // 주사위 생성
    public void createDice(int index) {
        int diceSize = diceArray.Length;
        int diceIndex = index;
        List<int> nullIndexList = new List<int>();

        // 주사위 배열에서 null인 인덱스만 별도의 리스트에 저장
        for (int i=0; i<diceArray.Length; i++) {
            if (diceArray[i] == null)
                nullIndexList.Add(i);
        }

        // 인덱스가 배열 범위를 초과하면, null을 가진 인덱스 중 랜덤한 값을 저장
        if (diceIndex < 0 || diceIndex >= diceArray.Length)
            diceIndex = nullIndexList[Random.Range(0, nullIndexList.Count)];    // null 인덱스 리스트에서 랜덤한 값을 저장함

        // 새로 생성할 주사위의 위치 계산 후, 주사위 생성
        float newDicePosX = marginX * (diceIndex % 5) + firstPosX;
        float newDicePosY = -marginY * (int)(diceIndex / 5) + firstPosY;
        diceArray[diceIndex]
            = Instantiate(dicePrefab, new Vector3(newDicePosX, newDicePosY, 0), Quaternion.identity);        
    }
}