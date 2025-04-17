using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObjects", menuName = "ScriptableObjects/DeckScriptable")]
public class DeckScriptable : ScriptableObject
{
    [SerializeField] public List<CardScriptable> Deck;
}
