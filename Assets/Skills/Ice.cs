using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
    public float freezeEffect;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Monster") {
            Monster monster = other.GetComponent<Monster>();
            transform.position = other.gameObject.transform.position;
            monster.moveSpeed *= 1 - (freezeEffect * 0.01f);

            if (monster.hp <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Monster")
            Destroy(gameObject);
    }
}