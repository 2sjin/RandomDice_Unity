using UnityEngine;

public class Wave : MonoBehaviour {
    public int wave;
    public GameObject waveText;

    void Start() {
        wave = 1;
    }

    void Update() {
        waveText.GetComponent<TextMesh>().text = wave.ToString();
    }
}