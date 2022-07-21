using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMonster : MonoBehaviour {
    private GameObject gameManager;
    [SerializeField] private Vector3 direction = Vector3.zero;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int hp;

    void Start() {
        gameManager = GameObject.Find("GameManager");
        direction = Vector3.up;     // 몬스터의 초기 방향은 위쪽
    }

    void Update() {
        // 모퉁이에서 몬스터의 방향 전환
        if (transform.position.y >= 1) {
            if (transform.position.x >= 2)
                direction = Vector3.down;   // 두 번째 모퉁이에서 방향 전환
            else
                direction = Vector3.right;  // 첫 번째 모퉁이에서 방향 전환
        }

        // 체력이 0 이하이면 몬스터 제거
        if (hp <= 0) {
            Destroy(gameObject);      // 몬스터 오브젝트 제거
            gameManager.GetComponent<GameManager>().removeMonster(gameObject);  // 몬스터 리스트에서 몬스터 제거
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // 몬스터와 총알 충돌 시(트리거)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            hp -= 100;
            Destroy(other.gameObject);  // 총알 삭제
        }
     }
}