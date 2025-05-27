using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Common;
using UnityEngine;

public class PlayerComands : NetworkBehaviour
{
    public void SetActive(GameObject gameObjectWithNetwork, bool Active)
    {
        if (isLocalPlayer)
        {
            Debug.Log("isLocalPlayer");
            SetActiveCommandRpc(gameObjectWithNetwork.GetComponent<NetworkIdentity>(), Active);
        }
    }

    [Command]
    public void SetActiveCommandRpc(NetworkIdentity networkIdentity, bool Active)
    {
        SetActiveRpcCheck(networkIdentity, Active);
    }

    [ClientRpc]
    public void SetActiveRpcCheck(NetworkIdentity networkIdentity, bool Active)
    {
        networkIdentity.gameObject.SetActive(Active);
    }

    ///////////////////////////////////////////


}
/*
*/