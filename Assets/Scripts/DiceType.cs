using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceType : MonoBehaviour {
    public int id;
    public Color32 color;

    public DiceType(int id, Color32 color) {
        this.id = id;
        this.color = color;
    }
}