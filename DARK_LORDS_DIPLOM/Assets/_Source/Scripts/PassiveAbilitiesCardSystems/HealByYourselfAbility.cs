using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealByYourselfAbility", menuName = "ScriptableObjects/PassiveAbilities/HealByYourselfAbility")]
public class HealByYourselfAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        Debug.Log("Хил Вампира");
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            if (gameManager.CardPlayerField1 != fieldOn && gameManager.CardPlayerField1 != null)
            {
                int diff = gameManager.CardPlayerField1.SelfCard.MaxHealth - gameManager.CardPlayerField1.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardPlayerField1.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardPlayerField1.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    Debug.Log("INSIDE Player1");
                    if (vHelath < 0)
                    {
                        gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardPlayerField2 != fieldOn && gameManager.CardPlayerField2 != null)
            {
                int diff = gameManager.CardPlayerField2.SelfCard.MaxHealth - gameManager.CardPlayerField2.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardPlayerField2.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardPlayerField2.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    Debug.Log("INSIDE Player2");
                    if (vHelath < 0)
                    {
                        gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardPlayerField3 != fieldOn && gameManager.CardPlayerField3 != null)
            {
                int diff = gameManager.CardPlayerField3.SelfCard.MaxHealth - gameManager.CardPlayerField3.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardPlayerField3.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardPlayerField3.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    Debug.Log("INSIDE Player3");
                    if (vHelath < 0)
                    {
                        gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardPlayerField4 != fieldOn && gameManager.CardPlayerField4 != null)
            {
                int diff = gameManager.CardPlayerField4.SelfCard.MaxHealth - gameManager.CardPlayerField4.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardPlayerField4.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardPlayerField4.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    Debug.Log("INSIDE Player4");
                    if (vHelath < 0)
                    {
                        gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            if (gameManager.CardEnemyField1 != fieldOn && gameManager.CardEnemyField1 != null)
            {
                int diff = gameManager.CardEnemyField1.SelfCard.MaxHealth - gameManager.CardEnemyField1.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardEnemyField1.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardEnemyField1.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    if (vHelath < 0)
                    {
                        gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardEnemyField2 != fieldOn && gameManager.CardEnemyField2 != null)
            {
                int diff = gameManager.CardEnemyField2.SelfCard.MaxHealth - gameManager.CardEnemyField2.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardEnemyField2.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardEnemyField2.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    if (vHelath < 0)
                    {
                        gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardEnemyField3 != fieldOn && gameManager.CardEnemyField3 != null)
            {
                int diff = gameManager.CardEnemyField3.SelfCard.MaxHealth - gameManager.CardEnemyField3.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardEnemyField3.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardEnemyField3.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    if (vHelath < 0)
                    {
                        gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
            }
            if (gameManager.CardEnemyField4 != fieldOn && gameManager.CardEnemyField4 != null)
            {
                int diff = gameManager.CardEnemyField4.SelfCard.MaxHealth - gameManager.CardEnemyField4.SelfCard.Health;
                Debug.Log("MX = " + gameManager.CardEnemyField4.SelfCard.MaxHealth);
                Debug.Log("NW = " + gameManager.CardEnemyField4.SelfCard.Health);
                Debug.Log("X = " + diff);
                Debug.Log("V = " + fieldOn.SelfCard.Health);
                int vHelath = fieldOn.SelfCard.Health - diff;
                if ((diff != 0) && (diff > 0))
                {
                    if (vHelath < 0)
                    {
                        gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + fieldOn.SelfCard.Health;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    if (vHelath == 0)
                    {
                        gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                        return;
                    }
                    else
                    {
                        gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + diff;
                        fieldOn.SelfCard.Health = fieldOn.SelfCard.Health - diff;
                        gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
                        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
                    }
                }
                Debug.Log("V2 = " + fieldOn.SelfCard.Health);
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
