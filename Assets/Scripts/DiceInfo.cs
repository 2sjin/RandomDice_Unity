using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInfo : MonoBehaviour {
    public int id;
    public float attackDamage;
    public float attackSpeed;
    public string target;
    public float[] special = new float[3];
    public Color32 color;

    public DiceInfo(int id, float damage, float speed, string target, float s0, float s1, float s2, Color32 color) {
        this.id = id;
        this.attackDamage = damage;
        this.attackSpeed = speed;
        this.target = target;
        this.special[0] = s0;
        this.special[1] = s1;
        this.special[2] = s2;
        this.color = color;
    }
}