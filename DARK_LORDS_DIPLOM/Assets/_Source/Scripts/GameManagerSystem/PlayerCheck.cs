using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCheck : NetworkBehaviour //MonoBehaviour
{
    public StartGameManager sgmanager;
    public SwithCamera swithCamera;
    public GameManager gameManager;
    public InicilizationGameManager inicilizationGameManager;

    private void Start()
    {
        sgmanager = FindObjectOfType<StartGameManager>();
        swithCamera = FindObjectOfType<SwithCamera>();
        inicilizationGameManager = FindObjectOfType<InicilizationGameManager>();

        sgmanager.inicilizationGameManager = inicilizationGameManager;
        gameManager = sgmanager.gameManager;
        swithCamera.playerComands = this.gameObject.GetComponent<PlayerComands>();
        gameManager.playerComands = this.gameObject.GetComponent<PlayerComands>();
        sgmanager.playerComands = this.gameObject.GetComponent<PlayerComands>();
        inicilizationGameManager.playerComands = this.gameObject.GetComponent<PlayerComands>();
        //sgmanager.ConnectedPlayer();
    }  
}
//        swithCamera.playerComands = this.gameObject.GetComponent<PlayerComands>();