using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private int PlayersCount = 0;

    public void ConnectedPlayer()
    {
        PlayersCount++;
        Debug.Log("+1 PLAYER");
        if (PlayersCount == 2)
        {
            Debug.Log("READY START GAME");
            gameManager.StartGame();
        }
    }
    
}