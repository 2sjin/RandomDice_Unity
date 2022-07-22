using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject monsterManager;
    [SerializeField] private GameObject monsterPrefab;
    private float time = 0.0f;

    void Update() {
        // 1초마다 몬스터 1마리씩 생성
        time += Time.deltaTime;
        if (time >= 1.0f) {
            GameObject newMonster = Instantiate(monsterPrefab);
            monsterManager.GetComponent<MonsterManager>().addMonster(newMonster);
            time = 0.0f;
        }
        

    }
}