using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealByYourselfAbility", menuName = "ScriptableObjects/PassiveAbilities/HealByYourselfAbility")]
public class HealByYourselfAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            if (gameManager.CardPlayerField1 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardPlayerField1.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardPlayerField2 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardPlayerField2.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardPlayerField3 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardPlayerField3.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardPlayerField4 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardPlayerField4.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            if (gameManager.CardEnemyField1 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardEnemyField1.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardEnemyField2 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardEnemyField2.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardEnemyField3 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardEnemyField3.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
            if (gameManager.CardEnemyField4 != fieldOn)
            {
                int diff = fieldOn.SelfCard.Health - gameManager.CardEnemyField4.SelfCard.Health;
                if (diff > 0)
                {
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                if (diff == 0)
                {
                    gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + diff;
                    fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
            }
        }
        //
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
