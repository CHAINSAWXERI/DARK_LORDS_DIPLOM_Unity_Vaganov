using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonSpawnPassiveAbility", menuName = "ScriptableObjects/Spells/SkeletonSpawnPassiveAbility")]
public class SkeletonSpawnPassiveAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;
    [SerializeField] public CardScriptable cardScr;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if ((dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD))
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameManager.EnemyHandCards.Count == gameManager.maxCardsInHand)
                {
                    return;
                }

                Card card = new(cardScr.Name + " Из Мешка", cardScr.Logo, cardScr.Attack, cardScr.Health, cardScr.Power, cardScr.PassiveAbilities, cardScr.CardType, WhoseCard.RedPlayer, cardScr.CoreId);

                card.Health = card.MaxHealth;
                card.Attack = card.MaxAttack;

                GameObject cardGO = Instantiate(gameManager.CardPref, gameManager.EnemyHand, false);

                CardInfoScript cardGOInfo = cardGO.GetComponent<CardInfoScript>();

                cardGOInfo.ShowCardInfo(card, gameManager.IdPlayerCardCount, gameManager, WhoseCard.RedPlayer);
                gameManager.EnemyHandCards.Add(cardGOInfo);
                gameManager.EnemyHandId.Add(gameManager.IdPlayerCardCount);
                gameManager.EnemyHandCoreID.Add(card.CoreID);

                gameManager.IdPlayerCardCount++;
            }
        }
        if ((dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD))
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameManager.PlayerHandCards.Count == gameManager.maxCardsInHand)
                {
                    return;
                }

                Card card = new(cardScr.Name + " Из Мешка", cardScr.Logo, cardScr.Attack, cardScr.Health, cardScr.Power, cardScr.PassiveAbilities, cardScr.CardType, WhoseCard.BluePlayer, cardScr.CoreId);

                card.Health = card.MaxHealth;
                card.Attack = card.MaxAttack;

                GameObject cardGO = Instantiate(gameManager.CardPref, gameManager.PlayerHand, false);

                CardInfoScript cardGOInfo = cardGO.GetComponent<CardInfoScript>();

                cardGOInfo.ShowCardInfo(card, gameManager.IdPlayerCardCount, gameManager, WhoseCard.BluePlayer);
                gameManager.PlayerHandCards.Add(cardGOInfo);
                gameManager.PlayerHandId.Add(gameManager.IdPlayerCardCount);
                gameManager.PlayerHandCoreID.Add(card.CoreID);

                gameManager.IdPlayerCardCount++;
            }
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
