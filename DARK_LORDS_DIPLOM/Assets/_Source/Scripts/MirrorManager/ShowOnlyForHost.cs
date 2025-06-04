using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ShowOnlyForHost : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isServer && isClient)
        {
            // Локальный клиент сервера — показываем объект
            gameObject.SetActive(true);
        }
        else
        {
            // Все остальные клиенты — скрываем объект
            gameObject.SetActive(false);
        }
    }
}
