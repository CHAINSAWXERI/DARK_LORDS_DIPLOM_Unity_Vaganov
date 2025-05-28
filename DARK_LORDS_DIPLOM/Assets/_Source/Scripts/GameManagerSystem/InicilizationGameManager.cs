using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class InicilizationGameManager : NetworkBehaviour
{
    [SerializeField] public GameManager gameManager;

    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;

    [SerializeField] public GameObject BlockPhone;
    [SerializeField] public GameObject BlockPhoneEnemy;
    [SerializeField] public GameObject LoseScreenEnemy;
    [SerializeField] public GameObject LoseScreenPlayer;
    [SerializeField] public GameObject WinScreenEnemy;
    [SerializeField] public GameObject WinScreenPlayer;
    [SerializeField] public GameObject EndTurnBtnEnemy;
    [SerializeField] public GameObject EndTurnBtnPlayer;
    [SerializeField] public GameObject BlueSpellScreen;
    [SerializeField] public GameObject RedSpellScreen;

    [HideInInspector] public PlayerComands playerComands;

    public void InicilizationGame()
    {
        Debug.Log("Inicilization Is Ready");

        if (NetworkServer.active) //&& NetworkClient.active
        {
            Debug.Log("RCP");

            if (EnemyCamera.gameObject.activeSelf)
            {
                SetActiveRCP(gameManager.RedSpellScreen, false);
                SetActiveRCP(gameManager.BlueSpellScreen, false);

                SetActiveRCP(gameManager.RedSpellScreen, false);
                SetActiveRCP(gameManager.BlueSpellScreen, false);

                SetActiveRCP(gameManager.LoseScreenEnemy, false);
                SetActiveRCP(gameManager.WinScreenEnemy, false);

                SetActiveRCP(gameManager.EnemyHand.gameObject, true);

                if (gameManager.IsPlayerTurn)
                {
                    Debug.Log("IsPlayerTurn");
                    SetActiveRCP(gameManager.BlockPhoneEnemy, true);
                }
                else
                {
                    Debug.Log("NotPlayerTurn");
                    SetActiveRCP(gameManager.BlockPhoneEnemy, false);
                }
            }

            if (PlayerCamera.gameObject.activeSelf)
            {
                SetActiveRCP(gameManager.RedSpellScreen, false);
                SetActiveRCP(gameManager.BlueSpellScreen, false);

                SetActiveRCP(gameManager.LoseScreenPlayer, false);
                SetActiveRCP(gameManager.WinScreenPlayer, false);

                SetActiveRCP(gameManager.PlayerHand.gameObject, true);

                if (gameManager.IsPlayerTurn)
                {
                    Debug.Log("IsPlayerTurn");
                    SetActiveRCP(gameManager.BlockPhone, false);
                }
                else
                {
                    Debug.Log("NotPlayerTurn");
                    SetActiveRCP(gameManager.BlockPhone, true);
                }
            }
        }
        else if (NetworkClient.isConnected)
        {
            Debug.Log("COM");

            if (EnemyCamera.gameObject.activeSelf)
            {
                playerComands.SetActive(gameManager.RedSpellScreen, false);
                playerComands.SetActive(gameManager.BlueSpellScreen, false);

                playerComands.SetActive(gameManager.RedSpellScreen, false);
                playerComands.SetActive(gameManager.BlueSpellScreen, false);

                playerComands.SetActive(gameManager.LoseScreenEnemy, false);
                playerComands.SetActive(gameManager.WinScreenEnemy, false);

                playerComands.SetActive(gameManager.EnemyHand.gameObject, true);

                if (gameManager.IsPlayerTurn)
                {
                    Debug.Log("IsPlayerTurn");
                    playerComands.SetActive(gameManager.BlockPhoneEnemy, true);
                }
                else
                {
                    Debug.Log("NotPlayerTurn");
                    playerComands.SetActive(gameManager.BlockPhoneEnemy, false);
                }
            }

            if (PlayerCamera.gameObject.activeSelf)
            {
                playerComands.SetActive(gameManager.RedSpellScreen, false);
                playerComands.SetActive(gameManager.BlueSpellScreen, false);

                playerComands.SetActive(gameManager.LoseScreenPlayer, false);
                playerComands.SetActive(gameManager.WinScreenPlayer, false);

                playerComands.SetActive(gameManager.PlayerHand.gameObject, true);

                if (gameManager.IsPlayerTurn)
                {
                    Debug.Log("IsPlayerTurn");
                    playerComands.SetActive(gameManager.BlockPhone, false);
                }
                else
                {
                    Debug.Log("NotPlayerTurn");
                    playerComands.SetActive(gameManager.BlockPhone, true);
                }
            }
        }
    }

    [ClientRpc]
    public void SetActiveRCP(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }
}

/*
        gameManager.BlockPhone = BlockPhone;
        gameManager.BlockPhoneEnemy = BlockPhoneEnemy;
        gameManager.LoseScreenEnemy = LoseScreenEnemy;
        gameManager.LoseScreenPlayer = LoseScreenPlayer;
        gameManager.WinScreenEnemy = WinScreenEnemy;
        gameManager.WinScreenPlayer = WinScreenPlayer;
        gameManager.EndTurnBtnEnemy = EndTurnBtnEnemy;
        gameManager.EndTurnBtnPlayer = EndTurnBtnPlayer;
        gameManager.BlueSpellScreen = BlueSpellScreen;
        gameManager.RedSpellScreen = RedSpellScreen;

        if (gameManager.BlockPhone       == null)
        {
            Debug.Log("gameManager.BlockPhone");
        }
        if (gameManager.BlockPhoneEnemy  == null)
        {
            Debug.Log("gameManager.BlockPhoneEnemy");
        }
        if (gameManager.LoseScreenEnemy  == null)
        {
            Debug.Log("gameManager.LoseScreenEnemy");
        }
        if (gameManager.LoseScreenPlayer == null)
        {
            Debug.Log("gameManager.LoseScreenPlayer");
        }
        if (gameManager.WinScreenEnemy   == null)
        {
            Debug.Log("gameManager.WinScreenEnemy");
        }
        if (gameManager.WinScreenPlayer  == null)
        {
            Debug.Log("gameManager.WinScreenPlayer");
        }
        if (gameManager.EndTurnBtnEnemy  == null)
        {
            Debug.Log("gameManager.EndTurnBtnEnemy");
        }
        if (gameManager.EndTurnBtnPlayer == null)
        {
            Debug.Log("gameManager.EndTurnBtnPlayer");
        }
        if (gameManager.BlueSpellScreen  == null)
        {
            Debug.Log("gameManager.BlueSpellScreen");
        }
        if (gameManager.RedSpellScreen   == null)
        {
            Debug.Log("gameManager.RedSpellScreen");
        }

        if (BlockPhone       == null)
        {
            Debug.Log("BlockPhone");
        }
        if (BlockPhoneEnemy  == null)
        {
            Debug.Log("BlockPhoneEnemy");
        }
        if (LoseScreenEnemy  == null)
        {
            Debug.Log("LoseScreenEnemy");
        }
        if (LoseScreenPlayer == null)
        {
            Debug.Log("LoseScreenPlayer");
        }
        if (WinScreenEnemy   == null)
        {
            Debug.Log("WinScreenEnemy");
        }
        if (WinScreenPlayer  == null)
        {
            Debug.Log("WinScreenPlayer");
        }
        if (EndTurnBtnEnemy  == null)
        {
            Debug.Log("EndTurnBtnEnemy");
        }
        if (EndTurnBtnPlayer == null)
        {
            Debug.Log("EndTurnBtnPlayer");
        }
        if (BlueSpellScreen  == null)
        {
            Debug.Log("BlueSpellScreen");
        }
        if (RedSpellScreen   == null)
        {
            Debug.Log("RedSpellScreen");
        }

        if (BlockPhone       == gameManager.BlockPhone       )
        {
            Debug.Log("(BlockPhone != gameManager.BlockPhone");
        }
        if (BlockPhoneEnemy  == gameManager.BlockPhoneEnemy  )
        {
            Debug.Log("BlockPhoneEnemy  != gameManager.BlockPhoneEnemy");
        }
        if (LoseScreenEnemy  == gameManager.LoseScreenEnemy  )
        {
            Debug.Log("LoseScreenEnemy  != gameManager.LoseScreenEnemy");
        }
        if (LoseScreenPlayer == gameManager.LoseScreenPlayer )
        {
            Debug.Log("LoseScreenPlayer != gameManager.LoseScreenPlayer");
        }
        if (WinScreenEnemy   == gameManager.WinScreenEnemy   )
        {
            Debug.Log("WinScreenEnemy   != gameManager.WinScreenEnemy");
        }
        if (WinScreenPlayer  == gameManager.WinScreenPlayer  )
        {
            Debug.Log("WinScreenPlayer  != gameManager.WinScreenPlayer");
        }
        if (EndTurnBtnEnemy  == gameManager.EndTurnBtnEnemy  )
        {
            Debug.Log("EndTurnBtnEnemy  != gameManager.EndTurnBtnEnemy");
        }
        if (EndTurnBtnPlayer == gameManager.EndTurnBtnPlayer )
        {
            Debug.Log("EndTurnBtnPlayer != gameManager.EndTurnBtnPlayer");
        }
        if (BlueSpellScreen  == gameManager.BlueSpellScreen  )
        {
            Debug.Log("BlueSpellScreen  != gameManager.BlueSpellScreen");
        }
        if (RedSpellScreen   == gameManager.RedSpellScreen   )
        {
            Debug.Log("RedSpellScreen   != gameManager.RedSpellScreen");
        }
        */