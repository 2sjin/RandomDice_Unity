using System;
using UnityEngine;

public class Dice : MonoBehaviour {
    private GameObject diceManager;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject levelText;
    private float attackTime = 0.0f;    // 공격 주기(초)
    private Vector3 currentPosition;    // 주사위의 현재 좌표(드래그 후 원위치)
    private Collider2D colliderDice;
    [SerializeField] private int level = 1;
    [SerializeField] public int type = 0;

    private bool isTrigger = false;

    private void Start() {
        diceManager = GameObject.Find("DiceManager");
        levelText = Instantiate(levelText);
    }

    private void Update() {
        updateLevelText();
        attack();
        diceManager.GetComponent<DiceManager>().applyDiceType(gameObject);
    }

    // 기본 공격
    private void attack() {
        attackTime += Time.deltaTime;
        if (attackTime >= 0.5f) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            attackTime = 0.0f;
        }
    }

    // LevelText 갱신
    private void updateLevelText() {
        levelText.GetComponent<TextMesh>().text = level.ToString();
        levelText.transform.position = transform.position;
    }

    // 레벨 설정
    public void setLevel(int level) {
        this.level = level;
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
        if (level != other.GetComponent<Dice>().level || level >= 7)
            return;

        // 두 주사위의 종류가 다를 경우 합성 없이 리턴
        if (type != other.GetComponent<Dice>().type)
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
        diceManagerScript.createDice(otherGameObjectIndex, level+1);
    }
}


