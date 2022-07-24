using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    [SerializeField] private GameObject monsterManager;
    
    private void OnTriggerEnter2D(Collider2D other) {
        other.GetComponent<Monster>().die();
    }
}