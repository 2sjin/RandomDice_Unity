using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    [SerializeField]
    private GameObject dicePrefab;
    [SerializeField]
    public GameObject [] diceArray = new GameObject[15];      // 주사위 배열
    
    public DiceInfo [] deckArray = new DiceInfo[5];    // 주사위 덱 정보

    float firstPosX = -1.1f;    // 첫 주사위의 X 좌표
    float firstPosY = -0.7f;     // 첫 주사위의 Y 좌표
    float marginX = 0.55f;   // X 간격
    float marginY = 0.55f;   // Y 간격

    void Start() {
        for (int i=0; i<15; i++) {
            diceArray[i] = null;
        }

        // 테스트용 코드(주사위 덱 불러오기)
        deckArray[0] = new DiceInfo(0, 20, 0.8f, "front", 20, 0, 0, new Color32(215, 32, 56, 255));
        deckArray[1] = new DiceInfo(1, 30, 0.7f, "front", 30, 0, 0, new Color32(236, 178, 54, 255));
        deckArray[2] = new DiceInfo(2, 20, 1.3f, "random", 50, 0, 0, new Color32(56, 181, 4, 255));
        deckArray[3] = new DiceInfo(3, 20, 0.45f, "front", 10, 0, 0, new Color32(0, 211, 166, 255));
        deckArray[4] = new DiceInfo(4, 30, 1.5f, "front", 5, 0, 0, new Color32(2, 142, 224, 255));
    }

    // 주사위 생성
    public void createDice(int index, int level) {
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

        // 새로 생성한 주사위 레벨 설정
        diceArray[diceIndex].GetComponent<Dice>().setLevel(level);

        // 새로 생성한 주사위 종류 설정(0~4)
        diceArray[diceIndex].GetComponent<Dice>().diceInfo = deckArray[Random.Range(0, 5)];
    }

    // 
    public void applyDiceType(GameObject dice) {
        Dice diceScript = dice.GetComponent<Dice>();

        dice.GetComponent<SpriteRenderer>().sprite = GetComponent<DiceSpriteManager>().spriteList[diceScript.diceInfo.id];
        diceScript.levelText.GetComponent<TextMesh>().color = diceScript.diceInfo.color;
    }
}