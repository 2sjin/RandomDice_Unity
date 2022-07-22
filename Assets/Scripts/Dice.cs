using System;
using UnityEngine;

public class Dice : MonoBehaviour {
    private GameObject diceManager;
    [SerializeField] private GameObject bulletPrefab;
    private float attackTime = 0.0f;    // 공격 주기(초)
    private Vector3 currentPosition;    // 주사위의 현재 좌표(드래그 후 원위치)
    
    void Start() {
        diceManager = GameObject.Find("DiceManager");
    }

    void Update() {
        Attack();
    }

    void Attack() {
        attackTime += Time.deltaTime;
        if (attackTime >= 0.5f) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            attackTime = 0.0f;
        }
    }

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
        // 주사위 원위치
        transform.position = currentPosition;
        // 레이어 순서 원래대로
        GetComponent<SpriteRenderer>().sortingOrder -= 1;
        tag = "Dice";
    }

    // 다음 코드는 한 번만 작성하였지만, 합성할 양쪽 주사위에 대한 트리거가 모두 실행됨
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Dice") {
            DiceManager diceManagerScript = diceManager.GetComponent<DiceManager>();

            // 합성할 두 주사위의 배열 인덱스 구하기
            int gameObjectIndex = Array.IndexOf(diceManagerScript.diceArray, gameObject);
            int otherGameObjectIndex = Array.IndexOf(diceManagerScript.diceArray, other.gameObject);
            // 배열에서 주사위 제거(null 값으로 변경)  
            diceManagerScript.diceArray[gameObjectIndex] = null;
            diceManagerScript.diceArray[otherGameObjectIndex] = null;
            // 주사위 오브젝트 제거
            Destroy(gameObject);
            Destroy(other.gameObject);

            // 새로운 주사위 생성
            diceManagerScript.createDice(otherGameObjectIndex);
        }
    }
}