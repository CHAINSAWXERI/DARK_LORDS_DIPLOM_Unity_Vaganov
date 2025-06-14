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
            Debug.Log("SELF_FIELD");
            if (gameManager.PlayerDiscardedDeck.Count < 6)
            {
                Debug.Log("Player Discarded Deck Count" + gameManager.PlayerDiscardedDeck.Count);
                fieldOn.SelfCard.MaxHealth = gameManager.PlayerDiscardedDeck.Count;
                fieldOn.SelfCard.Health = gameManager.PlayerDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Health = 6;
                fieldOn.SelfCard.MaxHealth = 6;
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
        {
            Debug.Log("Enemy Discarded Deck Count" + gameManager.EnemyDiscardedDeck.Count);
            if (gameManager.EnemyDiscardedDeck.Count < 6)
            {
                fieldOn.SelfCard.MaxHealth = fieldOn.SelfCard.MaxHealth + gameManager.EnemyDiscardedDeck.Count;
                fieldOn.SelfCard.Health = gameManager.EnemyDiscardedDeck.Count;
            }
            else
            {
                fieldOn.SelfCard.Health = 6;
                fieldOn.SelfCard.MaxHealth = 6;
            }
        }
        //
        fieldOn.ShowCardInfo(fieldOn.SelfCard, fieldOn.ID, gameManager, fieldOn.WhoseCard);
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