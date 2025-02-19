using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Health, Power;
    public PassiveAbilitiesCard PassiveAbilities;
    public string PassiveAbilitiesText;

    public Card(string name, Sprite logo, int attack, int health, int power, PassiveAbilitiesCard passiveAbilities, string passiveAbilitiesText)
    {
        Name = name;
        Logo = logo;
        Attack = attack;
        Health = health;
        Power = power;
        PassiveAbilities = passiveAbilities;
        PassiveAbilitiesText = passiveAbilitiesText;
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
            CardManagerStatic.AllCards.Add(new Card(card.name, card.Logo, card.Attack, card.Health, card.Power, card.PassiveAbilities, card.PassiveAbilities.GetPassiveAbilityText()));
        }
    }
}
