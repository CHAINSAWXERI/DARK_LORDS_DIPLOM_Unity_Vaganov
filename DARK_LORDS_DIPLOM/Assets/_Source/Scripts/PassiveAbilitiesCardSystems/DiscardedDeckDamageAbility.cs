using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiscardedDeckDamageAbility", menuName = "ScriptableObjects/PassiveAbilities/DiscardedDeckDamageAbility")]
public class DiscardedDeckDamageAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;
    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        int dc = 0;
        if (fieldOpposite != null)
        {
            if (dropPlaceOn.fieldType == FieldType.SELF_FIELD)
            {
                for (int i = 0; i < gameManager.PlayerDiscardedDeck.Count; i++)
                {
                    dc++;
                    if (dc == 3)
                    {
                        fieldOpposite.SelfCard.Health = fieldOpposite.SelfCard.Health - 1;
                        
                        if (fieldOpposite.SelfCard.Health == 0)
                        {
                            return;
                        }
                        dc = 0;
                    }
                }
                fieldOpposite.ShowCardInfo(fieldOpposite.SelfCard, fieldOpposite.ID, gameManager, fieldOpposite.SelfCard.WhoseCard);
            }
            if (dropPlaceOn.fieldType == FieldType.ENEMY_FIELD)
            {
                for (int i = 0; i < gameManager.EnemyDiscardedDeck.Count; i++)
                {
                    dc++;
                    if (dc == 3)
                    {
                        fieldOpposite.SelfCard.Health = fieldOpposite.SelfCard.Health - 1;
                        
                        if (fieldOpposite.SelfCard.Health == 0)
                        {
                            return;
                        }
                        dc = 0;
                    }
                }
                fieldOpposite.ShowCardInfo(fieldOpposite.SelfCard, fieldOpposite.ID, gameManager, fieldOpposite.SelfCard.WhoseCard);
            }
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
