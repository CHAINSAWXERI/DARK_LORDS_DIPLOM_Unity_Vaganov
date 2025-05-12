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
                    gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
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
                    gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
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
                    gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
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
                    gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
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
                    gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
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
                    gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
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
                    gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
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
                    gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
                    fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    return;
                }
                gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + diff;
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
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
