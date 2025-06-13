using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealCharacterFromDiscardPassiveAbility", menuName = "ScriptableObjects/Spells/HealCharacterFromDiscardPassiveAbility")]
public class HealCharacterFromDiscardPassiveAbility : PassiveAbility
{
    [SerializeField] public string TextPassiveAbility;

    public override void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager)
    {
        Debug.Log("HealCharacterFromDiscardPassiveAbility");
        if (dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD)
        {
            gameManager.PlayerHP = gameManager.PlayerHP + gameManager.PlayerDiscardedDeck.Count;

            if (gameManager.PlayerHP > gameManager.HPMax)
            {
                gameManager.PlayerHP = gameManager.HPMax;
            }

            gameManager.PlayerHPSlider.value = gameManager.PlayerHP;
            gameManager.HPTxtPlayerOnPlayer.text = gameManager.PlayerHP.ToString();
            gameManager.HPTxtPlayerOnEnemy.text = gameManager.PlayerHP.ToString();
        }
        if (dropPlaceOn.fieldType == FieldType.ENEMY_SPELL_FIELD)
        {
            gameManager.EnemyHP = gameManager.EnemyHP + gameManager.EnemyDiscardedDeck.Count;

            if (gameManager.EnemyHP > gameManager.HPMax)
            {
                gameManager.EnemyHP = gameManager.HPMax;
            }

            gameManager.EnemyHPSlider.value = gameManager.EnemyHP;
            gameManager.HPTxtEnemyOnPlayer.text = gameManager.EnemyHP.ToString();
            gameManager.HPTxtEnemyOnEnemy.text = gameManager.EnemyHP.ToString();
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}
