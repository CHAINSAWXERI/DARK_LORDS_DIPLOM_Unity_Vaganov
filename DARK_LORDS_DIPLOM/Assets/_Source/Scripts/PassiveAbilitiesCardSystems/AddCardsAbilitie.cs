using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddCardsAbilitie", menuName = "ScriptableObjects/PassiveAbilities/AddCardsAbilitie")]
public class AddCardsAbilitie : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        Debug.Log("ВОРОН АКТИВИРОВАН");
        if ((dropPlaceOn.fieldType == FieldType.SELF_FIELD) || (dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD))
        {
            Debug.Log("С ТВОЕЙ СТОРОНЫ");
            gameManager.GiveCardToHand(WhoseCard.BluePlayer, gameManager.CurrentGame.PlayerCharacter);
        }
        if ((dropPlaceOn.fieldType == FieldType.ENEMY_FIELD) || (dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD))
        {
            Debug.Log("СО СТОРОНЫ ВРАГА");
            gameManager.GiveCardToHand(WhoseCard.RedPlayer, gameManager.CurrentGame.EnemyCharacter);
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}

/*
*/ 