using UnityEngine;

// 주사위 생성 시 스프라이트가 점점 커지는 이펙트 구현

public class PopEffect : MonoBehaviour {
    float timer;

    void Start() {
        timer = 0.0f;
        gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0);
    }

    void Update() {
        if (gameObject.transform.localScale.x >= 0.33)
            Destroy(this);

        timer += Time.deltaTime;
        if (timer >= 0.005f) {
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            timer -= 0.005f;
        }
    }
}