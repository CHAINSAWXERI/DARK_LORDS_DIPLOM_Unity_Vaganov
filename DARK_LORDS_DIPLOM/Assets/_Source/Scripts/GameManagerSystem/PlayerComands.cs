using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Common;
using UnityEngine;

public class PlayerComands : NetworkBehaviour
{
    private StartGameManager startGameManager;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Найти StartGameManager в сцене (должен быть в сцене всегда)
        startGameManager = FindObjectOfType<StartGameManager>();

        if (startGameManager == null)
        {
            Debug.LogError("StartGameManager not found in scene!");
            return;
        }

        // Сообщить серверу, что этот игрок подключился
        CmdNotifyPlayerConnected();
    }

    // Команда вызывается с клиента, выполняется на сервере
    [Command]
    private void CmdNotifyPlayerConnected()
    {
        startGameManager = FindObjectOfType<StartGameManager>();

        if (startGameManager != null)
        {
            startGameManager.IncrementPlayersCount();
        }
        else
        {
            Debug.LogError("StartGameManager reference is null on server!");
        }
    }

    // Пример из вашего кода — включение/отключение объектов
    public void SetActive(GameObject gameObjectWithNetwork, bool active)
    {
        if (!isLocalPlayer) return;

        NetworkIdentity netId = gameObjectWithNetwork.GetComponent<NetworkIdentity>();
        if (netId == null)
        {
            Debug.LogError($"{gameObjectWithNetwork.name} NetworkIdentity is null");
            return;
        }

        CmdSetActive(netId, active);
    }

    [Command]
    private void CmdSetActive(NetworkIdentity netId, bool active)
    {
        RpcSetActive(netId, active);
    }

    [ClientRpc]
    private void RpcSetActive(NetworkIdentity netId, bool active)
    {
        netId.gameObject.SetActive(active);
    }
}

/*
*/