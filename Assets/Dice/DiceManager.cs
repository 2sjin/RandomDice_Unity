using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    [SerializeField]
    private GameObject dicePrefab;
    [SerializeField]
    public GameObject [] diceArray = new GameObject[15];      // 주사위 배열
    
    public DiceInfo.DiceStruct [] deckArray = new DiceInfo.DiceStruct[5];    // 주사위 덱 정보

    float firstPosX = -1.1f;    // 첫 주사위의 X 좌표
    float firstPosY = -0.7f;     // 첫 주사위의 Y 좌표
    float marginX = 0.55f;   // X 간격
    float marginY = 0.55f;   // Y 간격

    void Start() {
        for (int i=0; i<15; i++) {
            diceArray[i] = null;
        }

        // 테스트용 코드(주사위 덱 불러오기)
//        deckArray[0] = new DiceInfo.DiceStruct(0, 20, 0.8f, "front", 20, 0, 0, new Color32(215, 32, 56, 255));      // 불
//        deckArray[0] = new DiceInfo.DiceStruct(1, 30, 0.7f, "front", 30, 0, 0, new Color32(236, 178, 54, 255));     // 전기
//        deckArray[0] = new DiceInfo.DiceStruct(2, 20, 1.3f, "random", 50, 0, 0, new Color32(56, 181, 4, 255), "Common", 2);      // 독
//        deckArray[0] = new DiceInfo.DiceStruct(4, 30, 1.5f, "front", 30, 0, 0, new Color32(2, 142, 224, 255), "Common", 4);      // 얼음
//        deckArray[0] = new DiceInfo.DiceStruct(5, 50, 0.9f, "random", 0, 0, 0, new Color32(170, 0, 255, 255), "Common", 5);     // 고장난
        
        deckArray[0] = new DiceInfo.DiceStruct(3, 20, 0.45f, "front", 10, 0, 0, new Color32(0, 211, 166, 255), "common", 3);    // 바람
        deckArray[1] = new DiceInfo.DiceStruct(6, 7, 1.0f, "front", 0, 0, 0, new Color32(104, 0, 255, 255), "common", 6);       // 도박
        deckArray[2] = new DiceInfo.DiceStruct(7, 30, 1.0f, "front", 40, 0, 0, new Color32(162, 106, 52, 255), "rare", 0);        // 도성
        deckArray[3] = new DiceInfo.DiceStruct(8, 8, 2.0f, "front", 7, 60, 0, new Color32(192, 141, 231, 255), "unique", 0);      // 고성
        deckArray[4] = new DiceInfo.DiceStruct(9, 10, 2.0f, "front", 10, 0, 0, new Color32(53, 24, 93, 255), "legendary", 0);   // 성장
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

        // 새로 생성한 주사위 종류 설정
        diceArray[diceIndex].GetComponent<Dice>().diceStruct = deckArray[Random.Range(0, 5)];

        // 새로 생성한 주사위 레벨 설정
        diceArray[diceIndex].GetComponent<Dice>().diceStruct.level = level;
    }

    // 
    public void applyDiceSprite(GameObject dice, string rarity, int index) {
        DiceSprites diceSpritesScript = GetComponent<DiceSprites>();
        List<Sprite> SpriteList;

        switch(rarity) {
            case "common":
                SpriteList = diceSpritesScript.Common;
                break;
            case "rare":
                SpriteList = diceSpritesScript.Rare;
                break;
            case "unique":
                SpriteList = diceSpritesScript.Unique;
                break;
            case "legendary":
                SpriteList = diceSpritesScript.Legendary;
                break;
            default:
                SpriteList = diceSpritesScript.Common;
                break;
        }

        Dice diceScript = dice.GetComponent<Dice>();
        dice.GetComponent<SpriteRenderer>().sprite = SpriteList[index];
    }

    public void applyDiceColor(GameObject dice) {
        Dice diceScript = dice.GetComponent<Dice>();
        diceScript.levelText.GetComponent<TextMesh>().color = diceScript.diceStruct.color;
    }
}