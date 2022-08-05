using System;
using UnityEngine;

public class Dice : MonoBehaviour {
    private GameObject diceManager;
    private GameObject monsterManager;

    public GameObject bulletPrefab;
    public GameObject levelText;    // 레벨(눈금)
    public GameObject bulletPoint;  // 투사체 시작점(주사위를 드래그해도 변하지 않음)
    public DiceInfo.DiceStruct diceStruct;
    
    private float attackCooltime = 0.0f;    // 공격 주기(초)
    private Vector3 currentPosition;    // 주사위의 현재 좌표(드래그 후 원위치할 좌표)
    private Collider2D colliderDice;
    private bool isTrigger = false;

    private void Start() {
        diceManager = GameObject.Find("DiceManager");
        monsterManager = GameObject.Find("MonsterManager");
        levelText = Instantiate(levelText, transform.position, Quaternion.identity);     // 레벨(눈금) 텍스트 생성
        bulletPoint = Instantiate(bulletPoint, transform.position, Quaternion.identity); // 투사체 시작점 생성

        // 바람 주사위: 공격속도 증가
        if (diceStruct.name == "바람")
            diceStruct.attackSpeed = diceStruct.attackSpeed * (1 - (diceStruct.s0 * 0.01f));

        // 성장류 주사위
        if (diceStruct.name == "성장" || diceStruct.name == "고장난 성장" || diceStruct.name == "도박 성장")
            gameObject.AddComponent<Growth>();
    }

    private void Update() {
        updateLevelText();
        diceManager.GetComponent<DiceManager>().loadDiceLevelColor(gameObject);
        diceManager.GetComponent<DiceManager>().loadDiceSprite(gameObject, diceStruct.rarity, diceStruct.spriteID);

        // 기본공격
        attackCooltime += Time.deltaTime;
        if (attackCooltime >= diceStruct.attackSpeed / diceStruct.level) {   // (공격속도 / 눈금 수) 마다 한번씩 공격
            attack();
            attackCooltime = 0.0f;
        }
    }

    // 기본 공격
    private void attack() {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.gameObject.transform.position, Quaternion.identity); // 투사체 생성
        bullet.GetComponent<Bullet>().diceStruct = diceStruct;  // 주사위 정보 전달
        bullet.GetComponent<Bullet>().targetIndex = 0;      // 타겟 인덱스 전달
        if (diceStruct.target == "random") {   // 랜덤일 경우의 타겟 인덱스 전달
            int monsterCount = monsterManager.GetComponent<MonsterManager>().monsterList.Count;
            bullet.GetComponent<Bullet>().targetIndex = UnityEngine.Random.Range(0, monsterCount);
        }
    }

    // LevelText 갱신
    private void updateLevelText() {
        levelText.GetComponent<TextMesh>().text = diceStruct.level.ToString();
        levelText.transform.position = transform.position;
    }

    // 주사위 파괴
    public void destroyDice() {
        DiceManager diceManagerScript = diceManager.GetComponent<DiceManager>();
        int gameObjectIndex = Array.IndexOf(diceManagerScript.diceFieldArray, gameObject);
        diceManagerScript.diceFieldArray[gameObjectIndex] = null;
        Destroy(gameObject);    // 주사위 오브젝트 제거
        Destroy(levelText);     // 주사위 눈금 제거
        Destroy(bulletPoint);   // 투사체 시작점 제거
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnTriggerStay2D(Collider2D other) {
        colliderDice = other;
        isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        colliderDice = null;
        isTrigger = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnMouseDown() {
        // 원위치할 좌표 정보를 임시로 저장
        currentPosition = transform.position;
        // 드래그할 주사위가 다른 주사위보다 항상 앞으로 오도록 레이어 순서 1 증가
        GetComponent<SpriteRenderer>().sortingOrder += 1;
        tag = "SelectedDice";
    }

    private void OnMouseDrag() {
        // 마우스를 드래그하는 동안 주사위 오브젝트도 함께 이동
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    private void OnMouseUp() {
        // 주사위의 위치가 겹치면 주사위 합성 수행
        if (isTrigger) 
            combine(colliderDice);
        // 주사위 원위치
        transform.position = currentPosition;
        // 레이어 순서 원래대로
        GetComponent<SpriteRenderer>().sortingOrder -= 1;
        tag = "Dice";
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 주사위 합성
    private void combine(Collider2D other) {
        if (other.tag != "Dice")
            return;

        DiceManager diceManagerScript = diceManager.GetComponent<DiceManager>();

        // 합성할 두 주사위의 배열 인덱스 구하기
        int gameObjectIndex = Array.IndexOf(diceManagerScript.diceFieldArray, gameObject);
        int otherGameObjectIndex = Array.IndexOf(diceManagerScript.diceFieldArray, other.gameObject);

        // other 주사위의 구조체 구하기
        DiceInfo.DiceStruct otherDiceStruct = other.GetComponent<Dice>().diceStruct;

        // 두 주사위의 눈금이 다르면, 합성 없이 리턴
        if (diceStruct.level != otherDiceStruct.level)
            return;

        // 조커 주사위
        if (diceStruct.name == "조커" && otherDiceStruct.name != "조커") {      // 둘 다 조커 주사위면 복사 없이 합성
            // 조커 주사위 제거
            diceManagerScript.diceFieldArray[gameObjectIndex] = null;
            Destroy(gameObject);
            Destroy(GetComponent<Dice>().levelText);
            // 복사한 주사위를 조커 주사위 자리에 생성
            for (int i=0; i<5; i++) {
                PowerUpButton deck = GameObject.Find("PowerUpButton" + i.ToString()).GetComponent<PowerUpButton>();
                if (otherDiceStruct.id == deck.diceId)
                    diceManagerScript.createDice(gameObjectIndex, diceStruct.level, i);
            }
            return;
        }

        // 눈금이 최대치(7)일 경우 합성 없이 리턴
        if (diceStruct.level >= 7)
            return;

        // 두 주사위의 종류가 다를 경우 합성 없이 리턴
        if (diceStruct.id != otherDiceStruct.id)
            return;

        // 배열에서 주사위 제거(null 값으로 변경)
        try {
            diceManagerScript.diceFieldArray[gameObjectIndex] = null;
            diceManagerScript.diceFieldArray[otherGameObjectIndex] = null;
        } catch(IndexOutOfRangeException e) {
            isTrigger = false;
            return;
        }
        // 주사위 오브젝트 제거
        Destroy(gameObject);
        Destroy(other.gameObject);
        // 주사위 LevelText 오브젝트 제거
        Destroy(GetComponent<Dice>().levelText);
        Destroy(other.GetComponent<Dice>().levelText);

        // 새로운 주사위 1개 생성
        diceManagerScript.createDice(otherGameObjectIndex, diceStruct.level+1, -1);

    }
}


