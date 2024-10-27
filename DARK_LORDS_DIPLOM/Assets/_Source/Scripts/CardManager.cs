using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Health;

    public Card(string name, Sprite logo, int attack, int health)
    {
        Name = name;
        Logo = logo;
        Attack = attack;
        Health = health;
    }
}

public static class CardManagerStatic
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManager : MonoBehaviour
{
    [SerializeField] public List<CardScriptable> CardsAll;

    public void Awake()
    {
        if (CardManagerStatic.AllCards.Count > 0)
        {
            CardManagerStatic.AllCards.Clear();
        }

        foreach (var card in CardsAll)
        {
            CardManagerStatic.AllCards.Add(new Card(card.name, card.Logo, card.Attack, card.Health));
        }
    }
}
