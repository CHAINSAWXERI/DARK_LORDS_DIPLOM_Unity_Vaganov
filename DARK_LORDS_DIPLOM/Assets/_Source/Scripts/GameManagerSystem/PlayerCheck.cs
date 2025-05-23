using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCheck : NetworkBehaviour //MonoBehaviour
{
    public StartGameManager sgmanager;

    private void Awake()
    {
        sgmanager = FindObjectOfType<StartGameManager>();
        sgmanager.ConnectedPlayer();
    }
}
