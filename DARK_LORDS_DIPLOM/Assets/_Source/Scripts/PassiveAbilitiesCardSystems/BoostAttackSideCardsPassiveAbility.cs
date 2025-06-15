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
            fieldRight.SelfCard.Attack++;
            fieldRight.ShowCardInfo(fieldRight.SelfCard, fieldRight.ID, gameManager, fieldRight.WhoseCard);

            Debug.Log("Field Right is Not Null");
        }
        if (fieldLeft != null)
        {
            fieldLeft.SelfCard.Attack++;
            fieldLeft.ShowCardInfo(fieldLeft.SelfCard, fieldLeft.ID, gameManager, fieldLeft.WhoseCard);

            Debug.Log("Field Left is Not Null");
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}