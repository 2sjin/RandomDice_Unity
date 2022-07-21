using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private GameObject gameManager;
    
    void Start() {
        gameManager = GameObject.Find("GameManager");
    }

    void Update() {
        try {
            GameObject targetMonster = gameManager.GetComponent<GameManager>().monsterList[0];
            Vector3 targetPosition = targetMonster.transform.position; 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
        } catch(ArgumentOutOfRangeException e) {
            Destroy(gameObject);
        }

        if (transform.position.x > 3 || transform.position.y < -3 ||
            transform.position.y > 2 || transform.position.y < -2)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster") {
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameManager.GetComponent<GameManager>().removeMonster(other.gameObject);
        }
    }
}