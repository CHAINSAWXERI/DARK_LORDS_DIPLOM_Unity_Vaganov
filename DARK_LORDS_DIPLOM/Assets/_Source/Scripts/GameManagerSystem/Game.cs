using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;
    public DeckCharacter EnemyCharacter, PlayerCharacter;

    public Game(List<CardScriptable> enemyDeck, List<CardScriptable> playerDeck, List<int> enemyDeckId, List<int> playerDeckId, WhoseCard whoseCardEnemy, WhoseCard whoseCardPlayer, DeckCharacter enemyCharacter, DeckCharacter playerCharacter) // (DeckObj playerDeck, DeckObj enemyDeck)
    {
        EnemyDeck = GiveDeckCard(enemyDeck, enemyDeckId, whoseCardEnemy); // (enemyDeck.ListDeck)
        PlayerDeck = GiveDeckCard(playerDeck, playerDeckId, whoseCardPlayer); // (playerDeck.ListDeck)
        EnemyCharacter = enemyCharacter;
        PlayerCharacter = playerCharacter;
        //Shuffle(EnemyDeck);
        //Shuffle(PlayerDeck);
        //Shuffle(enemyDeckId);
        //Shuffle(playerDeckId);
    }

    List<Card> GiveDeckCard(List<CardScriptable> Deck, List<int> DeckId, WhoseCard whoseCard) 
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < Deck.Count; i++)
        {
            Card card = new Card(Deck[i].Name, Deck[i].Logo, Deck[i].Attack, Deck[i].Health, Deck[i].Power, Deck[i].PassiveAbilities, Deck[i].CardType, whoseCard, Deck[i].CoreId);
            list.Add(card);
            DeckId.Add(Deck[i].CoreId);
        }

        return list;
    }

    // Метод для перемешивания списка карт
    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int j = Random.Range(i, n); 
                                        
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    /*
    */
}
