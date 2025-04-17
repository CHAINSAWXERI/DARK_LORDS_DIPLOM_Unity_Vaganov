using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour //MonoBehaviour
{
    [SyncVar]
    [SerializeField] public int PlayerHP;
    [SerializeField] public DeckScriptable DeckObj;

    /*
    [Server]
    public void SetDeck(DeckScriptable deck)
    {
        DeckObj = deck;
        // Здесь вы можете добавить логику для передачи информации о колоде клиентам
    }
    */
}
