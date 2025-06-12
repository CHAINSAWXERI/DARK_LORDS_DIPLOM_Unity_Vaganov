using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostByOpCardsAbilitie", menuName = "ScriptableObjects/PassiveAbilities/BoostByOpCardsAbilitie")]
public class BoostByOpCardsAbilitie : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            Debug.Log(gameManager.EnemyHandCards.Count);

            for (int i = 2; (i <= gameManager.EnemyHandCards.Count) && (1 < gameManager.EnemyHandCards.Count); i += 2)
            {
                fieldOn.SelfCard.Attack += 1;
            }

            fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);

        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            Debug.Log(gameManager.PlayerHandCards.Count);

            for (int i = 2; (i <= gameManager.PlayerHandCards.Count) && (1 < gameManager.PlayerHandCards.Count); i += 2)
            {
                fieldOn.SelfCard.Attack += 1;
            }

            fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
        }
        //
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
