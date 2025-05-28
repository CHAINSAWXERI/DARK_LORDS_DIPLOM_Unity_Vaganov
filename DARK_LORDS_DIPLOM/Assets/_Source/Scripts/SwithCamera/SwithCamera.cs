using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SwithCamera : NetworkBehaviour
{
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;

    [SerializeField] public GameObject PlayerCanvas;
    [SerializeField] public GameObject EnemyCanvas;
    [SerializeField] public GameObject PlayerBtn;
    [SerializeField] public GameObject EnemyBtn;

    [SerializeField] public DeckScriptable KnightDeck;
    [SerializeField] public DeckScriptable NecroDeck;

    [SerializeField] public PlayerInfo PlayerInfo;
    [SerializeField] public PlayerInfo EnemyInfo;

    [SerializeField] public GameManager gameManager;

    [SerializeField] public PlayerCommands playerCommands;

    // --- Переключение камер ---
    public void SwitchToEnemy()
    {
        // Вызов с клиента или сервера
        if (NetworkServer.active)
        {
            Debug.Log("SERVER");
        }
        else if (NetworkClient.isConnected)
        {
            Debug.Log("CLIENT");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        PlayerCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void SwitchToPlayer()
    {
        // Вызов с клиента или сервера
        if (NetworkServer.active)
        {
            Debug.Log("SERVER");
        }
        else if (NetworkClient.isConnected)
        {
            Debug.Log("CLIENT");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        EnemyCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void SwitchToKnightLocal()
    {
        PlayerInfo.DeckObj = KnightDeck;
        EnemyInfo.DeckObj = NecroDeck;
        gameManager.StartGame();
    }

    public void SwitchToNecroLocal()
    {
        PlayerInfo.DeckObj = NecroDeck;
        EnemyInfo.DeckObj = KnightDeck;
        gameManager.StartGame();
    }
}
/*
*/