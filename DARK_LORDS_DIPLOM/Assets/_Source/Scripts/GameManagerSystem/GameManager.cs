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

    /*
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyHand = new List<Card>();
        PlayerDeck = new List<Card>();

        EnemyFields = new List<Card>();
        PlayerFields = new List<Card>();
    }

    List<Card> GiveDeckCard() 
    {

    }
    */
}
public class GameManager : MonoBehaviour
{
    public Game CurrentGame;

    void Start()
    {
        
    }

}
