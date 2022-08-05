using UnityEngine;

public class Wave : MonoBehaviour {
    public int wave;
    public GameObject waveText;

    public int timer;
    public float deltaTimer;
    public GameObject timerText;

    void Start() {
        wave = 1;
        deltaTimer = 0;
        timer = 90;
    }

    void Update() {
        waveText.GetComponent<TextMesh>().text = wave.ToString();
        timerText.GetComponent<TextMesh>().text = timer.ToString();

        // deltaTime 1초마다 타이머 1씩 감소
        deltaTimer += Time.deltaTime;
        if (deltaTimer >= 1.0f) {
            timer -= 1;
            deltaTimer -= 1.0f;
        }

    }
}