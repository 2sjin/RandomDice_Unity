using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject monsterManager;

    private float time = 0.0f;

    void Update() {
        // 일정 주기마다 몬스터 1마리씩 생성
        time += Time.deltaTime;
        if (time >= 0.5f) {
            monsterManager.GetComponent<MonsterManager>().addMonster();
            time = 0.0f;
        }
        

    }
}