using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    private GameObject monsterManager;
    [SerializeField] private Vector3 direction = Vector3.zero;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int hp;
    [SerializeField] private GameObject hpText;

    private void Start() {
        monsterManager = GameObject.Find("MonsterManager");
        hpText = Instantiate(hpText);
        direction = Vector3.up;     // 몬스터의 초기 방향은 위쪽
    }

    private void Update() {
        updateHpText();

        if (hp <= 0)    // 체력이 0 이하이면
            die();      // 몬스터 제거

        move();     // 몬스터 이동
    }

    // 몬스터와 총알 충돌 시(트리거)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            hp -= 100;
            Destroy(other.gameObject);  // 총알 삭제
        }
     }

    // HpText 갱신 및 이동
    private void updateHpText() {
        hpText.GetComponent<TextMesh>().text = hp.ToString();
        hpText.gameObject.transform.position = transform.position;  // HpText의 이동
    }

    // 몬스터 이동
     private void move() {        
        if (transform.position.y >= 1) {
            if (transform.position.x >= 2)
                direction = Vector3.down;   // 두 번째 모퉁이를 만났을 때의 방향 전환
            else
                direction = Vector3.right;  // 첫 번째 모퉁이를 만났을 때의 방향 전환
        }

        transform.position += direction * moveSpeed * Time.deltaTime;   // 몬스터 이동
     }

     // 몬스터 사망 처리
     private void die() {
        Destroy(gameObject);      // 몬스터 오브젝트 제거
        Destroy(hpText);          // HpText 오브젝트 제거
        monsterManager.GetComponent<MonsterManager>().removeMonster(gameObject);  // 몬스터 리스트에서 몬스터 제거
     }
}