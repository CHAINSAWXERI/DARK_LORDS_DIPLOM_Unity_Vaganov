using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NullPassiveAbility", menuName = "ScriptableObjects/PassiveAbilities/NullPassiveAbility")]
public class NullPassiveAbility : PassiveAbility
{
    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
    }

    public override string GetAbilityText()
    {
        return " ";
    }
}
