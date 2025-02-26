using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathAliesBoostAbility", menuName = "ScriptableObjects/PassiveAbilities/DeathAliesBoostAbility")]
public class DeathAliesBoostAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}