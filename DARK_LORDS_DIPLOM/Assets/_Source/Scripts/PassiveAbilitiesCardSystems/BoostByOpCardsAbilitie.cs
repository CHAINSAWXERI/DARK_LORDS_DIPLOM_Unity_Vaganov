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
            fieldOn.SelfCard.Attack = fieldOn.SelfCard.Attack + gameManager.EnemyHandCards.Count;
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            Debug.Log(gameManager.PlayerHandCards.Count);
            fieldOn.SelfCard.Attack = fieldOn.SelfCard.Attack + gameManager.PlayerHandCards.Count;
        }
        //
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
