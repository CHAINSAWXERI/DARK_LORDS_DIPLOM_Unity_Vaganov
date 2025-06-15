using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyForHostWithSetActive : NetworkBehaviour
{
    public void ShowObjectForHost()
    {
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
