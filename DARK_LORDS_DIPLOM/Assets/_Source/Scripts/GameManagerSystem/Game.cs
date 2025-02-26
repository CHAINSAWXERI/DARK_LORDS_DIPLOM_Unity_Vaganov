using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;
    public Game(List<CardScriptable> enemyDeck, List<CardScriptable> playerDeck) // (DeckObj playerDeck, DeckObj enemyDeck)
    {
        EnemyDeck = GiveDeckCard(enemyDeck); // (enemyDeck.ListDeck)
        PlayerDeck = GiveDeckCard(playerDeck); // (playerDeck.ListDeck)
    }

    List<Card> GiveDeckCard(List<CardScriptable> Deck) 
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < Deck.Count; i++)
        {
            Card card = new Card(Deck[i].Name, Deck[i].Logo, Deck[i].Attack, Deck[i].Health, Deck[i].Power, Deck[i].PassiveAbilities);
            list.Add(card);
        }

        Shuffle(list);
        return list;
    }

    // Метод для перемешивания списка карт
    void Shuffle<T>(List<T> list)
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
}
