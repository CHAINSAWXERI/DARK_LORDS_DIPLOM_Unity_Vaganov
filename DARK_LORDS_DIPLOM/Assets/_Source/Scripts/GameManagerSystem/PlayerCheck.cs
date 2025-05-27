using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCheck : NetworkBehaviour //MonoBehaviour
{
    public StartGameManager sgmanager;
    public SwithCamera swithCamera;
    public GameManager gameManager;

    private void Start()
    {
        sgmanager = FindObjectOfType<StartGameManager>();
        swithCamera = FindObjectOfType<SwithCamera>();
        gameManager = sgmanager.gameManager;
        swithCamera.playerComands = this.gameObject.GetComponent<PlayerComands>();
        gameManager.playerComands = this.gameObject.GetComponent<PlayerComands>();
        //sgmanager.ConnectedPlayer();
    }  
}
//        swithCamera.playerComands = this.gameObject.GetComponent<PlayerComands>();