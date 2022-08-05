using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    private static int MONSTER_SIZE = 12;

    public float corner1_Y = 0.0f;
    public float corner2_X = 2.05f;

    public GameObject monsterPrefab;
    public GameObject monsterSpawner;

    public GameObject monsterDataBaseConnector;
    public MonsterInfo.MonsterStruct [] monsterInfoArray = new MonsterInfo.MonsterStruct[MONSTER_SIZE];    // 몬스터 정보 배열
    public static string [] monsterDataText = new string[MONSTER_SIZE];   // DB에서 가져올 몬스터 정보

    public List<GameObject> monsterList = new List<GameObject>();   // 몬스터 리스트

    private void Start() {
        // DB에 연결하여 몬스터 ID에 해당하는 몬스터 정보를 가져옴
        MonsterDatabaseConnector dbConnector = monsterDataBaseConnector.GetComponent<MonsterDatabaseConnector>();
        for (int i=0; i<MONSTER_SIZE; i++)
            dbConnector.getMonsterInfoFromDatabase(i);
    }

    private void Update() {
        // DB에서 가져온 몬스터 정보를 덱에 적용
        for (int i=0; i<MONSTER_SIZE; i++)
            if (monsterDataText[i] != null)
                monsterInfoArray[i] = new MonsterInfo.MonsterStruct(monsterDataText[i]);
    }

    // 몬스터를 리스트에 추가
    public void addMonster(int monsterID, int monsterHp) {
        GameObject newMonster = Instantiate(monsterPrefab, monsterSpawner.transform.position, Quaternion.identity);
        newMonster.GetComponent<Monster>().monsterStruct = monsterInfoArray[monsterID];
        newMonster.GetComponent<Monster>().monsterStruct.hp = monsterHp;
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