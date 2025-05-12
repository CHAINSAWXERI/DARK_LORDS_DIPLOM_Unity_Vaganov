using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAbilitieSpell", menuName = "ScriptableObjects/Spells/DamageAbilitieSpell")]
public class DamageAbilitieSpell : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        Debug.Log("МАГИЯ РЫВОК");
        if ((dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD))
        {
            Debug.Log("С ТВОЕЙ СТОРОНЫ");
            if (gameManager.CardPlayerField1 != null)
            {
                gameManager.CardPlayerField1.SelfCard.Health--;
                gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
            }
            if (gameManager.CardPlayerField2 != null)
            {
                gameManager.CardPlayerField2.SelfCard.Attack--;
                gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
            }
            if (gameManager.CardPlayerField3 != null)
            {
                gameManager.CardPlayerField3.SelfCard.Attack--;
                gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
            }
            if (gameManager.CardPlayerField4 != null)
            {
                gameManager.CardPlayerField4.SelfCard.Attack--;
                gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
            }
        }
        if ((dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD))
        {
            Debug.Log("СО СТОРОНЫ ВРАГА");
            if (gameManager.CardEnemyField1 != null)
            {
                gameManager.CardEnemyField1.SelfCard.Attack--;
                gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
            }
            if (gameManager.CardEnemyField2 != null)
            {
                gameManager.CardEnemyField2.SelfCard.Attack--;
                gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
            }
            if (gameManager.CardEnemyField3 != null)
            {
                gameManager.CardEnemyField3.SelfCard.Attack--;
                gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
            }
            if (gameManager.CardEnemyField4 != null)
            {
                gameManager.CardEnemyField4.SelfCard.Attack--;
                gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
            }
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
