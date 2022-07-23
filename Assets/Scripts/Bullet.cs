using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private GameObject monsterManager;
    public DiceInfo diceInfo;
    public int targetIndex;

    void Start() {
        monsterManager = GameObject.Find("MonsterManager");
        GetComponent<SpriteRenderer>().color = diceInfo.color;
    }

    void Update() {
        // 투사체 발사
        try {
            GameObject targetMonster = monsterManager.GetComponent<MonsterManager>().monsterList[targetIndex];
            Vector3 targetPosition = targetMonster.transform.position; 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
        } catch(ArgumentOutOfRangeException e) {
            Destroy(gameObject);
        }
        
        /*
            // 투사체가 일정 범위를 벗어나면 제거
            if (isOutOfRange(transform.position))
                Destroy(gameObject);
        */
    }


    // 몬스터와 투사체 충돌 시(트리거)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster") {
            other.GetComponent<Monster>().hp -= (int) diceInfo.attackDamage;
            Destroy(gameObject);  // 총알 삭제
        }
     }

    // 투사체가 일정 범위 밖으로 벗어나면 true 리턴
    private bool isOutOfRange(Vector3 pos) {
        if (pos.x > 3 || pos.y < -3 || pos.y > 2 || pos.y < -2)
            return true;
        return false;
    }
    
}