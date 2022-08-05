using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private MonsterManager monsterManager;
    private SkillManager skillManager;
    public GameObject damageTextPrefab;
    public DiceInfo.DiceStruct diceStruct;
    public int targetIndex;

    void Start() {
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        GetComponent<SpriteRenderer>().color = diceStruct.color;
    }

    void Update() {
        // 필드에 몬스터가 없으면 투사체 소멸
        if (monsterManager.monsterList.Count == 0)
            Destroy(gameObject);

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

            // 도박 주사위 랜덤 데미지
            if (diceStruct.id == 6) {
                diceStruct.attackDamage = UnityEngine.Random.Range(diceStruct.attackDamage, diceStruct.attackDamage*25+1);
            }

            monster.monsterStruct.hp -= (int) diceStruct.attackDamage;
            GameObject damageText = Instantiate(damageTextPrefab, monster.transform.position, Quaternion.identity);
            damageText.GetComponent<TextMesh>().text = ((int) diceStruct.attackDamage).ToString();

            Destroy(gameObject);  // 총알 삭제

            // 스킬 발동
            switch(diceStruct.id) {
                case 0:     // 불 주사위
                    GameObject fire = Instantiate(skillManager.skillList[0]);
                    fire.transform.position = transform.position;
                    fire.GetComponent<Fire>().damage = diceStruct.s0;
                    break;
                case 1:     // 전기 주사위
                    GameObject electricMonster;
                    for (int i=0; i<3; i++) {                        
                        try {
                            electricMonster = monsterManager.monsterList[i].gameObject;                            
                            GameObject electric = Instantiate(skillManager.skillList[1]);
                            electric.transform.position = electricMonster.transform.position;
                            electric.GetComponent<Fire>().damage = diceStruct.s0;
                        } catch(ArgumentOutOfRangeException e) {
                            break;
                        }
                    }
                    break;
                case 2:     // 독 주사위
                    monster.GetComponent<MonsterStatus>().poisonDamage = diceStruct.s0;
                    monster.GetComponent<MonsterStatus>().isPoison = true;
                    break;
                case 4:     // 얼음 주사위
                    monster.GetComponent<MonsterStatus>().freezeEffect = diceStruct.s0;
                    monster.GetComponent<MonsterStatus>().isFreeze = true;
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