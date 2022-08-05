using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {
    // 구조체를 사용하지 않으면, 필드 내 같은 종류의 몬스터는 무조건 같은 정보가 적용됨
    public struct DiceStruct {
        public int id;          // 몬스터 ID
        public string name;     // 몬스터 이름
        public bool isBoss;     // 보스판정 여부
        public float moveSpeed;   // 이동 속도
        public int hp;          // 몬스터 HP

        public DiceStruct(string diceDataText) {
            // 구분자('/')를 기준으로 문자열을 토큰으로 분해하여 임시 리스트에 추가
            string[] tokens = diceDataText.Split('/');
            List<string> tempList = new List<string>();
            foreach(string token in tokens) {
                tempList.Add(token);
            }

            // 리스트에 저장된 각 토큰의 값을 순서대로 구조체 멤버 변수에 대입
            this.id = int.Parse(tempList[0]);
            this.name = tempList[1];
            this.isBoss = bool.Parse(tempList[2]);
            this.moveSpeed = float.Parse(tempList[3]);
            this.hp = 100;
        }
    }
}