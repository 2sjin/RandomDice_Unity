using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    [SerializeField] private GameObject monsterPrefab;
    public List<GameObject> monsterList = new List<GameObject>();   // 몬스터 리스트

    // 몬스터를 리스트에 추가
    public void addMonster() {
        GameObject newMonster = Instantiate(monsterPrefab);
        monsterList.Add(newMonster);
    }

    // 몬스터를 리스트에서 삭제
    public void removeMonster(GameObject monster) {
        monsterList.Remove(monster);
    }
}