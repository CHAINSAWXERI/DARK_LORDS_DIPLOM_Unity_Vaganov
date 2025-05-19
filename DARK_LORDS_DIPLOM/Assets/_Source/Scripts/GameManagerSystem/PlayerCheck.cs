using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public StartGameManager sgmanager;

    private void Awake()
    {
        sgmanager = FindObjectOfType<StartGameManager>();
        sgmanager.ConnectedPlayer();
    }
}
