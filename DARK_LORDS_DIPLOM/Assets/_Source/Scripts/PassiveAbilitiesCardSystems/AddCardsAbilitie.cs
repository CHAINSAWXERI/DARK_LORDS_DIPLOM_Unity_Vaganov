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
        if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
        {
            Debug.Log("С ТВОЕЙ СТОРОНЫ");
            gameManager.GiveCardsToHand(gameManager.CurrentGame.PlayerDeck, gameManager.PlayerHandCards, gameManager.PlayerHand, WhoseCard.BluePlayer);
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            Debug.Log("СО СТОРОНЫ ВРАГА");
            gameManager.GiveCardsToHand(gameManager.CurrentGame.EnemyDeck, gameManager.EnemyHandCards, gameManager.EnemyHand, WhoseCard.RedPlayer);
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}

 