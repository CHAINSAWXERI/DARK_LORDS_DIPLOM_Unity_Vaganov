using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Dynamic;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;

public class GameManager : MonoBehaviour
{
    public Game CurrentGame;

    [SerializeField] public PlayerInfo Enemy;
    [SerializeField] public PlayerInfo Player;

    [SerializeField] public Slider EnemyHPSlider;
    [SerializeField] public Slider PlayerHPSlider;

    [SerializeField] public Transform EnemyHand;
    [SerializeField] public Transform PlayerHand;
    [SerializeField] public GameObject CardPref;
    int Turn, TurnTime = 30;
    [SerializeField] public TextMeshProUGUI TurnTimeTxt;
    [SerializeField] public Button EndTurnBtn;

    public List<CardInfoScript> PlayerHandCards = new List<CardInfoScript>(),
                                EnemyHandCards = new List<CardInfoScript>();

    public List<Card> PlayerDiscardedDeck = new List<Card>(),
                      EnemyDiscardedDeck = new List<Card>();

    [SerializeField] public int maxCardsInHand;

    public CardInfoScript CardEnemyField1;
    public CardInfoScript CardEnemyField2;
    public CardInfoScript CardEnemyField3;
    public CardInfoScript CardEnemyField4;

    public CardInfoScript CardPlayerField1; ////////////////////////////////////////////////////////////////////////////////////////////////
    public CardInfoScript CardPlayerField2;
    public CardInfoScript CardPlayerField3;
    public CardInfoScript CardPlayerField4;

    [SerializeField] public Transform EnemyField1;
    [SerializeField] public Transform EnemyField2;
    [SerializeField] public Transform EnemyField3;
    [SerializeField] public Transform EnemyField4;

    [SerializeField] public Transform PlayerField1;
    [SerializeField] public Transform PlayerField2;
    [SerializeField] public Transform PlayerField3;
    [SerializeField] public Transform PlayerField4;

    [SerializeField] public GameObject BlockPhone;
    [SerializeField] public GameObject LoseScreen;
    [SerializeField] public GameObject WinScreen;

    private int IdPlayerCardCount = 0;
    private int IdEnemyCardCount = 0;

    private bool gameContinues = true;

    [HideInInspector] public bool firstCard = false;
    [HideInInspector] public bool secondCard = false;

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    void Start()
    {
        LoseScreen.SetActive(false);
        LoseScreen.SetActive(false);
        BlockPhone.SetActive(false);

        EnemyHPSlider.maxValue = Enemy.PlayerHP;
        PlayerHPSlider.maxValue = Player.PlayerHP;
        EnemyHPSlider.value = Enemy.PlayerHP;
        PlayerHPSlider.value = Player.PlayerHP;

        Turn = 0;
        CurrentGame = new Game(Enemy.DeckObj.Deck, Player.DeckObj.Deck);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHandCards, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHandCards, PlayerHand);
        StartCoroutine(TurnFunc());
    }

    public void GiveHandCards(List<Card> deck, List<CardInfoScript> hand, Transform handTransform)
    {
        int i = 0;
        while (i++ < 4)
        {
            GiveCardsToHand(deck, hand, handTransform); 
        }
    }

    public void GiveCardsToHand(List<Card> deck, List<CardInfoScript> hand, Transform handTransform)
    {
        if (deck.Count == 0)
        {
            return;
        }

        if (hand.Count == maxCardsInHand)
        {
            return;
        }

        Card card = deck[0];
        GameObject cardGO = Instantiate(CardPref, handTransform, false);

        cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this);
        IdPlayerCardCount++;
        hand.Add(cardGO.GetComponent<CardInfoScript>());

        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        if (IsPlayerTurn)
        {
            firstCard = false;
            secondCard = false;
            while (TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
            {
                EnemyTurn(EnemyHandCards, EnemyField1, EnemyField2, EnemyField3, EnemyField4);
            }
        }
        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScript> cards, Transform field1, Transform field2, Transform field3, Transform field4)
    {
        bool firstCard = true;
        for (int i = 0; i < 2; i++)
        {
            if (cards.Count == 0) // Проверяем, есть ли карты
            {
                break; // Выходим из цикла, если карт больше нет
            }
            int maxpower = -1;
            int maxPowerIndex = -1;

            for (int o = 0; o < cards.Count; o++)
            {
                if (cards[o].SelfCard.Power > maxpower)
                {
                    maxpower = cards[o].SelfCard.Power;
                    maxPowerIndex = o;
                }
            }

            cards[maxPowerIndex].ShowCardInfo(cards[maxPowerIndex].SelfCard, IdEnemyCardCount, this);
            IdEnemyCardCount++;

            bool cardIsPlace = false;
            if (firstCard == true)
            {
                if (PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField1);
                    CardEnemyField1 = cards[maxPowerIndex];
                    //CardEnemyField1.ShowCardInfo(CardEnemyField1.SelfCard, CardEnemyField1.ID, this);
                    EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField1.SelfCard.PassiveAbilities.Activate(EnemyField1.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField1, CardPlayerField1, CardEnemyField2, null, this);
                }
                if (PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField2);
                    CardEnemyField2 = cards[maxPowerIndex];
                    //CardEnemyField2.ShowCardInfo(CardEnemyField2.SelfCard, CardEnemyField2.ID, this);
                    EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField2.SelfCard.PassiveAbilities.Activate(EnemyField2.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField2, CardPlayerField2, CardEnemyField3, CardEnemyField1, this);
                }
                if (PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField3);
                    CardEnemyField3 = cards[maxPowerIndex];
                    //CardEnemyField3.ShowCardInfo(CardEnemyField3.SelfCard, CardEnemyField3.ID, this);
                    EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField3.SelfCard.PassiveAbilities.Activate(EnemyField3.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField3, CardPlayerField3, CardEnemyField4, CardEnemyField2, this);
                }
                if (PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField4);
                    CardEnemyField4 = cards[maxPowerIndex];
                    //CardEnemyField4.ShowCardInfo(CardEnemyField4.SelfCard, CardEnemyField4.ID, this);
                    EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField4.SelfCard.PassiveAbilities.Activate(EnemyField4.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField4, CardPlayerField4, null, CardEnemyField3, this);
                }

                if (cardIsPlace == false)
                {
                    return;
                }
            }
            else
            {
                if (PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField1);
                    CardEnemyField1 = cards[maxPowerIndex];
                    //CardEnemyField1.ShowCardInfo(CardEnemyField1.SelfCard, CardEnemyField1.ID, this);
                    EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField1.SelfCard.PassiveAbilities.Activate(EnemyField1.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField1, CardPlayerField1, CardEnemyField2, null, this);
                }
                if (PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField2);
                    CardEnemyField2 = cards[maxPowerIndex];
                    //CardEnemyField2.ShowCardInfo(CardEnemyField2.SelfCard, CardEnemyField2.ID, this);
                    EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField2.SelfCard.PassiveAbilities.Activate(EnemyField2.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField2, CardPlayerField2, CardEnemyField3, CardEnemyField1, this);
                }
                if (PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField3);
                    CardEnemyField3 = cards[maxPowerIndex];
                    //CardEnemyField3.ShowCardInfo(CardEnemyField3.SelfCard, CardEnemyField3.ID, this);
                    EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField3.SelfCard.PassiveAbilities.Activate(EnemyField3.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField3, CardPlayerField3, CardEnemyField4, CardEnemyField2, this);
                }
                if (PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    cards[maxPowerIndex].transform.SetParent(EnemyField4);
                    CardEnemyField4 = cards[maxPowerIndex];
                    //CardEnemyField4.ShowCardInfo(CardEnemyField4.SelfCard, CardEnemyField4.ID, this);
                    EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard = cards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    cards.RemoveAll(c => c.ID == cards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField4.SelfCard.PassiveAbilities.Activate(EnemyField4.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField4, CardPlayerField4, null, CardEnemyField3, this);
                }
            }
            cardIsPlace = false;
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        if (Turn > 2)
        {
            Attack();
        }

        EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveCardsToHand(CurrentGame.PlayerDeck, PlayerHandCards, PlayerHand);
            BlockPhone.SetActive(false);
        }
        else
        {
            GiveCardsToHand(CurrentGame.EnemyDeck, EnemyHandCards, EnemyHand);
            BlockPhone.SetActive(true);
        }

        if (gameContinues)
        {
            StartCoroutine(TurnFunc());
        }
        
    }

    public void Attack()
    {
        if (IsPlayerTurn)
        {
            Debug.Log("АТАКА ВРАГА!!!");
            if (CardEnemyField1 != null && CardEnemyField1.SelfCard.Attack > 0)
            {
                if (CardPlayerField1 != null)
                {
                    CardPlayerField1.SelfCard.Health = CardPlayerField1.SelfCard.Health - CardEnemyField1.SelfCard.Attack;
                    CardPlayerField1.Health.text = CardPlayerField1.SelfCard.Health.ToString();

                    if (CardPlayerField1.SelfCard.Health <= 0)
                    {
                        PlayerDiscardedDeck.Add(CardPlayerField1.SelfCard);
                        Destroy(PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        PlayerField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardPlayerField1 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 1 игрока");
                    PlayerHPSlider.value = PlayerHPSlider.value - CardEnemyField1.SelfCard.Attack;

                    if (PlayerHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Проиграна");
                        LoseScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardEnemyField2 != null && CardEnemyField2.SelfCard.Attack > 0)
            {
                if (CardPlayerField2 != null)
                {
                    CardPlayerField2.SelfCard.Health = CardPlayerField2.SelfCard.Health - CardEnemyField2.SelfCard.Attack;
                    CardPlayerField2.Health.text = CardPlayerField2.SelfCard.Health.ToString();

                    if (CardPlayerField2.SelfCard.Health <= 0)
                    {
                        PlayerDiscardedDeck.Add(CardPlayerField2.SelfCard);
                        Destroy(PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        PlayerField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardPlayerField2 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 1 игрока");
                    PlayerHPSlider.value = PlayerHPSlider.value - CardEnemyField2.SelfCard.Attack;

                    if (PlayerHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Проиграна");
                        LoseScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardEnemyField3 != null && CardEnemyField3.SelfCard.Attack > 0)
            {
                if (CardPlayerField3 != null)
                {
                    CardPlayerField3.SelfCard.Health = CardPlayerField3.SelfCard.Health - CardEnemyField3.SelfCard.Attack;
                    CardPlayerField3.Health.text = CardPlayerField3.SelfCard.Health.ToString();

                    if (CardPlayerField3.SelfCard.Health <= 0)
                    {
                        PlayerDiscardedDeck.Add(CardPlayerField3.SelfCard);
                        Destroy(PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        PlayerField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardPlayerField3 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 1 игрока");
                    PlayerHPSlider.value = PlayerHPSlider.value - CardEnemyField3.SelfCard.Attack;

                    if (PlayerHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Проиграна");
                        LoseScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardEnemyField4 != null && CardEnemyField4.SelfCard.Attack > 0)
            {
                if (CardPlayerField4 != null)
                {
                    CardPlayerField4.SelfCard.Health = CardPlayerField4.SelfCard.Health - CardEnemyField4.SelfCard.Attack;
                    CardPlayerField4.Health.text = CardPlayerField4.SelfCard.Health.ToString();

                    if (CardPlayerField4.SelfCard.Health <= 0)
                    {
                        PlayerDiscardedDeck.Add(CardPlayerField4.SelfCard);
                        Destroy(PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        PlayerField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardPlayerField4 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 1 игрока");
                    PlayerHPSlider.value = PlayerHPSlider.value - CardEnemyField4.SelfCard.Attack;

                    if (PlayerHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Проиграна");
                        LoseScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }
        }
        else
        {
            Debug.Log("АТАКА ИГРОКА!!!");

            if (CardPlayerField1 != null && CardPlayerField1.SelfCard.Attack > 0)
            {
                if (CardEnemyField1 != null)
                {
                    CardEnemyField1.SelfCard.Health = CardEnemyField1.SelfCard.Health - CardPlayerField1.SelfCard.Attack;
                    CardEnemyField1.Health.text = CardEnemyField1.SelfCard.Health.ToString();

                    if (CardEnemyField1.SelfCard.Health <= 0)
                    {
                        EnemyDiscardedDeck.Add(CardEnemyField1.SelfCard);
                        Destroy(EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        EnemyField1.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardEnemyField1 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 1 Врага");
                    EnemyHPSlider.value = EnemyHPSlider.value - CardPlayerField1.SelfCard.Attack;

                    if (EnemyHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Выйграна");
                        WinScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardPlayerField2 != null && CardPlayerField2.SelfCard.Attack > 0)
            {
                if (CardEnemyField2 != null)
                {
                    CardEnemyField2.SelfCard.Health = CardEnemyField2.SelfCard.Health - CardPlayerField2.SelfCard.Attack;
                    CardEnemyField2.Health.text = CardEnemyField2.SelfCard.Health.ToString();

                    if (CardEnemyField2.SelfCard.Health <= 0)
                    {
                        EnemyDiscardedDeck.Add(CardEnemyField2.SelfCard);
                        Destroy(EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        EnemyField2.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardEnemyField2 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 2 врага");
                    EnemyHPSlider.value = EnemyHPSlider.value - CardPlayerField2.SelfCard.Attack;

                    if (EnemyHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Выйграна");
                        WinScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardPlayerField3 != null && CardPlayerField3.SelfCard.Attack > 0)
            {
                if (CardEnemyField3 != null)
                {
                    CardEnemyField3.SelfCard.Health = CardEnemyField3.SelfCard.Health - CardPlayerField3.SelfCard.Attack;
                    CardEnemyField3.Health.text = CardEnemyField3.SelfCard.Health.ToString();

                    if (CardEnemyField3.SelfCard.Health <= 0)
                    {
                        EnemyDiscardedDeck.Add(CardEnemyField3.SelfCard);
                        Destroy(EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        EnemyField3.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardEnemyField3 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 3 врага");
                    EnemyHPSlider.value = EnemyHPSlider.value - CardPlayerField3.SelfCard.Attack;

                    if (EnemyHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Выйграна");
                        WinScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }

            if (CardPlayerField4 != null && CardPlayerField4.SelfCard.Attack > 0)
            {
                if (CardEnemyField4 != null)
                {
                    CardEnemyField4.SelfCard.Health = CardEnemyField4.SelfCard.Health - CardPlayerField4.SelfCard.Attack;
                    CardEnemyField4.Health.text = CardEnemyField4.SelfCard.Health.ToString();

                    if (CardEnemyField4.SelfCard.Health <= 0)
                    {
                        EnemyDiscardedDeck.Add(CardEnemyField4.SelfCard);
                        Destroy(EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard.gameObject);
                        EnemyField4.gameObject.GetComponent<DropPlaceScript>().ClearCurrentCard();
                        CardEnemyField4 = null;
                    }
                }
                else
                {
                    Debug.Log("Удар по пустому полю 4 врага");
                    EnemyHPSlider.value = EnemyHPSlider.value - CardPlayerField4.SelfCard.Attack;

                    if (EnemyHPSlider.value <= 0)
                    {
                        Debug.Log("Игра Выйграна");
                        WinScreen.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }
        }
    }
}
