using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject monsterManager;
    private int spawnHp;        // 몬스터 스폰될 때 체력
    private float spawnTimer;       // 스폰 타이머
    private float increaseHpTimer;  // 스폰 체력 증가 타이머

    void Start() {
        spawnHp = 100;
        spawnTimer = 0.0f;
    }

    void Update() {
        spawnTimer += Time.deltaTime;
        increaseHpTimer += Time.deltaTime;

        // 일정 주기마다 몬스터 1마리씩 생성
        if (spawnTimer >= 1.0f) {
            // 10초마다 빅 몬스터 생성 및 스폰 체력 증가
            if (increaseHpTimer >= 10.0f) {
                monsterManager.GetComponent<MonsterManager>().addMonster(spawnHp * 5);
                spawnHp += 100;
                increaseHpTimer = 0.0f;
            }
            // 일반 몬스터 생성
            else {
                monsterManager.GetComponent<MonsterManager>().addMonster(spawnHp);
            }
            // 타이머 초기화
            spawnTimer = 0.0f;
        }



    }
}