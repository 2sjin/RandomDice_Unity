using UnityEngine;

// 데미지 표시 이펙트

public class DamageTextPrefab : MonoBehaviour {
    private float timer;
    private float initialPosY;

    private void Start() {
        timer = 0.0f;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z);
        initialPosY = transform.position.y;
    }

    private void Update() {
        if (transform.position.y > initialPosY + 0.3f)
            Destroy(gameObject);
        
        timer += Time.deltaTime;
        if (timer >= 0.01f) {
            transform.position += new Vector3(0, 0.005f, 0);
            Color textColor = GetComponent<TextMesh>().color;
            GetComponent<TextMesh>().color = new Color(textColor.r, textColor.g, textColor.b, textColor.a - 0.01f);
            timer -= 0.01f;
        }
    }
}