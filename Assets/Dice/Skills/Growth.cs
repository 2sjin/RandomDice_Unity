using System;
using UnityEngine;

public class Growth : MonoBehaviour {
    private Dice dice;
    private float time;

    void Start() {
        dice = gameObject.GetComponent<Dice>();
    }

    void Update() {
        time += Time.deltaTime;
        if (time >= dice.diceStruct.s0) {   // 성장 대기 시간이 끝나면
            DiceManager diceManagerScript = GameObject.Find("DiceManager").GetComponent<DiceManager>();

            // 현재 주사위의 정보 저장
            int diceIndex = Array.IndexOf(diceManagerScript.diceFieldArray, gameObject);
            int diceId = dice.diceStruct.id;
            int diceLevel = dice.diceStruct.level;
            float diceS1 = dice.diceStruct.s1;

            // 기존 주사위 제거
            dice.destroyDice();

            switch (diceId) {
                case 7:    // 도박 성장 주사위: 같은 자리에 랜덤 주사위 생성
                    diceManagerScript.createDice(diceIndex, UnityEngine.Random.Range(1, 8), -1);
                    break;
                case 8:    // 고장난 성장 주사위: 같은 자리에 +1 또는 -1 주사위 생성
                    int randomNum = UnityEngine.Random.Range(0, 100);
                    // 성장 실패
                    if (randomNum < diceS1)
                        if (diceLevel > 1)     // 2눈금 이상 -> 1눈금 감소
                            diceManagerScript.createDice(diceIndex, diceLevel-1, -1);
                    // 성장 성공
                    else if (diceLevel < 7)      // 7눈금 미만일 경우 성장
                        diceManagerScript.createDice(diceIndex, diceLevel+1, -1);
                    break;
                case 9:    // 성장 주사위: 같은 자리에 +1 주사위 생성
                    if (diceLevel < 7)
                        diceManagerScript.createDice(diceIndex, diceLevel+1, -1);
                    break;
            }
        }        
    }
}