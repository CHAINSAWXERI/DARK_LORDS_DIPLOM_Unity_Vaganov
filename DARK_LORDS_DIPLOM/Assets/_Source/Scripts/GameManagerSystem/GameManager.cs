using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Dynamic;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;
using static System.Net.Mime.MediaTypeNames;
using Mirror;
using UnityEngine.PlayerLoop;
using System.Threading;
using System.Linq;

public enum GameType
{
    PVE,
    PVP
}


public class GameManager : NetworkBehaviour  //MonoBehaviour
{
    public Game CurrentGame;

    [SerializeField] public GameType GameType;

    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamer;

//    [SyncVar]
    [SerializeField] public PlayerInfo Enemy;
//    [SyncVar]
    [SerializeField] public PlayerInfo Player;

    [SerializeField] public Slider EnemyHPSlider;
    [SerializeField] public Slider PlayerHPSlider;
//    [SyncVar]
    [HideInInspector] public int EnemyHP;
//    [SyncVar]
    [HideInInspector] public int PlayerHP;


    [SerializeField] public Transform EnemyHand;
    [SerializeField] public Transform PlayerHand;
    [SerializeField] public GameObject CardPref;

    [SyncVar]
    int Turn;
    [SyncVar]
    int TurnTime = 30;


    [SerializeField] public TextMeshProUGUI TurnTimeTxtPlayer;
    [SerializeField] public TextMeshProUGUI TurnTimeTxtEnemy;

//    [SyncVar]
    [SerializeField] public GameObject EndTurnBtnPlayer;
//    [SyncVar]
    [SerializeField] public GameObject EndTurnBtnEnemy;

    [SyncVar]
    public List<int> PlayerDeckId = new List<int>();
    [SyncVar]
    public List<int> EnemyDeckId = new List<int>();

//    [SyncVar]
    public List<CardInfoScript> PlayerHandCards = new List<CardInfoScript>(),
                                EnemyHandCards = new List<CardInfoScript>();

//    [SyncVar]
    public List<Card> PlayerDiscardedDeck = new List<Card>(),
                      EnemyDiscardedDeck = new List<Card>();

    [SerializeField] public int maxCardsInHand;

//    [SyncVar]
    public CardInfoScript CardEnemyField1;
//    [SyncVar]
    public CardInfoScript CardEnemyField2;
//    [SyncVar]
    public CardInfoScript CardEnemyField3;
//    [SyncVar]
    public CardInfoScript CardEnemyField4;

//    [SyncVar]
    public CardInfoScript CardPlayerField1; ///////
//    [SyncVar]
    public CardInfoScript CardPlayerField2;
//    [SyncVar]
    public CardInfoScript CardPlayerField3;
//    [SyncVar]
    public CardInfoScript CardPlayerField4;

    [SerializeField] public Transform EnemyField1;
    [SerializeField] public Transform EnemyField2;
    [SerializeField] public Transform EnemyField3;
    [SerializeField] public Transform EnemyField4;

    [SerializeField] public Transform PlayerField1;
    [SerializeField] public Transform PlayerField2;
    [SerializeField] public Transform PlayerField3;
    [SerializeField] public Transform PlayerField4;

//    [SyncVar]
    [SerializeField] public GameObject BlockPhone;
//    [SyncVar]
    [SerializeField] public GameObject BlockPhoneEnemy;

//    [SyncVar]
    [SerializeField] public GameObject LoseScreenPlayer;
//    [SyncVar]
    [SerializeField] public GameObject WinScreenPlayer;
//    [SyncVar]
    [SerializeField] public GameObject LoseScreenEnemy;
//    [SyncVar]
    [SerializeField] public GameObject WinScreenEnemy;

    [SyncVar]
    [SerializeField] public GameObject BlueSpellScreen;
    [SyncVar]
    [SerializeField] public GameObject RedSpellScreen;

    private int IdPlayerCardCount = 0;
    private int IdEnemyCardCount = 0;

//    [SyncVar]
    private bool gameContinues = true;

//    [SyncVar]
    [HideInInspector] public bool firstCardPlayer = false;
//    [SyncVar]
    [HideInInspector] public bool secondCardPlayer = false;
//    [SyncVar]
    [HideInInspector] public bool firstCardEnemy = false;
//    [SyncVar]
    [HideInInspector] public bool secondCardEnemy = false;

    [HideInInspector] public PlayerCommands playerCommands;

    [SyncVar]
    public bool IsPlayerTurn;

    [SyncVar]
    public int CoreIdCardToTake = 0;

    [SyncVar]
    private bool isRpcInitDone = false;
    [SyncVar]
    private bool isInitilizationDone = false;
    [SyncVar]
    private bool isRandomTurnDone = false;


    [Server]
    public void StartGame()
    {
        Debug.Log("!!! START GAME !!!");
        Turn = 0;

        // Сбрасываем флаги
        isRpcInitDone = false;
        isInitilizationDone = false;
        isRandomTurnDone = false;

        // Запускаем последовательное выполнение
        StartCoroutine(StartGameSequence());
    }

    [Server]
    private IEnumerator StartGameSequence()
    {
        // Запускаем все необходимые операции
        RpcInitGame();
        Initilization();
        RandomTurn();

        // Ожидаем завершения всех операций
        yield return new WaitUntil(() => isRpcInitDone && isInitilizationDone && isRandomTurnDone);

        // Теперь безопасно вызываем
        if (CurrentGame != null)
        {
            GiveFiveCardsToHand(WhoseCard.BluePlayer, CurrentGame.PlayerCharacter);
            GiveFiveCardsToHand(WhoseCard.RedPlayer, CurrentGame.EnemyCharacter);
        }
        else
        {
            Debug.LogError("CurrentGame не инициализирован при вызове GiveHandCards");
        }

        // Начинаем игровой цикл
        //StartCoroutine(TurnFunc());
    }

    [ClientRpc]
    public void RandomTurn()
    {
        IsPlayerTurn = Random.value > 0.5f;
        Debug.Log("IsPlayerTurn = " + IsPlayerTurn);
        isRandomTurnDone = true; // Устанавливаем флаг завершения
        Debug.Log("RandomTurn completed");
    }


    [ClientRpc] //Ошибки возникают здесь!!!!!!!!!!!!!!
    public void RpcInitGame()
    {
        Debug.Log("RpcInitGame");
        // Создаем CurrentGame на клиентах
        CurrentGame = new Game(
            Enemy.DeckObj.Deck,
            Player.DeckObj.Deck,
            EnemyDeckId,
            PlayerDeckId,
            WhoseCard.RedPlayer,
            WhoseCard.BluePlayer,
            Enemy.DeckObj.deckCharacter,
            Player.DeckObj.deckCharacter
        );

        isRpcInitDone = true; // Устанавливаем флаг завершения
        Debug.Log("RpcInitGame completed");
    }

    [ClientRpc]
    public void Initilization()
    {
        Debug.Log("Initilization");

        playerCommands.DisambledObjServer(BlueSpellScreen, false);
        playerCommands.DisambledObjServer(RedSpellScreen, false);

        if (!EnemyHand.gameObject.activeInHierarchy)
        {
            TurnTimeTxtEnemy.gameObject.SetActive(true);
            EnemyHand.gameObject.SetActive(true);
        }
        if (!PlayerHand.gameObject.activeInHierarchy)
        {
            TurnTimeTxtPlayer.gameObject.SetActive(true);
            PlayerHand.gameObject.SetActive(true);
        }
        
        if ((IsPlayerTurn) && (EnemyCamera.gameObject.activeInHierarchy)) //Враг \ Ход Игрока
        {
            BlockPhoneEnemy.SetActive(true);
            EndTurnBtnEnemy.SetActive(false);
        }
        if ((!IsPlayerTurn) && (EnemyCamera.gameObject.activeInHierarchy)) //Враг \ Ход Врага 
        {
            BlockPhoneEnemy.SetActive(false);
            EndTurnBtnEnemy.SetActive(true);
        }

        if ((IsPlayerTurn) && (PlayerCamer.gameObject.activeInHierarchy)) //Игрок \ Ход Игрока
        {
            BlockPhone.SetActive(false);
            EndTurnBtnPlayer.SetActive(true);
        }
        if ((!IsPlayerTurn) && (PlayerCamer.gameObject.activeInHierarchy)) //Игрок \ Ход Врага 
        {
            BlockPhone.SetActive(true);
            EndTurnBtnPlayer.SetActive(false);
        }

        /*
        if (BlockPhoneEnemy.gameObject.activeInHierarchy)
        {
            Debug.Log("BlockPhoneEnemy Is Active");
        }
        else
        {
            Debug.Log("BlockPhoneEnemy Is Disactive");
        }

        
        LoseScreenEnemy
        LoseScreenPlayer
        WinScreenEnemy
        WinScreenPlayer       
        */

        isInitilizationDone = true; // Устанавливаем флаг завершения
        Debug.Log("Initilization completed");
    }

    /// //////////////////////////
    
    [Server]
    public void GenerateAndDistributeCoreIdCard(int minInclusive, int maxExclusive)
    {
        Debug.Log("++++++++++++++++GenerateAndDistributeCoreIdCard");
        int rnd = Random.Range(minInclusive, maxExclusive);
        //Debug.Log($"[Server] Generated CoreIdCardToTake: {CoreIdCardToTake} on client??????? {NetworkClient.connection.identity.netId}");
        RpcUpdateCoreIdCard(rnd);
    }

    [ClientRpc]
    void RpcUpdateCoreIdCard(int coreId)
    {
        CoreIdCardToTake = coreId;
        Debug.Log($"[ClientRpc] CoreIdCardToTake updated to {CoreIdCardToTake} on client {NetworkClient.connection.identity.netId}");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //(CurrentGame.PlayerDeck, PlayerHandCards, PlayerHand, WhoseCard.BluePlayer, CurrentGame.PlayerCharacter);
    //(CurrentGame.EnemyDeck, EnemyHandCards, EnemyHand, WhoseCard.RedPlayer, CurrentGame.EnemyCharacter);

    [Server]
    public void GiveFiveCardsToHand(WhoseCard whoseCard, DeckCharacter deckCharacter) //
    {
        //Debug.Log("11111");
        int i = 0;
        while (i++ < 4)
        {
            if (deckCharacter == DeckCharacter.Knight)
            {
                GenerateAndDistributeCoreIdCard(1, 11);
            }
            if (deckCharacter == DeckCharacter.Necromancer)
            {
                GenerateAndDistributeCoreIdCard(11, 22);
            }
            GiveCardToHand(whoseCard, deckCharacter);
        }
    }

    [ClientRpc]
    public void GiveCardToHand(WhoseCard whoseCard, DeckCharacter deckCharacter)
    {
        Debug.Log("------------------GiveCardToHand--------------------");

        //CoreIdCardToTake
        if (deckCharacter == DeckCharacter.Knight)
        {
            Debug.Log("Cards to Knight");
            if (CurrentGame.PlayerDeck.Count == 0 || PlayerHandCards.Count == maxCardsInHand)
            {
                return;
            }

            int r = 999;

            Card card = CurrentGame.PlayerDeck[0]; // Заглушка

            Debug.Log("Knight while");
            for (int j = 0; j < CurrentGame.PlayerDeck.Count; j++)
            {
                Debug.Log("Knight for");
                if (CurrentGame.PlayerDeck[j].CoreID == CoreIdCardToTake)
                {
                    Debug.Log("Knight if");
                    card = CurrentGame.PlayerDeck[j];
                    r = j;
                    Debug.Log($"Карта с CoreID {CoreIdCardToTake} найдена. Это карта {CurrentGame.PlayerDeck[r].CoreID} с именем {CurrentGame.PlayerDeck[r].Name}.");
                    break;
                }
            }
            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, PlayerHand, false);

            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);
            IdPlayerCardCount++;
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScript>());

            int indexToRemoveDeckId = PlayerDeckId.IndexOf(CoreIdCardToTake);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeck = " + r);
            Debug.Log("indexToRemoveDeckId = " + indexToRemoveDeckId);

            PlayerDeckId.RemoveAt(indexToRemoveDeckId);
            CurrentGame.PlayerDeck.RemoveAt(r);
        }
        if (deckCharacter == DeckCharacter.Necromancer)
        {
            Debug.Log("Cards to Necromancer");
            if (CurrentGame.EnemyDeck.Count == 0 || EnemyHandCards.Count == maxCardsInHand)
            {
                return;
            }

            int r = 999;

            Card card = CurrentGame.PlayerDeck[0]; // Заглушка


            Debug.Log("Necromancer while");
            for (int j = 0; j < CurrentGame.EnemyDeck.Count; j++)
            {
                Debug.Log("Necromancer for");
                if (CurrentGame.EnemyDeck[j].CoreID == CoreIdCardToTake)
                {
                    Debug.Log("Necromancer if");
                    card = CurrentGame.EnemyDeck[j];
                    r = j;
                    Debug.Log($"Карта с CoreID {CoreIdCardToTake} найдена. Это карта {CurrentGame.EnemyDeck[r].CoreID} с именем {CurrentGame.EnemyDeck[r].Name}.");
                    break;
                }
            }

            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, EnemyHand, false);

            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);
            IdPlayerCardCount++;
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());

            int indexToRemoveDeckId = EnemyDeckId.IndexOf(CoreIdCardToTake);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeck = " + r);
            Debug.Log("indexToRemoveDeckId = " + indexToRemoveDeckId);

            EnemyDeckId.RemoveAt(indexToRemoveDeckId);
            CurrentGame.EnemyDeck.RemoveAt(r);
        }

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*
    [ClientRpc]
    public void GiveHandCardsEnemy()
    {
        //Debug.Log("11111");
        int i = 0;
        while (i++ < 4)
        {
            GiveCardsToHandEnemy();
        }
    }

    ////(CurrentGame.EnemyDeck, EnemyHandCards, EnemyHand, WhoseCard.RedPlayer, CurrentGame.EnemyCharacter)
    public void GiveCardsToHandEnemy()
    {

        //CurrentGame.Shuffle(CurrentGame.EnemyDeck);

        if (CurrentGame.EnemyDeck.Count == 0)
        {
            return;
        }

        if (EnemyHandCards.Count == maxCardsInHand)
        {
            return;
        }

        int r = 0;
        bool i = false;
        Card card = CurrentGame.EnemyDeck[CurrentGame.EnemyDeck.Count - 1]; // Заглушка

        int attempts = 0;
        while (!i && attempts < 100)
        {
            for (int j = 0; j < CurrentGame.EnemyDeck.Count; j++)
            {
                if (CurrentGame.EnemyDeck[j].CoreID == CoreIdCardToTake)
                {
                    card = CurrentGame.EnemyDeck[j];
                    r = j;
                    i = true;
                    Debug.Log($"Карта с CoreID {CoreIdCardToTake} найдена. Это карта {CurrentGame.EnemyDeck[j].CoreID} с именем {CurrentGame.EnemyDeck[j].Name}.");
                    break;
                }
            }
            attempts++;
        }

        card.Health = card.MaxHealth;
        card.Attack = card.MaxAttack;

        GameObject cardGO = Instantiate(CardPref, EnemyHand, false);

        cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this, WhoseCard.RedPlayer);
        IdPlayerCardCount++;
        EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());
        CurrentGame.EnemyDeck.RemoveAt(r);

        Debug.Log($"Это карта с именем {CurrentGame.EnemyDeck[r].Name} и индексом {CurrentGame.EnemyDeck[r].CoreID}. Была Удалена из стопки");
    }
    */
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //[ClientRpc]
    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxtPlayer.text = TurnTime.ToString();
        TurnTimeTxtEnemy.text = TurnTime.ToString();

        if (IsPlayerTurn)
        {
            BlockPhoneEnemy.SetActive(true);
            firstCardPlayer = false;
            secondCardPlayer = false;
            while (TurnTime-- > 0)
            {
                TurnTimeTxtPlayer.text = TurnTime.ToString();
                TurnTimeTxtEnemy.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            BlockPhone.SetActive(true);
            if (GameType == GameType.PVE)
            {
                while (TurnTime-- > 27)
                {
                    TurnTimeTxtPlayer.text = TurnTime.ToString();
                    TurnTimeTxtEnemy.text = TurnTime.ToString();
                    yield return new WaitForSeconds(1);
                }

                if (EnemyHandCards.Count > 0)
                {
                    EnemyTurn(EnemyHandCards, EnemyField1, EnemyField2, EnemyField3, EnemyField4);
                }
            }
            if (GameType == GameType.PVP)
            {
                firstCardEnemy = false;
                secondCardEnemy = false;
                while (TurnTime-- > 0)
                {
                    TurnTimeTxtPlayer.text = TurnTime.ToString();
                    TurnTimeTxtEnemy.text = TurnTime.ToString();
                    yield return new WaitForSeconds(1);
                }
            }
        }
        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScript> handCards, Transform field1, Transform field2, Transform field3, Transform field4)
    {
        bool firstCard = true;
        for (int i = 0; i < 2; i++)
        {
            if (handCards.Count == 0) // Проверяем, есть ли карты
            {
                break; // Выходим из цикла, если карт больше нет
            }
            int maxpower = -1;
            int maxPowerIndex = -1;

            for (int o = 0; o < handCards.Count; o++)
            {
                if (handCards[o].SelfCard.Power > maxpower)
                {
                    maxpower = handCards[o].SelfCard.Power;
                    maxPowerIndex = o;
                }
            }

            handCards[maxPowerIndex].ShowCardInfo(handCards[maxPowerIndex].SelfCard, IdEnemyCardCount, this, handCards[maxPowerIndex].WhoseCard);
            IdEnemyCardCount++;

            bool cardIsPlace = false;
            if (firstCard == true)
            {
                if (PlayerField1.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField1);
                    CardEnemyField1 = handCards[maxPowerIndex];
                    //CardEnemyField1.ShowCardInfo(CardEnemyField1.SelfCard, CardEnemyField1.ID, this);
                    EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField1.SelfCard.PassiveAbilities.Activate(EnemyField1.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField1, CardPlayerField1, CardEnemyField2, null, this);
                }
                if (PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField2);
                    CardEnemyField2 = handCards[maxPowerIndex];
                    //CardEnemyField2.ShowCardInfo(CardEnemyField2.SelfCard, CardEnemyField2.ID, this);
                    EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAll(c => c.ID == handCards[maxPowerIndex].ID);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField2.SelfCard.PassiveAbilities.Activate(EnemyField2.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField2, CardPlayerField2, CardEnemyField3, CardEnemyField1, this);
                }
                if (PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField3);
                    CardEnemyField3 = handCards[maxPowerIndex];
                    //CardEnemyField3.ShowCardInfo(CardEnemyField3.SelfCard, CardEnemyField3.ID, this);
                    EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField3.SelfCard.PassiveAbilities.Activate(EnemyField3.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField3, CardPlayerField3, CardEnemyField4, CardEnemyField2, this);
                }
                if (PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField4);
                    CardEnemyField4 = handCards[maxPowerIndex];
                    //CardEnemyField4.ShowCardInfo(CardEnemyField4.SelfCard, CardEnemyField4.ID, this);
                    EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
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
                    handCards[maxPowerIndex].transform.SetParent(EnemyField1);
                    CardEnemyField1 = handCards[maxPowerIndex];
                    //CardEnemyField1.ShowCardInfo(CardEnemyField1.SelfCard, CardEnemyField1.ID, this);
                    EnemyField1.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField1.SelfCard.PassiveAbilities.Activate(EnemyField1.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField1, CardPlayerField1, CardEnemyField2, null, this);
                }
                if (PlayerField2.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField2);
                    CardEnemyField2 = handCards[maxPowerIndex];
                    //CardEnemyField2.ShowCardInfo(CardEnemyField2.SelfCard, CardEnemyField2.ID, this);
                    EnemyField2.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField2.SelfCard.PassiveAbilities.Activate(EnemyField2.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField2, CardPlayerField2, CardEnemyField3, CardEnemyField1, this);
                }
                if (PlayerField3.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField3);
                    CardEnemyField3 = handCards[maxPowerIndex];
                    //CardEnemyField3.ShowCardInfo(CardEnemyField3.SelfCard, CardEnemyField3.ID, this);
                    EnemyField3.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
                    cardIsPlace = true;
                    firstCard = false;

                    CardEnemyField3.SelfCard.PassiveAbilities.Activate(EnemyField3.gameObject.GetComponent<DropPlaceScript>(), CardEnemyField3, CardPlayerField3, CardEnemyField4, CardEnemyField2, this);
                }
                if (PlayerField4.gameObject.GetComponent<DropPlaceScript>().currentCard != null && EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard == null && cardIsPlace == false)
                {
                    handCards[maxPowerIndex].transform.SetParent(EnemyField4);
                    CardEnemyField4 = handCards[maxPowerIndex];
                    //CardEnemyField4.ShowCardInfo(CardEnemyField4.SelfCard, CardEnemyField4.ID, this);
                    EnemyField4.gameObject.GetComponent<DropPlaceScript>().currentCard = handCards[maxPowerIndex].gameObject.GetComponent<CardMoveScript>();

                    handCards.RemoveAt(maxPowerIndex);
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

        //EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveFiveCardsToHand(WhoseCard.BluePlayer, CurrentGame.PlayerCharacter); // clientrpcCardsToHand(); // PreGiveCardsToHandPlayer();
            BlockPhone.SetActive(false);
            BlockPhoneEnemy.SetActive(true);
            EndTurnBtnPlayer.SetActive(true);
            EndTurnBtnEnemy.SetActive(false);
            //GamePlaceCanvas.worldCamera =
        }
        else
        {
            GiveFiveCardsToHand(WhoseCard.RedPlayer, CurrentGame.EnemyCharacter); // PreGiveCardsToHandEnemy();
            BlockPhone.SetActive(true);
            BlockPhoneEnemy.SetActive(false);
            EndTurnBtnPlayer.SetActive(false);
            EndTurnBtnEnemy.SetActive(true);
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
                    PlayerHP = (int)(PlayerHPSlider.value) - CardEnemyField1.SelfCard.Attack;
                    PlayerHPSlider.value = PlayerHP;

                    if (PlayerHPSlider.value <= 0)
                    {
                        WinScreenEnemy.SetActive(true);
                        LoseScreenPlayer.SetActive(true);
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
                    PlayerHP = (int)(PlayerHPSlider.value) - CardEnemyField2.SelfCard.Attack;
                    PlayerHPSlider.value = PlayerHP;

                    if (PlayerHPSlider.value <= 0)
                    {
                        WinScreenEnemy.SetActive(true);
                        LoseScreenPlayer.SetActive(true);
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
                    PlayerHP = (int)(PlayerHPSlider.value) - CardEnemyField3.SelfCard.Attack;
                    PlayerHPSlider.value = PlayerHP;

                    if (PlayerHPSlider.value <= 0)
                    {
                        WinScreenEnemy.SetActive(true);
                        LoseScreenPlayer.SetActive(true);
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
                    PlayerHP = (int)(PlayerHPSlider.value) - CardEnemyField4.SelfCard.Attack;
                    PlayerHPSlider.value = PlayerHP;

                    if (PlayerHPSlider.value <= 0)
                    {
                        WinScreenEnemy.SetActive(true);
                        LoseScreenPlayer.SetActive(true);
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
                    EnemyHP = (int)(EnemyHPSlider.value) - CardPlayerField1.SelfCard.Attack;
                    EnemyHPSlider.value = EnemyHP;

                    if (EnemyHPSlider.value <= 0)
                    {
                        WinScreenPlayer.SetActive(true);
                        LoseScreenEnemy.SetActive(true);
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
                    EnemyHP = (int)(EnemyHPSlider.value) - CardPlayerField2.SelfCard.Attack;
                    EnemyHPSlider.value = EnemyHP;

                    if (EnemyHPSlider.value <= 0)
                    {
                        WinScreenPlayer.SetActive(true);
                        LoseScreenEnemy.SetActive(true);
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
                    EnemyHP = (int)(EnemyHPSlider.value) - CardPlayerField3.SelfCard.Attack;
                    EnemyHPSlider.value = EnemyHP;

                    if (EnemyHPSlider.value <= 0)
                    {
                        WinScreenPlayer.SetActive(true);
                        LoseScreenEnemy.SetActive(true);
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
                    EnemyHP = (int)(EnemyHPSlider.value) - CardPlayerField4.SelfCard.Attack;
                    EnemyHPSlider.value = EnemyHP;

                    if (EnemyHPSlider.value <= 0)
                    {
                        WinScreenPlayer.SetActive(true);
                        LoseScreenEnemy.SetActive(true);
                        gameContinues = false;
                        return;
                    }
                }
            }
        }
    }
}
