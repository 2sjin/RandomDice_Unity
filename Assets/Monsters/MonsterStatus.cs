using UnityEngine;

public class MonsterStatus : MonoBehaviour {
    private Monster monster;
    private float time = 0.0f;

    public bool isPoison = false;
    public float poisonDamage;

    public bool isFreeze = false;
    public float freezeEffect;

    private void Start() {
        monster = gameObject.GetComponent<Monster>();
    }

    private void Update() {
        time += Time.deltaTime;

        // 독
        if (time >= 1.0f) {
            if (isPoison)
                monster.hp -= (int) poisonDamage;
            time = 0.0f;
        }

        // 얼음
        if (isFreeze)
            monster.moveSpeed = 1 - (freezeEffect * 0.01f);
    }
}