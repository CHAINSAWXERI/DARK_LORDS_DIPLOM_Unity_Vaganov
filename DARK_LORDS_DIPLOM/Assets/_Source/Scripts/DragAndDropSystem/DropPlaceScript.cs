using System.Collections;
using System.Collections.Generic;
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

public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    [SerializeField] public GameManager GameManager;
    [SerializeField] public DropPlaceScript FieldOpposite;
    [SerializeField] public DropPlaceScript FieldRight;
    [SerializeField] public DropPlaceScript FieldLeft;
    [SerializeField] public CardMoveScript currentCard;
    [SerializeField] public FieldType fieldType;
    [SerializeField] public FieldNum fieldNum;

    public void OnDrop(PointerEventData eventData)
    {
        CardMoveScript card = eventData.pointerDrag.gameObject.GetComponent<CardMoveScript>();
        CardInfoScript cardInfo = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();

        if ((fieldType == FieldType.SELF_SPELL_FIELD) || (fieldType == FieldType.SELF_FIELD) || (fieldType == FieldType.ENEMY_SPELL_FIELD) || (fieldType == FieldType.ENEMY_FIELD))
        {
            // Проверяем, является ли карта BluePlayer и поле SELF_FIELD
            if (cardInfo.WhoseCard == WhoseCard.BluePlayer && fieldType == FieldType.ENEMY_FIELD)
            {
                Debug.Log("ОШИБКА ИГРОКА: Нельзя ставить карту игрока на поле врага.");
                return;
            }

            // Проверяем, является ли карта RedPlayer и поле ENEMY_FIELD
            if (cardInfo.WhoseCard == WhoseCard.RedPlayer && fieldType == FieldType.SELF_FIELD)
            {
                Debug.Log("ОШИБКА ВРАГА: Нельзя ставить карту врага на своё поле.");
                return;
            }

            if (currentCard != null)
            {
                eventData.pointerDrag.gameObject.transform.SetParent(eventData.pointerDrag.gameObject.GetComponent<CardMoveScript>().DeafoultParent);
                return;
            }

            GameManager.PlayerHandCards.RemoveAll(c => c.ID == cardInfo.ID);

            if (card)
            {

                if (fieldType == FieldType.SELF_FIELD)
                {
                    switch (fieldNum)
                    {
                        case FieldNum.Num1:
                            GameManager.CardPlayerField1 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardPlayerField1.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.CardPlayerField1.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField1, GameManager.CardEnemyField1, GameManager.CardPlayerField2, null, GameManager);
                            }
                            if (GameManager.CardPlayerField1.SelfCard.CardType == CardType.Spell)
                            {

                            }
                            break;
                        case FieldNum.Num2:
                            GameManager.CardPlayerField2 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardPlayerField2.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.CardPlayerField2.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField2, GameManager.CardEnemyField2, GameManager.CardPlayerField3, GameManager.CardPlayerField1, GameManager);
                            }
                            break;
                        case FieldNum.Num3:
                            GameManager.CardPlayerField3 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardPlayerField3.SelfCard.PassiveAbilities != null)
                            {
                                GameManager.CardPlayerField3.SelfCard.PassiveAbilities.Activate(this, GameManager.CardPlayerField3, GameManager.CardEnemyField3, GameManager.CardPlayerField4, GameManager.CardPlayerField2, GameManager);
                            }
                            break;
                        case FieldNum.Num4:
                            GameManager.CardPlayerField4 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardPlayerField4.SelfCard.PassiveAbilities != null)
                            {
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
                            GameManager.CardEnemyField1 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardEnemyField1.SelfCard.PassiveAbilities != null)
                            {
                                Debug.Log("11111111111");
                                GameManager.CardEnemyField1.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField1, GameManager.CardPlayerField1, GameManager.CardEnemyField2, null, GameManager);
                            }
                            break;
                        case FieldNum.Num2:
                            GameManager.CardEnemyField2 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardEnemyField2.SelfCard.PassiveAbilities != null)
                            {
                                Debug.Log("222222222222");
                                GameManager.CardEnemyField2.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField2, GameManager.CardPlayerField2, GameManager.CardEnemyField3, GameManager.CardEnemyField1, GameManager);
                            }
                            break;
                        case FieldNum.Num3:
                            GameManager.CardEnemyField3 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardEnemyField3.SelfCard.PassiveAbilities != null)
                            {
                                Debug.Log("33333333333");
                                GameManager.CardEnemyField3.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField3, GameManager.CardPlayerField3, GameManager.CardEnemyField4, GameManager.CardEnemyField2, GameManager);
                            }
                            break;
                        case FieldNum.Num4:
                            GameManager.CardEnemyField4 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                            if (GameManager.CardEnemyField4.SelfCard.PassiveAbilities != null)
                            {
                                Debug.Log("44444444444");
                                GameManager.CardEnemyField4.SelfCard.PassiveAbilities.Activate(this, GameManager.CardEnemyField4, GameManager.CardPlayerField4, null, GameManager.CardEnemyField3, GameManager);
                            }
                            break;
                    }
                }

                card.DeafoultParent = transform;
                currentCard = card;

                if (GameManager.firstCard == false)
                {
                    GameManager.firstCard = true;
                }
                else
                {
                    GameManager.secondCard = true;
                    GameManager.BlockPhone.SetActive(true);
                }
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

            GameManager.PlayerHandCards.RemoveAll(c => c.ID == cardInfo.ID);

            if (card)
            {

                card.DeafoultParent = transform;
                currentCard = card;
                if (fieldType == FieldType.SELF_SPELL_FIELD)
                {
                    GameManager.PlayerDiscardedDeck.Add(currentCard.GetComponent<CardInfoScript>().SelfCard);
                    Destroy(currentCard.gameObject);
                    currentCard = null;
                }
                if (fieldType == FieldType.ENEMY_SPELL_FIELD)
                {
                    GameManager.PlayerDiscardedDeck.Add(currentCard.GetComponent<CardInfoScript>().SelfCard);
                    Destroy(currentCard.gameObject);
                    currentCard = null;
                }

                gameObject.SetActive(false);


                if (GameManager.firstCard == false)
                {
                    GameManager.firstCard = true;
                }
                else
                {
                    GameManager.secondCard = true;
                    GameManager.BlockPhone.SetActive(true);
                }
            }
        }
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
}
