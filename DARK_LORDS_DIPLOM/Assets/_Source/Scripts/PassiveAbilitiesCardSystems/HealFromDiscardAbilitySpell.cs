using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealFromDiscardAbilitySpell", menuName = "ScriptableObjects/Spells/HealFromDiscardAbilitySpell")]
public class HealFromDiscardAbilitySpell : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        if (dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD)
        {
            if (gameManager.CardPlayerField1 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    Debug.Log("ПЕРВОЕ ПОЛЕ");
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].Name);
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].MaxHealth);
                    if (gameManager.PlayerDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardPlayerField1.SelfCard.Health = gameManager.CardPlayerField1.SelfCard.Health + gameManager.PlayerDiscardedDeck[x].MaxHealth;
                        gameManager.CardPlayerField1.ShowCardInfo(gameManager.CardPlayerField1.SelfCard, gameManager.CardPlayerField1.ID, gameManager, gameManager.CardPlayerField1.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardPlayerField2 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    Debug.Log("ВТОРОЕ ПОЛЕ");
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].Name);
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].MaxHealth);
                    if (gameManager.PlayerDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardPlayerField2.SelfCard.Health = gameManager.CardPlayerField2.SelfCard.Health + gameManager.PlayerDiscardedDeck[x].MaxHealth;
                        gameManager.CardPlayerField2.ShowCardInfo(gameManager.CardPlayerField2.SelfCard, gameManager.CardPlayerField2.ID, gameManager, gameManager.CardPlayerField2.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardPlayerField3 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    Debug.Log("ТРЕТЬЕ ПОЛЕ");
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].Name);
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].MaxHealth);
                    if (gameManager.PlayerDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardPlayerField3.SelfCard.Health = gameManager.CardPlayerField3.SelfCard.Health + gameManager.PlayerDiscardedDeck[x].MaxHealth;
                        gameManager.CardPlayerField3.ShowCardInfo(gameManager.CardPlayerField3.SelfCard, gameManager.CardPlayerField3.ID, gameManager, gameManager.CardPlayerField3.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardPlayerField4 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    Debug.Log("ЧЕТВЁРТОЕ ПОЛЕ");
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].Name);
                    Debug.Log(gameManager.PlayerDiscardedDeck[x].MaxHealth);
                    if (gameManager.PlayerDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardPlayerField4.SelfCard.Health = gameManager.CardPlayerField4.SelfCard.Health + gameManager.PlayerDiscardedDeck[x].MaxHealth;
                        gameManager.CardPlayerField4.ShowCardInfo(gameManager.CardPlayerField4.SelfCard, gameManager.CardPlayerField4.ID, gameManager, gameManager.CardPlayerField4.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD)
        {
            if (gameManager.CardEnemyField1 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    if (gameManager.EnemyDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardEnemyField1.SelfCard.Health = gameManager.CardEnemyField1.SelfCard.Health + gameManager.EnemyDiscardedDeck[x].MaxHealth;
                        gameManager.CardEnemyField1.ShowCardInfo(gameManager.CardEnemyField1.SelfCard, gameManager.CardEnemyField1.ID, gameManager, gameManager.CardEnemyField1.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardEnemyField2 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    if (gameManager.EnemyDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardEnemyField2.SelfCard.Health = gameManager.CardEnemyField2.SelfCard.Health + gameManager.EnemyDiscardedDeck[x].MaxHealth;
                        gameManager.CardEnemyField2.ShowCardInfo(gameManager.CardEnemyField2.SelfCard, gameManager.CardEnemyField2.ID, gameManager, gameManager.CardEnemyField2.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardEnemyField3 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    if (gameManager.EnemyDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardEnemyField3.SelfCard.Health = gameManager.CardEnemyField3.SelfCard.Health + gameManager.EnemyDiscardedDeck[x].MaxHealth;
                        gameManager.CardEnemyField3.ShowCardInfo(gameManager.CardEnemyField3.SelfCard, gameManager.CardEnemyField3.ID, gameManager, gameManager.CardEnemyField3.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
            if (gameManager.CardEnemyField4 != null)
            {
                bool i = false;
                int x = 0;
                while (!i)
                {
                    if (gameManager.EnemyDiscardedDeck[x].CardType == CardType.Creature)
                    {
                        gameManager.CardEnemyField4.SelfCard.Health = gameManager.CardEnemyField4.SelfCard.Health + gameManager.EnemyDiscardedDeck[x].MaxHealth;
                        gameManager.CardEnemyField4.ShowCardInfo(gameManager.CardEnemyField4.SelfCard, gameManager.CardEnemyField4.ID, gameManager, gameManager.CardEnemyField4.WhoseCard);
                        i = true;
                    }
                    x++;
                }
            }
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}