using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;
//using static System.Net.Mime.MediaTypeNames;

public class CardInfoScript : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI PassiveAbilitiesText;
    public int ID;


    public void ShowCardInfo(Card card, int id, GameManager gameManager)
    {
        SelfCard = card;

        if (card.Attack < 0)
        {
            card.Attack = 0;
        }
        if (card.Health <= 0)
        {
            card.Health = 0;
            ClearDeadCards(gameManager);
        }
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        Attack.text = card.Attack.ToString();
        Health.text = card.Health.ToString();
        PassiveAbilitiesText.text = card.PassiveAbilities.GetAbilityText();
        ID = id;
    }

    private void Start()
    {
        //ShowCardInfo(CardManagerStatic.AllCards[transform.GetSiblingIndex()]);
    }

    private void ClearDeadCards(GameManager gameManager)
    {
        Debug.Log("ПОДЧИСТКА");
        if (this == gameManager.CardPlayerField1)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField1.SelfCard);
            Destroy(gameManager.PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField1 = null;
        }
        if (this == gameManager.CardPlayerField2)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField2.SelfCard);
            Destroy(gameManager.PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField2 = null;
        }
        if (this == gameManager.CardPlayerField3)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField3.SelfCard);
            Destroy(gameManager.PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField3 = null;
        }
        if (this == gameManager.CardPlayerField4)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField4.SelfCard);
            Destroy(gameManager.PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField4 = null;
        }
        ///////////////////
        if (this == gameManager.CardEnemyField1)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardEnemyField1.SelfCard);
            Destroy(gameManager.EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField1 = null;
        }
        if (this == gameManager.CardEnemyField2)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardEnemyField2.SelfCard);
            Destroy(gameManager.EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField2 = null;
        }
        if (this == gameManager.CardEnemyField3)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardEnemyField3.SelfCard);
            Destroy(gameManager.EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField3 = null;
        }
        if (this == gameManager.CardEnemyField4)
        {
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardEnemyField4.SelfCard);
            Destroy(gameManager.EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField4 = null;
        }
    }
}
