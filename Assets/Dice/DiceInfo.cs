using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInfo : MonoBehaviour {
    public struct DiceStruct {
        public int id;  // 주사위 ID
        public float attackDamage;  // 기본 공격력
        public float attackSpeed;   // 공격 속도(초)
        public string target;       // 타겟

        public float s0;
        public float s1;
        public float s2;

        public Color32 color;   // 색상
        
        public int level;

        public DiceStruct(int id, float damage, float speed, string target, float s0, float s1, float s2, Color32 color) {
            this.id = id;
            this.attackDamage = damage;
            this.attackSpeed = speed;
            this.target = target;
            this.s0 = s0;
            this.s1 = s1;
            this.s2 = s2;
            this.color = color;
            this.level = 1;
        }
    }
}