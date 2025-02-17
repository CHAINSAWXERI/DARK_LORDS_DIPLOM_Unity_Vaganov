using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
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
    [SerializeField] public CardMoveScript currentCard;
    [SerializeField] public FieldType fieldType;
    [SerializeField] public FieldNum fieldNum;

    public void OnDrop(PointerEventData eventData)
    {
        if (fieldType != FieldType.SELF_FIELD)
        {
            return;
        }

        CardMoveScript card = eventData.pointerDrag.gameObject.GetComponent<CardMoveScript>();
        CardInfoScript cardInfo = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();

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
                            GameManager.CardPlayerField1.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num2:
                        GameManager.CardPlayerField2 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardPlayerField2.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardPlayerField2.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num3:
                        GameManager.CardPlayerField3 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardPlayerField3.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardPlayerField3.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num4:
                        GameManager.CardPlayerField4 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardPlayerField4.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardPlayerField4.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
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
                            GameManager.CardEnemyField1.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num2:
                        GameManager.CardEnemyField2 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardEnemyField2.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardEnemyField2.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num3:
                        GameManager.CardEnemyField3 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardEnemyField3.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardEnemyField3.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
                        }
                        break;
                    case FieldNum.Num4:
                        GameManager.CardEnemyField4 = eventData.pointerDrag.gameObject.GetComponent<CardInfoScript>();
                        if (GameManager.CardEnemyField4.SelfCard.PassiveAbilities != null)
                        {
                            GameManager.CardEnemyField4.SelfCard.PassiveAbilities.ActivatePassiveAbilities();
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
