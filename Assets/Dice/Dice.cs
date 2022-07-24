using System;
using UnityEngine;

public class Dice : MonoBehaviour {
    private GameObject diceManager;
    private GameObject monsterManager;

    public GameObject bulletPrefab;
    public GameObject levelText;
    public DiceInfo.DiceStruct diceStruct;
    
    private float attackCooltime = 0.0f;    // 공격 주기(초)
    private Vector3 currentPosition;    // 주사위의 현재 좌표(드래그 후 원위치할 좌표)
    private Collider2D colliderDice;
    private bool isTrigger = false;

    private void Start() {
        diceManager = GameObject.Find("DiceManager");
        monsterManager = GameObject.Find("MonsterManager");
        levelText = Instantiate(levelText);
    }

    private void Update() {
        updateLevelText();
        diceManager.GetComponent<DiceManager>().applyDiceType(gameObject);

        // 바람 주사위: 공격속도 증가
        if (diceStruct.id == 3)
            diceStruct.attackSpeed = diceStruct.attackSpeed * (1 - (diceStruct.s0 * 0.01f));

        attackCooltime += Time.deltaTime;
        if (attackCooltime >= diceStruct.attackSpeed / diceStruct.level) {   // (공격속도 / 눈금 수) 마다 한번씩 공격
            attack();
            attackCooltime = 0.0f;
        }
    }

    // 기본 공격
    private void attack() {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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

    // 레벨 설정
    public void setLevel(int level) {
        diceStruct.level = level;
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
        int gameObjectIndex = Array.IndexOf(diceManagerScript.diceArray, gameObject);
        int otherGameObjectIndex = Array.IndexOf(diceManagerScript.diceArray, other.gameObject);

        // 두 주사위의 눈금이 다르거나, 눈금이 최대치(7)일 경우 합성 없이 리턴
        if (diceStruct.level != other.GetComponent<Dice>().diceStruct.level || diceStruct.level >= 7)
            return;

        // 두 주사위의 종류가 다를 경우 합성 없이 리턴
        if (diceStruct.id != other.GetComponent<Dice>().diceStruct.id)
            return;

        // 배열에서 주사위 제거(null 값으로 변경)
        try {
            diceManagerScript.diceArray[gameObjectIndex] = null;
            diceManagerScript.diceArray[otherGameObjectIndex] = null;
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
        diceManagerScript.createDice(otherGameObjectIndex, diceStruct.level+1);
    }
}


