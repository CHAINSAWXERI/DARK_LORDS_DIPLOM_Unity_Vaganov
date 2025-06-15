using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostAttackSideCardsPassiveAbility", menuName = "ScriptableObjects/PassiveAbilities/BoostAttackSideCardsPassiveAbility")]
public class BoostAttackSideCardsPassiveAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (fieldRight != null)
        {
            Debug.Log("Field Right is Not Null");
        }
        if (fieldLeft != null)
        {
            Debug.Log("Field Left is Not Null");
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}