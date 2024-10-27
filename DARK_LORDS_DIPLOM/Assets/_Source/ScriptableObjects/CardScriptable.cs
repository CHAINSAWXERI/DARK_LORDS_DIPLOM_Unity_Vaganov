using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObjects", menuName = "ScriptableObjects/CardScriptable")]
public class CardScriptable : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public Sprite Logo;
    [SerializeField] public int Attack;
    [SerializeField] public int Health;
}
