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
    [SerializeField] public int Power;
    [SerializeField] public PassiveAbility PassiveAbilities; // Поле для пассивного умения
}