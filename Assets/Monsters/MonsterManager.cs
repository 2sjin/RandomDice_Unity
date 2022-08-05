using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    public float corner1_Y = 0.0f;
    public float corner2_X = 2.05f;

    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawner;

    public List<GameObject> monsterList = new List<GameObject>();   // 몬스터 리스트

    // 몬스터를 리스트에 추가
    public void addMonster(int monsterHp) {
        GameObject newMonster = Instantiate(monsterPrefab, monsterSpawner.transform.position, Quaternion.identity);
        newMonster.GetComponent<Monster>().hp = monsterHp;
        monsterList.Add(newMonster);
    }

    // 몬스터를 리스트에서 삭제
    public void removeMonster(GameObject monster) {
        monsterList.Remove(monster);
    }

    // 몬스터 스프라이트 불러오기
    public void loadMonsterSprite(GameObject monster, int spriteNum) {
        MonsterSprites monsterSpritesScript = GetComponent<MonsterSprites>();
        Dice diceScript = monster.GetComponent<Dice>();
        monster.GetComponent<SpriteRenderer>().sprite = monsterSpritesScript.Sprites[spriteNum];
    }
}