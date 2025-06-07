using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
//using static System.Net.Mime.MediaTypeNames;

public class CardInfoScript : NetworkBehaviour  // MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI PassiveAbilitiesText;
    public int CoreID;
    public int ID;
    public WhoseCard WhoseCard;
    public GameManager GameManager;

    public void ShowCardInfo(Card card, int id, GameManager gameManager, WhoseCard whoseCard)
    {
        Debug.Log("ShowCardInfo");
        SelfCard = card;
        GameManager = gameManager;

        if (card.Attack < 0)
        {
            card.Attack = 0;
        }
        if (card.Health <= 0)
        {
            card.Health = 0;
            ClearDeadCards(GameManager);
        }
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        if (SelfCard.CardType != CardType.Spell)
        {
            Attack.text = card.Attack.ToString();
            Health.text = card.Health.ToString();
        }
        else
        {
            Attack.text = "";
            Health.text = "";
        }
        PassiveAbilitiesText.text = card.PassiveAbilities.GetAbilityText();
        ID = id;
        WhoseCard = whoseCard;
        CoreID = card.CoreID;

        if (GameManager == null)
        {
            Debug.Log("GameManager is Null");
        }
    }

    private void Start()
    {
        //ShowCardInfo(CardManagerStatic.AllCards[transform.GetSiblingIndex()]);
    }

    private void ClearDeadCards(GameManager gameManager)
    {
        //Debug.Log("ПОДЧИСТКА");
        if (this == gameManager.CardPlayerField1)
        {
            Debug.Log("ПОДЧИСТКА ИГРОК ТРИГЕР 11111111111111111111111111111");
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField1.SelfCard);
            Destroy(gameManager.PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject); /////
            gameManager.PlayerField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField1 = null;
        }
        if (this == gameManager.CardPlayerField2)
        {
            Debug.Log("ПОДЧИСТКА ИГРОК ТРИГЕР 222222222222222222222222222222");
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField2.SelfCard);
            Destroy(gameManager.PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField2 = null;
        }
        if (this == gameManager.CardPlayerField3)
        {
            Debug.Log("ПОДЧИСТКА ИГРОК ТРИГЕР 33333333333333333333333333333333");
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField3.SelfCard);
            Destroy(gameManager.PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField3 = null;
        }
        if (this == gameManager.CardPlayerField4)
        {
            Debug.Log("ПОДЧИСТКА ИГРОК ТРИГЕР 44444444444444444444444444444444");
            gameManager.PlayerDiscardedDeck.Add(gameManager.CardPlayerField4.SelfCard);
            Destroy(gameManager.PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.PlayerField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardPlayerField4 = null;
        }
        ///////////////////
        if (this == gameManager.CardEnemyField1)
        {
            Debug.Log("ПОДЧИСТКА ВРАГ ТРИГЕР 11111111111111111111111111111");
            gameManager.EnemyDiscardedDeck.Add(gameManager.CardEnemyField1.SelfCard);
            Destroy(gameManager.EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField1 = null;
        }
        if (this == gameManager.CardEnemyField2)
        {
            Debug.Log("ПОДЧИСТКА ВРАГ ТРИГЕР 222222222222222222222222222222");
            gameManager.EnemyDiscardedDeck.Add(gameManager.CardEnemyField2.SelfCard);
            Destroy(gameManager.EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField2 = null;
        }
        if (this == gameManager.CardEnemyField3)
        {
            Debug.Log("ПОДЧИСТКА ВРАГ ТРИГЕР 33333333333333333333333333333333");
            gameManager.EnemyDiscardedDeck.Add(gameManager.CardEnemyField3.SelfCard);
            Destroy(gameManager.EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField3 = null;
        }
        if (this == gameManager.CardEnemyField4)
        {
            Debug.Log("ПОДЧИСТКА ВРАГ ТРИГЕР 44444444444444444444444444444444");
            gameManager.EnemyDiscardedDeck.Add(gameManager.CardEnemyField4.SelfCard);
            Destroy(gameManager.EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
            gameManager.EnemyField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
            gameManager.CardEnemyField4 = null;
        }
    }
}
