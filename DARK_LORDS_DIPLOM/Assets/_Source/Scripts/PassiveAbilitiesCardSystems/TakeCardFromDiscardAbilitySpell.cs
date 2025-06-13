using Mirror;
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
            if (NetworkServer.active)
            {
                Debug.Log("HOST DROP PLACE");
                gameManager.GiveCardsToHandServer(WhoseCard.RedPlayer, gameManager.CurrentGame.EnemyCharacter, 1, FromDeck.DiscaredDeck);
            }
            else if (NetworkClient.isConnected)
            {
                Debug.Log("CLIENT DROP PLACE");
                gameManager.GiveCardsToHandCommand(WhoseCard.RedPlayer, gameManager.CurrentGame.EnemyCharacter, 1, FromDeck.DiscaredDeck);
            }
        }
        if ((dropPlaceOn.fieldType == FieldType.SELF_SPELL_FIELD))
        {
            if (NetworkServer.active)
            {
                Debug.Log("HOST DROP PLACE");
                gameManager.GiveCardsToHandServer(WhoseCard.BluePlayer, gameManager.CurrentGame.PlayerCharacter, 1, FromDeck.DiscaredDeck);
            }
            else if (NetworkClient.isConnected)
            {
                Debug.Log("CLIENT DROP PLACE");
                gameManager.GiveCardsToHandCommand(WhoseCard.BluePlayer, gameManager.CurrentGame.PlayerCharacter, 1, FromDeck.DiscaredDeck);
            }
        }
    }

    public override string GetAbilityText()
    {
        return TextPassiveAbility;
    }
}