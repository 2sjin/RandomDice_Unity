using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    public Text [] text = new Text[5];

    public void play() {
        for (int i=0; i<5; i++)
            DiceManager.deckIdArray[i] = int.Parse(text[i].text);

        SceneManager.LoadScene("PlayScene");
    }
}