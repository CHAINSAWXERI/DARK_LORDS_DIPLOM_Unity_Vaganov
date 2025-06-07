using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestingSync : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] public int aaaaaaaaaa;

    public void plusone()
    {
        aaaaaaaaaa++;
    }
}
