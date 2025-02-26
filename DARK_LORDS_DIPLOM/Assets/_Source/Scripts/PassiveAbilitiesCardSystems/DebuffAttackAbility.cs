using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffAttackAbility", menuName = "ScriptableObjects/PassiveAbilities/DebuffAttackAbility")]
public class DebuffAttackAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (fieldOpposite != null)
        {
            fieldOpposite.SelfCard.Attack = fieldOpposite.SelfCard.Attack - 2;

            fieldOpposite.ShowCardInfo(fieldOpposite.SelfCard, fieldOpposite.ID, gameManager);
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
