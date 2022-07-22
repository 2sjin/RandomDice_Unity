using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<GameObject> monsterList = new List<GameObject>();   // 몬스터 리스트
    public GameObject [] diceArray = new GameObject[15];      // 주사위 배열

    // 몬스터를 리스트에 추가
    public void addMonster(GameObject monster) {
        monsterList.Add(monster);
    }

    // 몬스터를 리스트에서 삭제
    public void removeMonster(GameObject monster) {
        monsterList.Remove(monster);
    }

}