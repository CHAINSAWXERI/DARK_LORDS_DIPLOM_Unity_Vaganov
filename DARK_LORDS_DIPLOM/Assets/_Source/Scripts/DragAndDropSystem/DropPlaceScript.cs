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
public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    [SerializeField] public DropPlaceScript FieldOpposite;
    [SerializeField] private CardMoveScript currentCard;
    [SerializeField] public FieldType fieldType;

    public void OnDrop(PointerEventData eventData)
    {
        CardMoveScript card = eventData.pointerDrag.GetComponent<CardMoveScript>();

        if (card)
        {
            if (fieldType == FieldType.ENEMY_FIELD)
            {
                return;
            }

            card.DeafoultParent = transform;
            currentCard = card;
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
    }
}
