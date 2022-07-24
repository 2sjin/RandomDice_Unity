using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSprites : MonoBehaviour {
    [SerializeField] public List<Sprite> Common = new List<Sprite>();
    [SerializeField] public List<Sprite> Rare = new List<Sprite>();
    [SerializeField] public List<Sprite> Unique = new List<Sprite>();
    [SerializeField] public List<Sprite> Legendary = new List<Sprite>();
}