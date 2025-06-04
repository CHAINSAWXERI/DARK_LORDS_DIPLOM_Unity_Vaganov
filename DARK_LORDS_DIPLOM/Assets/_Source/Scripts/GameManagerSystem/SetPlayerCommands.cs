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
        swithCamera = FindObjectOfType<SwithCamera>();
        mirrorManager = FindObjectOfType<MirrorManager>();
        gameManager = FindObjectOfType<GameManager>();
        swithCamera.playerCommands = commands;
        mirrorManager.playerCommands = commands;
        gameManager.playerCommands = commands;
        commands.gameManager = gameManager;

        Debug.Log("Commands Is Everywhere");
    }
}
