using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    public GameObject monsterManager;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster") {
            other.GetComponent<Monster>().monsterStruct.hp = 0;
            player.GetComponent<Player>().life -= 1;   // 하트 1 감소
        }
        if (other.tag == "Skill")
            Destroy(other.gameObject);
    }
}