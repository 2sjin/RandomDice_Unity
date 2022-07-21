using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] GameObject monster;
    private float time = 0.0f;

    void Start() {
        
    }

    void Update() {
        // 1초마다 몬스터 1마리씩 생성
        time += Time.deltaTime;
        if (time >= 1.0f) {
            Instantiate(monster);
            time = 0.0f;
        }
        

    }
}