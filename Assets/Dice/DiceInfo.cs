using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DiceInfo : MonoBehaviour {
    public string selectURL = "http://152.70.94.65/random_dice/select_dice.php";   // PHP 스크립트

    // 구조체를 사용하지 않으면, 필드 내 같은 종류의 주사위는 무조건 같은 눈금이 적용됨
    public struct DiceStruct {
        public int id;  // 주사위 ID
        public float attackDamage;  // 기본 공격력
        public float attackSpeed;   // 공격 속도(초)
        public string target;       // 타겟

        public float s0;
        public float s1;
        public float s2;

        public string rarity;   // 희귀도
        public int spriteID;    // 스프라이트 ID
        public Color32 color;   // 색상
        
        public int level;

        public DiceStruct(int id, float damage, float speed, string target, float s0, float s1, float s2, Color32 color,
                          string rarity, int spriteID) {
            this.id = id;
            this.attackDamage = damage;
            this.attackSpeed = speed;
            this.target = target;
            this.s0 = s0;
            this.s1 = s1;
            this.s2 = s2;
            this.color = color;
            this.rarity = rarity;
            this.spriteID = spriteID;

            this.level = 1;
        }
    }

    // 데이터베이스에서 주사위 정보 가져오기(SELECT)
    public void select() {
        StartCoroutine(getDiceInfoFromDatabase());
    }


// 데이터베이스에서 주사위 정보 가져오기(SELECT)
    IEnumerator getDiceInfoFromDatabase(){
        // 데이터 POST 전송
        WWWForm form = new WWWForm();
        form.AddField("id_field", "7");
        UnityWebRequest webRequest = UnityWebRequest.Post(selectURL, form);
        yield return webRequest.SendWebRequest();

        // 메시지 출력
        if (webRequest.error != null)
            Debug.Log(webRequest.error);
        else {
            string dataText = webRequest.downloadHandler.text;
            Debug.Log(dataText);
        }
    }
}