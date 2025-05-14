using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TakeCardFromDiscardAbility", menuName = "ScriptableObjects/Spells/TakeCardFromDiscardAbility")]
public class TakeCardFromDiscardAbilitySpell : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if ((dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD))
        {
            gameManager.GiveCardsToHand(gameManager.EnemyDiscardedDeck, gameManager.EnemyHandCards, gameManager.EnemyHand, WhoseCard.RedPlayer);
        }
        if ((dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD))
        {
            Debug.Log(gameManager.PlayerDiscardedDeck[gameManager.PlayerDiscardedDeck.Count - 1].Name);
            gameManager.GiveCardsToHand(gameManager.PlayerDiscardedDeck, gameManager.PlayerHandCards, gameManager.PlayerHand, WhoseCard.BluePlayer);
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}