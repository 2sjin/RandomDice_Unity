using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDiceButton : MonoBehaviour {
    float firstPosX = -1.4f;    // 첫 주사위의 X 좌표
    float firstPosY = 0.0f;     // 첫 주사위의 Y 좌표
    float marginX = 0.7f;   // X 간격
    float marginY = 0.7f;   // Y 간격

    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject dicePrefab;

    private void OnMouseUp() {
        GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
        int diceIndex;
        List<int> nullIndexList = new List<int>();

        // 주사위 배열에서 null인 인덱스만 별도의 리스트에 저장
        for (int i=0; i<15; i++) {
            if (gameManagerScript.diceArray[i] == null)
                nullIndexList.Add(i);
        }

        // null 인덱스 리스트에서 랜덤한 값을 저장함
        diceIndex = nullIndexList[Random.Range(0, nullIndexList.Count)];

        // 새로 생성할 주사위의 위치 계산 후, 주사위 생성
        float newDicePosX = marginX * (diceIndex % 5) + firstPosX;
        float newDicePosY = -marginY * (int)(diceIndex / 5) + firstPosY;
        gameManagerScript.diceArray[diceIndex]
            = Instantiate(dicePrefab, new Vector3(newDicePosX, newDicePosY, 0), Quaternion.identity);
    }
}