using Mirror;
using Mirror.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD,
    SELF_SPELL_FIELD,
    ENEMY_SPELL_FIELD
}

public enum FieldNum
{
    Num1,
    Num2,
    Num3,
    Num4
}

public class DropPlaceScript : NetworkBehaviour, IDropHandler //MonoBehaviour
{
    [SerializeField] public GameManager GameManager;
    [SerializeField] public DropPlaceScript FieldOpposite;
    [SerializeField] public DropPlaceScript FieldRight;
    [SerializeField] public DropPlaceScript FieldLeft;
    [SerializeField] public CardMoveScript currentCard;

    [SerializeField] public GameObject currentCardObj;
    [SerializeField] public FieldType fieldType;
    [SerializeField] public FieldNum fieldNum;
    [HideInInspector] public CardMoveScript card;
    [HideInInspector] public CardInfoScript cardInfo;
    [HideInInspector] public bool SetCardDone = false;

    public void OnDrop(PointerEventData eventData)
    {
        currentCardObj = eventData.pointerDrag.gameObject;
        //
        card = eventData.pointerDrag.gameObject.GetComponent<CardMoveScript>();
        cardInfo = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
        

        if ((fieldType == FieldType.SELF_FIELD) || (fieldType == FieldType.ENEMY_FIELD) || (fieldType == FieldType.SELF_SPELL_FIELD) || (fieldType == FieldType.ENEMY_SPELL_FIELD))
        {
            card.canDrag = false;
            // Проверяем, является ли карта BluePlayer и поле SELF_FIELD
            if (cardInfo.WhoseCard == WhoseCard.BluePlayer && fieldType == FieldType.ENEMY_FIELD)
            {
                Debug.Log(cardInfo.WhoseCard);
                Debug.Log(fieldType);
                Debug.Log("ОШИБКА ИГРОКА: Нельзя ставить карту игрока на поле врага.");
                return;
            }

            // Проверяем, является ли карта RedPlayer и поле ENEMY_FIELD
            if (cardInfo.WhoseCard == WhoseCard.RedPlayer && fieldType == FieldType.SELF_FIELD)
            {
                Debug.Log(cardInfo.WhoseCard);
                Debug.Log(fieldType);
                Debug.Log("ОШИБКА ВРАГА: Нельзя ставить карту врага на своё поле.");
                return;
            }

            if (currentCard != null)
            {
                eventData.pointerDrag.gameObject.transform.SetParent(eventData.pointerDrag.gameObject.GetComponent<CardMoveScript>().DeafoultParent);
                return;
            }

            if (card)
            {

                card.DeafoultParent = transform;
                currentCard = card;
                if (NetworkServer.active)
                {
                    Debug.Log("HOST DROP PLACE");
                    Debug.Log("cardInfo.CoreID" + cardInfo.CoreID);
                    Debug.Log("cardInfo.ID" + cardInfo.ID);
                    SetCardRpc(cardInfo.CoreID, cardInfo.ID, fieldType);
                }
                else if (NetworkClient.isConnected)
                {
                    Debug.Log("CLIENT DROP PLACE");
                    Debug.Log("cardInfo.CoreID" + cardInfo.CoreID);
                    Debug.Log("cardInfo.ID" + cardInfo.ID);
                    CmdSetCard(cardInfo.CoreID, cardInfo.ID, fieldType); //
                }

                ////////////!!!!!!!!!!!!!!
                //card.DeafoultParent = transform;
                //currentCard = card;
                ////////////!!!!!!!!!!!!!! Каким то образом перенести это на верх
            }
        }
        else
        {
            // Проверяем, является ли карта BluePlayer и поле SELF_FIELD
            if (cardInfo.WhoseCard == WhoseCard.BluePlayer && fieldType == FieldType.ENEMY_SPELL_FIELD)
            {
                Debug.Log("ОШИБКА ИГРОКА: Нельзя ставить карту игрока на поле врага.");
                return;
            }

            // Проверяем, является ли карта RedPlayer и поле ENEMY_FIELD
            if (cardInfo.WhoseCard == WhoseCard.RedPlayer && fieldType == FieldType.SELF_SPELL_FIELD)
            {
                Debug.Log("ОШИБКА ВРАГА: Нельзя ставить карту врага на своё поле.");
                return;
            }

            // Проверяем, является ли карта BluePlayer и поле SELF_FIELD
            if (cardInfo.WhoseCard == WhoseCard.BluePlayer && fieldType == FieldType.SELF_HAND)
            {
                Debug.Log("Карту Вернули В руку");
                if (fieldType == FieldType.SELF_SPELL_FIELD)
                {
                    gameObject.SetActive(false);
                }
                return;
            }

            // Проверяем, является ли карта RedPlayer и поле ENEMY_FIELD
            if (cardInfo.WhoseCard == WhoseCard.RedPlayer && fieldType == FieldType.ENEMY_HAND)
            {
                Debug.Log("Карту Вернули В руку");
                if (fieldType == FieldType.ENEMY_SPELL_FIELD)
                {
                    gameObject.SetActive(false);
                }
                return;
            }

            //GameManager.PlayerHandCards.RemoveAll(c => c.ID == cardInfo.ID);
        }

        //StartCoroutine(IEDiscardSpell());

        /*
        
        */

    }

    public CardMoveScript GetCurrentCard()
    {
        return currentCard;
    }

    // Метод для очистки текущей карты (если необходимо)
    public void ClearCurrentCard()
    {
        currentCard = null;
        if (fieldType == FieldType.SELF_FIELD)
        {
            switch (fieldNum)
            {
                case FieldNum.Num1:
                    GameManager.CardPlayerField1 = null;
                    break;
                case FieldNum.Num2:
                    GameManager.CardPlayerField2 = null;
                    break;
                case FieldNum.Num3:
                    GameManager.CardPlayerField3 = null;
                    break;
                case FieldNum.Num4:
                    GameManager.CardPlayerField4 = null;
                    break;
            }
        }
        else if (fieldType == FieldType.ENEMY_FIELD)
        {
            switch (fieldNum)
            {
                case FieldNum.Num1:
                    GameManager.CardEnemyField1 = null;
                    break;
                case FieldNum.Num2:
                    GameManager.CardEnemyField2 = null;
                    break;
                case FieldNum.Num3:
                    GameManager.CardEnemyField3 = null;
                    break;
                case FieldNum.Num4:
                    GameManager.CardEnemyField4 = null;
                    break;
            }
        }
    }

    [ClientRpc]
    public void SetCardRpc(int findCoreId, int findID, FieldType fieldType)
    {
        // FindObject with Core ID
        /*
        card.DeafoultParent = transform;
        currentCard = card;
        currentCard.UpdateDeafoultParent();
        */
        CardInfoScript[] cardInfoScripts = FindObjectsOfType<CardInfoScript>(true);
        GameObject card;
        CardInfoScript cardInf = null;
        // Перебираем все найденные объекты
        foreach (CardInfoScript cardInfo in cardInfoScripts)
        {
            // Проверяем, равен ли CoreID 4
            if (cardInfo.CoreID == findCoreId && cardInfo.ID == findID)
            {
                cardInf = cardInfo;
                Debug.Log(cardInfo.SelfCard.Name + " SelfCard Name");
                Debug.Log(cardInfo.Name.text + " Name");
                Debug.Log(cardInfo.CoreID + " CoreID");
                Debug.Log(cardInfo.ID + " ID");

                CardMoveScript crd = cardInfo.gameObject.GetComponent<CardMoveScript>();
                CardInfoScript crdInfo = cardInfo.gameObject.GetComponent<CardInfoScript>();

                crd.DeafoultParent = transform;
                currentCard = crd;
                crd.UpdateDeafoultParent();

                card = cardInfo.gameObject;

                Debug.Log("findCoreId " + findCoreId);
                Debug.Log("findID " + findID);

                /*
                */
                if (GameManager.EnemyCamera.gameObject.activeInHierarchy)
                {
                    if (fieldType == FieldType.SELF_FIELD)
                    {
                        Debug.Log("ПЕРЕВЕРНУЛИ КАРТУ ИГРОКА");
                        RectTransform rectTransform = crd.gameObject.GetComponent<RectTransform>();

                        rectTransform.localEulerAngles = new Vector3(rectTransform.localEulerAngles.x, rectTransform.localEulerAngles.y, 180);
                    }
                }

                if (GameManager.PlayerCamera.gameObject.activeInHierarchy)
                {
                    if (fieldType == FieldType.ENEMY_FIELD)
                    {
                        Debug.Log("ПЕРЕВЕРНУЛИ КАРТУ ВРАГА");
                        RectTransform rectTransform = crd.gameObject.GetComponent<RectTransform>();

                        rectTransform.localEulerAngles = new Vector3(rectTransform.localEulerAngles.x, rectTransform.localEulerAngles.y, 0);
                    }
                }

                if (fieldType == FieldType.SELF_FIELD || fieldType == FieldType.SELF_SPELL_FIELD)
                {
                    Debug.Log("PLAYER CARD");

                    // Удаляем карту из основного списка
                    GameManager.PlayerHandCards.RemoveAll(c => c.ID == findID);

                    // Удаляем по значению, а не по индексу
                    GameManager.PlayerHandCoreID.Remove(findCoreId);
                    GameManager.PlayerHandId.Remove(findID);
                }
                if (fieldType == FieldType.ENEMY_FIELD || fieldType == FieldType.ENEMY_SPELL_FIELD)
                {
                    Debug.Log("ENEMY CARD");

                    GameManager.EnemyHandCards.RemoveAll(c => c.ID == findID); //////////////////////////////////////////////////////////////////////////

                    GameManager.EnemyHandCoreID.Remove(findCoreId);
                    GameManager.EnemyHandId.Remove(findID);
                }


                //crdInfo.ShowCardInfo();
                if (fieldType == FieldType.SELF_FIELD)
                {
                    switch (fieldNum)
                    {
                        case FieldNum.Num1:
                            GameManager.CardPlayerField1 = crdInfo;
                            if (GameManager.CardPlayerField1.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Синее Поле 1 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Синее Поле 1 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardPlayerField1.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField1, GameManager.CardEnemyField1, GameManager.CardPlayerField2, null, GameManager);
                            }
                            break;
                        case FieldNum.Num2:
                            GameManager.CardPlayerField2 = crdInfo;
                            if (GameManager.CardPlayerField2.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Синее Поле 2 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Синее Поле 2 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardPlayerField2.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField2, GameManager.CardEnemyField2, GameManager.CardPlayerField3, GameManager.CardPlayerField1, GameManager);
                            }
                            break;
                        case FieldNum.Num3:
                            GameManager.CardPlayerField3 = crdInfo;
                            if (GameManager.CardPlayerField3.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Синее Поле 3 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Синее Поле 3 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardPlayerField3.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField3, GameManager.CardEnemyField3, GameManager.CardPlayerField4, GameManager.CardPlayerField2, GameManager);
                            }
                            break;
                        case FieldNum.Num4:
                            GameManager.CardPlayerField4 = crdInfo;
                            if (GameManager.CardPlayerField4.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Синее Поле 4 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Синее Поле 4 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardPlayerField4.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField4, GameManager.CardEnemyField4, null, GameManager.CardPlayerField3, GameManager);
                            }
                            break;
                    }
                }
                else if (fieldType == FieldType.ENEMY_FIELD)
                {
                    switch (fieldNum)
                    {
                        case FieldNum.Num1:
                            GameManager.CardEnemyField1 = crdInfo;
                            if (GameManager.CardEnemyField1.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Красное Поле 1 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Красное Поле 1 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardEnemyField1.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField1, GameManager.CardPlayerField1, GameManager.CardEnemyField2, null, GameManager);
                            }
                            break;
                        case FieldNum.Num2:
                            GameManager.CardEnemyField2 = crdInfo;
                            if (GameManager.CardEnemyField2.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Красное Поле 2 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Красное Поле 2 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardEnemyField2.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField2, GameManager.CardPlayerField2, GameManager.CardEnemyField3, GameManager.CardEnemyField1, GameManager);
                            }
                            break;
                        case FieldNum.Num3:
                            GameManager.CardEnemyField3 = crdInfo;
                            if (GameManager.CardEnemyField3.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Красное Поле 3 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Красное Поле 3 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardEnemyField3.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField3, GameManager.CardPlayerField3, GameManager.CardEnemyField4, GameManager.CardEnemyField2, GameManager);
                            }
                            break;
                        case FieldNum.Num4:
                            GameManager.CardEnemyField4 = crdInfo;
                            if (GameManager.CardEnemyField4.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.LogTxtPlayer.text = $"На Красное Поле 4 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.LogTxtEnemy.text = $"На Красное Поле 4 Положили Карту {crdInfo.SelfCard.Name}";
                                GameManager.CardEnemyField4.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField4, GameManager.CardPlayerField4, null, GameManager.CardEnemyField3, GameManager);
                            }
                            break;
                    }
                }

                break;

                // Здесь можно добавить дополнительные действия с найденным объектом
                // Например, вызвать метод ShowCardInfo или что-то еще
            }
        }

        if ((fieldType == FieldType.SELF_FIELD || fieldType == FieldType.SELF_SPELL_FIELD))
        {
            if (GameManager.firstCardPlayer == false)
            {
                Debug.Log("ПЕРВАЯ КАРТА ИГРОКА!");
                GameManager.firstCardPlayer = true;
            }
            else
            {
                Debug.Log("ВТОРАЯ КАРТА ИГРОКА!");
                GameManager.secondCardPlayer = true;
                GameManager.BlockPhone.SetActive(true);
            }
        }
        else
        {
            if (GameManager.firstCardEnemy == false)
            {
                Debug.Log("ПЕРВАЯ КАРТА ВРАГА!");
                GameManager.firstCardEnemy = true;
            }
            else
            {
                Debug.Log("ВТОРАЯ КАРТА ВРАГА!");
                GameManager.secondCardEnemy = true;
                GameManager.BlockPhoneEnemy.SetActive(true);
            }
        }


        SetCardDone = true;
        if (fieldType == FieldType.SELF_SPELL_FIELD)
        {
            GameManager.LogTxtPlayer.text = $"Синий Игрок Разыграл Заклинание {cardInf.SelfCard.Name} со способностью {cardInf.SelfCard.PassiveAbilitiesText}";
            GameManager.LogTxtEnemy.text = $"Синий Игрок Разыграл Заклинание {cardInf.SelfCard.Name} со способностью {cardInf.SelfCard.PassiveAbilitiesText}";
            cardInf.SelfCard.PassiveAbilities.Activate(this, null, null, null, null, GameManager);
            GameManager.PlayerDiscardedDeck.Add(cardInf.SelfCard);
            Destroy(currentCard.gameObject);
            currentCard = null;
            gameObject.SetActive(false);
        }
        else if (fieldType == FieldType.ENEMY_SPELL_FIELD)
        {
            GameManager.LogTxtPlayer.text = $"Красный Игрок Разыграл Заклинание {cardInf.SelfCard.Name} со способностью {cardInf.SelfCard.PassiveAbilitiesText}";
            GameManager.LogTxtEnemy.text = $"Красный Игрок Разыграл Заклинание {cardInf.SelfCard.Name} со способностью {cardInf.SelfCard.PassiveAbilitiesText}";
            cardInf.SelfCard.PassiveAbilities.Activate(this, null, null, null, null, GameManager);
            GameManager.EnemyDiscardedDeck.Add(cardInf.SelfCard);
            Destroy(currentCard.gameObject);
            currentCard = null;
            gameObject.SetActive(false);
        }

        Debug.Log("SetCardRpc");
    }

    [Command(requiresAuthority = false)]
    public void CmdSetCard(int findCoreId, int findID, FieldType fieldType) //
    {
        Debug.Log("CmdSetCard");
        CmdSetCardOnAllClients(findCoreId, findID, fieldType); //
    }

    [Server]
    public void CmdSetCardOnAllClients(int findCoreId, int findID, FieldType fieldType) //
    {
        Debug.Log("CmdSetCardOnAllClients");
        SetCardRpc(findCoreId, findID, fieldType);
    }
}
/*
[HideInInspector] public CardMoveScript card;
[SerializeField] public CardMoveScript currentCard;

[SerializeField] public GameObject currentCardObj;


card.DeafoultParent = transform;
                currentCard = card;
*/