using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageBoostAbility", menuName = "ScriptableObjects/PassiveAbilities/DamageBoostAbility")]
public class DamageBoostAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            if ((fieldOn.SelfCard.Attack + gameManager.PlayerDiscardedDeck.Count) < 6)
            {
                fieldOn.SelfCard.Attack = fieldOn.SelfCard.Attack + gameManager.PlayerDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Attack = 6;
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            if ((fieldOn.SelfCard.Attack + gameManager.EnemyDiscardedDeck.Count) < 6)
            {
                fieldOn.SelfCard.Attack = fieldOn.SelfCard.Attack + gameManager.EnemyDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Attack = 6;
            }
        }
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager);
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }

//    public override stats 
}