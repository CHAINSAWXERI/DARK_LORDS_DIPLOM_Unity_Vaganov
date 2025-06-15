using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCommands : MonoBehaviour
{
    public PlayerCommands commands;
    public SwithCamera swithCamera;
    public MirrorManager mirrorManager;
    public GameManager gameManager;

    void Start()
    {
        commands = gameObject.GetComponent<PlayerCommands>();
        if (commands == null)
        {
            Debug.Log("commands is null");
        }
        swithCamera = FindObjectOfType<SwithCamera>();
        if (swithCamera == null)
        {
            Debug.Log("swithCamera is null");
        }
        mirrorManager = FindObjectOfType<MirrorManager>();
        if (mirrorManager == null)
        {
            Debug.Log("mirrorManager is null");
        }
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("gameManager is null");
        }
        swithCamera.playerCommands = commands;
        if (swithCamera.playerCommands == null)
        {
            Debug.Log("swithCamera.playerCommands is null");
        }
        mirrorManager.playerCommands = commands;
        if (mirrorManager.playerCommands == null)
        {
            Debug.Log("mirrorManager.playerCommands is null");
        }
        gameManager.playerCommands = commands;
        if (gameManager.playerCommands == null)
        {
            Debug.Log("gameManager.playerCommands is null");
        }
        commands.gameManager = gameManager;
        if (commands.gameManager == null)
        {
            Debug.Log("commands.gameManager is null");
        }

        /*
        gameManager.PlayerInScene++;

        if (gameManager.PlayerInScene == 2)
        {
            Debug.Log("2 ИГРОКА!!!!!!");
            gameManager.BtnStartGamePlayer.ShowObjectForHost();
            gameManager.BtnStartGameEnemy.ShowObjectForHost();
        }
        */

        Debug.Log("Commands Is Everywhere");
    }
}
