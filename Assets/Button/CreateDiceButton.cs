using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDiceButton : MonoBehaviour {
    public GameObject diceManager;
    public GameObject player;
    private DiceManager diceManagerScript;

    private void Start() {
        diceManagerScript = diceManager.GetComponent<DiceManager>();
    }

    private void OnMouseUp() {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript.sp >= playerScript.spCost) {
            diceManagerScript.createDice(-1, 1, -1);        // 랜덤한 주사위 생성
            playerScript.sp -= playerScript.spCost;     // SP 소모
            playerScript.spCost += 10;   // SP 비용 증가
        }
    }
}