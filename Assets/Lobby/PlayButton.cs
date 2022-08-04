using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    public Text [] diceIdText = new Text[5];

    public void play() {
        for (int i=0; i<5; i++)
            DiceManager.deckIdArray[i] = int.Parse(diceIdText[i].text);

        SceneManager.LoadScene("PlayScene");
    }
}