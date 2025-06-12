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
using UnityEditor.UIElements;

public enum GameType
{
    PVE,
    PVP
}

public enum FromDeck
{
    PlayDeck,
    DiscaredDeck
}


public class GameManager : NetworkBehaviour  //MonoBehaviour
{
    public Game CurrentGame;

    [SerializeField] public GameType GameType;

    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;

    //    [SyncVar]
    [SerializeField] public PlayerInfo Enemy;
    //    [SyncVar]
    [SerializeField] public PlayerInfo Player;

    [SerializeField] public Slider EnemyHPSlider;
    [SerializeField] public Slider PlayerHPSlider;
    [SyncVar]
    [SerializeField] public int EnemyHP;
    [SyncVar]
    [SerializeField] public int PlayerHP;


    [SerializeField] public Transform EnemyHand;
    [SerializeField] public Transform PlayerHand;
    [SerializeField] public GameObject CardPref;

    [SerializeField] public int TurnTimeConst;

    [SyncVar]
    int Turn;
    [SyncVar]
    int TurnTime;


    [SerializeField] public TextMeshProUGUI TurnTimeTxtPlayer;
    [SerializeField] public TextMeshProUGUI TurnTimeTxtEnemy;

    //    [SyncVar]
    [SerializeField] public GameObject EndTurnBtnPlayer;
    //    [SyncVar]
    [SerializeField] public GameObject EndTurnBtnEnemy;

    //    [SyncVar]
    public List<int> PlayerDeckCoreID = new List<int>();
    //    [SyncVar]
    public List<int> EnemyDeckCoreID = new List<int>();

    //    [SyncVar]
    public List<int> PlayerHandCoreID = new List<int>();
    //    [SyncVar]
    public List<int> EnemyHandCoreID = new List<int>();

    //    [SyncVar]
    public List<int> PlayerHandId = new List<int>();
    //    [SyncVar]
    public List<int> EnemyHandId = new List<int>();

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

    [SerializeField] public DropPlaceScript BlueSpellScreen;
    [SerializeField] public DropPlaceScript RedSpellScreen;

    [HideInInspector] public int IdPlayerCardCount = 0;
    private int IdEnemyCardCount = 0;

    //    [SyncVar]
    private bool gameContinues = true;

    //    [SyncVar]
    public bool firstCardPlayer = false;
    //    [SyncVar]
    public bool secondCardPlayer = false;
    //    [SyncVar]
    public bool firstCardEnemy = false;
    //    [SyncVar]
    public bool secondCardEnemy = false;

    [SerializeField] public GameObject StartScreenPhone;

    [HideInInspector] public PlayerCommands playerCommands;

    [SerializeField] public GameObject NoteHostEnemy;
    [SerializeField] public GameObject NoteClientEnemy;

    [SerializeField] public GameObject NoteHostPlayer;
    [SerializeField] public GameObject NoteClientPlayer;

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

        IsPlayerTurn = Random.value > 0.5f;
        Debug.Log("IsPlayerTurn = " + IsPlayerTurn);
        isRandomTurnDone = true;

        // Запускаем последовательное выполнение
        StartCoroutine(StartGameSequence());
    }

    [Server]
    private IEnumerator StartGameSequence()
    {
        // Запускаем все необходимые операции

        RpcInitGame();
        Initilization(IsPlayerTurn);
        // Ожидаем завершения всех операций
        yield return new WaitUntil(() => isRpcInitDone && isInitilizationDone && isRandomTurnDone);

        // Теперь безопасно вызываем
        if (CurrentGame != null)
        {
            GiveCardsToHandServer(WhoseCard.BluePlayer, CurrentGame.PlayerCharacter, 4, FromDeck.PlayDeck);
            GiveCardsToHandServer(WhoseCard.RedPlayer, CurrentGame.EnemyCharacter, 4, FromDeck.PlayDeck);
        }
        else
        {
            Debug.LogError("CurrentGame не инициализирован при вызове GiveHandCards");
        }

        // Начинаем игровой цикл
        StartCoroutine(TurnFunc());
    }


    [ClientRpc]
    public void RpcInitGame()
    {
        Debug.Log("RpcInitGame");
        // Создаем CurrentGame на клиентах
        CurrentGame = new Game(
            Enemy.DeckObj.Deck,
            Player.DeckObj.Deck,
            EnemyDeckCoreID,
            PlayerDeckCoreID,
            WhoseCard.RedPlayer,
            WhoseCard.BluePlayer,
            Enemy.DeckObj.deckCharacter,
            Player.DeckObj.deckCharacter
        );

        isRpcInitDone = true; // Устанавливаем флаг завершения
        Debug.Log("RpcInitGame completed");
    }

    [ClientRpc]
    public void Initilization(bool isPlayerTurn)
    {
        Debug.Log("Initilization with isPlayerTurn: " + isPlayerTurn);

        // Устанавливаем флаг завершения

        PlayerHPSlider.maxValue = PlayerHP;
        EnemyHPSlider.maxValue = EnemyHP;

        PlayerHPSlider.value = PlayerHP;
        EnemyHPSlider.value = EnemyHP;

        BlueSpellScreen.gameObject.SetActive(false);
        RedSpellScreen.gameObject.SetActive(false);

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

        if ((isPlayerTurn)) // Ход Игрока
        {
            BlockPhone.SetActive(false);
            BlockPhoneEnemy.SetActive(true);

            EndTurnBtnPlayer.SetActive(true);
            EndTurnBtnEnemy.SetActive(false);
        }
        if ((!isPlayerTurn)) // Ход Врага 
        {
            BlockPhone.SetActive(true);
            BlockPhoneEnemy.SetActive(false);

            EndTurnBtnPlayer.SetActive(false);
            EndTurnBtnEnemy.SetActive(true);
        }

        if ((EnemyCamera.gameObject.activeInHierarchy))
        {
            TurnTimeTxtEnemy.gameObject.SetActive(true);
        }

        if ((PlayerCamera.gameObject.activeInHierarchy))
        {
            TurnTimeTxtPlayer.gameObject.SetActive(true);
        }

        StartScreenPhone.SetActive(false);

        NoteHostEnemy.SetActive(false);
        NoteClientEnemy.SetActive(false);
        NoteHostPlayer.SetActive(false);
        NoteClientPlayer.SetActive(false);

        


        if (NetworkServer.active)
        {

        }
        else if (NetworkClient.isConnected)
        {

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

    [Command(requiresAuthority = false)]
    public void GiveCardsToHandCommand(WhoseCard whoseCard, DeckCharacter deckCharacter, int howMuchCards, FromDeck fromDeck)
    {
        GiveCardsToHandServer(whoseCard, deckCharacter, howMuchCards, fromDeck);
    }

    [Server]
    public void GiveCardsToHandServer(WhoseCard whoseCard, DeckCharacter deckCharacter, int howMuchCards, FromDeck fromDeck) //
    {
        //Debug.Log("11111");
        int i = 0;
        while (i++ < howMuchCards)
        {
            if (fromDeck == FromDeck.PlayDeck)
            {
                if (deckCharacter == DeckCharacter.Knight)
                {
                    GenerateAndDistributeCoreIdCard(0, CurrentGame.PlayerDeck.Count - i);
                }
                if (deckCharacter == DeckCharacter.Necromancer)
                {
                    GenerateAndDistributeCoreIdCard(0, CurrentGame.EnemyDeck.Count - i);
                }

                GiveCardToHand(whoseCard, deckCharacter);
            }
            if (fromDeck == FromDeck.DiscaredDeck)
            {
                if (deckCharacter == DeckCharacter.Knight)
                {
                    GenerateAndDistributeCoreIdCard(0, PlayerDiscardedDeck.Count - i);
                }
                if (deckCharacter == DeckCharacter.Necromancer)
                {
                    GenerateAndDistributeCoreIdCard(0, EnemyDiscardedDeck.Count - i);
                }

                GiveCardToHandFromDiscared(whoseCard, deckCharacter);
            }
            
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

            Debug.Log($"Карта По Индекск {CoreIdCardToTake} найдена. Это карта с именем {CurrentGame.PlayerDeck[CoreIdCardToTake].Name}.");

            Card card = CurrentGame.PlayerDeck[CoreIdCardToTake];

            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, PlayerHand, false);

            CardInfoScript cardGOInfo = cardGO.GetComponent<CardInfoScript>();

            cardGOInfo.ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);
            PlayerHandCards.Add(cardGOInfo);
            PlayerHandId.Add(IdPlayerCardCount);
            PlayerHandCoreID.Add(card.CoreID);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeckId = " + PlayerDeckCoreID[CoreIdCardToTake]);

            PlayerDeckCoreID.RemoveAt(CoreIdCardToTake);
            CurrentGame.PlayerDeck.RemoveAt(CoreIdCardToTake);
            IdPlayerCardCount++;
        }
        if (deckCharacter == DeckCharacter.Necromancer)
        {
            Debug.Log("Cards to Necromancer");
            if (CurrentGame.EnemyDeck.Count == 0 || EnemyHandCards.Count == maxCardsInHand)
            {
                return;
            }

            Debug.Log($"Карта По Индекск {CoreIdCardToTake} найдена. Это карта с именем {CurrentGame.EnemyDeck[CoreIdCardToTake].Name}.");

            Card card = CurrentGame.EnemyDeck[CoreIdCardToTake];

            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, EnemyHand, false);

            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);
            
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());
            EnemyHandId.Add(IdPlayerCardCount);
            EnemyHandCoreID.Add(card.CoreID);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeckId = " + EnemyDeckCoreID[CoreIdCardToTake]);

            EnemyDeckCoreID.RemoveAt(CoreIdCardToTake);
            CurrentGame.EnemyDeck.RemoveAt(CoreIdCardToTake);
            IdPlayerCardCount++;
        }

    }

    [ClientRpc]
    public void GiveCardToHandFromDiscared(WhoseCard whoseCard, DeckCharacter deckCharacter)
    {
        Debug.Log("------------------GiveCardToHandFromDiscared--------------------");
        
        //CoreIdCardToTake
        if (deckCharacter == DeckCharacter.Knight)
        {
            Debug.Log("Cards to Knight");
            if (PlayerDiscardedDeck.Count == 0 || PlayerHandCards.Count == maxCardsInHand)
            {
                return;
            }

            Debug.Log($"Карта По Индекск {CoreIdCardToTake} найдена. Это карта с именем {PlayerDiscardedDeck[CoreIdCardToTake].Name}.");

            Card card = PlayerDiscardedDeck[CoreIdCardToTake];

            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, PlayerHand, false);

            CardInfoScript cardGOInfo = cardGO.GetComponent<CardInfoScript>();

            cardGOInfo.ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);
            PlayerHandCards.Add(cardGOInfo);
            PlayerHandId.Add(IdPlayerCardCount);
            PlayerHandCoreID.Add(card.CoreID);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeckId = " + PlayerDeckCoreID[CoreIdCardToTake]);

            PlayerDiscardedDeck.RemoveAt(CoreIdCardToTake);
        }
        if (deckCharacter == DeckCharacter.Necromancer)
        {
            Debug.Log("Cards to Necromancer");
            if (EnemyDiscardedDeck.Count == 0 || EnemyHandCards.Count == maxCardsInHand)
            {
                return;
            }

            Debug.Log($"Карта По Индекск {CoreIdCardToTake} найдена. Это карта с именем {EnemyDiscardedDeck[CoreIdCardToTake].Name}.");

            Card card = EnemyDiscardedDeck[CoreIdCardToTake];

            Debug.Log($"Это карта с именем {card.Name} и индексом {card.CoreID}. Была Удалена из стопки");

            card.Health = card.MaxHealth;
            card.Attack = card.MaxAttack;

            GameObject cardGO = Instantiate(CardPref, EnemyHand, false);

            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, IdPlayerCardCount, this, whoseCard);

            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());
            EnemyHandId.Add(IdPlayerCardCount);
            EnemyHandCoreID.Add(card.CoreID);

            Debug.Log("CoreIdCardToTake = " + CoreIdCardToTake);
            Debug.Log("indexToRemoveDeckId = " + EnemyDeckCoreID[CoreIdCardToTake]);

            CurrentGame.EnemyDeck.RemoveAt(CoreIdCardToTake);
        }
    }

    [Server]
    public IEnumerator TurnFunc()
    {
        TurnTime = TurnTimeConst;
        RpcUpdateTurnTimeText(TurnTime);

        if (IsPlayerTurn)
        {
            Debug.Log("CRDS firstCardPlayer " + firstCardPlayer);
            Debug.Log("CRDS secondCardPlayer " + secondCardPlayer);
            while (TurnTime > 0)
            {
                yield return new WaitForSeconds(1);
                TurnTime--;
                RpcUpdateTurnTimeText(TurnTime);
            }
        }
        else
        {
            if (GameType == GameType.PVP)
            {
                Debug.Log("CRDS firstCardEnemy" + firstCardEnemy);
                Debug.Log("CRDS secondCardEnemy" + secondCardEnemy);
                while (TurnTime > 0)
                {
                    yield return new WaitForSeconds(1);
                    TurnTime--;
                    RpcUpdateTurnTimeText(TurnTime);
                }
            }
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
        }
        ChangeTurn();
    }

    [ClientRpc]
    void RpcUpdateTurnTimeText(int time)
    {
        TurnTimeTxtPlayer.text = time.ToString();
        TurnTimeTxtEnemy.text = time.ToString();
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
        if (NetworkServer.active)
        {
            Debug.Log("HOST ChangeTurn");
            ChangeTurnServer();
        }
        else if (NetworkClient.isConnected)
        {
            Debug.Log("CLIENT ChangeTurn");
            ChangeTurnCommand();
        }
    }

    [Server]
    public void ChangeTurnServer()
    {
        StopAllCoroutines();
        Turn++;
        if (Turn > 2)
        {
            Attack(IsPlayerTurn, PlayerHP, EnemyHP);
        }

        //EndTurnBtn.interactable = IsPlayerTurn;
        IsPlayerTurn = !IsPlayerTurn;
        ChangeTurnRPC(IsPlayerTurn);

        if (Turn > 2)
        {
            if (IsPlayerTurn)
            {
                GiveCardsToHandServer(WhoseCard.BluePlayer, CurrentGame.PlayerCharacter, 1, FromDeck.PlayDeck);
            }
            else
            {
                GiveCardsToHandServer(WhoseCard.RedPlayer, CurrentGame.EnemyCharacter, 1, FromDeck.PlayDeck);
            }
        }

        if (gameContinues)
        {
            StartCoroutine(TurnFunc());
        }
    }

    [ClientRpc]
    public void ChangeTurnRPC(bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            Debug.Log("PlayerTurn");
            BlockPhone.SetActive(false);
            BlockPhoneEnemy.SetActive(true);
            EndTurnBtnPlayer.SetActive(true);
            EndTurnBtnEnemy.SetActive(false);
            
            firstCardPlayer = false;
            secondCardPlayer = false;

            Debug.Log("IsPlayerTurn " + isPlayerTurn);
        }
        else
        {
            Debug.Log("EnemyTurn");
            BlockPhone.SetActive(true);
            BlockPhoneEnemy.SetActive(false);
            EndTurnBtnPlayer.SetActive(false);
            EndTurnBtnEnemy.SetActive(true);

            firstCardEnemy = false;
            secondCardEnemy = false;

            Debug.Log("IsPlayerTurn " + isPlayerTurn);
        }
    }

    [Command(requiresAuthority = false)]
    public void ChangeTurnCommand()
    {
        ChangeTurnServer();
    }


    [ClientRpc]
    public void Attack(bool isPlayerTurn, int playerHP, int enemyHP)
    {
        if (isPlayerTurn == false)
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
                    playerHP = playerHP - CardEnemyField1.SelfCard.Attack;
                    PlayerHP = playerHP;
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
                    playerHP = playerHP - CardEnemyField2.SelfCard.Attack;
                    PlayerHP = playerHP;
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
                    playerHP = playerHP - CardEnemyField3.SelfCard.Attack;
                    PlayerHP = playerHP;
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
                    playerHP = playerHP - CardEnemyField4.SelfCard.Attack;
                    PlayerHP = playerHP;
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
                    enemyHP = enemyHP - CardPlayerField1.SelfCard.Attack;
                    EnemyHP = enemyHP;
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
                    enemyHP = enemyHP - CardPlayerField2.SelfCard.Attack;
                    EnemyHP = enemyHP;
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
                    enemyHP = enemyHP - CardPlayerField3.SelfCard.Attack;
                    EnemyHP = enemyHP;
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
                    enemyHP = enemyHP - CardPlayerField4.SelfCard.Attack;
                    EnemyHP = enemyHP;
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
