using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    [SerializeField] private GameObject monsterManager;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster")
            other.GetComponent<Monster>().hp = 0;
        if (other.tag == "Skill")
            Destroy(other.gameObject);
    }
}