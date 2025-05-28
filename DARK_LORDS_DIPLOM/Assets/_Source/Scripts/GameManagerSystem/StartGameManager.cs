using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;


public class StartGameManager : NetworkBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public InicilizationGameManager inicilizationGameManager;

    // Если нужно хранить ссылку на playerCommands
    public PlayerComands playerComands;

    [SyncVar(hook = nameof(OnPlayersCountChanged))]
    public int PlayersCount = 0;

    [Server]
    public void IncrementPlayersCount()
    {
        PlayersCount++;
        Debug.Log($"[Server] PlayersCount incremented: {PlayersCount}");

        if (PlayersCount == 2)
        {
            Debug.Log("[Server] Ready to start game");
            gameManager.StartGame();
        }
    }

    private void OnPlayersCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"[Client] PlayersCount changed from {oldCount} to {newCount}");
        if (newCount == 2)
        {
            // Локальные действия клиентов при старте игры
        }
    }
}

