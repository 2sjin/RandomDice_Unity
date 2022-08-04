using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour {
    public int deckIndex;
    public GameObject diceManager;
    DiceInfo.DiceStruct [] deckArray;    // 주사위 덱 정보
    public int diceId;

    void Start() {
        deckArray = diceManager.GetComponent<DiceManager>().deckArray;
    }

    void Update() {
        diceId = deckArray[deckIndex].id;
        diceManager.GetComponent<DiceManager>().loadDiceSprite(gameObject, deckArray[deckIndex].rarity, deckArray[deckIndex].spriteID);
    }
}