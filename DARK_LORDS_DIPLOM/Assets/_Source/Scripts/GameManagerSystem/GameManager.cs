using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck,
                      EnemyHand, PlayerHand,
                      EnemyFields, PlayerFields;
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyHand = new List<Card>();
        PlayerHand = new List<Card>();

        EnemyFields = new List<Card>();
        PlayerFields = new List<Card>();
    }

    List<Card> GiveDeckCard() 
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(CardManagerStatic.AllCards[Random.Range(0, CardManagerStatic.AllCards.Count)]);
        }
        return list;
    }
}
public class GameManager : MonoBehaviour
{
    public Game CurrentGame;
    [SerializeField] public Transform EnemyHand;
    [SerializeField] public Transform PlayerHand;
    [SerializeField] public GameObject CardPref;

    void Start()
    {
        CurrentGame = new Game();
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
    }

    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
        {
            GiveCardsToHand(deck, hand);
        }
    }

    void GiveCardsToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
        {
            return;
        }

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);
        cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card);

        deck.RemoveAt(0);
    }
}
