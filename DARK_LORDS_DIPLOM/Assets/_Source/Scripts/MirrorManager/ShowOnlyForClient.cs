using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyForClient : NetworkBehaviour
{
    private void Start()
    {
        // Проверяем что мы на выделенном сервере (не клиент)
        if (isServer)
        {
            Debug.Log("----------------------------------------------------------------------");
            gameObject.SetActive(false);
        }
    }
}
