using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDiceButton : MonoBehaviour {
    [SerializeField] private GameObject diceManager;
    private DiceManager diceManagerScript;

    private void Start() {
        diceManagerScript = diceManager.GetComponent<DiceManager>();
    }

    private void OnMouseUp() {
        diceManagerScript.createDice(-1);     // 랜덤한 주사위 생성
    }
}