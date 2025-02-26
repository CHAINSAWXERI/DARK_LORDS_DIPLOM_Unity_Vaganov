using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealBoostAbility", menuName = "ScriptableObjects/PassiveAbilities/HealBoostAbility")]
public class HealBoostAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            if ((fieldOn.SelfCard.Health + gameManager.PlayerDiscardedDeck.Count) < 8)
            {
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health + gameManager.PlayerDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Health = 8;
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            if ((fieldOn.SelfCard.Health + gameManager.EnemyDiscardedDeck.Count) < 8)
            {
                fieldOn.SelfCard.Health = fieldOn.SelfCard.Health + gameManager.PlayerDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Health = 8;
            }
        }
        //
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager);
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
/*
public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
}
*/