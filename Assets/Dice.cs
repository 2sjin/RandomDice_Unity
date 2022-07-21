using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour {
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject bulletPrefab;
    private float attackTime = 0.0f;


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
}