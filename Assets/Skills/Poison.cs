using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {
    private float time = 0.0f;
    public float damage;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Monster") {
            transform.position = other.gameObject.transform.position;       
            time += Time.deltaTime;
            if (time > 1.0f) {
                other.GetComponent<Monster>().hp -= (int) damage;
                time = 0.0f;
            }
            if (other.GetComponent<Monster>().hp <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Monster")
            Destroy(gameObject);
    }
}