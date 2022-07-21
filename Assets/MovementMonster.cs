using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMonster : MonoBehaviour {
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] float moveSpeed; 

    void Start() {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update() {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void setDirection(Vector3 v) {
        direction = v;
    }
}