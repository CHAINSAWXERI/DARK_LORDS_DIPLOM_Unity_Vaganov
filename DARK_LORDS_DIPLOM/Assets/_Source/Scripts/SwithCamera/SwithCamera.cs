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

    public PlayerCommands playerCommands;

    [SyncVar]
    public int PlayersCount = 0;

    // --- Переключение камер ---
    public void SwitchToEnemy()
    {
        // Вызов с клиента или сервера
        if (NetworkServer.active)
        {
            playerCommands.DisambledObjServer(EnemyBtn, false);
            PlayersCount++;
            Debug.Log("SERVER");
        }
        else if (NetworkClient.isConnected)
        {
            playerCommands.DisambledObjClient(EnemyBtn, false);
            PlayersCount++;
            Debug.Log("CLIENT");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        PlayerCanvas.GetComponent<GraphicRaycaster>().enabled = false;

        if (PlayersCount == 2)
        {
            Debug.Log("READY TO PLAY");
        }
    }

    public void SwitchToPlayer()
    {
        // Вызов с клиента или сервера
        if (NetworkServer.active)
        {
            playerCommands.DisambledObjServer(PlayerBtn, false);
            PlayersCount++;
            Debug.Log("SERVER");
        }
        else if (NetworkClient.isConnected)
        {
            playerCommands.DisambledObjClient(PlayerBtn, false);
            PlayersCount++;
            Debug.Log("CLIENT");
        }

        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        EnemyCanvas.GetComponent<GraphicRaycaster>().enabled = false;

        if (PlayersCount == 2)
        {
            Debug.Log("READY TO PLAY");
        }
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