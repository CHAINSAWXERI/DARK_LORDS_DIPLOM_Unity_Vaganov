using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicilizationGameManager : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

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

    public void InicilizationGame()
    {
        Debug.Log("Inicilization Is Ready");
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










        gameManager.RedSpellScreen.SetActive(false);
        gameManager.BlueSpellScreen.SetActive(false);

        gameManager.PlayerHand.gameObject.SetActive(true); // строка 87
        gameManager.EnemyHand.gameObject.SetActive(true);


        if (gameManager.IsPlayerTurn)
        {
            Debug.Log("IsPlayerTurn");
            gameManager.BlockPhone.SetActive(false);
            gameManager.BlockPhoneEnemy.SetActive(true);
        }
        else
        {
            Debug.Log("NotPlayerTurn");
            gameManager.BlockPhone.SetActive(true);
            gameManager.BlockPhoneEnemy.SetActive(false);

        }

        gameManager.LoseScreenPlayer.SetActive(false);
        gameManager.WinScreenPlayer.SetActive(false);

        gameManager.RedSpellScreen.SetActive(false);
        gameManager.BlueSpellScreen.SetActive(false);

        gameManager.LoseScreenEnemy.SetActive(false);
        gameManager.WinScreenEnemy.SetActive(false);
    }
}
