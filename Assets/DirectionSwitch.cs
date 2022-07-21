using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSwitch : MonoBehaviour {
    [SerializeField] Vector3 direction;

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<MovementMonster>().setDirection(direction);
    }
}