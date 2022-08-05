using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject monsterManager;
    private int spawnHp;                  // 몬스터 스폰될 때 체력
    private float increaseHpTimer;        // 스폰 체력 증가 타이머
    private float [] spawnTimer = new float[3];       // 몬스터 스폰 타이머(common, speed, big)

    void Start() {
        spawnHp = 100;
        increaseHpTimer = -20.0f;
        spawnTimer[0] = 0.0f;
        spawnTimer[1] = -10.0f;
        spawnTimer[2] = -10.0f;
    }

    void Update() {
        // 타이머를 deltaTime만큼 증가
        increaseHpTimer += Time.deltaTime;
        for (int i=0; i<spawnTimer.Length; i++)
            spawnTimer[i] += Time.deltaTime;

        // 주기적으로 몬스터 생성
        if (spawnTimer[0] >= 2.0f) {
            monsterManager.GetComponent<MonsterManager>().addMonster(0, spawnHp);
            spawnTimer[0] = 0.0f;
        }
        if (spawnTimer[1] >= 5.0f) {
            monsterManager.GetComponent<MonsterManager>().addMonster(1, (int)(spawnHp / 2));
            spawnTimer[1] = 0.0f;
        }
        if (spawnTimer[2] >= 11.0f) {
            monsterManager.GetComponent<MonsterManager>().addMonster(2, spawnHp * 5);
            spawnTimer[2] = 0.0f;
        }


        // 10초마다 스폰 체력 증가
        if (increaseHpTimer >= 10.0f) {
            spawnHp += 100;
            increaseHpTimer = 0.0f;
        }
    }
}