using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SwithCamera : NetworkBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;
    [SerializeField] public GameObject PlayerCanvas;
    [SerializeField] public GameObject EnemyCanvas;
    [SyncVar]
    [SerializeField] public GameObject PlayerBtn;
    [SyncVar]
    [SerializeField] public GameObject EnemyBtn;

    void Awake()
    {
    }

    public void SwitchToEnemy()
    {
        //EnemyBtn.SetActive(false);

        if (NetworkServer.active) // && NetworkClient.active
        {
            OffButtonRCP(EnemyBtn);
            Debug.Log("RCP");
        }
        else if (NetworkClient.isConnected)
        {
            OffButtonCOM(EnemyBtn);
            Debug.Log("COM");
        }
        
        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        PlayerCanvas.GetComponent<GraphicRaycaster>().enabled = false;
        gameManager.RedSpellScreen.SetActive(false);
        gameManager.BlueSpellScreen.SetActive(false);
    }

    public void SwitchToPlayer()
    {
        //PlayerBtn.SetActive(false);

        if (NetworkServer.active) //&& NetworkClient.active
        {
            OffButtonRCP(PlayerBtn);
            Debug.Log("RCP");
        }
        else if (NetworkClient.isConnected)
        {
            OffButtonCOM(PlayerBtn);
            Debug.Log("COM");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        EnemyCanvas.GetComponent<GraphicRaycaster>().enabled = false;
        gameManager.RedSpellScreen.SetActive(false);
        gameManager.BlueSpellScreen.SetActive(false);
    }

    [ClientRpc]
    public void OffButtonRCP(GameObject button)
    {
        Debug.Log("button");
        button.SetActive(false);
    }

    [Command]
    public void OffButtonCOM(GameObject button)
    {
        OffButtonRCP(button);
        Debug.Log("button COM");
    }
}
