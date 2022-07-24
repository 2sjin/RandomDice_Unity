using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster") {
            other.GetComponent<Monster>().hp -= (int) damage;
            Destroy(gameObject);
        }
    }
}