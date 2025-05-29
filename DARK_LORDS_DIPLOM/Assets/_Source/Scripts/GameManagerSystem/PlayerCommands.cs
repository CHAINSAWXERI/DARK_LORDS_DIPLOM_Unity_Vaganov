using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mirror;
using UnityEngine;

public class PlayerCommands : NetworkBehaviour
{
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

    ////////////////////////////

    public void AddIntServer()
    {
        AddIntOnAllClients();
    }

    public void AddIntClient()
    {
        CmdAddInt();
    }

    // Клиент -> Сервер
    [Command]
    public void CmdAddInt()
    {

        AddIntOnAllClients();
    }

    // Сервер -> Клиенты
    [ClientRpc]
    public void RpcAddInt()
    {

    }

    [Server]
    public void AddIntOnAllClients()
    {
        RpcAddInt();
    }
}

/*
public void AddIntServer(int a, int b)
    {
        AddIntOnAllClients(a, b);
    }

    public void AddIntClient(int a, int b)
    {
        CmdAddInt(a, b);
    }

    // Клиент -> Сервер
    [Command]
    public void CmdAddInt(int a, int b)
    {

        AddIntOnAllClients(a, b);
    }

    // Сервер -> Клиенты
    [ClientRpc]
    public void RpcAddInt(int a, int b)
    {
        
    }

    [Server]
    public void AddIntOnAllClients(int a, int b)
    {
        RpcAddInt(a, b);
    }
*/
