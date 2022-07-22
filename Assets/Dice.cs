using System;
using UnityEngine;

public class Dice : MonoBehaviour {
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject bulletPrefab;
    private float attackTime = 0.0f;    // 공격 주기(초)
    private Vector3 currentPosition;    // 주사위의 현재 좌표(드래그 후 원위치)

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
    }

    private void OnMouseDrag() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    private void OnMouseUp() {
        // 주사위 원위치
        transform.position = currentPosition;
        // 레이어 순서 원래대로
        GetComponent<SpriteRenderer>().sortingOrder -= 1;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Dice") {
            Debug.Log("합성!");
        }
    }

}