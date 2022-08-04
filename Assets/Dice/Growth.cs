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

            // 기존 주사위 제거
            int diceIndex = Array.IndexOf(diceManagerScript.diceFieldArray, gameObject);
            dice.destroyDice();

            switch (dice.diceStruct.id) {
                case 7:    // 도박 성장 주사위: 같은 자리에 랜덤 주사위 생성
                    diceManagerScript.createDice(diceIndex, UnityEngine.Random.Range(1, 8));
                    break;
                case 8:    // 고장난 성장 주사위: 같은 자리에 +1 또는 -1 주사위 생성
                    int randomNum = UnityEngine.Random.Range(0, 100);
                    // 성장 실패
                    if (randomNum < dice.diceStruct.s1)
                        if (dice.diceStruct.level > 1)     // 2눈금 이상 -> 1눈금 감소
                            diceManagerScript.createDice(diceIndex, dice.diceStruct.level - 1);
                    // 성장 성공
                    else if (dice.diceStruct.level < 7)      // 7눈금 미만일 경우 성장
                        diceManagerScript.createDice(diceIndex, dice.diceStruct.level + 1);
                    break;
                case 9:    // 성장 주사위: 같은 자리에 +1 주사위 생성
                    if (dice.diceStruct.level < 7)
                        diceManagerScript.createDice(diceIndex, dice.diceStruct.level + 1);
                    break;

            }
        }        
    }
}