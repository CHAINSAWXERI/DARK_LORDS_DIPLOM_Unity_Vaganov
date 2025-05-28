using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SwithCamera : NetworkBehaviour
{
    [SerializeField] public StartGameManager startGameManager;
    [SerializeField] public GameManager gameManager;
    
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;
    [SerializeField] public GameObject PlayerCanvas;
    [SerializeField] public GameObject EnemyCanvas;

    [SerializeField] public GameObject PlayerBtn;
    [SerializeField] public GameObject EnemyBtn;

    [HideInInspector] public PlayerComands playerComands;

    public void SwitchToEnemy()
    {
        if (NetworkServer.active) //&& NetworkClient.active
        {
            OffButtonRCP(EnemyBtn);
            Debug.Log("RCP");
        }
        else if (NetworkClient.isConnected)
        {
            playerComands.SetActive(EnemyBtn, false);
            Debug.Log("COM");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        PlayerCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void SwitchToPlayer()
    {
        if (NetworkServer.active) //&& NetworkClient.active
        {
            OffButtonRCP(PlayerBtn);
            Debug.Log("RCP");
        }
        else if (NetworkClient.isConnected)
        {
            playerComands.SetActive(PlayerBtn, false);
            Debug.Log("COM");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        EnemyCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }


    [ClientRpc]
    public void OffButtonRCP(GameObject button)
    {
        button.SetActive(false);
    }
}