using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mirror;
using UnityEngine;

public class PlayerCommands : NetworkBehaviour
{
    [HideInInspector] public GameManager gameManager;

    public void DisambledObjServer(GameObject Obj, bool state)
    {
        NetworkIdentity nId = Obj.GetComponent<NetworkIdentity>();
        DisableObjOnAllClients(nId.netId, state);
        //Debug.LogError("DisambledObjServer: Object has no NetworkIdentity!");
    }

    public void DisambledObjClient(GameObject Obj, bool state)
    {
        NetworkIdentity nId = Obj.GetComponent<NetworkIdentity>();
        CmdRequestDisableObj(nId.netId, state);
        //Debug.LogError("DisambledObjClient: Object has no NetworkIdentity!");
    }

    // Клиент -> Сервер
    [Command]
    public void CmdRequestDisableObj(uint netId, bool state)
    {
        //Debug.Log("CmdRequestDisableObj called with netId: " + netId);
        DisableObjOnAllClients(netId, state);
    }

    // Сервер -> Клиенты
    [ClientRpc]
    public void RpcDisableObj(uint netId, bool state)
    {
        //Debug.Log("RpcDisableObj called with netId: " + netId);

        NetworkIdentity identity;
        if (NetworkClient.spawned.TryGetValue(netId, out identity))
        {
            identity.gameObject.SetActive(state);
        }
        else
        {
            Debug.LogError("RpcDisableObj: Object with netId " + netId + " not found on client!");
        }
    }

    [Server]
    public void DisableObjOnAllClients(uint netId, bool state)
    {
        //Debug.Log("DisableObjOnAllClients called on server with netId: " + netId);

        NetworkIdentity identity;
        if (NetworkServer.spawned.TryGetValue(netId, out identity))
        {
            RpcDisableObj(netId, state);
            identity.gameObject.SetActive(state);
        }
        else
        {
            Debug.LogError("DisableObjOnAllClients: Object with netId " + netId + " not found on server!");
        }
    }

    //////////////////////////////


    //////////////////////////////
}

/*
    public void RandomServer(int res, int minInclusive, int maxExclusive)
    {
        Debug.Log("RandomServer");
        RandomOnAllClients(res, minInclusive, maxExclusive);
    }

    public void RandomClient(int res, int minInclusive, int maxExclusive)
    {
        Debug.Log("RandomClient");
        CmdRandom(res, minInclusive, maxExclusive);
    }

    // Клиент -> Сервер
    [Command]
    public void CmdRandom(int res, int minInclusive, int maxExclusive)
    {
        Debug.Log("CmdRandom");
        RandomOnAllClients(res, minInclusive, maxExclusive);
    }

    // Сервер -> Клиенты
    [ClientRpc]
    public void RpcRandom(int res, int minInclusive, int maxExclusive)
    {
        Debug.Log("RpcRandom");
        res = Random.Range(minInclusive, maxExclusive);
        gameManager.CoreIdCardToTake = res;
    }

    [Server]
    public void RandomOnAllClients(int res, int minInclusive, int maxExclusive)
    {
        Debug.Log("RandomOnAllClients");
        RpcRandom(res, minInclusive, maxExclusive);
    }


    public void __Server()
    {
        __OnAllClients();
    }

    public void __Client()
    {
        Cmd__();
    }

    // Клиент -> Сервер
    [Command]
    public void Cmd__()
    {

        __OnAllClients();
    }

    // Сервер -> Клиенты
    [ClientRpc]
    public void Rpc__()
    {

    }

    [Server]
    public void __OnAllClients()
    {
        Rpc__();
    }
*/
