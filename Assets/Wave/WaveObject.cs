using UnityEngine;

public class WaveObject : MonoBehaviour {
    public int wave;
    public GameObject waveText;

    public int timer;               // 웨이브 메인 타이머
    public GameObject timerText;

    private float deltaTimer;        // deltaTime으로 1초 단위를 측정하는 타이머

    void Start() {
        wave = 1;
        deltaTimer = 0;
        timer = 90;
    }

    void Update() {
        // 현재 웨이브를 텍스트에 출력
        waveText.GetComponent<TextMesh>().text = wave.ToString();

        // 타이머의 시간을 텍스트에 출력(mm:ss)
        timerText.GetComponent<TextMesh>().text = Mathf.FloorToInt(timer / 60).ToString("D2") + ":" + (timer % 60).ToString("D2");

        // deltaTime 1초마다 타이머 1씩 감소
        deltaTimer += Time.deltaTime;
        if (deltaTimer >= 1.0f) {
            timer -= 1;
            deltaTimer -= 1.0f;
        }

        // 타이머 음수 방지
        if (timer < 0)
            timer = 0;

    }
}