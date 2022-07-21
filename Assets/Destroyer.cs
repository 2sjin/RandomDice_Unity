using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    [SerializeField] private GameObject gameManager;
    
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        gameManager.GetComponent<GameManager>().removeMonster(other.gameObject);
    }
}