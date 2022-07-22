using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private GameObject monsterManager;
    
    void Start() {
        monsterManager = GameObject.Find("MonsterManager");
    }

    void Update() {
        try {
            GameObject targetMonster = monsterManager.GetComponent<MonsterManager>().monsterList[0];
            Vector3 targetPosition = targetMonster.transform.position; 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
        } catch(ArgumentOutOfRangeException e) {
            Destroy(gameObject);
        }

        // 총알이 일정 범위를 벗어나면 제거
        if (isOutOfRange(transform.position))
            Destroy(gameObject);
    }

    // 총알이 일정 범위 밖으로 벗어나면 true 리턴
    private bool isOutOfRange(Vector3 pos) {
        if (pos.x > 3 || pos.y < -3 || pos.y > 2 || pos.y < -2)
            return true;
        return false;
    }
    
}