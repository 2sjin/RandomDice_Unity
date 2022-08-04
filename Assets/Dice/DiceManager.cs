using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    public GameObject dicePrefab;
    public GameObject [] diceFieldArray = new GameObject[15];      // 주사위 필드 배열

    public GameObject diceDataBaseConnector;
    public DiceInfo.DiceStruct [] deckArray = new DiceInfo.DiceStruct[5];    // 주사위 덱 정보
    public static string [] diceDataText = new string[5];   // DB에서 가져올 주사위 정보

    float firstPosX = -1.1f;    // 첫 주사위의 X 좌표
    float firstPosY = -0.7f;    // 첫 주사위의 Y 좌표
    float marginX = 0.55f;   // X 간격
    float marginY = 0.55f;   // Y 간격

    void Start() {
        // 주사위 필드 배열 초기화
        for (int i=0; i<15; i++)
            diceFieldArray[i] = null;

        // (테스트용 코드) DB에 연결하여 (주사위ID, 덱 인덱스)에 해당하는 주사위 정보를 가져옴
        DiceDatabaseConnector dbConnector = diceDataBaseConnector.GetComponent<DiceDatabaseConnector>();
        dbConnector.getDiceInfoFromDatabase(3, 0);     // 바람        
        dbConnector.getDiceInfoFromDatabase(6, 1);     // 도박
        dbConnector.getDiceInfoFromDatabase(7, 2);     // 도성
        dbConnector.getDiceInfoFromDatabase(8, 3);     // 고성
        dbConnector.getDiceInfoFromDatabase(9, 4);     // 성장
    }

    void Update() {
        // 덱이 비어있으면(null) DB에서 가져온 주사위 정보를 덱에 적용
        for (int i=0; i<5; i++)
            if (diceDataText[i] != null)
                deckArray[i] = new DiceInfo.DiceStruct(diceDataText[i]);
    }

    // 주사위 생성
    public void createDice(int index, int level) {
        int diceSize = diceFieldArray.Length;
        int diceIndex = index;
        List<int> nullIndexList = new List<int>();

        // 주사위 배열에서 null인 인덱스만 별도의 리스트에 저장
        for (int i=0; i<diceFieldArray.Length; i++) {
            if (diceFieldArray[i] == null)
                nullIndexList.Add(i);
        }

        // 인덱스가 배열 범위를 초과하면, null을 가진 인덱스 중 랜덤한 값을 저장
        if (diceIndex < 0 || diceIndex >= diceFieldArray.Length)
            diceIndex = nullIndexList[Random.Range(0, nullIndexList.Count)];    // null 인덱스 리스트에서 랜덤한 값을 저장함

        // 새로 생성할 주사위의 위치 계산 후, 주사위 생성
        float newDicePosX = marginX * (diceIndex % 5) + firstPosX;
        float newDicePosY = -marginY * (int)(diceIndex / 5) + firstPosY;
        diceFieldArray[diceIndex]
            = Instantiate(dicePrefab, new Vector3(newDicePosX, newDicePosY, 0), Quaternion.identity);        

        // 새로 생성한 주사위 종류 설정
        diceFieldArray[diceIndex].GetComponent<Dice>().diceStruct = deckArray[Random.Range(0, 5)];

        // 새로 생성한 주사위 레벨 설정
        diceFieldArray[diceIndex].GetComponent<Dice>().diceStruct.level = level;
    }

    // 주사위 스프라이트 불러오기
    public void loadDiceSprite(GameObject dice, string rarity, int index) {
        DiceSprites diceSpritesScript = GetComponent<DiceSprites>();
        List<Sprite> SpriteList;

        // 주사위의 등급에 따라 스프라이트 리스트 적용
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

        // 위에서 적용한 리스트에서 주사위 스프라이트 불러오기
        Dice diceScript = dice.GetComponent<Dice>();
        dice.GetComponent<SpriteRenderer>().sprite = SpriteList[index];
    }

    // 주사위 레벨(눈금)에 주사위 색상 적용
    public void loadDiceLevelColor(GameObject dice) {
        Dice diceScript = dice.GetComponent<Dice>();
        diceScript.levelText.GetComponent<TextMesh>().color = diceScript.diceStruct.color;
    }
}