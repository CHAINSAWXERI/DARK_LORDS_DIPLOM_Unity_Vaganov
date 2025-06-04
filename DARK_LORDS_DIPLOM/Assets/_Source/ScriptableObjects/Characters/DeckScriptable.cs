using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObjects", menuName = "ScriptableObjects/DeckScriptable")]
public class DeckScriptable : ScriptableObject
{
    [SerializeField] public DeckCharacter deckCharacter;
    [SerializeField] public List<CardScriptable> Deck;
}

public enum DeckCharacter
{
    Knight,
    Necromancer,
    Theif,
    Warlock
}
