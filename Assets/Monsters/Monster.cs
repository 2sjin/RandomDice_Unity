using UnityEngine;

public class Monster : MonoBehaviour {
    private GameObject monsterManager;

    [SerializeField] private Vector3 direction = Vector3.zero;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int hp;
    [SerializeField] private GameObject hpText;

    private void Start() {
        monsterManager = GameObject.Find("MonsterManager");
        hpText = Instantiate(hpText);
        direction = Vector3.up;     // 몬스터의 초기 방향은 위쪽
    }

    private void Update() {
        updateHpText();         // 몬스터 체력 갱신
        if (hp <= 0) die();     // 체력 없으면 사망 처리
        move();                 // 몬스터 이동
    }

    // HpText 갱신 및 이동
    private void updateHpText() {
        hpText.GetComponent<TextMesh>().text = hp.ToString();
        hpText.gameObject.transform.position = transform.position;  // HpText의 이동
    }

    // 몬스터 이동
     private void move() {
        if (transform.position.y >= monsterManager.GetComponent<MonsterManager>().corner1_Y) {
            if (transform.position.x >= monsterManager.GetComponent<MonsterManager>().corner2_X)
                direction = Vector3.down;   // 두 번째 모퉁이를 만났을 때의 방향 전환
            else
                direction = Vector3.right;  // 첫 번째 모퉁이를 만났을 때의 방향 전환
        }
        transform.position += direction * moveSpeed * Time.deltaTime;   // 몬스터 이동
     }

     // 몬스터 사망 처리
     public void die() {
        hp = 0;
        Destroy(gameObject);      // 몬스터 오브젝트 제거
        Destroy(hpText);          // HpText 오브젝트 제거
        monsterManager.GetComponent<MonsterManager>().removeMonster(gameObject);  // 몬스터 리스트에서 몬스터 제거
     }
}