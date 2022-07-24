using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private MonsterManager monsterManager;
    private SkillManager skillManager;
    public DiceInfo diceInfo;
    public int targetIndex;

    void Start() {
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        GetComponent<SpriteRenderer>().color = diceInfo.color;
    }

    void Update() {
        // 투사체 발사
        try {
            GameObject targetMonster = monsterManager.monsterList[targetIndex];
            Vector3 targetPosition = targetMonster.transform.position; 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
        } catch(ArgumentOutOfRangeException e) {
            Destroy(gameObject);
        }
    }


    // 몬스터와 투사체 충돌 시(트리거)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster") {
            Monster monster = other.GetComponent<Monster>(); 
            monster.hp -= (int) diceInfo.attackDamage;
            Destroy(gameObject);  // 총알 삭제

            // 스킬 발동
            switch(diceInfo.id) {
                case 0:     // 불 주사위
                    GameObject fire = Instantiate(skillManager.skillList[0]);
                    fire.transform.position = transform.position;
                    fire.GetComponent<Fire>().damage = diceInfo.special[0];
                    break;
                case 2:     // 독 주사위
                    monster.GetComponent<StatusManager>().poisonDamage = diceInfo.special[0];
                    monster.GetComponent<StatusManager>().isPoison = true;
                    break;
                case 4:     // 얼음 주사위
                    monster.GetComponent<StatusManager>().freezeEffect = diceInfo.special[0];
                    monster.GetComponent<StatusManager>().isFreeze = true;
                    break;
            }
        }
     }

    // 투사체가 일정 범위 밖으로 벗어나면 true 리턴
    private bool isOutOfRange(Vector3 pos) {
        if (pos.x > 3 || pos.y < -3 || pos.y > 2 || pos.y < -2)
            return true;
        return false;
    }
    
}