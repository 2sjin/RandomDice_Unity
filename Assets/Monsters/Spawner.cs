using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject monsterManager;
    public GameObject waveObject;
    private WaveObject waveObjectScript;

    private int spawnHp;                  // 몬스터 스폰될 때 체력
    private float [] spawnTimer = new float[3];       // 몬스터 스폰 타이머(common, speed, big)

    void Start() {
        waveObjectScript = waveObject.GetComponent<WaveObject>();
        spawnHp = 100;
        spawnTimer[0] = 0.0f;
        spawnTimer[1] = -5.0f;
        spawnTimer[2] = -10.0f;
    }

    void Update() {
        // 스폰 체력 설정
        if (waveObjectScript.timer > 60)
            spawnHp = 100 * waveObjectScript.wave;
        else
            spawnHp = 100 * waveObjectScript.wave * (6 - (int)(waveObjectScript.timer / 10));


        // 타이머를 deltaTime만큼 증가
        for (int i=0; i<spawnTimer.Length; i++)
            spawnTimer[i] += Time.deltaTime;

        // 주기적으로 몬스터 생성
        if (spawnTimer[0] >= 2.0f) {
            // big
            if (spawnTimer[2] >= 10.0f) {
                monsterManager.GetComponent<MonsterManager>().addMonster(2, spawnHp * 5);
                spawnTimer[2] = 0.0f;
            }
            // speed
            else if (spawnTimer[1] >= 10.0f) {
                monsterManager.GetComponent<MonsterManager>().addMonster(1, (int)(spawnHp / 2));
                spawnTimer[1] = 0.0f;
            }
            // common
            else {
                monsterManager.GetComponent<MonsterManager>().addMonster(0, spawnHp);
            }
            spawnTimer[0] = 0.0f;
        }
    }
}